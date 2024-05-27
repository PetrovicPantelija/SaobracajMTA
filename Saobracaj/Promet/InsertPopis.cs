using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace TrackModal.Promet
{
    class InsertPopis
    {
        public void InsPopis(DateTime DatumPopisa, string Napomena, DateTime Datum, string Korisnik)
        { 
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPopis";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@DatumPopisa";
            parameter0.SqlDbType = SqlDbType.DateTime;
            // parameter0.Size = 3;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = DatumPopisa;
            myCommand.Parameters.Add(parameter0);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Napomena";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 120;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Napomena;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Datum";
            parameter7.SqlDbType = SqlDbType.DateTime;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Datum;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Korisnik";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 20;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Korisnik;
            myCommand.Parameters.Add(parameter8);



            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void UpdPopis(int ID, DateTime DatumPopisa, string Napomena, DateTime Datum, string Korisnik)
        {
            /*  @DatumPopisa datetime
            ,@Napomena nvarchar(120)
            ,@Datum datetime
            ,@Korisnik nvarchar(20)
             */



            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePopis";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter00 = new SqlParameter();
            parameter00.ParameterName = "@ID";
            parameter00.SqlDbType = SqlDbType.Int;
            // parameter0.Size = 3;
            parameter00.Direction = ParameterDirection.Input;
            parameter00.Value = ID;
            myCommand.Parameters.Add(parameter00);

            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@DatumPopisa";
            parameter0.SqlDbType = SqlDbType.DateTime;
            // parameter0.Size = 3;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = DatumPopisa;
            myCommand.Parameters.Add(parameter0);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Napomena";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 120;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Napomena;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Datum";
            parameter7.SqlDbType = SqlDbType.DateTime;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Datum;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Korisnik";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 20;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Korisnik;
            myCommand.Parameters.Add(parameter8);



            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void DelPopis(int ID)
        {
            /*  @DatumPopisa datetime
            ,@Napomena nvarchar(120)
            ,@Datum datetime
            ,@Korisnik nvarchar(20)
             */



            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeletePopis";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter00 = new SqlParameter();
            parameter00.ParameterName = "@ID";
            parameter00.SqlDbType = SqlDbType.Int;
            parameter00.Direction = ParameterDirection.Input;
            parameter00.Value = ID;
            myCommand.Parameters.Add(parameter00);



            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }
        public void InsStavkePopisa(int IDNadredjenog,DateTime Datum,string Korisnik)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertStavkePopisa";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@IDNadredjenog";
            parameter0.SqlDbType = SqlDbType.Int;
            // parameter0.Size = 3;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = IDNadredjenog;
            myCommand.Parameters.Add(parameter0);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Datum";
            parameter7.SqlDbType = SqlDbType.DateTime;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Datum;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Korisnik";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 20;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Korisnik;
            myCommand.Parameters.Add(parameter8);

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
        public void InsStvarnoStanje(int ID,decimal StvarnoStanje)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertStvarnoStanje";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@ID";
            parameter0.SqlDbType = SqlDbType.Int;
            // parameter0.Size = 3;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = ID;
            myCommand.Parameters.Add(parameter0);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@StvarnoStanje";
            parameter.SqlDbType = SqlDbType.Decimal;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = StvarnoStanje;
            myCommand.Parameters.Add(parameter);

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException ex)
            {
                throw new Exception("Neuspešan upis u bazu\n"+ex.ToString());
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }
        public void InsPopisStavke(int IDNadredjenog, string BrojKontejnera, int SkladisteU, int LokacijaU, DateTime Datum, string Korisnik)
        {
            /*  
                @IDNadredjenog [int],
	            @BrojKontejnera [nvarchar](20),
	            @SkladisteU [int],
	            @LokacijaU [int], 
	            @Datum datetime,
                @Korisnik nvarchar(20)
             */



            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPopisStavke";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@IDNadredjenog";
            parameter0.SqlDbType = SqlDbType.Int;
            // parameter0.Size = 3;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = IDNadredjenog;
            myCommand.Parameters.Add(parameter0);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@BrojKontejnera";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 20;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = BrojKontejnera;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@SkladisteU";
            parameter1.SqlDbType = SqlDbType.Int;
            //  parameter1.Size = 20;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = SkladisteU;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@LokacijaU";
            parameter2.SqlDbType = SqlDbType.Int;
            //  parameter1.Size = 20;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = LokacijaU;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Datum";
            parameter7.SqlDbType = SqlDbType.DateTime;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Datum;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Korisnik";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 20;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Korisnik;
            myCommand.Parameters.Add(parameter8);



            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void UpdPopisStavke(int ID, int IDNadredjenog, string BrojKontejnera, int SkladisteU, int LokacijaU, DateTime Datum, string Korisnik)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePopisStavke";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter00 = new SqlParameter();
            parameter00.ParameterName = "@ID";
            parameter00.SqlDbType = SqlDbType.Int;
            // parameter0.Size = 3;
            parameter00.Direction = ParameterDirection.Input;
            parameter00.Value = ID;
            myCommand.Parameters.Add(parameter00);

            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@IDNadredjenog";
            parameter0.SqlDbType = SqlDbType.Int;
            // parameter0.Size = 3;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = IDNadredjenog;
            myCommand.Parameters.Add(parameter0);

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@BrojKontejnera";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 20;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = BrojKontejnera;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@SkladisteU";
            parameter1.SqlDbType = SqlDbType.Int;
            //  parameter1.Size = 20;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = SkladisteU;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@LokacijaU";
            parameter2.SqlDbType = SqlDbType.Int;
            //  parameter1.Size = 20;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = LokacijaU;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Datum";
            parameter7.SqlDbType = SqlDbType.DateTime;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Datum;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Korisnik";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 20;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Korisnik;
            myCommand.Parameters.Add(parameter8);


            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void DelPopisStavke(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeletePopisStavke";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter00 = new SqlParameter();
            parameter00.ParameterName = "@ID";
            parameter00.SqlDbType = SqlDbType.Int;
            // parameter0.Size = 3;
            parameter00.Direction = ParameterDirection.Input;
            parameter00.Value = ID;
            myCommand.Parameters.Add(parameter00);


            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

        public void InsertPopisStavkeUporedni(int ID)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "SelectPopisStavkeUporedni";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter00 = new SqlParameter();
            parameter00.ParameterName = "@Dokument";
            parameter00.SqlDbType = SqlDbType.Int;
            // parameter0.Size = 3;
            parameter00.Direction = ParameterDirection.Input;
            parameter00.Value = ID;
            myCommand.Parameters.Add(parameter00);


            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis u bazu");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }
        public void InsPrometTA(DateTime DatumTransakcije,string VrstaDokumenta,int PrStDokumenta,string BrojKontejnera,string PrSifVrstePrometa,decimal PrPrimKol,decimal PrIzdKol,
            int SkladisteU,int LokacijaU,int SkladisteIz,int LokacijaIz,DateTime Datum,string Korisnik,int SredstvoRada,DateTime DatumRasporeda)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPrometPopis";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter dT = new SqlParameter();
            dT.ParameterName = "@DatumTransakcije";
            dT.SqlDbType = SqlDbType.DateTime;
            dT.Direction = ParameterDirection.Input;
            dT.Value = DatumTransakcije;
            myCommand.Parameters.Add(dT);

            SqlParameter vrstaD = new SqlParameter();
            vrstaD.ParameterName = "@VrstaDokumenta";
            vrstaD.SqlDbType = SqlDbType.NVarChar;
            vrstaD.Direction = ParameterDirection.Input;
            vrstaD.Value = VrstaDokumenta;
            myCommand.Parameters.Add(vrstaD);

            SqlParameter prDok = new SqlParameter();
            prDok.ParameterName = "@PrStDokumenta";
            prDok.SqlDbType = SqlDbType.Int;
            prDok.Direction = ParameterDirection.Input;
            prDok.Value = PrStDokumenta;
            myCommand.Parameters.Add(prDok);

            SqlParameter kont = new SqlParameter();
            kont.ParameterName = "@BrojKontejnera";
            kont.SqlDbType = SqlDbType.NVarChar;
            kont.Direction = ParameterDirection.Input;
            kont.Value = BrojKontejnera;
            myCommand.Parameters.Add(kont);

            SqlParameter sifVrste = new SqlParameter();
            sifVrste.ParameterName = "@PrSifVrstePrometa";
            sifVrste.SqlDbType = SqlDbType.NVarChar;
            sifVrste.Direction = ParameterDirection.Input;
            sifVrste.Value = PrSifVrstePrometa;
            myCommand.Parameters.Add(sifVrste);

            SqlParameter prim = new SqlParameter();
            prim.ParameterName = "@PrPrimKol";
            prim.SqlDbType = SqlDbType.Decimal;
            prim.Direction= ParameterDirection.Input;
            prim.Value = PrPrimKol;
            myCommand.Parameters.Add(prim);

            SqlParameter izd = new SqlParameter();
            izd.ParameterName = "@PrIzdKol";
            izd.SqlDbType = SqlDbType.Decimal;
            izd.Direction = ParameterDirection.Input;
            izd.Value = PrIzdKol;
            myCommand.Parameters.Add(izd);

            SqlParameter skU = new SqlParameter();
            skU.ParameterName = "@SkladisteU";
            skU.SqlDbType= SqlDbType.Int;
            skU.Direction = ParameterDirection.Input;
            skU.Value = SkladisteU;
            myCommand.Parameters.Add(skU);

            SqlParameter lokU = new SqlParameter();
            lokU.ParameterName = "@LokacijaU";
            lokU.SqlDbType=SqlDbType.Int; lokU.Direction = ParameterDirection.Input;
            lokU.Value = LokacijaU;
            myCommand.Parameters.Add(lokU);

            SqlParameter sklIz = new SqlParameter();
            sklIz.ParameterName = "@SkladisteIz";
            sklIz.SqlDbType = SqlDbType.Int;
            sklIz.Direction= ParameterDirection.Input;
            sklIz.Value = SkladisteIz;
            myCommand.Parameters.Add(sklIz);

            SqlParameter lokIz = new SqlParameter();
            lokIz.ParameterName = "@LokacijaIz";
            lokIz.SqlDbType = SqlDbType.Int;
            lokIz.Direction=ParameterDirection.Input;
            lokIz.Value = LokacijaIz;
            myCommand.Parameters.Add(lokIz);

            SqlParameter kor = new SqlParameter();
            kor.ParameterName = "@Korisnik";
            kor.SqlDbType = SqlDbType.NVarChar;
            kor.Direction = ParameterDirection.Input;
            kor.Value = Korisnik;
            myCommand.Parameters.Add(kor);

            SqlParameter sr = new SqlParameter();
            sr.ParameterName = "@SredstvoRada";
            sr.SqlDbType = SqlDbType.Int;
            sr.Direction= ParameterDirection.Input;
            sr.Value = SredstvoRada;
            myCommand.Parameters.Add(sr);

            SqlParameter datR = new SqlParameter();
            datR.ParameterName = "@DatumRasporeda";
            datR.SqlDbType = SqlDbType.DateTime;
            datR.Direction = ParameterDirection.Input;
            datR.Value = DatumRasporeda;
            myCommand.Parameters.Add(datR);

            SqlParameter datum = new SqlParameter();
            datum.ParameterName = "@Datum";
            datum.SqlDbType = SqlDbType.DateTime;
            datum.Direction = ParameterDirection.Input;
            datum.Value = Datum;
            myCommand.Parameters.Add(datum);



            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            bool error = true;
            try
            {
                myCommand.ExecuteNonQuery();
                myTransaction.Commit();
                myTransaction = myConnection.BeginTransaction();
                myCommand.Transaction = myTransaction;
            }

            catch (SqlException ex)
            {
                throw new Exception("Neuspešan upis u bazu\n"+ex.ToString());
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Nije uspeo upis ", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }


            }
        }

    }
}
