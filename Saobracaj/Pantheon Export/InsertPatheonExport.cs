﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Syncfusion.Styles;

namespace Saobracaj.Pantheon_Export
{
    internal class InsertPatheonExport
    {
        public string connect = Sifarnici.frmLogovanje.connectionString;

        public void InsUlFak(int ID, string CRMDocID, string VrstaDokumenta, string FakturaBr, int IDDobavljaca, string Tip, DateTime DatumPrijema, string Valuta, decimal Kurs, string RacunDobavljaca, DateTime DatumIzdavanja,
            DateTime DatumPDVa, DateTime DatumValute, int Referent, int Predvidjanje, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "InsertUlFak";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@CRMDocID", SqlDbType.Char, 30) { Value = CRMDocID });
                            cmd.Parameters.Add(new SqlParameter("@VrstaDokumenta", SqlDbType.Char, 4) { Value = VrstaDokumenta });
                            cmd.Parameters.Add(new SqlParameter("@FakturaBr", SqlDbType.Char, 20) { Value = FakturaBr });
                            cmd.Parameters.Add(new SqlParameter("@IDDobavljaca", SqlDbType.Int) { Value = IDDobavljaca });
                            cmd.Parameters.Add(new SqlParameter("@Tip", SqlDbType.Char, 1) { Value = Tip });
                            cmd.Parameters.Add(new SqlParameter("@DatumPrijema", SqlDbType.DateTime) { Value = DatumPrijema });
                            cmd.Parameters.Add(new SqlParameter("@Valuta", SqlDbType.Char, 30) { Value = Valuta });
                            cmd.Parameters.Add(new SqlParameter("@Kurs", SqlDbType.Decimal) { Value = Kurs });
                            cmd.Parameters.Add(new SqlParameter("@RacunDobavljaca", SqlDbType.Char, 35) { Value = RacunDobavljaca });
                            cmd.Parameters.Add(new SqlParameter("@DatumIzdavanja", SqlDbType.DateTime) { Value = DatumIzdavanja });
                            cmd.Parameters.Add(new SqlParameter("@DatumPDVa", SqlDbType.DateTime) { Value = DatumPDVa });
                            cmd.Parameters.Add(new SqlParameter("@DatumValute", SqlDbType.DateTime) { Value = DatumValute });
                            cmd.Parameters.Add(new SqlParameter("@Referent", SqlDbType.Int) { Value = Referent });
                            cmd.Parameters.Add(new SqlParameter("@Predvidjanje", SqlDbType.Int) { Value = Predvidjanje });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 255) { Value = Napomena });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }
        public void UpdUlFak(int ID, string CRMDocID, string VrstaDokumenta, string FakturaBr, int IDDobavljaca, string Tip, DateTime DatumPrijema, string Valuta, decimal Kurs, string RacunDobavljaca, DateTime DatumIzdavanja,
            DateTime DatumPDVa, DateTime DatumValute, int Referent, int Predvidjanje, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "UpdateUlFak";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@CRMDocID", SqlDbType.Char, 30) { Value = CRMDocID });
                            cmd.Parameters.Add(new SqlParameter("@VrstaDokumenta", SqlDbType.Char, 4) { Value = VrstaDokumenta });
                            cmd.Parameters.Add(new SqlParameter("@FakturaBr", SqlDbType.Char, 20) { Value = FakturaBr });
                            cmd.Parameters.Add(new SqlParameter("@IDDobavljaca", SqlDbType.Int) { Value = IDDobavljaca });
                            cmd.Parameters.Add(new SqlParameter("@Tip", SqlDbType.Char, 1) { Value = Tip });
                            cmd.Parameters.Add(new SqlParameter("@DatumPrijema", SqlDbType.DateTime) { Value = DatumPrijema });
                            cmd.Parameters.Add(new SqlParameter("@Valuta", SqlDbType.Char, 30) { Value = Valuta });
                            cmd.Parameters.Add(new SqlParameter("@Kurs", SqlDbType.Decimal) { Value = Kurs });
                            cmd.Parameters.Add(new SqlParameter("@RacunDobavljaca", SqlDbType.Char, 35) { Value = RacunDobavljaca });
                            cmd.Parameters.Add(new SqlParameter("@DatumIzdavanja", SqlDbType.DateTime) { Value = DatumIzdavanja });
                            cmd.Parameters.Add(new SqlParameter("@DatumPDVa", SqlDbType.DateTime) { Value = DatumPDVa });
                            cmd.Parameters.Add(new SqlParameter("@DatumValute", SqlDbType.DateTime) { Value = DatumValute });
                            cmd.Parameters.Add(new SqlParameter("@Referent", SqlDbType.Int) { Value = Referent });
                            cmd.Parameters.Add(new SqlParameter("@Predvidjanje", SqlDbType.Int) { Value = Predvidjanje });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 255) { Value = Napomena });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }
        public void InsUlFakPostav(int IDFak, int RB, int MP, decimal Kolicina, decimal Cena, int NosilacTroska, string JM, string Proizvod)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "InsertUlFakPostav";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@IDFak", SqlDbType.Int) { Value = IDFak });
                            cmd.Parameters.Add(new SqlParameter("@RB", SqlDbType.Int) { Value = RB });
                            cmd.Parameters.Add(new SqlParameter("@MP", SqlDbType.Int) { Value = MP });
                            cmd.Parameters.Add(new SqlParameter("@Kolicina", SqlDbType.Decimal) { Value = Kolicina });
                            cmd.Parameters.Add(new SqlParameter("@Cena", SqlDbType.Decimal) { Value = Cena });
                            cmd.Parameters.Add(new SqlParameter("@NosilacTroska", SqlDbType.Int) { Value = NosilacTroska });
                            cmd.Parameters.Add(new SqlParameter("@JM", SqlDbType.Char, 30) { Value = JM });
                            cmd.Parameters.Add(new SqlParameter("@Proizvod", SqlDbType.NVarChar) { Value = Proizvod });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }
        public void InsNosiociTroskova(string NosilacTroska, string NazivNosiocaTroska, string Grupa, int Kupac, string Odeljenje)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "InsertNosiociTroskova";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@NosilacTroska", SqlDbType.Char, 30) { Value = NosilacTroska });
                            cmd.Parameters.Add(new SqlParameter("@NazivNosiocaTroska", SqlDbType.Char, 100) { Value = NazivNosiocaTroska });
                            cmd.Parameters.Add(new SqlParameter("@Grupa", SqlDbType.Char, 16) { Value = Grupa });
                            cmd.Parameters.Add(new SqlParameter("@Kupac", SqlDbType.Int) { Value = Kupac });
                            cmd.Parameters.Add(new SqlParameter("@Odeljenje", SqlDbType.Char, 30) { Value = Odeljenje });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }
        public void UpdNosiociTroskova(int ID, string NosilacTroska, string NazivNosiocaTroska, string Grupa, int Kupac, string Odeljenje)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "UpdateNosiociTroska";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@NosilacTroska", SqlDbType.Char, 30) { Value = NosilacTroska });
                            cmd.Parameters.Add(new SqlParameter("@NazivNosiocaTroska", SqlDbType.Char, 100) { Value = NazivNosiocaTroska });
                            cmd.Parameters.Add(new SqlParameter("@Grupa", SqlDbType.Char, 16) { Value = Grupa });
                            cmd.Parameters.Add(new SqlParameter("@Kupac", SqlDbType.Int) { Value = Kupac });
                            cmd.Parameters.Add(new SqlParameter("@Odeljenje", SqlDbType.Char, 30) { Value = Odeljenje });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }
        public void DelNosiociTroskova(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "DeleteNosiociTroska";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }
        public void InsPredvidjanje(string PredvidjanjeID, int PredvidjanjePoz, DateTime Datum, int Subjekat, int NosilacTroska, string Odeljenje, decimal Iznos, string Valuta, int Status)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "InsertPredvidjanje";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@PredividjanjeId", SqlDbType.Char, 30) { Value = PredvidjanjeID });
                            cmd.Parameters.Add(new SqlParameter("@PredvidjanjePoz", SqlDbType.Int) { Value = PredvidjanjePoz });
                            cmd.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime) { Value = Datum });
                            cmd.Parameters.Add(new SqlParameter("@Subjekt", SqlDbType.Int) { Value = Subjekat });
                            cmd.Parameters.Add(new SqlParameter("@NosilacTroska", SqlDbType.Int) { Value = NosilacTroska });
                            cmd.Parameters.Add(new SqlParameter("@Odeljenje", SqlDbType.Char, 30) { Value = Odeljenje });
                            cmd.Parameters.Add(new SqlParameter("@Iznos", SqlDbType.Decimal) { Value = Iznos });
                            cmd.Parameters.Add(new SqlParameter("@Valuta", SqlDbType.Char, 3) { Value = Valuta });
                            cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int) { Value = Status });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspesan upis predvidjanja", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }
        public void UpdPredvidjanje(int ID, string PredvidjanjeID, int PredvidjanjePoz, DateTime Datum, int Subjekat, int NosilacTroska, string Odeljenje, decimal Iznos, string Valuta, int Status)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "UpdatePredvidjanje";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@PredividjanjeId", SqlDbType.Char, 30) { Value = PredvidjanjeID });
                            cmd.Parameters.Add(new SqlParameter("@PredvidjanjePoz", SqlDbType.Int) { Value = PredvidjanjePoz });
                            cmd.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime) { Value = Datum });
                            cmd.Parameters.Add(new SqlParameter("@Subjekt", SqlDbType.Int) { Value = Subjekat });
                            cmd.Parameters.Add(new SqlParameter("@NosilacTroska", SqlDbType.Int) { Value = NosilacTroska });
                            cmd.Parameters.Add(new SqlParameter("@Odeljenje", SqlDbType.Char, 30) { Value = Odeljenje });
                            cmd.Parameters.Add(new SqlParameter("@Iznos", SqlDbType.Decimal) { Value = Iznos });
                            cmd.Parameters.Add(new SqlParameter("@Valuta", SqlDbType.Char, 3) { Value = Valuta });
                            cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Int) { Value = Status });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspesan upis predvidjanja", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }
        public void DelPredvidjanje(int ID)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "DeletePredvidjanje";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspesan upis predvidjanja", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }
        public void InstFaktura(int ID, DateTime DatumDok, int Primalac, string Ulica, string Naziv, string Mesto, string MB, string Valuta, decimal Kurs, DateTime DatumPDV, DateTime DatumValute, string MestoUtovara, DateTime DatumUtovara,
            string MestoIstovara, DateTime DatumIstovara, string Referent, int ReferentID, string Izjava, string Napomena)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = tran;
                            cmd.CommandText = "InsertFaktura";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumDok", SqlDbType.DateTime) { Value = DatumDok });
                            cmd.Parameters.Add(new SqlParameter("@Primalac", SqlDbType.Int) { Value = Primalac });
                            cmd.Parameters.Add(new SqlParameter("@Ulica", SqlDbType.NVarChar, 100) { Value = Ulica });
                            cmd.Parameters.Add(new SqlParameter("@Naziv", SqlDbType.NVarChar, 100) { Value = Naziv });
                            cmd.Parameters.Add(new SqlParameter("@Mesto", SqlDbType.NVarChar, 100) { Value = Mesto });
                            cmd.Parameters.Add(new SqlParameter("@MB", SqlDbType.NVarChar, 35) { Value = MB });
                            cmd.Parameters.Add(new SqlParameter("@Valuta", SqlDbType.Char, 3) { Value = Valuta });
                            cmd.Parameters.Add(new SqlParameter("@Kurs", SqlDbType.Decimal) { Value = Kurs });
                            cmd.Parameters.Add(new SqlParameter("@DatumPDV", SqlDbType.DateTime) { Value = DatumPDV });
                            cmd.Parameters.Add(new SqlParameter("@DatumValute", SqlDbType.DateTime) { Value = DatumValute });
                            cmd.Parameters.Add(new SqlParameter("@MestoUtovara", SqlDbType.NVarChar, 35) { Value = MestoUtovara });
                            cmd.Parameters.Add(new SqlParameter("@DatumUtovara", SqlDbType.DateTime) { Value = DatumUtovara });
                            cmd.Parameters.Add(new SqlParameter("@MestoIstovara", SqlDbType.NVarChar, 35) { Value = MestoIstovara });
                            cmd.Parameters.Add(new SqlParameter("@DatumIstovara", SqlDbType.DateTime) { Value = DatumIstovara });
                            cmd.Parameters.Add(new SqlParameter("@Referent", SqlDbType.NVarChar, 35) { Value = Referent });
                            cmd.Parameters.Add(new SqlParameter("@ReferentID", SqlDbType.Int) { Value = ReferentID });
                            cmd.Parameters.Add(new SqlParameter("@Izjava", SqlDbType.NVarChar, 500) { Value = Izjava });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });

                            cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspesan upis", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                conn.Close();
            }
        }
        public void InsFakturaPostav(string Referent, int Faktura, int RB, int MP, string MPNaziv, string JM, decimal Kolicina, decimal Cena, int NosilacTroska, int Najava)
        {
            using (SqlConnection conn = new SqlConnection(connect))
            {
                conn.Open();
                using(SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using(SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = "InsertFakturaPostav";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@Referent", SqlDbType.NVarChar, 35) { Value = Referent });
                            cmd.Parameters.Add(new SqlParameter("@Faktura", SqlDbType.Int) { Value = Faktura });
                            cmd.Parameters.Add(new SqlParameter("@RB", SqlDbType.Int) { Value = RB });
                            cmd.Parameters.Add(new SqlParameter("@MP", SqlDbType.Int) { Value = MP });
                            cmd.Parameters.Add(new SqlParameter("@MPNaziv", SqlDbType.NVarChar, 100) { Value = MPNaziv });
                            cmd.Parameters.Add(new SqlParameter("@JM", SqlDbType.Char, 10) { Value = JM });
                            cmd.Parameters.Add(new SqlParameter("@Kolicina", SqlDbType.Decimal) { Value = Kolicina });
                            cmd.Parameters.Add(new SqlParameter("@Cena", SqlDbType.Decimal) { Value = Cena });
                            cmd.Parameters.Add(new SqlParameter("@NosilacTroska", SqlDbType.Int) { Value = NosilacTroska });
                            cmd.Parameters.Add(new SqlParameter("@NajavaID", SqlDbType.Int) { Value = Najava });

                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspesan upis", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                conn.Close();
            }
        }
    }
}