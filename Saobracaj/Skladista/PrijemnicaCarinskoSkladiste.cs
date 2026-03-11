using Microsoft.ReportingServices.Diagnostics.Internal;
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

namespace Saobracaj.Skladista
{
    public partial class PrijemnicaCarinskoSkladiste : Form
    {
        string Tip;
        int RN;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string tKorisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        public PrijemnicaCarinskoSkladiste(string tip,int rn)
        {
            InitializeComponent();
            Tip = tip;
            RN = rn;

            FillCombo();
            VratiPodatke(RN);
            FillCkList(txtID.Text);
        }
        int MagacinskiBroj;
        string tipMB;
        private void VratiPodatke(int id)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"SELECT ID,TipRN,CarinskoSkladiste,MagacinskiBroj,TipMB,Nalogodavac,VlasnikRobe,VrstaRobe,NacinPakovanja,OstalaSkladista,VrednostRobe,Valuta,PIB,
VrstaPrevoznogSredstva,VrstaKamiona,Vozilo,Vozac,BrojLK,BrojTelefona,OdredisnaCarinarnica,Spediter,KontakOsobaSpeditera,MestoIstovara,Adresa,KontaktOsobaIstovar,
PlaniraniDatum,PlaniraniDatum2,PosebniUslovi,DodatneUslugeID,Napomena,Aktivan,Formiran
From RNCarinskoSkladiste
Where ID=" + id, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                Tip = dr["TipRN"].ToString();
                textBox1.Text = dr["CarinskoSkladiste"].ToString();
                MagacinskiBroj = Convert.ToInt32(dr["MagacinskiBroj"].ToString());
                tipMB = dr["TipMB"].ToString();
                cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Nalogodavac"].ToString());
                cboVlasnikRobe.SelectedValue = Convert.ToInt32(dr["VlasnikRobe"].ToString());
                txtVrstaRobe.Text = dr["VrstaRobe"].ToString();
                txtNacinPakovanja.Text = dr["NacinPakovanja"].ToString();
                cboADR.SelectedValue = Convert.ToInt32(dr["OstalaSkladista"].ToString());
                txtVrednostRobe.Text = dr["VrednostRobe"].ToString();
                cboValuta.SelectedValue = dr["Valuta"].ToString();
                txtPIB.Text = dr["PIB"].ToString();
                cboTipTransporta.SelectedValue = Convert.ToInt32(dr["VrstaPrevoznogSredstva"].ToString());
                cboVrstaKamiona.SelectedValue = Convert.ToInt32(dr["VrstaKamiona"].ToString());
                txtVozilo.Text = dr["Vozilo"].ToString();
                txtVozac.Text = dr["Vozac"].ToString();
                txtLK.Text = dr["BrojLk"].ToString();
                txtTelefon.Text = dr["BrojTelefona"].ToString();
                cboCarinarnica.SelectedValue = Convert.ToInt32(dr["OdredisnaCarinarnica"].ToString());
                cboSpediter.SelectedValue = Convert.ToInt32(dr["Spediter"].ToString());
                txtKontakOsobaSpediter.Text = dr["KontakOsobaSpeditera"].ToString();
                cboMestoIstovara.SelectedValue = Convert.ToInt32(dr["MestoIstovara"].ToString());
                txtAdresa.Text = dr["Adresa"].ToString();
                txtKontaktOsoba.Text = dr["KontaktOsobaIstovar"].ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dr["PlaniraniDatum"].ToString());
                dateTimePicker2.Value = Convert.ToDateTime(dr["PlaniraniDatum2"].ToString());
                txtPosebniUslovi.Text = dr["PosebniUslovi"].ToString();
                txtNapomena.Text = dr["Napomena"].ToString();

            }
            conn.Close();

        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var partner5 = "Select ID, Napomena from MagacinskiBrojevi order by ID Desc";
            var partAD5 = new SqlDataAdapter(partner5, conn);
            var partDS5 = new System.Data.DataSet();
            partAD5.Fill(partDS5);
            cboMagacinskiBroj.DataSource = partDS5.Tables[0];
            cboMagacinskiBroj.DisplayMember = "Napomena";
            cboMagacinskiBroj.ValueMember = "ID";

            var nalogodava = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var daNalogodava = new SqlDataAdapter(nalogodava, conn);
            var dsNalogodava = new System.Data.DataSet();
            daNalogodava.Fill(dsNalogodava);
            cboNalogodavac.DataSource = dsNalogodava.Tables[0];
            cboNalogodavac.DisplayMember = "PaNaziv";
            cboNalogodavac.ValueMember = "PaSifra";

            var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new System.Data.DataSet();
            partAD.Fill(partDS);
            cboVlasnikRobe.DataSource = partDS.Tables[0];
            cboVlasnikRobe.DisplayMember = "PaNaziv";
            cboVlasnikRobe.ValueMember = "PaSifra";


            var spediter = "Select PaSifra,PaNaziv From Partnerji Where Spediter=1 order by PaSifra desc";
            var daSpediter = new SqlDataAdapter(spediter, conn);
            var dsSpediter = new System.Data.DataSet();
            daSpediter.Fill(dsSpediter);
            cboSpediter.DataSource = dsSpediter.Tables[0];
            cboSpediter.DisplayMember = "PaNaziv";
            cboSpediter.ValueMember = "PaSifra";

            var valuta = "Select VaSifra,VaNaziv From Valute Order by VaSifra asc";
            var daValuta = new SqlDataAdapter(valuta, conn);
            var dsValuta = new System.Data.DataSet();
            daValuta.Fill(dsValuta);
            cboValuta.DataSource = dsValuta.Tables[0];
            cboValuta.DisplayMember = "VaNaziv";
            cboValuta.ValueMember = "VaSifra";

            var vrstaVozila = "Select ID,Naziv From VrstePrevoznogSredstva order by ID asc";
            var daVrstaVozila = new SqlDataAdapter(vrstaVozila, conn);
            var dsVrstaVozila = new System.Data.DataSet();
            daVrstaVozila.Fill(dsVrstaVozila);
            cboTipTransporta.DataSource = dsVrstaVozila.Tables[0];
            cboTipTransporta.DisplayMember = "Naziv";
            cboTipTransporta.ValueMember = "ID";


            var vrstaKamiona = "Select ID,Naziv From VrstaVozila order by ID asc";
            var daVrstaKamiona = new SqlDataAdapter(vrstaKamiona, conn);
            var dsVrstaKamiona = new System.Data.DataSet();
            daVrstaKamiona.Fill(dsVrstaKamiona);
            cboVrstaKamiona.DataSource = dsVrstaKamiona.Tables[0];
            cboVrstaKamiona.DisplayMember = "Naziv";
            cboVrstaKamiona.ValueMember = "ID";

            var carinarnica = "Select ID,Naziv From Carinarnice order by ID asc";
            var daCarinarnica = new SqlDataAdapter(carinarnica, conn);
            var dsCarinarnica = new System.Data.DataSet();
            daCarinarnica.Fill(dsCarinarnica);
            cboCarinarnica.DataSource = dsCarinarnica.Tables[0];
            cboCarinarnica.DisplayMember = "Naziv";
            cboCarinarnica.ValueMember = "ID";

            var mu = "select ID, Naziv from MestaUtovara order by Naziv";
            var muAD = new SqlDataAdapter(mu, conn);
            var muDS = new System.Data.DataSet();
            muAD.Fill(muDS);
            cboMestoIstovara.DataSource = muDS.Tables[0];
            cboMestoIstovara.DisplayMember = "Naziv";
            cboMestoIstovara.ValueMember = "ID";

            var nhm = "select ID,RTrim(Broj)+'-'+RTRIM(Naziv) as Naziv From NHM order by ID asc";
            var nhmAD = new SqlDataAdapter(nhm, conn);
            var nhmDS = new System.Data.DataSet();
            nhmAD.Fill(nhmDS);
            cboArtikal.DataSource = nhmDS.Tables[0];
            cboArtikal.DisplayMember = "Naziv";
            cboArtikal.ValueMember = "ID";

            var jm = "Select MeSifra From MerskeEnote order by MeSifra asc";
            var jmDA = new SqlDataAdapter(jm, conn);
            var jmDS = new System.Data.DataSet();
            jmDA.Fill(jmDS);
            cboJM.DataSource = jmDS.Tables[0];
            cboJM.DisplayMember = "MeSifra";
            cboJM.ValueMember = "MeSifra";

            var valutaS = "Select VaSifra,VaNaziv From Valute Order by VaSifra asc";
            var daValutaS = new SqlDataAdapter(valutaS, conn);
            var dsValutaS = new System.Data.DataSet();
            daValutaS.Fill(dsValutaS);
            cboValutaStavka.DataSource = dsValutaS.Tables[0];
            cboValutaStavka.DisplayMember = "VaNaziv";
            cboValutaStavka.ValueMember = "VaSifra";

            var paleta = "Select ID,Naziv From TipPalete order by ID asc";
            var daPaleta = new SqlDataAdapter(paleta, conn);
            var dsPaleta = new System.Data.DataSet();
            daPaleta.Fill(dsPaleta);
            cboVrstaPaleta.DataSource = dsPaleta.Tables[0];
            cboVrstaPaleta.DisplayMember = "Naziv";
            cboVrstaPaleta.ValueMember = "ID";

        }
        string pomUsluge;
        private void FillCkList(string rn)
        {
            checkedListBox1.DataSource = null;
            checkedListBox1.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                // 1. Učitaj sve usluge
                string querySve = "SELECT ID, RTRIM(Naziv) AS Naziv FROM VrstaManipulacije ORDER BY ID ASC";

                DataTable dt = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter(querySve, conn))
                {
                    da.Fill(dt);
                }

                checkedListBox1.DataSource = dt;
                checkedListBox1.DisplayMember = "Naziv";
                checkedListBox1.ValueMember = "ID";

                // 2. Učitaj izabrane usluge za RN
                string queryIzabrane = "SELECT Usluga FROM RNCarinskoSkladisteDodatneUsluge WHERE RN = @RN";

                HashSet<int> izabraneUsluge = new HashSet<int>();

                using (SqlCommand cmd = new SqlCommand(queryIzabrane, conn))
                {
                    cmd.Parameters.AddWithValue("@RN", rn);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            izabraneUsluge.Add(Convert.ToInt32(dr["Usluga"]));
                        }
                    }
                }

                // 3. Čekiranje stavki
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    DataRowView row = (DataRowView)checkedListBox1.Items[i];
                    int id = Convert.ToInt32(row["ID"]);

                    checkedListBox1.SetItemChecked(i, izabraneUsluge.Contains(id));
                }
            }
        }

        private void cboMagacinskiBroj_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MagacinskiBroj = Convert.ToInt32(cboMagacinskiBroj.SelectedValue);
            if (MagacinskiBroj != 0)
            {
                var conn = new SqlConnection(connection);
                conn.Open();
                var cmd = new SqlCommand("Select Tip From MagacinskiBrojevi Where ID=" + Convert.ToInt32(cboMagacinskiBroj.SelectedValue), conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tipMB = dr[0].ToString();
                }
                conn.Close();
            }
            else
            {
                tipMB = "";
            }

        }
        public class PrijemnicaStavke
        {
            public int RB { get; set; }
            public int NHM { get; set; }
            public string Naziv { get; set; }
            public string JM { get; set; }
            public decimal Koleta { get; set; }
            public decimal Bruto { get; set; }
            public decimal Vrednost { get; set; }
            public string Valuta { get; set; }
            public string Pozicija { get; set; }
            public int Paleta { get; set; }
            public int VrstaPaleta { get; set; }
            public string PaletaOpis { get; set; }
            public string DimenzijePaleta { get; set; }
            public decimal PDV { get; set; }
            public decimal Carina { get; set; }
        }
        private List<PrijemnicaStavke> list = new List<PrijemnicaStavke>();
        private int rb = 1;

        private void OsveziGrid()
        {
            gridGroupingControl1.DataSource = null;
            gridGroupingControl1.DataSource = list.ToList();
            gridGroupingControl1.Refresh();
        }
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                var stavka = new PrijemnicaStavke
                {
                    RB = rb,
                    NHM = Convert.ToInt32(cboArtikal.SelectedValue.ToString()),
                    Naziv = cboArtikal.Text.ToString(),
                    JM = cboJM.SelectedValue.ToString(),
                    Koleta = Convert.ToDecimal(txtKoleta.Text),
                    Bruto = Convert.ToDecimal(txtBruto.Text),
                    Vrednost = Convert.ToDecimal(txtVrednost.Text),
                    Valuta = cboValutaStavka.SelectedValue.ToString(),
                    Pozicija = cboPozicija.Text,
                    Paleta = Convert.ToInt32(txtPaleta.Text),
                    VrstaPaleta = Convert.ToInt32(cboVrstaPaleta.SelectedValue),
                    PaletaOpis = cboVrstaPaleta.Text.ToString(),
                    DimenzijePaleta = "",
                    PDV = Convert.ToDecimal(txtPDV.Text),
                    Carina = Convert.ToDecimal(txtCarina.Text)
                };

                list.Add(stavka);
                rb++;
                OsveziGrid();
            }
            catch { }
        }
    }
}
