using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Saobracaj.Pantheon_Export;
using Syncfusion.Windows.Forms.Chart;

namespace Saobracaj.Carinsko
{
    internal class InsertPrijemnicaCarinskaStavke
    {

        string connect = Sifarnici.frmLogovanje.connectionString;

        public void InsPrijemnicaCarinskaStavke(int ID, int IDNadredjena, string Artikal, string JM, double Koleta, double Bruto, int Pozicija, double Vrednost, string Valuta,
string BrojKontejnera, string Paleta, string VrstaPalete, string Dimenzije)
        {

          

            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPrijemnicaCarinskaStavka";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

  




            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@ID";
            parameter4.SqlDbType = SqlDbType.Int;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = ID;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@IDNadredjena";
            parameter5.SqlDbType = SqlDbType.Int;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = IDNadredjena;
            myCommand.Parameters.Add(parameter5);


            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@Artikal";
            parameter6.SqlDbType = SqlDbType.NVarChar;
            parameter6.Size = 100;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Artikal;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@JM";
            parameter7.SqlDbType = SqlDbType.Char;
            parameter7.Size = 3;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = JM;
            myCommand.Parameters.Add(parameter7);



            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Koleta";
            parameter8.SqlDbType = SqlDbType.Decimal;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Koleta;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@Bruto";
            parameter9.SqlDbType = SqlDbType.Decimal;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Bruto;
            myCommand.Parameters.Add(parameter9);

  SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@Pozicija";
            parameter10.SqlDbType = SqlDbType.Int;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = Pozicija;
            myCommand.Parameters.Add(parameter10);


            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@Vrednost";
            parameter11.SqlDbType = SqlDbType.Decimal;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = Vrednost;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@Valuta";
            parameter12.SqlDbType = SqlDbType.Char;
            parameter12.Size = 3;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = Valuta;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@BrojKontejnera";
            parameter13.SqlDbType = SqlDbType.NVarChar;
            parameter13.Size = 30;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = BrojKontejnera;
            myCommand.Parameters.Add(parameter13);


            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@Paleta";
            parameter14.SqlDbType = SqlDbType.NVarChar;
            parameter14.Size = 30;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = Paleta;
            myCommand.Parameters.Add(parameter14);


            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@VrstaPalete";
            parameter16.SqlDbType = SqlDbType.NVarChar;
            parameter16.Size = 30;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = VrstaPalete;
            myCommand.Parameters.Add(parameter16);


            SqlParameter parameter17 = new SqlParameter();
            parameter17.ParameterName = "@Dimenzije";
            parameter17.SqlDbType = SqlDbType.NVarChar;
            parameter17.Size = 50;
            parameter17.Direction = ParameterDirection.Input;
            parameter17.Value = Dimenzije;
            myCommand.Parameters.Add(parameter17);



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
                throw new Exception("Neuspešan upis stavki");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos NHM broja je uspešno završena", "",
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
