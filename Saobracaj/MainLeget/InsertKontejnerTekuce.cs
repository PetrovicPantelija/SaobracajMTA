using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.MainLeget
{
    internal class InsertKontejnerTekuce
    {
        public string connect = Sifarnici.frmLogovanje.connectionString;

        public void InsKontejnerTerkuce(string Kontejner, int Skladiste, int Pozicija, int Kvalitet, int TipKontejnera, double Kolicina)
        {
           
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertKontejnerTekuce";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter kontejner = new SqlParameter();
            kontejner.ParameterName = "@Kontejner";
            kontejner.SqlDbType = SqlDbType.NVarChar;
            kontejner.Size = 50;
            kontejner.Direction = ParameterDirection.Input;
            kontejner.Value = Kontejner;
            myCommand.Parameters.Add(kontejner);

            SqlParameter skladiste = new SqlParameter();
            skladiste.ParameterName = "@Skladiste";
            skladiste.SqlDbType = SqlDbType.Int;
            skladiste.Direction = ParameterDirection.Input;
            skladiste.Value = Skladiste;
            myCommand.Parameters.Add(skladiste);

            SqlParameter pozicija = new SqlParameter();
            pozicija.ParameterName = "@Pozicija";
            pozicija.SqlDbType = SqlDbType.Int;
            pozicija.Direction = ParameterDirection.Input;
            pozicija.Value = Pozicija;
            myCommand.Parameters.Add(pozicija);

            SqlParameter kvalitet = new SqlParameter();
            kvalitet.ParameterName = "@Kvalitet";
            kvalitet.SqlDbType = SqlDbType.Int;
            kvalitet.Direction = ParameterDirection.Input;
            kvalitet.Value = Kvalitet;
            myCommand.Parameters.Add(kvalitet);

            SqlParameter tipKontejnera = new SqlParameter();
            tipKontejnera.ParameterName = "@TipKontejnera";
            tipKontejnera.SqlDbType = SqlDbType.Int;
            tipKontejnera.Direction = ParameterDirection.Input;
            tipKontejnera.Value = TipKontejnera;
            myCommand.Parameters.Add(tipKontejnera);

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            myCommand.Parameters.Add(kolicina);

           

            myConnection.Open();
            SqlTransaction myTransaction = myConnection.BeginTransaction();
            myCommand.Transaction = myTransaction;
            // IDPom = (int)myCommand.Parameters["@IDPom"].Value;
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
                MessageBox.Show(ex.ToString());

                throw new Exception("Neuspešna promena podataka");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Neuspešna promena podataka", "",
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
