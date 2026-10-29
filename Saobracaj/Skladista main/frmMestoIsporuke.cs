using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Skladista;
using Saobracaj.Skladista_main.Dokumenta;
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
    public partial class frmMestoIsporuke: Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int IdNaloga = 0;
        public frmMestoIsporuke(int idNaloga)
        {
            InitializeComponent();
            IdNaloga = idNaloga;
        }

        private void cboMestoIstovaraPrijem_Leave(object sender, EventArgs e)
        {
            PopuniAdresu(cboMestoIstovaraPrijem, cboAdresaPrijem);
            PopuniKontaktOsobu(cboMestoIstovaraPrijem, cboKontaktOsobaPrijem);
        }

        private void PopuniAdresu(ComboBox cboIzvor, ComboBox cboCilj)
        {
            // Provera da li je vrednost validan broj (da izbegnemo grešku pri konverziji)
            if (cboIzvor.SelectedValue == null || !int.TryParse(cboIzvor.SelectedValue.ToString(), out int sifra))
            {
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {
                    // Upit sa parametrom @Sifra
                    string sql = "SELECT PaKoZapSt, LTRIM(Rtrim(PaKOOpomba)) as Naziv FROM partnerjiKontOsebaMU WHERE PaKOSifra = @Sifra ORDER BY PaKOIme";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Sifra", sifra);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        cboCilj.DataSource = dt;
                        cboCilj.DisplayMember = "Naziv";
                        cboCilj.ValueMember = "PaKoZapSt";
                        cboCilj.SelectedIndex = 0;
                    }
                    else
                    {
                        // Ako nema rezultata null
                        cboCilj.DataSource = null;
                        cboCilj.Items.Clear();
                        cboCilj.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        private void PopuniKontaktOsobu(ComboBox cboMesto, ComboBox cboKontakt)
        {
            // Provera selekcije
            if (cboMesto.SelectedValue == null || cboMesto.SelectedValue == DBNull.Value)
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connection))
                {

                    string sql = @"SELECT PaKoZapSt, 
                           (Rtrim(PaKOIme) + ' ' + Rtrim(PaKoPriimek)) as Naziv 
                           FROM partnerjiKontOsebaMU 
                           WHERE PaKOSifra = @Sifra 
                           ORDER BY PaKOIme";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Sifra", Convert.ToInt32(cboMesto.SelectedValue));

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            cboKontakt.DataSource = dt;
                            cboKontakt.DisplayMember = "Naziv";
                            cboKontakt.ValueMember = "PaKoZapSt";
                            cboKontakt.SelectedIndex = 0;
                        }
                        else
                        {
                            // Ako nema rezultata null
                            cboKontakt.DataSource = null;
                            cboKontakt.Items.Clear();
                            cboKontakt.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška kod kontakta: " + ex.Message);
            }
        }

        private void VratiPodatke(int id)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"SELECT rns.ID
                                  ,rns.MestoIstovaraPrijem
                                  ,rns.AdresaIntPrijem
                                  ,rns.KontaktOsobaIstovarIntPrijem
                                  ,rns.PlaniraniDatumPrijem
                                  ,rns.PlaniraniDatum2Prijem

                                FROM RadniNalogSkladista rns
                                       LEFT JOIN RadniNalogInterni rni on rns.ID = rni.BrojRN AND rni.IDManipulacijaJED = 74
                                       LEFT JOIN RadniNalogInterniPotvrda rnp ON  rnp.IDNaloga = rni.ID
                                       LEFT JOIN VlasniciRobe vlr ON  vlr.id = rns.VlasnikRobe
                                Where rns.ID=" + id, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

               
                if (cboMestoIstovaraPrijem != null && dr["MestoIstovaraPrijem"] != DBNull.Value)
                {
                    cboMestoIstovaraPrijem.SelectedValue = Convert.ToInt32(dr["MestoIstovaraPrijem"].ToString());
                    PopuniAdresu(cboMestoIstovaraPrijem, cboAdresaPrijem);
                    if (dr["AdresaIntPrijem"] != DBNull.Value)
                        cboAdresaPrijem.SelectedValue = Convert.ToInt32(dr["AdresaIntPrijem"].ToString());
                    PopuniKontaktOsobu(cboMestoIstovaraPrijem, cboKontaktOsobaPrijem);
                    if (dr["KontaktOsobaIstovarIntPrijem"] != DBNull.Value)
                        cboKontaktOsobaPrijem.SelectedValue = Convert.ToInt32(dr["KontaktOsobaIstovarIntPrijem"].ToString());

                }
            }
            conn.Close();
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

               

               
                // 9. Mesta utovara
                NapuniCombo(cboMestoIstovaraPrijem, "SELECT ID, Naziv FROM MestaUtovara ORDER BY Naziv", "Naziv", "ID");
                cboMestoIstovaraPrijem.SelectedValue = 2;


            }
        }

        private void frmMestoIsporuke_Load(object sender, EventArgs e)
        {
            FillCombo();
            VratiPodatke(IdNaloga);
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            int? mestoIstovara = null;

            if (int.TryParse(cboMestoIstovaraPrijem.SelectedValue.ToString(), out int mestoIstovaraInt))
            {

                mestoIstovara = mestoIstovaraInt;
            }
            int? adresaPrijem = null;
            if (cboAdresaPrijem.SelectedValue != null)
            {
                adresaPrijem = Convert.ToInt32(cboAdresaPrijem.SelectedValue);
            }
            int? kontaktOsobaPrijem = null;
            if (cboKontaktOsobaPrijem.SelectedValue != null)
            {
                kontaktOsobaPrijem = Convert.ToInt32(cboKontaktOsobaPrijem.SelectedValue);
            }

            InsertCarinskoSkladiste insc = new InsertCarinskoSkladiste();
            insc.UpdateRadniNalogDirektniIstovar(IdNaloga, mestoIstovara, adresaPrijem, kontaktOsobaPrijem);

            MessageBox.Show("Podaci su uspešno snimljeni! ");
        }
    }
    

    
}
