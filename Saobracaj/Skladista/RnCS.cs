using Saobracaj.Drumski;
using Syncfusion.Windows.Forms.Diagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saobracaj.Skladista
{
    public partial class RnCS : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string tKorisnik = Saobracaj.Sifarnici.frmLogovanje.user;
        string Tip;
        int ID;
        public RnCS(string tip)
        {
            InitializeComponent();
            Tip = tip;
            if (tip == "Carinsko")
            {
                textBox1.Text = "1008";
            }
            FillCkList(txtID.Text);

            FillCombo();
        }
        public RnCS(string tip,int id)
        {
            InitializeComponent();
            Tip = tip;
            ID = id;
            

            FillCombo();
            VratiPodatke(ID);
            FillCkList(txtID.Text);
        }
        private void RnCS_Load(object sender, EventArgs e)
        {
            
        }
        private void VratiPodatke(int id)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"SELECT ID,TipRN,CarinskoSkladiste,MagacinskiBroj,TipMB,Nalogodavac,VlasnikRobe,VrstaRobe,NacinPakovanja,OstalaSkladista,VrednostRobe,Valuta,PIB,
VrstaPrevoznogSredstva,VrstaKamiona,Vozilo,Vozac,BrojLK,BrojTelefona,OdredisnaCarinarnica,Spediter,KontakOsobaSpeditera,MestoIstovara,Adresa,KontaktOsobaIstovar,
PlaniraniDatum,PlaniraniDatum2,PosebniUslovi,DodatneUslugeID,Napomena,Aktivan,Formiran
From RNCarinskoSkladiste
Where ID="+id, conn);
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

                int aktivan = Convert.ToInt32(dr["Aktivan"].ToString());
                if (aktivan == 1)
                {
                    chkAktivan.Checked = true;
                }
                int formiran = Convert.ToInt32(dr["Formiran"].ToString());
                if (formiran == 1)
                {
                    chkFormiran.Checked = true;
                }

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

            var valuta= "Select VaSifra,VaNaziv From Valute Order by VaSifra asc";
            var daValuta = new SqlDataAdapter(valuta, conn);
            var dsValuta = new System.Data.DataSet();
            daValuta.Fill(dsValuta);
            cboValuta.DataSource= dsValuta.Tables[0];
            cboValuta.DisplayMember = "VaNaziv";
            cboValuta.ValueMember = "VaSifra";

            var vrstaVozila= "Select ID,Naziv From VrstePrevoznogSredstva order by ID asc";
            var daVrstaVozila=new SqlDataAdapter(vrstaVozila, conn);
            var dsVrstaVozila = new System.Data.DataSet();
            daVrstaVozila.Fill(dsVrstaVozila);
            cboTipTransporta.DataSource = dsVrstaVozila.Tables[0];
            cboTipTransporta.DisplayMember = "Naziv";
            cboTipTransporta.ValueMember = "ID";


            var vrstaKamiona= "Select ID,Naziv From VrstaVozila order by ID asc";
            var daVrstaKamiona = new SqlDataAdapter(vrstaKamiona, conn);
            var dsVrstaKamiona = new System.Data.DataSet();
            daVrstaKamiona.Fill(dsVrstaKamiona);
            cboVrstaKamiona.DataSource = dsVrstaKamiona.Tables[0];
            cboVrstaKamiona.DisplayMember = "Naziv";
            cboVrstaKamiona.ValueMember = "ID";

            var carinarnica = "Select ID,Naziv From Carinarnice order by ID asc";
            var daCarinarnica = new SqlDataAdapter(carinarnica, conn);
            var dsCarinarnica = new DataSet();
            daCarinarnica.Fill(dsCarinarnica);
            cboCarinarnica.DataSource = dsCarinarnica.Tables[0];
            cboCarinarnica.DisplayMember = "Naziv";
            cboCarinarnica.ValueMember = "ID";

            var mu = "select ID, Naziv from MestaUtovara order by Naziv";
            var muAD = new SqlDataAdapter(mu, conn);
            var muDS = new DataSet();
            muAD.Fill(muDS);
            cboMestoIstovara.DataSource = muDS.Tables[0];
            cboMestoIstovara.DisplayMember = "Naziv";
            cboMestoIstovara.ValueMember = "ID";

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
        int MagacinskiBroj = 0;
        string tipMB = "";
        private void btnSnimi_Click(object sender, EventArgs e)
        {
            InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();
            if (txtID.Text != "")
            {
                try
                {
                    int idUsluge = 0;
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("Select top 1 ID From RNCarinskoSkladisteDodatneUsluge Where RN=" + Convert.ToInt32(txtID.Text), conn))
                        {
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                idUsluge = Convert.ToInt32(dr[0].ToString());
                            }
                        }
                    }
                    ins.DeleteDodatneUsluge(Convert.ToInt32(txtID.Text));

                    foreach (var item in checkedListBox1.CheckedItems)
                    {
                        DataRowView row = item as DataRowView;
                        if (row == null)
                            continue;

                        int valueMember = Convert.ToInt32(row["ID"]);

                        ins.InsertDodatneUsluge(idUsluge, Convert.ToInt32(txtID.Text), valueMember);
                    }

                    int formiran = 0;
                    int aktivan = 0;
                    if (chkFormiran.Checked == true)
                    {
                        formiran = 1;
                    }
                    if (chkAktivan.Checked == true)
                    {
                        aktivan = 1;
                    }

                    ins.UpdateRN(Convert.ToInt32(txtID.Text), Tip, textBox1.Text, MagacinskiBroj, tipMB, Convert.ToInt32(cboNalogodavac.SelectedValue), Convert.ToInt32(cboVlasnikRobe.SelectedValue),
                        txtVrstaRobe.Text.ToString().TrimEnd(), txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue), Convert.ToDecimal(txtVrednostRobe.Text),
                        cboValuta.SelectedValue.ToString(), Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransporta.SelectedValue), Convert.ToInt32(cboVrstaKamiona.SelectedValue), txtVozilo.Text, txtVozac.Text,
                        txtLK.Text, txtTelefon.Text, Convert.ToInt32(cboCarinarnica.SelectedValue), Convert.ToInt32(cboSpediter.SelectedValue), txtKontakOsobaSpediter.Text, Convert.ToInt32(cboMestoIstovara.SelectedValue),
                        txtAdresa.Text, txtKontaktOsoba.Text, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToDateTime(dateTimePicker2.Value), txtPosebniUslovi.Text, idUsluge, txtNapomena.Text,
                        aktivan, formiran);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Update:\n" + ex.ToString());
                }

            }
            else
            {
                try
                {
                    int rn = 0;

                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(ID),0)+1 FROM RNCarinskoSkladiste", conn))
                        {
                            object result = cmd.ExecuteScalar();
                            rn = Convert.ToInt32(result);
                        }
                    }

                    int IdUsluge = 0;

                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(ID),0)+1 FROM RNCarinskoSkladisteDodatneUsluge", conn))
                        {
                            object result = cmd.ExecuteScalar();
                            IdUsluge = Convert.ToInt32(result);
                        }
                    }

                    foreach (var item in checkedListBox1.CheckedItems)
                    {
                        DataRowView row = item as DataRowView;
                        if (row == null)
                            continue;

                        int valueMember = Convert.ToInt32(row["ID"]);

                        ins.InsertDodatneUsluge(IdUsluge, rn, valueMember);
                    }

                    int formiran = 0;
                    int aktivan = 0;
                    if (chkFormiran.Checked == true)
                    {
                        formiran = 1;
                    }
                    if (chkAktivan.Checked == true)
                    {
                        aktivan = 1;
                    }

                    ins.InsertRN(Tip, textBox1.Text, MagacinskiBroj, tipMB, Convert.ToInt32(cboNalogodavac.SelectedValue), Convert.ToInt32(cboVlasnikRobe.SelectedValue),
                        txtVrstaRobe.Text.ToString().TrimEnd(), txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue), Convert.ToDecimal(txtVrednostRobe.Text),
                        cboValuta.SelectedValue.ToString(), Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransporta.SelectedValue), Convert.ToInt32(cboVrstaKamiona.SelectedValue), txtVozilo.Text, txtVozac.Text,
                        txtLK.Text, txtTelefon.Text, Convert.ToInt32(cboCarinarnica.SelectedValue), Convert.ToInt32(cboSpediter.SelectedValue), txtKontakOsobaSpediter.Text, Convert.ToInt32(cboMestoIstovara.SelectedValue),
                        txtAdresa.Text, txtKontaktOsoba.Text, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToDateTime(dateTimePicker2.Value), txtPosebniUslovi.Text, IdUsluge, txtNapomena.Text,
                        aktivan, formiran);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Insert \n:" + ex.ToString());
                }
            }
            MessageBox.Show("RADNI NALOG SACUVAN");
        }

        private void btnMagacinskiBroj_Click(object sender, EventArgs e)
        {
            Carinko.frmMagacinskiBrojevi frm = new Carinko.frmMagacinskiBrojevi();
            frm.Show();
        }
        private void cboMagacinskiBroj_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }
        int PIB;
        private void cboVlasnikRobe_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand("SELECT PaEMatSt1 from Partnerji Where PaSifra=" + Convert.ToInt32(cboVlasnikRobe.SelectedValue), conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                PIB = Convert.ToInt32(dr[0].ToString());
                txtPIB.Text = PIB.ToString();
            }
            conn.Close();
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

                    chkAktivan.Checked = true;
            }
            else
            {
                tipMB = "";
            }
            
        }

        private void btnFormiranRN_Click(object sender, EventArgs e)
        {
            if (chkFormiran.Checked == true)
            {
                MessageBox.Show("RN je vec formiran!");
                return;
            }
            else
            {
                chkFormiran.Checked = true;
                try
                {
                    InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();
                    int idUsluge = 0;
                    using (SqlConnection conn = new SqlConnection(connection))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("Select top 1 ID From RNCarinskoSkladisteDodatneUsluge Where RN=" + Convert.ToInt32(txtID.Text), conn))
                        {
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                idUsluge = Convert.ToInt32(dr[0].ToString());
                            }
                        }
                    }
                    ins.DeleteDodatneUsluge(Convert.ToInt32(txtID.Text));

                    foreach (var item in checkedListBox1.CheckedItems)
                    {
                        DataRowView row = item as DataRowView;
                        if (row == null)
                            continue;

                        int valueMember = Convert.ToInt32(row["ID"]);

                        ins.InsertDodatneUsluge(idUsluge, Convert.ToInt32(txtID.Text), valueMember);
                    }

                    int formiran = 0;
                    int aktivan = 0;
                    if (chkFormiran.Checked == true)
                    {
                        formiran = 1;
                    }
                    if (chkAktivan.Checked == true)
                    {
                        aktivan = 1;
                    }

                    ins.UpdateRN(Convert.ToInt32(txtID.Text), Tip, textBox1.Text, MagacinskiBroj, tipMB, Convert.ToInt32(cboNalogodavac.SelectedValue), Convert.ToInt32(cboVlasnikRobe.SelectedValue),
                        txtVrstaRobe.Text.ToString().TrimEnd(), txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue), Convert.ToDecimal(txtVrednostRobe.Text),
                        cboValuta.SelectedValue.ToString(), Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransporta.SelectedValue), Convert.ToInt32(cboVrstaKamiona.SelectedValue), txtVozilo.Text, txtVozac.Text,
                        txtLK.Text, txtTelefon.Text, Convert.ToInt32(cboCarinarnica.SelectedValue), Convert.ToInt32(cboSpediter.SelectedValue), txtKontakOsobaSpediter.Text, Convert.ToInt32(cboMestoIstovara.SelectedValue),
                        txtAdresa.Text, txtKontaktOsoba.Text, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToDateTime(dateTimePicker2.Value), txtPosebniUslovi.Text, idUsluge, txtNapomena.Text,
                        aktivan, formiran);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Update:\n" + ex.ToString());
                }
            }
        }

        private void btnPrijemnica_Click(object sender, EventArgs e)
        {
            if (!chkAktivan.Checked)
            {
                MessageBox.Show("RN nije aktivan!");
                return;
            }

            if (!chkFormiran.Checked)
            {
                MessageBox.Show("RN nije formiran!");
                return;
            }

            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new PrijemnicaCarinskoSkladiste(Tip, ID));
        }
    }
}
