using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Saobracaj.Izvoz
{
    /// <summary>
    /// Parametrizovani upiti i provere za formu Vaganje (zamena za spajanje teksta u SQL-u).
    /// Originalni Vaganje.cs je ostao netaknut; ispod su zamene za delove koji se menjaju.
    /// </summary>
    public static class VaganjeDb
    {
        private static string Conn { get { return Saobracaj.Sifarnici.frmLogovanje.connectionString; } }

        /// <summary>Zamena za: "Select ID From Vaganje Where IzvozKonacnaID='" + txtKontID.Text + "'"</summary>
        public static bool VecVagan(int izvozKonacnaId)
        {
            using (var con = new SqlConnection(Conn))
            using (var cmd = new SqlCommand("SELECT TOP 1 1 FROM Vaganje WHERE IzvozKonacnaID = @id", con))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = izvozKonacnaId;
                con.Open();
                return cmd.ExecuteScalar() != null;
            }
        }

        /// <summary>Zamena za VratiVrstuKontejnera (spajanje teksta). Vraca null ako kontejner ne postoji.</summary>
        public static DataRow VratiKontejner(int izvozKonacnaId)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(Conn))
            using (var da = new SqlDataAdapter("SELECT VrstaKontejnera, BrojKontejnera, Vozilo FROM IzvozKonacna WHERE ID = @id", con))
            {
                da.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = izvozKonacnaId;
                da.Fill(dt);
            }
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        /// <summary>Zamena za VratiPodatkeZaLog: parametrizovano, jedan red po kontejneru, greska se belezi (ne guta).</summary>
        public static DataTable VratiPodatkeZaLog(int izvozKonacnaId)
        {
            const string sql = @"
SELECT i.ID, i.Scenario,
       LTRIM(RTRIM(mu.Naziv)) AS MestoSpustanjaPunogKontejnera,
       (SELECT TOP 1 vg.DatumMerenja FROM Vaganje vg WHERE vg.IzvozKonacnaID = i.ID ORDER BY vg.ID DESC) AS DatumMerenja
FROM IzvozKonacna i
LEFT JOIN MestaUtovara mu ON i.MestoPreuzimanja2 = mu.ID
WHERE i.ID = @id";
            var dt = new DataTable();
            try
            {
                using (var con = new SqlConnection(Conn))
                using (var da = new SqlDataAdapter(sql, con))
                {
                    da.SelectCommand.Parameters.Add("@id", SqlDbType.Int).Value = izvozKonacnaId;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Vaganje log (kontejner {0}): {1}", izvozKonacnaId, ex);
            }
            return dt;
        }

        /// <summary>Da li je usluga vaganja (IDManipulacijaJed = 102) vec zavrsena. Isto kao PoveriDaLiJeUradjena.</summary>
        public static bool JeZavrsenoVaganje(int izvozKonacnaId)
        {
            using (var con = new SqlConnection(Conn))
            using (var cmd = new SqlCommand(
                "SELECT TOP 1 ISNULL(Uradjen,0) FROM RadniNalogInterni WHERE BrojOsnov = @id AND IDManipulacijaJed = 102 ORDER BY ID DESC", con))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = izvozKonacnaId;
                con.Open();
                object r = cmd.ExecuteScalar();
                return r != null && r != DBNull.Value && Convert.ToInt32(r) == 1;
            }
        }

        /// <summary>Racuna neto u trenutku snimanja i proverava ispravnost. Vraca poruku greske ili null.</summary>
        public static string ProveriMerenje(decimal bruto, decimal tara, out decimal neto)
        {
            neto = bruto - tara;
            if (bruto <= 0) return "Unesite bruto težinu.";
            if (tara < 0) return "Tara ne može biti negativna.";
            if (neto <= 0) return "Neto mora biti veći od nule (bruto mora biti veći od tare).";
            return null;
        }
    }
}

/* ---------------------------------------------------------------------------------------------
   KAKO UKLOPITI U Vaganje.cs (mala izmena, redom):

   1) button1_Click / button21_Click, na pocetku, umesto direktnog SqlDataReader upita:
        int kontId;
        if (!int.TryParse(txtKontID.Text, out kontId)) { MessageBox.Show("Nije izabran kontejner."); return; }
        if (VaganjeDb.JeZavrsenoVaganje(kontId)) { MessageBox.Show("Vaganje je već završeno."); return; }
        decimal neto;
        string g = VaganjeDb.ProveriMerenje(txtBruto.Value, txtTara.Value, out neto);
        if (g != null) { MessageBox.Show(g); return; }
        txtNeto.Value = neto;                       // uvek preracunato pri snimanju
        // u button1_Click jos: if (VaganjeDb.VecVagan(kontId)) { MessageBox.Show("Kontejner je već vagan!"); return; }

   2) VratiVrstuKontejnera(string KontID):
        var r = VaganjeDb.VratiKontejner(int.Parse(KontID));
        if (r != null) { cbovrstakontejnera.SelectedValue = Convert.ToInt32(r["VrstaKontejnera"]);
                         txtKontejner.Text = r["BrojKontejnera"].ToString(); txtVozilo.Text = r["Vozilo"].ToString(); }

   3) VratiPodatkeZaLog(int? id)  ->  return VaganjeDb.VratiPodatkeZaLog(id.Value);

   4) Tip (Cirada/Platforma): umesto chkCirada.Checked procitati IzvozKonacna.Cirada za taj kontejner.

   BAZA (da se spreci dvostruko vaganje istog kontejnera):
     CREATE UNIQUE INDEX UX_Vaganje_IzvozKonacna ON dbo.Vaganje (IzvozKonacnaID);   -- ako vazi "jedno vaganje po kontejneru";
     proveriti prvo duplikate:  SELECT IzvozKonacnaID, COUNT(*) FROM Vaganje GROUP BY IzvozKonacnaID HAVING COUNT(*) > 1;
   --------------------------------------------------------------------------------------------- */
