using System;
using System.Data;
using System.Data.SqlClient;

namespace Saobracaj.RNI
{
    public enum TerminalPrivTip
    {
        Tekst,
        Datum,
        TekstZaPotvrdu   // tekst samo u poljima ispod grida (VOZAČ); ne prikazuje se u gridu, ne izvozi se i ne menja grupno
    }

    public class TerminalPrivPolje
    {
        public string Kolona;       // naziv kolone u tabeli TerminalPriv
        public string Parametar;    // naziv parametra stored procedure
        public TerminalPrivTip Tip;
        public int Velicina;        // maksimalan broj karaktera (za tekst)
        public string[] Dozvoljeno; // dozvoljene vrednosti (null = bez ogranicenja)
        public bool TacnaDuzina;    // uneta vrednost mora imati tačno Velicina karaktera (prazno je dozvoljeno)

        public TerminalPrivPolje(string kolona, string parametar, TerminalPrivTip tip, int velicina, string[] dozvoljeno, bool tacnaDuzina = false)
        {
            Kolona = kolona;
            Parametar = parametar;
            Tip = tip;
            Velicina = velicina;
            Dozvoljeno = dozvoljeno;
            TacnaDuzina = tacnaDuzina;
        }
    }

    class insertTerminalPriv
    {
        public const int DuzinaKontejnera = 11;

        public static readonly string[] Statusi = { "PRAZAN", "PUN", "?", "RAZVOZ", "Blanks", "PRETOVAR", "U RAZVOZU", "UTOVAR" };
        public static readonly string[] VrsteCnt =
        {
            "20", "20' DB", "20' HC", "20' OT", "20' TC", "40", "40' DB", "40' HC", "40' OT", "40' FLAT", "40' RF", "45' HC"
        };
        public static readonly string[] Brodari =
        {
            "CMA", "MAERSK", "COSCO", "MSC", "HAPAG", "OOCL", "HMM", "EVERGREEN", "FLEXBOX", "YANG MING", "LEGET", "ONE"
        };
        public static readonly string[] Stanja ={ "DOBAR", "LOŠ", "FOOD GRADE", "OŠTEĆEN", "FLEXI" };
        public static readonly string[] GateInGateOut =
        {
            "GATE IN E", "GATE IN F", "GATE OUT E", "GATE OUT F", "GATE IN E/REPOZICIJA",
            "PRIJAVITI GATE IN E", "NE ŠALJEMO POKRET BRODARU", "REUSE", "REPOZICIJA", "PRIVREMENO",
            "NE PRIJAVLJUJEMO POKRETE", "PRIJAVITI SVE POKRETE"
        };

        // Sva polja tabele osim ID, redosledom iz tabele
        public static readonly TerminalPrivPolje[] Polja =
        {
            new TerminalPrivPolje("KONTEJNER", "@KONTEJNER", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("STATUS", "@STATUS", TerminalPrivTip.Tekst, 30, Statusi),
            new TerminalPrivPolje("POZICIJA", "@POZICIJA", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("VRSTA", "@VRSTA", TerminalPrivTip.Tekst, 30, VrsteCnt),
            new TerminalPrivPolje("BRODAR", "@BRODAR", TerminalPrivTip.Tekst, 30, Brodari),
            new TerminalPrivPolje("NALOGODAVAC/UVOZ", "@NALOGODAVAC_UVOZ", TerminalPrivTip.Tekst, 50, null),
            new TerminalPrivPolje("POSTUPAK/UVOZ", "@POSTUPAK_UVOZ", TerminalPrivTip.Tekst, 50, null),
            new TerminalPrivPolje("UVOZNIK", "@UVOZNIK", TerminalPrivTip.Tekst, 50, null),
            new TerminalPrivPolje("BL/UVOZ", "@BL_UVOZ", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("PLOMBA_UVOZ", "@PLOMBA_UVOZ", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("VOZ/kamion", "@VOZ_KAMION", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("STANJE", "@STANJE", TerminalPrivTip.Tekst, 30, Stanja),
            new TerminalPrivPolje("GATE_IN_E/F", "@GATE_IN_E_F", TerminalPrivTip.Datum, 0, null),
            new TerminalPrivPolje("PREUZIMANJE_PUNOG/Razvoz", "@PREUZIMANJE_PUNOG_RAZVOZ", TerminalPrivTip.Datum, 0, null),
            new TerminalPrivPolje("VRAĆANJE_PRAZNOG/iz_Razvoza", "@VRACANJE_PRAZNOG_IZ_RAZVOZA", TerminalPrivTip.Datum, 0, null),
            new TerminalPrivPolje("Konačni_GATE_OUT", "@KONACNI_GATE_OUT", TerminalPrivTip.Datum, 0, null),
            new TerminalPrivPolje("BOOKING/IZVOZ", "@BOOKING_IZVOZ", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("KLIJENT/IZVOZ", "@KLIJENT_IZVOZ", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("GATE_OUT_EMPTY/Utovar", "@GATE_OUT_EMPTY_UTOVAR", TerminalPrivTip.Datum, 0, null),
            new TerminalPrivPolje("GATE_IN_FULL/sa_Utovara", "@GATE_IN_FULL_SA_UTOVARA", TerminalPrivTip.Datum, 0, null),
            new TerminalPrivPolje("GATE_IN/_GATE_OUT", "@GATE_IN_GATE_OUT", TerminalPrivTip.Tekst, 50, GateInGateOut),
            new TerminalPrivPolje("L/R", "@L_R", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("TARA", "@TARA", TerminalPrivTip.Tekst, 4, null, true),                          // kolona je nvarchar(30), unos je tačno 4 karaktera
            new TerminalPrivPolje("MAX_NOSIVOST_CNT", "@MAX_NOSIVOST_CNT", TerminalPrivTip.Tekst, 5, null, true),  // kolona je nvarchar(30), unos je tačno 5 karaktera
            new TerminalPrivPolje("VOZILO/PREUZIMANJE", "@VOZILO_PREUZIMANJE", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("PLOMBA/IZVOZ", "@PLOMBA_IZVOZ", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("NAPOMENA", "@NAPOMENA", TerminalPrivTip.Tekst, 300, null),
            new TerminalPrivPolje("OPIS", "@OPIS", TerminalPrivTip.Tekst, 300, null),
            new TerminalPrivPolje("OTPREMA", "@OTPREMA", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("POSLATE SLIKE", "@POSLATE_SLIKE", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("Prevoznik", "@PREVOZNIK", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("BELEŠKA", "@BELESKA", TerminalPrivTip.Tekst, 300, null),
            new TerminalPrivPolje("VOZAČ", "@VOZAC", TerminalPrivTip.TekstZaPotvrdu, 50, null),   // ime vozača, za potvrdu (TerminalPriv_VozacTekst_Vracanje_2026_11.sql)
        };

        // Vraca ID novog zapisa. zapisiMaersk = true samo za dugme 'Sačuvaj novi': procedura tada za Maersk
        // kontejner upisuje pokrete u TerminalPrivMaersk za popunjene datume
        public int InsTerminalPriv(DataRow red, bool zapisiMaersk = false)
        {
            int brojMaersk, brojMSC, brojCMA, brojOOCL, brojHMM, brojHAPAG, brojFLEXBOX, brojCOSCO;
            return InsTerminalPriv(red, zapisiMaersk, out brojMaersk, out brojMSC, out brojCMA, out brojOOCL, out brojHMM, out brojHAPAG, out brojFLEXBOX, out brojCOSCO);
        }

        // brojMaersk / brojMSC / brojCMA / brojOOCL / brojHMM / brojHAPAG / brojFLEXBOX / brojCOSCO = broj pokreta upisanih u TerminalPrivMaersk /
        // TerminalPrivMSC / TerminalPrivCMA / TerminalPrivOOCL / TerminalPrivHMM / TerminalPrivHAPAG / TerminalPrivFLEXBOX / TerminalPrivCOSCO
        public int InsTerminalPriv(DataRow red, bool zapisiMaersk, out int brojMaersk, out int brojMSC, out int brojCMA, out int brojOOCL, out int brojHMM, out int brojHAPAG, out int brojFLEXBOX, out int brojCOSCO)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "insTerminalPriv";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter maersk = new SqlParameter();
            maersk.ParameterName = "@ZapisiMaersk";
            maersk.SqlDbType = SqlDbType.Bit;
            maersk.Direction = ParameterDirection.Input;
            maersk.Value = zapisiMaersk;
            myCommand.Parameters.Add(maersk);

            SqlParameter broj = DodajParametarBroj(myCommand, "@BrojMaersk");
            SqlParameter brojM = DodajParametarBroj(myCommand, "@BrojMSC");
            SqlParameter brojC = DodajParametarBroj(myCommand, "@BrojCMA");
            SqlParameter brojO = DodajParametarBroj(myCommand, "@BrojOOCL");
            SqlParameter brojH = DodajParametarBroj(myCommand, "@BrojHMM");
            SqlParameter brojHA = DodajParametarBroj(myCommand, "@BrojHAPAG");
            SqlParameter brojF = DodajParametarBroj(myCommand, "@BrojFLEXBOX");
            SqlParameter brojCo = DodajParametarBroj(myCommand, "@BrojCOSCO");

            DodajParametreZaPolja(myCommand, red);

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            try
            {
                object rezultat = myCommand.ExecuteScalar();
                myTransaction.Commit();
                brojMaersk = Broj(broj);
                brojMSC = Broj(brojM);
                brojCMA = Broj(brojC);
                brojOOCL = Broj(brojO);
                brojHMM = Broj(brojH);
                brojHAPAG = Broj(brojHA);
                brojFLEXBOX = Broj(brojF);
                brojCOSCO = Broj(brojCo);
                return Convert.ToInt32(rezultat);
            }
            catch (SqlException ex)
            {
                myTransaction.Rollback();
                throw new Exception("Neuspešan upis u TerminalPriv: " + ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }

        // zapisiMaersk = true za izmenu dugmetom 'Promeni' i 'Sačuvaj izmene': procedura tada za Maersk kontejner upisuje
        // pokrete u TerminalPrivMaersk za datume koji su prešli iz praznog u popunjen
        public void UpdTerminalPriv(DataRow red, bool zapisiMaersk = false)
        {
            int brojMaersk, brojMSC, brojCMA, brojOOCL, brojHMM, brojHAPAG, brojFLEXBOX, brojCOSCO;
            UpdTerminalPriv(red, zapisiMaersk, out brojMaersk, out brojMSC, out brojCMA, out brojOOCL, out brojHMM, out brojHAPAG, out brojFLEXBOX, out brojCOSCO);
        }

        // brojMaersk / brojMSC / brojCMA / brojOOCL / brojHMM / brojHAPAG / brojFLEXBOX / brojCOSCO = broj pokreta upisanih u TerminalPrivMaersk /
        // TerminalPrivMSC / TerminalPrivCMA / TerminalPrivOOCL / TerminalPrivHMM / TerminalPrivHAPAG / TerminalPrivFLEXBOX / TerminalPrivCOSCO
        public void UpdTerminalPriv(DataRow red, bool zapisiMaersk, out int brojMaersk, out int brojMSC, out int brojCMA, out int brojOOCL, out int brojHMM, out int brojHAPAG, out int brojFLEXBOX, out int brojCOSCO)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "updTerminalPriv";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter maersk = new SqlParameter();
            maersk.ParameterName = "@ZapisiMaersk";
            maersk.SqlDbType = SqlDbType.Bit;
            maersk.Direction = ParameterDirection.Input;
            maersk.Value = zapisiMaersk;
            myCommand.Parameters.Add(maersk);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Convert.ToInt32(red["ID"]);
            myCommand.Parameters.Add(parameter);

            SqlParameter broj = DodajParametarBroj(myCommand, "@BrojMaersk");
            SqlParameter brojM = DodajParametarBroj(myCommand, "@BrojMSC");
            SqlParameter brojC = DodajParametarBroj(myCommand, "@BrojCMA");
            SqlParameter brojO = DodajParametarBroj(myCommand, "@BrojOOCL");
            SqlParameter brojH = DodajParametarBroj(myCommand, "@BrojHMM");
            SqlParameter brojHA = DodajParametarBroj(myCommand, "@BrojHAPAG");
            SqlParameter brojF = DodajParametarBroj(myCommand, "@BrojFLEXBOX");
            SqlParameter brojCo = DodajParametarBroj(myCommand, "@BrojCOSCO");

            DodajParametreZaPolja(myCommand, red);

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                brojMaersk = Broj(broj);
                brojMSC = Broj(brojM);
                brojCMA = Broj(brojC);
                brojOOCL = Broj(brojO);
                brojHMM = Broj(brojH);
                brojHAPAG = Broj(brojHA);
                brojFLEXBOX = Broj(brojF);
                brojCOSCO = Broj(brojCo);
            }
            catch (SqlException ex)
            {
                myTransaction.Rollback();
                throw new Exception("Neuspešna izmena u TerminalPriv: " + ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Menja samo polje POSLATE SLIKE (broj slika u folderu zapisa)
        public void UpdTerminalPrivPoslateSlike(int ID, int brojSlika)
        {
            UpdPoslateSlike("updTerminalPrivPoslateSlike", ID, brojSlika);
        }

        // Isto za zapis u arhivi (TerminalPrivArhiv)
        public void UpdTerminalPrivArhivPoslateSlike(int ID, int brojSlika)
        {
            UpdPoslateSlike("updTerminalPrivArhivPoslateSlike", ID, brojSlika);
        }

        private void UpdPoslateSlike(string procedura, int ID, int brojSlika)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = procedura;
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@POSLATE_SLIKE";
            parameter1.SqlDbType = SqlDbType.NVarChar;
            parameter1.Size = Polje("POSLATE SLIKE").Velicina;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = brojSlika.ToString();
            myCommand.Parameters.Add(parameter1);

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
            }
            catch (SqlException ex)
            {
                myTransaction.Rollback();
                throw new Exception("Neuspešna izmena broja slika: " + ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Prebacuje sve zapise iz TerminalPriv kod kojih Konacni_GATE_OUT nije NULL u TerminalPrivArhiv i brise ih
        // iz TerminalPriv (procedura InsertTerminalPrivArhiv). Vraca broj arhiviranih zapisa.
        public int InsTerminalPrivArhiv()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertTerminalPrivArhiv";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            try
            {
                object rezultat = myCommand.ExecuteScalar();
                myTransaction.Commit();
                return Convert.ToInt32(rezultat);
            }
            catch (SqlException ex)
            {
                myTransaction.Rollback();
                throw new Exception("Neuspešno arhiviranje: " + ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Šta će vraćanje zapisa iz arhive uraditi, bez izmena u bazi (procedura VratiTerminalPrivIzArhive, @Proveri = 1):
        // Tables[0] = Kontejner, KonacniGateOut, NadjenLog, UTerminalu, BrojPokreta, DatumIzmene, BrojIzmena; Tables[1] = pokreti
        // koji se brišu iz izveštaja brodaru (Izvestaj, Pokret, DatumPokreta, Poslat, DatumIzvoza, Polje); Tables[2] = polja koja
        // se vraćaju (Polje, JeDatum, Sada, Postaje; datum kao tekst 2026-11-23 12:51:00, NULL = prazno)
        public DataSet ProveriVracanjeIzArhive(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            using (var myConnection = new SqlConnection(s_connection))
            using (var myCommand = new SqlCommand("VratiTerminalPrivIzArhive", myConnection))
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                myCommand.Parameters.Add("@Proveri", SqlDbType.Bit).Value = true;

                var rezultat = new DataSet();
                try
                {
                    new SqlDataAdapter(myCommand).Fill(rezultat);
                }
                catch (SqlException ex)
                {
                    throw new Exception("Neuspešna provera arhiviranog zapisa: " + ex.Message);
                }
                return rezultat;
            }
        }

        // Vraća zapis iz TerminalPrivArhiv u TerminalPriv (isti ID) i poništava poslednju izmenu zapisa (kao Undo): polja se vraćaju
        // na vrednost pre nje, brišu se pokreti koje je upisala u izveštaj brodaru i ona iz loga; Konačni_GATE_OUT se uvek briše.
        // Procedura ima svoju transakciju.
        public void VratiIzArhive(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            using (var myConnection = new SqlConnection(s_connection))
            using (var myCommand = new SqlCommand("VratiTerminalPrivIzArhive", myConnection))
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                myCommand.Parameters.Add("@Proveri", SqlDbType.Bit).Value = false;

                try
                {
                    myConnection.Open();
                    myCommand.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Neuspešno vraćanje iz arhive: " + ex.Message);
                }
            }
        }

        public void DelTerminalPriv(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "delTerminalPriv";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
            }
            catch (SqlException ex)
            {
                myTransaction.Rollback();
                throw new Exception("Brisanje neuspešno: " + ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Uvoz iz Excel-a: redovi su nizovi od 7 vrednosti, redom kao kolone ispod.
        // Svi redovi se upisuju u jednoj transakciji. Vraca broj upisanih redova.
        public static readonly string[] KoloneIzExcela =
        {
            "KONTEJNER", "VRSTA", "BRODAR", "NALOGODAVAC/UVOZ", "POSTUPAK/UVOZ", "UVOZNIK", "BL/UVOZ", "VOZ/kamion", "L/R", "PLOMBA_UVOZ"
        };

        // Kolone za SELECT redosledom iz Polja (u migriranoj bazi nova kolona BL/UVOZ je fizički poslednja)
        public static string SqlKolone()
        {
            var kolone = new System.Collections.Generic.List<string>();
            foreach (TerminalPrivPolje polje in Polja)
                kolone.Add("[" + polje.Kolona + "]");
            return string.Join(", ", kolone);
        }

        // Vrednost iz liste dozvoljenih koja odgovara unetoj bez obzira na velika/mala slova, razmake i
        // apostrofe (40'HC, 40HC -> 40' HC; maersk -> MAERSK); null ako takve nema
        public static string DozvoljenaVrednost(TerminalPrivPolje polje, string vrednost)
        {
            string kljuc = KljucPoredjenja(vrednost);
            return Array.Find(polje.Dozvoljeno, d => KljucPoredjenja(d) == kljuc);
        }

        private static string KljucPoredjenja(string vrednost)
        {
            return (vrednost ?? "").Replace(" ", "").Replace("'", "").ToUpperInvariant();
        }

        public static TerminalPrivPolje Polje(string kolona)
        {
            foreach (TerminalPrivPolje polje in Polja)
            {
                if (polje.Kolona == kolona)
                    return polje;
            }
            throw new ArgumentException("Nepoznata kolona: " + kolona);
        }

        // Kontejneri koji su već u TerminalPriv i nisu konačno izašli (Konačni_GATE_OUT je prazan).
        // Uvoz Excela ih preskače, da se isti kontejner ne upiše dvaput.
        public System.Collections.Generic.HashSet<string> AktivniKontejneri()
        {
            var aktivni = new System.Collections.Generic.HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (SqlConnection con = new SqlConnection(Saobracaj.Sifarnici.frmLogovanje.connectionString))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT DISTINCT RTRIM(KONTEJNER) FROM dbo.TerminalPriv WHERE [Konačni_GATE_OUT] IS NULL AND KONTEJNER IS NOT NULL", con))
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                        aktivni.Add(Convert.ToString(dr[0]));
                }
            }
            return aktivni;
        }

        public int InsTerminalPrivFromExcel(System.Collections.Generic.IList<string[]> redovi)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "insTerminalPrivFromExcel";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            foreach (string kolona in KoloneIzExcela)
            {
                TerminalPrivPolje polje = Polje(kolona);
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = polje.Parametar;
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Size = polje.Velicina;
                parameter.Direction = ParameterDirection.Input;
                myCommand.Parameters.Add(parameter);
            }

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            int upisano = 0;
            try
            {
                foreach (string[] red in redovi)
                {
                    for (int i = 0; i < KoloneIzExcela.Length; i++)
                    {
                        string tekst = red[i] == null ? "" : red[i].Trim();
                        myCommand.Parameters[i].Value = tekst.Length == 0 ? (object)DBNull.Value : tekst;
                    }
                    myCommand.ExecuteNonQuery();
                    upisano++;
                }
                myTransaction.Commit();
                return upisano;
            }
            catch (SqlException ex)
            {
                myTransaction.Rollback();
                throw new Exception("Neuspešan uvoz u TerminalPriv (nijedan red nije upisan): " + ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Izlazni parametar procedure sa brojem upisanih pokreta brodara (@BrojMaersk, @BrojMSC, @BrojCMA, @BrojOOCL, @BrojHMM, @BrojHAPAG, @BrojFLEXBOX, @BrojCOSCO)
        private static SqlParameter DodajParametarBroj(SqlCommand myCommand, string naziv)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = naziv;
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameter);
            return parameter;
        }

        private static int Broj(SqlParameter parameter)
        {
            return parameter.Value == null || parameter.Value == DBNull.Value ? 0 : Convert.ToInt32(parameter.Value);
        }

        private static void DodajParametreZaPolja(SqlCommand myCommand, DataRow red)
        {
            foreach (TerminalPrivPolje polje in Polja)
            {
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = polje.Parametar;
                parameter.Direction = ParameterDirection.Input;

                object vrednost = red[polje.Kolona];
                if (polje.Tip == TerminalPrivTip.Datum)
                {
                    parameter.SqlDbType = SqlDbType.DateTime;
                    parameter.Value = vrednost == null || vrednost == DBNull.Value ? (object)DBNull.Value : vrednost;
                }
                else
                {
                    parameter.SqlDbType = SqlDbType.NVarChar;
                    parameter.Size = polje.Velicina;
                    string tekst = vrednost == null || vrednost == DBNull.Value ? "" : vrednost.ToString().Trim();
                    // prazan VOZAČ ide kao prazan tekst i briše ime; NULL procedura čita kao 'bez izmene' (starija verzija programa)
                    parameter.Value = tekst.Length == 0 && polje.Tip != TerminalPrivTip.TekstZaPotvrdu ? (object)DBNull.Value : tekst;
                }
                myCommand.Parameters.Add(parameter);
            }
        }
    }
}
