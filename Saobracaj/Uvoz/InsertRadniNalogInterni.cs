﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    class InsertRadniNalogInterni
    {
        string connection = Sifarnici.frmLogovanje.connectionString;

        public void InsRadniNalogInterni(int OJIzdavanja, int OJRealizacije, DateTime DatumIzdavanja, DateTime DatumRealizacije, string Napomena, int Uradjen, string Osnov, int BrojOsnov, string KorisnikIzdao, string KorisnikZavrsio)
        {
            //, int IDManipulacijaJED, int PlanID, string StatusIzdavanja, int KonkretanIDUsluge
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRadniNalogInterni";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter ojizdavanja = new SqlParameter();
            ojizdavanja.ParameterName = "@OJIzdavanja";
            ojizdavanja.SqlDbType = SqlDbType.Int;
            ojizdavanja.Direction = ParameterDirection.Input;
            ojizdavanja.Value = OJIzdavanja;
            cmd.Parameters.Add(ojizdavanja);

            SqlParameter ojrealizacije = new SqlParameter();
            ojrealizacije.ParameterName = "@OJRealizacije";
            ojrealizacije.SqlDbType = SqlDbType.Int;
            ojrealizacije.Direction = ParameterDirection.Input;
            ojrealizacije.Value = OJRealizacije;
            cmd.Parameters.Add(ojrealizacije);


            SqlParameter datumizdavanja = new SqlParameter();
            datumizdavanja.ParameterName = "@DatumIzdavanja";
            datumizdavanja.SqlDbType = SqlDbType.DateTime;
            datumizdavanja.Direction = ParameterDirection.Input;
            datumizdavanja.Value = DatumIzdavanja;
            cmd.Parameters.Add(datumizdavanja);

            SqlParameter datumrealizacije = new SqlParameter();
            datumrealizacije.ParameterName = "@DatumRealizacije";
            datumrealizacije.SqlDbType = SqlDbType.DateTime;
            datumrealizacije.Direction = ParameterDirection.Input;
            datumrealizacije.Value = DatumRealizacije;
            cmd.Parameters.Add(datumrealizacije);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter uradjen = new SqlParameter();
            uradjen.ParameterName = "@Uradjen";
            uradjen.SqlDbType = SqlDbType.Int;
            uradjen.Direction = ParameterDirection.Input;
            uradjen.Value = Uradjen;
            cmd.Parameters.Add(uradjen);

            SqlParameter osnov = new SqlParameter();
            osnov.ParameterName = "@Osnov";
            osnov.SqlDbType = SqlDbType.NVarChar;
            osnov.Size = 50;
            osnov.Direction = ParameterDirection.Input;
            osnov.Value = Osnov;
            cmd.Parameters.Add(osnov);

            SqlParameter brojosnov = new SqlParameter();
            brojosnov.ParameterName = "@BrojOsnov";
            brojosnov.SqlDbType = SqlDbType.Int;
            brojosnov.Direction = ParameterDirection.Input;
            brojosnov.Value = BrojOsnov;
            cmd.Parameters.Add(brojosnov);

            SqlParameter korisnikizdao = new SqlParameter();
            korisnikizdao.ParameterName = "@KorisnikIzdao";
            korisnikizdao.SqlDbType = SqlDbType.NVarChar;
            korisnikizdao.Size = 50;
            korisnikizdao.Direction = ParameterDirection.Input;
            korisnikizdao.Value = KorisnikIzdao;
            cmd.Parameters.Add(korisnikizdao);

            SqlParameter korisnikzavrsio = new SqlParameter();
            korisnikzavrsio.ParameterName = "@KorisnikZavrsio";
            korisnikzavrsio.SqlDbType = SqlDbType.NVarChar;
            korisnikzavrsio.Size = 50;
            korisnikzavrsio.Direction = ParameterDirection.Input;
            korisnikzavrsio.Value = KorisnikZavrsio;
            cmd.Parameters.Add(korisnikzavrsio);
            /*
                        SqlParameter idmanipulacijaJED = new SqlParameter();
                        idmanipulacijaJED.ParameterName = "@IDManipulacijaJED";
                        idmanipulacijaJED.SqlDbType = SqlDbType.Int;
                        idmanipulacijaJED.Direction = ParameterDirection.Input;
                        idmanipulacijaJED.Value = IDManipulacijaJED;
                        cmd.Parameters.Add(idmanipulacijaJED);

                        SqlParameter planid = new SqlParameter();
                        planid.ParameterName = "@PlanID";
                        planid.SqlDbType = SqlDbType.Int;
                        planid.Direction = ParameterDirection.Input;
                        planid.Value = PlanID;
                        cmd.Parameters.Add(planid);

                        SqlParameter statusizdavanja = new SqlParameter();
                        statusizdavanja.ParameterName = "@StatusIzdavanja";
                        statusizdavanja.SqlDbType = SqlDbType.NVarChar;
                        statusizdavanja.Size = 50;
                        statusizdavanja.Direction = ParameterDirection.Input;
                        statusizdavanja.Value = StatusIzdavanja;
                        cmd.Parameters.Add(statusizdavanja);

                        SqlParameter konkretaIDUsluge = new SqlParameter();
                        konkretaIDUsluge.ParameterName = "@KonkretaIDUsluge";
                        konkretaIDUsluge.SqlDbType = SqlDbType.Int;
                        konkretaIDUsluge.Direction = ParameterDirection.Input;
                        konkretaIDUsluge.Value = konkretaIDUsluge;
                        cmd.Parameters.Add(konkretaIDUsluge);


                        */

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

        public void InsRadniNalogInterni2(int OJIzdavanja, int OJRealizacije, DateTime DatumIzdavanja, DateTime DatumRealizacije, string Napomena, int Uradjen, string Osnov, int BrojOsnov, string KorisnikIzdao, string KorisnikZavrsio)
        {
            //, int IDManipulacijaJED, int PlanID, string StatusIzdavanja, int KonkretanIDUsluge
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRadniNalogInterni2";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter ojizdavanja = new SqlParameter();
            ojizdavanja.ParameterName = "@OJIzdavanja";
            ojizdavanja.SqlDbType = SqlDbType.Int;
            ojizdavanja.Direction = ParameterDirection.Input;
            ojizdavanja.Value = OJIzdavanja;
            cmd.Parameters.Add(ojizdavanja);

            SqlParameter ojrealizacije = new SqlParameter();
            ojrealizacije.ParameterName = "@OJRealizacije";
            ojrealizacije.SqlDbType = SqlDbType.Int;
            ojrealizacije.Direction = ParameterDirection.Input;
            ojrealizacije.Value = OJRealizacije;
            cmd.Parameters.Add(ojrealizacije);


            SqlParameter datumizdavanja = new SqlParameter();
            datumizdavanja.ParameterName = "@DatumIzdavanja";
            datumizdavanja.SqlDbType = SqlDbType.DateTime;
            datumizdavanja.Direction = ParameterDirection.Input;
            datumizdavanja.Value = DatumIzdavanja;
            cmd.Parameters.Add(datumizdavanja);

            SqlParameter datumrealizacije = new SqlParameter();
            datumrealizacije.ParameterName = "@DatumRealizacije";
            datumrealizacije.SqlDbType = SqlDbType.DateTime;
            datumrealizacije.Direction = ParameterDirection.Input;
            datumrealizacije.Value = DatumRealizacije;
            cmd.Parameters.Add(datumrealizacije);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter uradjen = new SqlParameter();
            uradjen.ParameterName = "@Uradjen";
            uradjen.SqlDbType = SqlDbType.Int;
            uradjen.Direction = ParameterDirection.Input;
            uradjen.Value = Uradjen;
            cmd.Parameters.Add(uradjen);

            SqlParameter osnov = new SqlParameter();
            osnov.ParameterName = "@Osnov";
            osnov.SqlDbType = SqlDbType.NVarChar;
            osnov.Size = 50;
            osnov.Direction = ParameterDirection.Input;
            osnov.Value = Osnov;
            cmd.Parameters.Add(osnov);

            SqlParameter brojosnov = new SqlParameter();
            brojosnov.ParameterName = "@BrojOsnov";
            brojosnov.SqlDbType = SqlDbType.Int;
            brojosnov.Direction = ParameterDirection.Input;
            brojosnov.Value = BrojOsnov;
            cmd.Parameters.Add(brojosnov);

            SqlParameter korisnikizdao = new SqlParameter();
            korisnikizdao.ParameterName = "@KorisnikIzdao";
            korisnikizdao.SqlDbType = SqlDbType.NVarChar;
            korisnikizdao.Size = 50;
            korisnikizdao.Direction = ParameterDirection.Input;
            korisnikizdao.Value = KorisnikIzdao;
            cmd.Parameters.Add(korisnikizdao);

            SqlParameter korisnikzavrsio = new SqlParameter();
            korisnikzavrsio.ParameterName = "@KorisnikZavrsio";
            korisnikzavrsio.SqlDbType = SqlDbType.NVarChar;
            korisnikzavrsio.Size = 50;
            korisnikzavrsio.Direction = ParameterDirection.Input;
            korisnikzavrsio.Value = KorisnikZavrsio;
            cmd.Parameters.Add(korisnikzavrsio);
            /*
                        SqlParameter idmanipulacijaJED = new SqlParameter();
                        idmanipulacijaJED.ParameterName = "@IDManipulacijaJED";
                        idmanipulacijaJED.SqlDbType = SqlDbType.Int;
                        idmanipulacijaJED.Direction = ParameterDirection.Input;
                        idmanipulacijaJED.Value = IDManipulacijaJED;
                        cmd.Parameters.Add(idmanipulacijaJED);

                        SqlParameter planid = new SqlParameter();
                        planid.ParameterName = "@PlanID";
                        planid.SqlDbType = SqlDbType.Int;
                        planid.Direction = ParameterDirection.Input;
                        planid.Value = PlanID;
                        cmd.Parameters.Add(planid);

                        SqlParameter statusizdavanja = new SqlParameter();
                        statusizdavanja.ParameterName = "@StatusIzdavanja";
                        statusizdavanja.SqlDbType = SqlDbType.NVarChar;
                        statusizdavanja.Size = 50;
                        statusizdavanja.Direction = ParameterDirection.Input;
                        statusizdavanja.Value = StatusIzdavanja;
                        cmd.Parameters.Add(statusizdavanja);

                        SqlParameter konkretaIDUsluge = new SqlParameter();
                        konkretaIDUsluge.ParameterName = "@KonkretaIDUsluge";
                        konkretaIDUsluge.SqlDbType = SqlDbType.Int;
                        konkretaIDUsluge.Direction = ParameterDirection.Input;
                        konkretaIDUsluge.Value = konkretaIDUsluge;
                        cmd.Parameters.Add(konkretaIDUsluge);


                        */

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

        public void UpdRadniNalogInterni(int ID, int OJIzdavanja, int OJRealizacije, DateTime DatumIzdavanja, DateTime DatumRealizacije, string Napomena, int Uradjen, string Osnov, int BrojOsnov, string KorisnikIzdao, string KorisnikZavrsio)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRadniNalogInterni";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter ojizdavanja = new SqlParameter();
            ojizdavanja.ParameterName = "@OJIzdavanja";
            ojizdavanja.SqlDbType = SqlDbType.Int;
            ojizdavanja.Direction = ParameterDirection.Input;
            ojizdavanja.Value = OJIzdavanja;
            cmd.Parameters.Add(ojizdavanja);

            SqlParameter ojrealizacije = new SqlParameter();
            ojrealizacije.ParameterName = "@OJRealizacije";
            ojrealizacije.SqlDbType = SqlDbType.Int;
            ojrealizacije.Direction = ParameterDirection.Input;
            ojrealizacije.Value = OJRealizacije;
            cmd.Parameters.Add(ojrealizacije);


            SqlParameter datumizdavanja = new SqlParameter();
            datumizdavanja.ParameterName = "@DatumIzdavanja";
            datumizdavanja.SqlDbType = SqlDbType.DateTime;
            datumizdavanja.Direction = ParameterDirection.Input;
            datumizdavanja.Value = DatumIzdavanja;
            cmd.Parameters.Add(datumizdavanja);

            SqlParameter datumrealizacije = new SqlParameter();
            datumrealizacije.ParameterName = "@DatumRealizacije";
            datumrealizacije.SqlDbType = SqlDbType.DateTime;
            datumrealizacije.Direction = ParameterDirection.Input;
            datumrealizacije.Value = DatumRealizacije;
            cmd.Parameters.Add(datumrealizacije);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter uradjen = new SqlParameter();
            uradjen.ParameterName = "@Uradjen";
            uradjen.SqlDbType = SqlDbType.Int;
            uradjen.Direction = ParameterDirection.Input;
            uradjen.Value = Uradjen;
            cmd.Parameters.Add(uradjen);

            SqlParameter osnov = new SqlParameter();
            osnov.ParameterName = "@Osnov";
            osnov.SqlDbType = SqlDbType.NVarChar;
            osnov.Size = 50;
            osnov.Direction = ParameterDirection.Input;
            osnov.Value = Osnov;
            cmd.Parameters.Add(osnov);

            SqlParameter brojosnov = new SqlParameter();
            brojosnov.ParameterName = "@BrojOsnov";
            brojosnov.SqlDbType = SqlDbType.Int;
            brojosnov.Direction = ParameterDirection.Input;
            brojosnov.Value = BrojOsnov;
            cmd.Parameters.Add(brojosnov);

            SqlParameter korisnikizdao = new SqlParameter();
            korisnikizdao.ParameterName = "@KorisnikIzdao";
            korisnikizdao.SqlDbType = SqlDbType.NVarChar;
            korisnikizdao.Size = 50;
            korisnikizdao.Direction = ParameterDirection.Input;
            korisnikizdao.Value = KorisnikIzdao;
            cmd.Parameters.Add(korisnikizdao);

            SqlParameter korisnikzavrsio = new SqlParameter();
            korisnikzavrsio.ParameterName = "@KorisnikZavrsio";
            korisnikzavrsio.SqlDbType = SqlDbType.NVarChar;
            korisnikzavrsio.Size = 50;
            korisnikzavrsio.Direction = ParameterDirection.Input;
            korisnikzavrsio.Value = KorisnikZavrsio;
            cmd.Parameters.Add(korisnikzavrsio);
            /*
            SqlParameter idmanipulacijaJED = new SqlParameter();
            idmanipulacijaJED.ParameterName = "@IDManipulacijaJED";
            idmanipulacijaJED.SqlDbType = SqlDbType.Int;
            idmanipulacijaJED.Direction = ParameterDirection.Input;
            idmanipulacijaJED.Value = IDManipulacijaJED;
            cmd.Parameters.Add(idmanipulacijaJED);

            SqlParameter planid = new SqlParameter();
            planid.ParameterName = "@PlanID";
            planid.SqlDbType = SqlDbType.Int;
            planid.Direction = ParameterDirection.Input;
            planid.Value = PlanID;
            cmd.Parameters.Add(planid);

            SqlParameter statusizdavanja = new SqlParameter();
            statusizdavanja.ParameterName = "@StatusIzdavanja";
            statusizdavanja.SqlDbType = SqlDbType.NVarChar;
            statusizdavanja.Size = 50;
            statusizdavanja.Direction = ParameterDirection.Input;
            statusizdavanja.Value = StatusIzdavanja;
            cmd.Parameters.Add(statusizdavanja);

            SqlParameter konkretaIDUsluge = new SqlParameter();
            konkretaIDUsluge.ParameterName = "@KonkretaIDUsluge";
            konkretaIDUsluge.SqlDbType = SqlDbType.Int;
            konkretaIDUsluge.Direction = ParameterDirection.Input;
            konkretaIDUsluge.Value = konkretaIDUsluge;
            cmd.Parameters.Add(konkretaIDUsluge);
            */
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
       
        public void DelRadniNalogInterni(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DeleteRadniNalogInterni";
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
                throw new Exception("Neuspešan upis");
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

        public void PromeniStatusStorno(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRadniNalogInterniStorno";
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
                throw new Exception("Neuspešan upis");
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

        public void PromeniFormiranRNIVoz(int PlanID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRNIVozFormiran";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter planid = new SqlParameter();
            planid.ParameterName = "@PlanID";
            planid.SqlDbType = SqlDbType.Int;
            planid.Direction = ParameterDirection.Input;
            planid.Value = PlanID;
            cmd.Parameters.Add(planid);

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
                throw new Exception("Neuspešan upis");
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
        public void DelRadniNalogInterniSaDokumentima(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DelRadniNalogInterniSaDokumentima";
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
                throw new Exception("Neuspešan upis");
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

        public void UpdRadniNalogInterniZavrsen(int ID, string Korisnik)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRadniNalogInterniZavrsen";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);


            SqlParameter korisnik = new SqlParameter();
            korisnik.ParameterName = "@Korisnik";
            korisnik.SqlDbType = SqlDbType.Int;
            korisnik.Direction = ParameterDirection.Input;
            korisnik.Value = Korisnik;
            cmd.Parameters.Add(korisnik);

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
                throw new Exception("Neuspešan upis");
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

        public void UpdRadniNalogInterniFormiran(int ID)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRadniNalogInterniFormiran";
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
                throw new Exception("Neuspešan upis");
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
        //
        public void InsRadniNalogInterniIzvoz(int OJIzdavanja, int OJRealizacije, DateTime DatumIzdavanja, DateTime DatumRealizacije, string Napomena, int Uradjen, string Osnov, int BrojOsnov, string KorisnikIzdao, string KorisnikZavrsio)
        {

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRadniNalogInterniIzvoz";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter ojizdavanja = new SqlParameter();
            ojizdavanja.ParameterName = "@OJIzdavanja";
            ojizdavanja.SqlDbType = SqlDbType.Int;
            ojizdavanja.Direction = ParameterDirection.Input;
            ojizdavanja.Value = OJIzdavanja;
            cmd.Parameters.Add(ojizdavanja);

            SqlParameter ojrealizacije = new SqlParameter();
            ojrealizacije.ParameterName = "@OJRealizacije";
            ojrealizacije.SqlDbType = SqlDbType.Int;
            ojrealizacije.Direction = ParameterDirection.Input;
            ojrealizacije.Value = OJRealizacije;
            cmd.Parameters.Add(ojrealizacije);


            SqlParameter datumizdavanja = new SqlParameter();
            datumizdavanja.ParameterName = "@DatumIzdavanja";
            datumizdavanja.SqlDbType = SqlDbType.DateTime;
            datumizdavanja.Direction = ParameterDirection.Input;
            datumizdavanja.Value = DatumIzdavanja;
            cmd.Parameters.Add(datumizdavanja);

            SqlParameter datumrealizacije = new SqlParameter();
            datumrealizacije.ParameterName = "@DatumRealizacije";
            datumrealizacije.SqlDbType = SqlDbType.DateTime;
            datumrealizacije.Direction = ParameterDirection.Input;
            datumrealizacije.Value = DatumRealizacije;
            cmd.Parameters.Add(datumrealizacije);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter uradjen = new SqlParameter();
            uradjen.ParameterName = "@Uradjen";
            uradjen.SqlDbType = SqlDbType.Int;
            uradjen.Direction = ParameterDirection.Input;
            uradjen.Value = Uradjen;
            cmd.Parameters.Add(uradjen);

            SqlParameter osnov = new SqlParameter();
            osnov.ParameterName = "@Osnov";
            osnov.SqlDbType = SqlDbType.NVarChar;
            osnov.Size = 50;
            osnov.Direction = ParameterDirection.Input;
            osnov.Value = Osnov;
            cmd.Parameters.Add(osnov);

            SqlParameter brojosnov = new SqlParameter();
            brojosnov.ParameterName = "@BrojOsnov";
            brojosnov.SqlDbType = SqlDbType.Int;
            brojosnov.Direction = ParameterDirection.Input;
            brojosnov.Value = BrojOsnov;
            cmd.Parameters.Add(brojosnov);

            SqlParameter korisnikizdao = new SqlParameter();
            korisnikizdao.ParameterName = "@KorisnikIzdao";
            korisnikizdao.SqlDbType = SqlDbType.NVarChar;
            korisnikizdao.Size = 50;
            korisnikizdao.Direction = ParameterDirection.Input;
            korisnikizdao.Value = KorisnikIzdao;
            cmd.Parameters.Add(korisnikizdao);

            SqlParameter korisnikzavrsio = new SqlParameter();
            korisnikzavrsio.ParameterName = "@KorisnikZavrsio";
            korisnikzavrsio.SqlDbType = SqlDbType.NVarChar;
            korisnikzavrsio.Size = 50;
            korisnikzavrsio.Direction = ParameterDirection.Input;
            korisnikzavrsio.Value = KorisnikZavrsio;
            cmd.Parameters.Add(korisnikzavrsio);



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

        public void InsRadniNalogInterniIzUVoza(int OJIzdavanja, int OJRealizacije, DateTime DatumIzdavanja, DateTime DatumRealizacije, string Napomena, int Uradjen, string Osnov, int BrojOsnov, string KorisnikIzdao, string KorisnikZavrsio)
        {

            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "InsertRadniNalogInterni";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter ojizdavanja = new SqlParameter();
            ojizdavanja.ParameterName = "@OJIzdavanja";
            ojizdavanja.SqlDbType = SqlDbType.Int;
            ojizdavanja.Direction = ParameterDirection.Input;
            ojizdavanja.Value = OJIzdavanja;
            cmd.Parameters.Add(ojizdavanja);

            SqlParameter ojrealizacije = new SqlParameter();
            ojrealizacije.ParameterName = "@OJRealizacije";
            ojrealizacije.SqlDbType = SqlDbType.Int;
            ojrealizacije.Direction = ParameterDirection.Input;
            ojrealizacije.Value = OJRealizacije;
            cmd.Parameters.Add(ojrealizacije);


            SqlParameter datumizdavanja = new SqlParameter();
            datumizdavanja.ParameterName = "@DatumIzdavanja";
            datumizdavanja.SqlDbType = SqlDbType.DateTime;
            datumizdavanja.Direction = ParameterDirection.Input;
            datumizdavanja.Value = DatumIzdavanja;
            cmd.Parameters.Add(datumizdavanja);

            SqlParameter datumrealizacije = new SqlParameter();
            datumrealizacije.ParameterName = "@DatumRealizacije";
            datumrealizacije.SqlDbType = SqlDbType.DateTime;
            datumrealizacije.Direction = ParameterDirection.Input;
            datumrealizacije.Value = DatumRealizacije;
            cmd.Parameters.Add(datumrealizacije);

            SqlParameter napomena = new SqlParameter();
            napomena.ParameterName = "@Napomena";
            napomena.SqlDbType = SqlDbType.NVarChar;
            napomena.Size = 500;
            napomena.Direction = ParameterDirection.Input;
            napomena.Value = Napomena;
            cmd.Parameters.Add(napomena);

            SqlParameter uradjen = new SqlParameter();
            uradjen.ParameterName = "@Uradjen";
            uradjen.SqlDbType = SqlDbType.Int;
            uradjen.Direction = ParameterDirection.Input;
            uradjen.Value = Uradjen;
            cmd.Parameters.Add(uradjen);

            SqlParameter osnov = new SqlParameter();
            osnov.ParameterName = "@Osnov";
            osnov.SqlDbType = SqlDbType.NVarChar;
            osnov.Size = 50;
            osnov.Direction = ParameterDirection.Input;
            osnov.Value = Osnov;
            cmd.Parameters.Add(osnov);

            SqlParameter brojosnov = new SqlParameter();
            brojosnov.ParameterName = "@BrojOsnov";
            brojosnov.SqlDbType = SqlDbType.Int;
            brojosnov.Direction = ParameterDirection.Input;
            brojosnov.Value = BrojOsnov;
            cmd.Parameters.Add(brojosnov);

            SqlParameter korisnikizdao = new SqlParameter();
            korisnikizdao.ParameterName = "@KorisnikIzdao";
            korisnikizdao.SqlDbType = SqlDbType.NVarChar;
            korisnikizdao.Size = 50;
            korisnikizdao.Direction = ParameterDirection.Input;
            korisnikizdao.Value = KorisnikIzdao;
            cmd.Parameters.Add(korisnikizdao);

            SqlParameter korisnikzavrsio = new SqlParameter();
            korisnikzavrsio.ParameterName = "@KorisnikZavrsio";
            korisnikzavrsio.SqlDbType = SqlDbType.NVarChar;
            korisnikzavrsio.Size = 50;
            korisnikzavrsio.Direction = ParameterDirection.Input;
            korisnikzavrsio.Value = KorisnikZavrsio;
            cmd.Parameters.Add(korisnikzavrsio);



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


        public void UpdRadniNalogInterniGenerisan(int ID, string TipDokPrevoza, int BrDokPrevoza, string TIPRN, int BROJRN)
        {
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UpdateRadniNalogInterniGenerisan";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id = new SqlParameter();
            id.ParameterName = "@ID";
            id.SqlDbType = SqlDbType.Int;
            id.Direction = ParameterDirection.Input;
            id.Value = ID;
            cmd.Parameters.Add(id);

            SqlParameter tipdokprevoza = new SqlParameter();
            tipdokprevoza.ParameterName = "@TipDokPrevoza";
            tipdokprevoza.SqlDbType = SqlDbType.NVarChar;
            tipdokprevoza.Size = 3;
            tipdokprevoza.Direction = ParameterDirection.Input;
            tipdokprevoza.Value = TipDokPrevoza;
            cmd.Parameters.Add(tipdokprevoza);

            SqlParameter brdokprevoza = new SqlParameter();
            brdokprevoza.ParameterName = "@BrDokPrevoza";
            brdokprevoza.SqlDbType = SqlDbType.Int;
            brdokprevoza.Direction = ParameterDirection.Input;
            brdokprevoza.Value = BrDokPrevoza;
            cmd.Parameters.Add(brdokprevoza);

            SqlParameter tiprn = new SqlParameter();
            tiprn.ParameterName = "@TIPRN";
            tiprn.SqlDbType = SqlDbType.NVarChar;
            tiprn.Size = 3;
            tiprn.Direction = ParameterDirection.Input;
            tiprn.Value = TIPRN;
            cmd.Parameters.Add(tiprn);

            SqlParameter brojrn = new SqlParameter();
            brojrn.ParameterName = "@BROJRN";
            brojrn.SqlDbType = SqlDbType.Int;
            brojrn.Direction = ParameterDirection.Input;
            brojrn.Value = BROJRN;
            cmd.Parameters.Add(brojrn);

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
                throw new Exception("Neuspešan upis");
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
