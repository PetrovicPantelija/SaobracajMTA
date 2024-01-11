using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    internal class InsertRN
    {
        public string connect = Sifarnici.frmLogovanje.connectionString;
        public void InsRNPPrijemVoza(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNPrijemVoza";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);

            SqlParameter br = new SqlParameter();
            br.ParameterName = "@BrojKontejnera";
            br.SqlDbType = SqlDbType.NVarChar;
            br.Size = 50;
            br.Direction = ParameterDirection.Input;
            br.Value = BrojKontejnera;
            cmd.Parameters.Add(br);

            SqlParameter vrsta = new SqlParameter();
            vrsta.ParameterName = "@VrstaKontejnera";
            vrsta.SqlDbType = SqlDbType.Int;
            vrsta.Direction = ParameterDirection.Input;
            vrsta.Value = VrstaKontejnera;
            cmd.Parameters.Add(vrsta);

            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);

            SqlParameter plomba = new SqlParameter();
            plomba.ParameterName = "@BrojPlombe";
            plomba.SqlDbType = SqlDbType.NVarChar;
            plomba.Size = 50;
            plomba.Direction = ParameterDirection.Input;
            plomba.Value = BrojPlombe;
            cmd.Parameters.Add(plomba);

            SqlParameter uvoznik = new SqlParameter();
            uvoznik.ParameterName = "@Uvoznik";
            uvoznik.SqlDbType = SqlDbType.Int;
            uvoznik.Direction = ParameterDirection.Input;
            uvoznik.Value = Uvoznik;
            cmd.Parameters.Add(uvoznik);

            SqlParameter brodar = new SqlParameter();
            brodar.ParameterName = "@NazivBrodara";
            brodar.SqlDbType = SqlDbType.Int;
            brodar.Direction = ParameterDirection.Input;
            brodar.Value = NazivBrodara;
            cmd.Parameters.Add(brodar);

            SqlParameter roba = new SqlParameter();
            roba.ParameterName = "@VrstaRobe";
            roba.SqlDbType = SqlDbType.Int;
            roba.Direction = ParameterDirection.Input;
            roba.Value = VrstaRobe;
            cmd.Parameters.Add(roba);

            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis cena u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis cena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRNPPrijemVozaCeoVoz(DateTime DatumRasporeda,  string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int PrijemID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNPrijemVozaCeoVoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);

           

            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);

            

            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@PrijemID";
            prijemid.SqlDbType = SqlDbType.Int;

            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = PrijemID;
            cmd.Parameters.Add(prijemid);

          

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis cena u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis cena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void InsRNOtpremaVozaCeoVoz(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena, int OtpremaID)
        {
                                               
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRNOtpremaVozaCeoVoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);

            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);



            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter prijemid = new SqlParameter();
            prijemid.ParameterName = "@OtpremaID";
            prijemid.SqlDbType = SqlDbType.Int;

            prijemid.Direction = ParameterDirection.Input;
            prijemid.Value = OtpremaID;
            cmd.Parameters.Add(prijemid);



            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis cena u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis cena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }

        public void UpdRNPPrijemVoza(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRNPrijemVoza";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter dat1 = new SqlParameter();
            dat1.ParameterName = "@DatumRasporeda";
            dat1.SqlDbType = SqlDbType.DateTime;
            dat1.Direction = ParameterDirection.Input;
            dat1.Value = DatumRasporeda;
            cmd.Parameters.Add(dat1);

            SqlParameter br = new SqlParameter();
            br.ParameterName = "@BrojKontejnera";
            br.SqlDbType = SqlDbType.NVarChar;
            br.Size = 50;
            br.Direction = ParameterDirection.Input;
            br.Value = BrojKontejnera;
            cmd.Parameters.Add(br);

            SqlParameter vrsta = new SqlParameter();
            vrsta.ParameterName = "@VrstaKontejnera";
            vrsta.SqlDbType = SqlDbType.Int;
            vrsta.Direction = ParameterDirection.Input;
            vrsta.Value = VrstaKontejnera;
            cmd.Parameters.Add(vrsta);

            SqlParameter nalogIzdao = new SqlParameter();
            nalogIzdao.ParameterName = "@NalogIzdao";
            nalogIzdao.SqlDbType = SqlDbType.NVarChar;
            nalogIzdao.Size = 50;
            nalogIzdao.Direction = ParameterDirection.Input;
            nalogIzdao.Value = NalogIzdao;
            cmd.Parameters.Add(nalogIzdao);

            SqlParameter dat2 = new SqlParameter();
            dat2.ParameterName = "@DatumRealizacije";
            dat2.SqlDbType = SqlDbType.DateTime;
            dat2.Direction = ParameterDirection.Input;
            dat2.Value = DatumRealizacije;
            cmd.Parameters.Add(dat2);

            SqlParameter saSredstva = new SqlParameter();
            saSredstva.ParameterName = "@SaVoznogSredstva";
            saSredstva.SqlDbType = SqlDbType.Int;
            saSredstva.Direction = ParameterDirection.Input;
            saSredstva.Value = SaVoznogSredstva;
            cmd.Parameters.Add(saSredstva);

            SqlParameter plomba = new SqlParameter();
            plomba.ParameterName = "@BrojPlombe";
            plomba.SqlDbType = SqlDbType.NVarChar;
            plomba.Size = 50;
            plomba.Direction = ParameterDirection.Input;
            plomba.Value = BrojPlombe;
            cmd.Parameters.Add(plomba);

            SqlParameter uvoznik = new SqlParameter();
            uvoznik.ParameterName = "@Uvoznik";
            uvoznik.SqlDbType = SqlDbType.Int;
            uvoznik.Direction = ParameterDirection.Input;
            uvoznik.Value = Uvoznik;
            cmd.Parameters.Add(uvoznik);

            SqlParameter brodar = new SqlParameter();
            brodar.ParameterName = "@NazivBrodara";
            brodar.SqlDbType = SqlDbType.Int;
            brodar.Direction = ParameterDirection.Input;
            brodar.Value = NazivBrodara;
            cmd.Parameters.Add(brodar);

            SqlParameter roba = new SqlParameter();
            roba.ParameterName = "@VrstaRobe";
            roba.SqlDbType = SqlDbType.Int;
            roba.Direction = ParameterDirection.Input;
            roba.Value = VrstaRobe;
            cmd.Parameters.Add(roba);

            SqlParameter naSklad = new SqlParameter();
            naSklad.ParameterName = "@NaSkladiste";
            naSklad.SqlDbType = SqlDbType.Int;
            naSklad.Direction = ParameterDirection.Input;
            naSklad.Value = NaSkladiste;
            cmd.Parameters.Add(naSklad);

            SqlParameter naPoz = new SqlParameter();
            naPoz.ParameterName = "@NaPozicijuSklad";
            naPoz.SqlDbType = SqlDbType.Int;
            naPoz.Direction = ParameterDirection.Input;
            naPoz.Value = NaPozicijuSklad;
            cmd.Parameters.Add(naPoz);

            SqlParameter usluga = new SqlParameter();
            usluga.ParameterName = "@IdUsluge";
            usluga.SqlDbType = SqlDbType.Int;
            usluga.Direction = ParameterDirection.Input;
            usluga.Value = IdUsluge;
            cmd.Parameters.Add(usluga);

            SqlParameter real = new SqlParameter();
            real.ParameterName = "@NalogRealizovao";
            real.SqlDbType = SqlDbType.NVarChar;
            real.Size = 50;
            real.Direction = ParameterDirection.Input;
            real.Value = NalogRealizovao;
            cmd.Parameters.Add(real);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis cena u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis cena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void DelRNPPrijemVoza(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteRNPrijemVoza";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            conn.Open();
            SqlTransaction myTransaction = conn.BeginTransaction();
            cmd.Transaction = myTransaction;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = conn.BeginTransaction();
                cmd.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis cena u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis cena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                { }
            }
        }
        public void InsRnMedjuskladisni(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
    int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "InsertRNMedjuskladisni";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter(@"NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }

        public void UpdRnMedjuskladisni(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "UpdateRNMedjuskladisni";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value=ID});
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter(@"NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });

                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }

        public void DelRnMedjuskladisni(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "DeleteRNMedjuskladisni";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRnOtpremaCirade(DateTime DatumRasporeda, string BrojKontejnera, int NazivBrodara, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo, int CarinskiPostupak,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNOtpremaCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }

        public void UpdRnOtpremaCirade(int ID, DateTime DatumRasporeda, string BrojKontejnera, int NazivBrodara, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo, int CarinskiPostupak,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNOtpremaCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(ex.ToString());
                    }
                }
                conn.Close();
            }
        }

        public void DelRnOtpremaCirade(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNOtpremaCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNOtpremaPlatforme(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int Uvoznik, int CarinskiPostupak, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNOtpremaPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    } 
                    catch (SqlException ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show(ex.ToString());

                    }
                }
                conn.Close();
            }
        }

        public void UpdRNOtpremaPlatforme(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int Uvoznik, int CarinskiPostupak, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNOtpremaPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNOtpremaPlatforme(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNOtpremaPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNOtpremaPlatforme2(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNOtpremaPlatforme2(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNOtpremaPlatforme2(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNOtpremaVoza(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            string BrojPlombe, string BrojVagona,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNOtpremaVoza";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@BrojVagona", SqlDbType.NVarChar, 50) { Value = BrojVagona });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNOtpremaVoza(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            string BrojPlombe, string BrojVagona,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNOtpremaVoza";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@BrojVagona", SqlDbType.NVarChar, 50) { Value = BrojVagona });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNOtpremaVoza(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNOtpremaVoza";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNPregledIpostavkaKontejnera(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPregledIpostavkaKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPregledIpostavkaKontejnera(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPregledIpostavkaKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPregledIpostavkaKontejnera(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPregledIpostavkaKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNPregledIspraznjenogKontejnera(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NazivBrodara,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPregledIspraznjenogKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPregledIspraznjenogKontejnera(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NazivBrodara,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPregledIspraznjenogKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPregledIspraznjenogKontejnera(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPregledIspraznjenogKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNPPrijemCirade(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva,
            string BrojPlombe, int CarinskiPostupak, int InspekcijskiPregled, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad,
            int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPrijemCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPPrijemCirade(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva,
            string BrojPlombe, int CarinskiPostupak, int InspekcijskiPregled, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad,
            int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPrijemCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPPrijemCirade(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPrijemCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNPPrijemPlatforme(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            string BrojPlombe, int Izvoznik, int NazivBrodara, int CarinskiPostupak, int InspekcijskiPregled, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge,
            string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPrijemPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Izvoznik", SqlDbType.Int) { Value = Izvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijaSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPPrijemPlatforme(int ID, DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            string BrojPlombe, int Izvoznik, int NazivBrodara, int CarinskiPostupak, int InspekcijskiPregled, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge,
            string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPrijemPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Izvoznik", SqlDbType.Int) { Value = Izvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijaSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPPrijemPlatforme(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPrijemPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void InsRNPPrijemPlatforme2(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPrijemPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijaSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPPrijemPlatforme2(int ID, DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPrijemPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijaSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPPrijemPlatforme2(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPrijemPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }
        public void InsRNPPrijemVoza2(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int CarinskiPostupak, int InspekcijskiPregled, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertRNPrijemVoza2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void UpdRNPPrijemVoza2(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int CarinskiPostupak, int InspekcijskiPregled, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "UpdateRNPrijemVoza2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@USkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@UPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }

        public void DelRNPPrijemVoza2(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "DeleteRNPrijemVoza2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch (SqlException ex) { tran.Rollback(); MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information); MessageBox.Show(ex.ToString()); }
                }
                conn.Close();
            }
        }
    }
}