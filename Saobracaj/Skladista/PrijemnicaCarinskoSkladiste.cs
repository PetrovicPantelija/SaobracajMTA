using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.TrackModal.Sifarnici;
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
            InitTable();
            FillCombo();
            VratiPodatke(RN);
            FillDodatneUsluge(RN.ToString());
            VratiStavkePrijemnice(Convert.ToInt32(txtPrijemnica.Text));

            panel6.Visible = false;
            panel7.Visible = false;
        }
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

        int MagacinskiBroj;
        string tipMB;
        int DodatneUslugeID;
        private void VratiPodatke(int id)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"SELECT RNCarinskoSkladiste.ID,TipRN,CarinskoSkladiste,MagacinskiBroj,Nalogodavac,VlasnikRobe,VrstaRobe,NacinPakovanja,OstalaSkladista,PIB,
VrstaPrevoznogSredstva,VrstaKamiona,Vozilo,Vozac,BrojLK,BrojTelefona,OdredisnaCarinarnica,Spediter,KontakOsobaSpeditera,MestoIstovara,Adresa,KontaktOsobaIstovar,
PlaniraniDatum,PlaniraniDatum2,PosebniUslovi,DodatneUslugeID,Napomena,Aktivan,Formiran,BrojKontejnera,
RNCarinskoSkladistePrijemnica.ID as Prijemnica,CarinskiPostupak,SmestajniDokument,Rok,Posiljalac,Faktura,CRM,uProcesu
From RNCarinskoSkladiste
inner join RNCarinskoSkladistePrijemnica on RNCarinskoSkladiste.ID=RNCarinskoSkladistePrijemnica.RN
Where RNCarinskoSkladiste.ID=" + id, conn);
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
                txtKontejner.Text = dr["BrojKontejnera"].ToString();
                txtPrijemnica.Text = dr["Prijemnica"].ToString();
                DodatneUslugeID = Convert.ToInt32(dr["DodatneUslugeID"].ToString());
                if (dr["CarinskiPostupak"] != DBNull.Value)
                {
                  cboCarinskiPostupak.SelectedValue = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
                }
                txtSmestajniDokument.Text = dr["SmestajniDokument"].ToString();
                txtRok.Text = dr["Rok"].ToString();
                if (dr["Posiljalac"] != DBNull.Value)
                {
                    cboPosiljalac.SelectedValue = Convert.ToInt32(dr["Posiljalac"].ToString());
                }
                txtFaktura.Text = dr["Faktura"].ToString();
                txtCRM.Text = dr["CRM"].ToString();

                int proces = Convert.ToInt32(dr["uProcesu"].ToString());
                if (proces == 1)
                {
                    chkUprocesu.Checked = true;
                }

            }
            conn.Close();

        }

        private void VratiStavkePrijemnice(int prijemnica)
        {
            list.Clear();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                string query = @"
            SELECT 
                RB,
                NHM,
                Naziv,
                Naimenovanje,
                JM,
                Koleta,
                Bruto,
                Vrednost,
                Valuta,
                Pozicija,
                Paleta,
                VrstaPaleta,
                PDV,
                Carina
            FROM RNCarinskoPrijemnicaStavke
            WHERE IDNadredjena = @ID
            ORDER BY RB";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", prijemnica);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var stavka = new PrijemnicaStavke
                            {
                                RB = Convert.ToInt32(dr["RB"]),
                                NHM = dr["NHM"] == DBNull.Value ? 0 : Convert.ToInt32(dr["NHM"]),
                                Naziv = dr["Naziv"] == DBNull.Value ? "" : dr["Naziv"].ToString(),
                                Naimenovanje = dr["Naimenovanje"] == DBNull.Value ? "" : dr["Naimenovanje"].ToString(),
                                JM = dr["JM"] == DBNull.Value ? "" : dr["JM"].ToString(),
                                Koleta = dr["Koleta"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Koleta"]),
                                Bruto = dr["Bruto"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Bruto"]),
                                Vrednost = dr["Vrednost"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Vrednost"]),
                                Valuta = dr["Valuta"] == DBNull.Value ? "" : dr["Valuta"].ToString(),
                                Pozicija = dr["Pozicija"] == DBNull.Value ? "" : dr["Pozicija"].ToString(),
                                Paleta = dr["Paleta"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Paleta"]),
                                VrstaPaleta = dr["VrstaPaleta"] == DBNull.Value ? 0 : Convert.ToInt32(dr["VrstaPaleta"]),
                                PaletaOpis = "",       // ako ovo nemaš u tabeli, ostavi prazno
                                DimenzijePaleta = "",  // ako ovo nemaš u tabeli, ostavi prazno
                                PDV = dr["PDV"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["PDV"]),
                                Carina = dr["Carina"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["Carina"])
                            };

                            list.Add(stavka);
                        }
                    }
                }
            }

            if (list.Count > 0)
                rb = list.Max(x => x.RB) + 1;
            else
                rb = 1;

            OsveziGrid();
        }
        private void FillCombo()
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
            }

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

            var postupak = "Select Id,Naziv From VrstaCarinskogPostupka order by ID desc";
            var daPostupak = new SqlDataAdapter(postupak, conn);
            var dsPostupak = new System.Data.DataSet();
            daPostupak.Fill(dsPostupak);
            cboCarinskiPostupak.DataSource = dsPostupak.Tables[0];
            cboCarinskiPostupak.DisplayMember = "Naziv";
            cboCarinskiPostupak.ValueMember = "Id";

            var posiljalac= "Select PaSifra,PaNaziv From Partnerji Where Posiljalac=1 order by PaSifra";
            var daPosiljalac = new SqlDataAdapter(posiljalac, conn);
            var dsPosiljalac = new System.Data.DataSet();
            daPosiljalac.Fill(dsPosiljalac);
            cboPosiljalac.DataSource = dsPosiljalac.Tables[0];
            cboPosiljalac.DisplayMember = "PaNaziv";
            cboPosiljalac.ValueMember = "PaSifra";


            var pozicija = "Select ID,Naziv From Skladista Order by id asc";
            var daPozicija = new SqlDataAdapter(pozicija, conn);
            var dsPozicija = new System.Data.DataSet();
            daPozicija.Fill(dsPozicija);
            cboPozicija.DataSource = dsPozicija.Tables[0];
            cboPozicija.DisplayMember = "Naziv";
            cboPozicija.ValueMember = "ID";

        }
        
        private void cboMagacinskiBroj_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
        public class PrijemnicaStavke
        {
            public int RB { get; set; }
            public int NHM { get; set; }
            public string Naziv { get; set; }
            public string Naimenovanje { get; set; }
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
                    NHM = nhm,
                    Naziv = txtArtikal.Text.ToString(),
                    Naimenovanje = txtNaimenovanje.Text.ToString().TrimEnd(),
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
                nhm = 0;
                txtArtikal.Text = "";
                txtNaimenovanje.Text = "";
                txtKoleta.Text = "";
                txtBruto.Text = "";
                txtVrednost.Text = "";
                txtPaleta.Text = "";
            }
            catch { }
        }

        private void btnMapa_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuBezPrava(() => new TerminalMap.TerminalMapFRM());
        }

        private void btnIzbaci_Click(object sender, EventArgs e)
        {
            if (dgvUsluge.CurrentRow == null)
                return;

            dgvUsluge.Rows.Remove(dgvUsluge.CurrentRow);
        }

        private void btnDodajUsluge_Click(object sender, EventArgs e)
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

        private void FillNHM()
        {
            SqlConnection conn = new SqlConnection(connection);
            var query = "select ID,Broj,RTRIM(Naziv) as Naziv from NHM order by ID asc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new System.Data.DataSet();

            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

        }

        private void NHMKod()
        {
            SqlConnection conn = new SqlConnection(connection);
            var query = "select ID,Broj,RTRIM(Naziv) as Naziv from NHM Where Broj like '%"+txtNhm.Text.ToString().TrimEnd()+"%'order by ID asc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new System.Data.DataSet();

            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
        }
        private void NHMNaziv()
        {
            SqlConnection conn = new SqlConnection(connection);
            var query = "select ID,Broj,RTRIM(Naziv) as Naziv from NHM Where Naziv like '%" + txtNhmNaziv.Text.ToString().TrimEnd() + "%'order by ID asc";
            var da = new SqlDataAdapter(query, conn);
            var ds = new System.Data.DataSet();

            da.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;

        }

        private void btnNHM_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            FillNHM();
            txtNhm.Text = "";
            txtNhmNaziv.Text = "";
        }

        private void txtNhm_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtNhm.Text != "")
            {
                NHMKod();
            }
            else
            {
                FillNHM();
            }
        }

        private void txtNhmNaziv_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtNhmNaziv.Text != "")
            {
                NHMNaziv();
            }
            else
            {
                FillNHM();
            }
        }

        private void btnNHMNazad_Click(object sender, EventArgs e)
        {
            txtNhm.Text = "";
            txtNhmNaziv.Text = "";
            panel6.Visible = false;
        }
        int nhm = 0;

        private void btnNHMDodaj_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    nhm = Convert.ToInt32(row.Cells["ID"].Value);
                    txtArtikal.Text = row.Cells["Naziv"].Value.ToString().TrimEnd();
                }
            }
            panel6.Visible = false;
        }

        private void btnPaleta_Click(object sender, EventArgs e)
        {
            frmTipPalete frm = new frmTipPalete();
            frm.Show();
        }

        private void btnSnimi_Click(object sender, EventArgs e)
        {
            InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();
            try
            {
                int proces;
                if (chkUprocesu.Checked)
                {
                    proces = 1;
                }
                else
                {
                    proces = 0;
                }
                    ins.UpdatePrijemnicaCarinska(tKorisnik, Convert.ToInt32(txtPrijemnica.Text), Convert.ToInt32(txtID.Text), Convert.ToInt32(cboCarinskiPostupak.SelectedValue),
                        txtSmestajniDokument.Text.ToString().TrimEnd(), txtRok.Text.ToString().TrimEnd(), Convert.ToInt32(cboPosiljalac.SelectedValue), txtFaktura.Text.ToString().TrimEnd(),
                        txtCRM.Text.ToString().TrimEnd(), "OD",proces);

                foreach(var i in list)
                {
                    ins.InsertPrijemnicaCarinskaStavke(Convert.ToInt32(txtPrijemnica.Text), i.RB, i.NHM, i.Naziv, i.Naimenovanje, i.JM, i.Koleta, i.Bruto, i.Vrednost, i.Valuta,
                        i.Pozicija, i.Paleta, i.VrstaPaleta, Convert.ToInt32(i.PDV), Convert.ToInt32(i.Carina));
                }

                MessageBox.Show("Sačuvane stavke prijemnice");
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR Insert prijemnica:" + ex.ToString());
            }
        }

        private void NalogZaViljuskaristu()
        {
            SqlConnection conn = new SqlConnection(connection);
            var query = "SELECT IDNadredjena as Prijemnica,Pozicija,VrstaPaleta,TipPalete.Naziv as Paleta,SUM(Koleta) AS Koleta,SUM(Paleta) AS Paleta,SUM(BRuto) as Bruto " +
                "FROM RNCarinskoPrijemnicaStavke " +
                "inner join TipPalete on RNCarinskoPrijemnicaStavke.VrstaPaleta=TipPalete.ID " +
                "Where IDNadredjena=" + Convert.ToInt32(txtPrijemnica.Text) + " GROUP BY IDNadredjena,Pozicija,VrstaPaleta,TipPalete.Naziv ORDER BY IDNadredjena,Pozicija,VrstaPaleta;";
            var da = new SqlDataAdapter(query, conn);
            var ds = new System.Data.DataSet();

            da.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[1].Width = 80;
            dataGridView2.Columns[3].Width = 60;
            dataGridView2.Columns[4].Width = 60;
            dataGridView2.Columns[5].Width = 60;
            dataGridView2.Columns[6].Width = 80;

            label49.Text = "Prijemnica: " + txtPrijemnica.Text.ToString();

            var viljuskarista = "SELECT DeSifra, (RTrim(DeIme)+' '+RTrim(DePriimek)) as Zaposleni from Delavci Where Viljuskarista=1";
            var daViljuskarista = new SqlDataAdapter(viljuskarista, conn);
            var dsViljuskarista = new System.Data.DataSet();
            daViljuskarista.Fill(dsViljuskarista);
            cboRukovalac.DataSource = dsViljuskarista.Tables[0];
            cboRukovalac.DisplayMember = "Zaposleni";
            cboRukovalac.ValueMember = "DeSifra";

        }
        int viljuskaristaID;
        private void btnNalog_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            NalogZaViljuskaristu();
        }

        private void btnVratiNalog_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
        }

        private void cboRukovalac_SelectionChangeCommitted(object sender, EventArgs e)
        {
            viljuskaristaID= Convert.ToInt32(cboRukovalac.SelectedValue);
        }

        private void btnIzdajNalog_Click(object sender, EventArgs e)
        {
            var conn = new SqlConnection(connection);
            conn.Open();
            var cmd = new SqlCommand(@"select ID From RNCarinskoSkladisteDodatneUsluge Where RN=" + Convert.ToInt32(txtID.Text), conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DodatneUslugeID = Convert.ToInt32(dr["ID"].ToString());
            }
            conn.Close();

            int rnID = 0;
            conn.Open();
            var cmd2 = new SqlCommand(@"select (ISNULL(Max(ID)+1,0)) from RNCarinskoSkladisteRukovalac", conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                rnID = Convert.ToInt32(dr2[0].ToString());
            }
            conn.Close();

            if(rnID==0)
            {
                rnID = 1;
            }

            InsertCarinskoSkladiste ins = new InsertCarinskoSkladiste();
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    ins.InsertNalogRukovalac(rnID, Convert.ToInt32(txtPrijemnica.Text), Convert.ToInt32(cboRukovalac.SelectedValue), row.Cells["Pozicija"].Value.ToString(), Convert.ToDecimal(row.Cells["Koleta"].Value),
                    Convert.ToInt32(row.Cells["Paleta1"].Value), Convert.ToInt32(row.Cells["VrstaPaleta"].Value), Convert.ToDecimal(row.Cells["Bruto"].Value), DodatneUslugeID, "", txtVozilo.Text.ToString().TrimEnd(),
                    1, tKorisnik);
                }
                MessageBox.Show("Kreiran nalog za rukovaoca. ID:" + rnID.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            //ins.InsertNalogRukovalac(Convert.ToInt32(txtPrijemnica.Text), viljuskaristaID, DodatneUslugeID);

        }
    }
}
