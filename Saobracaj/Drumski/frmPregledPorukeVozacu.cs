using Syncfusion.Windows.Forms;
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

namespace Saobracaj.Drumski
{
    public partial class frmPregledPorukeVozacu: Form
    {
        private DataTable detaljnaTabela;
        int? radniNalogDrumskiID = 0;
        private string poruka = "";

        public frmPregledPorukeVozacu(DataTable table, int? RadniNalogDrumskiID)
        {
            InitializeComponent();
            ChangeTextBox();
            detaljnaTabela = table;
            radniNalogDrumskiID = RadniNalogDrumskiID;
            //this.Padding = new Padding(20);
            //this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.ControlBox = true;          // prikazuje X dugme
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowIcon = false;           // sakriva ikonicu
            this.Text = "Pregled poruke za slanje vozaču za skeniranje dokumenata";                  // ako ne želiš naslov
        }

        private void ChangeTextBox()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;

                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
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

                //    if (control is System.Windows.Forms.TextBox textBox)
                //    {

                //        textBox.BackColor = Color.White;// Example: Change background color
                //        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                //        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //        // Example: Change font
                //    }


                //    if (control is System.Windows.Forms.Label label)
                //    {
                //        // Change properties here
                //        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                //        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                //        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                //    }

                //    if (control is DateTimePicker dtp)
                //    {
                //        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                //        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.CheckBdatagridviewox chk)
                //    {
                //        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                //        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.ListBox lb)
                //    {
                //        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                //        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.ComboBox cb)
                //    {
                //        cb.ForeColor = Color.FromArgb(51, 51, 54);
                //        cb.BackColor = Color.White;// Example: Change background color
                //        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }

                //    if (control is System.Windows.Forms.NumericUpDown nu)
                //    {
                //        nu.ForeColor = Color.FromArgb(51, 51, 54);
                //        nu.BackColor = Color.White;// Example: Change background color
                //        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                //    }
                //}
            }
            else
            {

                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }



            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                foreach (System.Windows.Forms.Control control in Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPregledPorukeVozacu_Load(object sender, EventArgs e)
        {
            DataRow prviRed = detaljnaTabela.Rows[0];

            int Uvoz = prviRed["Uvoz"] != DBNull.Value ? Convert.ToInt32(prviRed["Uvoz"].ToString()) : -1;

            string datumUtovara = "";
            string adresaUtovara = "";
            string datumPreuzimanjaPraznog = "";
            string datumIstovara = "";
            string mestoPreuzimanjaPraznog = "";
            string polazncarinarnica = "";
            string odredisnacarinarnica = "";
            string mestoIstovara = "";
            string mestoUtovara = "";
            string adresaIstovara = "";
            string kontaktOsobaUtovarIstovar = "";
            string odredisnaSpedicijaKontakt = "";
            string napomenaZaPozicioniranje = "";
            string dodatniOpis = "";
            string nalogID = "";
            string polaznaSpedicija = "";
            string polaznaSpedicijaKontakt = "";
            string datumSlanjaPoruke = "";
            string brojKontejnera = "";
           
            datumPreuzimanjaPraznog = prviRed["DtPreuzimanjaPraznogKontejnera"] != DBNull.Value ? prviRed["DtPreuzimanjaPraznogKontejnera"].ToString() : "";
            mestoPreuzimanjaPraznog = prviRed["MestoPreuzimanjaKontejnera"] != DBNull.Value ? prviRed["MestoPreuzimanjaKontejnera"].ToString() : "";
            polazncarinarnica = prviRed["polaznaCarinarnica"] != DBNull.Value ? prviRed["polaznaCarinarnica"].ToString() : "";
            datumUtovara = prviRed["DatumUtovara"] != DBNull.Value ? prviRed["DatumUtovara"].ToString() : "";
            mestoUtovara = prviRed["MestoUtovara"] != DBNull.Value ? prviRed["MestoUtovara"].ToString() : "";
            mestoIstovara = prviRed["MestoIstovara"] != DBNull.Value ? prviRed["MestoIstovara"].ToString() : "";
            adresaIstovara = prviRed["AdresaIstovara"] != DBNull.Value ? prviRed["AdresaIstovara"].ToString() : "";
           
            kontaktOsobaUtovarIstovar = prviRed["KontaktOsobaUtovarIstovar"] != DBNull.Value ? prviRed["KontaktOsobaUtovarIstovar"].ToString() : "";
            adresaUtovara = prviRed["AdresaUtovara"] != DBNull.Value ? prviRed["AdresaUtovara"].ToString() : "";
            napomenaZaPozicioniranje = prviRed["NapomenaZaPozicioniranje"] != DBNull.Value ? prviRed["NapomenaZaPozicioniranje"].ToString() : "";
            datumIstovara = prviRed["DatumUtovara"] != DBNull.Value ? prviRed["DatumUtovara"].ToString() : "";
            odredisnacarinarnica = prviRed["OdredisnaCarina"] != DBNull.Value ? prviRed["OdredisnaCarina"].ToString() : "";
            odredisnaSpedicijaKontakt = prviRed["OdredisnaSpedicijaKontakt"] != DBNull.Value ? prviRed["OdredisnaSpedicijaKontakt"].ToString() : "";
            dodatniOpis = prviRed["DodatniOpis"] != DBNull.Value ? prviRed["DodatniOpis"].ToString() : "";
            nalogID = prviRed["NalogID"] != DBNull.Value ? prviRed["NalogID"].ToString() : "";
            polaznaSpedicija = prviRed["PolaznaSpedicija"] != DBNull.Value ? prviRed["PolaznaSpedicija"].ToString() : "";
            polaznaSpedicijaKontakt = prviRed["PolaznaSpedicijaKontakt"] != DBNull.Value ? prviRed["PolaznaSpedicijaKontakt"].ToString() : "";
          
            string kontejner1 = prviRed["BrojKontejnera"] != DBNull.Value ? prviRed["BrojKontejnera"].ToString() : "";
            string kontejner2 = prviRed["BrojKontejnera2"] != DBNull.Value ? prviRed["BrojKontejnera2"].ToString() : "";
            datumSlanjaPoruke = prviRed["DatumKreiranjaTokena"] != DBNull.Value ? prviRed["DatumKreiranjaTokena"].ToString() : ""; 



            if (!string.IsNullOrWhiteSpace(kontejner1) && !string.IsNullOrWhiteSpace(kontejner2))
                brojKontejnera = $"{kontejner1}, {kontejner2}";
            else
                brojKontejnera = !string.IsNullOrWhiteSpace(kontejner1) ? kontejner1 : kontejner2;

            //izvoz
            if (Uvoz == 0 || Uvoz == 3 || Uvoz == 5)
            {                                                                
                 poruka = $"Kontejner {brojKontejnera} preuzimate {datumPreuzimanjaPraznog} na {mestoPreuzimanjaPraznog}\n" +
                         $"Utovarate {datumUtovara} u {mestoUtovara}, adresa utovara {adresaUtovara} {kontaktOsobaUtovarIstovar} \n" +
                         $"Kontakt na utovaru: {kontaktOsobaUtovarIstovar} \n" +
                         $"Izvozno carinjenje radite u {polazncarinarnica} \n" +
                         $"Pun kontejner spustate na {mestoIstovara} , {adresaIstovara} \n" +
                         $"\n" +
                         $"Posebni uslovi transporta:{napomenaZaPozicioniranje} {dodatniOpis} \n" +
                         $"Broj naloga: {nalogID}" + $"\n " +
                         $"Poslednje slanje viber poruke: {datumSlanjaPoruke}";

            }
            //uvoz
            else if (Uvoz == 1 || Uvoz == 2 || Uvoz == 4)
            {               
                poruka =   $"Kontejner {brojKontejnera} preuzimate {datumUtovara} na {mestoUtovara}\n" +
                           $"Propratnicu Vam radi {polazncarinarnica} {polaznaSpedicija}\n" +
                           $"Javite se {datumIstovara} na {odredisnacarinarnica} {odredisnaSpedicijaKontakt} \n" +
                           $"Kontejner istovarate {mestoIstovara}, {adresaIstovara}, {kontaktOsobaUtovarIstovar} \n" +
                           $"\n" +
                           $"Posebni uslovi transporta: {napomenaZaPozicioniranje}\n" +
                           $"\n " +
                           $"Poslednje slanje viber poruke: {datumSlanjaPoruke}";
            }

            lblPoruka.Text = poruka;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertRadniNalogDrumski ins = new InsertRadniNalogDrumski();
            string token = ins.SnimiToken(radniNalogDrumskiID);
            int temp = PostaviVrednostZaposleni();
            int? NajavuPoslaoKorisnik = temp == 0 ? (int?)null : temp;
            ins.PoslateInstrukcije(radniNalogDrumskiID, NajavuPoslaoKorisnik);

            linkLabel1.Text = $"http://legetvozaci.hibint.rs/upload/{token}";
            linkLabel1.Visible = true;

        }
        private int PostaviVrednostZaposleni()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);
            int ulogovaniZaposleniID = 0;

            con.Open();

            SqlCommand cmd = new SqlCommand(" Select  k.DeSifra as ID, (RTrim(DeIme) + ' ' + Rtrim(DePriimek)) as Zaposleni " +
                                            " FROM Korisnici k " +
                                            " INNER JOIN Delavci d ON k.DeSifra = d.DeSifra " +
                                            " where Trim(Korisnik) like '" + Saobracaj.Sifarnici.frmLogovanje.user.Trim() + "'", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["ID"] != DBNull.Value)
                    ulogovaniZaposleniID = Convert.ToInt32(dr["ID"].ToString());
            }
            return ulogovaniZaposleniID;

        }
    }
}
