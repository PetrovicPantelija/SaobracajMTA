using Saobracaj.Drumski;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Tools.MultiColumnTreeView;
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
        string Status;
        int ID;
        public RnCS(string tip)
        {
            InitializeComponent();
            Tip = tip;
            if (tip == "Carinsko")
            {
                textBox1.Text = "1008";
            }
            panel5.Visible = false;

            InitTable();
            FillCombo();
            FillMagacinskiBroj();

            FillDodatneUsluge(txtID.Text);

        }
        public RnCS(string tip, int id, string status)
        {
            InitializeComponent();
            Tip = tip;
            ID = id;
            Status = status;

            panel5.Visible = false;

            FillCombo();
            InitTable();
            FillMagacinskiBroj();
            VratiPodatke(ID);
            FillDodatneUsluge(txtID.Text);
            

        }
        private void RnCS_Load(object sender, EventArgs e)
        {

        }
        private void VratiPodatke(int id)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"SELECT ID,TipRN,CarinskoSkladiste,MagacinskiBroj,Nalogodavac,VlasnikRobe,VrstaRobe,NacinPakovanja,OstalaSkladista,VrednostRobe,Valuta,PIB,
VrstaPrevoznogSredstva,VrstaKamiona,Vozilo,Vozac,BrojLK,BrojTelefona,OdredisnaCarinarnica,Spediter,KontakOsobaSpeditera,MestoIstovara,Adresa,KontaktOsobaIstovar,
PlaniraniDatum,PlaniraniDatum2,PosebniUslovi,DodatneUslugeID,Napomena,Aktivan,Formiran,BrojKontejnera
From RNCarinskoSkladiste
Where ID=" + id, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                Tip = dr["TipRN"].ToString();
                textBox1.Text = dr["CarinskoSkladiste"].ToString();
                MagacinskiBroj = Convert.ToInt32(dr["MagacinskiBroj"].ToString());
                cboNalogodavac.SelectedValue = Convert.ToInt32(dr["Nalogodavac"].ToString());
                cboVlasnikRobe.SelectedValue = Convert.ToInt32(dr["VlasnikRobe"].ToString());
                txtVrstaRobe.Text = dr["VrstaRobe"].ToString();
                txtNacinPakovanja.Text = dr["NacinPakovanja"].ToString();
                cboADR.SelectedValue = Convert.ToInt32(dr["OstalaSkladista"].ToString());
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
                txtKontejner.Text = dr["BrojKontejnera"].ToString().TrimEnd();

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
                if (MagacinskiBroj != 0)
                {
                    cboMagacinskiBroj.SelectedValue = MagacinskiBroj;
                }

            }
            conn.Close();

        }
        private void FillMagacinskiBroj()
        {
            SqlConnection conn = new SqlConnection(connection);

            if (Tip == "Carinsko")
            {
                var partner5 = "Select ID, Naziv from MagacinskiBrojCarinski order by ID Desc";
                var partAD5 = new SqlDataAdapter(partner5, conn);
                var partDS5 = new System.Data.DataSet();
                partAD5.Fill(partDS5);
                cboMagacinskiBroj.DataSource = partDS5.Tables[0];
                cboMagacinskiBroj.DisplayMember = "Naziv";
                cboMagacinskiBroj.ValueMember = "ID";

                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection myConnection = new SqlConnection(s_connection);
                var c = new SqlConnection(s_connection);
                var dataAdapter = new SqlDataAdapter(partner5, c);

                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();
                dataAdapter.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];

                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select ISNUll(MIN(ID),0) from MagacinskiBrojCarinski Where Naziv=''", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (Convert.ToInt32(dr[0].ToString()) == 0)
                        {
                            txtMbID.Text = "";
                        }
                        else
                        {
                            txtMbID.Text = dr[0].ToString();
                        }
                    }

                }
            }
        }

        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var dodatneUsluge = "SELECT ID, RTRIM(Naziv) AS Naziv FROM VrstaManipulacije ORDER BY ID ASC";
            var daDodatneUsluge = new SqlDataAdapter(dodatneUsluge, conn);
            var dsDodatneUsluge = new System.Data.DataSet();
            daDodatneUsluge.Fill(dsDodatneUsluge);
            cboDodatneUsluge.DataSource = dsDodatneUsluge.Tables[0];
            cboDodatneUsluge.DisplayMember = "Naziv";
            cboDodatneUsluge.ValueMember = "ID";

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
            var dsCarinarnica = new DataSet();
            daCarinarnica.Fill(dsCarinarnica);
            cboCarinarnica.DataSource = dsCarinarnica.Tables[0];
            cboCarinarnica.DisplayMember = "Naziv";
            cboCarinarnica.ValueMember = "ID";
            cboCarinarnica.SelectedValue = 140;


            var mu = "select ID, Naziv from MestaUtovara order by Naziv";
            var muAD = new SqlDataAdapter(mu, conn);
            var muDS = new DataSet();
            muAD.Fill(muDS);
            cboMestoIstovara.DataSource = muDS.Tables[0];
            cboMestoIstovara.DisplayMember = "Naziv";
            cboMestoIstovara.ValueMember = "ID";
            cboMestoIstovara.SelectedValue = 2;

        }
        string pomUsluge;

        int MagacinskiBroj = 0;


        DataTable dtUsluge = new DataTable();
        private void InitTable()
        {
            dtUsluge = new DataTable();
            dtUsluge.Columns.Add("UslugaID", typeof(int));
            dtUsluge.Columns.Add("Naziv", typeof(string));

            dgvUsluge.AutoGenerateColumns = true;
            dgvUsluge.DataSource = dtUsluge;
        }
        private void FillDodatneUsluge(string rn)
        {
            dtUsluge.Rows.Clear();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = @"SELECT d.Usluga, RTRIM(v.Naziv) Naziv
                         FROM RNCarinskoSkladisteDodatneUsluge d
                         INNER JOIN VrstaManipulacije v ON d.Usluga = v.ID
                         WHERE d.RN = @RN";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@RN", rn);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            dtUsluge.Rows.Add(
                                Convert.ToInt32(dr["Usluga"]),
                                dr["Naziv"].ToString()
                            );
                        }
                    }
                }
            }
        }
        private void btnDodaj_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cboDodatneUsluge.SelectedValue);
            string naziv = cboDodatneUsluge.Text;

            foreach (DataRow row in dtUsluge.Rows)
            {
                if ((int)row["UslugaID"] == id)
                {
                    MessageBox.Show("Usluga je već dodata.");
                    return;
                }
            }

            dtUsluge.Rows.Add(id, naziv);
        }

        private void btnIzbaci_Click(object sender, EventArgs e)
        {
            if (dgvUsluge.CurrentRow == null)
                return;

            dgvUsluge.Rows.Remove(dgvUsluge.CurrentRow);
        }
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

                    foreach (DataRow row in dtUsluge.Rows)
                    {
                        int valueMember = Convert.ToInt32(row["UslugaID"]);
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

                    ins.UpdateRN(Convert.ToInt32(txtID.Text), Tip, textBox1.Text, MagacinskiBroj, Convert.ToInt32(cboNalogodavac.SelectedValue), Convert.ToInt32(cboVlasnikRobe.SelectedValue),
                        txtVrstaRobe.Text.ToString().TrimEnd(), txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue),
                        Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransporta.SelectedValue), Convert.ToInt32(cboVrstaKamiona.SelectedValue), txtVozilo.Text, txtVozac.Text,
                        txtLK.Text, txtTelefon.Text, Convert.ToInt32(cboCarinarnica.SelectedValue), Convert.ToInt32(cboSpediter.SelectedValue), txtKontakOsobaSpediter.Text, Convert.ToInt32(cboMestoIstovara.SelectedValue),
                        txtAdresa.Text, txtKontaktOsoba.Text, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToDateTime(dateTimePicker2.Value), txtPosebniUslovi.Text, idUsluge, txtNapomena.Text,
                        aktivan, formiran, Status,tKorisnik,txtKontejner.Text.ToString().TrimEnd());

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

                    foreach (DataRow row in dtUsluge.Rows)
                    {
                        int valueMember = Convert.ToInt32(row["UslugaID"]);
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

                    ins.InsertRN(Tip, textBox1.Text, MagacinskiBroj, Convert.ToInt32(cboNalogodavac.SelectedValue), Convert.ToInt32(cboVlasnikRobe.SelectedValue),
                        txtVrstaRobe.Text.ToString().TrimEnd(), txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue),
                        Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransporta.SelectedValue), Convert.ToInt32(cboVrstaKamiona.SelectedValue), txtVozilo.Text, txtVozac.Text,
                        txtLK.Text, txtTelefon.Text, Convert.ToInt32(cboCarinarnica.SelectedValue), Convert.ToInt32(cboSpediter.SelectedValue), txtKontakOsobaSpediter.Text, Convert.ToInt32(cboMestoIstovara.SelectedValue),
                        txtAdresa.Text, txtKontaktOsoba.Text, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToDateTime(dateTimePicker2.Value), txtPosebniUslovi.Text, IdUsluge, txtNapomena.Text,
                        aktivan, formiran,tKorisnik,txtKontejner.Text.ToString().TrimEnd());

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
            FillMagacinskiBroj();
            panel5.Visible = true;

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
                chkAktivan.Checked = true;
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

                    foreach (DataRow row in dtUsluge.Rows)
                    {
                        int valueMember = Convert.ToInt32(row["UslugaID"]);
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

                    ins.UpdateRN(Convert.ToInt32(txtID.Text), Tip, textBox1.Text, MagacinskiBroj, Convert.ToInt32(cboNalogodavac.SelectedValue), Convert.ToInt32(cboVlasnikRobe.SelectedValue),
                        txtVrstaRobe.Text.ToString().TrimEnd(), txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue),
                        Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransporta.SelectedValue), Convert.ToInt32(cboVrstaKamiona.SelectedValue), txtVozilo.Text, txtVozac.Text,
                        txtLK.Text, txtTelefon.Text, Convert.ToInt32(cboCarinarnica.SelectedValue), Convert.ToInt32(cboSpediter.SelectedValue), txtKontakOsobaSpediter.Text, Convert.ToInt32(cboMestoIstovara.SelectedValue),
                        txtAdresa.Text, txtKontaktOsoba.Text, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToDateTime(dateTimePicker2.Value), txtPosebniUslovi.Text, idUsluge, txtNapomena.Text,
                        aktivan, formiran, Status,tKorisnik,txtKontejner.Text.ToString().TrimEnd());


                    ins.InsertPrijemnicaCarinska(tKorisnik, Convert.ToInt32(txtID.Text), null, "", "", null, "", "", "OD");

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

        private void btnMbNazad_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void btnMbSave_Click(object sender, EventArgs e)
        {
            InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();

            if (Tip == "Carinsko")
            {
                if (string.IsNullOrWhiteSpace(txtMbID.Text))
                {
                    using(SqlConnection conn=new SqlConnection(connection))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("select Max(ID)+1 from MagacinskiBrojCarinski", conn))
                        {
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                MagacinskiBroj = Convert.ToInt32(dr[0].ToString());
                                if (MagacinskiBroj != 0)
                                {
                                    chkAktivan.Checked = true;
                                }
                            }
                        }
                    }
                    
                    ins.InsertMagacinskiBrojCarinski(MagacinskiBroj, txtMbNaziv.Text.Trim());
                    FillMagacinskiBroj();
                    cboMagacinskiBroj.SelectedValue = MagacinskiBroj;

                }
                else
                {
                    int mbId = Convert.ToInt32(txtMbID.Text.Trim());

                    ins.UpdateMagacinskiBrojCarinski(mbId, txtMbNaziv.Text.Trim());
                    FillMagacinskiBroj();
                    MagacinskiBroj = mbId;
                    cboMagacinskiBroj.SelectedValue = MagacinskiBroj;
                    if (MagacinskiBroj != 0)
                    {
                        chkAktivan.Checked = true;
                    }
                }

                panel5.Visible = false;
            }
        }

        private void btnIspravka_Click(object sender, EventArgs e)
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

                    foreach (DataRow row in dtUsluge.Rows)
                    {
                        int valueMember = Convert.ToInt32(row["UslugaID"]);
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

                    ins.UpdateRN(Convert.ToInt32(txtID.Text), Tip, textBox1.Text, MagacinskiBroj, Convert.ToInt32(cboNalogodavac.SelectedValue), Convert.ToInt32(cboVlasnikRobe.SelectedValue),
                        txtVrstaRobe.Text.ToString().TrimEnd(), txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue),
                        Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransporta.SelectedValue), Convert.ToInt32(cboVrstaKamiona.SelectedValue), txtVozilo.Text, txtVozac.Text,
                        txtLK.Text, txtTelefon.Text, Convert.ToInt32(cboCarinarnica.SelectedValue), Convert.ToInt32(cboSpediter.SelectedValue), txtKontakOsobaSpediter.Text, Convert.ToInt32(cboMestoIstovara.SelectedValue),
                        txtAdresa.Text, txtKontaktOsoba.Text, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToDateTime(dateTimePicker2.Value), txtPosebniUslovi.Text, idUsluge, txtNapomena.Text,
                        aktivan, formiran, Status,tKorisnik,txtKontejner.Text.ToString().TrimEnd());

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Update:\n" + ex.ToString());
                }

            }
            else
            {
                MessageBox.Show("Mora se prvo snimit RN!");

            }
        }

        private void btnStorno_Click(object sender, EventArgs e)
        {
            DialogResult result=MessageBox.Show("Da li ste sigurni da želite stornirati ovaj RN?", "Storno RN", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes) 
            { 
                InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();
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

                    ins.UpdateRN(Convert.ToInt32(txtID.Text), Tip, textBox1.Text, MagacinskiBroj, Convert.ToInt32(cboNalogodavac.SelectedValue), Convert.ToInt32(cboVlasnikRobe.SelectedValue),
                       txtVrstaRobe.Text.ToString().TrimEnd(), txtNacinPakovanja.Text.ToString().TrimEnd(), Convert.ToInt32(cboADR.SelectedValue),
                       Convert.ToInt32(txtPIB.Text), Convert.ToInt32(cboTipTransporta.SelectedValue), Convert.ToInt32(cboVrstaKamiona.SelectedValue), txtVozilo.Text, txtVozac.Text,
                       txtLK.Text, txtTelefon.Text, Convert.ToInt32(cboCarinarnica.SelectedValue), Convert.ToInt32(cboSpediter.SelectedValue), txtKontakOsobaSpediter.Text, Convert.ToInt32(cboMestoIstovara.SelectedValue),
                       txtAdresa.Text, txtKontaktOsoba.Text, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToDateTime(dateTimePicker2.Value), txtPosebniUslovi.Text, idUsluge, txtNapomena.Text,
                       0, 0, "STORNIRAN",tKorisnik,txtKontejner.Text.ToString().TrimEnd());

                    if(MagacinskiBroj != 0)
                    {
                        ins.DeleteMagacinskiBrojCarinski(MagacinskiBroj);
                    }

                    MessageBox.Show("RN je storniran!");

                    var main = this.TopLevelControl as NewMain;
                    if (main == null) return;

                    main.OtvoriFormuBezPrava(() => new RNCSPregled(Tip));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Storno:\n" + ex.ToString());
                }
            }
            else
            {
                return;
            }
        }
    }
}
