using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Saobracaj.Sifarnici
{
    class InsertPartnerji
    {
        public string connect = Sifarnici.frmLogovanje.connectionString;

        public void InsPartneri(string Naziv, string Ulica, string Mesto, string Posta, string Drzava, string Telefon, string TR, string Napomena, string MaticniBroj, string Email, string PIB, string UIC, bool Prevoznik, bool Posiljalac, bool Primalac, int Brodar, int Vlasnik, int Spediter, int Platilac, int Organizator, int Nalogodavac, int Uvoznik, string MUAdresa, string MUKontakt, string UICDrzava, string TR2, string Faks, int PomIzvoznik, int Logisticar, int Kamioner, int Agent, string Kupac, string Obveznik, string Valuta, string Dobavljac, int Referent, int FREC)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertParnerji";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@PaNaziv";
            parameter1.SqlDbType = SqlDbType.Char;
            parameter1.Size = 35;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = Naziv;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@PaUlicaHisnaSt";
            parameter2.SqlDbType = SqlDbType.Char;
            parameter2.Size = 35;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Ulica;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@PaKraj";
            parameter3.SqlDbType = SqlDbType.Char;
            parameter3.Size = 35;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Mesto;
            myCommand.Parameters.Add(parameter3);

            /*SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@PaDelDrzave";
            parameter4.SqlDbType = SqlDbType.Char;
            parameter4.Size = 9;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Oblast;
            myCommand.Parameters.Add(parameter4);*/

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@PaPostnaSt";
            parameter5.SqlDbType = SqlDbType.Char;
            parameter5.Size = 9;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Posta;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@PaSifDrzave";
            parameter6.SqlDbType = SqlDbType.Char;
            parameter6.Size = 3;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Drzava;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@PaTelefon1";
            parameter7.SqlDbType = SqlDbType.Char;
            parameter7.Size = 17;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Telefon;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@PaZiroRac";
            parameter8.SqlDbType = SqlDbType.Char;
            parameter8.Size = 44;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = TR;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@PaOpomba";
            parameter9.SqlDbType = SqlDbType.VarChar;
            parameter9.Size = 2048;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Napomena;
            myCommand.Parameters.Add(parameter9);


            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@PaDMatSt";
            parameter10.SqlDbType = SqlDbType.Char;
            parameter10.Size = 35;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = MaticniBroj;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@PaEMail";
            parameter11.SqlDbType = SqlDbType.Char;
            parameter11.Size = 70;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = Email;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@PaEMatSt1";
            parameter12.SqlDbType = SqlDbType.Char;
            parameter12.Size = 35;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = PIB;
            myCommand.Parameters.Add(parameter12);

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@Kupac";
            sqlParameter.SqlDbType = SqlDbType.Char;
            sqlParameter.Size = 1;
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = Kupac;
            myCommand.Parameters.Add(sqlParameter);

            SqlParameter sqlParameter2 = new SqlParameter();
            sqlParameter2.ParameterName = "@Obveznik";
            sqlParameter2.SqlDbType = SqlDbType.Char;
            sqlParameter2.Size = 1;
            sqlParameter2.Direction = ParameterDirection.Input;
            sqlParameter2.Value = Obveznik;
            myCommand.Parameters.Add(sqlParameter2);

            SqlParameter sqlParameter3 = new SqlParameter();
            sqlParameter3.ParameterName = "@Valuta";
            sqlParameter3.SqlDbType = SqlDbType.Char;
            sqlParameter3.Size = 3;
            sqlParameter3.Direction = ParameterDirection.Input;
            sqlParameter3.Value = Valuta;
            myCommand.Parameters.Add(sqlParameter3);

            SqlParameter sqlParameter1 = new SqlParameter();
            sqlParameter1.ParameterName = "@Dobavljac";
            sqlParameter1.SqlDbType = SqlDbType.Char;
            sqlParameter1.Size = 1;
            sqlParameter1.Direction = ParameterDirection.Input;
            sqlParameter1.Value = Dobavljac;
            myCommand.Parameters.Add(sqlParameter1);
            /*
                     ,< PaNaziv, char(35),>
                     ,< PaUlicaHisnaSt, char(35),>
                     ,< PaKraj, char(35),>
                     ,< PaDelDrzave, char(9),>
                     ,< PaPostnaSt, char(9),>
                     ,< PaSifDrzave, char(3),>
                     ,< PaTelefon1, char(17),>
                     ,< PaZiroRac, char(44),>
                     ,< PaOpomba, varchar(2048),>
                     ,< PaDMatSt, char(35),>
                     ,< PaEMail, char(70),>
                     ,< PaEMatSt1, char(35),>

                     ,< UIC, nvarchar(10),>
                     ,< Prevoznik, tinyint,>
                     ,< Posiljalac, tinyint,>
                     ,< Primalac, tinyint,>)
            */
            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@UIC";
            parameter13.SqlDbType = SqlDbType.NVarChar;
            parameter13.Size = 10;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = UIC;
            myCommand.Parameters.Add(parameter13);

            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@Prevoznik";
            parameter14.SqlDbType = SqlDbType.TinyInt;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = Prevoznik;
            myCommand.Parameters.Add(parameter14);

            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@Posiljalac";
            parameter15.SqlDbType = SqlDbType.TinyInt;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = Posiljalac;
            myCommand.Parameters.Add(parameter15);

            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@Primalac";
            parameter16.SqlDbType = SqlDbType.TinyInt;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = Primalac;
            myCommand.Parameters.Add(parameter16);



            SqlParameter parameter17 = new SqlParameter();
            parameter17.ParameterName = "@Brodar";
            parameter17.SqlDbType = SqlDbType.Int;
            parameter17.Direction = ParameterDirection.Input;
            parameter17.Value = Brodar;
            myCommand.Parameters.Add(parameter17);

            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@Vlasnik";
            parameter18.SqlDbType = SqlDbType.Int;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = Vlasnik;
            myCommand.Parameters.Add(parameter18);

            SqlParameter parameter19 = new SqlParameter();
            parameter19.ParameterName = "@Spediter";
            parameter19.SqlDbType = SqlDbType.Int;
            parameter19.Direction = ParameterDirection.Input;
            parameter19.Value = Spediter;
            myCommand.Parameters.Add(parameter19);

            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@Platilac";
            parameter20.SqlDbType = SqlDbType.Int;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = Platilac;
            myCommand.Parameters.Add(parameter20);

            SqlParameter parameter21 = new SqlParameter();
            parameter21.ParameterName = "@Organizator";
            parameter21.SqlDbType = SqlDbType.Int;
            parameter21.Direction = ParameterDirection.Input;
            parameter21.Value = Organizator;
            myCommand.Parameters.Add(parameter21);


            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@Nalogodavac";
            parameter22.SqlDbType = SqlDbType.Int;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = Nalogodavac;
            myCommand.Parameters.Add(parameter22);

            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@Uvoznik";
            parameter23.SqlDbType = SqlDbType.Int;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = Uvoznik;
            myCommand.Parameters.Add(parameter23);


            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@MUAdresa";
            parameter24.SqlDbType = SqlDbType.NVarChar;
            parameter24.Size = 150;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = MUAdresa;
            myCommand.Parameters.Add(parameter24);

            SqlParameter parameter25 = new SqlParameter();
            parameter25.ParameterName = "@MUKontakt";
            parameter25.SqlDbType = SqlDbType.NVarChar;
            parameter25.Size = 150;
            parameter25.Direction = ParameterDirection.Input;
            parameter25.Value = MUKontakt;
            myCommand.Parameters.Add(parameter25);


            SqlParameter parameter27 = new SqlParameter();
            parameter27.ParameterName = "@UICDrzava";
            parameter27.SqlDbType = SqlDbType.NVarChar;
            parameter27.Size = 60;
            parameter27.Direction = ParameterDirection.Input;
            parameter27.Value = UICDrzava;
            myCommand.Parameters.Add(parameter27);


            //string UICDrzava , string TR2, string Faks , int PomIzvoznik


            SqlParameter parameter28 = new SqlParameter();
            parameter28.ParameterName = "@TR2";
            parameter28.SqlDbType = SqlDbType.NVarChar;
            parameter28.Size = 60;
            parameter28.Direction = ParameterDirection.Input;
            parameter28.Value = TR2;
            myCommand.Parameters.Add(parameter28);

            SqlParameter parameter29 = new SqlParameter();
            parameter29.ParameterName = "@Faks";
            parameter29.SqlDbType = SqlDbType.NVarChar;
            parameter29.Size = 60;
            parameter29.Direction = ParameterDirection.Input;
            parameter29.Value = Faks;
            myCommand.Parameters.Add(parameter29);

            SqlParameter parameter30 = new SqlParameter();
            parameter30.ParameterName = "@PomIzvoznik";
            parameter30.SqlDbType = SqlDbType.Int;
            parameter30.Direction = ParameterDirection.Input;
            parameter30.Value = PomIzvoznik;
            myCommand.Parameters.Add(parameter30);

            SqlParameter parameter31 = new SqlParameter();
            parameter31.ParameterName = "@Logisticar";
            parameter31.SqlDbType = SqlDbType.Int;
            parameter31.Direction = ParameterDirection.Input;
            parameter31.Value = Logisticar;
            myCommand.Parameters.Add(parameter31);

            SqlParameter parameter32 = new SqlParameter();
            parameter32.ParameterName = "@Kamioner";
            parameter32.SqlDbType = SqlDbType.Int;
            parameter32.Direction = ParameterDirection.Input;
            parameter32.Value = Kamioner;
            myCommand.Parameters.Add(parameter32);

            SqlParameter parameter33 = new SqlParameter();
            parameter33.ParameterName = "@Agent";
            parameter33.SqlDbType = SqlDbType.Int;
            parameter33.Direction = ParameterDirection.Input;
            parameter33.Value = Agent;
            myCommand.Parameters.Add(parameter33);

            SqlParameter sqlParameter4 = new SqlParameter();
            sqlParameter4.ParameterName = "@Referent";
            sqlParameter4.SqlDbType = SqlDbType.Int;
            sqlParameter4.Direction = ParameterDirection.Input;
            sqlParameter4.Value = Referent;
            myCommand.Parameters.Add(sqlParameter4);

            SqlParameter sqlParameter41 = new SqlParameter();
            sqlParameter41.ParameterName = "@FREC";
            sqlParameter41.SqlDbType = SqlDbType.Int;
            sqlParameter41.Direction = ParameterDirection.Input;
            sqlParameter41.Value = FREC;
            myCommand.Parameters.Add(sqlParameter41);


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

        public void UpdPartneri(int ID, string Naziv, string Ulica, string Mesto, string Oblast, string Posta, string Drzava, string Telefon, string TR, string Napomena, string MaticniBroj, string Email, string PIB, string UIC, bool Prevoznik, bool Posiljalac, bool Primalac, int Brodar, int Vlasnik, int Spediter, int Platilac, int Organizator, int Nalogodavac, int Uvoznik, string MUAdresa, string MUKontakt, string UICDrzava, string TR2, string Faks, int PomIzvoznik, int Logisticar, int Kamioner, int Agent, string Kupac, string Obveznik, string Dobavljac, string Valuta, int FREC)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "UpdatePartneri";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@ID";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;
            myCommand.Parameters.Add(parameter);

            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@PaNaziv";
            parameter1.SqlDbType = SqlDbType.Char;
            parameter1.Size = 35;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = Naziv;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@PaUlicaHisnaSt";
            parameter2.SqlDbType = SqlDbType.Char;
            parameter2.Size = 35;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Ulica;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@PaKraj";
            parameter3.SqlDbType = SqlDbType.Char;
            parameter3.Size = 35;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Mesto;
            myCommand.Parameters.Add(parameter3);

            SqlParameter parameter4 = new SqlParameter();
            parameter4.ParameterName = "@PaDelDrzave";
            parameter4.SqlDbType = SqlDbType.Char;
            parameter4.Size = 9;
            parameter4.Direction = ParameterDirection.Input;
            parameter4.Value = Oblast;
            myCommand.Parameters.Add(parameter4);

            SqlParameter parameter5 = new SqlParameter();
            parameter5.ParameterName = "@PaPostnaSt";
            parameter5.SqlDbType = SqlDbType.Char;
            parameter5.Size = 9;
            parameter5.Direction = ParameterDirection.Input;
            parameter5.Value = Posta;
            myCommand.Parameters.Add(parameter5);

            SqlParameter parameter6 = new SqlParameter();
            parameter6.ParameterName = "@PaSifDrzave";
            parameter6.SqlDbType = SqlDbType.Char;
            parameter6.Size = 3;
            parameter6.Direction = ParameterDirection.Input;
            parameter6.Value = Drzava;
            myCommand.Parameters.Add(parameter6);

            SqlParameter parameter7 = new SqlParameter();
            parameter7.ParameterName = "@PaTelefon1";
            parameter7.SqlDbType = SqlDbType.Char;
            parameter7.Size = 17;
            parameter7.Direction = ParameterDirection.Input;
            parameter7.Value = Telefon;
            myCommand.Parameters.Add(parameter7);

            SqlParameter parameter8 = new SqlParameter();
            parameter8.ParameterName = "@PaZiroRac";
            parameter8.SqlDbType = SqlDbType.Char;
            parameter8.Size = 44;
            parameter8.Direction = ParameterDirection.Input;
            parameter8.Value = TR;
            myCommand.Parameters.Add(parameter8);

            SqlParameter parameter9 = new SqlParameter();
            parameter9.ParameterName = "@PaOpomba";
            parameter9.SqlDbType = SqlDbType.VarChar;
            parameter9.Size = 2048;
            parameter9.Direction = ParameterDirection.Input;
            parameter9.Value = Napomena;
            myCommand.Parameters.Add(parameter9);


            SqlParameter parameter10 = new SqlParameter();
            parameter10.ParameterName = "@PaDMatSt";
            parameter10.SqlDbType = SqlDbType.Char;
            parameter10.Size = 35;
            parameter10.Direction = ParameterDirection.Input;
            parameter10.Value = MaticniBroj;
            myCommand.Parameters.Add(parameter10);

            SqlParameter parameter11 = new SqlParameter();
            parameter11.ParameterName = "@PaEMail";
            parameter11.SqlDbType = SqlDbType.Char;
            parameter11.Size = 70;
            parameter11.Direction = ParameterDirection.Input;
            parameter11.Value = Email;
            myCommand.Parameters.Add(parameter11);

            SqlParameter parameter12 = new SqlParameter();
            parameter12.ParameterName = "@PaEMatSt1";
            parameter12.SqlDbType = SqlDbType.Char;
            parameter12.Size = 35;
            parameter12.Direction = ParameterDirection.Input;
            parameter12.Value = PIB;
            myCommand.Parameters.Add(parameter12);

            SqlParameter parameter13 = new SqlParameter();
            parameter13.ParameterName = "@UIC";
            parameter13.SqlDbType = SqlDbType.NVarChar;
            parameter13.Size = 10;
            parameter13.Direction = ParameterDirection.Input;
            parameter13.Value = UIC;
            myCommand.Parameters.Add(parameter13);

            SqlParameter parameter14 = new SqlParameter();
            parameter14.ParameterName = "@Prevoznik";
            parameter14.SqlDbType = SqlDbType.TinyInt;
            parameter14.Direction = ParameterDirection.Input;
            parameter14.Value = Prevoznik;
            myCommand.Parameters.Add(parameter14);

            SqlParameter parameter15 = new SqlParameter();
            parameter15.ParameterName = "@Posiljalac";
            parameter15.SqlDbType = SqlDbType.TinyInt;
            parameter15.Direction = ParameterDirection.Input;
            parameter15.Value = Posiljalac;
            myCommand.Parameters.Add(parameter15);

            SqlParameter parameter16 = new SqlParameter();
            parameter16.ParameterName = "@Primalac";
            parameter16.SqlDbType = SqlDbType.TinyInt;
            parameter16.Direction = ParameterDirection.Input;
            parameter16.Value = Primalac;
            myCommand.Parameters.Add(parameter16);

            SqlParameter parameter17 = new SqlParameter();
            parameter17.ParameterName = "@Brodar";
            parameter17.SqlDbType = SqlDbType.Int;
            parameter17.Direction = ParameterDirection.Input;
            parameter17.Value = Brodar;
            myCommand.Parameters.Add(parameter17);

            SqlParameter parameter18 = new SqlParameter();
            parameter18.ParameterName = "@Vlasnik";
            parameter18.SqlDbType = SqlDbType.Int;
            parameter18.Direction = ParameterDirection.Input;
            parameter18.Value = Vlasnik;
            myCommand.Parameters.Add(parameter18);

            SqlParameter parameter19 = new SqlParameter();
            parameter19.ParameterName = "@Spediter";
            parameter19.SqlDbType = SqlDbType.Int;
            parameter19.Direction = ParameterDirection.Input;
            parameter19.Value = Spediter;
            myCommand.Parameters.Add(parameter19);

            SqlParameter parameter20 = new SqlParameter();
            parameter20.ParameterName = "@Platilac";
            parameter20.SqlDbType = SqlDbType.Int;
            parameter20.Direction = ParameterDirection.Input;
            parameter20.Value = Platilac;
            myCommand.Parameters.Add(parameter20);

            SqlParameter parameter21 = new SqlParameter();
            parameter21.ParameterName = "@Organizator";
            parameter21.SqlDbType = SqlDbType.Int;
            parameter21.Direction = ParameterDirection.Input;
            parameter21.Value = Organizator;
            myCommand.Parameters.Add(parameter21);

            SqlParameter parameter22 = new SqlParameter();
            parameter22.ParameterName = "@Nalogodavac";
            parameter22.SqlDbType = SqlDbType.Int;
            parameter22.Direction = ParameterDirection.Input;
            parameter22.Value = Nalogodavac;
            myCommand.Parameters.Add(parameter22);

            SqlParameter parameter23 = new SqlParameter();
            parameter23.ParameterName = "@Uvoznik";
            parameter23.SqlDbType = SqlDbType.Int;
            parameter23.Direction = ParameterDirection.Input;
            parameter23.Value = Uvoznik;
            myCommand.Parameters.Add(parameter23);


            SqlParameter parameter24 = new SqlParameter();
            parameter24.ParameterName = "@MUAdresa";
            parameter24.SqlDbType = SqlDbType.NVarChar;
            parameter24.Size = 150;
            parameter24.Direction = ParameterDirection.Input;
            parameter24.Value = MUAdresa;
            myCommand.Parameters.Add(parameter24);

            SqlParameter parameter25 = new SqlParameter();
            parameter25.ParameterName = "@MUKontakt";
            parameter25.SqlDbType = SqlDbType.NVarChar;
            parameter25.Size = 150;
            parameter25.Direction = ParameterDirection.Input;
            parameter25.Value = MUKontakt;
            myCommand.Parameters.Add(parameter25);


            SqlParameter parameter27 = new SqlParameter();
            parameter27.ParameterName = "@UICDrzava";
            parameter27.SqlDbType = SqlDbType.NVarChar;
            parameter27.Size = 60;
            parameter27.Direction = ParameterDirection.Input;
            parameter27.Value = UICDrzava;
            myCommand.Parameters.Add(parameter27);


            //string UICDrzava , string TR2, string Faks , int PomIzvoznik


            SqlParameter parameter28 = new SqlParameter();
            parameter28.ParameterName = "@TR2";
            parameter28.SqlDbType = SqlDbType.NVarChar;
            parameter28.Size = 60;
            parameter28.Direction = ParameterDirection.Input;
            parameter28.Value = TR2;
            myCommand.Parameters.Add(parameter28);

            SqlParameter parameter29 = new SqlParameter();
            parameter29.ParameterName = "@Faks";
            parameter29.SqlDbType = SqlDbType.NVarChar;
            parameter29.Size = 60;
            parameter29.Direction = ParameterDirection.Input;
            parameter29.Value = Faks;
            myCommand.Parameters.Add(parameter29);

            SqlParameter parameter30 = new SqlParameter();
            parameter30.ParameterName = "@PomIzvoznik";
            parameter30.SqlDbType = SqlDbType.Int;
            parameter30.Direction = ParameterDirection.Input;
            parameter30.Value = PomIzvoznik;
            myCommand.Parameters.Add(parameter30);

            SqlParameter parameter31 = new SqlParameter();
            parameter31.ParameterName = "@Logisticar";
            parameter31.SqlDbType = SqlDbType.Int;
            parameter31.Direction = ParameterDirection.Input;
            parameter31.Value = Logisticar;
            myCommand.Parameters.Add(parameter31);

            SqlParameter parameter32 = new SqlParameter();
            parameter32.ParameterName = "@Kamioner";
            parameter32.SqlDbType = SqlDbType.Int;
            parameter32.Direction = ParameterDirection.Input;
            parameter32.Value = Kamioner;
            myCommand.Parameters.Add(parameter32);

            SqlParameter parameter33 = new SqlParameter();
            parameter33.ParameterName = "@Agent";
            parameter33.SqlDbType = SqlDbType.Int;
            parameter33.Direction = ParameterDirection.Input;
            parameter33.Value = Agent;
            myCommand.Parameters.Add(parameter33);

            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@Kupac";
            sqlParameter.SqlDbType = SqlDbType.Char;
            sqlParameter.Size = 1;
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.Value = Kupac;
            myCommand.Parameters.Add(sqlParameter);

            SqlParameter sqlParameter2 = new SqlParameter();
            sqlParameter2.ParameterName = "@Obveznik";
            sqlParameter2.SqlDbType = SqlDbType.Char;
            sqlParameter2.Size = 1;
            sqlParameter2.Direction = ParameterDirection.Input;
            sqlParameter2.Value = Obveznik;
            myCommand.Parameters.Add(sqlParameter2);

            SqlParameter sqlParameter1 = new SqlParameter();
            sqlParameter1.ParameterName = "@Dobavljac";
            sqlParameter1.SqlDbType = SqlDbType.Char;
            sqlParameter1.Size = 1;
            sqlParameter1.Direction = ParameterDirection.Input;
            sqlParameter1.Value = Dobavljac;
            myCommand.Parameters.Add(sqlParameter1);

            SqlParameter sqlParameter3 = new SqlParameter();
            sqlParameter3.ParameterName = "@Valuta";
            sqlParameter3.SqlDbType = SqlDbType.Char;
            sqlParameter3.Size = 3;
            sqlParameter3.Direction = ParameterDirection.Input;
            sqlParameter3.Value = Valuta;
            myCommand.Parameters.Add(sqlParameter3);

            SqlParameter sqlParameter41 = new SqlParameter();
            sqlParameter41.ParameterName = "@FREC";
            sqlParameter41.SqlDbType = SqlDbType.Int;
            sqlParameter41.Direction = ParameterDirection.Input;
            sqlParameter41.Value = FREC;
            myCommand.Parameters.Add(sqlParameter41);
            /*
            
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

            catch (SqlException ex)
            {
                throw new Exception("Neuspešna promena podataka\n" + ex.ToString());
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

        public void DelPartneri(int ID)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "DeletePartnerji";
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
        public void InsDrzave(string Korisnik,string Drzava, string Oznaka, string Valuta)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertDrzave";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter par = new SqlParameter();
            par.ParameterName = "@Korisnik";
            par.SqlDbType = SqlDbType.NVarChar;
            par.Direction = ParameterDirection.Input;
            par.Size = 50;
            par.Value = Korisnik;
            myCommand.Parameters.Add(par);


            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@Drzava";
            parameter1.SqlDbType = SqlDbType.NVarChar;
            parameter1.Size = 100;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = Drzava;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Oznaka";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 10;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Oznaka;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Valuta";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 10;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Valuta;
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
        public void InsPoste(string Korisnik, string Sifra, string Naziv, string Oznaka)
        {
            SqlConnection myConnection = new SqlConnection(connect);
            SqlCommand myCommand = myConnection.CreateCommand();
            myCommand.CommandText = "InsertPoste";
            myCommand.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter par = new SqlParameter();
            par.ParameterName = "@Korisnik";
            par.SqlDbType = SqlDbType.NVarChar;
            par.Direction = ParameterDirection.Input;
            par.Size = 50;
            par.Value = Korisnik;
            myCommand.Parameters.Add(par);


            SqlParameter parameter1 = new SqlParameter();
            parameter1.ParameterName = "@Sifra";
            parameter1.SqlDbType = SqlDbType.NVarChar;
            parameter1.Size = 10;
            parameter1.Direction = ParameterDirection.Input;
            parameter1.Value = Sifra;
            myCommand.Parameters.Add(parameter1);

            SqlParameter parameter2 = new SqlParameter();
            parameter2.ParameterName = "@Naziv";
            parameter2.SqlDbType = SqlDbType.NVarChar;
            parameter2.Size = 50;
            parameter2.Direction = ParameterDirection.Input;
            parameter2.Value = Naziv;
            myCommand.Parameters.Add(parameter2);

            SqlParameter parameter3 = new SqlParameter();
            parameter3.ParameterName = "@Oznaka";
            parameter3.SqlDbType = SqlDbType.NVarChar;
            parameter3.Size = 10;
            parameter3.Direction = ParameterDirection.Input;
            parameter3.Value = Oznaka;
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
