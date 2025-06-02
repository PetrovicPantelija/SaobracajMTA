using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    class InsertIzvoz
    {
        string connection = Sifarnici.frmLogovanje.connectionString;

        public void InsIzvozNHM(int IDNadredjena, int idNHM)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertIzvozNHM";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idnhm = new SqlParameter();
            idnhm.ParameterName = "@IDNHM";
            idnhm.SqlDbType = SqlDbType.Int;
            idnhm.Direction = ParameterDirection.Input;
            idnhm.Value = idNHM;
            cmd.Parameters.Add(idnhm);

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

        public void InsIzvozKonacnaNHM(int IDNadredjena, int idNHM)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertIzvozKonacnaNHM";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idnhm = new SqlParameter();
            idnhm.ParameterName = "@IDNHM";
            idnhm.SqlDbType = SqlDbType.Int;
            idnhm.Direction = ParameterDirection.Input;
            idnhm.Value = idNHM;
            cmd.Parameters.Add(idnhm);

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

        public void PrenesiKontejnerIzNerasporedjenihKamion(int KontejnerID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "spPrenesiKontejnerIzNerasporedjenihUPrijPlatforma";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@KontejnerID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = KontejnerID;
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

        public void DelIzvozKonacnaUsluga(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteIzvozKonacnaVrstaManipulacije";
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

        public void DelIzvozUsluga(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteIzvozVrstaManipulacije";
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

        public void DelIzvozSve(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteIzvozSve";
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

    

        public void IzvozOpredelio(string BrojKontejnera)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateIzvozStatus";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter brojkontejnera = new SqlParameter();
            brojkontejnera.ParameterName = "@BrojKontejnera";
            brojkontejnera.SqlDbType = SqlDbType.NVarChar;
            brojkontejnera.Size = 50;
            brojkontejnera.Direction = ParameterDirection.Input;
            brojkontejnera.Value = BrojKontejnera;
            cmd.Parameters.Add(brojkontejnera);

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

        public void IzvozOpredelioTerminal(int ID, string BrojKontejnera)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateIzvozStatusTerminal";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter brojkontejnera = new SqlParameter();
            brojkontejnera.ParameterName = "@BrojKontejnera";
            brojkontejnera.SqlDbType = SqlDbType.NVarChar;
            brojkontejnera.Size = 50;
            brojkontejnera.Direction = ParameterDirection.Input;
            brojkontejnera.Value = BrojKontejnera;
            cmd.Parameters.Add(brojkontejnera);

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

        public void UpdPlatiocaIzvozKonacnaUsluga(int ID, int Platioc)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdPlatiocaIzvozKonacnaVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter platioc = new SqlParameter();
            platioc.ParameterName = "@Platioc";
            platioc.SqlDbType = SqlDbType.Int;
            platioc.Direction = ParameterDirection.Input;
            platioc.Value = Platioc;
            cmd.Parameters.Add(platioc);

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

        public void UpdPlatiocaIzvozUsluga(int ID, int Platioc)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdPlatiocaIzvozVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter platioc = new SqlParameter();
            platioc.ParameterName = "@Platioc";
            platioc.SqlDbType = SqlDbType.Int;
            platioc.Direction = ParameterDirection.Input;
            platioc.Value = Platioc;
            cmd.Parameters.Add(platioc);

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

        public void UpdCENAIzvozKonacnaUsluga(int ID, double CENA)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdCenaIzvozKonacnaVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = CENA;
            cmd.Parameters.Add(cena);

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

        public void UpdCENAIzvozUsluga(int ID, double CENA)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdCenaIzvozVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Decimal;
            cena.Direction = ParameterDirection.Input;
            cena.Value = CENA;
            cmd.Parameters.Add(cena);

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

        public void UpdPDVIzvozUsluga(int ID, int PDV)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdPDVIzvozVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter pdv = new SqlParameter();
            pdv.ParameterName = "@PDV";
            pdv.SqlDbType = SqlDbType.Int;
            pdv.Direction = ParameterDirection.Input;
            pdv.Value = PDV;
            cmd.Parameters.Add(pdv);

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

        public void UpdPDVIzvozKonacnaUsluga(int ID, int PDV)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdPDVIzvozKonacnaVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter pdv = new SqlParameter();
            pdv.ParameterName = "@PDV";
            pdv.SqlDbType = SqlDbType.Int;
            pdv.Direction = ParameterDirection.Input;
            pdv.Value = PDV;
            cmd.Parameters.Add(pdv);

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




        public void DelIzvozNHM(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteIzvozNHM";
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

        public void DelIzvozKonacnaNHM(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteIzvozKonacnaNHM";
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
        public void InsUbaciUslugu(int IDNadredjena, int IDVrstaManipulacije, double Cena, double Kolicina, int OrgJed, int Platilac, int SaPDV, string Pokret, int StatusKontejnera, string Forma)
        {
            //  @IdNadredjena int,
            //@IDVrstaManipulacije int,
            //@Cena numeric(18, 2)

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertIzvozVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idVrstaManipulacije = new SqlParameter();
            idVrstaManipulacije.ParameterName = "@IDVrstaManipulacije";
            idVrstaManipulacije.SqlDbType = SqlDbType.Int;
            idVrstaManipulacije.Direction = ParameterDirection.Input;
            idVrstaManipulacije.Value = IDVrstaManipulacije;
            cmd.Parameters.Add(idVrstaManipulacije);


            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Int;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena;
            cmd.Parameters.Add(cena);

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);

            SqlParameter orgjed = new SqlParameter();
            orgjed.ParameterName = "@OrgJed";
            orgjed.SqlDbType = SqlDbType.Int;
            orgjed.Direction = ParameterDirection.Input;
            orgjed.Value = OrgJed;
            cmd.Parameters.Add(orgjed);

            SqlParameter platilac = new SqlParameter();
            platilac.ParameterName = "@Platilac";
            platilac.SqlDbType = SqlDbType.Int;
            platilac.Direction = ParameterDirection.Input;
            platilac.Value = Platilac;
            cmd.Parameters.Add(platilac);

            SqlParameter sapdv = new SqlParameter();
            sapdv.ParameterName = "@SaPDV";
            sapdv.SqlDbType = SqlDbType.Int;
            sapdv.Direction = ParameterDirection.Input;
            sapdv.Value = SaPDV;
            cmd.Parameters.Add(sapdv);

            SqlParameter pokret = new SqlParameter();
            pokret.ParameterName = "@Pokret";
            pokret.SqlDbType = SqlDbType.NVarChar;
            pokret.Size = 50;
            pokret.Direction = ParameterDirection.Input;
            pokret.Value = Pokret;
            cmd.Parameters.Add(pokret);

            SqlParameter statuskontejnera = new SqlParameter();
            statuskontejnera.ParameterName = "@StatusKontejnera";
            statuskontejnera.SqlDbType = SqlDbType.Int;
            statuskontejnera.Direction = ParameterDirection.Input;
            statuskontejnera.Value = StatusKontejnera;
            cmd.Parameters.Add(statuskontejnera);

            SqlParameter forma = new SqlParameter();
            forma.ParameterName = "@Forma";
            forma.SqlDbType = SqlDbType.NVarChar;
            forma.Size = 50;
            forma.Direction = ParameterDirection.Input;
            forma.Value = Forma;
            cmd.Parameters.Add(forma);

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

        public void InsUbaciUsluguKonacnaPlan(int ID, int IDNAdredjena)
        {
           

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertIzvozKonacnaVMIzUvoza";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNAdredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNAdredjena;
            cmd.Parameters.Add(idnadredjena);

           

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
        public void InsUbaciUsluguPlan(int ID, int IDNAdredjena)
        {


            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertIzvozVMIzUvoza";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNAdredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNAdredjena;
            cmd.Parameters.Add(idnadredjena);



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

        public void InsUbaciUsluguKonacna(int IDNadredjena, int IDVrstaManipulacije, double Cena, double Kolicina, int OrgJed, int Platilac, int SaPDV, string Pokret, int StatusKontejnera, string Forma)
        {
            //  @IdNadredjena int,
            //@IDVrstaManipulacije int,
            //@Cena numeric(18, 2)

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertIzvozKonacnaVrstaManipulacije";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idVrstaManipulacije = new SqlParameter();
            idVrstaManipulacije.ParameterName = "@IDVrstaManipulacije";
            idVrstaManipulacije.SqlDbType = SqlDbType.Int;
            idVrstaManipulacije.Direction = ParameterDirection.Input;
            idVrstaManipulacije.Value = IDVrstaManipulacije;
            cmd.Parameters.Add(idVrstaManipulacije);


            SqlParameter cena = new SqlParameter();
            cena.ParameterName = "@Cena";
            cena.SqlDbType = SqlDbType.Int;
            cena.Direction = ParameterDirection.Input;
            cena.Value = Cena;
            cmd.Parameters.Add(cena);

            SqlParameter kolicina = new SqlParameter();
            kolicina.ParameterName = "@Kolicina";
            kolicina.SqlDbType = SqlDbType.Decimal;
            kolicina.Direction = ParameterDirection.Input;
            kolicina.Value = Kolicina;
            cmd.Parameters.Add(kolicina);

            SqlParameter orgjed = new SqlParameter();
            orgjed.ParameterName = "@OrgJed";
            orgjed.SqlDbType = SqlDbType.Int;
            orgjed.Direction = ParameterDirection.Input;
            orgjed.Value = OrgJed;
            cmd.Parameters.Add(orgjed);

            SqlParameter platilac = new SqlParameter();
            platilac.ParameterName = "@Platilac";
            platilac.SqlDbType = SqlDbType.Int;
            platilac.Direction = ParameterDirection.Input;
            platilac.Value = Platilac;
            cmd.Parameters.Add(platilac);

            SqlParameter sapdv = new SqlParameter();
            sapdv.ParameterName = "@SaPDV";
            sapdv.SqlDbType = SqlDbType.Int;
            sapdv.Direction = ParameterDirection.Input;
            sapdv.Value = SaPDV;
            cmd.Parameters.Add(sapdv);


            SqlParameter pokret = new SqlParameter();
            pokret.ParameterName = "@Pokret";
            pokret.SqlDbType = SqlDbType.NVarChar;
            pokret.Size = 50;
            pokret.Direction = ParameterDirection.Input;
            pokret.Value = Pokret;
            cmd.Parameters.Add(pokret);

            SqlParameter statuskontejnera = new SqlParameter();
            statuskontejnera.ParameterName = "@StatusKontejnera";
            statuskontejnera.SqlDbType = SqlDbType.Int;
            statuskontejnera.Direction = ParameterDirection.Input;
            statuskontejnera.Value = StatusKontejnera;
            cmd.Parameters.Add(statuskontejnera);

            SqlParameter forma = new SqlParameter();
            forma.ParameterName = "@Forma";
            forma.SqlDbType = SqlDbType.NVarChar;
            forma.Size = 50;
            forma.Direction = ParameterDirection.Input;
            forma.Value = Forma;
            cmd.Parameters.Add(forma);


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

        public void DelIzvozVrstaRobeHS(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteIzvozVrstaRobeHS";
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

        public void InsIzvozVrstaRobeHS(int IDNadredjena, int idVrstaRobeHS)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertIzvozVrstaRobeHS";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idvrstarobehs = new SqlParameter();
            idvrstarobehs.ParameterName = "@IDVrstaRobeHS";
            idvrstarobehs.SqlDbType = SqlDbType.Int;
            idvrstarobehs.Direction = ParameterDirection.Input;
            idvrstarobehs.Value = idVrstaRobeHS;
            cmd.Parameters.Add(idvrstarobehs);

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

        public void DelIzvozKonacnaVrstaRobeHS(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteIzvozKonacnaVrstaRobeHS";
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

        public void InsIzvozKonacnaVrstaRobeHS(int IDNadredjena, int idVrstaRobeHS)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertIzvozkonacnaVrstaRobeHS";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter idvrstarobehs = new SqlParameter();
            idvrstarobehs.ParameterName = "@IDVrstaRobeHS";
            idvrstarobehs.SqlDbType = SqlDbType.Int;
            idvrstarobehs.Direction = ParameterDirection.Input;
            idvrstarobehs.Value = idVrstaRobeHS;
            cmd.Parameters.Add(idvrstarobehs);

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

        public void UpdIzvoz(int ID, string BrojVagona, string BrojKontejnera, int VrstaKontejnera, string BrodskaPlomba,
                            int BookingBrodara, int Brodar, DateTime CutOffPort, decimal NetoRobe,
                            decimal BrutoRobe, decimal BrutoRobeO, int BrojKoleta, int BrojKoletaO,
                            decimal CBM, decimal CBMO, decimal VrednostRobeFaktura, string Valuta,
                            int KrajnaDestinacija, int Postupanje, int MestoPreuzimanja, int Cirada,
                            DateTime PlaniraniDatumUtovara, int MesoUtovara, int KontaktOsoba, int MestoCarinjenja,
                            int Spedicija, string AdresaSlanjaStatusa, string NaslovSlanjaStatusa, DateTime EtaLeget,
                            int NapomenaReexport, int Inspekcija, decimal AutoDana, int NajavaVozila,
                            int NacinPakovanja, int NacinPretovara, string DodatneNapomeneDrumski, int Vaganje,
                            decimal VGMTezina, decimal Tara, decimal VGMBrod, int Izvoznik,
                            int Klijent1, int Napomena1REf, int DobijenNalogKlijent1, int Klijent2,
                            int Napomena2REf, int Klijent3, int Napomena3REf, int SpediterRijeka, string OstalePlombe, int ADR,
                            string Vozilo, string Vozac, int SpedicijaJ, DateTime PeriodSkladistenjaOd, DateTime PeriodSkladistenjaDo, 
                            int VrstaBrodskePlombe, string NapomenaZaRobu, decimal VGMBrod2, string KontaktSpeditera, string KontaktOsobe, 
                            int UvozniID, int Terminal, int Scenario, decimal TaraZ, int MestoPreuzimanja2, int MestoPreuzimanja3)
        {





            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter brojvagona = new SqlParameter();
            brojvagona.ParameterName = "@BrojVagona";
            brojvagona.SqlDbType = SqlDbType.NVarChar;
            brojvagona.Size = 30;
            brojvagona.Direction = ParameterDirection.Input;
            brojvagona.Value = BrojVagona;
            cmd.Parameters.Add(brojvagona);

            SqlParameter brojkontejnera = new SqlParameter();
            brojkontejnera.ParameterName = "@BrojKontejnera";
            brojkontejnera.SqlDbType = SqlDbType.NVarChar;
            brojkontejnera.Size = 30;
            brojkontejnera.Direction = ParameterDirection.Input;
            brojkontejnera.Value = BrojKontejnera;
            cmd.Parameters.Add(brojkontejnera);

            SqlParameter vrstakontejnera = new SqlParameter();
            vrstakontejnera.ParameterName = "@VrstaKontejnera";
            vrstakontejnera.SqlDbType = SqlDbType.Int;
            vrstakontejnera.Direction = ParameterDirection.Input;
            vrstakontejnera.Value = VrstaKontejnera;
            cmd.Parameters.Add(vrstakontejnera);

            SqlParameter brodskaplomba = new SqlParameter();
            brodskaplomba.ParameterName = "@BrodskaPlomba";
            brodskaplomba.SqlDbType = SqlDbType.NVarChar;
            brodskaplomba.Size = 30;
            brodskaplomba.Direction = ParameterDirection.Input;
            brodskaplomba.Value = BrodskaPlomba;
            cmd.Parameters.Add(brodskaplomba);

            SqlParameter bookingbrodara = new SqlParameter();
            bookingbrodara.ParameterName = "@BookingBrodara";
            bookingbrodara.SqlDbType = SqlDbType.Int;
            bookingbrodara.Direction = ParameterDirection.Input;
            bookingbrodara.Value = BookingBrodara;
            cmd.Parameters.Add(bookingbrodara);

            SqlParameter brodar = new SqlParameter();
            brodar.ParameterName = "@Brodar";
            brodar.SqlDbType = SqlDbType.Int;
            brodar.Direction = ParameterDirection.Input;
            brodar.Value = Brodar;
            cmd.Parameters.Add(brodar);

            SqlParameter cutoffPort = new SqlParameter();
            cutoffPort.ParameterName = "@CutOffPort";
            cutoffPort.SqlDbType = SqlDbType.DateTime;
            cutoffPort.Direction = ParameterDirection.Input;
            cutoffPort.Value = CutOffPort;
            cmd.Parameters.Add(cutoffPort);

            SqlParameter netorobe = new SqlParameter();
            netorobe.ParameterName = "@NetoRobe";
            netorobe.SqlDbType = SqlDbType.Decimal;
            netorobe.Direction = ParameterDirection.Input;
            netorobe.Value = NetoRobe;
            cmd.Parameters.Add(netorobe);

            SqlParameter brutorobe = new SqlParameter();
            brutorobe.ParameterName = "@BrutoRobe";
            brutorobe.SqlDbType = SqlDbType.Decimal;
            brutorobe.Direction = ParameterDirection.Input;
            brutorobe.Value = BrutoRobe;
            cmd.Parameters.Add(brutorobe);

            SqlParameter brutorobeO = new SqlParameter();
            brutorobeO.ParameterName = "@BrutoRobeO";
            brutorobeO.SqlDbType = SqlDbType.Decimal;
            brutorobeO.Direction = ParameterDirection.Input;
            brutorobeO.Value = BrutoRobeO;
            cmd.Parameters.Add(brutorobeO);

            SqlParameter brojkoleta = new SqlParameter();
            brojkoleta.ParameterName = "@BrojKoleta";
            brojkoleta.SqlDbType = SqlDbType.Int;
            brojkoleta.Direction = ParameterDirection.Input;
            brojkoleta.Value = BrojKoleta;
            cmd.Parameters.Add(brojkoleta);

            SqlParameter brojkoletaO = new SqlParameter();
            brojkoletaO.ParameterName = "@BrojKoletaO";
            brojkoletaO.SqlDbType = SqlDbType.Int;
            brojkoletaO.Direction = ParameterDirection.Input;
            brojkoletaO.Value = BrojKoletaO;
            cmd.Parameters.Add(brojkoletaO);



            SqlParameter cmb = new SqlParameter();
            cmb.ParameterName = "@CBM";
            cmb.SqlDbType = SqlDbType.Decimal;
            cmb.Direction = ParameterDirection.Input;
            cmb.Value = CBM;
            cmd.Parameters.Add(cmb);

            SqlParameter cmbO = new SqlParameter();
            cmbO.ParameterName = "@CBMO";
            cmbO.SqlDbType = SqlDbType.Decimal;
            cmbO.Direction = ParameterDirection.Input;
            cmbO.Value = CBMO;
            cmd.Parameters.Add(cmbO);

            SqlParameter vrednostrobefaktura = new SqlParameter();
            vrednostrobefaktura.ParameterName = "@VrednostRobeFaktura";
            vrednostrobefaktura.SqlDbType = SqlDbType.Decimal;
            vrednostrobefaktura.Direction = ParameterDirection.Input;
            vrednostrobefaktura.Value = VrednostRobeFaktura;
            cmd.Parameters.Add(vrednostrobefaktura);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Size = 50;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = Valuta;
            cmd.Parameters.Add(valuta);

            SqlParameter krajnaDestinacija = new SqlParameter();
            krajnaDestinacija.ParameterName = "@KrajnaDestinacija";
            krajnaDestinacija.SqlDbType = SqlDbType.Int;
            krajnaDestinacija.Direction = ParameterDirection.Input;
            krajnaDestinacija.Value = KrajnaDestinacija;
            cmd.Parameters.Add(krajnaDestinacija);

            SqlParameter postupanje = new SqlParameter();
            postupanje.ParameterName = "@Postupanje";
            postupanje.SqlDbType = SqlDbType.Int;
            postupanje.Direction = ParameterDirection.Input;
            postupanje.Value = Postupanje;
            cmd.Parameters.Add(postupanje);

            SqlParameter mestopreuzimanja = new SqlParameter();
            mestopreuzimanja.ParameterName = "@MestoPreuzimanja";
            mestopreuzimanja.SqlDbType = SqlDbType.Int;
            mestopreuzimanja.Direction = ParameterDirection.Input;
            mestopreuzimanja.Value = MestoPreuzimanja;
            cmd.Parameters.Add(mestopreuzimanja);

            SqlParameter cirada = new SqlParameter();
            cirada.ParameterName = "@Cirada";
            cirada.SqlDbType = SqlDbType.Int;
            cirada.Direction = ParameterDirection.Input;
            cirada.Value = Cirada;
            cmd.Parameters.Add(cirada);


            SqlParameter planiraniDatumUtovara = new SqlParameter();
            planiraniDatumUtovara.ParameterName = "@PlaniraniDatumUtovara";
            planiraniDatumUtovara.SqlDbType = SqlDbType.DateTime;
            planiraniDatumUtovara.Direction = ParameterDirection.Input;
            planiraniDatumUtovara.Value = PlaniraniDatumUtovara;
            cmd.Parameters.Add(planiraniDatumUtovara);

            SqlParameter mesoutovara = new SqlParameter();
            mesoutovara.ParameterName = "@MesoUtovara";
            mesoutovara.SqlDbType = SqlDbType.Int;
            mesoutovara.Direction = ParameterDirection.Input;
            mesoutovara.Value = MesoUtovara;
            cmd.Parameters.Add(mesoutovara);

            SqlParameter kontaktOsoba = new SqlParameter();
            kontaktOsoba.ParameterName = "@KontaktOsoba";
            kontaktOsoba.SqlDbType = SqlDbType.Int;
            // kontaktOsoba.Size = 50;
            kontaktOsoba.Direction = ParameterDirection.Input;
            kontaktOsoba.Value = KontaktOsoba;
            cmd.Parameters.Add(kontaktOsoba);

            SqlParameter mestocarinjenja = new SqlParameter();
            mestocarinjenja.ParameterName = "@MestoCarinjenja";
            mestocarinjenja.SqlDbType = SqlDbType.Int;
            mestocarinjenja.Direction = ParameterDirection.Input;
            mestocarinjenja.Value = MestoCarinjenja;
            cmd.Parameters.Add(mestocarinjenja);

            SqlParameter spedicija = new SqlParameter();
            spedicija.ParameterName = "@Spedicija";
            spedicija.SqlDbType = SqlDbType.Int;
            spedicija.Direction = ParameterDirection.Input;
            spedicija.Value = Spedicija;
            cmd.Parameters.Add(spedicija);

            SqlParameter adresaslanjastatusa = new SqlParameter();
            adresaslanjastatusa.ParameterName = "@AdresaSlanjaStatusa";
            adresaslanjastatusa.SqlDbType = SqlDbType.NVarChar;
            adresaslanjastatusa.Size = 100;
            adresaslanjastatusa.Direction = ParameterDirection.Input;
            adresaslanjastatusa.Value = AdresaSlanjaStatusa;
            cmd.Parameters.Add(adresaslanjastatusa);

            SqlParameter naslovSlanjaStatusa = new SqlParameter();
            naslovSlanjaStatusa.ParameterName = "@NaslovSlanjaStatusa";
            naslovSlanjaStatusa.SqlDbType = SqlDbType.NVarChar;
            naslovSlanjaStatusa.Size = 1000;
            naslovSlanjaStatusa.Direction = ParameterDirection.Input;
            naslovSlanjaStatusa.Value = NaslovSlanjaStatusa;
            cmd.Parameters.Add(naslovSlanjaStatusa);

            SqlParameter etaLeget = new SqlParameter();
            etaLeget.ParameterName = "@EtaLeget";
            etaLeget.SqlDbType = SqlDbType.DateTime;
            etaLeget.Direction = ParameterDirection.Input;
            etaLeget.Value = EtaLeget;
            cmd.Parameters.Add(etaLeget);

            SqlParameter napomenaReexport = new SqlParameter();
            napomenaReexport.ParameterName = "@NapomenaReexport";
            napomenaReexport.SqlDbType = SqlDbType.Int;
            napomenaReexport.Direction = ParameterDirection.Input;
            napomenaReexport.Value = NapomenaReexport;
            cmd.Parameters.Add(napomenaReexport);

            SqlParameter inspekcija = new SqlParameter();
            inspekcija.ParameterName = "@Inspekcija";
            inspekcija.SqlDbType = SqlDbType.Int;
            inspekcija.Direction = ParameterDirection.Input;
            inspekcija.Value = Inspekcija;
            cmd.Parameters.Add(inspekcija);

            SqlParameter autoDana = new SqlParameter();
            autoDana.ParameterName = "@AutoDana";
            autoDana.SqlDbType = SqlDbType.Int;
            autoDana.Direction = ParameterDirection.Input;
            autoDana.Value = AutoDana;
            cmd.Parameters.Add(autoDana);

            SqlParameter najavaVozila = new SqlParameter();
            najavaVozila.ParameterName = "@NajavaVozila";
            najavaVozila.SqlDbType = SqlDbType.Int;
            najavaVozila.Direction = ParameterDirection.Input;
            najavaVozila.Value = NajavaVozila;
            cmd.Parameters.Add(najavaVozila);

            SqlParameter nacinPakovanja = new SqlParameter();
            nacinPakovanja.ParameterName = "@NacinPakovanja";
            nacinPakovanja.SqlDbType = SqlDbType.Int;
            nacinPakovanja.Direction = ParameterDirection.Input;
            nacinPakovanja.Value = NacinPakovanja;
            cmd.Parameters.Add(nacinPakovanja);

            SqlParameter nacinPretovara = new SqlParameter();
            nacinPretovara.ParameterName = "@NacinPretovara";
            nacinPretovara.SqlDbType = SqlDbType.Int;
            nacinPretovara.Direction = ParameterDirection.Input;
            nacinPretovara.Value = NacinPretovara;
            cmd.Parameters.Add(nacinPretovara);

            SqlParameter dodatneNapomeneDrumski = new SqlParameter();
            dodatneNapomeneDrumski.ParameterName = "@DodatneNapomeneDrumski";
            dodatneNapomeneDrumski.SqlDbType = SqlDbType.NVarChar;
            dodatneNapomeneDrumski.Size = 500;
            dodatneNapomeneDrumski.Direction = ParameterDirection.Input;
            dodatneNapomeneDrumski.Value = DodatneNapomeneDrumski;
            cmd.Parameters.Add(dodatneNapomeneDrumski);

            SqlParameter vaganje = new SqlParameter();
            vaganje.ParameterName = "@Vaganje";
            vaganje.SqlDbType = SqlDbType.Int;
            vaganje.Direction = ParameterDirection.Input;
            vaganje.Value = Vaganje;
            cmd.Parameters.Add(vaganje);

            SqlParameter vGMTezina = new SqlParameter();
            vGMTezina.ParameterName = "@VGMTezina";
            vGMTezina.SqlDbType = SqlDbType.Decimal;
            vGMTezina.Direction = ParameterDirection.Input;
            vGMTezina.Value = VGMTezina;
            cmd.Parameters.Add(vGMTezina);

            SqlParameter tara = new SqlParameter();
            tara.ParameterName = "@Tara";
            tara.SqlDbType = SqlDbType.Decimal;
            tara.Direction = ParameterDirection.Input;
            tara.Value = Tara;
            cmd.Parameters.Add(tara);

            SqlParameter vGMBrod = new SqlParameter();
            vGMBrod.ParameterName = "@VGMBrod";
            vGMBrod.SqlDbType = SqlDbType.Decimal;
            vGMBrod.Direction = ParameterDirection.Input;
            vGMBrod.Value = VGMBrod;
            cmd.Parameters.Add(vGMBrod);

            SqlParameter izvoznik = new SqlParameter();
            izvoznik.ParameterName = "@Izvoznik";
            izvoznik.SqlDbType = SqlDbType.Int;
            izvoznik.Direction = ParameterDirection.Input;
            izvoznik.Value = Izvoznik;
            cmd.Parameters.Add(izvoznik);

            SqlParameter klijent1 = new SqlParameter();
            klijent1.ParameterName = "@Klijent1";
            klijent1.SqlDbType = SqlDbType.Int;
            klijent1.Direction = ParameterDirection.Input;
            klijent1.Value = Klijent1;
            cmd.Parameters.Add(klijent1);

            SqlParameter napomena1REf = new SqlParameter();
            napomena1REf.ParameterName = "@Napomena1REf";
            napomena1REf.SqlDbType = SqlDbType.Int;
            napomena1REf.Direction = ParameterDirection.Input;
            napomena1REf.Value = Napomena1REf;
            cmd.Parameters.Add(napomena1REf);

            SqlParameter dobijenNalogKlijent1 = new SqlParameter();
            dobijenNalogKlijent1.ParameterName = "@DobijenNalogKlijent1";
            dobijenNalogKlijent1.SqlDbType = SqlDbType.Int;
            dobijenNalogKlijent1.Direction = ParameterDirection.Input;
            dobijenNalogKlijent1.Value = DobijenNalogKlijent1;
            cmd.Parameters.Add(dobijenNalogKlijent1);

            SqlParameter klijent2 = new SqlParameter();
            klijent2.ParameterName = "@Klijent2";
            klijent2.SqlDbType = SqlDbType.Int;
            klijent2.Direction = ParameterDirection.Input;
            klijent2.Value = Klijent2;
            cmd.Parameters.Add(klijent2);

            SqlParameter napomena2REf = new SqlParameter();
            napomena2REf.ParameterName = "@Napomena2REf";
            napomena2REf.SqlDbType = SqlDbType.Int;
            napomena2REf.Direction = ParameterDirection.Input;
            napomena2REf.Value = Napomena2REf;
            cmd.Parameters.Add(napomena2REf);

            SqlParameter klijent3 = new SqlParameter();
            klijent3.ParameterName = "@Klijent3";
            klijent3.SqlDbType = SqlDbType.Int;
            klijent3.Direction = ParameterDirection.Input;
            klijent3.Value = Klijent3;
            cmd.Parameters.Add(klijent3);

            SqlParameter napomena3REf = new SqlParameter();
            napomena3REf.ParameterName = "@Napomena3REf";
            napomena3REf.SqlDbType = SqlDbType.Int;
            napomena3REf.Direction = ParameterDirection.Input;
            napomena3REf.Value = Napomena3REf;
            cmd.Parameters.Add(napomena3REf);

            SqlParameter spediterRijeka = new SqlParameter();
            spediterRijeka.ParameterName = "@SpediterRijeka";
            spediterRijeka.SqlDbType = SqlDbType.Int;
            spediterRijeka.Direction = ParameterDirection.Input;
            spediterRijeka.Value = SpediterRijeka;
            cmd.Parameters.Add(spediterRijeka);

            SqlParameter ostaleplombe = new SqlParameter();
            ostaleplombe.ParameterName = "@OstalePlombe";
            ostaleplombe.SqlDbType = SqlDbType.NVarChar;
            ostaleplombe.Size = 50;
            ostaleplombe.Direction = ParameterDirection.Input;
            ostaleplombe.Value = OstalePlombe;
            cmd.Parameters.Add(ostaleplombe);

            SqlParameter adr = new SqlParameter();
            adr.ParameterName = "@ADR";
            adr.SqlDbType = SqlDbType.Int;
            adr.Direction = ParameterDirection.Input;
            adr.Value = ADR;
            cmd.Parameters.Add(adr);

            SqlParameter vozilo = new SqlParameter();
            vozilo.ParameterName = "@Vozilo";
            vozilo.SqlDbType = SqlDbType.NVarChar;
            vozilo.Size = 50;
            vozilo.Direction = ParameterDirection.Input;
            vozilo.Value = Vozilo;
            cmd.Parameters.Add(vozilo);

            SqlParameter vozac = new SqlParameter();
            vozac.ParameterName = "@Vozac";
            vozac.SqlDbType = SqlDbType.NVarChar;
            vozac.Size = 50;
            vozac.Direction = ParameterDirection.Input;
            vozac.Value = Vozac;
            cmd.Parameters.Add(vozac);

            SqlParameter spedicijaJ = new SqlParameter();
            spedicijaJ.ParameterName = "@SpedicijaJ";
            spedicijaJ.SqlDbType = SqlDbType.Int;
            spedicijaJ.Direction = ParameterDirection.Input;
            spedicijaJ.Value = SpedicijaJ;
            cmd.Parameters.Add(spedicijaJ);

            SqlParameter periodskladistenjaOd = new SqlParameter();
            periodskladistenjaOd.ParameterName = "@PeriodSkladistenjaOd";
            periodskladistenjaOd.SqlDbType = SqlDbType.DateTime;
            periodskladistenjaOd.Direction = ParameterDirection.Input;
            periodskladistenjaOd.Value = PeriodSkladistenjaOd;
            cmd.Parameters.Add(periodskladistenjaOd);

            SqlParameter periodskladistenjaDo = new SqlParameter();
            periodskladistenjaDo.ParameterName = "@PeriodSkladistenjaDo";
            periodskladistenjaDo.SqlDbType = SqlDbType.DateTime;
            periodskladistenjaDo.Direction = ParameterDirection.Input;
            periodskladistenjaDo.Value = PeriodSkladistenjaDo;
            cmd.Parameters.Add(periodskladistenjaDo);


            SqlParameter vrstabrodplombe = new SqlParameter();
            vrstabrodplombe.ParameterName = "@VrstaBrodskePlombe";
            vrstabrodplombe.SqlDbType = SqlDbType.Int;
            vrstabrodplombe.Direction = ParameterDirection.Input;
            vrstabrodplombe.Value = VrstaBrodskePlombe;
            cmd.Parameters.Add(vrstabrodplombe);


            SqlParameter napomenazarobu = new SqlParameter();
            napomenazarobu.ParameterName = "@NapomenaZaRobu";
            napomenazarobu.SqlDbType = SqlDbType.NVarChar;
            napomenazarobu.Size = 500;
            napomenazarobu.Direction = ParameterDirection.Input;
            napomenazarobu.Value = NapomenaZaRobu;
            cmd.Parameters.Add(napomenazarobu);

            SqlParameter vgmbrod2 = new SqlParameter();
            vgmbrod2.ParameterName = "@VGMBrod2";
            vgmbrod2.SqlDbType = SqlDbType.Decimal;
            vgmbrod2.Direction = ParameterDirection.Input;
            vgmbrod2.Value = VGMBrod2;
            cmd.Parameters.Add(vgmbrod2);


            SqlParameter kontaktspeditera = new SqlParameter();
            kontaktspeditera.ParameterName = "@KontaktSpeditera";
            kontaktspeditera.SqlDbType = SqlDbType.NVarChar;
            kontaktspeditera.Size = 100;
            kontaktspeditera.Direction = ParameterDirection.Input;
            kontaktspeditera.Value = KontaktSpeditera;
            cmd.Parameters.Add(kontaktspeditera);


            SqlParameter kontaktosobe = new SqlParameter();
            kontaktosobe.ParameterName = "@KontaktOsobe";
            kontaktosobe.SqlDbType = SqlDbType.NVarChar;
            kontaktosobe.Size = 500;
            kontaktosobe.Direction = ParameterDirection.Input;
            kontaktosobe.Value = KontaktOsobe;
            cmd.Parameters.Add(kontaktosobe);
           
            SqlParameter uvozniid = new SqlParameter();
            uvozniid.ParameterName = "@UvozniID";
            uvozniid.SqlDbType = SqlDbType.Int;
            uvozniid.Direction = ParameterDirection.Input;
            uvozniid.Value = UvozniID;
            cmd.Parameters.Add(uvozniid);

            SqlParameter terminal = new SqlParameter();
            terminal.ParameterName = "@Terminal";
            terminal.SqlDbType = SqlDbType.Int;
            terminal.Direction = ParameterDirection.Input;
            terminal.Value = Terminal;
            cmd.Parameters.Add(terminal);

            SqlParameter scenario = new SqlParameter();
            scenario.ParameterName = "@Scenario";
            scenario.SqlDbType = SqlDbType.Int;
            scenario.Direction = ParameterDirection.Input;
            scenario.Value = Scenario;
            cmd.Parameters.Add(scenario);

            SqlParameter taraz = new SqlParameter();
            taraz.ParameterName = "@TaraZ";
            taraz.SqlDbType = SqlDbType.Decimal;
            taraz.Direction = ParameterDirection.Input;
            taraz.Value = TaraZ;
            cmd.Parameters.Add(taraz);

            SqlParameter mestopreuzimanja2 = new SqlParameter();
            mestopreuzimanja2.ParameterName = "@MestoPreuzimanja2";
            mestopreuzimanja2.SqlDbType = SqlDbType.Int;
            mestopreuzimanja2.Direction = ParameterDirection.Input;
            mestopreuzimanja2.Value = MestoPreuzimanja2;
            cmd.Parameters.Add(mestopreuzimanja2);

            SqlParameter mestopreuzimanja3 = new SqlParameter();
            mestopreuzimanja3.ParameterName = "@MestoPreuzimanja3";
            mestopreuzimanja3.SqlDbType = SqlDbType.Int;
            mestopreuzimanja3.Direction = ParameterDirection.Input;
            mestopreuzimanja3.Value = MestoPreuzimanja3;
            cmd.Parameters.Add(mestopreuzimanja3);






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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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

        public void UpdIzvozKonacna(int ID, string BrojVagona, string BrojKontejnera, int VrstaKontejnera, string BrodskaPlomba,
                           int BookingBrodara, int Brodar, DateTime CutOffPort, decimal NetoRobe,
                           decimal BrutoRobe, decimal BrutoRobeO, int BrojKoleta, int BrojKoletaO,
                           decimal CBM, decimal CBMO, decimal VrednostRobeFaktura, string Valuta,
                           int KrajnaDestinacija, int Postupanje, int MestoPreuzimanja, int Cirada,
                           DateTime PlaniraniDatumUtovara, int MesoUtovara, string KontaktOsoba, int MestoCarinjenja,
                           int Spedicija, string AdresaSlanjaStatusa, string NaslovSlanjaStatusa, DateTime EtaLeget,
                           int NapomenaReexport, int Inspekcija, decimal AutoDana, int NajavaVozila,
                           int NacinPakovanja, int NacinPretovara, string DodatneNapomeneDrumski, int Vaganje,
                           decimal VGMTezina, decimal Tara, decimal VGMBrod, int Izvoznik,
                           int Klijent1, int Napomena1REf, int DobijenNalogKlijent1, int Klijent2,
                           int Napomena2REf, int Klijent3, int Napomena3REf, int SpediterRijeka, string OstalePlombe, int ADR, int IDNadredjena, string Vozilo, string Vozac, int SpedicijaJ, DateTime PeriodSkladistenjaOd, DateTime PeriodSkladistenjaDo, int VrstaBrodskePlombe, string NapomenaZaRobu, decimal VGMBrod2, string KontaktSpeditera, string KontaktOsobe, int UvozniID, int Terminal, int Scenario, decimal TaraZ,int MestoPreuzimanja2, int MestoPreuzimanja3)
        {





            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateIzvozKonacna";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter brojvagona = new SqlParameter();
            brojvagona.ParameterName = "@BrojVagona";
            brojvagona.SqlDbType = SqlDbType.NVarChar;
            brojvagona.Size = 30;
            brojvagona.Direction = ParameterDirection.Input;
            brojvagona.Value = BrojVagona;
            cmd.Parameters.Add(brojvagona);

            SqlParameter brojkontejnera = new SqlParameter();
            brojkontejnera.ParameterName = "@BrojKontejnera";
            brojkontejnera.SqlDbType = SqlDbType.NVarChar;
            brojkontejnera.Size = 30;
            brojkontejnera.Direction = ParameterDirection.Input;
            brojkontejnera.Value = BrojKontejnera;
            cmd.Parameters.Add(brojkontejnera);

            SqlParameter vrstakontejnera = new SqlParameter();
            vrstakontejnera.ParameterName = "@VrstaKontejnera";
            vrstakontejnera.SqlDbType = SqlDbType.Int;
            vrstakontejnera.Direction = ParameterDirection.Input;
            vrstakontejnera.Value = VrstaKontejnera;
            cmd.Parameters.Add(vrstakontejnera);

            SqlParameter brodskaplomba = new SqlParameter();
            brodskaplomba.ParameterName = "@BrodskaPlomba";
            brodskaplomba.SqlDbType = SqlDbType.NVarChar;
            brodskaplomba.Size = 30;
            brodskaplomba.Direction = ParameterDirection.Input;
            brodskaplomba.Value = BrodskaPlomba;
            cmd.Parameters.Add(brodskaplomba);

            SqlParameter bookingbrodara = new SqlParameter();
            bookingbrodara.ParameterName = "@BookingBrodara";
            bookingbrodara.SqlDbType = SqlDbType.Int;
            bookingbrodara.Direction = ParameterDirection.Input;
            bookingbrodara.Value = BookingBrodara;
            cmd.Parameters.Add(bookingbrodara);

            SqlParameter brodar = new SqlParameter();
            brodar.ParameterName = "@Brodar";
            brodar.SqlDbType = SqlDbType.Int;
            brodar.Direction = ParameterDirection.Input;
            brodar.Value = Brodar;
            cmd.Parameters.Add(brodar);

            SqlParameter cutoffPort = new SqlParameter();
            cutoffPort.ParameterName = "@CutOffPort";
            cutoffPort.SqlDbType = SqlDbType.DateTime;
            cutoffPort.Direction = ParameterDirection.Input;
            cutoffPort.Value = CutOffPort;
            cmd.Parameters.Add(cutoffPort);

            SqlParameter netorobe = new SqlParameter();
            netorobe.ParameterName = "@NetoRobe";
            netorobe.SqlDbType = SqlDbType.Decimal;
            netorobe.Direction = ParameterDirection.Input;
            netorobe.Value = NetoRobe;
            cmd.Parameters.Add(netorobe);

            SqlParameter brutorobe = new SqlParameter();
            brutorobe.ParameterName = "@BrutoRobe";
            brutorobe.SqlDbType = SqlDbType.Decimal;
            brutorobe.Direction = ParameterDirection.Input;
            brutorobe.Value = BrutoRobe;
            cmd.Parameters.Add(brutorobe);

            SqlParameter brutorobeO = new SqlParameter();
            brutorobeO.ParameterName = "@BrutoRobeO";
            brutorobeO.SqlDbType = SqlDbType.Decimal;
            brutorobeO.Direction = ParameterDirection.Input;
            brutorobeO.Value = BrutoRobeO;
            cmd.Parameters.Add(brutorobeO);

            SqlParameter brojkoleta = new SqlParameter();
            brojkoleta.ParameterName = "@BrojKoleta";
            brojkoleta.SqlDbType = SqlDbType.Int;
            brojkoleta.Direction = ParameterDirection.Input;
            brojkoleta.Value = BrojKoleta;
            cmd.Parameters.Add(brojkoleta);

            SqlParameter brojkoletaO = new SqlParameter();
            brojkoletaO.ParameterName = "@BrojKoletaO";
            brojkoletaO.SqlDbType = SqlDbType.Int;
            brojkoletaO.Direction = ParameterDirection.Input;
            brojkoletaO.Value = BrojKoletaO;
            cmd.Parameters.Add(brojkoletaO);



            SqlParameter cmb = new SqlParameter();
            cmb.ParameterName = "@CBM";
            cmb.SqlDbType = SqlDbType.Decimal;
            cmb.Direction = ParameterDirection.Input;
            cmb.Value = CBM;
            cmd.Parameters.Add(cmb);

            SqlParameter cmbO = new SqlParameter();
            cmbO.ParameterName = "@CBMO";
            cmbO.SqlDbType = SqlDbType.Decimal;
            cmbO.Direction = ParameterDirection.Input;
            cmbO.Value = CBMO;
            cmd.Parameters.Add(cmbO);

            SqlParameter vrednostrobefaktura = new SqlParameter();
            vrednostrobefaktura.ParameterName = "@VrednostRobeFaktura";
            vrednostrobefaktura.SqlDbType = SqlDbType.Decimal;
            vrednostrobefaktura.Direction = ParameterDirection.Input;
            vrednostrobefaktura.Value = VrednostRobeFaktura;
            cmd.Parameters.Add(vrednostrobefaktura);

            SqlParameter valuta = new SqlParameter();
            valuta.ParameterName = "@Valuta";
            valuta.SqlDbType = SqlDbType.NVarChar;
            valuta.Size = 50;
            valuta.Direction = ParameterDirection.Input;
            valuta.Value = Valuta;
            cmd.Parameters.Add(valuta);

            SqlParameter krajnaDestinacija = new SqlParameter();
            krajnaDestinacija.ParameterName = "@KrajnaDestinacija";
            krajnaDestinacija.SqlDbType = SqlDbType.Int;
            krajnaDestinacija.Direction = ParameterDirection.Input;
            krajnaDestinacija.Value = KrajnaDestinacija;
            cmd.Parameters.Add(krajnaDestinacija);

            SqlParameter postupanje = new SqlParameter();
            postupanje.ParameterName = "@Postupanje";
            postupanje.SqlDbType = SqlDbType.Int;
            postupanje.Direction = ParameterDirection.Input;
            postupanje.Value = Postupanje;
            cmd.Parameters.Add(postupanje);

            SqlParameter mestopreuzimanja = new SqlParameter();
            mestopreuzimanja.ParameterName = "@MestoPreuzimanja";
            mestopreuzimanja.SqlDbType = SqlDbType.Int;
            mestopreuzimanja.Direction = ParameterDirection.Input;
            mestopreuzimanja.Value = MestoPreuzimanja;
            cmd.Parameters.Add(mestopreuzimanja);

            SqlParameter cirada = new SqlParameter();
            cirada.ParameterName = "@Cirada";
            cirada.SqlDbType = SqlDbType.Int;
            cirada.Direction = ParameterDirection.Input;
            cirada.Value = Cirada;
            cmd.Parameters.Add(cirada);


            SqlParameter planiraniDatumUtovara = new SqlParameter();
            planiraniDatumUtovara.ParameterName = "@PlaniraniDatumUtovara";
            planiraniDatumUtovara.SqlDbType = SqlDbType.DateTime;
            planiraniDatumUtovara.Direction = ParameterDirection.Input;
            planiraniDatumUtovara.Value = PlaniraniDatumUtovara;
            cmd.Parameters.Add(planiraniDatumUtovara);

            SqlParameter mesoutovara = new SqlParameter();
            mesoutovara.ParameterName = "@MesoUtovara";
            mesoutovara.SqlDbType = SqlDbType.Int;
            mesoutovara.Direction = ParameterDirection.Input;
            mesoutovara.Value = MesoUtovara;
            cmd.Parameters.Add(mesoutovara);

            SqlParameter kontaktOsoba = new SqlParameter();
            kontaktOsoba.ParameterName = "@KontaktOsoba";
            kontaktOsoba.SqlDbType = SqlDbType.NVarChar;
            kontaktOsoba.Size = 50;
            kontaktOsoba.Direction = ParameterDirection.Input;
            kontaktOsoba.Value = KontaktOsoba;
            cmd.Parameters.Add(kontaktOsoba);

            SqlParameter mestocarinjenja = new SqlParameter();
            mestocarinjenja.ParameterName = "@MestoCarinjenja";
            mestocarinjenja.SqlDbType = SqlDbType.Int;
            mestocarinjenja.Direction = ParameterDirection.Input;
            mestocarinjenja.Value = MestoCarinjenja;
            cmd.Parameters.Add(mestocarinjenja);

            SqlParameter spedicija = new SqlParameter();
            spedicija.ParameterName = "@Spedicija";
            spedicija.SqlDbType = SqlDbType.Int;
            spedicija.Direction = ParameterDirection.Input;
            spedicija.Value = Spedicija;
            cmd.Parameters.Add(spedicija);

            SqlParameter adresaslanjastatusa = new SqlParameter();
            adresaslanjastatusa.ParameterName = "@AdresaSlanjaStatusa";
            adresaslanjastatusa.SqlDbType = SqlDbType.NVarChar;
            adresaslanjastatusa.Size = 100;
            adresaslanjastatusa.Direction = ParameterDirection.Input;
            adresaslanjastatusa.Value = AdresaSlanjaStatusa;
            cmd.Parameters.Add(adresaslanjastatusa);

            SqlParameter naslovSlanjaStatusa = new SqlParameter();
            naslovSlanjaStatusa.ParameterName = "@NaslovSlanjaStatusa";
            naslovSlanjaStatusa.SqlDbType = SqlDbType.NVarChar;
            naslovSlanjaStatusa.Size = 1000;
            naslovSlanjaStatusa.Direction = ParameterDirection.Input;
            naslovSlanjaStatusa.Value = NaslovSlanjaStatusa;
            cmd.Parameters.Add(naslovSlanjaStatusa);

            SqlParameter etaLeget = new SqlParameter();
            etaLeget.ParameterName = "@EtaLeget";
            etaLeget.SqlDbType = SqlDbType.DateTime;
            etaLeget.Direction = ParameterDirection.Input;
            etaLeget.Value = EtaLeget;
            cmd.Parameters.Add(etaLeget);

            SqlParameter napomenaReexport = new SqlParameter();
            napomenaReexport.ParameterName = "@NapomenaReexport";
            napomenaReexport.SqlDbType = SqlDbType.Int;
            napomenaReexport.Direction = ParameterDirection.Input;
            napomenaReexport.Value = NapomenaReexport;
            cmd.Parameters.Add(napomenaReexport);

            SqlParameter inspekcija = new SqlParameter();
            inspekcija.ParameterName = "@Inspekcija";
            inspekcija.SqlDbType = SqlDbType.Int;
            inspekcija.Direction = ParameterDirection.Input;
            inspekcija.Value = Inspekcija;
            cmd.Parameters.Add(inspekcija);

            SqlParameter autoDana = new SqlParameter();
            autoDana.ParameterName = "@AutoDana";
            autoDana.SqlDbType = SqlDbType.Int;
            autoDana.Direction = ParameterDirection.Input;
            autoDana.Value = AutoDana;
            cmd.Parameters.Add(autoDana);

            SqlParameter najavaVozila = new SqlParameter();
            najavaVozila.ParameterName = "@NajavaVozila";
            najavaVozila.SqlDbType = SqlDbType.Int;
            najavaVozila.Direction = ParameterDirection.Input;
            najavaVozila.Value = NajavaVozila;
            cmd.Parameters.Add(najavaVozila);

            SqlParameter nacinPakovanja = new SqlParameter();
            nacinPakovanja.ParameterName = "@NacinPakovanja";
            nacinPakovanja.SqlDbType = SqlDbType.Int;
            nacinPakovanja.Direction = ParameterDirection.Input;
            nacinPakovanja.Value = NacinPakovanja;
            cmd.Parameters.Add(nacinPakovanja);

            SqlParameter nacinPretovara = new SqlParameter();
            nacinPretovara.ParameterName = "@NacinPretovara";
            nacinPretovara.SqlDbType = SqlDbType.Int;
            nacinPretovara.Direction = ParameterDirection.Input;
            nacinPretovara.Value = NacinPretovara;
            cmd.Parameters.Add(nacinPretovara);

            SqlParameter dodatneNapomeneDrumski = new SqlParameter();
            dodatneNapomeneDrumski.ParameterName = "@DodatneNapomeneDrumski";
            dodatneNapomeneDrumski.SqlDbType = SqlDbType.NVarChar;
            dodatneNapomeneDrumski.Size = 500;
            dodatneNapomeneDrumski.Direction = ParameterDirection.Input;
            dodatneNapomeneDrumski.Value = DodatneNapomeneDrumski;
            cmd.Parameters.Add(dodatneNapomeneDrumski);

            SqlParameter vaganje = new SqlParameter();
            vaganje.ParameterName = "@Vaganje";
            vaganje.SqlDbType = SqlDbType.Int;
            vaganje.Direction = ParameterDirection.Input;
            vaganje.Value = Vaganje;
            cmd.Parameters.Add(vaganje);

            SqlParameter vGMTezina = new SqlParameter();
            vGMTezina.ParameterName = "@VGMTezina";
            vGMTezina.SqlDbType = SqlDbType.Decimal;
            vGMTezina.Direction = ParameterDirection.Input;
            vGMTezina.Value = VGMTezina;
            cmd.Parameters.Add(vGMTezina);

            SqlParameter tara = new SqlParameter();
            tara.ParameterName = "@Tara";
            tara.SqlDbType = SqlDbType.Decimal;
            tara.Direction = ParameterDirection.Input;
            tara.Value = Tara;
            cmd.Parameters.Add(tara);

            SqlParameter vGMBrod = new SqlParameter();
            vGMBrod.ParameterName = "@VGMBrod";
            vGMBrod.SqlDbType = SqlDbType.Decimal;
            vGMBrod.Direction = ParameterDirection.Input;
            vGMBrod.Value = VGMBrod;
            cmd.Parameters.Add(vGMBrod);

            SqlParameter izvoznik = new SqlParameter();
            izvoznik.ParameterName = "@Izvoznik";
            izvoznik.SqlDbType = SqlDbType.Int;
            izvoznik.Direction = ParameterDirection.Input;
            izvoznik.Value = Izvoznik;
            cmd.Parameters.Add(izvoznik);

            SqlParameter klijent1 = new SqlParameter();
            klijent1.ParameterName = "@Klijent1";
            klijent1.SqlDbType = SqlDbType.Int;
            klijent1.Direction = ParameterDirection.Input;
            klijent1.Value = Klijent1;
            cmd.Parameters.Add(klijent1);

            SqlParameter napomena1REf = new SqlParameter();
            napomena1REf.ParameterName = "@Napomena1REf";
            napomena1REf.SqlDbType = SqlDbType.Int;
            napomena1REf.Direction = ParameterDirection.Input;
            napomena1REf.Value = Napomena1REf;
            cmd.Parameters.Add(napomena1REf);

            SqlParameter dobijenNalogKlijent1 = new SqlParameter();
            dobijenNalogKlijent1.ParameterName = "@DobijenNalogKlijent1";
            dobijenNalogKlijent1.SqlDbType = SqlDbType.Int;
            dobijenNalogKlijent1.Direction = ParameterDirection.Input;
            dobijenNalogKlijent1.Value = DobijenNalogKlijent1;
            cmd.Parameters.Add(dobijenNalogKlijent1);

            SqlParameter klijent2 = new SqlParameter();
            klijent2.ParameterName = "@Klijent2";
            klijent2.SqlDbType = SqlDbType.Int;
            klijent2.Direction = ParameterDirection.Input;
            klijent2.Value = Klijent2;
            cmd.Parameters.Add(klijent2);

            SqlParameter napomena2REf = new SqlParameter();
            napomena2REf.ParameterName = "@Napomena2REf";
            napomena2REf.SqlDbType = SqlDbType.Int;
            napomena2REf.Direction = ParameterDirection.Input;
            napomena2REf.Value = Napomena2REf;
            cmd.Parameters.Add(napomena2REf);

            SqlParameter klijent3 = new SqlParameter();
            klijent3.ParameterName = "@Klijent3";
            klijent3.SqlDbType = SqlDbType.Int;
            klijent3.Direction = ParameterDirection.Input;
            klijent3.Value = Klijent3;
            cmd.Parameters.Add(klijent3);

            SqlParameter napomena3REf = new SqlParameter();
            napomena3REf.ParameterName = "@Napomena3REf";
            napomena3REf.SqlDbType = SqlDbType.Int;
            napomena3REf.Direction = ParameterDirection.Input;
            napomena3REf.Value = Napomena3REf;
            cmd.Parameters.Add(napomena3REf);

            SqlParameter spediterRijeka = new SqlParameter();
            spediterRijeka.ParameterName = "@SpediterRijeka";
            spediterRijeka.SqlDbType = SqlDbType.Int;
            spediterRijeka.Direction = ParameterDirection.Input;
            spediterRijeka.Value = SpediterRijeka;
            cmd.Parameters.Add(spediterRijeka);

            SqlParameter ostaleplombe = new SqlParameter();
            ostaleplombe.ParameterName = "@OstalePlombe";
            ostaleplombe.SqlDbType = SqlDbType.NVarChar;
            ostaleplombe.Size = 50;
            ostaleplombe.Direction = ParameterDirection.Input;
            ostaleplombe.Value = OstalePlombe;
            cmd.Parameters.Add(ostaleplombe);

            SqlParameter adr = new SqlParameter();
            adr.ParameterName = "@ADR";
            adr.SqlDbType = SqlDbType.Int;
            adr.Direction = ParameterDirection.Input;
            adr.Value = ADR;
            cmd.Parameters.Add(adr);

            SqlParameter idnadredjena = new SqlParameter();
            idnadredjena.ParameterName = "@IDNadredjena";
            idnadredjena.SqlDbType = SqlDbType.Int;
            idnadredjena.Direction = ParameterDirection.Input;
            idnadredjena.Value = IDNadredjena;
            cmd.Parameters.Add(idnadredjena);

            SqlParameter vozilo = new SqlParameter();
            vozilo.ParameterName = "@Vozilo";
            vozilo.SqlDbType = SqlDbType.NVarChar;
            vozilo.Size = 50;
            vozilo.Direction = ParameterDirection.Input;
            vozilo.Value = Vozilo;
            cmd.Parameters.Add(vozilo);

            SqlParameter vozac = new SqlParameter();
            vozac.ParameterName = "@Vozac";
            vozac.SqlDbType = SqlDbType.NVarChar;
            vozac.Size = 50;
            vozac.Direction = ParameterDirection.Input;
            vozac.Value = Vozac;
            cmd.Parameters.Add(vozac);

            SqlParameter spedicijaJ = new SqlParameter();
            spedicijaJ.ParameterName = "@SpedicijaJ";
            spedicijaJ.SqlDbType = SqlDbType.Int;
            spedicijaJ.Direction = ParameterDirection.Input;
            spedicijaJ.Value = SpedicijaJ;
            cmd.Parameters.Add(spedicijaJ);

            SqlParameter periodskladistenjaOd = new SqlParameter();
            periodskladistenjaOd.ParameterName = "@PeriodSkladistenjaOd";
            periodskladistenjaOd.SqlDbType = SqlDbType.DateTime;
            periodskladistenjaOd.Direction = ParameterDirection.Input;
            periodskladistenjaOd.Value = PeriodSkladistenjaOd;
            cmd.Parameters.Add(periodskladistenjaOd);

            SqlParameter periodskladistenjaDo = new SqlParameter();
            periodskladistenjaDo.ParameterName = "@PeriodSkladistenjaDo";
            periodskladistenjaDo.SqlDbType = SqlDbType.DateTime;
            periodskladistenjaDo.Direction = ParameterDirection.Input;
            periodskladistenjaDo.Value = PeriodSkladistenjaDo;
            cmd.Parameters.Add(periodskladistenjaDo);


            SqlParameter vrstabrodplombe = new SqlParameter();
            vrstabrodplombe.ParameterName = "@VrstaBrodskePlombe";
            vrstabrodplombe.SqlDbType = SqlDbType.Int;
            vrstabrodplombe.Direction = ParameterDirection.Input;
            vrstabrodplombe.Value = VrstaBrodskePlombe;
            cmd.Parameters.Add(vrstabrodplombe);


            SqlParameter napomenazarobu = new SqlParameter();
            napomenazarobu.ParameterName = "@NapomenaZaRobu";
            napomenazarobu.SqlDbType = SqlDbType.NVarChar;
            napomenazarobu.Size = 500;
            napomenazarobu.Direction = ParameterDirection.Input;
            napomenazarobu.Value = NapomenaZaRobu;
            cmd.Parameters.Add(napomenazarobu);

            SqlParameter vgmbrod2 = new SqlParameter();
            vgmbrod2.ParameterName = "@VGMBrod2";
            vgmbrod2.SqlDbType = SqlDbType.Decimal;
            vgmbrod2.Direction = ParameterDirection.Input;
            vgmbrod2.Value = VGMBrod2;
            cmd.Parameters.Add(vgmbrod2);

            SqlParameter kontaktspeditera = new SqlParameter();
            kontaktspeditera.ParameterName = "@KontaktSpeditera";
            kontaktspeditera.SqlDbType = SqlDbType.NVarChar;
            kontaktspeditera.Size = 100;
            kontaktspeditera.Direction = ParameterDirection.Input;
            kontaktspeditera.Value = KontaktSpeditera;
            cmd.Parameters.Add(kontaktspeditera);

            SqlParameter kontaktosobe = new SqlParameter();
            kontaktosobe.ParameterName = "@KontaktOsobe";
            kontaktosobe.SqlDbType = SqlDbType.NVarChar;
            kontaktosobe.Size = 500;
            kontaktosobe.Direction = ParameterDirection.Input;
            kontaktosobe.Value = KontaktOsobe;
            cmd.Parameters.Add(kontaktosobe);

            SqlParameter uvozniid = new SqlParameter();
            uvozniid.ParameterName = "@UvozniID";
            uvozniid.SqlDbType = SqlDbType.Int;
            uvozniid.Direction = ParameterDirection.Input;
            uvozniid.Value = UvozniID;
            cmd.Parameters.Add(uvozniid);

            SqlParameter terminal = new SqlParameter();
            terminal.ParameterName = "@Terminal";
            terminal.SqlDbType = SqlDbType.Int;
            terminal.Direction = ParameterDirection.Input;
            terminal.Value = Terminal;
            cmd.Parameters.Add(terminal);

            SqlParameter scenario = new SqlParameter();
            scenario.ParameterName = "Scenario";
            scenario.SqlDbType = SqlDbType.Int;
            scenario.Direction = ParameterDirection.Input;
            scenario.Value = Scenario;
            cmd.Parameters.Add(scenario);


            SqlParameter taraz = new SqlParameter();
            taraz.ParameterName = "TaraZ";
            taraz.SqlDbType = SqlDbType.Decimal;
            taraz.Direction = ParameterDirection.Input;
            taraz.Value = TaraZ;
            cmd.Parameters.Add(taraz);

            SqlParameter mestopreuzimanja2 = new SqlParameter();
            mestopreuzimanja2.ParameterName = "@MestoPreuzimanja2";
            mestopreuzimanja2.SqlDbType = SqlDbType.Int;
            mestopreuzimanja2.Direction = ParameterDirection.Input;
            mestopreuzimanja2.Value = MestoPreuzimanja2;
            cmd.Parameters.Add(mestopreuzimanja2);



            SqlParameter mestopreuzimanja3 = new SqlParameter();
            mestopreuzimanja3.ParameterName = "@MestoPreuzimanja3";
            mestopreuzimanja3.SqlDbType = SqlDbType.Int;
            mestopreuzimanja3.Direction = ParameterDirection.Input;
            mestopreuzimanja3.Value = MestoPreuzimanja3;
            cmd.Parameters.Add(mestopreuzimanja3);


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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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
        public void InsVaganje(int KontID, string Vozilo, string BrojKontejnera, string VagarskaPotvrdaBroj,decimal Bruto,decimal Tara,decimal Neto,DateTime DatumMerenja, string Korisnik, string Roba)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertVaganje";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter kontid = new SqlParameter();
            kontid.ParameterName = "@KontID";
            kontid.SqlDbType = SqlDbType.Int;
            kontid.Direction = ParameterDirection.Input;
            kontid.Value = KontID;
            cmd.Parameters.Add(kontid);

            SqlParameter vozilo = new SqlParameter();
            vozilo.ParameterName = "@Vozilo";
            vozilo.SqlDbType = SqlDbType.NVarChar;
            vozilo.Size = 20;
            vozilo.Direction = ParameterDirection.Input;
            vozilo.Value = Vozilo;
            cmd.Parameters.Add(vozilo);

            /*
            SqlParameter brKont = new SqlParameter();
            brKont.ParameterName = "@BrojKontejnera";
            brKont.SqlDbType = SqlDbType.NVarChar;
            brKont.Size = 100;
            brKont.Direction = ParameterDirection.Input;
            brKont.Value = BrojKontejnera;
            cmd.Parameters.Add(brKont);

     */

            SqlParameter potrvrda = new SqlParameter();
            potrvrda.ParameterName = "@VagarskaPotvrdaBroj";
            potrvrda.SqlDbType = SqlDbType.NVarChar;
            potrvrda.Size = 50;
            potrvrda.Direction = ParameterDirection.Input;
            potrvrda.Value = VagarskaPotvrdaBroj;
            cmd.Parameters.Add(potrvrda);

            SqlParameter bruto = new SqlParameter();
            bruto.ParameterName = "@Bruto";
            bruto.SqlDbType = SqlDbType.Decimal;
            bruto.Direction= ParameterDirection.Input;
            bruto.Value = Bruto;
            cmd.Parameters.Add(bruto);

            SqlParameter tara = new SqlParameter();
            tara.ParameterName = "@Tara";
            tara.SqlDbType = SqlDbType.Decimal;
            tara.Direction= ParameterDirection.Input;
            tara.Value= Tara;
            cmd.Parameters.Add(tara);

            SqlParameter neto = new SqlParameter();
            neto.ParameterName = "@Neto";
            neto.SqlDbType = SqlDbType.Decimal;
            neto.Direction= ParameterDirection.Input;
            neto.Value = Neto;
            cmd.Parameters.Add(neto);

            SqlParameter datum = new SqlParameter();
            datum.ParameterName = "@DatumMerenja";
            datum.SqlDbType = SqlDbType.DateTime;
            datum.Direction= ParameterDirection.Input;
            datum.Value = DatumMerenja;
            cmd.Parameters.Add(datum);


            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

            SqlParameter roba = new SqlParameter();
            roba.ParameterName = "@Roba";
            roba.SqlDbType = SqlDbType.NVarChar;
            roba.Size = 30;
            roba.Direction = ParameterDirection.Input;
            roba.Value = Roba;
            cmd.Parameters.Add(roba);

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

        public void UpdVaganje(int ID, int KontID, string Vozilo, string BrojKontejnera, string VagarskaPotvrdaBroj, decimal Bruto, decimal Tara, decimal Neto, DateTime DatumMerenja, string Korisnik, string Roba)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateVaganje";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter vozilo = new SqlParameter();
            vozilo.ParameterName = "@Kamion";
            vozilo.SqlDbType = SqlDbType.NVarChar;
            vozilo.Size = 20;
            vozilo.Direction = ParameterDirection.Input;
            vozilo.Value = Vozilo;
            cmd.Parameters.Add(vozilo);

            SqlParameter potrvrda = new SqlParameter();
            potrvrda.ParameterName = "@VagarskaPotvrdaBroj";
            potrvrda.SqlDbType = SqlDbType.NVarChar;
            potrvrda.Size = 50;
            potrvrda.Direction = ParameterDirection.Input;
            potrvrda.Value = VagarskaPotvrdaBroj;
            cmd.Parameters.Add(potrvrda);


            SqlParameter bruto = new SqlParameter();
            bruto.ParameterName = "@Bruto";
            bruto.SqlDbType = SqlDbType.Decimal;
            bruto.Direction = ParameterDirection.Input;
            bruto.Value = Bruto;
            cmd.Parameters.Add(bruto);

            SqlParameter tara = new SqlParameter();
            tara.ParameterName = "@Tara";
            tara.SqlDbType = SqlDbType.Decimal;
            tara.Direction = ParameterDirection.Input;
            tara.Value = Tara;
            cmd.Parameters.Add(tara);

            SqlParameter neto = new SqlParameter();
            neto.ParameterName = "@Neto";
            neto.SqlDbType = SqlDbType.Decimal;
            neto.Direction = ParameterDirection.Input;
            neto.Value = Neto;
            cmd.Parameters.Add(neto);


            SqlParameter datum = new SqlParameter();
            datum.ParameterName = "@DatumMerenja";
            datum.SqlDbType = SqlDbType.DateTime;
            datum.Direction = ParameterDirection.Input;
            datum.Value = DatumMerenja;
            cmd.Parameters.Add(datum);

            SqlParameter kontid = new SqlParameter();
            kontid.ParameterName = "@IzvozKonacnaID";
            kontid.SqlDbType = SqlDbType.Int;
            kontid.Direction = ParameterDirection.Input;
            kontid.Value = KontID;
            cmd.Parameters.Add(kontid);


            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.NVarChar;
            korisnik.Size = 20;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

            SqlParameter roba = new SqlParameter();
            roba.ParameterName = "@Roba";
            roba.SqlDbType = SqlDbType.NVarChar;
            roba.Size = 30;
            roba.Direction = ParameterDirection.Input;
            roba.Value = Roba;
            cmd.Parameters.Add(roba);

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

        public void DelVaganje(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteVaganje";
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

        public void UpdateIzvozKonacnaCirada(int ID, decimal Bruto)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateIzvozKonacnaVaganjeCirada";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter bruto = new SqlParameter();
            bruto.ParameterName = "@Bruto";
            bruto.SqlDbType = SqlDbType.Decimal;
            bruto.Direction = ParameterDirection.Input;
            bruto.Value = Bruto;
            cmd.Parameters.Add(bruto);



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

        public void UpdateIzvozKonacnaPlatforma(int ID, decimal Bruto)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateIzvozKonacnaVaganjePlatforma";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter bruto = new SqlParameter();
            bruto.ParameterName = "@Bruto";
            bruto.SqlDbType = SqlDbType.Decimal;
            bruto.Direction = ParameterDirection.Input;
            bruto.Value = Bruto;
            cmd.Parameters.Add(bruto);



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

        public void KopirajKontejner(int ID, int SaUslugama)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "KopirajKontejnerIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter sauslugama = new SqlParameter();
            sauslugama.ParameterName = "@SaUslugama";
            sauslugama.SqlDbType = SqlDbType.Int;
            sauslugama.Direction = ParameterDirection.Input;
            sauslugama.Value = SaUslugama;
            cmd.Parameters.Add(sauslugama);

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

        public void KopirajKontejnerskeUslugeTerminalske(int IDSa, int IDNa, int IzIzvoza)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            string tekst = "KopirajKontejnerUslugeIzvozTerminal";
            if (IzIzvoza > 1)
            {
                tekst = "KopirajKontejnerUslugeIzvozKonacnaTerminalske";
            }

            cmd.CommandText = tekst;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idsa = new SqlParameter();
            idsa.ParameterName = "@IDSa";
            idsa.SqlDbType = SqlDbType.Int;
            idsa.Direction = ParameterDirection.Input;
            idsa.Value = IDSa;
            cmd.Parameters.Add(idsa);

            SqlParameter idna = new SqlParameter();
            idna.ParameterName = "@IDNa";
            idna.SqlDbType = SqlDbType.Int;
            idna.Direction = ParameterDirection.Input;
            idna.Value = IDNa;
            cmd.Parameters.Add(idna);




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

        public void KopirajKontejnerskeUslugeDodatne(int IDSa, int IDNa, int IzIzvoza)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            string tekst = "KopirajKontejnerUslugeIzvozDodatne";
            if (IzIzvoza> 1)
            {
                tekst = "KopirajKontejnerUslugeizvozKonacnaDodatne";
            }

            cmd.CommandText = tekst;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idsa = new SqlParameter();
            idsa.ParameterName = "@IDSa";
            idsa.SqlDbType = SqlDbType.Int;
            idsa.Direction = ParameterDirection.Input;
            idsa.Value = IDSa;
            cmd.Parameters.Add(idsa);

            SqlParameter idna = new SqlParameter();
            idna.ParameterName = "@IDNa";
            idna.SqlDbType = SqlDbType.Int;
            idna.Direction = ParameterDirection.Input;
            idna.Value = IDNa;
            cmd.Parameters.Add(idna);




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

        public void KopirajKontejnerskeUslugeAdministrativne(int IDSa, int IDNa, int IzIzvoza)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            string tekst = "KopirajKontejnerUslugeIzvozAdministrativne";
            if (IzIzvoza > 1)
            {
                tekst = "KopirajKontejnerUslugeIzvozKonacnaAdministrativne";
            }

            cmd.CommandText = tekst;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter idsa = new SqlParameter();
            idsa.ParameterName = "@IDSa";
            idsa.SqlDbType = SqlDbType.Int;
            idsa.Direction = ParameterDirection.Input;
            idsa.Value = IDSa;
            cmd.Parameters.Add(idsa);

            SqlParameter idna = new SqlParameter();
            idna.ParameterName = "@IDNa";
            idna.SqlDbType = SqlDbType.Int;
            idna.Direction = ParameterDirection.Input;
            idna.Value = IDNa;
            cmd.Parameters.Add(idna);




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


        public void UpdIzvozTerminalIzbor(int ID,string OstalePlombe,  decimal VGMBrod, decimal Tara, decimal VGMTezina,  decimal CBMO,int BrojKoletaO,  decimal BrutoRobeO,  int BookingBrodara,    decimal TaraZ, int Rasporedjen)
            {

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateIzvozTerminalIzbor";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter ostaleplombe = new SqlParameter();
            ostaleplombe.ParameterName = "@OstalePlombe";
            ostaleplombe.SqlDbType = SqlDbType.NVarChar;
            ostaleplombe.Size = 50;
            ostaleplombe.Direction = ParameterDirection.Input;
            ostaleplombe.Value = OstalePlombe;
            cmd.Parameters.Add(ostaleplombe);


            SqlParameter vGMBrod = new SqlParameter();
            vGMBrod.ParameterName = "@VGMBrod";
            vGMBrod.SqlDbType = SqlDbType.Decimal;
            vGMBrod.Direction = ParameterDirection.Input;
            vGMBrod.Value = VGMBrod;
            cmd.Parameters.Add(vGMBrod);

            SqlParameter tara = new SqlParameter();
            tara.ParameterName = "@Tara";
            tara.SqlDbType = SqlDbType.Decimal;
            tara.Direction = ParameterDirection.Input;
            tara.Value = Tara;
            cmd.Parameters.Add(tara);

            SqlParameter vGMTezina = new SqlParameter();
            vGMTezina.ParameterName = "@VGMTezina";
            vGMTezina.SqlDbType = SqlDbType.Decimal;
            vGMTezina.Direction = ParameterDirection.Input;
            vGMTezina.Value = VGMTezina;
            cmd.Parameters.Add(vGMTezina);

            SqlParameter cmbO = new SqlParameter();
            cmbO.ParameterName = "@CBMO";
            cmbO.SqlDbType = SqlDbType.Decimal;
            cmbO.Direction = ParameterDirection.Input;
            cmbO.Value = CBMO;
            cmd.Parameters.Add(cmbO);

            SqlParameter brojkoletaO = new SqlParameter();
            brojkoletaO.ParameterName = "@BrojKoletaO";
            brojkoletaO.SqlDbType = SqlDbType.Int;
            brojkoletaO.Direction = ParameterDirection.Input;
            brojkoletaO.Value = BrojKoletaO;
            cmd.Parameters.Add(brojkoletaO);

            SqlParameter brutorobeO = new SqlParameter();
            brutorobeO.ParameterName = "@BrutoRobeO";
            brutorobeO.SqlDbType = SqlDbType.Decimal;
            brutorobeO.Direction = ParameterDirection.Input;
            brutorobeO.Value = BrutoRobeO;
            cmd.Parameters.Add(brutorobeO);


            SqlParameter bookingbrodara = new SqlParameter();
            bookingbrodara.ParameterName = "@BookingBrodara";
            bookingbrodara.SqlDbType = SqlDbType.Int;
            bookingbrodara.Direction = ParameterDirection.Input;
            bookingbrodara.Value = BookingBrodara;
            cmd.Parameters.Add(bookingbrodara);


            SqlParameter taraz = new SqlParameter();
            taraz.ParameterName = "@TaraZ";
            taraz.SqlDbType = SqlDbType.Decimal;
            taraz.Direction = ParameterDirection.Input;
            taraz.Value = TaraZ;
            cmd.Parameters.Add(taraz);

            SqlParameter rasporedjen = new SqlParameter();
            rasporedjen.ParameterName = "@Raspoeredjen";
            rasporedjen.SqlDbType = SqlDbType.Int;
            rasporedjen.Direction = ParameterDirection.Input;
            rasporedjen.Value = Rasporedjen;
            cmd.Parameters.Add(rasporedjen);


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

            catch (SqlException ex)
            {
                throw new Exception(ex.Message.ToString());
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

    }
}
