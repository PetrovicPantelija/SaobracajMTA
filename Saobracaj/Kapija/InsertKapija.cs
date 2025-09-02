using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Kapija
{
    class InsertKapija
    {


        string connect = Sifarnici.frmLogovanje.connectionString;

        public int InsKapija(DateTime? @DatumDolaska, int? Status, string Vozac, string RegistarskiBroj, string Kontakt,
                                            string RazlogDolaska, DateTime? @DatumZakazanogDolaska, string KontaktUnutarFirme, DateTime? @DatumOdlaska)
        {
            int IDPom = 0;
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertKapija";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter datumDolaska = new SqlParameter();
            datumDolaska.ParameterName = "@DatumDolaska";
            datumDolaska.SqlDbType = SqlDbType.DateTime;
            datumDolaska.Direction = ParameterDirection.Input;
            datumDolaska.Value = DatumDolaska.HasValue ? (object)DatumDolaska.Value : DBNull.Value;
            myCommand.Parameters.Add(datumDolaska);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@Status";
            status.SqlDbType = SqlDbType.Int;
            status.Direction = ParameterDirection.Input;
            status.Value = Status.HasValue ? (object)Status.Value : DBNull.Value; 
            myCommand.Parameters.Add(status);

            SqlParameter vozac = new SqlParameter();
            vozac.ParameterName = "@Vozac";
            vozac.SqlDbType = SqlDbType.NVarChar;
            vozac.Size = 100;
            vozac.Direction = ParameterDirection.Input;
            vozac.Value = Vozac;
            myCommand.Parameters.Add(vozac);

            SqlParameter rgBroj = new SqlParameter();
            rgBroj.ParameterName = "@RegistarskiBroj";
            rgBroj.SqlDbType = SqlDbType.NVarChar;
            rgBroj.Size = 50;
            rgBroj.Direction = ParameterDirection.Input;
            rgBroj.Value = RegistarskiBroj;
            myCommand.Parameters.Add(rgBroj);

            SqlParameter kontakt = new SqlParameter();
            kontakt.ParameterName = "@Kontakt";
            kontakt.SqlDbType = SqlDbType.NVarChar;
            kontakt.Size = 100;
            kontakt.Direction = ParameterDirection.Input;
            kontakt.Value = Kontakt;
            myCommand.Parameters.Add(kontakt);

            SqlParameter razlogDolaska = new SqlParameter();
            razlogDolaska.ParameterName = "@RazlogDolaska";
            razlogDolaska.SqlDbType = SqlDbType.NVarChar;
            razlogDolaska.Size = 200;
            razlogDolaska.Direction = ParameterDirection.Input;
            razlogDolaska.Value = RazlogDolaska;
            myCommand.Parameters.Add(razlogDolaska);

            SqlParameter datumZakazanogDolaska = new SqlParameter();
            datumZakazanogDolaska.ParameterName = "@DatumZakazanogDolaska";
            datumZakazanogDolaska.SqlDbType = SqlDbType.DateTime;
            datumZakazanogDolaska.Direction = ParameterDirection.Input;
            datumZakazanogDolaska.Value = DatumZakazanogDolaska.HasValue ? (object)DatumZakazanogDolaska.Value : DBNull.Value;
            myCommand.Parameters.Add(datumZakazanogDolaska);

            SqlParameter kontaktUFirmi = new SqlParameter();
            kontaktUFirmi.ParameterName = "@KontaktUnutarFirme";
            kontaktUFirmi.SqlDbType = SqlDbType.NVarChar;
            kontaktUFirmi.Size = 100;
            kontaktUFirmi.Direction = ParameterDirection.Input;
            kontaktUFirmi.Value = KontaktUnutarFirme;
            myCommand.Parameters.Add(kontaktUFirmi);

            SqlParameter datumOdlaska = new SqlParameter();
            datumOdlaska.ParameterName = "@DatumOdlaska";
            datumOdlaska.SqlDbType = SqlDbType.DateTime;
            datumOdlaska.Direction = ParameterDirection.Input;
            datumOdlaska.Value = DatumOdlaska.HasValue ? (object)DatumOdlaska.Value : DBNull.Value;
            myCommand.Parameters.Add(datumOdlaska);

            SqlParameter idParam = new SqlParameter("@IDPom", SqlDbType.Int);
            idParam.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(idParam);

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
                IDPom = (int)myCommand.Parameters["@IDPom"].Value;
                error = false;
            }

            catch (SqlException ex)
            {
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                myTransaction.Rollback(); // Ne zaboravi i rollback
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos je uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
            return IDPom;

        }

        public void UpdeteKapija(int ID, DateTime? @DatumDolaska, int? Status, string Vozac, string RegistarskiBroj, string Kontakt,
                                            string RazlogDolaska, DateTime? @DatumZakazanogDolaska, string KontaktUnutarFirme, DateTime? @DatumOdlaska)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateKapija";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@ID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = ID;
            myCommand.Parameters.Add(iD);

            SqlParameter datumDolaska = new SqlParameter();
            datumDolaska.ParameterName = "@DatumDolaska";
            datumDolaska.SqlDbType = SqlDbType.DateTime;
            datumDolaska.Direction = ParameterDirection.Input;
            datumDolaska.Value = DatumDolaska.HasValue ? (object)DatumDolaska.Value : DBNull.Value;
            myCommand.Parameters.Add(datumDolaska);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@Status";
            status.SqlDbType = SqlDbType.Int;
            status.Direction = ParameterDirection.Input;
            status.Value = Status.HasValue ? (object)Status.Value : DBNull.Value;
            myCommand.Parameters.Add(status);

            SqlParameter vozac = new SqlParameter();
            vozac.ParameterName = "@Vozac";
            vozac.SqlDbType = SqlDbType.NVarChar;
            vozac.Size = 100;
            vozac.Direction = ParameterDirection.Input;
            vozac.Value = Vozac;
            myCommand.Parameters.Add(vozac);

            SqlParameter rgBroj = new SqlParameter();
            rgBroj.ParameterName = "@RegistarskiBroj";
            rgBroj.SqlDbType = SqlDbType.NVarChar;
            rgBroj.Size = 50;
            rgBroj.Direction = ParameterDirection.Input;
            rgBroj.Value = RegistarskiBroj;
            myCommand.Parameters.Add(rgBroj);

            SqlParameter kontakt = new SqlParameter();
            kontakt.ParameterName = "@Kontakt";
            kontakt.SqlDbType = SqlDbType.NVarChar;
            kontakt.Size = 100;
            kontakt.Direction = ParameterDirection.Input;
            kontakt.Value = Kontakt;
            myCommand.Parameters.Add(kontakt);

            SqlParameter razlogDolaska = new SqlParameter();
            razlogDolaska.ParameterName = "@RazlogDolaska";
            razlogDolaska.SqlDbType = SqlDbType.NVarChar;
            razlogDolaska.Size = 200;
            razlogDolaska.Direction = ParameterDirection.Input;
            razlogDolaska.Value = RazlogDolaska;
            myCommand.Parameters.Add(razlogDolaska);

            SqlParameter datumZakazanogDolaska = new SqlParameter();
            datumZakazanogDolaska.ParameterName = "@DatumZakazanogDolaska";
            datumZakazanogDolaska.SqlDbType = SqlDbType.DateTime;
            datumZakazanogDolaska.Direction = ParameterDirection.Input;
            datumZakazanogDolaska.Value = DatumZakazanogDolaska.HasValue ? (object)DatumZakazanogDolaska.Value : DBNull.Value;
            myCommand.Parameters.Add(datumZakazanogDolaska);

            SqlParameter kontaktUFirmi = new SqlParameter();
            kontaktUFirmi.ParameterName = "@KontaktUnutarFirme";
            kontaktUFirmi.SqlDbType = SqlDbType.NVarChar;
            kontaktUFirmi.Size = 100;
            kontaktUFirmi.Direction = ParameterDirection.Input;
            kontaktUFirmi.Value = KontaktUnutarFirme;
            myCommand.Parameters.Add(kontaktUFirmi);

            SqlParameter datumOdlaska = new SqlParameter();
            datumOdlaska.ParameterName = "@DatumOdlaska";
            datumOdlaska.SqlDbType = SqlDbType.DateTime;
            datumOdlaska.Direction = ParameterDirection.Input;
            datumOdlaska.Value = DatumOdlaska.HasValue ? (object)DatumOdlaska.Value : DBNull.Value;
            myCommand.Parameters.Add(datumOdlaska);

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
                error = false;
            }

            catch (SqlException)
            {
                throw new Exception("Neuspešan upis");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos je uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                myConnection.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }


        }

        public void DelKapija(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteKapija";
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
                throw new Exception("Neuspešana izmena ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Izmena uspešno završena", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }
        }
    }
    }
