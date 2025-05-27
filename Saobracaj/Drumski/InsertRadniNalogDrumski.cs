using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Pantheon_Export;
using Saobracaj.Sifarnici;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    class InsertRadniNalogDrumski
    {
        public string connect = frmLogovanje.connectionString;
        
                 //string MesoUtovara, string AdresaUtovara, string MesoIstovara, DateTime? DatumUtovara, DateTime? DatumIstovara, string AdresaIstovara,
                 //DateTime? DtPreuzimanjaPraznogKontejnera, string GranicniPrelaz, string KontaktSpeditera,
                 //decimal? Trosak, int? Valuta, int? KamionID, int? StatusID
        public void UpdateRadniNalogDrumski(int ID, int AutoDan, string Ref, string MestoPreuzimanja, string MestoUtovara, string AdresaUtovara,
                    string MestoIstovara, DateTime? DatumUtovara, DateTime? DatumIstovara, string AdresaIstovara, DateTime? DtPreuzimanjaPraznogKontejnera,
                    string GranicniPrelaz, string KontaktSpeditera, decimal? Trosak, string Valuta, int? KamionID, int? StatusID, string DodatniOpis, decimal? Cena, string KontaktNaIstovaru)
        
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRadniNalogDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@ID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = ID;
            cmd.Parameters.Add(iD);

            SqlParameter autoDan = new SqlParameter();
            autoDan.ParameterName = "@AutoDan";
            autoDan.SqlDbType = SqlDbType.Int;
            autoDan.Direction = ParameterDirection.Input;
            autoDan.Value = AutoDan;
            cmd.Parameters.Add(autoDan);

            SqlParameter ref3 = new SqlParameter();
            ref3.ParameterName = "@Ref";
            ref3.SqlDbType = SqlDbType.NVarChar;
            ref3.Size = 100;
            ref3.Direction = ParameterDirection.Input;
            ref3.Value = (object)Ref ?? DBNull.Value;
            cmd.Parameters.Add(ref3);

            SqlParameter mestoPreuzimanjaKontejnera = new SqlParameter();
            mestoPreuzimanjaKontejnera.ParameterName = "@MestoPreuzimanja";
            mestoPreuzimanjaKontejnera.SqlDbType = SqlDbType.NVarChar;
            mestoPreuzimanjaKontejnera.Size = 100;
            mestoPreuzimanjaKontejnera.Direction = ParameterDirection.Input;
            mestoPreuzimanjaKontejnera.Value = (object)MestoPreuzimanja ?? DBNull.Value;
            cmd.Parameters.Add(mestoPreuzimanjaKontejnera);

            SqlParameter mestoUtovara = new SqlParameter();
            mestoUtovara.ParameterName = "@MestoUtovara";
            mestoUtovara.SqlDbType = SqlDbType.NVarChar;
            mestoUtovara.Size = 50;
            mestoUtovara.Direction = ParameterDirection.Input;
            mestoUtovara.Value = (object)MestoUtovara ?? DBNull.Value;
            cmd.Parameters.Add(mestoUtovara);

            SqlParameter adresaUtovara = new SqlParameter();
            adresaUtovara.ParameterName = "@AdresaUtovara";
            adresaUtovara.SqlDbType = SqlDbType.NVarChar;
            adresaUtovara.Size = 100;
            adresaUtovara.Direction = ParameterDirection.Input;
            adresaUtovara.Value = (object)AdresaUtovara ?? DBNull.Value;
            cmd.Parameters.Add(adresaUtovara);

            SqlParameter mestoIstovara = new SqlParameter();
            mestoIstovara.ParameterName = "@MestoIstovara";
            mestoIstovara.SqlDbType = SqlDbType.NVarChar;
            mestoIstovara.Size = 50;
            mestoIstovara.Direction = ParameterDirection.Input;
            mestoIstovara.Value = (object)MestoIstovara ?? DBNull.Value; 
            cmd.Parameters.Add(mestoIstovara);

            SqlParameter datumUtovara = new SqlParameter();
            datumUtovara.ParameterName = "@DatumUtovara";
            datumUtovara.SqlDbType = SqlDbType.DateTime;
            datumUtovara.Direction = ParameterDirection.Input;
            datumUtovara.Value = DatumUtovara.HasValue ? (object)DatumUtovara.Value : DBNull.Value;
            cmd.Parameters.Add(datumUtovara);

            SqlParameter datumIstovara = new SqlParameter();
            datumIstovara.ParameterName = "@DatumIstovara";
            datumIstovara.SqlDbType = SqlDbType.DateTime;
            datumIstovara.Direction = ParameterDirection.Input;
            datumIstovara.Value = DatumIstovara.HasValue ? (object)DatumIstovara.Value : DBNull.Value;
            cmd.Parameters.Add(datumIstovara);

            SqlParameter adresaIstovara = new SqlParameter();
            adresaIstovara.ParameterName = "@AdresaIstovara";
            adresaIstovara.SqlDbType = SqlDbType.NVarChar;
            adresaIstovara.Size = 100;
            adresaIstovara.Direction = ParameterDirection.Input;
            adresaIstovara.Value = (object)AdresaIstovara ?? DBNull.Value;
            cmd.Parameters.Add(adresaIstovara);

            SqlParameter dtPreuzimanjaPraznogKontejnera = new SqlParameter();
            dtPreuzimanjaPraznogKontejnera.ParameterName = "@DtPreuzimanjaPraznogKontejnera";
            dtPreuzimanjaPraznogKontejnera.SqlDbType = SqlDbType.DateTime;
            dtPreuzimanjaPraznogKontejnera.Direction = ParameterDirection.Input;
            dtPreuzimanjaPraznogKontejnera.Value = DtPreuzimanjaPraznogKontejnera.HasValue ? (object)DtPreuzimanjaPraznogKontejnera.Value : DBNull.Value;
            cmd.Parameters.Add(dtPreuzimanjaPraznogKontejnera);

            SqlParameter granicniPrelaz = new SqlParameter();
            granicniPrelaz.ParameterName = "@GranicniPrelaz";
            granicniPrelaz.SqlDbType = SqlDbType.NVarChar;
            granicniPrelaz.Size = 100;
            granicniPrelaz.Direction = ParameterDirection.Input;
            granicniPrelaz.Value = (object)GranicniPrelaz ?? DBNull.Value;
            cmd.Parameters.Add(granicniPrelaz);

            SqlParameter kontaktSpeditera = new SqlParameter();
            kontaktSpeditera.ParameterName = "@KontaktSpeditera";
            kontaktSpeditera.SqlDbType = SqlDbType.NVarChar;
            kontaktSpeditera.Size = 100;
            kontaktSpeditera.Direction = ParameterDirection.Input;
            kontaktSpeditera.Value = (object)KontaktSpeditera ?? DBNull.Value;
            cmd.Parameters.Add(kontaktSpeditera);

            SqlParameter trosak = new SqlParameter();
            trosak.ParameterName = "@Trosak";
            trosak.SqlDbType = SqlDbType.Decimal;
            trosak.Direction = ParameterDirection.Input;
            trosak.Value = Trosak.HasValue ? (object)Trosak.Value : DBNull.Value;
            cmd.Parameters.Add(trosak);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Size = 50;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = (object)Valuta ?? DBNull.Value;
            cmd.Parameters.Add(valuta);

            SqlParameter kamion = new SqlParameter();
            kamion.ParameterName = "@KamionID";
            kamion.SqlDbType = SqlDbType.Int;
            kamion.Direction = ParameterDirection.Input;
            kamion.Value = KamionID.HasValue ? (object)KamionID.Value : DBNull.Value;
            cmd.Parameters.Add(kamion);

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@StatusID";
            status.SqlDbType = SqlDbType.Int;
            status.Direction = ParameterDirection.Input;
            status.Value = (StatusID.HasValue && StatusID.Value > 0) ? (object)StatusID.Value : DBNull.Value;
            cmd.Parameters.Add(status);

            SqlParameter dodatniOpis = new SqlParameter();
            dodatniOpis.ParameterName = "@DodatniOpis";
            dodatniOpis.SqlDbType = SqlDbType.NVarChar;
            dodatniOpis.Size = 100;
            dodatniOpis.Direction = ParameterDirection.Input;
            dodatniOpis.Value = (object)DodatniOpis ?? DBNull.Value;
            cmd.Parameters.Add(dodatniOpis);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena.HasValue ? (object)Cena.Value : DBNull.Value;
            cmd.Parameters.Add(cena);

            SqlParameter kontaktNaistovaru = new SqlParameter();
            kontaktNaistovaru.ParameterName = "@KontaktNaIstovaru";
            kontaktNaistovaru.SqlDbType = SqlDbType.NVarChar;
            kontaktNaistovaru.Size = 50;
            kontaktNaistovaru.Direction = ParameterDirection.Input;
            kontaktNaistovaru.Value = (object)KontaktNaIstovaru ?? DBNull.Value;
            cmd.Parameters.Add(kontaktNaistovaru);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                error = false;
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException ex)
            {
                //throw new Exception("Neuspešan upis");
                MessageBox.Show("Greška u SQL izvršavanju: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tran.Rollback(); // Ne zaboravi i rollback
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje radnog naloga broja je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }

        public void DodeliKamion(int ID, int KamionID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DodeliKamionZaRadniNalogDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter kamionID = new SqlParameter();
            kamionID.ParameterName = "@KamionID";
            kamionID.SqlDbType = SqlDbType.Int;
            kamionID.Direction = ParameterDirection.Input;
            kamionID.Value = KamionID;
            cmd.Parameters.Add(kamionID);

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
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }

        }

        public void DodeliKamionP(int ID, string RegBr)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DodeliKamionZaRadniNalogDrumskiP";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter kamionID = new SqlParameter();
            kamionID.ParameterName = "@RegBr";
            kamionID.SqlDbType = SqlDbType.NVarChar;
            kamionID.Size = 50;
            kamionID.Direction = ParameterDirection.Input;
            kamionID.Value = RegBr;
            cmd.Parameters.Add(kamionID);

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
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }

        }

        public void ObrisiKamion(int ID)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "PbrisiKamionZaRadniNalogDrumski";
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
                throw new Exception("Neuspešan upis ");
            }

            finally
            {
                if (!error)
                {
                    myTransaction.Commit();
                    MessageBox.Show("Unos uspešno završen", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                conn.Close();

                if (error)
                {
                    // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                }
            }

        }

        public void UpdateStatusRadniNalogDrumski(int ID, int? Status)
        {
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateStatusRadniNalogDrumski";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter iD = new SqlParameter();
            iD.ParameterName = "@ID";
            iD.SqlDbType = SqlDbType.Int;
            iD.Direction = ParameterDirection.Input;
            iD.Value = ID;
            cmd.Parameters.Add(iD);  

            SqlParameter status = new SqlParameter();
            status.ParameterName = "@Status";
            status.SqlDbType = SqlDbType.Int;
            status.Direction = ParameterDirection.Input;
            status.Value = Status.HasValue ? (object)Status.Value : DBNull.Value; 
            cmd.Parameters.Add(status);

            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            bool error = true;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
            }
            catch (SqlException)
            {
                throw new Exception("Neuspešan upis");
            }
            finally
            {
                if (!error)
                {
                    tran.Commit();
                    MessageBox.Show("Ažuriranje radnog naloga broja je uspešno završeno", "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            if (error)
            {
            }
        }
    }
}
