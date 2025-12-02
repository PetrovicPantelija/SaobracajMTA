using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.MainLeget
{
    public partial class GlavniEkran : Form
    {
        public GlavniEkran()
        {
            InitializeComponent();
        }

        private void GlavniEkran_Load(object sender, EventArgs e)
        {
             VratiBrojKontejneraKomercijalnaZona();
        }
        private void VratiBrojKontejneraKomercijalnaZona()
        {
            // Add this method to the form that contains `lblKomercijalnaZona`

            const string sql = "SELECT ISNULL(SUM(Kolicina), 0) FROM KontejnerTekuce WHERE Skladiste = 1";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            using (var conn = new SqlConnection(s_connection))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                var result = cmd.ExecuteScalar();
                decimal kolicina = 0;
                if (result != null && result != DBNull.Value)
                    decimal.TryParse(result.ToString(), out kolicina);

                // format as needed, e.g. no decimals:
                lblKomercijalnaZona.Text = kolicina.ToString("N0");
            }

   
        }

        private void sparkLine1_Click(object sender, EventArgs e)
        {

        }
    }
}
