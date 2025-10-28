using Microsoft.Office.Interop.Excel;
using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmRadniNalogInterniPregled : Form
    {
        string Korisnik = "";

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            // panelHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                 meniHeader.Visible = false;
                 panelHeader.Visible = true;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);



                foreach (Control control in splitContainer1.Panel2.Controls)
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
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                }
            }
            else
            {
                //  meniHeader.Visible = true;
                //  panelHeader.Visible = false;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxGradientPanel1()
        {
         

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {



                foreach (Control control in gradientPanel1.Controls)
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
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                }
            }
            else
            {
                //  meniHeader.Visible = true;
                //  panelHeader.Visible = false;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxGradientPanel2()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {



                foreach (Control control in gradientPanel2.Controls)
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
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                }
            }
            else
            {
                //  meniHeader.Visible = true;
                //  panelHeader.Visible = false;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxGradientPanel3()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {



                foreach (Control control in gradientPanel3.Controls)
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
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, FontStyle.Regular);
                    }

                }
            }
            else
            {
                //  meniHeader.Visible = true;
                //  panelHeader.Visible = false;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        public frmRadniNalogInterniPregled(string KorisnikTekuci)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            ChangeTextBox();
            ChangeTextBoxGradientPanel1();
            ChangeTextBoxGradientPanel2();
            ChangeTextBoxGradientPanel3();
            Korisnik = KorisnikTekuci;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            string DodatniAND = " AND 1=1 ";
            if (chkVOZ.Checked == true)
            {
                DodatniAND = DodatniAND + " AND VrstaManipulacije.Vozna = 1 ";
            }

            if (chkKamion.Checked == true)
            {
                DodatniAND = DodatniAND + " AND VrstaManipulacije.Kamionska = 1 ";

            }
            if (chkAdministrativna.Checked == true)
            {
                DodatniAND = DodatniAND + " AND VrstaManipulacije.Administrativna = 1 ";
            }

            if (chkFormiranTerminal.Checked == true)
            {
                DodatniAND = DodatniAND + " And FormiranTerminal = 1";


            }
           


                var select = "";
            if (cboIzdatOd.Text == "Uvoz")
            {
                select = "  SELECT rn.[ID]  ,UvozKonacna.BrojKontejnera, VrstaManipulacije.Naziv,   [Uradjen],  " +
                    " (select Top 1 Naziv from Scenario  inner join UvozKonacna  on UvozKonacna.Scenario = Scenario.ID  where UvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv, " +
                    " (select Top 1 stNapomene from UvozKonacnaNapomenePozicioniranja inner join UvozKonacna  on UvozKonacna.ID = UvozKonacnaNapomenePozicioniranja.IDNadredjena  where UvozKonacna.ID = rn.BrojOsnov order by UvozKonacnaNapomenePozicioniranja.ID DEsc) as ScenarioNapomena, " +
                    " (select Top 1 Voz.NAzivVoza as OznakaVoza from UvozKonacnaZaglavlje " +
" inner join Voz on Voz.ID = UvozKonacnaZaglavlje.IDVoza " +
"  where UvozKonacnaZaglavlje.ID = rn.PlanID) as VozDolaska ," +
" TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv, rn.[StatusIzdavanja]  ," +
 " (select Top 1 PaNaziv from Partnerji  inner join UvozKonacna  on UvozKonacna.Brodar = Partnerji.PaSifra  where UvozKonacna.ID = rn.BrojOsnov) as Brodar, " +
" [OJIzdavanja]      , o1.Naziv as Izdao " +
" ,[OJRealizacije]       ,o2.Naziv as Realizuje  ,[DatumIzdavanja]      ,[DatumRealizacije]  ,rn.[Napomena]  , " +
" UvozKonacnaVrstaManipulacije.IDVrstaManipulacije ,[Osnov] , PlanID as PlanUtovara  ," +
" [BrojOsnov] as BrojOsnov ,  VezniNalogID, [KorisnikIzdao]      ,[KorisnikZavrsio]       , uv.PaNaziv as Platilac  , " +
"  rn.Pokret,  rn.TipDokPrevoza, " +
" rn.BrojDokPrevoza, rn.TipRN, rn.BrojRN " +
" FROM [RadniNalogInterni] rn " +
" inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID  inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID  " +
" inner join UvozKonacna on UvozKonacna.ID = BrojOsnov " +
" inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije  " +
" inner join Partnerji uv on uv.PaSifra = UvozKonacnaVrstaManipulacije.Platilac " +
" Inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera  Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera  " +
           " where OJIzdavanja = " + Convert.ToInt32(cboIzdatOd.SelectedValue) + DodatniAND +
           " order by rn.ID desc";
            }
            else if (cboIzdatOd.Text == "Izvoz")
            {
                /* STA JE OVO 
                select = "  SELECT RadniNalogInterni.[ID]  ,(Select Top 1 BrojKontejnera from IzvozKonacna where IzvozKOnacna.ID =RadniNalogInterni.BrojOsnov) as BrojKontejnera , " +
  " Scenario.Naziv as Scenario, RadniNalogInterni.[StatusIzdavanja], [OJIzdavanja]      , o1.Naziv as Izdao  ,[OJRealizacije]      " +
  " ,o2.Naziv as Realizuje  ,[DatumIzdavanja]      ,[DatumRealizacije]  ,RadniNalogInterni.[Napomena]  , RadniNalogInterni.IDManipulacijaJed, " +
  " [Uradjen]  ,[Osnov], PlanID as PlanUtovara  ,[BrojOsnov] as BrojOsnov ,  VezniNalogID ,[KorisnikIzdao]      ,[KorisnikZavrsio] " +
" ,  VrstaManipulacije.Naziv,    uv.PaNaziv as Platilac  , " +
 "  TipKontenjera.Naziv as Tipkontejnera, RadniNalogInterni.Pokret, KontejnerStatus.Naziv, RadniNalogInterni.TipDokPrevoza, RadniNalogInterni.BrojDokPrevoza, RadniNalogInterni.TipRN, " +
 "  RadniNalogInterni.BrojRN   FROM [RadniNalogInterni]  " +
 " inner join IzvozKonacna on IzvozKonacna.ID = [RadniNalogInterni].BrojOsnov and RadniNalogInterni.PlanID= IzvozKonacna.IDNAdredjena " +
 " Inner join KontejnerStatus on KontejnerStatus.ID = RadniNalogInterni.StatusKontejnera   " +
 "  inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera  " +
 "  inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID  " +
 "  inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID  " +
 "  inner join VrstaManipulacije on RadniNalogInterni.IDManipulacijaJed = VrstaManipulacije.ID " +
 "  inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.ID = RadniNalogInterni.KonkretaIDUsluge  " +
 "    inner join Partnerji uv on uv.PaSifra = IzvozKonacnaVrstaManipulacije.Platilac " +
 " where OJIzdavanja = 2 AND 1=1  order by RadniNalogInterni.ID desc ";

                */

                select = "   SELECT rn.[ID]  ,IzvozKonacna.BrojKontejnera , VrstaManipulacije.Naziv, [Uradjen]  , " +
                    " (select Top 1 Naziv from Scenario  inner join IzvozKonacna  on IzvozKonacna.Scenario = Scenario.ID  where IzvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv, " +
                    " '' as ScenarioNapomena, " +
                    "   (select Top 1 Voz.NAzivVoza as OznakaVoza from IzvozKonacnaZaglavlje " +
         " inner join Voz on Voz.ID = IzvozKonacnaZaglavlje.IDVoza " +
                    "   where IzvozKonacnaZaglavlje.ID = rn.PlanID) as VozOdlaska , TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv, rn.[StatusIzdavanja]," +
                     " (select Top 1 PaNaziv from Partnerji  inner join IzvozKonacna  on IzvozKonacna.Brodar = Partnerji.PaSifra  where izvozKonacna.ID = rn.BrojOsnov) as Brodar, " +
                    " [OJIzdavanja]    " +
                    ", o1.Naziv as Izdao  ,[OJRealizacije]      ,o2.Naziv as Realizuje  ,[DatumIzdavanja]   " +
                    "   ,[DatumRealizacije]  ,rn.[Napomena] " +
      " , IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije, [Osnov], PlanID as PlanUtovara " +
      " ,[BrojOsnov] as BrojOsnov ,  VezniNalogID ,[KorisnikIzdao]      ,[KorisnikZavrsio]       , uv.PaNaziv as Platilac " +
      " , rn.Pokret,  rn.TipDokPrevoza, rn.BrojDokPrevoza," +
      " rn.TipRN, rn.BrojRN   FROM RadniNalogInterni rn " +
      " inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
      " inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
            " inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge" +
       " inner join IzvozKonacna on IzvozKonacna.ID = IzvozKonacnaVrstaManipulacije.IDNAdredjena " +
      " inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
      " inner join Partnerji uv on uv.PaSifra = IzvozKonacnaVrstaManipulacije.Platilac " +
         " Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera " +
      " inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera" +
              " where OJIzdavanja = " + Convert.ToInt32(cboIzdatOd.SelectedValue) + DodatniAND +
              " order by rn.ID desc";

            }

            else if (cboIzdatOd.Text == "Terminal")
            {

                DialogResult result = MessageBox.Show("Da li Gate in Brodar ako nije onda će biti prikazani GAte Out Brodara?", "Potvrda", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    select = "  SELECT rn.[ID] ,UvozKonacna.BrojKontejnera , VrstaManipulacije.Naziv,  [Uradjen]  , " +
                        " (select Top 1 Naziv from Scenario  inner join UvozKonacna  on UvozKonacna.Scenario = Scenario.ID  where UvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv, " +
                        " '' as ScenarioNapomena, " +
                        "  (select Top 1 Voz.NAzivVoza as OznakaVoza from UvozKonacnaZaglavlje  inner join Voz on Voz.ID = UvozKonacnaZaglavlje.IDVoza  where UvozKonacnaZaglavlje.ID = rn.PlanID) as VozDolaska , " +
                        " TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv," +
                        " rn.[StatusIzdavanja]  ," +
                           " (select Top 1 PaNaziv from Partnerji  inner join IzvozKonacna  on IzvozKonacna.Brodar = Partnerji.PaSifra  where izvozKonacna.ID = rn.BrojOsnov) as Brodar, " +
                        " [OJIzdavanja]    " +
                        "  , o1.Naziv as Izdao  ,[OJRealizacije]     " +
  "  ,o2.Naziv as Realizuje  ,[DatumIzdavanja]      ,[DatumRealizacije]  ,rn.[Napomena]  , UvozKonacnaVrstaManipulacije.IDVrstaManipulacije," +
   " [Osnov]  ,[BrojOsnov] as BrojOsnov ,  VezniNalogID, [KorisnikIzdao]      ,[KorisnikZavrsio]      " +
   "  , uv.PaNaziv as Platilac  , " +
   "  PlanID as PlanUtovara, rn.Pokret,  rn.TipDokPrevoza, rn.BrojDokPrevoza, " +
   " rn.TipRN, rn.BrojRN  FROM [RadniNalogInterni] rn " +
   " inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
   " inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
   " inner join UvozKonacna on UvozKonacna.ID = BrojOsnov " +
   " inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge " +
   " inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
   " inner join Partnerji uv on uv.PaSifra = UvozKonacnaVrstaManipulacije.Platilac " +
   " Inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
   " Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera" +
           " where OJIzdavanja = " + Convert.ToInt32(cboIzdatOd.SelectedValue) + " Order by ID desc"; ;
                }
                else if (result == DialogResult.No)
                {
                    select =
        " SELECT rn.[ID] ,IzvozKonacna.BrojKontejnera , VrstaManipulacije.Naziv,  [Uradjen]  ,     " +
        "  (select Top 1 Naziv from Scenario inner join IzvozKonacna  on IzvozKonacna.Scenario = Scenario.ID  where IzvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv, " +
          "   (select Top 1 Voz.NAzivVoza as OznakaVoza from IzvozKonacnaZaglavlje " +
         " inner join Voz on Voz.ID = IzvozKonacnaZaglavlje.IDVoza " +
                    "   where IzvozKonacnaZaglavlje.ID = rn.PlanID) as VozOdlaska , TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv," +
        "rn.[StatusIzdavanja]  ," +
           " (select Top 1 PaNaziv from Partnerji  inner join UvozKonacna  on UvozKonacna.Brodar = Partnerji.PaSifra  where UvozKonacna.ID = rn.BrojOsnov) as Brodar, " +
        " [OJIzdavanja]   " +
        " , o1.Naziv as Izdao  ,[OJRealizacije]       ,o2.Naziv as Realizuje  ,[DatumIzdavanja]      ,[DatumRealizacije]  ,RadniNalogInterni.[Napomena]  , " +
        " IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije, [Osnov]  ,[BrojOsnov] as BrojOsnov , " +
        " VezniNalogID, [KorisnikIzdao]      ,[KorisnikZavrsio]        , uv.PaNaziv as Platilac  ,  " +
        " PlanID as PlanUtovara, rn.Pokret, rn.TipDokPrevoza, rn.BrojDokPrevoza, rn.TipRN, " +
        " rn.BrojRN  FROM [RadniNalogInterni] rn  inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
        " inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID  inner join IzvozKonacna on IzvozKonacna.ID = BrojOsnov  " +
        " inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge  " +
        " inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije  " +
        " inner join Partnerji uv on uv.PaSifra = IzvozKonacnaVrstaManipulacije.Platilac  Inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera " +
        " Inner join KontejnerStatus on KontejnerStatus.ID = RadniNalogInterni.StatusKontejnera  " +
        "     where OJIzdavanja =  " + Convert.ToInt32(cboIzdatOd.SelectedValue) + "Order by ID desc";
                }
            }


              
            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            gridGroupingControl1.TableDescriptor.FrozenColumn = "ID";
            gridGroupingControl1.TableDescriptor.FrozenColumn = "BrojKontejnera";
            gridGroupingControl1.TableDescriptor.FrozenColumn = "Naziv";
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "[Uradjen] =  '1'";
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd3);


            GridConditionalFormatDescriptor gcfd31 = new GridConditionalFormatDescriptor();
            gcfd31.Appearance.AnyRecordFieldCell.BackColor = Color.Blue;
            gcfd31.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd31.Expression = "[Uradjen] =  '2'";
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd31);




            GridConditionalFormatDescriptor gcfd = new GridConditionalFormatDescriptor();
            gcfd.Appearance.AnyRecordFieldCell.BackColor = Color.Orange;
            gcfd.Appearance.AnyRecordFieldCell.TextColor = Color.Black;

            gcfd.Expression = "[StatusIzdavanja] =  'DOPUNA'";

            //To add the conditional format instances to the ConditionalFormats collection. 
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd);

            GridConditionalFormatDescriptor gcfd2 = new GridConditionalFormatDescriptor();
            gcfd2.Appearance.AnyRecordFieldCell.BackColor = Color.DarkRed;
            gcfd2.Appearance.AnyRecordFieldCell.TextColor = Color.White;

            gcfd2.Expression = "[StatusIzdavanja] =  'STORNO'";
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd2);

        
            //To add the conditional format instances to the ConditionalFormats collection. 


            this.gridGroupingControl1.TableDescriptor.Columns[0].Width = 50;
            this.gridGroupingControl1.TableDescriptor.Columns[1].Width = 120;

            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJIzdavanja");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJRealizacije");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("IDVrstaManipulacije");

            /*
            this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.CellType = "CheckBox";
            GridConditionalFormatDescriptor format1 = new GridConditionalFormatDescriptor();
            format1.Appearance.AnyRecordFieldCell.Interior = new BrushInfo(Color.FromArgb(255, 191, 52));
            format1.Appearance.AnyRecordFieldCell.TextColor = Color.White;
            format1.Expression = "[Uradjen]  =  '1'";
            format1.Name = "ConditionalFormat 1";

            // Add the descriptor to the TableDescriptor.ConditionalFormats property.
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(format1);
            */
            this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.CellType = "CheckBox";

            //To set '1' and '0' instead of "True" and "False" 
            this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.CheckBoxOptions = new GridCheckBoxCellInfo("1", "0", "", true);
            this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.ReadOnly = false;
            this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.Enabled = true;
            /*
            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(192, 255, 162));
            summaryColumnDescriptor.DataMember = "Uradjen";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "Uradjeno";
            summaryColumnDescriptor.SummaryType = Syncfusion.Grouping.SummaryType.Int32Aggregate;

            GridSummaryRowDescriptor summaryRowDescriptor = new GridSummaryRowDescriptor();
            summaryRowDescriptor.SummaryColumns.Add(summaryColumnDescriptor);
            summaryRowDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(255, 231, 162));

            this.gridGroupingControl1.TableDescriptor.SummaryRows.Add(summaryRowDescriptor);
            */

            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);


            RefreshDataGrid2PoScenariju();
        }


        private void RefreshDataGrid2PoScenariju()
        {


            ///START
            ///
        


            var select = "";
            /*
                 select = "  select Distinct RadniNalogInterni.PlanID, UvozKonacna.BrojKontejnera, Scenario.Naziv, 'Uvozni' as OJ from RadniNalogInterni " +
               "  inner join UvozKonacna on RadniNalogInterni.BrojOsnov = UvozKonacna.ID inner join Scenario on UvozKonacna.Scenario = Scenario.ID " +
               "  where Uradjen not in (1, 2) " +
                " union " +
               "  select Distinct RadniNalogInterni.PlanID, IzvozKonacna.BrojKontejnera, Scenario.Naziv , 'Izvozni' as OJ  from RadniNalogInterni " +
               "  inner join IzvozKonacna on RadniNalogInterni.BrojOsnov = IzvozKonacna.ID " +
               "  inner   join Scenario on IzvozKonacna.Scenario = Scenario.ID " +
               "  where Uradjen not in (1, 2)";
            */
            if (cboIzdatOd.Text == "Uvoz")
            {
                                            select = "    SELECT UvozKonacna.ID, [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera," +
                                            " b.PaNaziv as Brodar, p1.PaNaziv as Uvoznik,Voz.NAzivVoza as Voz ,  " +
                            " (select Top 1 Naziv from Scenario inner join UvozKonacna uv on uv.Scenario = Scenario.ID  where UvozKonacna.ID = uv.ID) as ScenarioNaziv, " +
                            " VrstaCarinskogPostupka.Naziv as CarinskiPostupak, VrstePostupakaUvoz.Naziv as PostupakSaRobom, NetoRobe, BrutoRobe,  Koleta ,TaraKontejnera, BrutoKontejnera,      " +
                            " (select Top 1 stNapomene from UvozNapomenePozicioniranja inner join UvozKonacna uv on UvozKonacna.ID = UvozNapomenePozicioniranja.IDNadredjena  where UvozKonacna.ID = uv.ID order by UvozNapomenePozicioniranja.ID DEsc) as ScenarioNapomena, " +
                             " (SELECT  STUFF((SELECT distinct   '/' + '*' + TarifniBroj + '-' + (KomercijalniNaziv) " +
                            " from UvozKonacnaNHM " +
                            "  where UvozKonacnaNHM.IDNadredjena = UvozKonacna.ID " +
                            " FOR XML PATH('')), 1, 1, '') As Skupljen2)    as NHM, " +
                            " Napomena1 as Napomena1, Brodovi.Naziv as Brod,  BrodskaTeretnica as BL, " +
                            " KontejnerskiTerminali.Naziv as T1, " +
                            " k2.Naziv as T2, k3.Naziv as T3, " +
                            " VrstaRobeADR.Naziv as ADR, n1.PaNaziv as NalogodavacZaVoz, n2.PaNaziv as Logisticar1,n3.PaNaziv as Logisticar2, " +
                            "  p3.PaNaziv as SpedicijaGranica,p2.PaNaziv as SpedicijaRTC," +
                            " uvNacinPakovanja.Naziv as NacinPakovanja, EtaBroda, p4.PaNaziv as OdredisnaSpedicija, Carinarnice.Naziv as Carinarnica, " +
                            " Email,  " +
                            " CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet , " +
                            " CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as DobijenBZ , " +
                            " Ref1 as Ref1, Ref2 as Ref2,DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda, " +
                            "  DobijeBZ as DatumBZ ,PIN,     pp1.Naziv as Dirigacija_Kontejnera_Za,   BrodskaTeretnica, " +
                            "   Ref3 as Ref3,         VrstaPregleda as InsTret, " +
                            " UvozKonacna.Napomena as Napomena2, " +
                            " MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, " +
                            " BrojPlombe1, BrojPlombe2 FROM UvozKonacna inner join Partnerji on PaSifra = VlasnikKontejnera" +
                            " inner join Partnerji p1 on p1.PaSifra = Uvoznik" +
                            " inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC" +
                            " inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica" +
                            " inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera" +
                            " inner join Carinarnice on Carinarnice.ID = UvozKonacna.OdredisnaCarina" +
                            "  inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = UvozKonacna.CarinskiPostupak" +
                            "  inner join Predefinisaneporuke on PredefinisanePoruke.ID = UvozKonacna.NapomenaZaPozicioniranje" +
                            "  inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = UvozKonacna.RLTErminali" +
                            "   inner join KontejnerskiTerminali k2 on k2.ID = UvozKonacna.RLTErminali2" +
                            "    inner join KontejnerskiTerminali k3 on k3.ID = UvozKonacna.RLTErminali3" +
                            "  inner join Partnerji n1 on n1.PaSifra = Nalogodavac1" +
                            "  inner join Partnerji n2 on n2.PaSifra = Nalogodavac2" +
                            "  inner join Partnerji n3 on n3.PaSifra = Nalogodavac3" +
                            "  inner join Partnerji b on b.PaSifra = UvozKonacna.Brodar" +
                            "  inner join  DirigacijaKontejneraZa pp1 on pp1.ID = UvozKonacna.DirigacijaKontejeraZa" +
                            "  inner join Brodovi on Brodovi.ID = UvozKonacna.NazivBroda" +
                            "  inner join VrstaRobeADR on VrstaRobeADR.ID = ADR" +
                            "  inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom" +
                            "  inner join MestaUtovara on UvozKonacna.MestoIstovara = MestaUtovara.ID" +
                            "  inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja" +
                            "  inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija" +
                            "  inner join Partnerji pv on pv.PaSifra = UvozKonacna.VlasnikKontejnera " +
                            " inner join UvozKonacnaZaglavlje on UvozKonacna.IDNAdredjeni = UvozKonacnaZaglavlje.ID " +
                             " inner Join Voz on Voz.ID = UvozKonacnaZaglavlje.IDVoza " +
                                           " order by UvozKonacna.ID desc";
            }
            else if (cboIzdatOd.Text == "Izvoz")
            {
                select = "     SELECT IzvozKonacna.ID,[BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, " +
 "     b.PaNaziv as Brodar, p1.PaNaziv as Izvoznik,Voz.NAzivVoza as Voz ,  n1.PaNaziv as Nalogodavac1, n2.PaNaziv as Nalogodavac2, n3.PaNaziv as Nalogodavac3, " +
 "     (select Top 1 Naziv from Scenario inner join IzvozKonacna uv on uv.Scenario = Scenario.ID  where IzvozKonacna.ID = uv.ID) as ScenarioNaziv, " +
 "      NetoRobe, BrutoRobe, IzvozKonacna.BrojKoleta, KontejnerskiTerminali.Naziv as Terminal1, k2.Naziv as Terminal2, k3.Naziv as Terminal3 " +
"     FROM IzvozKonacna " +
"     inner join IzvozKonacnaZaglavlje on IzvozKonacnaZaglavlje.ID = IzvozKonacna.IDNadredjena " +
"      inner join Partnerji p1 on p1.PaSifra = Izvoznik " +
"     inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera " +
"     inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = IzvozKonacna.MestoPreuzimanja " +
"     inner join KontejnerskiTerminali k2 on k2.ID = IzvozKonacna.MestoPreuzimanja2 " +
"     inner join KontejnerskiTerminali k3 on k3.ID = IzvozKonacna.MestoPreuzimanja3 " +
"     inner join Partnerji n1 on n1.PaSifra = Klijent1 " +
"     inner join Partnerji n2 on n2.PaSifra = Klijent2 " +
"     inner join Partnerji n3 on n3.PaSifra = Klijent3 " +
"     inner join Partnerji b on b.PaSifra = IzvozKonacna.Brodar " +
"     inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
"     inner Join Voz on Voz.ID = IzvozKonacnaZaglavlje.IDVoza " +
"     order by IzvozKonacna.ID desc ";
            }

                var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            var select = "  SELECT RadniNalogInterni.[ID] " +
      " ,[OJIzdavanja]      , o1.Naziv as Izdao " +
      " ,[OJRealizacije]      ,o2.Naziv as Realizuje " +
      " ,[DatumIzdavanja]      ,[DatumRealizacije] " +
      " ,RadniNalogInterni.[Napomena]      ,[Uradjen] " +
      " ,[Osnov]      ,[BrojOsnov] " +
      " ,[KorisnikIzdao]      ,[KorisnikZavrsio] " +
      " ,UvozKonacna.BrojKontejnera      , uv.PaNaziv as Uvoznik " +
      " , VL.PaSifra as Vlasnik      , TipKontenjera.Naziv as Tipkontejnera " +
      " FROM [RadniNalogInterni] " +
      " inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
      " inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
      " inner join UvozKonacna on UvozKonacna.ID = BrojOsnov " +
      " inner join Partnerji uv on uv.PaSifra = UvozKonacna.Uvoznik " +
      " inner join Partnerji VL on VL.PaSifra = UvozKonacna.VlasnikKontejnera " +
      " inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
      " where OJRealizacije = " + Convert.ToInt32(cboIzdatZa.SelectedValue) +
      " order by RadniNalogInterni.ID desc";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
            /*
            GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
            summaryColumnDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(192, 255, 162));
            summaryColumnDescriptor.DataMember = "Uradjen";
            summaryColumnDescriptor.Format = "{Sum}";
            summaryColumnDescriptor.Name = "Uradjeno";
            summaryColumnDescriptor.SummaryType = Syncfusion.Grouping.SummaryType.Int32Aggregate;

            GridSummaryRowDescriptor summaryRowDescriptor = new GridSummaryRowDescriptor();
            summaryRowDescriptor.SummaryColumns.Add(summaryColumnDescriptor);
            summaryRowDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(255, 231, 162));

            this.gridGroupingControl1.TableDescriptor.SummaryRows.Add(summaryRowDescriptor);
            */
        }

        private void frmRadniNalogInterniPregled_Load(object sender, EventArgs e)
        {
            var select8 = "  Select Distinct ID, Naziv   From OrganizacioneJedinice ";
            var s_connection8 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection8 = new SqlConnection(s_connection8);
            var c8 = new SqlConnection(s_connection8);
            var dataAdapter8 = new SqlDataAdapter(select8, c8);

            var commandBuilder8 = new SqlCommandBuilder(dataAdapter8);
            var ds8 = new DataSet();
            dataAdapter8.Fill(ds8);
            cboIzdatOd.DataSource = ds8.Tables[0];
            cboIzdatOd.DisplayMember = "Naziv";
            cboIzdatOd.ValueMember = "ID";



            var select9 = "  Select Distinct ID, Naziv   From OrganizacioneJedinice ";
            var s_connection9 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection9 = new SqlConnection(s_connection9);
            var c9 = new SqlConnection(s_connection9);
            var dataAdapter9 = new SqlDataAdapter(select9, c9);

            var commandBuilder9 = new SqlCommandBuilder(dataAdapter9);
            var ds9 = new DataSet();
            dataAdapter9.Fill(ds9);
            cboIzdatZa.DataSource = ds9.Tables[0];
            cboIzdatZa.DisplayMember = "Naziv";
            cboIzdatZa.ValueMember = "ID";
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    textBox1.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojOsnov").ToString();
                    txtNALOGID.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                 //   RadniNalogInterni.TipDokPrevoza, RadniNalogInterni.BrojDokPrevoza, RadniNalogInterni.TipRN, RadniNalogInterni.BrojRN
                    txtTip.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("TipDokPrevoza").ToString();
                    txtTipBroj.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojDokPrevoza").ToString();
                    txtRNTip.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("TipRN").ToString();
                    txtRNBroj.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojRN").ToString();
                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPrijemKamionaIzPlana pkip = new frmPrijemKamionaIzPlana(textBox1.Text);
            pkip.Show();

            /*
           *  InsertUvozKonacna ins = new InsertUvozKonacna();
            ins.PrenesiKontejnerIzPlanaNaPrijemnicu(Convert.ToInt32(textBox1.Text));
            */


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Izvoz.frmOtpremaKontejneraKamionomIzKontejnera pkip = new Izvoz.frmOtpremaKontejneraKamionomIzKontejnera(textBox1.Text);
            pkip.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {
                    InsertRadniNalogInterni ins = new InsertRadniNalogInterni();
                    ins.PromeniStatusStorno(Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()));

                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {
                    Saobracaj.Uvoz.InsertRadniNalogInterni ins = new Saobracaj.Uvoz.InsertRadniNalogInterni();
                    ins.UpdRadniNalogInterniZavrsen(Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), Korisnik);
                    //To get the cell value of particular column of selected records   
                    //  string cellValue = selectedRecord.Record.GetValue("ID").ToString();
                    // MessageBox.Show(cellValue);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {


        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {


            if (textBox1.Text != "")

            {
                DialogResult dialogResult = MessageBox.Show("Formirate otpremu platforme Kamionom", "Usluga?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //OVO JE FUNKCIONALNOST KOJA NESTAJE
                    Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion okk = new Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion(Korisnik, Convert.ToInt32(txtNALOGID.Text));
                    okk.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }

            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")

            {
                DialogResult dialogResult = MessageBox.Show("Formirate prijem platforme Kamionom", "Usluga?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    // Zadnja dva parametra CIRADA/PLATFORMA, POreklo je OJ
                    Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetIzvoz okk = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetIzvoz(Korisnik, 0, txtNALOGID.Text, 0,4);
                    okk.Show();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }

            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana();
            rd1.Show();
        }
        int VratiKonkretanIDUsluge()
        {
            int Konkretan = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select KonkretaIDUsluge from RadniNalogInterni where ID = " + txtNALOGID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Konkretan = Convert.ToInt32(dr["KonkretaIDUsluge"].ToString().TrimEnd());



            }
            con.Close();
            return Konkretan;

        }

        int VratiOJIzdavanja()
        {
            int Konkretan = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select OJIzdavanja from RadniNalogInterni where ID = " + txtNALOGID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Konkretan = Convert.ToInt32(dr["OJIzdavanja"].ToString().TrimEnd());
            }
            con.Close();
            return Konkretan;

        }

        private void OtvaranjeRobnogDokumenta()
        {
            string Forma = VratiFormu();
            if (Forma == "GATE IN VOZ")
            {
                //Jasno Prijemnica voza
                //RN 1

            }
            if (Forma == "GATE OUT KAMION" || Forma == "GATE OUT KAMION TERMINAL")
            {
                /*
                if (chkIzvoz.Checked == true)
                {
                    Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion ter2 = new Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion(KorisnikCene, txtNalogID.Text, 0, 2, txtSifra.Text);
                    ter2.Show();
                }
                else if (chkUvoz.Checked == true)
                {
                    Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion ter2 = new Saobracaj.Dokumenta.frmOtpremaKontejneraUvozKamion(Convert.ToInt32(txtSifra.Text), KorisnikCene);
                    ter2.Show();
                }
                else if (chkTerminal.Checked == true)
                {
                    Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion ter2 = new Saobracaj.Dokumenta.frmOtpremaKontejneraIzvozKamion(KorisnikCene, txtNalogID.Text, 0, 4, txtSifra.Text);
                    ter2.Show();
                }
                */


            }
            if (Forma == "GATE IN KAMION" || Forma == "GATE IN KAMION IZVOZ" || Forma == "GATE IN KAMION TERMINAL")
            {
                //frmPrijemKontejneraKamionLegetUvoz -- OTVARANJE KAMIONA IZ UVOZA


                // Ako je izvoz
                /// frmPrijemKontejneraKamionLegetIzvoz li = new frmPrijemKontejneraKamionLegetIzvoz(txtSifra.Text, 0, OJD);
                // li.Show();


              //  frmPrijemKontejneraKamionLegetIzvoz li = new frmPrijemKontejneraKamionLegetIzvoz(txtSifra.Text, 0, OJD);
              //  li.Show();
            }
            if (Forma == "GATE OUT PRETOVAR")
            {
              //  frmPrijemKontejneraKamionLegetUvoz
            }
            if (Forma == "GATE IN PRETOVAR")
            {
                //frmOtpremaKontejneraKamionomIzKontejnera
            }
            if (Forma == "GATE OUT VOZ")
            {
                //Jasno Prijemnica voza
                //RN 2


            }
        }

        int ProveriDaLiJeUradjenaPredhodnaOperacija(string Nalog)
        {
            int Uradjen = 1;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 ID, BrojOsnov, Uradjen from RadniNalogInterni where IDManipulacijaJed <> 84 and  ID < " + txtNALOGID.Text + " and ID > "  + txtNALOGID.Text + " -10 and BrojOsnov = (Select BrojOsnov from RadniNalogInterni where ID = "  + txtNALOGID.Text + ") order by ID DEsc ", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Uradjen = Convert.ToInt32(dr["Uradjen"].ToString().TrimEnd());
            }
            con.Close();
            return Uradjen;

        }

        string TipRN = "";
        int BrojRN = 0;
        int ProveriDaLijeVecGenerisanaOperacija(string Nalog)
        {
            int Uradjen = 0;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 TipRN, BrojRN from RadniNalogInterni where ID = " + txtNALOGID.Text , con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                   Uradjen = 1; 
                   TipRN =  dr["TipRN"].ToString().TrimEnd();
                   BrojRN = Convert.ToInt32(dr["BrojRN"].ToString().TrimEnd());
                if (BrojRN == 0)
                {
                    Uradjen = 0;
                }
            }
            con.Close();
            return Uradjen;

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            j = ProveriDaLijeVecGenerisanaOperacija(txtNALOGID.Text);
            if (j > 0)
            {
            MessageBox.Show("Za ovu uslugu već je generisan RN, " + TipRN + " broj :" + BrojRN);
            return;
            }
            i = ProveriDaLiJeUradjenaPredhodnaOperacija(txtNALOGID.Text);
            if (i == 0)
            {
                MessageBox.Show("Nije zavrsena predhodna usluga ne mozete generisati novu!!!");
            return;
            }
            string Forma = VratiFormu();
            int KISUsl = 0;
            int OJ = VratiOJIzdavanja();
            if (Forma == "GATE IN VOZ")
            {
                MessageBox.Show("Formirate GATE IN VOZ");
                frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana(Convert.ToInt32(txtNALOGID.Text),0,OJ);
                rd1.Show();
            }
            if (Forma == "GATE OUT KAMION" || Forma ==  "GATE OUT KAMION TERMINAL")
            {
                
                MessageBox.Show("Formirate GATE OUT KAMION Platforma");
                KISUsl = VratiKonkretanIDUsluge();
                Saobracaj.Izvoz.frmOtpremaKontejneraKamionomIzKontejnera okk = new Izvoz.frmOtpremaKontejneraKamionomIzKontejnera(textBox1.Text, txtNALOGID.Text, Korisnik, 0 ,OJ,  txtBrojKontejnera.Text);
                okk.Show();

              


            }

            if (Forma == "GATE IN KAMION" || Forma ==  "GATE IN KAMION IZVOZ" || Forma == "GATE IN KAMION TERMINAL")
            {
                if (txtRNBroj.Text == "" || txtRNBroj.Text == "0")
                {
                    //ZAdnja nula je Uvoz
                    if (OJ == 4)
                    {
                        //
                        MessageBox.Show("Formirate GATE IN Platforma TERMINAL");
                        //OVDE TREBA DA URADIM TERMINALSKI GATE IN KAMION
                        frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana(Convert.ToInt32(txtNALOGID.Text), 1, OJ);
                        rd1.Show();
                        // Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz prijemplat = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz(Korisnik, 0, txtNALOGID.Text, 0,4);
                        //  prijemplat.Show();
                    }
                    else if (OJ == 2)
                    {

                        // MessageBox.Show("Formirate Prijem kamionom Platforma Izvoz");
                        MessageBox.Show("Formirate GATE IN Platforma Izvoz");
                        frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana(Convert.ToInt32(txtNALOGID.Text), 1, OJ);
                        rd1.Show();


                        // Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetIzvoz prijemplat = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetIzvoz(Korisnik, 0, txtNALOGID.Text,0, 2);
                        // prijemplat.Show();
                    }
                    else if (OJ == 1)
                    {
                        //Prijem platforme //Uvoz SC1
                        MessageBox.Show("Formirate GATE IN Platforma Uvoz");
                        frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana(Convert.ToInt32(txtNALOGID.Text), 1, OJ);
                        rd1.Show();
                        //   Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz prijemplat = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz(Korisnik, 0, txtNALOGID.Text, 0, 1);
                        // prijemplat.Show();

                    }

                }
                else
                {
                    MessageBox.Show("Radni nalog je veće formiran");
                }

               
              

            }

            if (Forma == "GATE OUT PRETOVAR")
            {
                //
                MessageBox.Show("Formirate GATE IN kamion Cirada");
                Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz prijemplat = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz(Korisnik, 0, txtNALOGID.Text,1, 1);
                prijemplat.Show();

            }

            if (Forma == "GATE IN PRETOVAR")
            {
                MessageBox.Show("Formirate GATE OUT kamion Cirada");
                Saobracaj.Izvoz.frmOtpremaKontejneraKamionomIzKontejnera okk = new Izvoz.frmOtpremaKontejneraKamionomIzKontejnera(textBox1.Text, txtNALOGID.Text, Korisnik, 1,OJ, txtBrojKontejnera.Text);
                okk.Show();
            }

            if (Forma == "GATE OUT VOZ")
            {
                MessageBox.Show("GATE OUT VOZ");
                frmOtpremaVozaIzPlana ovizpl = new frmOtpremaVozaIzPlana(txtNALOGID.Text);
                ovizpl.Show();

                /*
                Saobracaj.Izvoz.frmOtpremaKontejneraKamionomIzKontejnera okk = new Izvoz.frmOtpremaKontejneraKamionomIzKontejnera(textBox1.Text, txtNALOGID.Text, Korisnik, 1);
                okk.Show();
                */
            }

            if (Forma == "INTERNI NALOG PRENOS")
            {
                MessageBox.Show("Formirate nalog za prenos");
                string Napomena = "";
                string Kontejner = "";
                string Usluga = "";
                try
                {
                    if (gridGroupingControl1.Table.CurrentRecord != null)
                    {
                        Napomena = gridGroupingControl1.Table.CurrentRecord.GetValue("Napomena").ToString();
                       Kontejner = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojKontejnera").ToString();
                        Usluga = gridGroupingControl1.Table.CurrentRecord.GetValue("Naziv").ToString();

                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                RadniNalozi.RN12MedjuskladisniKontejnera rnpv = new RadniNalozi.RN12MedjuskladisniKontejnera(Convert.ToInt32(txtNALOGID.Text), Kontejner, Usluga + ' ' + Napomena);
                rnpv.Show();

                /*
                Saobracaj.Izvoz.frmOtpremaKontejneraKamionomIzKontejnera okk = new Izvoz.frmOtpremaKontejneraKamionomIzKontejnera(textBox1.Text, txtNALOGID.Text, Korisnik, 1);
                okk.Show();
                */
            }

        }
        string VratiFormu()
        {
            if (txtNALOGID.Text == "")
            {

                MessageBox.Show("Obelezite bar jednu stavku voza");
                return "";
            }
            else
            {
                string formica = "";
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                SqlConnection con = new SqlConnection(s_connection);

                con.Open();

                SqlCommand cmd = new SqlCommand("select Forma from RadniNalogInterni where ID = " + txtNALOGID.Text, con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    formica = dr["Forma"].ToString().TrimEnd();



                }
                con.Close();
                return formica;
            }


        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Funkcija će obrisati sve selektovane redove i njihove pripadajuće RN i prevoze ukoliko su napravljeni?", "Potvrda", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
                {
                    foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                    {
                        Saobracaj.Uvoz.InsertRadniNalogInterni ins = new Saobracaj.Uvoz.InsertRadniNalogInterni();
                        ins.DelRadniNalogInterniSaDokumentima(Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()));
                        //To get the cell value of particular column of selected records   
                        //  string cellValue = selectedRecord.Record.GetValue("ID").ToString();
                        // MessageBox.Show(cellValue);
                    }
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            ZapisnikONepravilnosti zon = new ZapisnikONepravilnosti(txtNALOGID.Text);
            zon.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmFormiranjePlanaIzvoz fpi = new frmFormiranjePlanaIzvoz(Convert.ToInt32(txtNALOGID.Text), Convert.ToInt32(1));
            fpi.Show();
            //Kom vozu -- PLAN PRETOVARA
            //
            //Napraviti novi kontejner
            //Napraviti uslugu izvoznu
        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {

                if (gridGroupingControl2.Table.CurrentRecord != null)
                {
                    textBox1.Text = gridGroupingControl2.Table.CurrentRecord.GetValue("ID").ToString();
                    //Sve usluge za odredjeni kontejner

                    var select = "";

                    if (cboIzdatOd.Text == "Uvoz")
                    {
                        select = "  SELECT rn.[ID]  ,UvozKonacna.BrojKontejnera, VrstaManipulacije.Naziv,   [Uradjen],  " +
                                          " (select Top 1 Naziv from Scenario  inner join UvozKonacna  on UvozKonacna.Scenario = Scenario.ID  where UvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv, " +
                                          " (select Top 1 stNapomene from UvozKonacnaNapomenePozicioniranja inner join UvozKonacna  on UvozKonacna.ID = UvozKonacnaNapomenePozicioniranja.IDNadredjena  where UvozKonacna.ID = rn.BrojOsnov order by UvozKonacnaNapomenePozicioniranja.ID DEsc) as ScenarioNapomena, " +
                                          " (select Top 1 Voz.NAzivVoza as OznakaVoza from UvozKonacnaZaglavlje " +
                      " inner join Voz on Voz.ID = UvozKonacnaZaglavlje.IDVoza " +
                      "  where UvozKonacnaZaglavlje.ID = rn.PlanID) as VozDolaska ," +
                      " TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv, rn.[StatusIzdavanja]  ," +
                       " (select Top 1 PaNaziv from Partnerji  inner join UvozKonacna  on UvozKonacna.Brodar = Partnerji.PaSifra  where UvozKonacna.ID = rn.BrojOsnov) as Brodar, " +
                      " [OJIzdavanja]      , o1.Naziv as Izdao " +
                      " ,[OJRealizacije]       ,o2.Naziv as Realizuje  ,[DatumIzdavanja]      ,[DatumRealizacije]  ,rn.[Napomena]  , " +
                      " UvozKonacnaVrstaManipulacije.IDVrstaManipulacije ,[Osnov]   ," +
                      " [BrojOsnov] as BrojOsnov ,  VezniNalogID, [KorisnikIzdao]      ,[KorisnikZavrsio]       , uv.PaNaziv as Platilac  , " +
                      "  rn.Pokret,  rn.TipDokPrevoza, " +
                      " rn.BrojDokPrevoza, rn.TipRN, rn.BrojRN " +
                      " FROM [RadniNalogInterni] rn " +
                      " inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID  inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID  " +
                      " inner join UvozKonacna on UvozKonacna.ID = BrojOsnov " +
                      " inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge " +
                      " inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije  " +
                      " inner join Partnerji uv on uv.PaSifra = UvozKonacnaVrstaManipulacije.Platilac " +
                      " Inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera  Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera  " +
                                 " where OJIzdavanja = 1 and  rn.brojosnov = " + textBox1.Text +
                                 " order by rn.ID desc";

                    }

                    else if (cboIzdatOd.Text == "Izvoz")
                    {

                        select = "   SELECT rn.[ID]  ,IzvozKonacna.BrojKontejnera , VrstaManipulacije.Naziv, [Uradjen]  , " +
                  " (select Top 1 Naziv from Scenario  inner join IzvozKonacna  on IzvozKonacna.Scenario = Scenario.ID  where IzvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv, " +
                  " '' as ScenarioNapomena, " +
                  "   (select Top 1 Voz.NAzivVoza as OznakaVoza from IzvozKonacnaZaglavlje " +
       " inner join Voz on Voz.ID = IzvozKonacnaZaglavlje.IDVoza " +
                  "   where IzvozKonacnaZaglavlje.ID = rn.PlanID) as VozOdlaska , TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv, rn.[StatusIzdavanja]," +
                   " (select Top 1 PaNaziv from Partnerji  inner join IzvozKonacna  on IzvozKonacna.Brodar = Partnerji.PaSifra  where izvozKonacna.ID = rn.BrojOsnov) as Brodar, " +
                  " [OJIzdavanja]    " +
                  ", o1.Naziv as Izdao  ,[OJRealizacije]      ,o2.Naziv as Realizuje  ,[DatumIzdavanja]   " +
                  "   ,[DatumRealizacije]  ,rn.[Napomena] " +
    " , IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije, [Osnov], PlanID as PlanUtovara " +
    " ,[BrojOsnov] as BrojOsnov ,  VezniNalogID ,[KorisnikIzdao]      ,[KorisnikZavrsio]       , uv.PaNaziv as Platilac " +
    " , rn.Pokret,  rn.TipDokPrevoza, rn.BrojDokPrevoza," +
    " rn.TipRN, rn.BrojRN   FROM RadniNalogInterni rn " +
    " inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
    " inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
          " inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge" +
     " inner join IzvozKonacna on IzvozKonacna.ID = IzvozKonacnaVrstaManipulacije.IDNAdredjena " +
    " inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
    " inner join Partnerji uv on uv.PaSifra = IzvozKonacnaVrstaManipulacije.Platilac " +
       " Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera " +
    " inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKontejnera" +
            " where OJIzdavanja = 2 and rn.brojosnov = " + textBox1.Text  +
            " order by rn.ID desc";
                    }




                        var s_connection = Sifarnici.frmLogovanje.connectionString;
                    SqlConnection myConnection = new SqlConnection(s_connection);
                    var c = new SqlConnection(s_connection);
                    var dataAdapter = new SqlDataAdapter(select, c);

                    var commandBuilder = new SqlCommandBuilder(dataAdapter);
                    var ds = new DataSet();
                    dataAdapter.Fill(ds);
                    gridGroupingControl1.DataSource = ds.Tables[0];
                    gridGroupingControl1.ShowGroupDropArea = true;
                    gridGroupingControl1.TableDescriptor.FrozenColumn = "ID";
                    gridGroupingControl1.TableDescriptor.FrozenColumn = "BrojKontejnera";
                    gridGroupingControl1.TableDescriptor.FrozenColumn = "Naziv";
                    this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

                    GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
                    gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
                    gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

                    gcfd3.Expression = "[Uradjen] =  '1'";
                    this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd3);


                    GridConditionalFormatDescriptor gcfd31 = new GridConditionalFormatDescriptor();
                    gcfd31.Appearance.AnyRecordFieldCell.BackColor = Color.Blue;
                    gcfd31.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

                    gcfd31.Expression = "[Uradjen] =  '2'";
                    this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd31);




                    GridConditionalFormatDescriptor gcfd = new GridConditionalFormatDescriptor();
                    gcfd.Appearance.AnyRecordFieldCell.BackColor = Color.Orange;
                    gcfd.Appearance.AnyRecordFieldCell.TextColor = Color.Black;

                    gcfd.Expression = "[StatusIzdavanja] =  'DOPUNA'";

                    //To add the conditional format instances to the ConditionalFormats collection. 
                    this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd);

                    GridConditionalFormatDescriptor gcfd2 = new GridConditionalFormatDescriptor();
                    gcfd2.Appearance.AnyRecordFieldCell.BackColor = Color.DarkRed;
                    gcfd2.Appearance.AnyRecordFieldCell.TextColor = Color.White;

                    gcfd2.Expression = "[StatusIzdavanja] =  'STORNO'";
                    this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd2);


                    //To add the conditional format instances to the ConditionalFormats collection. 


                    this.gridGroupingControl1.TableDescriptor.Columns[0].Width = 50;
                    this.gridGroupingControl1.TableDescriptor.Columns[1].Width = 120;

                    this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJIzdavanja");
                    this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("OJRealizacije");
                    this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("IDVrstaManipulacije");

                    /*
                    this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.CellType = "CheckBox";
                    GridConditionalFormatDescriptor format1 = new GridConditionalFormatDescriptor();
                    format1.Appearance.AnyRecordFieldCell.Interior = new BrushInfo(Color.FromArgb(255, 191, 52));
                    format1.Appearance.AnyRecordFieldCell.TextColor = Color.White;
                    format1.Expression = "[Uradjen]  =  '1'";
                    format1.Name = "ConditionalFormat 1";

                    // Add the descriptor to the TableDescriptor.ConditionalFormats property.
                    this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(format1);
                    */
                    this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.CellType = "CheckBox";

                    //To set '1' and '0' instead of "True" and "False" 
                    this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.CheckBoxOptions = new GridCheckBoxCellInfo("1", "0", "", true);
                    this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.ReadOnly = false;
                    this.gridGroupingControl1.TableDescriptor.Columns["Uradjen"].Appearance.AnyRecordFieldCell.Enabled = true;
                    /*
                    GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                    summaryColumnDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(192, 255, 162));
                    summaryColumnDescriptor.DataMember = "Uradjen";
                    summaryColumnDescriptor.Format = "{Sum}";
                    summaryColumnDescriptor.Name = "Uradjeno";
                    summaryColumnDescriptor.SummaryType = Syncfusion.Grouping.SummaryType.Int32Aggregate;

                    GridSummaryRowDescriptor summaryRowDescriptor = new GridSummaryRowDescriptor();
                    summaryRowDescriptor.SummaryColumns.Add(summaryColumnDescriptor);
                    summaryRowDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(255, 231, 162));

                    this.gridGroupingControl1.TableDescriptor.SummaryRows.Add(summaryRowDescriptor);
                    */

                    foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
                    {
                        column.AllowFilter = true;
                    }

                    GridExcelFilter gridExcelFilter = new GridExcelFilter();

                    //Wiring GridExcelFilter to GridGroupingControl
                    gridExcelFilter.WireGrid(this.gridGroupingControl1);

                    GridDynamicFilter dynamicFilter = new GridDynamicFilter();
                    dynamicFilter.WireGrid(this.gridGroupingControl1);


                    RefreshDataGrid2PoScenariju();

                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtRNTip.Text == "RN1")
            { 
             RN1PrijemVoza rn1o = new RN1PrijemVoza(Convert.ToInt16(txtRNBroj.Text));
                rn1o.Show();
            }
            if (txtRNTip.Text == "RN2")
            {
                RN2OtpremaVoza rn1o = new RN2OtpremaVoza(Convert.ToInt16(txtRNBroj.Text));
                rn1o.Show();
            }


            if (txtRNTip.Text == "RN4")
            {
                RN4PrijemPlatforme rn1o = new RN4PrijemPlatforme(Convert.ToInt16(txtRNBroj.Text));
                rn1o.Show();
            } 
            if (txtRNTip.Text == "RN5")
            {
                RN5PrijemPlatforme2 rn1o = new RN5PrijemPlatforme2(Convert.ToInt16(txtRNBroj.Text));
                rn1o.Show();
            }

            if (txtRNTip.Text == "RN6")
            {
                RN6OtpremaPlatforme rn1o = new RN6OtpremaPlatforme(Convert.ToInt16(txtRNBroj.Text));
                rn1o.Show();
            }

            if (txtRNTip.Text == "RN7")
            {
                RN7OtpremaPlatforme2 rn1o = new RN7OtpremaPlatforme2(Convert.ToInt16(txtRNBroj.Text));
                rn1o.Show();
            }




            if (txtRNTip.Text == "RN12")
            {
                RN12MedjuskladisniKontejnera rn1o = new RN12MedjuskladisniKontejnera(Convert.ToInt16(txtRNBroj.Text));
                rn1o.Show();
            }

     
        }
    }
}
