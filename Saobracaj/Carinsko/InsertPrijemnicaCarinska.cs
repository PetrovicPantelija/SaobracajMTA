using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Carinko
{
    internal class InsertPrijemnicaCarinska
    {
        string connect = Sifarnici.frmLogovanje.connectionString;

        public void updetePrijemnicaCarinskaStatus(int Prijemnica, string Status)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePrijemnicaCarinskaStatus";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Prijemnica;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Status";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 50;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Status;
            myCommand.Parameters.Add(parameter2);




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
                throw new Exception("Neuspešan upis zaglavlje");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos zaglavlja je uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }
        public void InsPrijemnicaCarinska( string Status, DateTime Datum,
                    string Korisnik, int SkladisteID, string Dokument,
                    int MBR, string VrstaSkladista, string Sektor,
                    int Vlasnik, int KorisnikRoba, int Posiljalac,
                    int Primalac, string BrFakture,
                    string Prevoznik, string BrojKamiona,
                    string Napomena1, string Napomena2,
                    string TransportNo, DateTime OcekivanoVreme)
        {


        
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPrijemnicaCarinska";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Status";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Status;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Datum";
            parameter2.SqlDbType = SqlDbType.DateTime;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Datum;
            myCommand.Parameters.Add(parameter2);


            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Korisnik";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 50;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Korisnik;
            myCommand.Parameters.Add(parameter3);




            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@SkladisteID";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = SkladisteID;
            myCommand.Parameters.Add(parameter4);



            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Dokument";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Size = 150;
           parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Dokument;
            myCommand.Parameters.Add(parameter5);


            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@MBR";
            parameter6.SqlDbType = SqlDbType.Int;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = MBR;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@VrstaSkladista";
            parameter7.SqlDbType = SqlDbType.NVarChar;
            parameter7.Size = 50;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = VrstaSkladista;
            myCommand.Parameters.Add(parameter7);


            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Sektor";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 50;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Sektor;
            myCommand.Parameters.Add(parameter8);


            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@Vlasnik";
            parameter9.SqlDbType = SqlDbType.Int;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Vlasnik;
            myCommand.Parameters.Add(parameter9);


            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@KorisnikRoba";
            parameter10.SqlDbType = SqlDbType.Int;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = KorisnikRoba;
            myCommand.Parameters.Add(parameter10);


            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@Posiljalac";
            parameter11.SqlDbType = SqlDbType.Int;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = Posiljalac;
            myCommand.Parameters.Add(parameter11);


            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@Primalac";
            parameter12.SqlDbType = SqlDbType.Int;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = Primalac;
            myCommand.Parameters.Add(parameter12);


            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@BrFakture";
            parameter13.SqlDbType = SqlDbType.NVarChar;
            parameter13.Size = 50;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = BrFakture;
            myCommand.Parameters.Add(parameter13);


            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@Prevoznik";
            parameter14.SqlDbType = SqlDbType.NVarChar;
            parameter14.Size = 50;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = Prevoznik;
            myCommand.Parameters.Add(parameter14);





            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@BrojKamiona";
            parameter16.SqlDbType = SqlDbType.NVarChar;
            parameter16.Size = 50;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = BrojKamiona;
            myCommand.Parameters.Add(parameter16);


            SqlParameter parameter17 = new SqlParameter();
            parameter17.ParameterName = "@Napomena1";
            parameter17.SqlDbType = SqlDbType.NVarChar;
            parameter17.Size = 500;
            parameter17.Direction = ParameterDirection.Input;
            parameter17.Value = Napomena1;
            myCommand.Parameters.Add(parameter17);


            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@Napomena2";
            parameter18.SqlDbType = SqlDbType.NVarChar;
            parameter18.Size = 500;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = Napomena2;
            myCommand.Parameters.Add(parameter18);


            SqlParameter parameter19 = new SqlParameter();
            parameter19.ParameterName = "@TransportNo";
            parameter19.SqlDbType = SqlDbType.NVarChar;
            parameter19.Size = 50;
            parameter19.Direction = ParameterDirection.Input;
            parameter19.Value = TransportNo;
            myCommand.Parameters.Add(parameter19);


            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@OcekivanoVreme";
            parameter20.SqlDbType = SqlDbType.DateTime;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = OcekivanoVreme;
            myCommand.Parameters.Add(parameter20);


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
                throw new Exception("Neuspešan upis zaglavlje");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos zaglavlja je uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }


        public void UpdPrijemnicaCarinska(int ID , string Status, DateTime Datum,
string Korisnik, int SkladisteID, string Dokument,
int MBR, string VrstaSkladista, string Sektor,
int Vlasnik, int KorisnikRoba, int Posiljalac,
int Primalac, string BrFakture,
string Prevoznik, string BrojKamiona,
string Napomena1, string Napomena2,
string TransportNo, DateTime OcekivanoVreme)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePrijemnicaCarinska";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter0 = new SqlParameter();
            parameter0.ParameterName = "@ID";
            parameter0.SqlDbType = SqlDbType.Int;
            parameter0.Direction = ParameterDirection.Input;
            parameter0.Value = ID;
            myCommand.Parameters.Add(parameter0);


            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@Status";
            parameter.SqlDbType = SqlDbType.NVarChar;
            parameter.Size = 50;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = Status;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Datum";
            parameter2.SqlDbType = SqlDbType.DateTime;
            parameter.Size = 1000;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Datum;
            myCommand.Parameters.Add(parameter2);


            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Korisnik";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 50;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Korisnik;
            myCommand.Parameters.Add(parameter3);




            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@SkladisteID";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = SkladisteID;
            myCommand.Parameters.Add(parameter4);



            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@Dokument";
            parameter5.SqlDbType = SqlDbType.NVarChar;
            parameter5.Size = 150;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Dokument;
            myCommand.Parameters.Add(parameter5);


            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@MBR";
            parameter6.SqlDbType = SqlDbType.Int;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = MBR;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@VrstaSkladista";
            parameter7.SqlDbType = SqlDbType.NVarChar;
            parameter7.Size = 50;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = VrstaSkladista;
            myCommand.Parameters.Add(parameter7);


            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Sektor";
            parameter8.SqlDbType = SqlDbType.NVarChar;
            parameter8.Size = 50;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Sektor;
            myCommand.Parameters.Add(parameter8);


            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@Vlasnik";
            parameter9.SqlDbType = SqlDbType.Int;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Vlasnik;
            myCommand.Parameters.Add(parameter9);


            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@KorisnikRoba";
            parameter10.SqlDbType = SqlDbType.Int;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = KorisnikRoba;
            myCommand.Parameters.Add(parameter10);


            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@Posiljalac";
            parameter11.SqlDbType = SqlDbType.Int;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = Posiljalac;
            myCommand.Parameters.Add(parameter11);


            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@Primalac";
            parameter12.SqlDbType = SqlDbType.Int;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = Primalac;
            myCommand.Parameters.Add(parameter12);


            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@BrFakture";
            parameter13.SqlDbType = SqlDbType.NVarChar;
            parameter13.Size = 50;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = BrFakture;
            myCommand.Parameters.Add(parameter13);


            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@Prevoznik";
            parameter14.SqlDbType = SqlDbType.NVarChar;
            parameter14.Size = 50;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = Prevoznik;
            myCommand.Parameters.Add(parameter14);


            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@Prevoznik";
            parameter15.SqlDbType = SqlDbType.NVarChar;
            parameter15.Size = 50;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = Prevoznik;
            myCommand.Parameters.Add(parameter15);


            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@BrojKamiona";
            parameter16.SqlDbType = SqlDbType.NVarChar;
            parameter16.Size = 50;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = BrojKamiona;
            myCommand.Parameters.Add(parameter16);


            SqlParameter parameter17 = new SqlParameter();
            parameter17.ParameterName = "@Napomena1";
            parameter17.SqlDbType = SqlDbType.NVarChar;
            parameter17.Size = 500;
            parameter17.Direction = ParameterDirection.Input;
            parameter17.Value = Napomena1;
            myCommand.Parameters.Add(parameter17);


            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@Napomena2";
            parameter18.SqlDbType = SqlDbType.NVarChar;
            parameter18.Size = 500;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = Napomena2;
            myCommand.Parameters.Add(parameter18);


            SqlParameter parameter19 = new SqlParameter();
            parameter19.ParameterName = "@TransportNo";
            parameter19.SqlDbType = SqlDbType.NVarChar;
            parameter19.Size = 50;
            parameter19.Direction = ParameterDirection.Input;
            parameter19.Value = TransportNo;
            myCommand.Parameters.Add(parameter19);


            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@OcekivanoVreme";
            parameter20.SqlDbType = SqlDbType.DateTime;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = OcekivanoVreme;
            myCommand.Parameters.Add(parameter20);

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
                throw new Exception("Neuspešna promena NHM brojeva");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Promena NHM broja je uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }


        public void DeletePrijemnicaCarinska(int ID)
        {

            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeletePrijemnicaCarinska";
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
                throw new Exception("Brisanje neuspešno");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Brisanje nije uspešno", "",
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

