﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Saobracaj.Dokumenta
{
    class InsertOtpremaKontejneraStavke
    {

        public void InsertOtpremaKontejneraStav(int IdNadredjenog, string BrojKontejnera, String BrojVagona, double Granica, double BrojOsovina, double SopstvenaMasa, double Tara, double Neto, int Posiljalac, int Primalac, int VlasnikKontejnera, int TipKontejnera, int VrstaRobe, string Buking, int StatusKontejnera, string BrojPlombe, int PlaniraniLager, int IdVoza,  DateTime VremePripremljen, DateTime VremeOdlaska, DateTime Datum, string Korisnik, string BrojPlombe2, int Organizator, string NapomenaS, DateTime PeriodSkladistenjaOd, DateTime PeriodSkladistenjaDo, Double BTTORobe, int KontejnerID, double BTTOKOntejnera, string Napomena2, int PostupakSaRobom,  double BTTORobeFaktura, double BTTORobeOTP, double BTTORobeODVAGA, double BTTORobeKONTEJNER, string PLOMBAVLASN, string CBMOTP, string KOLETAOTP, int NalogID)
        {
           

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertOtpremaKontejneraVozStavke";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@IdNadredjenog";
            parameter1.SqlDbType = SqlDbType.Int;
       
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = IdNadredjenog;
            myCommand.Parameters.Add(parameter1);

          
		  

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@BrojKontejnera";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 20;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = BrojKontejnera;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@BrojVagona";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 20;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = BrojVagona;
            myCommand.Parameters.Add(parameter3);
             
           

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Granica";
            parameter4.SqlDbType = SqlDbType.Decimal;
           // parameter4.Size = 20;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Granica;
            myCommand.Parameters.Add(parameter4);
           
            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@BrojOsovina";
            parameter5.SqlDbType = SqlDbType.Decimal;
            // parameter4.Size = 20;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = BrojOsovina;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@SopstvenaMasa";
            parameter6.SqlDbType = SqlDbType.Decimal;
            // parameter4.Size = 20;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = SopstvenaMasa;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Tara";
            parameter7.SqlDbType = SqlDbType.Decimal;
            // parameter4.Size = 20;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Tara;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Neto";
            parameter8.SqlDbType = SqlDbType.Decimal;
            // parameter4.Size = 20;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Neto;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@Posiljalac";
            parameter9.SqlDbType = SqlDbType.Int;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Posiljalac;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@Primalac";
            parameter10.SqlDbType = SqlDbType.Int;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = Primalac;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@VlasnikKontejnera";
            parameter11.SqlDbType = SqlDbType.Int;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = VlasnikKontejnera;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@TipKontejnera";
            parameter12.SqlDbType = SqlDbType.Int;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = TipKontejnera;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@VrstaRobe";
            parameter13.SqlDbType = SqlDbType.Int;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = VrstaRobe;
            myCommand.Parameters.Add(parameter13);

            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@Buking";
            parameter14.SqlDbType = SqlDbType.NVarChar;
            parameter14.Size = 20;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = Buking;
            myCommand.Parameters.Add(parameter14);

            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@StatusKontejnera";
            parameter15.SqlDbType = SqlDbType.Int;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = StatusKontejnera;
            myCommand.Parameters.Add(parameter15);

            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@BrojPlombe";
            parameter16.SqlDbType = SqlDbType.NVarChar;
            parameter16.Size = 30;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = BrojPlombe;
            myCommand.Parameters.Add(parameter16);

            SqlParameter parameter17 = new SqlParameter();
            parameter17.ParameterName = "@PlaniraniLager";
            parameter17.SqlDbType = SqlDbType.Int;
           // parameter13.Size = 30;
            parameter17.Direction = ParameterDirection.Input;
            parameter17.Value = PlaniraniLager;
            myCommand.Parameters.Add(parameter17);

            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@IdVoza";
            parameter18.SqlDbType = SqlDbType.Int;
            // parameter13.Size = 30;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = IdVoza;
            myCommand.Parameters.Add(parameter18);



            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@VremePripremljen";
            parameter20.SqlDbType = SqlDbType.DateTime;
            // parameter13.Size = 30;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = VremePripremljen;
            myCommand.Parameters.Add(parameter20);

            SqlParameter parameter21 = new SqlParameter();
            parameter21.ParameterName = "@VremeOdlaska";
            parameter21.SqlDbType = SqlDbType.DateTime;
            // parameter13.Size = 30;
            parameter21.Direction = ParameterDirection.Input;
            parameter21.Value = VremeOdlaska;
            myCommand.Parameters.Add(parameter21);

            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@Datum";
            parameter22.SqlDbType = SqlDbType.DateTime;
           // parameter22.Size = 20;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = Datum;
            myCommand.Parameters.Add(parameter22);
            
            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@Korisnik";
            parameter23.SqlDbType = SqlDbType.NVarChar;
            parameter23.Size = 20;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = Korisnik;
            myCommand.Parameters.Add(parameter23);

            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@BrojPlombe2";
            parameter24.SqlDbType = SqlDbType.NVarChar;
            parameter24.Size = 20;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = BrojPlombe2;
            myCommand.Parameters.Add(parameter24);

            SqlParameter parameter25 = new SqlParameter();
            parameter25.ParameterName = "@Organizator";
            parameter25.SqlDbType = SqlDbType.Int;
            // parameter13.Size = 30;
            parameter25.Direction = ParameterDirection.Input;
            parameter25.Value = Organizator;
            myCommand.Parameters.Add(parameter25);


            SqlParameter parameter26 = new SqlParameter();
            parameter26.ParameterName = "@NapomenaS";
            parameter26.SqlDbType = SqlDbType.NVarChar;
            parameter26.Size = 100;
            parameter26.Direction = ParameterDirection.Input;
            parameter26.Value = NapomenaS;
            myCommand.Parameters.Add(parameter26);

            SqlParameter parameter28 = new SqlParameter();
            parameter28.ParameterName = "@PeriodSkladistenjaOd";
            parameter28.SqlDbType = SqlDbType.DateTime;
            parameter28.Direction = ParameterDirection.Input;
            parameter28.Value = PeriodSkladistenjaOd;
            myCommand.Parameters.Add(parameter28);

            SqlParameter parameter29 = new SqlParameter();
            parameter29.ParameterName = "@PeriodSkladistenjaDo";
            parameter29.SqlDbType = SqlDbType.DateTime;
            parameter29.Direction = ParameterDirection.Input;
            parameter29.Value = PeriodSkladistenjaDo;
            myCommand.Parameters.Add(parameter29);

            SqlParameter parameter30 = new SqlParameter();
            parameter30.ParameterName = "@BTTORobe";
            parameter30.SqlDbType = SqlDbType.Decimal;
            parameter30.Direction = ParameterDirection.Input;
            parameter30.Value = BTTORobe;
            myCommand.Parameters.Add(parameter30);

            SqlParameter parameter31 = new SqlParameter();
            parameter31.ParameterName = "@KontejnerID";
            parameter31.SqlDbType = SqlDbType.Int;
            parameter31.Direction = ParameterDirection.Input;
            parameter31.Value = KontejnerID;
            myCommand.Parameters.Add(parameter31);





            SqlParameter parameter32 = new SqlParameter();
            parameter32.ParameterName = "@BTTOKontejnera";
            parameter32.SqlDbType = SqlDbType.Decimal;
            parameter32.Direction = ParameterDirection.Input;
            parameter32.Value = BTTOKOntejnera;
            myCommand.Parameters.Add(parameter32);

            SqlParameter parameter33 = new SqlParameter();
            parameter33.ParameterName = "@Napomena2";
            parameter33.SqlDbType = SqlDbType.NVarChar;
            parameter33.Size = 100;
            parameter33.Direction = ParameterDirection.Input;
            parameter33.Value = Napomena2;
            myCommand.Parameters.Add(parameter33);
            /*
            SqlParameter parameter34 = new SqlParameter();
            parameter34.ParameterName = "@PostupakSaRobom";
            parameter34.SqlDbType = SqlDbType.Int;
            parameter34.Direction = ParameterDirection.Input;
            parameter34.Value = PostupakSaRobom;
            myCommand.Parameters.Add(parameter34);
            */
            SqlParameter parameter35 = new SqlParameter();
            parameter35.ParameterName = "@BTTORobeFaktura";
            parameter35.SqlDbType = SqlDbType.Decimal;
            parameter35.Direction = ParameterDirection.Input;
            parameter35.Value = BTTORobeFaktura;
            myCommand.Parameters.Add(parameter35);

            SqlParameter parameter36 = new SqlParameter();
            parameter36.ParameterName = "@BTTORobeOTP";
            parameter36.SqlDbType = SqlDbType.Decimal;
            parameter36.Direction = ParameterDirection.Input;
            parameter36.Value = BTTORobeOTP;
            myCommand.Parameters.Add(parameter36);

            SqlParameter parameter37 = new SqlParameter();
            parameter37.ParameterName = "@BTTORobeODVAGA";
            parameter37.SqlDbType = SqlDbType.Decimal;
            parameter37.Direction = ParameterDirection.Input;
            parameter37.Value = BTTORobeODVAGA;
            myCommand.Parameters.Add(parameter37);

            SqlParameter parameter38 = new SqlParameter();
            parameter38.ParameterName = "@BTTORobeKONTEJNER";
            parameter38.SqlDbType = SqlDbType.Decimal;
            parameter38.Direction = ParameterDirection.Input;
            parameter38.Value = BTTORobeKONTEJNER;
            myCommand.Parameters.Add(parameter38);

            SqlParameter parameter39 = new SqlParameter();
            parameter39.ParameterName = "@PLOMBAVLASN";
            parameter39.SqlDbType = SqlDbType.NVarChar;
            parameter39.Size = 100;
            parameter39.Direction = ParameterDirection.Input;
            parameter39.Value = PLOMBAVLASN;
            myCommand.Parameters.Add(parameter39);

            SqlParameter parameter40 = new SqlParameter();
            parameter40.ParameterName = "@CBMOTP";
            parameter40.SqlDbType = SqlDbType.NVarChar;
            parameter40.Size = 100;
            parameter40.Direction = ParameterDirection.Input;
            parameter40.Value = CBMOTP;
            myCommand.Parameters.Add(parameter40);

            SqlParameter parameter41 = new SqlParameter();
            parameter41.ParameterName = "@KOLETAOTP";
            parameter41.SqlDbType = SqlDbType.NVarChar;
            parameter41.Size = 100;
            parameter41.Direction = ParameterDirection.Input;
            parameter41.Value = KOLETAOTP;
            myCommand.Parameters.Add(parameter41);

            SqlParameter parameter42 = new SqlParameter();
            parameter42.ParameterName = "@NalogID";
            parameter42.SqlDbType = SqlDbType.Int;

            parameter42.Direction = ParameterDirection.Input;
            parameter42.Value = NalogID;
            myCommand.Parameters.Add(parameter42);


            /*
			@KOLETAOTP nvarchar(100)
            */

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

        public void InsertOtpremaKontejneraStavBuking(int Id, string BrojKontejnera, int IdNadredjenog)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "PrekopirajStavke";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@ID";
            parameter1.SqlDbType = SqlDbType.Int;

            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = Id;
            myCommand.Parameters.Add(parameter1);
            
            
          


            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@BrojKontejnera";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 20;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = BrojKontejnera;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@IdNadredjenog";
            parameter3.SqlDbType = SqlDbType.Int;

            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = IdNadredjenog;
            myCommand.Parameters.Add(parameter3);

           

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

        public void PromeniBrojVagona(int Id, string BrojVagona, double SopstvenaMasa, double BrojOsovina)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "PromeniBrojVagona";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@ID";
            parameter1.SqlDbType = SqlDbType.Int;

            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = Id;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@BrojVagona";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 20;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = BrojVagona;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@SopstvenaMasa";
            parameter4.SqlDbType = SqlDbType.Decimal;
            // parameter4.Size = 20;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = SopstvenaMasa;
            myCommand.Parameters.Add(parameter4);
            
            
            
            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@BrojOsovina";
            parameter5.SqlDbType = SqlDbType.Decimal;
            // parameter4.Size = 20;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = BrojOsovina;
            myCommand.Parameters.Add(parameter5);

          



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

        public void UpdOtpremaKontejneraVozStav(int ID, int IdNadredjenog, string BrojKontejnera, String BrojVagona, double Granica, double BrojOsovina, double SopstvenaMasa, double Tara, double Neto, int Posiljalac, int Primalac, int VlasnikKontejnera, int TipKontejnera, int VrstaRobe, string Buking, int StatusKontejnera, string BrojPlombe, int PlaniraniLager, int IdVoza, DateTime VremePripremljen, DateTime VremeOdlaska, DateTime Datum, string Korisnik, int RB, string BrojPlombe2, int Organizator, string NapomenaS, DateTime PeriodSkladistenjaOd, DateTime PeriodSkladistenjaDo, Double BTTORobe, int KontejnerID, double BTTOKOntejnera, string Napomena2, int PostupakSaRobom,  double BTTORobeFaktura, double BTTORobeOTP, double BTTORobeODVAGA, double BTTORobeKONTEJNER, string PLOMBAVLASN, string CBMOTP, string KOLETAOTP)
        {

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateOtpremaKontejneraVozStavke";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);


            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@IdNadredjenog";
            parameter1.SqlDbType = SqlDbType.Int;

            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = IdNadredjenog;
            myCommand.Parameters.Add(parameter1);



            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@BrojKontejnera";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 20;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = BrojKontejnera;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@BrojVagona";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 20;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = BrojVagona;
            myCommand.Parameters.Add(parameter3);


            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@Granica";
            parameter4.SqlDbType = SqlDbType.Decimal;
            // parameter4.Size = 20;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Granica;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@BrojOsovina";
            parameter5.SqlDbType = SqlDbType.Decimal;
            // parameter4.Size = 20;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = BrojOsovina;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@SopstvenaMasa";
            parameter6.SqlDbType = SqlDbType.Decimal;
            // parameter4.Size = 20;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = SopstvenaMasa;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@Tara";
            parameter7.SqlDbType = SqlDbType.Decimal;
            // parameter4.Size = 20;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Tara;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@Neto";
            parameter8.SqlDbType = SqlDbType.Decimal;
            // parameter4.Size = 20;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = Neto;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@Posiljalac";
            parameter9.SqlDbType = SqlDbType.Int;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Posiljalac;
            myCommand.Parameters.Add(parameter9);

            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@Primalac";
            parameter10.SqlDbType = SqlDbType.Int;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = Primalac;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@VlasnikKontejnera";
            parameter11.SqlDbType = SqlDbType.Int;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = VlasnikKontejnera;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@TipKontejnera";
            parameter12.SqlDbType = SqlDbType.Int;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = TipKontejnera;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@VrstaRobe";
            parameter13.SqlDbType = SqlDbType.Int;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = VrstaRobe;
            myCommand.Parameters.Add(parameter13);

            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@Buking";
            parameter14.SqlDbType = SqlDbType.NVarChar;
            parameter14.Size = 20;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = Buking;
            myCommand.Parameters.Add(parameter14);

            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@StatusKontejnera";
            parameter15.SqlDbType = SqlDbType.Int;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = StatusKontejnera;
            myCommand.Parameters.Add(parameter15);

            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@BrojPlombe";
            parameter16.SqlDbType = SqlDbType.NVarChar;
            parameter16.Size = 30;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = BrojPlombe;
            myCommand.Parameters.Add(parameter16);

            SqlParameter parameter17 = new SqlParameter();
            parameter17.ParameterName = "@PlaniraniLager";
            parameter17.SqlDbType = SqlDbType.Int;
            // parameter13.Size = 30;
            parameter17.Direction = ParameterDirection.Input;
            parameter17.Value = PlaniraniLager;
            myCommand.Parameters.Add(parameter17);

            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@IdVoza";
            parameter18.SqlDbType = SqlDbType.Int;
            // parameter13.Size = 30;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = IdVoza;
            myCommand.Parameters.Add(parameter18);

          

            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@VremePripremljen";
            parameter20.SqlDbType = SqlDbType.DateTime;
            // parameter13.Size = 30;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = VremePripremljen;
            myCommand.Parameters.Add(parameter20);

            SqlParameter parameter191 = new SqlParameter();
            parameter191.ParameterName = "@VremeOdlaska";
            parameter191.SqlDbType = SqlDbType.DateTime;
            // parameter13.Size = 30;
            parameter191.Direction = ParameterDirection.Input;
            parameter191.Value = VremeOdlaska;
            myCommand.Parameters.Add(parameter191);

          

            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@Datum";
            parameter22.SqlDbType = SqlDbType.Date;
           // parameter22.Size = 20;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = Datum;
            myCommand.Parameters.Add(parameter22);

            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@Korisnik";
            parameter23.SqlDbType = SqlDbType.NVarChar;
            parameter23.Size = 20;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = Korisnik;
            myCommand.Parameters.Add(parameter23);

            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@RB";
            parameter24.SqlDbType = SqlDbType.Int;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = RB;
            myCommand.Parameters.Add(parameter24);

            SqlParameter parameter25 = new SqlParameter();
            parameter25.ParameterName = "@BrojPlombe2";
            parameter25.SqlDbType = SqlDbType.NVarChar;
            parameter25.Size = 30;
            parameter25.Direction = ParameterDirection.Input;
            parameter25.Value = BrojPlombe2;
            myCommand.Parameters.Add(parameter25);

            SqlParameter parameter26 = new SqlParameter();
            parameter26.ParameterName = "@Organizator";
            parameter26.SqlDbType = SqlDbType.Int;
            parameter26.Direction = ParameterDirection.Input;
            parameter26.Value = Organizator;
            myCommand.Parameters.Add(parameter26);


            SqlParameter parameter27 = new SqlParameter();
            parameter27.ParameterName = "@NapomenaS";
            parameter27.SqlDbType = SqlDbType.NVarChar;
            parameter27.Size = 100;
            parameter27.Direction = ParameterDirection.Input;
            parameter27.Value = NapomenaS;
            myCommand.Parameters.Add(parameter27);


            SqlParameter parameter28 = new SqlParameter();
            parameter28.ParameterName = "@PeriodSkladistenjaOd";
            parameter28.SqlDbType = SqlDbType.DateTime;
            parameter28.Direction = ParameterDirection.Input;
            parameter28.Value = PeriodSkladistenjaOd;
            myCommand.Parameters.Add(parameter28);

            SqlParameter parameter29 = new SqlParameter();
            parameter29.ParameterName = "@PeriodSkladistenjaDo";
            parameter29.SqlDbType = SqlDbType.DateTime;
            parameter29.Direction = ParameterDirection.Input;
            parameter29.Value = PeriodSkladistenjaDo;
            myCommand.Parameters.Add(parameter29);

            SqlParameter parameter30 = new SqlParameter();
            parameter30.ParameterName = "@BTTORobe";
            parameter30.SqlDbType = SqlDbType.Decimal;
            parameter30.Direction = ParameterDirection.Input;
            parameter30.Value = BTTORobe;
            myCommand.Parameters.Add(parameter30);

            SqlParameter parameter31 = new SqlParameter();
            parameter31.ParameterName = "@KontejnerID";
            parameter31.SqlDbType = SqlDbType.Int;
            parameter31.Direction = ParameterDirection.Input;
            parameter31.Value = KontejnerID;
            myCommand.Parameters.Add(parameter31);





            SqlParameter parameter32 = new SqlParameter();
            parameter32.ParameterName = "@BTTOKOntejnera";
            parameter32.SqlDbType = SqlDbType.Decimal;
            parameter32.Direction = ParameterDirection.Input;
            parameter32.Value = BTTOKOntejnera;
            myCommand.Parameters.Add(parameter32);

            SqlParameter parameter33 = new SqlParameter();
            parameter33.ParameterName = "@Napomena2";
            parameter33.SqlDbType = SqlDbType.NVarChar;
            parameter33.Size = 100;
            parameter33.Direction = ParameterDirection.Input;
            parameter33.Value = Napomena2;
            myCommand.Parameters.Add(parameter33);
            /*
            SqlParameter parameter34 = new SqlParameter();
            parameter34.ParameterName = "@PostupakSaRobom";
            parameter34.SqlDbType = SqlDbType.Int;
            parameter34.Direction = ParameterDirection.Input;
            parameter34.Value = PostupakSaRobom;
            myCommand.Parameters.Add(parameter34);
            */
            SqlParameter parameter35 = new SqlParameter();
            parameter35.ParameterName = "@BTTORobeFaktura";
            parameter35.SqlDbType = SqlDbType.Decimal;
            parameter35.Direction = ParameterDirection.Input;
            parameter35.Value = BTTORobeFaktura;
            myCommand.Parameters.Add(parameter35);

            SqlParameter parameter36 = new SqlParameter();
            parameter36.ParameterName = "@BTTORobeOTP";
            parameter36.SqlDbType = SqlDbType.Decimal;
            parameter36.Direction = ParameterDirection.Input;
            parameter36.Value = BTTORobeOTP;
            myCommand.Parameters.Add(parameter36);

            SqlParameter parameter37 = new SqlParameter();
            parameter37.ParameterName = "@BTTORobeODVAGA";
            parameter37.SqlDbType = SqlDbType.Decimal;
            parameter37.Direction = ParameterDirection.Input;
            parameter37.Value = BTTORobeODVAGA;
            myCommand.Parameters.Add(parameter37);

            SqlParameter parameter38 = new SqlParameter();
            parameter38.ParameterName = "@BTTORobeKONTEJNER";
            parameter38.SqlDbType = SqlDbType.Decimal;
            parameter38.Direction = ParameterDirection.Input;
            parameter38.Value = BTTORobeKONTEJNER;
            myCommand.Parameters.Add(parameter38);

            SqlParameter parameter39 = new SqlParameter();
            parameter39.ParameterName = "@PLOMBAVLASN";
            parameter39.SqlDbType = SqlDbType.NVarChar;
            parameter39.Size = 100;
            parameter39.Direction = ParameterDirection.Input;
            parameter39.Value = PLOMBAVLASN;
            myCommand.Parameters.Add(parameter39);

            SqlParameter parameter40 = new SqlParameter();
            parameter40.ParameterName = "@CBMOTP";
            parameter40.SqlDbType = SqlDbType.NVarChar;
            parameter40.Size = 100;
            parameter40.Direction = ParameterDirection.Input;
            parameter40.Value = CBMOTP;
            myCommand.Parameters.Add(parameter40);

            SqlParameter parameter41 = new SqlParameter();
            parameter41.ParameterName = "@KOLETAOTP";
            parameter41.SqlDbType = SqlDbType.NVarChar;
            parameter41.Size = 100;
            parameter41.Direction = ParameterDirection.Input;
            parameter41.Value = KOLETAOTP;
            myCommand.Parameters.Add(parameter41);


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
                throw new Exception("Neuspešan upis u Bazu");
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

        public void DeleteOtpremaKontejneraVozStav(int ID)
          {
              var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
              SqlConnection myConnection = new SqlConnection(s_connection);
              SqlCommand myCommand = myConnection.CreateCommand();
              myCommand.CommandText = "DeleteOtpremaKontejneraVozStavke";
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
                      MessageBox.Show("Brisanje uspešno završeno", "",
                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                  }
                  myConnection.Close();

                  if (error)
                  {
                      // Nedra.DataSet1TableAdapters.QueriesTableAdapter adapter = new Nedra.DataSet1TableAdapters.QueriesTableAdapter();
                  }
              }
          }

        public void UpdOtpremaKontejneraVozStavVagoni(int ID, string BrojVagona)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdateOtpremaKontVozStavkeVagon";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@BrojVagona";
            parameter1.SqlDbType = SqlDbType.NVarChar;
            parameter1.Size = 20;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = BrojVagona;
            myCommand.Parameters.Add(parameter1);

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
                    MessageBox.Show("Brisanje uspešno završeno", "",
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




