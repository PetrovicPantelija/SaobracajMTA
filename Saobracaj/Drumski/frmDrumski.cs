using Saobracaj.Pantheon_Export;
using Saobracaj.Uvoz;
using Syncfusion.Windows.Forms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class frmDrumski : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        int nalogId = 0;
        int id = 0;
        public frmDrumski()
        {
            InitializeComponent();
            ChangeTextBox();
            FillCombo();
            this.BindingContext = new BindingContext();
            VratiPodatke();
        }

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            meniHeader.Visible = false;
            this.ControlBox = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                meniHeader.Visible = true;
                meniHeader.Visible = false;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                //foreach (Control control in groupBox1.Controls)
                //{
                //    if (control is System.Windows.Forms.Button buttons)
                //    {

                //        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                //        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                //        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                //        buttons.FlatStyle = FlatStyle.Flat;
                //    }
                //}


                foreach (Control control in this.Controls)
                {

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {
                meniHeader.Visible = false;
                meniHeader.Visible = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }


        public frmDrumski(int ID)
        {
            this.id = ID;
            InitializeComponent();
            FillCombo();
            this.BindingContext = new BindingContext();
            VratiPodatke();
            ChangeTextBox();
        }
        private void VratiPodatke()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT	rn.ID ," +
             "rn.NalogID, rn.Uvoz, rn.KontejnerID, rn.Status, rn.IDVrstaManipulacije, rn.Uvoz, rn.AutoDan, rn.Ref, rn.MestoPreuzimanjaKontejnera, " +
             "p.PaNaziv AS Klijent, mu.Naziv AS MestoUtovara, rn.AdresaUtovara, rn.MestoIstovara AS MestoIstovara, rn.DatumUtovara, rn.DatumIstovara, rn.AdresaIstovara, " +
             "rn.KontaktOsobaNaIstovaru, rn.DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz, CAST(ik.Spedicija AS nvarchar) AS KontaktSpeditera, " +
             "rn.Trosak, rn.Valuta, ik.BookingBrodara, p2.PaNaziv AS BrojPlombe, ik.VGMBrod AS BTTKontejnetra, ik.BrutoRobe AS BTTRobe, " +
             "ik.NapomenaZaRobu as NapomenaZaPozicioniranje, a.RegBr,rn.KamionID , a.LicnaKarta, a.Vozac, a.BrojTelefona, rn.Cena, cc.Naziv AS CarinjenjeIzvozno,CAST(ik.Cirada AS VARCHAR) as TipTransporta," +
             "(ccp.Oznaka + ' ' + ccp.Naziv) AS OdredisnaCarina, '' as OdredisnaSpedicija, '' AS DodatniOpis, rn.KontaktNaIstovaru " +
             "FROM    RadniNalogDrumski rn " +
                      "INNER JOIN IzvozKonacna ik ON rn.KontejnerID = ik.ID " +
                      "LEFT JOIN MestaUtovara mu on mu.ID = ik.MesoUtovara " +
                      "LEFT JOIN Partnerji p on ik.Klijent3 = p.PaSifra " +
                      "LEFT JOIN Partnerji p2 ON ik.BrodskaPlomba = p2.PaSifra AND p2.Brodar = 1 " +
                      "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                       "LEFT JOIN VrstaCarinskogPostupka ccp on ccp.ID = ik.NapomenaReexport " +
                      "LEFT JOIN Carinarnice cc on cc.ID = ik.MestoCarinjenja " +
             "where rn.ID=" + id + " AND rn.Uvoz = 0 " +
             "UNION " +
             "SELECT	rn.ID ," +
             "rn.NalogID, rn.Uvoz, rn.KontejnerID, rn.Status, rn.IDVrstaManipulacije, rn.Uvoz, rn.AutoDan, rn.Ref, rn.MestoPreuzimanjaKontejnera, " +
             "p.PaNaziv AS Klijent,  mu.Naziv AS MestoUtovara, rn.AdresaUtovara,rn.MestoIstovara AS MestoIstovara, rn.DatumUtovara, rn.DatumIstovara, rn.AdresaIstovara, " +
             "rn.KontaktOsobaNaIstovaru, rn.DtPreuzimanjaPraznogKontejnera, rn.GranicniPrelaz,CAST(i.Spedicija AS nvarchar) AS KontaktSpeditera, " +
             "rn.Trosak, rn.Valuta, i.BookingBrodara, p2.PaNaziv AS BrojPlombe, i.VGMBrod AS BTTKontejnetra, i.BrutoRobe AS BTTRobe, " +
             "i.NapomenaZaRobu AS NapomenaZaPozicioniranje, a.RegBr, rn.KamionID,  a.LicnaKarta, a.Vozac, a.BrojTelefona, rn.Cena, cc.Naziv AS CarinjenjeIzvozno, CAST(i.Cirada AS VARCHAR) as TipTransporta," +
             "(ccp.Oznaka + ' ' + ccp.Naziv) AS OdredisnaCarina, '' as OdredisnaSpedicija, '' AS DodatniOpis, rn.KontaktNaIstovaru " +
             "FROM    RadniNalogDrumski rn " +
                      "INNER JOIN  Izvoz i ON rn.KontejnerID = i.ID  " +
                      "LEFT JOIN MestaUtovara mu on mu.ID = i.MesoUtovara " +
                      "LEFT JOIN Partnerji p on i.Klijent3 = p.PaSifra " +
                      "LEFT JOIN Partnerji p2 ON i.BrodskaPlomba = p2.PaSifra AND p2.Brodar = 1 " +
                      "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                       "LEFT JOIN VrstaCarinskogPostupka ccp on ccp.ID = i.NapomenaReexport " +
                      "LEFT JOIN Carinarnice cc on cc.ID = i.MestoCarinjenja " +
             "where rn.ID=" + id + " AND rn.Uvoz = 0 " +
             "UNION " +
             "SELECT rn.ID ," +
             "rn.NalogID,rn.Uvoz,rn.KontejnerID,rn.Status,rn.IDVrstaManipulacije,rn.Uvoz,rn.AutoDan,uk.Ref3 AS Ref,rn.MestoPreuzimanjaKontejnera, " +
             "p.PaNaziv AS Klijent,rn.MestoUtovara,rn.AdresaUtovara,mu.Naziv AS MestoIstovara,rn.DatumUtovara,rn.DatumIstovara,(Rtrim(pko.PaKOOpomba)) AS AdresaIstovara, " +
             "rn.KontaktOsobaNaIstovaru,rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz,rn.KontaktSpeditera, " +
             "rn.Trosak,rn.Valuta,0 AS BookingBrodara, uk.BrodskaTeretnica AS BrojPlombe,uk.BrutoKontejnera AS BTTKontejnetra, uk.BrutoRobe AS BTTRobe," +
             " np.Naziv as NapomenaZaPozicioniranje, a.RegBr, rn.KamionID,  a.LicnaKarta, a.Vozac, a.BrojTelefona , rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno, pr.Naziv as TipTransporta, " +
             " c.Naziv as OdredisnaCarina ,p2.PaNaziv as OdredisnaSpedicija,  rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru " +
             "FROM  RadniNalogDrumski rn " +
                    "INNER JOIN UvozKonacna uk ON rn.KontejnerID = uk.ID " +
                    "LEFT JOIN Partnerji p on uk.Nalogodavac3 = p.PaSifra " +
                    "LEFT JOIN MestaUtovara mu on mu.ID = uk.MestoIstovara " +
                    "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = uk.MestoIstovara " + /*AND PaKOSifra = mu.Naziv*/
                    "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = uk.NapomenaZaPozicioniranje " +
                    "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                    "LEFT JOIN VrstePostupakaUvoz pr ON pr.ID = uk.PostupakSaRobom " +
                    "LEFT JOIN VrstaCarinskogPostupka vcp on vcp.ID = uk.CarinskiPostupak " +
                    "LEFT JOIN Carinarnice c on c.ID = uk.OdredisnaCarina " +
                     "LEFT JOIN Partnerji p2 on p2.PaSifra = uk.OdredisnaSpedicija " +
             "where rn.ID= " + id + " AND rn.Uvoz = 1 " +
             "UNION " +
             "SELECT rn.ID ," +
             "rn.NalogID,rn.Uvoz,rn.KontejnerID,rn.Status,rn.IDVrstaManipulacije,rn.Uvoz,rn.AutoDan,u.Ref3 AS Ref,rn.MestoPreuzimanjaKontejnera, " +
             "p.PaNaziv AS Klijent,rn.MestoUtovara,rn.AdresaUtovara,mu.Naziv AS MestoIstovara,rn.DatumUtovara,rn.DatumIstovara,(Rtrim(pko.PaKOOpomba)) AS AdresaIstovara,  " +
             "rn.KontaktOsobaNaIstovaru,rn.DtPreuzimanjaPraznogKontejnera,rn.GranicniPrelaz,rn.KontaktSpeditera, " +
             "rn.Trosak,rn.Valuta,0 AS BookingBrodara, u.BrodskaTeretnica AS BrojPlombe,u.BrutoKontejnera AS BTTKontejnetra, u.BrutoRobe AS BTTRobe, "+
             " np.Naziv as NapomenaZaPozicioniranje, a.RegBr, rn.KamionID, a.LicnaKarta, a.Vozac, a.BrojTelefona, rn.Cena, (vcp.Oznaka + ' ' + vcp.Naziv) as CarinjenjeIzvozno, pr.Naziv as TipTransporta," +
             " c.Naziv as OdredisnaCarina, p2.PaNaziv as OdredisnaSpedicija, rn.Opis AS DodatniOpis, rn.KontaktNaIstovaru  " +
             "FROM  RadniNalogDrumski rn " +
                    "INNER JOIN  Uvoz u ON rn.KontejnerID = u.ID " +
                    "LEFT JOIN Partnerji p on u.Nalogodavac3 = p.PaSifra " +
                    "LEFT JOIN MestaUtovara mu on mu.ID = u.MestoIstovara " +
                    "LEFT JOIN partnerjiKontOsebaMU pko ON pko.PaKOSifra = u.MestoIstovara " + /*AND PaKOSifra = mu.Naziv*/
                    "LEFT JOIN NapomenaZaPozicioniranje np ON np.ID = u.NapomenaZaPozicioniranje " +
                    "LEFT JOIN Automobili a on a.ID = rn.KamionID " +
                    "LEFT JOIN VrstePostupakaUvoz pr ON pr.ID = u.PostupakSaRobom " +
                    "LEFT JOIN VrstaCarinskogPostupka vcp on vcp.ID = u.CarinskiPostupak " +
                    "LEFT JOIN Carinarnice c on c.ID = u.OdredisnaCarina " +
                     "LEFT JOIN Partnerji p2 on p2.PaSifra = u.OdredisnaSpedicija " +
             "where rn.ID= " + id + " AND rn.Uvoz = 1", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
              
                txtID.Text = dr["ID"].ToString();
                txtReferenca.Text = dr["Ref"].ToString();
                txtTipNaloga.Text = Convert.ToInt32(dr["Uvoz"].ToString()) == 1 ? "Uvoz" : "Izvoz";
                txtBokingBrodara.Text = dr["BookingBrodara"].ToString();
                txtBL.Text = dr["BrojPlombe"].ToString();
                //// txtKontaktOsobeIstovar.Text = dr["KontaktOsobaNaIstovaru"].ToString();
                if (dr["AutoDan"] != DBNull.Value && Convert.ToInt32(dr["AutoDan"].ToString()) == 1)
                    chkAutoDan.Checked = true;
                txtMestoPreuzimanja.Text = dr["MestoPreuzimanjaKontejnera"].ToString();
                txtMestoUtovara.Text = dr["MestoUtovara"].ToString();
                txtAdresaUtovara.Text = dr["AdresaUtovara"].ToString();
                txtMestoIstovara.Text = dr["MestoIstovara"].ToString();
                txtKlijent.Text = dr["Klijent"].ToString();
                txtNapomenaPoz.Text = dr["NapomenaZaPozicioniranje"].ToString();
                txtVozac.Text =  dr["Vozac"].ToString();
                txtBrojTelefona.Text =  dr["BrojTelefona"].ToString();
                txtBrojLK.Text = dr["LicnaKarta"].ToString();

                if (dr["DatumUtovara"] != DBNull.Value)
                    dtpUtovara.Value = Convert.ToDateTime(dr["DatumUtovara"].ToString());
                if (dr["DatumIstovara"] != DBNull.Value)
                    dtIstovara.Value = Convert.ToDateTime(dr["DatumIstovara"].ToString());
                if (dr["DtPreuzimanjaPraznogKontejnera"] != DBNull.Value)
                    dtPreuzimanjaPraznogKontejnera.Value = Convert.ToDateTime(dr["DtPreuzimanjaPraznogKontejnera"].ToString());
                txtGranicniPrelaz.Text = dr["GranicniPrelaz"].ToString();
                //txtKontaktOsobeSpeditera.Text = dr["KontaktSpeditera"].ToString();
                if (dr["Trosak"] != DBNull.Value)
                    txtTrosak.Value = Convert.ToDecimal(dr["Trosak"].ToString());
                //if (dr["Valuta"] != DBNull.Value)
                //    txtValuta.SelectedValue = (dr["Valuta"]);
                if (dr["Valuta"] != DBNull.Value)
                {
                    string sifraValute = dr["Valuta"].ToString().Trim();
                    txtValuta.SelectedValue = sifraValute;
                }
                else
                {
                    txtValuta.SelectedIndex = -1;
                }
                if (dr["Status"] != DBNull.Value && int.TryParse(dr["Status"].ToString(), out int parsedMesoIstovaraID))
                {
                    cboStatus.SelectedValue = parsedMesoIstovaraID;
                }
                else
                {
                    cboStatus.SelectedIndex = -1;
                }
                if (dr["BTTKontejnetra"] != DBNull.Value)
                    txtBrutoK.Value = Convert.ToDecimal(dr["BTTKontejnetra"].ToString());
                if (dr["BTTRobe"] != DBNull.Value)
                    txtBrutoR.Value = Convert.ToDecimal(dr["BTTRobe"].ToString());
                if(dr["KamionID"]!=DBNull.Value)
                    comboBox1.SelectedValue =(dr["KamionID"].ToString());
                txtDodatniOpis.Text = dr["DodatniOpis"].ToString();

                txtOdredisnaCarinarnica.Text = dr["OdredisnaCarina"].ToString();
                txtSpediterCarinarnice.Text = dr["OdredisnaSpedicija"].ToString();
                txtCarinjenjeIzvozno.Text = dr["CarinjenjeIzvozno"].ToString().Trim();

                //
                txtkontaktNaIstovaru.Text = dr["KontaktNaIstovaru"].ToString();
                if (dr["Cena"] != DBNull.Value)
                    txtCena.Value = Convert.ToDecimal(dr["Cena"].ToString());

                if (Convert.ToInt32(dr["Uvoz"].ToString()) == 0)
                {
                    txtBokingBrodara.Enabled = false;
                    label18.Text = "BL";
                    txtMestoUtovara.Enabled = false; // Read-only prikaz
                    txtAdresaUtovara.Enabled = false;
                    txtKlijent.Enabled = false;
                    txtBrutoK.Enabled = false;
                    txtBrutoR.Enabled = false;
                    txtNapomenaPoz.Enabled = false;
                    txtDodatniOpis.Enabled = false;
                    txtOdredisnaCarinarnica.Enabled = false;
                    txtKontaktSpeditera.Enabled = false;
                    txtSpediterCarinarnice.Enabled = false;
                    txtCarinjenjeIzvozno.Enabled = false;
                    txtVozac.Enabled = false;
                    txtBrojLK.Enabled = false;
                    txtBrojTelefona.Enabled = false;
                }
                else if (Convert.ToInt32(dr["Uvoz"].ToString()) == 1)
                {
                    label18.Text = "Broj plombe";
                    txtBL.Enabled = false;
                    txtBokingBrodara.Enabled = false;
                    txtMestoUtovara.Enabled = true;
                    txtReferenca.Enabled = false;
                    txtKlijent.Enabled = false;
                    txtMestoIstovara.Enabled = false;
                    txtAdresaIstovara.Enabled = false;
                    txtBrutoK.Enabled = false;
                    txtBrutoR.Enabled = false;
                    txtNapomenaPoz.Enabled = false;
                    txtOdredisnaCarinarnica.Enabled = false;
                    txtKontaktSpeditera.Enabled = false;
                    txtSpediterCarinarnice.Enabled = false;
                    txtCarinjenjeIzvozno.Enabled = false;
                    txtVozac.Enabled = false;
                    txtBrojLK.Enabled = false;
                    txtBrojTelefona.Enabled = false;
                }
            }
            con.Close();
        }

        public void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);
            var val = "Select VaSifra, VaNaziv from Valute order by VaSifra";
            var valSAD = new SqlDataAdapter(val, conn);
            var valSDS = new DataSet();
            valSAD.Fill(valSDS);
            txtValuta.DataSource = valSDS.Tables[0];
            txtValuta.DisplayMember = "VaNaziv";
            txtValuta.ValueMember = "VaSifra";

            var kam =   "SELECT ID, Marka, RegBr, Vozac " +
                        " FROM Automobili " +
                        " WHERE Vozac IS NOT NULL AND Vozac <>''";
            var kamAD = new SqlDataAdapter(kam, conn);
            var kmaDS = new DataSet();
            kamAD.Fill(kmaDS);
            comboBox1.DataSource = kmaDS.Tables[0];
            comboBox1.DisplayMember = "RegBr";
            comboBox1.ValueMember = "ID";

            var stv = "select ID, Naziv from StatusVozila order by Naziv";
            var stvAD = new SqlDataAdapter(stv, conn);
            var stvDS = new DataSet();
            stvAD.Fill(stvDS);

            System.Data.DataTable dt = stvDS.Tables[0];
            DataRow prazanRed = dt.NewRow();
            prazanRed["ID"] = DBNull.Value; 
            prazanRed["Naziv"] = "";       
            dt.Rows.InsertAt(prazanRed, 0);

            cboStatus.DataSource = dt;
            cboStatus.DisplayMember = "Naziv";
            cboStatus.ValueMember = "ID";
        }

        private void frmDrumski_Load(object sender, EventArgs e)
        {
            //FillCombo();
            //this.BindingContext = new BindingContext();
            //VratiPodatke();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }


        private void button21_Click(object sender, EventArgs e)
        {

            int iD = 0;
            if (txtID.Text != null && int.TryParse(txtID.Text, out int parsedID))
            {
                iD = parsedID;
            }
            int AutoDan = 0;
            if (chkAutoDan.Checked == true)
            { AutoDan = 1; }
            string referenca = string.IsNullOrWhiteSpace(txtReferenca.Text) ? null : txtReferenca.Text;
            string mestoPreuzimanja = string.IsNullOrWhiteSpace(txtMestoPreuzimanja.Text) ? null : txtMestoPreuzimanja.Text;
            string mestoUtovara = string.IsNullOrWhiteSpace(txtMestoUtovara.Text) ? null : txtMestoUtovara.Text;
            string adresaUtovara = string.IsNullOrWhiteSpace(txtAdresaUtovara.Text) ? null : txtAdresaUtovara.Text;
            string mestoIstovara = string.IsNullOrWhiteSpace(txtMestoIstovara.Text) ? null : txtMestoIstovara.Text;
            string kontaktistovara = string.IsNullOrWhiteSpace(txtkontaktNaIstovaru.Text) ? null : txtkontaktNaIstovaru.Text;
            DateTime? datumIstovara = null;
            DateTime? datumUtovara = null;
            string kontaktSpeditera = string.IsNullOrWhiteSpace(txtKontaktOsobeSpeditera.Text) ? null : txtKontaktOsobeSpeditera.Text;
            if (dtpUtovara.Checked)
            {
                datumUtovara = dtpUtovara.Value;
            }
            if (dtIstovara.Checked)
            {
                datumIstovara = dtIstovara.Value;
            }
            string adresaIstovara = string.IsNullOrWhiteSpace(txtAdresaIstovara.Text) ? null : txtAdresaIstovara.Text;
            DateTime? dtPreuzimanjaPraznogKont = null;
            if (dtPreuzimanjaPraznogKontejnera.Checked)
            {
                dtPreuzimanjaPraznogKont = dtPreuzimanjaPraznogKontejnera.Value;
            }
            string granicniPrelaz = string.IsNullOrWhiteSpace(txtGranicniPrelaz.Text) ? null : txtGranicniPrelaz.Text;
            decimal? trosak = null;
            decimal? cena = null;
            if (!string.IsNullOrWhiteSpace(txtTrosak.Text) && decimal.TryParse(txtTrosak.Text, out decimal parsedTrosak))
            {
                trosak = parsedTrosak;
            }

            if (!string.IsNullOrWhiteSpace(txtCena.Text) && decimal.TryParse(txtCena.Text, out decimal parsedCena))
            {
                cena = parsedCena;
            }

            string valutaID = null;
            if (txtValuta.SelectedValue != null)
            {
                valutaID = txtValuta.SelectedValue.ToString();
            }
            int? kamionID = null;
            if (comboBox1.SelectedValue != null && int.TryParse(comboBox1.SelectedValue.ToString(), out int parsedKamionID))
            {
                kamionID = parsedKamionID;
            }
            int? statusID = null;
            if (cboStatus.SelectedValue != null && int.TryParse(cboStatus.SelectedValue.ToString(), out int parsedStatusID))
            {
                statusID = parsedStatusID;
            }
            string dodatniOpis = string.IsNullOrWhiteSpace(txtDodatniOpis.Text) ? null : txtDodatniOpis.Text;
            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
            ins.UpdateRadniNalogDrumski(iD, AutoDan, referenca, mestoPreuzimanja, mestoUtovara, adresaUtovara, mestoIstovara, datumUtovara, datumIstovara, adresaIstovara,
                dtPreuzimanjaPraznogKont, granicniPrelaz, kontaktSpeditera, trosak, valutaID, kamionID, statusID, dodatniOpis, cena, kontaktistovara);
        }
    }
}
