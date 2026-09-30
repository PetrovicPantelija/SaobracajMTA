using System;
using System.Data;
using System.Data.SqlClient;

namespace Saobracaj.RNI
{
    public enum TerminalPrivTip
    {
        Tekst,
        Datum
    }

    public class TerminalPrivPolje
    {
        public string Kolona;       // naziv kolone u tabeli TerminalPriv
        public string Parametar;    // naziv parametra stored procedure
        public TerminalPrivTip Tip;
        public int Velicina;        // maksimalan broj karaktera (za tekst)
        public string[] Dozvoljeno; // dozvoljene vrednosti (null = bez ogranicenja)

        public TerminalPrivPolje(string kolona, string parametar, TerminalPrivTip tip, int velicina, string[] dozvoljeno)
        {
            Kolona = kolona;
            Parametar = parametar;
            Tip = tip;
            Velicina = velicina;
            Dozvoljeno = dozvoljeno;
        }
    }

    class insertTerminalPriv
    {
        public const int DuzinaKontejnera = 11;

        public static readonly string[] Statusi = { "PRAZAN", "PUN", "?", "RAZVOZ", "Blanks", "PRETOVAR", "U RAZVOZU", "UTOVAR" };
        public static readonly string[] Stanja = { "DOBAR", "LOŠ", "FOOD GRADE", "OŠTEĆEN", "FLEXI" };
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
            new TerminalPrivPolje("VRSTA", "@VRSTA", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("BRODAR", "@BRODAR", TerminalPrivTip.Tekst, 30, null),
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
            new TerminalPrivPolje("TARA", "@TARA", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("MAX_NOSIVOST_CNT", "@MAX_NOSIVOST_CNT", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("VOZILO/PREUZIMANJE", "@VOZILO_PREUZIMANJE", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("PLOMBA/IZVOZ", "@PLOMBA_IZVOZ", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("NAPOMENA", "@NAPOMENA", TerminalPrivTip.Tekst, 300, null),
            new TerminalPrivPolje("OPIS", "@OPIS", TerminalPrivTip.Tekst, 300, null),
            new TerminalPrivPolje("OTPREMA", "@OTPREMA", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("POSLATE SLIKE", "@POSLATE_SLIKE", TerminalPrivTip.Tekst, 30, null),
            new TerminalPrivPolje("Prevoznik", "@PREVOZNIK", TerminalPrivTip.Tekst, 30, null),
        };

        // Vraca ID novog zapisa
        public int InsTerminalPriv(DataRow red)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "insTerminalPriv";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            DodajParametreZaPolja(myCommand, red);

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
                throw new Exception("Neuspešan upis u TerminalPriv: " + ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }

        public void UpdTerminalPriv(DataRow red)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "updTerminalPriv";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Convert.ToInt32(red["ID"]);
            myCommand.Parameters.Add(parameter);

            DodajParametreZaPolja(myCommand, red);

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
            "KONTEJNER", "VRSTA", "BRODAR", "NALOGODAVAC/UVOZ", "POSTUPAK/UVOZ", "UVOZNIK", "PLOMBA_UVOZ"
        };

        // Kolone za SELECT redosledom iz Polja (u migriranoj bazi nova kolona BL/UVOZ je fizički poslednja)
        public static string SqlKolone()
        {
            var kolone = new System.Collections.Generic.List<string>();
            foreach (TerminalPrivPolje polje in Polja)
                kolone.Add("[" + polje.Kolona + "]");
            return string.Join(", ", kolone);
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
                    parameter.Value = tekst.Length == 0 ? (object)DBNull.Value : tekst;
                }
                myCommand.Parameters.Add(parameter);
            }
        }
    }
}
