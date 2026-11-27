using System;
using System.Data.SqlClient;

namespace Saobracaj.Skladista_main
{
    // Prikaz carinskog magacinskog broja kao „broj/godina“ (npr. 23/2026). MagacinskiBrojCarinski.ID ostaje interni
    // ključ (vrednost u listama, vezivanje naloga); broj u godini i godinu dodeljuje baza
    // (Carina_MagacinskiBrojGodina_2026_11.sql, funkcija dbo.fnMagacinskiBrojOznaka). Bez skripte se prikazuje ID, kao ranije.
    internal static class MagacinskiBrojOznaka
    {
        static bool postoji;   // pamti se samo kad funkcija postoji: posle puštanja skripte radi bez ponovnog pokretanja

        public static bool Postoji(string connection)
        {
            if (postoji)
                return true;
            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand("SELECT OBJECT_ID('dbo.fnMagacinskiBrojOznaka')", conn))
                {
                    conn.Open();
                    object o = cmd.ExecuteScalar();
                    postoji = o != null && o != DBNull.Value;
                }
            }
            catch
            {
                return false;
            }
            return postoji;
        }

        // SQL izraz za oznaku magacinskog broja iz kolone (ID carinskog magacinskog broja)
        public static string Izraz(string connection, string kolona)
        {
            return Postoji(connection)
                ? "dbo.fnMagacinskiBrojOznaka(" + kolona + ")"
                : "CONVERT(nvarchar(20), " + kolona + ")";
        }

        // SQL izraz „oznaka - naziv“ (naziv samo kad nije prazan)
        public static string IzrazSaNazivom(string connection, string kolonaID, string kolonaNaziv)
        {
            return Izraz(connection, kolonaID) + " + ISNULL(N' - ' + NULLIF(RTRIM(CONVERT(nvarchar(200), " + kolonaNaziv + ")), N''), N'')";
        }

        // oznaka jednog magacinskog broja; "" za 0 ili null
        public static string Za(string connection, int? id)
        {
            if (!id.HasValue || id.Value <= 0)
                return "";
            if (!Postoji(connection))
                return id.Value.ToString();
            try
            {
                using (var conn = new SqlConnection(connection))
                using (var cmd = new SqlCommand("SELECT dbo.fnMagacinskiBrojOznaka(@ID)", conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id.Value);
                    conn.Open();
                    return Convert.ToString(cmd.ExecuteScalar());
                }
            }
            catch
            {
                return id.Value.ToString();
            }
        }
    }
}
