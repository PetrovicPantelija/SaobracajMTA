using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Saobracaj.TrackModal.Sifarnici
{
    public enum NivoSkladista
    {
        Zona,
        Lokacija,
        Objekat,
        Polje,
        Pozicija
    }

    // Hijerarhija skladišnog prostora: Zona -> Lokacija (SkladistaGrupa) -> Objekat (SkladisteObjekat) -> Polje (Skladista) -> Pozicija.
    // Pravila zajednička za forme šifarnika: aktivnost u odnosu na roditelja, deaktivacija i brisanje roditelja sa decom,
    // jedinstvena oznaka, lista roditelja i podaci nadređenih nivoa za prikaz.
    public static class HijerarhijaSkladista
    {
        private class Opis
        {
            public string Tabela;          // tabela nivoa
            public string KolonaAktivnosti;
            public string Naziv;           // za poruke: "Lokacija"
            public NivoSkladista? Roditelj;
            public string KolonaRoditelja; // kolona u ovoj tabeli koja pokazuje na roditelja
            public string[][] Deca;        // { tabela, kolona roditelja, aktivnost, naziv u množini }
        }

        private static readonly Dictionary<NivoSkladista, Opis> Nivoi = new Dictionary<NivoSkladista, Opis>
        {
            { NivoSkladista.Zona, new Opis { Tabela = "Zone", KolonaAktivnosti = "Aktivna", Naziv = "Zona",
                Deca = new[] { new[] { "SkladistaGrupa", "ZonaID", "Aktivna", "lokacija" } } } },
            // polja vezana direktno za lokaciju (pre migracije) računaju se kao deca lokacije
            { NivoSkladista.Lokacija, new Opis { Tabela = "SkladistaGrupa", KolonaAktivnosti = "Aktivna", Naziv = "Lokacija",
                Roditelj = NivoSkladista.Zona, KolonaRoditelja = "ZonaID",
                Deca = new[] { new[] { "SkladisteObjekat", "SkladistaGrupaID", "Aktivan", "objekata" },
                               new[] { "Skladista", "GrupaPoljaID", "Aktivan", "polja" } } } },
            { NivoSkladista.Objekat, new Opis { Tabela = "SkladisteObjekat", KolonaAktivnosti = "Aktivan", Naziv = "Objekat",
                Roditelj = NivoSkladista.Lokacija, KolonaRoditelja = "SkladistaGrupaID",
                Deca = new[] { new[] { "Skladista", "SkladisteObjekatID", "Aktivan", "polja" } } } },
            { NivoSkladista.Polje, new Opis { Tabela = "Skladista", KolonaAktivnosti = "Aktivan", Naziv = "Polje",
                Roditelj = NivoSkladista.Objekat, KolonaRoditelja = "SkladisteObjekatID",
                Deca = new[] { new[] { "Pozicija", "Skladiste", "Aktivna", "pozicija" } } } },
            { NivoSkladista.Pozicija, new Opis { Tabela = "Pozicija", KolonaAktivnosti = "Aktivna", Naziv = "Pozicija",
                Roditelj = NivoSkladista.Polje, KolonaRoditelja = "Skladiste",
                Deca = new string[0][] } },
        };

        private static string Konekcija
        {
            get { return Saobracaj.Sifarnici.frmLogovanje.connectionString; }
        }

        // ---------------------------------------------------------------------------------
        // Pristup bazi
        // ---------------------------------------------------------------------------------
        public static DataTable Tabela(string sql, params SqlParameter[] parametri)
        {
            using (var conn = new SqlConnection(Konekcija))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddRange(parametri);
                var tabela = new DataTable();
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(tabela);
                return tabela;
            }
        }

        public static object Skalar(string sql, params SqlParameter[] parametri)
        {
            using (var conn = new SqlConnection(Konekcija))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddRange(parametri);
                conn.Open();
                object rezultat = cmd.ExecuteScalar();
                return rezultat == DBNull.Value ? null : rezultat;
            }
        }

        // Stored procedure u transakciji, kao u klasama Insert*; vraća prvu vrednost rezultata (npr. novi ID)
        public static object Procedura(string naziv, params SqlParameter[] parametri)
        {
            using (var conn = new SqlConnection(Konekcija))
            using (var cmd = new SqlCommand(naziv, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parametri);
                conn.Open();
                using (SqlTransaction tr = conn.BeginTransaction())
                {
                    cmd.Transaction = tr;
                    try
                    {
                        object rezultat = cmd.ExecuteScalar();
                        tr.Commit();
                        return rezultat == DBNull.Value ? null : rezultat;
                    }
                    catch (SqlException ex)
                    {
                        tr.Rollback();
                        throw new Exception("Neuspešan upis u bazu: " + ex.Message);
                    }
                }
            }
        }

        public static SqlParameter Param(string naziv, object vrednost)
        {
            return new SqlParameter(naziv, vrednost ?? DBNull.Value);
        }

        public static SqlParameter ParamTekst(string naziv, string vrednost, int duzina)
        {
            var p = new SqlParameter(naziv, SqlDbType.NVarChar, duzina);
            string tekst = (vrednost ?? "").Trim();
            p.Value = tekst.Length == 0 ? (object)DBNull.Value : tekst;
            return p;
        }

        public static SqlParameter ParamKoordinata(string naziv, decimal? vrednost)
        {
            var p = new SqlParameter(naziv, SqlDbType.Decimal) { Precision = 9, Scale = 6 };
            p.Value = vrednost.HasValue ? (object)vrednost.Value : DBNull.Value;
            return p;
        }

        // ID zapisa upisanog postojećom procedurom koja ne vraća ID (oznaka je jedinstvena)
        public static int IdPoOznaci(NivoSkladista nivo, string oznaka)
        {
            object id = Skalar("SELECT MAX(ID) FROM dbo." + Nivoi[nivo].Tabela + " WHERE Oznaka = @Oznaka",
                Param("@Oznaka", (oznaka ?? "").Trim()));
            return id == null ? 0 : Convert.ToInt32(id);
        }

        // ---------------------------------------------------------------------------------
        // Pravila
        // ---------------------------------------------------------------------------------
        // Oznaka mora biti jedinstvena u tabeli nivoa; id = 0 za novi zapis
        public static string ProveriOznaku(NivoSkladista nivo, int id, string oznaka)
        {
            Opis o = Nivoi[nivo];
            if (string.IsNullOrWhiteSpace(oznaka))
                return "Unesite oznaku.";

            object broj = Skalar("SELECT COUNT(*) FROM dbo." + o.Tabela + " WHERE LTRIM(RTRIM(Oznaka)) = @Oznaka AND ISNULL(ID, 0) <> @ID",
                Param("@Oznaka", oznaka.Trim()), Param("@ID", id));
            return Convert.ToInt32(broj) > 0 ? "Oznaka '" + oznaka.Trim() + "' već postoji." : null;
        }

        // Aktivan zapis mora imati aktivnog roditelja. Deaktivacija je uvek dozvoljena: prenosi se na sve
        // podređene zapise (PostaviAktivnost).
        public static string ProveriAktivnost(NivoSkladista nivo, int id, int roditeljId, bool aktivan)
        {
            Opis o = Nivoi[nivo];
            if (aktivan && o.Roditelj.HasValue && roditeljId > 0 && !JeAktivan(o.Roditelj.Value, roditeljId))
                return "Zapis ne može biti aktivan jer nadređeni zapis (" + Nivoi[o.Roditelj.Value].Naziv.ToLower()
                    + ") nije aktivan.";
            return null;
        }

        // Aktivnost u dubinu: zapis i svi njegovi podređeni zapisi (Zona -> Lokacije -> Objekti -> Polja -> Pozicije)
        // dobijaju istu aktivnost. Vraća broj podređenih zapisa kojima je aktivnost promenjena.
        public static int PostaviAktivnost(NivoSkladista nivo, int id, bool aktivan)
        {
            var broj = new SqlParameter("@BrojPodredjenih", SqlDbType.Int) { Direction = ParameterDirection.Output };
            Procedura("AktivnostHijerarhijaSkladista",
                Param("@Nivo", nivo.ToString()), Param("@ID", id), Param("@Aktivan", aktivan ? 1 : 0), broj);
            return CeoBroj(broj.Value);
        }

        // Dugmad Aktivne / Neaktivne: izabrani red grida (kolone ID i Oznaka) i njegovi podređeni zapisi;
        // red čiji roditelj nije aktivan ne može se aktivirati. Prikazuje rezultat.
        public static void PromeniAktivnostIzabranih(Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl grid,
            NivoSkladista nivo, bool aktivan, string naslov)
        {
            int id = SifarnikGrid.IzabraniId(grid);
            if (id == 0)
            {
                MessageBox.Show("Izaberite zapis u tabeli.", naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sifra = Convert.ToString(SifarnikGrid.Vrednost(grid, "Oznaka"));
            try
            {
                string greska = ProveriPromenuAktivnosti(nivo, id, aktivan);
                if (greska != null)
                {
                    MessageBox.Show(sifra + ": " + greska, naslov, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int podredjenih = PostaviAktivnost(nivo, id, aktivan);
                MessageBox.Show(sifra + ": " + (aktivan ? "aktivirano" : "deaktivirano")
                    + (podredjenih > 0 ? ", podređenih zapisa: " + podredjenih : "") + ".",
                    naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, naslov, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Pri snimanju: ako je zapisu promenjena aktivnost, ista aktivnost se prenosi na podređene zapise.
        // bioAktivan = aktivnost pre snimanja (JeAktivan pozvan pre upisa).
        public static void PrenesiAktivnostPosleSnimanja(NivoSkladista nivo, int id, bool bioAktivan, bool aktivan, string naslov)
        {
            if (id <= 0 || bioAktivan == aktivan)
                return;
            int podredjenih = PostaviAktivnost(nivo, id, aktivan);
            if (podredjenih > 0)
                MessageBox.Show((aktivan ? "Aktivirano" : "Deaktivirano") + " i podređenih zapisa: " + podredjenih + ".",
                    naslov, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Zapis koji ima decu (aktivnu ili neaktivnu) ne briše se
        public static string ProveriBrisanje(NivoSkladista nivo, int id)
        {
            Opis o = Nivoi[nivo];
            var poruke = new List<string>();
            foreach (string[] deca in o.Deca)
            {
                int broj = BrojDece(deca, id, false);
                if (broj > 0)
                    poruke.Add(deca[3] + ": " + broj);
            }
            return poruke.Count == 0 ? null
                : o.Naziv + " ima podređene zapise (" + string.Join(", ", poruke) + ") i ne može se obrisati.";
        }

        // Za dugmad Aktivne / Neaktivne: proverava izabrani zapis i vraća poruku ili null
        public static string ProveriPromenuAktivnosti(NivoSkladista nivo, int id, bool aktivan)
        {
            Opis o = Nivoi[nivo];
            int roditeljId = 0;
            if (o.Roditelj.HasValue)
            {
                object r = Skalar("SELECT " + o.KolonaRoditelja + " FROM dbo." + o.Tabela + " WHERE ID = @ID", Param("@ID", id));
                roditeljId = r == null ? 0 : Convert.ToInt32(r);
            }
            string greska = ProveriAktivnost(nivo, id, roditeljId, aktivan);
            if (greska == null && nivo == NivoSkladista.Pozicija && aktivan)
            {
                object vrsta = Skalar("SELECT VrstaPozicija FROM dbo.Pozicija WHERE ID = @ID", Param("@ID", id));
                greska = ProveriVrstuPozicije(roditeljId, CeoBroj(vrsta));
            }
            return greska;
        }

        // ---------------------------------------------------------------------------------
        // Vrsta pozicija: polje radi sa kontejnerskim ili paletnim pozicijama; aktivne su samo pozicije njegove vrste
        // ---------------------------------------------------------------------------------
        public const int KontejnerskePozicije = 1;
        public const int PaletnePozicije = 2;

        public static DataTable VrstePozicija()
        {
            var t = new DataTable();
            t.Columns.Add("ID", typeof(int));
            t.Columns.Add("Naziv", typeof(string));
            t.Rows.Add(KontejnerskePozicije, "Kontejnerske pozicije");
            t.Rows.Add(PaletnePozicije, "Paletne pozicije");
            return t;
        }

        public static string NazivVrstePozicija(int vrsta)
        {
            return vrsta == PaletnePozicije ? "paletne" : "kontejnerske";
        }

        public static int VrstaPozicijaPolja(int poljeId)
        {
            object v = Skalar("SELECT VrstaPozicija FROM dbo.Skladista WHERE ID = @ID", Param("@ID", poljeId));
            return v == null ? KontejnerskePozicije : CeoBroj(v);
        }

        // Aktivna pozicija mora biti iste vrste kao polje
        public static string ProveriVrstuPozicije(int poljeId, int vrstaPozicije)
        {
            if (poljeId <= 0)
                return null;
            int vrstaPolja = VrstaPozicijaPolja(poljeId);
            return vrstaPolja == vrstaPozicije ? null
                : "Pozicija ne može biti aktivna: polje radi sa " + NazivVrstePozicija(vrstaPolja) + " pozicijama, a pozicija je "
                  + (vrstaPozicije == PaletnePozicije ? "paletna." : "kontejnerska.");
        }

        // Upisuje vrstu pozicija polja; ako je promenjena, pozicije nove vrste postaju aktivne (ako je polje aktivno),
        // a ostale neaktivne. Vraća broj pozicija kojima je promenjena aktivnost.
        public static int PostaviVrstuPozicijaPolja(int poljeId, int vrsta)
        {
            var broj = new SqlParameter("@BrojPozicija", SqlDbType.Int) { Direction = ParameterDirection.Output };
            Procedura("UpdateSkladistaVrstaPozicija", Param("@ID", poljeId), Param("@VrstaPozicija", vrsta), broj);
            return CeoBroj(broj.Value);
        }

        public static bool JeAktivan(NivoSkladista nivo, int id)
        {
            Opis o = Nivoi[nivo];
            object a = Skalar("SELECT ISNULL(" + o.KolonaAktivnosti + ", 0) FROM dbo." + o.Tabela + " WHERE ID = @ID", Param("@ID", id));
            return a != null && Convert.ToInt32(a) > 0;
        }

        private static int BrojDece(string[] deca, int id, bool samoAktivna)
        {
            string sql = "SELECT COUNT(*) FROM dbo." + deca[0] + " WHERE " + deca[1] + " = @ID";
            if (samoAktivna)
                sql += " AND ISNULL(" + deca[2] + ", 0) > 0";
            return Convert.ToInt32(Skalar(sql, Param("@ID", id)));
        }

        // ---------------------------------------------------------------------------------
        // Lista roditelja: "Oznaka – Naziv", samo aktivni; zapis koji se menja zadržava i neaktivnog roditelja
        // ---------------------------------------------------------------------------------
        public static DataView ListaRoditelja(NivoSkladista roditelj, string dodatniUslov = null)
        {
            Opis o = Nivoi[roditelj];
            string sql = "SELECT ID, ISNULL(Oznaka, '') + ' – ' + ISNULL(Naziv, '') AS Prikaz, "
                + "CASE WHEN ISNULL(" + o.KolonaAktivnosti + ", 0) > 0 THEN 1 ELSE 0 END AS Aktivan"
                + (dodatniUslov == null ? ", 1 AS Dozvoljen" : ", CASE WHEN " + dodatniUslov + " THEN 1 ELSE 0 END AS Dozvoljen")
                + " FROM dbo." + o.Tabela + " WHERE ID IS NOT NULL ORDER BY Oznaka";
            var pogled = Tabela(sql).DefaultView;
            PostaviTrenutnog(pogled, 0);
            return pogled;
        }

        public static void PostaviTrenutnog(DataView pogled, int trenutniId)
        {
            pogled.RowFilter = "(Aktivan = 1 AND Dozvoljen = 1) OR ID = " + trenutniId.ToString(CultureInfo.InvariantCulture);
        }

        public static void PoveziListu(ComboBox cbo, DataView pogled)
        {
            cbo.DataSource = pogled;
            cbo.DisplayMember = "Prikaz";
            cbo.ValueMember = "ID";
            cbo.SelectedIndex = -1;
        }

        // Vrednost iz liste ili 0 kada ništa nije izabrano
        public static int IzabraniId(ComboBox cbo)
        {
            if (cbo.SelectedValue == null || cbo.SelectedValue == DBNull.Value)
                return 0;
            int id;
            return int.TryParse(cbo.SelectedValue.ToString(), out id) ? id : 0;
        }

        // ---------------------------------------------------------------------------------
        // Podaci nadređenih nivoa za read-only prikaz ispod liste roditelja
        // ---------------------------------------------------------------------------------
        public static string OpisNadredjenih(NivoSkladista nivo, int roditeljId)
        {
            if (roditeljId <= 0)
                return "";

            string od;
            switch (nivo)
            {
                case NivoSkladista.Lokacija:
                    od = " FROM dbo.Zone z WHERE z.ID = @ID";
                    return Formatiraj(nivo, Tabela("SELECT " + KoloneZone + od, Param("@ID", roditeljId)));
                case NivoSkladista.Objekat:
                    od = " FROM dbo.SkladistaGrupa g LEFT JOIN dbo.Zone z ON z.ID = g.ZonaID"
                       + " LEFT JOIN dbo.Carinarnice c ON c.ID = g.CarinskaIspostavaID WHERE g.ID = @ID";
                    return Formatiraj(nivo, Tabela("SELECT " + KoloneZone + ", " + KoloneLokacije + od, Param("@ID", roditeljId)));
                case NivoSkladista.Polje:
                    od = " FROM dbo.SkladisteObjekat o LEFT JOIN dbo.TipSkladista ts ON ts.ID = o.TipSkladistaID"
                       + " LEFT JOIN dbo.SkladistaGrupa g ON g.ID = o.SkladistaGrupaID LEFT JOIN dbo.Zone z ON z.ID = g.ZonaID"
                       + " LEFT JOIN dbo.Carinarnice c ON c.ID = g.CarinskaIspostavaID WHERE o.ID = @ID";
                    return Formatiraj(nivo, Tabela("SELECT " + KoloneZone + ", " + KoloneLokacije + ", " + KoloneObjekta + od,
                        Param("@ID", roditeljId)));
                case NivoSkladista.Pozicija:
                    od = " FROM dbo.Skladista s LEFT JOIN dbo.SkladisteObjekat o ON o.ID = s.SkladisteObjekatID"
                       + " LEFT JOIN dbo.TipSkladista ts ON ts.ID = o.TipSkladistaID"
                       + " LEFT JOIN dbo.SkladistaGrupa g ON g.ID = ISNULL(o.SkladistaGrupaID, s.GrupaPoljaID)"
                       + " LEFT JOIN dbo.Zone z ON z.ID = g.ZonaID LEFT JOIN dbo.Carinarnice c ON c.ID = g.CarinskaIspostavaID"
                       + " WHERE s.ID = @ID";
                    return Formatiraj(nivo, Tabela("SELECT " + KoloneZone + ", " + KoloneLokacije + ", " + KoloneObjekta
                        + ", s.Oznaka AS PoOznaka, s.Naziv AS PoNaziv, s.Aktivan AS PoAktivan" + od, Param("@ID", roditeljId)));
                default:
                    return "";
            }
        }

        private const string KoloneZone =
            "z.Oznaka AS ZOznaka, z.Naziv AS ZNaziv, z.Aktivna AS ZAktivna, (SELECT Naziv FROM dbo.TipZone WHERE ID = z.TipZoneID) AS ZTip";
        private const string KoloneLokacije =
            "g.Oznaka AS LOznaka, g.Naziv AS LNaziv, g.Aktivna AS LAktivna, g.Drzava, g.Grad, g.Adresa, g.PostanskiBroj, "
            + "g.Latitude AS LLat, g.Longitude AS LLon, g.CarinskoSkladiste, c.Naziv AS Ispostava";
        private const string KoloneObjekta =
            "o.Oznaka AS OOznaka, o.Naziv AS ONaziv, o.Aktivan AS OAktivan, ts.Naziv AS OTip";

        private static string Formatiraj(NivoSkladista nivo, DataTable t)
        {
            if (t.Rows.Count == 0)
                return "";
            DataRow r = t.Rows[0];
            var sb = new StringBuilder();

            sb.Append("Zona: ").Append(Spoj(r["ZOznaka"], r["ZNaziv"]));
            if (nivo == NivoSkladista.Lokacija)
                sb.Append(", ").Append(Tekst(r["ZTip"]));
            sb.Append(", ").Append(Aktivnost(r["ZAktivna"]));

            if (nivo == NivoSkladista.Lokacija)
                return sb.ToString();

            sb.AppendLine();
            sb.Append("Lokacija: ").Append(Spoj(r["LOznaka"], r["LNaziv"])).Append(", ").Append(Aktivnost(r["LAktivna"]));
            sb.AppendLine();
            sb.Append("   ").Append(Spoj(r["Drzava"], r["Grad"], ", ")).Append(", ").Append(Tekst(r["Adresa"]));
            if (nivo == NivoSkladista.Objekat)
            {
                sb.Append(", ").Append(Tekst(r["PostanskiBroj"]));
                sb.AppendLine();
                sb.Append("   Lat/Lon: ").Append(Tekst(r["LLat"])).Append(" / ").Append(Tekst(r["LLon"]));
            }
            sb.AppendLine();
            bool carinsko = r["CarinskoSkladiste"] != DBNull.Value && Convert.ToInt32(r["CarinskoSkladiste"]) == 1;
            sb.Append("Carinsko skladište: ").Append(carinsko ? "DA, " + Tekst(r["Ispostava"]) : "NE");

            if (nivo == NivoSkladista.Objekat)
                return sb.ToString();

            sb.AppendLine();
            sb.Append("Objekat: ").Append(Spoj(r["OOznaka"], r["ONaziv"])).Append(", ").Append(Tekst(r["OTip"]))
              .Append(", ").Append(Aktivnost(r["OAktivan"]));

            if (nivo == NivoSkladista.Polje)
                return sb.ToString();

            sb.AppendLine();
            sb.Append("Polje: ").Append(Spoj(r["PoOznaka"], r["PoNaziv"])).Append(", ").Append(Aktivnost(r["PoAktivan"]));
            return sb.ToString();
        }

        private static string Tekst(object v)
        {
            return v == null || v == DBNull.Value || v.ToString().Trim().Length == 0 ? "-" : v.ToString().Trim();
        }

        private static string Spoj(object a, object b, string razdvoji = " – ")
        {
            return Tekst(a) + razdvoji + Tekst(b);
        }

        private static string Aktivnost(object v)
        {
            return v != null && v != DBNull.Value && Convert.ToInt32(v) > 0 ? "aktivno" : "neaktivno";
        }

        // ---------------------------------------------------------------------------------
        // Unos koordinata: prazno = null; decimalni zarez ili tačka
        // ---------------------------------------------------------------------------------
        public static bool ProcitajKoordinatu(string tekst, decimal min, decimal max, out decimal? vrednost)
        {
            vrednost = null;
            string t = (tekst ?? "").Trim().Replace(',', '.');
            if (t.Length == 0)
                return true;
            decimal d;
            if (!decimal.TryParse(t, NumberStyles.Float, CultureInfo.InvariantCulture, out d) || d < min || d > max)
                return false;
            vrednost = Math.Round(d, 6);
            return true;
        }

        public static string KoordinataTekst(object v)
        {
            return v == null || v == DBNull.Value ? "" : Convert.ToDecimal(v).ToString("0.######", CultureInfo.InvariantCulture);
        }

        public static int CeoBroj(object v)
        {
            int i;
            return v == null || v == DBNull.Value || !int.TryParse(v.ToString(), out i) ? 0 : i;
        }
    }
}
