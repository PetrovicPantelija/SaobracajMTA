using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Izvoz;
using Saobracaj.Skladista;
using Saobracaj.Skladista_main.Dokumenta;
using Saobracaj.Uvoz;
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

namespace Saobracaj.Skladista_main
{
   
    public partial class frmVozac: Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int IdNaloga = 0;
        public frmVozac(int idNaloga)
        {
            InitializeComponent();
            IdNaloga = idNaloga;
        }

        private void FillCombo()
        {
            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // Pomoćna funkcija za brzo punjenje preko jedne konekcije
                void NapuniCombo(ComboBox cbo, string sqlQuery, string displayMember, string valueMember)
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            cbo.DataSource = dt;
                            cbo.DisplayMember = displayMember;
                            cbo.ValueMember = valueMember;
                        }
                    }
                }

              
                NapuniCombo(cboVrstaKamionaPrijem, "SELECT ID, LTRIM(RTRIM(Naziv)) as Naziv  FROM VrstaVozila ORDER BY ID ASC", "Naziv", "ID"); 


            }
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            //tipTransporta, vrstaKamionaPrijem,
            //                  voziloPrijem, vozacPrijem, lKPrijem, telefonPrijem,
            int? vrstaKamionaPrijem = null;
            if (int.TryParse(cboVrstaKamionaPrijem.SelectedValue.ToString(), out int vrstaKamionaPrijemInt))
            {
                vrstaKamionaPrijem = vrstaKamionaPrijemInt;
            }

            string voziloPrijem = string.IsNullOrWhiteSpace(txtVoziloPrijem.Text) ? null : txtVoziloPrijem.Text.Trim();
            string vozacPrijem = string.IsNullOrWhiteSpace(txtVozacPrijem.Text) ? null : txtVozacPrijem.Text.Trim();
            string lKPrijem = string.IsNullOrWhiteSpace(txtLKPrijem.Text) ? null : txtLKPrijem.Text.Trim();
            string telefonPrijem = string.IsNullOrWhiteSpace(txtTelefonPrijem.Text) ? null : txtTelefonPrijem.Text.Trim();

            InsertCarinskoSkladiste insc = new InsertCarinskoSkladiste();
            insc.UpdateRadniNalogDirektniVozilo(IdNaloga, vrstaKamionaPrijem, voziloPrijem, vozacPrijem, lKPrijem, telefonPrijem);

            MessageBox.Show("Podaci su uspešno snimljeni! ");
        }

        private void frmVozac_Load(object sender, EventArgs e)
        {
            FillCombo();
            VratiPodatke(IdNaloga);
        }


        private void VratiPodatke(int id)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"SELECT rns.ID
                                  ,rns.VrstaPrevoznogSredstvaPrijem
                                  ,rns.VrstaKamionaPrijem
                                  ,rns.VoziloPrijem
                                  ,rns.VozacPrijem
                                  ,rns.BrojLKPrijem
                                  ,rns.BrojTelefonaPrijem
                                FROM RadniNalogSkladista rns
                                Where rns.ID=" + id, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                if (cboVrstaKamionaPrijem != null && dr["VrstaKamionaPrijem"] != DBNull.Value)
                    cboVrstaKamionaPrijem.SelectedValue = Convert.ToInt32(dr["VrstaKamionaPrijem"].ToString());
                txtVoziloPrijem.Text = dr["VoziloPrijem"].ToString();
                txtVozacPrijem.Text = dr["VozacPrijem"].ToString();
                txtLKPrijem.Text = dr["BrojLKPrijem"].ToString();
                txtTelefonPrijem.Text = dr["BrojTelefonaPrijem"].ToString();
    


            }
            conn.Close();
        }
    }
}
