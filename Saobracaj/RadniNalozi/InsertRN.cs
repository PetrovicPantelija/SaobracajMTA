using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.RadniNalozi
{
    internal class InsertRN
    {
        public string connect = Sifarnici.frmLogovanje.connectionString;

        public void InsRnMedjuskladisni(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "InsertRNMedjuskladisni";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSkla", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter(@"NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRnMedjuskladisni(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "UpdateRNMedjuskladisni";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSkla", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter(@"NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRnMedjuskladisni(int ID)
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
                            cmd.CommandText = "DeleteRNMedjuskladisni";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRnOtpremaCirade(DateTime DatumRasporeda, string BrojKontejnera, int NazivBrodara, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo, int CarinskiPostupak,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "InsertRNOtpremaCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRnOtpremaCirade(int ID, DateTime DatumRasporeda, string BrojKontejnera, int NazivBrodara, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo, int CarinskiPostupak,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "UpdateRNOtpremaCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRnOtpremaCirade(int ID)
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
                            cmd.CommandText = "DeleteRNOtpremaCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRNOtpremaPlatforme(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int Uvoznik, int CarinskiPostupak, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao)
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
                            cmd.CommandText = "InsertRNOtpremaPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRNOtpremaPlatforme(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int Uvoznik, int CarinskiPostupak, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao)
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
                            cmd.CommandText = "UpdateRNOtpremaPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRNOtpremaPlatforme(int ID)
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
                            cmd.CommandText = "DeleteRNOtpremaPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRNOtpremaPlatforme2(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "InsertRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRNOtpremaPlatforme2(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "UpdateRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRNOtpremaPlatforme2(int ID)
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
                            cmd.CommandText = "DeleteRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRNOtpremaVoza(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            string BrojPlombe, string BrojVagona,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "InsertRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@BrojVagona", SqlDbType.NVarChar, 50) { Value = BrojVagona });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRNOtpremaVoza(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NaVoznoSredstvo,
            string BrojPlombe, string BrojVagona,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "UpdateRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NaVoznoSredstvo", SqlDbType.Int) { Value = NaVoznoSredstvo });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@BrojVagona", SqlDbType.NVarChar, 50) { Value = BrojVagona });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRNOtpremaVoza(int ID)
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
                            cmd.CommandText = "DeleteRNOtpremaPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRNPregledIpostavkaKontejnera(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "InsertRNPregledIpostavkaKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRNPregledIpostavkaKontejnera(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije,
            int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "UpdateRNPregledIpostavkaKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRNPregledIpostavkaKontejnera(int ID)
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
                            cmd.CommandText = "DeleteRNPregledIpostavkaKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRNPregledIspraznjenogKontejnera(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NazivBrodara,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "InsertRNPregledIspraznjenogKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRNPregledIspraznjenogKontejnera(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int NazivBrodara,
            int VrstaRobe, int SaSkladista, int SaPozicijeSklad, int NaSkladiste, int NaPoziciju, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "UpdateRNPregledIspraznjenogKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPoziciju", SqlDbType.Int) { Value = NaPoziciju });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRNPregledIspraznjenogKontejnera(int ID)
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
                            cmd.CommandText = "DeleteRNPregledIspraznjenogKontejnera";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRNPPrijemCirade(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva,
            string BrojPlombe, int CarinskiPostupak, int InspekcijskiPregled, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad,
            int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "InsertRNPPrijemCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRNPPrijemCirade(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva,
            string BrojPlombe, int CarinskiPostupak, int InspekcijskiPregled, int SpedicijaRTC, int NazivBrodara, int VrstaRobe, int SaSkladista, int SaPozicijeSklad,
            int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "UpdateRNPPrijemCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@SpedicijaRTC", SqlDbType.Int) { Value = SpedicijaRTC });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@SaSkladista", SqlDbType.Int) { Value = SaSkladista });
                            cmd.Parameters.Add(new SqlParameter("@SaPozicijeSklad", SqlDbType.Int) { Value = SaPozicijeSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRNPPrijemCirade(int ID)
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
                            cmd.CommandText = "DeleteRNPPrijemCirade";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRNPPrijemPlatforme(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            string BrojPlombe, int Izvoznik, int NazivBrodara, int CarinskiPostupak, int InspekcijskiPregled, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge,
            string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "InsertRNPPrijemPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Izvoznik", SqlDbType.Int) { Value = Izvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRNPPrijemPlatforme(int ID, DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            string BrojPlombe, int Izvoznik, int NazivBrodara, int CarinskiPostupak, int InspekcijskiPregled, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge,
            string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "UpdateRNPPrijemPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Izvoznik", SqlDbType.Int) { Value = Izvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRNPPrijemPlatforme(int ID)
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
                            cmd.CommandText = "DeleteRNPPrijemPlatforme";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRNPPrijemPlatforme2(DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "InsertRNPPrijemPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRNPPrijemPlatforme2(int ID, DateTime DatumRasporeda, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojKontejnera, int VrstaKontejnera,
            int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "UpdateRNPPrijemPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRNPPrijemPlatforme2(int ID)
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
                            cmd.CommandText = "DeleteRNPPrijemPlatforme2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRNPPrijemVoza(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "InsertRNPPrijemVoza";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRNPPrijemVoza(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "UpdateRNPPrijemVoza";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRNPPrijemVoza(int ID)
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
                            cmd.CommandText = "DeleteRNPPrijemVoza";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void InsRNPPrijemVoza2(DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int CarinskiPostupak, int InspekcijskiPregled, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "InsertRNPPrijemVoza2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void UpdRNPPrijemVoza2(int ID, DateTime DatumRasporeda, string BrojKontejnera, int VrstaKontejnera, string NalogIzdao, DateTime DatumRealizacije, int SaVoznogSredstva, string BrojPlombe,
            int Uvoznik, int CarinskiPostupak, int InspekcijskiPregled, int NazivBrodara, int VrstaRobe, int NaSkladiste, int NaPozicijuSklad, int IdUsluge, string NalogRealizovao, string Napomena)
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
                            cmd.CommandText = "UpdateRNPPrijemVoza2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                            cmd.Parameters.Add(new SqlParameter("@DatumRasporeda", SqlDbType.DateTime) { Value = DatumRasporeda });
                            cmd.Parameters.Add(new SqlParameter("@VrstaKontejnera", SqlDbType.Int) { Value = VrstaKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NalogIzdao", SqlDbType.NVarChar, 50) { Value = NalogIzdao });
                            cmd.Parameters.Add(new SqlParameter("@DatumRealizacije", SqlDbType.DateTime) { Value = DatumRealizacije });
                            cmd.Parameters.Add(new SqlParameter("@SaVoznogSredstva", SqlDbType.Int) { Value = SaVoznogSredstva });
                            cmd.Parameters.Add(new SqlParameter("@BrojPlombe", SqlDbType.NVarChar, 50) { Value = BrojPlombe });
                            cmd.Parameters.Add(new SqlParameter("@Uvoznik", SqlDbType.Int) { Value = Uvoznik });
                            cmd.Parameters.Add(new SqlParameter("@CarinskiPostupak", SqlDbType.Int) { Value = CarinskiPostupak });
                            cmd.Parameters.Add(new SqlParameter("@InspekcijskiPregled", SqlDbType.Int) { Value = InspekcijskiPregled });
                            cmd.Parameters.Add(new SqlParameter("@BrojKontejnera", SqlDbType.NVarChar, 50) { Value = BrojKontejnera });
                            cmd.Parameters.Add(new SqlParameter("@NazivBrodara", SqlDbType.Int) { Value = NazivBrodara });
                            cmd.Parameters.Add(new SqlParameter("@VrstaRobe", SqlDbType.Int) { Value = VrstaRobe });
                            cmd.Parameters.Add(new SqlParameter("@NaSkladiste", SqlDbType.Int) { Value = NaSkladiste });
                            cmd.Parameters.Add(new SqlParameter("@NaPozicijuSklad", SqlDbType.Int) { Value = NaPozicijuSklad });
                            cmd.Parameters.Add(new SqlParameter("@IdUsluge", SqlDbType.Int) { Value = IdUsluge });
                            cmd.Parameters.Add(new SqlParameter("@NalogRealizovao", SqlDbType.NVarChar, 50) { Value = NalogRealizovao });
                            cmd.Parameters.Add(new SqlParameter("@Napomena", SqlDbType.NVarChar, 500) { Value = Napomena });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void DelRNPPrijemVoza2(int ID)
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
                            cmd.CommandText = "DeleteRNPPrijemVoza2";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
                        }
                    }
                    catch (SqlException)
                    {
                        tran.Rollback();
                        MessageBox.Show("Neuspešan upis cena u bazu", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}