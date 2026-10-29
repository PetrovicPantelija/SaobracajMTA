using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Saobracaj.HR
{
    // Pristup bazi za HR modul: upiti sa parametrima, procedure sa izlaznim parametrima.
    // Poruke iz procedura (THROW 50001..) su na srpskom i prikazuju se korisniku kakve jesu.
    public static class HRBaza
    {
        public static string Konekcija => Sifarnici.frmLogovanje.connectionString;

        public static string Korisnik
        {
            get
            {
                string k = Sifarnici.frmLogovanje.user ?? "";
                k = k.Trim();
                return k.Length > 20 ? k.Substring(0, 20) : k;
            }
        }

        // Otvara konekciju i postavlja korisnika aplikacije u SESSION_CONTEXT (okidači ga upisuju u HRLog)
        public static void Otvori(SqlConnection con)
        {
            con.Open();
            try
            {
                using (var cmd = new SqlCommand("EXEC sp_set_session_context N'Korisnik', @k", con))
                {
                    cmd.Parameters.AddWithValue("@k", Korisnik);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                // starija verzija SQL Servera: log ostaje bez korisnika
            }
        }

        public static DataTable Tabela(string upit, params SqlParameter[] parametri)
        {
            using (var con = new SqlConnection(Konekcija))
            using (var cmd = new SqlCommand(upit, con))
            {
                cmd.Parameters.AddRange(parametri);
                var dt = new DataTable();
                Otvori(con);
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dt);
                return dt;
            }
        }

        public static object Skalar(string upit, params SqlParameter[] parametri)
        {
            using (var con = new SqlConnection(Konekcija))
            using (var cmd = new SqlCommand(upit, con))
            {
                cmd.Parameters.AddRange(parametri);
                Otvori(con);
                object v = cmd.ExecuteScalar();
                return v == DBNull.Value ? null : v;
            }
        }

        public static int Izvrsi(string upit, params SqlParameter[] parametri)
        {
            using (var con = new SqlConnection(Konekcija))
            using (var cmd = new SqlCommand(upit, con))
            {
                cmd.Parameters.AddRange(parametri);
                Otvori(con);
                return cmd.ExecuteNonQuery();
            }
        }

        // Procedura; izlazni parametri se čitaju iz prosleđenih SqlParameter objekata posle poziva
        public static void Procedura(string naziv, params SqlParameter[] parametri)
        {
            using (var con = new SqlConnection(Konekcija))
            using (var cmd = new SqlCommand(naziv, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parametri);
                Otvori(con);
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable ProceduraTabela(string naziv, params SqlParameter[] parametri)
        {
            using (var con = new SqlConnection(Konekcija))
            using (var cmd = new SqlCommand(naziv, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parametri);
                var dt = new DataTable();
                Otvori(con);
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(dt);
                return dt;
            }
        }

        public static SqlParameter P(string naziv, object vrednost)
        {
            return new SqlParameter(naziv, vrednost ?? DBNull.Value);
        }

        public static SqlParameter Izlaz(string naziv, SqlDbType tip, int velicina = 0)
        {
            var p = new SqlParameter(naziv, tip) { Direction = ParameterDirection.Output };
            if (velicina > 0)
                p.Size = velicina;
            return p;
        }

        // Prazan tekst je NULL u bazi
        public static object Tekst(string s)
        {
            return string.IsNullOrWhiteSpace(s) ? (object)DBNull.Value : s.Trim();
        }

        public static int CeoBroj(object v)
        {
            if (v == null || v == DBNull.Value)
                return 0;
            int.TryParse(Convert.ToString(v, CultureInfo.InvariantCulture), out int n);
            return n;
        }

        public static decimal Decimalni(object v)
        {
            if (v == null || v == DBNull.Value)
                return 0;
            return Convert.ToDecimal(v);
        }

        public static DateTime? Datum(object v)
        {
            if (v == null || v == DBNull.Value)
                return null;
            return Convert.ToDateTime(v);
        }

        public static string DatumTekst(object v)
        {
            DateTime? d = Datum(v);
            return d.HasValue ? d.Value.ToString("dd.MM.yyyy") : "";
        }

        public static string Iznos(decimal v)
        {
            return v.ToString("#,##0.##", CultureInfo.GetCultureInfo("sr-Latn-RS"));
        }

        public static DataRow Red(string upit, params SqlParameter[] parametri)
        {
            DataTable dt = Tabela(upit, parametri);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        // Lista za ComboBox: prva kolona je vrednost, druga tekst
        public static DataTable Lista(string upit, bool praznaStavka)
        {
            DataTable dt = Tabela(upit);
            if (praznaStavka)
            {
                DataRow r = dt.NewRow();
                r[1] = "";
                dt.Rows.InsertAt(r, 0);
            }
            return dt;
        }

        public static Dictionary<string, object> RedUMapu(DataRow red)
        {
            var mapa = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            if (red == null)
                return mapa;
            foreach (DataColumn c in red.Table.Columns)
                mapa[c.ColumnName] = red[c];
            return mapa;
        }
    }
}
