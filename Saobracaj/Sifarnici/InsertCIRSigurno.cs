using System;
using System.Data;
using System.Data.SqlClient;

namespace Saobracaj.Sifarnici
{
    /// <summary>
    /// Sigurnija zamena za InsertCIR.InsCIR + UpdateCIRPrijem/UpdateCIROtprema.
    /// Sve se radi u JEDNOJ transakciji, greska se ne guta, a ID novog CIR-a se vraca pozivaocu
    /// (nema vise "select Max(ID) from CIR").
    /// Originalni InsertCIR.cs je ostao netaknut.
    /// </summary>
    public class InsertCIRSigurno
    {
        /// <summary>Parametri procedure InsertCIR (isti nazivi kao u InsertCIR.InsCIR).</summary>
        public class CirPodaci
        {
            public int Size, TiKontejnera, MaterijalCelik, MaterijalAlumini, incoming, Pun;
            public double Tezina;
            public string BrKontejnera;
            public int Plomba1, Plomba2;
            public DateTime DatumIn;
            public string Vagon, TruckNo;
            public int Damaged, Ispravan, Prevoz;
            public string Containerresponsible, primedbe, Received, Inspected, Delivery;
            public DateTime Datum;
            public string Korisnik;
            public int Prijem, Dokument;
            public double Duzina, Sirina, Visina;
            public string sPlomba, sPlomba2;
            public int Interni;
            public double Nosivost;
            public string TipRN;
            public int RNID, NalogID, PregledacSkladiste;
        }

        /// <summary>Proverava obavezna polja; vraca poruku greske ili null ako je sve u redu.</summary>
        public static string Proveri(CirPodaci p)
        {
            if (string.IsNullOrWhiteSpace(p.BrKontejnera)) return "Unesite broj kontejnera.";
            if (p.TiKontejnera <= 0) return "Izaberite tip kontejnera.";
            if (p.Tezina < 0) return "Težina ne može biti negativna.";
            if (p.Dokument <= 0) return "CIR mora biti vezan za dokument prijema/otpreme.";
            return null;
        }

        /// <summary>
        /// Snima novi CIR i oznacava izvorni dokument (prijem ili otprema) u istoj transakciji.
        /// Vraca ID novog CIR-a. Baca izuzetak sa originalnom greskom (SqlException) ako nesto ne uspe.
        /// </summary>
        public int SnimiNovi(CirPodaci p)
        {
            string greska = Proveri(p);
            if (greska != null) throw new InvalidOperationException(greska);

            using (var con = new SqlConnection(Saobracaj.Sifarnici.frmLogovanje.connectionString))
            {
                con.Open();
                using (var tran = con.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        using (var cmd = new SqlCommand("InsertCIR", con, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            Dodaj(cmd, p);
                            cmd.ExecuteNonQuery();
                        }

                        // Proc InsertCIR ne vraca ID. Ceo korak je u Serializable transakciji, pa upit
                        // uzima poslednji CIR ovog korisnika za ovaj kontejner i dokument.
                        // (Bolje resenje: u proceduri dodati OUTPUT @NoviID = SCOPE_IDENTITY(), vidi SQL ispod.)
                        int noviId;
                        using (var cmd = new SqlCommand(
                            "SELECT TOP 1 ID FROM CIR WHERE BrKontejnera = @k AND Dokument = @d AND Korisnik = @u ORDER BY ID DESC",
                            con, tran))
                        {
                            cmd.Parameters.AddWithValue("@k", p.BrKontejnera);
                            cmd.Parameters.AddWithValue("@d", p.Dokument);
                            cmd.Parameters.AddWithValue("@u", (object)p.Korisnik ?? DBNull.Value);
                            object r = cmd.ExecuteScalar();
                            if (r == null || r == DBNull.Value)
                                throw new InvalidOperationException("CIR je upisan, ali ID nije pronađen.");
                            noviId = Convert.ToInt32(r);
                        }

                        string proc = p.Prijem == 1 ? "UpdatePrijemKontejneraVozCIR" : "UpdateOtpremaKontejneraCIR";
                        using (var cmd = new SqlCommand(proc, con, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = p.Dokument;
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                        return noviId;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw; // originalna greska, ne generican tekst
                    }
                }
            }
        }

        private static void Dodaj(SqlCommand c, CirPodaci p)
        {
            c.Parameters.Add("@Size", SqlDbType.Int).Value = p.Size;
            c.Parameters.Add("@TiKontejnera", SqlDbType.Int).Value = p.TiKontejnera;
            c.Parameters.Add("@MaterijalCelik", SqlDbType.Int).Value = p.MaterijalCelik;
            c.Parameters.Add("@MaterijalAlumini", SqlDbType.Int).Value = p.MaterijalAlumini;
            c.Parameters.Add("@incoming", SqlDbType.Int).Value = p.incoming;
            c.Parameters.Add("@Pun", SqlDbType.Int).Value = p.Pun;
            c.Parameters.Add("@Tezina", SqlDbType.Float).Value = p.Tezina;
            c.Parameters.Add("@BrKontejnera", SqlDbType.NVarChar, 50).Value = (object)p.BrKontejnera ?? DBNull.Value;
            c.Parameters.Add("@Plomba1", SqlDbType.Int).Value = p.Plomba1;
            c.Parameters.Add("@Plomba2", SqlDbType.Int).Value = p.Plomba2;
            c.Parameters.Add("@DatumIn", SqlDbType.DateTime).Value = p.DatumIn;
            c.Parameters.Add("@Vagon", SqlDbType.NVarChar, 50).Value = (object)p.Vagon ?? DBNull.Value;
            c.Parameters.Add("@TruckNo", SqlDbType.NVarChar, 50).Value = (object)p.TruckNo ?? DBNull.Value;
            c.Parameters.Add("@Damaged", SqlDbType.Int).Value = p.Damaged;
            c.Parameters.Add("@Ispravan", SqlDbType.Int).Value = p.Ispravan;
            c.Parameters.Add("@Prevoz", SqlDbType.Int).Value = p.Prevoz;
            c.Parameters.Add("@Containerresponsible", SqlDbType.NVarChar, 100).Value = (object)p.Containerresponsible ?? DBNull.Value;
            c.Parameters.Add("@primedbe", SqlDbType.NVarChar, -1).Value = (object)p.primedbe ?? DBNull.Value;
            c.Parameters.Add("@Received", SqlDbType.NVarChar, 100).Value = (object)p.Received ?? DBNull.Value;
            c.Parameters.Add("@Inspected", SqlDbType.NVarChar, 100).Value = (object)p.Inspected ?? DBNull.Value;
            c.Parameters.Add("@Delivery", SqlDbType.NVarChar, 100).Value = (object)p.Delivery ?? DBNull.Value;
            c.Parameters.Add("@Datum", SqlDbType.DateTime).Value = p.Datum;
            c.Parameters.Add("@Korisnik", SqlDbType.NVarChar, 50).Value = (object)p.Korisnik ?? DBNull.Value;
            c.Parameters.Add("@Prijem", SqlDbType.Int).Value = p.Prijem;
            c.Parameters.Add("@Dokument", SqlDbType.Int).Value = p.Dokument;
            c.Parameters.Add("@Duzina", SqlDbType.Float).Value = p.Duzina;
            c.Parameters.Add("@Sirina", SqlDbType.Float).Value = p.Sirina;
            c.Parameters.Add("@Visina", SqlDbType.Float).Value = p.Visina;
            c.Parameters.Add("@sPlomba", SqlDbType.NVarChar, 50).Value = (object)p.sPlomba ?? DBNull.Value;
            c.Parameters.Add("@sPlomba2", SqlDbType.NVarChar, 50).Value = (object)p.sPlomba2 ?? DBNull.Value;
            c.Parameters.Add("@Interni", SqlDbType.Int).Value = p.Interni;
            c.Parameters.Add("@Nosivost", SqlDbType.Float).Value = p.Nosivost;
            c.Parameters.Add("@TipRN", SqlDbType.NVarChar, 50).Value = (object)p.TipRN ?? DBNull.Value;
            c.Parameters.Add("@RNID", SqlDbType.Int).Value = p.RNID;
            c.Parameters.Add("@NalogID", SqlDbType.Int).Value = p.NalogID;
            c.Parameters.Add("@PregledacSkladiste", SqlDbType.Int).Value = p.PregledacSkladiste;
        }
    }
}

/* ---------------------------------------------------------------------------------------------
   PREPORUKA ZA BAZU (pokrenuti nakon provere definicije procedure):
   U proceduri InsertCIR dodati na kraj:   SET @NoviID = SCOPE_IDENTITY();
   i u listu parametara:                   @NoviID int = NULL OUTPUT
   Tada se upit "SELECT TOP 1 ID FROM CIR ..." iznad zamenjuje citanjem @NoviID.

   Takodje (da se spreci dupli CIR za isti dokument):
   CREATE UNIQUE INDEX UX_CIR_Dokument ON dbo.CIR (Dokument, Prijem) WHERE Dokument > 0;   -- ako poslovno vazi
   --------------------------------------------------------------------------------------------- */
