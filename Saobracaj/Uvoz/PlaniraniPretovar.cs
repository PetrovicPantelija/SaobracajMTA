using Microsoft.ReportingServices.Diagnostics.Internal;
using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
using Saobracaj.Sifarnici;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;


using System.Configuration;
using System.Windows;
using Syncfusion.Windows.Forms;

using Syncfusion.GridHelperClasses;
using Saobracaj.Tehnologija;


namespace Saobracaj.Uvoz
{
    public partial class PlaniraniPretovar : Form
    {
        string korisnik = frmLogovanje.user;
        string s_connection = Sifarnici.frmLogovanje.connectionString;
        int OJ = 1;
        int SelektovanaPrijemnica = 0;
        int SelektovanaOtpremnica = 0;

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
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
             
                panelHeader.Visible = true;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);



                foreach (Control control in this.Controls)
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
                //  meniHeader.Visible = true;
                //  panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxSplitKontejner1()
        {
         

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
              
                foreach (Control control in tabSplitterContainer1.Controls)
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
                        textBox.Font = new System.Drawing.Font("Helvetica", 9);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9); // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                }
            }
            else
            {
                //  meniHeader.Visible = true;
                //  panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxSplitKontejner2()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {

                foreach (Control control in tabSplitterContainer1.Controls)
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
                        textBox.Font = new System.Drawing.Font("Helvetica", 9);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                }
            }
            else
            {
                //  meniHeader.Visible = true;
                //  panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxPanel2()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {

                foreach (Control control in panel2.Controls)
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
                        textBox.Font = new System.Drawing.Font("Helvetica", 9);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                }
            }
            else
            {
                //  meniHeader.Visible = true;
                //  panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        private void ChangeTextBoxPanel3()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {

                foreach (Control control in panel3.Controls)
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
                        textBox.Font = new System.Drawing.Font("Helvetica", 9);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }
                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9);
                    }
                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9);
                    }

                }
            }
            else
            {
                //  meniHeader.Visible = true;
                //  panelHeader.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        public PlaniraniPretovar()
        {
            InitializeComponent();
            ChangeTextBox();
            ChangeTextBoxSplitKontejner1();
            ChangeTextBoxSplitKontejner2();
            ChangeTextBoxPanel2();
            ChangeTextBoxPanel3();
        }
        private void Fill()
        {
            string DodatniAND = " ";



           
                DodatniAND = DodatniAND + " AND rn.Forma in ('GATE IN PRETOVAR' , 'GATE OUT PRETOVAR', 'PRETOVAR')";

           

            var select = "Select * from (SELECT rn.[ID]  ,UvozKonacna.BrojKontejnera,VrstaManipulacije.Naziv,[Uradjen]  , " +
                 " (select Top 1 Naziv from Scenario  inner join UvozKonacna  on UvozKonacna.Scenario = Scenario.ID  where UvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv, " +
                "CASE WHEN UvozKonacna.PotvrdioKlijent = 0 THEN 'ČEKA SE'    WHEN UvozKonacna.PotvrdioKlijent = 1 THEN 'ODOBREN POČETAK – BEZ DODATNIH INSTRUKCIJA ' ELSE 'ODOBREN POČETAK – UZ DODATNE INSTRUKCIJE' END AS PotvrdioKlijent, " +
                "CASE WHEN UvozKonacna.UradilaCarina = 0 THEN 'ČEKA SE'    ELSE 'Potvrđen carinski postupak'END AS UradilaCarina, " +
                " TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv as KS ,  " +  
" (select Top 1 OperacijaUradjena from KontejnerTekuce inner join UvozKonacna  on UvozKonacna.BrojKontejnera = KontejnerTekuce.Kontejner  where UvozKonacna.ID = rn.BrojOsnov) as ZadnjaOperacija, " +
" (select Top 1 Operacija from KontejnerTekuceOperacije inner join UvozKonacna  on UvozKonacna.BrojKontejnera = KontejnerTekuceOperacije.Kontejner  where UvozKonacna.ID = rn.BrojOsnov order by KontejnerTekuceOperacije.ID DESC) as LogOperacija, " +
" rn.BrojDokPrevoza, rn.BrojRN, " +
" rn.[StatusIzdavanja]  , [OJIzdavanja], o1.Naziv as Izdao, " +
" [OJRealizacije], o2.Naziv as Realizuje,  [DatumIzdavanja],[DatumRealizacije]  ,rn.[Napomena]  ,  UvozKonacnaVrstaManipulacije.IDVrstaManipulacije, " +
" [Osnov]  ,[BrojOsnov]  ,[KorisnikIzdao] ,  [KorisnikZavrsio]  ,uv.PaNaziv as Platilac  , " +
" PlanID as PlanUtovara, rn.Pokret FROM[RadniNalogInterni] rn " +
" inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
" inner join UvozKonacna on UvozKonacna.ID = BrojOsnov  inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
" inner join Partnerji uv on uv.PaSifra = UvozKonacnaVrstaManipulacije.Platilac " +
" Inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
" Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera " +
" where 1=1  " + DodatniAND +
" union " +
" SELECT rn.[ID] , IzvozKonacna.BrojKontejnera,VrstaManipulacije.Naziv,[Uradjen]," +
 " (select Top 1 Naziv from Scenario  inner join IzvozKonacna  on IzvozKonacna.Scenario = Scenario.ID  where IzvozKonacna.ID = rn.BrojOsnov) as ScenarioNaziv, " +
" '0' as PotvrdioKlijent,  '0' as UradilaCarina, TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv as KS, " +
" (select Top 1 OperacijaUradjena from KontejnerTekuce inner join IzvozKonacna  on IzvozKonacna.BrojKontejnera = KontejnerTekuce.Kontejner  where IzvozKonacna.ID = rn.BrojOsnov) as ZadnjaOperacija, " +
" (select Top 1 Operacija from KontejnerTekuceOperacije inner join IzvozKonacna  on IzvozKonacna.BrojKontejnera = KontejnerTekuceOperacije.Kontejner  where IzvozKonacna.ID = rn.BrojOsnov order by KontejnerTekuceOperacije.ID DESC) as LogOperacija, " +
"  rn.BrojDokPrevoza, " +
" rn.BrojRN , rn.[StatusIzdavanja]  , [OJIzdavanja], o1.Naziv as Izdao, " +
" [OJRealizacije], o2.Naziv as Realizuje,  [DatumIzdavanja],[DatumRealizacije]  ,rn.[Napomena]  , " +
" IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije, [Osnov]  ,[BrojOsnov]  , " +
" [KorisnikIzdao] ,   [KorisnikZavrsio]  ,uv.PaNaziv as Platilac  , " +
" PlanID as PlanUtovara, rn.Pokret FROM[RadniNalogInterni] rn inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
" inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID  inner join IzvozKonacna on IzvozKonacna.ID = BrojOsnov " +
" inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
" inner join Partnerji uv on uv.PaSifra = IzvozKonacnaVrstaManipulacije.Platilac " +
" Inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKOntejnera " +
" Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera " +
" where 1=1  " +DodatniAND + " ) as t1 " +
" order by t1.[ID] desc";
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "[Uradjen] =  '1'";
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd3);


            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);


        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(s_connection);
            var sklad = "select ID,naziv from Skladista";
            var daSklad = new SqlDataAdapter(sklad, conn);
            var dsSklad = new System.Data.DataSet();
            daSklad.Fill(dsSklad);
            cboSaSklad.DataSource = dsSklad.Tables[0];
            cboSaSklad.DisplayMember = "Naziv";
            cboSaSklad.ValueMember = "ID";

            var pozicija = "Select Id,Opis from Pozicija";
            var daPoz = new SqlDataAdapter(pozicija, conn);
            var dsPoz = new System.Data.DataSet();
            daPoz.Fill(dsPoz);
            cboSaPoz.DataSource = dsPoz.Tables[0];
            cboSaPoz.DisplayMember = "Opis";
            cboSaPoz.ValueMember = "ID";
        }
        private void PlaniraniPretovar_Load(object sender, EventArgs e)
        {
            Fill();
            FillCombo();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        string Usluga;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        private void FillDodatneUSluge(string RN)
        {

            var select = "";

            if (txtOsnov.Text == "")
            { return; }

            if (chkIzvoz.Checked == true)
            {
                select = " Select RadniNalogInterni.ID as ID,  " +
 " RadniNalogInterni.IDManipulacijaJed, VrstaManipulacije.Naziv, Uradjen , RadniNalogInterni.KonkretaIDUsluge from RadniNalogInterni " +
 " inner join IzvozKonacna on IzvozKonacna.ID = RadniNalogInterni.BrojOsnov " +
 " inner join VrstaManipulacije on VrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJed " +
 " where RadniNalogInterni.OJIzdavanja = 2  and VrstaManipulacije.Dodatna = 1 " +
 " and RadniNalogInterni.BrojOsnov = " + txtOsnov.Text;
            }
            else if (chkUvoz.Checked == true)
            {
               select =  " Select RadniNalogInterni.ID as ID, RadniNalogInterni.IDManipulacijaJed, VrstaManipulacije.Naziv,  RadniNalogInterni.KonkretaIDUsluge from RadniNalogInterni " +
              " inner join UvozKonacna on UvozKOnacna.ID = RadniNalogInterni.BrojOsnov " +
"  inner join VrstaManipulacije on VrstaManipulacije.ID = RadniNalogInterni.IDManipulacijaJed " +
"  where RadniNalogInterni.OJIzdavanja = 1 and VrstaManipulacije.Dodatna = 1 " +
"  and RadniNalogInterni.BrojOSnov =  " + txtOsnov.Text;
            }

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView7.ReadOnly = false;
            dataGridView7.DataSource = ds.Tables[0];


           // dataGridView7.BorderStyle = BorderStyle.None;
            dataGridView7.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView7.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView7.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView7.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView7.BackgroundColor = Color.White;

            dataGridView7.EnableHeadersVisualStyles = false;
            dataGridView7.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView7.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView7.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView7.Columns[0];
            dataGridView7.Columns[0].HeaderText = "ID";
            dataGridView7.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView7.Columns[1];
            dataGridView7.Columns[1].HeaderText = "USL ID";
            dataGridView7.Columns[1].Width = 40;

            DataGridViewColumn column3 = dataGridView7.Columns[2];
            dataGridView7.Columns[2].HeaderText = "USLUGA";
            dataGridView7.Columns[2].Width = 350;

         

        }

        private void FillDodatnePrijemnice(string NalogID)
        {

            var select = "";

            if (NalogID == "")
            {
                return;
            }

           
                select = " select  Distinct PrStDokumenta, BrojKontejnera from Promet where VrstaDokumenta = 'PRI' and NalogID =  " + NalogID;
           

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = false;
            dataGridView1.DataSource = ds.Tables[0];


            // dataGridView7.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "Postojeca prijemnica";
            dataGridView1.Columns[0].Width = 100;

            



        }

        private void FillDodatneOtpremnice(string NalogID)
        {

            var select = "";

            if (txtOsnov.Text == "")
            { return; }
            select = " Select  Distinct PrStDokumenta,  BrojKontejnera from Promet where VrstaDokumenta = 'OTP' and NalogID =  " + NalogID;


            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];


            // dataGridView7.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "Postojeća otpremnica";
            dataGridView2.Columns[0].Width = 100;





        }


        private void gridGroupingControl1_TableControlCellClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
           

            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtID.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                    txtKontejner.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojKontejnera").ToString();
                    txtPlanUtovara.Text= gridGroupingControl1.Table.CurrentRecord.GetValue("PlanUtovara").ToString();
                    txtOsnov.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojOsnov").ToString();
                    Usluga= gridGroupingControl1.Table.CurrentRecord.GetValue("IDVrstaManipulacije").ToString();
                    txtPrijemID.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojDokPrevoza").ToString();
                    txtRN.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojRN").ToString();
                   
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            OJ = VratiOJIzdavanja();
            if (OJ == 1)
            { 
                chkUvoz.Checked = true;
                chkIzvoz.Checked = false;
                panel2.Visible = true;
                panel3.Visible = false;
                panel1.Visible = false;
            }
            else if (OJ == 2)
            {
                chkUvoz.Checked = false;
                chkIzvoz.Checked = true;
                panel3.Visible = true;
                panel2.Visible = false;
                panel1.Visible = false;
            }
            if (txtKontejner.Text.StartsWith("ROB-") )
            {
                panel3.Visible = false;
                panel2.Visible = false;
                panel1.Visible = true;
            }
            FillDodatneUSluge(txtRN.Text);
            FillDodatnePrijemnice(txtID.Text);
            FillDodatneOtpremnice(txtID.Text);
    

        }

        int VratiOJIzdavanja()
        {
            int Konkretan = 0;
            if (txtID.Text == "")
            {
               return 0;
            }
        
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select OJIzdavanja from RadniNalogInterni where ID = " + txtID.Text, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Konkretan = Convert.ToInt32(dr["OJIzdavanja"].ToString().TrimEnd());
            }
            con.Close();
            return Konkretan;

        }
        string BrojPlombe;
        int Brodar,Spedicija,VrstaPregleda,CariniskiPostupak;
        private void VratiUvozKonacna()
        {
            SqlConnection conn = new SqlConnection(s_connection);

            conn.Open();

            SqlCommand cmd = new SqlCommand("Select BrojPlombe1,Brodar,SpedicijaRTC,VrstaPregleda,CarinskiPostupak From UvozKonacna Where ID="+Convert.ToInt32(txtOsnov.Text),conn);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                BrojPlombe = dr["BrojPlombe1"].ToString();
                Brodar = Convert.ToInt32(dr["Brodar"].ToString());
                Spedicija = Convert.ToInt32(dr["SpedicijaRTC"].ToString());
                VrstaPregleda = Convert.ToInt32(dr["VrstaPregleda"].ToString());
                CariniskiPostupak = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
            }

            conn.Close();
            dr.Close();
        }

        private void VratiIzvozKonacna()
        {
            SqlConnection conn = new SqlConnection(s_connection);

            conn.Open();

            SqlCommand cmd = new SqlCommand("Select BrodskaPlomba,Brodar,0,0,0 From IzvozKonacna  Where ID=" + Convert.ToInt32(txtOsnov.Text), conn);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                BrojPlombe = dr["BrodskaPlomba"].ToString();
                Brodar = Convert.ToInt32(dr["Brodar"].ToString());
                Spedicija = Convert.ToInt32(0);
                VrstaPregleda = Convert.ToInt32(0);
                CariniskiPostupak = Convert.ToInt32(0);
            }

            conn.Close();
            dr.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
                    {
                Prijemnica frm = new Prijemnica(Convert.ToInt32(txtID.Text), txtKontejner.Text.ToString().TrimEnd(), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue));
                frm.Show();

            }
            
        }

        private void btnOtpremnica_Click(object sender, EventArgs e)
        {
           

        }
        int OtpremaKontejneraID;
        private void VratiPodatkeMax()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select Max([ID]) as ID from OtpremaKontejnera", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                OtpremaKontejneraID = Convert.ToInt32(dr["ID"].ToString());
            }

            con.Close();
        }

        private void btnOtpremnicaRoba_Click(object sender, EventArgs e)
        {
            RadniNalozi.Otpremnica frm = new Otpremnica(Convert.ToInt32(txtID.Text), txtKontejner.Text.ToString().TrimEnd(),txtReg.Text,txtVozac.Text);
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            up.ZatvoriSkladisninuKontejnera(Convert.ToInt32(txtID.Text), korisnik);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            up.PokreniSkladisninuKontejnera(Convert.ToInt32(txtID.Text), korisnik);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Fill();
        }

        int PrijemID;
        private void VratiPrijemID()
        {
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Max(ID) from PrijemKontejneraVoz",conn);
            SqlDataReader dr=cmd.ExecuteReader();
            while(dr.Read())
            {
                PrijemID = Convert.ToInt32(dr[0].ToString());
            }
            conn.Close();
            dr.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Otvaranje prijema CIrada Uvoz
            //  Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz ter2 = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz(korisnik, 0, txtID.Text, 1,1);
            if (chkUvoz.Checked == true)
            {
                Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz ter2 = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz(Convert.ToInt32(txtPrijemID.Text), korisnik, 0);
                ter2.Show();

            }

            if (chkIzvoz.Checked == true)
            {
                Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetIzvoz ter2 = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetIzvoz(txtPrijemID.Text, 0, 2);
                ter2.Show();

            }

            return;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int Ciradatmp = 0;
            int ModulPorekla = 0;
            if (chkUvoz.Checked == true)
            {
                ModulPorekla = 1;
            }
            else if (chkIzvoz.Checked == true)
            {
                ModulPorekla = 2;
            }
            if (chkCirada.Checked == true)
                Ciradatmp = 1;

            Dokumenta.InsertOtprema ins = new Dokumenta.InsertOtprema();
            ins.InsertOtp(Convert.ToDateTime(dateTimePicker1.Value), 0, 0, txtReg.Text.ToString().TrimEnd(), txtVozac.Text.ToString().TrimEnd(), Convert.ToDateTime(dateTimePicker1.MinDate), 0, Convert.ToDateTime(DateTime.Now), korisnik, txtNapomena.Text, 0, 0, Ciradatmp, ModulPorekla);
            VratiPodatkeMax();
            if (chkUvoz.Checked == true)
            {
                InsertIzvozKonacna ins2 = new InsertIzvozKonacna();
                ins2.PrenesiKontejnerUOtpremuKamionomUvoz(Convert.ToInt32(txtOsnov.Text), Convert.ToInt32(txtID.Text));
                System.Windows.MessageBox.Show("Uspešno ste formirali Otpremu kamionom");
            }
            else if (chkIzvoz.Checked == true)
            {
                InsertIzvozKonacna ins2 = new InsertIzvozKonacna();
                ins2.PrenesiKontejnerUOtpremuKamionomIzvoz(OtpremaKontejneraID, Convert.ToInt32(txtOsnov.Text), Convert.ToInt32(txtID.Text));
                System.Windows.MessageBox.Show("Uspešno ste formirali Otpremu kamionom");

                //MessageBox.Show("Mora se obeležiti uvoz!");
            }
            RadniNalozi.InsertRN ir = new InsertRN();
            if (chkPlatforma.Checked)
            {
                ir.InsRN6OtpremaPlatformeKam(Convert.ToDateTime(dateTimePicker1.Value), korisnik, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(0), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(Usluga), "", "", OtpremaKontejneraID, txtReg.Text, Convert.ToInt32(txtID.Text), 1, 0);
            }
            if (chkCirada.Checked)
            {
                if (chkUvoz.Checked == true)
                {
                    ir.InsRN8OtpremaCiradeKam(Convert.ToDateTime(dateTimePicker1.Value), korisnik, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(0), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(Usluga), "", "", OtpremaKontejneraID, txtReg.Text, 0, txtID.Text);
                    VratiRNOtpremaCiradeID();
                    InsertRadniNalogInterni radniNalogInterni = new InsertRadniNalogInterni();
                    radniNalogInterni.UpdRadniNalogInterniGenerisan(Convert.ToInt32(txtID.Text), "OTP", OtpremaKontejneraID, "RN8", RadniNalog8ID);
                    System.Windows.MessageBox.Show("Automatski su formirane forme ZAVRSENI PPRETOVARI i RN ZA KALMARISTU POSTAVKA IZ PRIVREMENE ZONE, OTPREMA CIRADE i RN OTPREMA CIRADE ");


                }
                else if (chkIzvoz.Checked == true)
                {
                    ir.InsRN8OtpremaCiradeKamIzvoz(Convert.ToDateTime(dateTimePicker1.Value), korisnik, Convert.ToDateTime(dateTimePicker1.Value), Convert.ToInt32(0), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(Usluga), "", "", OtpremaKontejneraID, txtReg.Text, 0, txtID.Text);
                }

            }
        }

        int RadniNalogID;
        int RadniNalog8ID;

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            RadniNalozi.InsertRN rn = new RadniNalozi.InsertRN();
            rn.InsRN9PrijmCiradeKam(Convert.ToDateTime(dateTimePicker1.Value), korisnik, Convert.ToDateTime(dateTimePicker1.MinDate), 0, Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue)
                , Convert.ToInt32(Usluga), "", "", PrijemID, txtReg.Text.ToString().TrimEnd(), CariniskiPostupak, VrstaPregleda, Spedicija, Brodar, BrojPlombe, Convert.ToInt32(txtID.Text));
            VratiRNPrijemCiradeID();
            InsertRadniNalogInterni radniNalogInterni = new InsertRadniNalogInterni();
            radniNalogInterni.UpdRadniNalogInterniGenerisan(Convert.ToInt32(txtID.Text), "PRI", PrijemID, "RN9", RadniNalogID);
                System.Windows.MessageBox.Show("RN ZA KALMARISTU POSTAVKA U PRIVREMENU ZONU - RN PRIJEM CIRADE ");
        }

        int pc = 0; // Potvrdila carina
        int pk = 0;

        int PotvrdioKlijent(int RNI)
        {
            int pk = 0;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select UVozKonacna.PotvrdioKlijent, UvozKonacna.UradilaCarina  from RadniNalogInterni inner join UvozKonacna on RadniNalogInterni.BrojOsnov = UvozKonacna.ID where RadniNalogInterni.ID = " + RNI, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               pk = Convert.ToInt32(dr["PotvrdioKlijent"].ToString());
            }
            conn.Close();
            dr.Close();
            return pk;
        }
        int UradilaCarina(int RNI)
        {
            int pk = 0;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select UVozKonacna.PotvrdioKlijent, UvozKonacna.UradilaCarina  from RadniNalogInterni inner join UvozKonacna on RadniNalogInterni.BrojOsnov = UvozKonacna.ID where RadniNalogInterni.ID = " + RNI, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                pk = Convert.ToInt32(dr["UradilaCarina"].ToString());
            }
            conn.Close();
            dr.Close();
            return pk;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
            int i = PotvrdioKlijent(Convert.ToInt32(txtID.Text));
            int j = UradilaCarina(Convert.ToInt32(txtID.Text));
            if (i == 0)
            {
                System.Windows.Forms.MessageBox.Show("Nije potvrdio klijent");
                return;
            }
            if (j == 0)
            {
                System.Windows.Forms.MessageBox.Show("Nije uradila carina");
                return;
            }

           // RN9PrijemCirade pc = new RN9PrijemCirade();
            RN12MedjuskladisniKontejnera rn12 = new RN12MedjuskladisniKontejnera(Convert.ToInt16(txtID.Text), txtKontejner.Text, txtNapomena.Text);
            rn12.Show();

            RadniNalozi.InsertRN rn = new RadniNalozi.InsertRN();
            rn.InsRN9PrijmCiradeKam(Convert.ToDateTime(dateTimePicker1.Value), korisnik, Convert.ToDateTime(dateTimePicker1.MinDate), 0, Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue)
                , Convert.ToInt32(Usluga), "", "", PrijemID, txtReg.Text.ToString().TrimEnd(), CariniskiPostupak, VrstaPregleda, Spedicija, Brodar, "", Convert.ToInt32(txtID.Text));

            VratiRNPrijemCiradeID();
            InsertRadniNalogInterni radniNalogInterni = new InsertRadniNalogInterni();
            radniNalogInterni.UpdRadniNalogInterniGenerisan(Convert.ToInt32(txtID.Text), "PRI", PrijemID, "RN9", RadniNalogID);
            System.Windows.MessageBox.Show("Automatski su formirane forme PLANIRANI PRETOVARI i RN ZA KALMARISTU POSTAVKA U PRIVREMENU ZONU,  RN PRIJEM CIRADE ");
            // RN12MedjuskladisniKontejnera(int NalogID, string BrojKontejnera, string Napomena)
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int i = PotvrdioKlijent(Convert.ToInt32(txtID.Text));
            int j = UradilaCarina(Convert.ToInt32(txtID.Text));
            if (i == 0)
            {
                System.Windows.Forms.MessageBox.Show("Nije potvrdio klijent");
                return;
            }
            if (j == 0)
            {
                System.Windows.Forms.MessageBox.Show("Nije uradila carina");
                return;
            }


            if (txtID.Text != "")
            {
                Prijemnica frm = new Prijemnica(Convert.ToInt32(txtID.Text), txtKontejner.Text.ToString().TrimEnd(), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue));
                frm.Show();

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int i = PotvrdioKlijent(Convert.ToInt32(txtID.Text));
            int j = UradilaCarina(Convert.ToInt32(txtID.Text));
            if (i == 0)
            {
                System.Windows.Forms.MessageBox.Show("Nije potvrdio klijent");
                return;
            }
            if (j == 0)
            {
                System.Windows.Forms.MessageBox.Show("Nije uradila carina");
                return;
            }


            if (txtID.Text == "")
            {
                return;
            
            }
            RadniNalozi.Otpremnica frm = new Otpremnica(Convert.ToInt32(txtID.Text), txtKontejner.Text.ToString().TrimEnd(), txtReg.Text, txtVozac.Text);
            frm.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int i = PotvrdioKlijent(Convert.ToInt32(txtID.Text));
            int j = UradilaCarina(Convert.ToInt32(txtID.Text));
            if (i == 0)
            {
                System.Windows.Forms.MessageBox.Show("Nije potvrdio klijent");
                return;
            }
            if (j == 0)
            {
                System.Windows.Forms.MessageBox.Show("Nije uradila carina");
                return;
            }


            int Ciradatmp = 0;
            int ModulPorekla = 0;
            if (chkUvoz.Checked == true)
            {
                ModulPorekla = 1;
            }
            else if (chkIzvoz.Checked == true)
            {
                ModulPorekla = 2;
            }
            if (chkCirada.Checked == true)
                Ciradatmp = 1;

            Dokumenta.InsertOtprema ins = new Dokumenta.InsertOtprema();
            ins.InsertOtp(Convert.ToDateTime(dateTimePicker1.Value), 0, 0, txtReg.Text.ToString().TrimEnd(), txtVozac.Text.ToString().TrimEnd(), Convert.ToDateTime(dateTimePicker1.MinDate), 0, Convert.ToDateTime(DateTime.Now), korisnik, txtNapomena.Text, 0, 0, Ciradatmp, ModulPorekla);
            VratiPodatkeMax();
            if (chkUvoz.Checked == true)
            {
                InsertIzvozKonacna ins2 = new InsertIzvozKonacna();
                ins2.PrenesiKontejnerUOtpremuKamionomUvoz(Convert.ToInt32(txtOsnov.Text), Convert.ToInt32(txtID.Text));
                System.Windows.MessageBox.Show("Uspešno ste formirali Otpremu kamionom");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int i = PotvrdioKlijent(Convert.ToInt32(txtID.Text));
            int j = UradilaCarina(Convert.ToInt32(txtID.Text));
            if (i == 0)
            {
                System.Windows.Forms.MessageBox.Show("Nije potvrdio klijent");
                return;
            }
            if (j == 0)
            {
                System.Windows.Forms.MessageBox.Show("Nije uradila carina");
                return;
            }

            RN12MedjuskladisniKontejnera rn12 = new RN12MedjuskladisniKontejnera(Convert.ToInt16(txtID.Text), txtKontejner.Text, txtNapomena.Text);
            rn12.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            RN12MedjuskladisniKontejnera rn12 = new RN12MedjuskladisniKontejnera(Convert.ToInt16(txtID.Text), txtKontejner.Text, txtNapomena.Text);
            rn12.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            VratiIzvozKonacna();
            Dokumeta.InsertPrijemKontejneraVoz ins = new Dokumeta.InsertPrijemKontejneraVoz();
            ins.InsertPrijemKontVoz(Convert.ToDateTime(dateTimePicker1.Value.ToString()), 0, 0, Convert.ToDateTime(dateTimePicker1.MinDate.ToString()), DateTime.Now, korisnik, txtReg.Text.ToString(), txtVozac.Text.ToString(),
                0, txtNapomena.Text.ToString(), 0, 0, 1, 1, 0, 2);
            InsertUvozKonacna ins2 = new InsertUvozKonacna();
            ins2.PrenesiKontejnerIzPlanaNaPrijemnicuIzvoz(Convert.ToInt32(txtOsnov.Text), Convert.ToInt32(txtID.Text));
            VratiPrijemID();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                Prijemnica frm = new Prijemnica(Convert.ToInt32(txtID.Text), txtKontejner.Text.ToString().TrimEnd(), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue));
                frm.Show();

            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            RadniNalozi.Otpremnica frm = new Otpremnica(Convert.ToInt32(txtID.Text), txtKontejner.Text.ToString().TrimEnd(), txtReg.Text, txtVozac.Text);
            frm.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            RN12MedjuskladisniKontejnera rn12 = new RN12MedjuskladisniKontejnera(Convert.ToInt16(txtID.Text), txtKontejner.Text, txtNapomena.Text);
            rn12.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            InsertRadniNalogInterni irn = new InsertRadniNalogInterni();
            irn.UpdRadniNalogInterniZavrsen(Convert.ToInt32(txtID.Text), korisnik);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            InsertRadniNalogInterni irn = new InsertRadniNalogInterni();
            irn.UpdRadniNalogInterniZavrsen(Convert.ToInt32(txtID.Text), korisnik);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            InsertRadniNalogInterni irn = new InsertRadniNalogInterni();
            irn.UpdRadniNalogInterniZavrsen(Convert.ToInt32(txtID.Text), korisnik);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            InsertRadniNalogInterni irn = new InsertRadniNalogInterni();
            irn.UpdRadniNalogInterniZavrsen(Convert.ToInt32(txtID.Text), korisnik);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Fill();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            int OJ = 0;
            if (chkUvoz.Checked == true)
            {
                OJ = 1;
            }
            else
            {
                OJ = 2;
            }
            frmDodatneUsluge du = new frmDodatneUsluge(txtOsnov.Text, OJ);
            du.Show();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            int ModulPorekla = 1;
            VratiIzvozKonacna();
            if (chkUvoz.Checked == true)
            {
                ModulPorekla = 1;
            }
            else if (chkIzvoz.Checked == true)
            {
                ModulPorekla = 2;
            }
            Dokumeta.InsertPrijemKontejneraVoz ins = new Dokumeta.InsertPrijemKontejneraVoz();
            ins.InsertPrijemKontVoz(Convert.ToDateTime(dateTimePicker1.Value.ToString()), 0, 0, Convert.ToDateTime(dateTimePicker1.MinDate.ToString()), DateTime.Now, korisnik, txtReg.Text.ToString(), txtVozac.Text.ToString(),
                0, txtNapomena.Text.ToString(), 0, 0, 1, 1, 0, ModulPorekla);
            InsertUvozKonacna ins2 = new InsertUvozKonacna();
            ins2.PrenesiKontejnerIzPlanaNaPrijemnicuIzvoz(Convert.ToInt32(txtOsnov.Text), Convert.ToInt32(txtID.Text));
            VratiPrijemID();

            InsertKontejnerTekuceOperacije ikto = new InsertKontejnerTekuceOperacije();
            ikto.InsKontejnerTekuceOperacije(txtKontejner.Text, korisnik, "Dolazak Cirada " + txtReg.Text);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                Prijemnica frm = new Prijemnica(Convert.ToInt32(txtID.Text), txtKontejner.Text.ToString().TrimEnd(), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue));
                frm.Show();

            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (txtID.Text == "")
            {
                return;

            }
            RadniNalozi.Otpremnica frm = new Otpremnica(Convert.ToInt32(txtID.Text), txtKontejner.Text.ToString().TrimEnd(), txtReg.Text, txtVozac.Text);
            frm.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            int Ciradatmp = 0;
            int ModulPorekla = 0;
            if (chkUvoz.Checked == true)
            {
                ModulPorekla = 1;
            }
            else if (chkIzvoz.Checked == true)
            {
                ModulPorekla = 2;
            }
            if (chkCirada.Checked == true)
                Ciradatmp = 1;

            Dokumenta.InsertOtprema ins = new Dokumenta.InsertOtprema();
            ins.InsertOtp(Convert.ToDateTime(dateTimePicker1.Value), 0, 0, txtReg.Text.ToString().TrimEnd(), txtVozac.Text.ToString().TrimEnd(), Convert.ToDateTime(dateTimePicker1.MinDate), 0, Convert.ToDateTime(DateTime.Now), korisnik, txtNapomena.Text, 0, 0, Ciradatmp, ModulPorekla);
            VratiPodatkeMax();

            InsertKontejnerTekuceOperacije ikto = new InsertKontejnerTekuceOperacije();
            ikto.InsKontejnerTekuceOperacije(txtKontejner.Text, korisnik, "Odlazak Cirada " + txtReg.Text);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // Treba mi Otvaranje postojece
            if (txtID.Text != "")
            {
                Prijemnica frm = new Prijemnica(Convert.ToInt32(txtID.Text), txtKontejner.Text.ToString().TrimEnd(), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), SelektovanaPrijemnica);
                frm.Show();

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                       SelektovanaPrijemnica = Convert.ToInt32(row.Cells[0].Value.ToString());
                    }
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (txtOsnov.Text != "") 
            {
                InsertUvozKonacna ins = new InsertUvozKonacna();
                ins.UpdUvozKonacnaUC(Convert.ToInt32(txtOsnov.Text));
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                Otpremnica frm = new Otpremnica(Convert.ToInt32(txtID.Text), txtKontejner.Text.ToString().TrimEnd(),txtReg.Text, txtVozac.Text, SelektovanaOtpremnica);
                frm.Show();

            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        SelektovanaOtpremnica = Convert.ToInt32(row.Cells[0].Value.ToString());
                    }
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Nije uspela selekcija stavki");
            }
        }

        private void VratiRNOtpremaCiradeID()
        {
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Max(ID) from RNOtpremaCirade", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                RadniNalog8ID = Convert.ToInt32(dr[0].ToString());
            }
            conn.Close();
            dr.Close();
        }
        private void VratiRNPrijemCiradeID()
        {
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Max(ID) from RNPrijemCirade", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                RadniNalogID = Convert.ToInt32(dr[0].ToString());
            }
            conn.Close();
            dr.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (chkUvoz.Checked == true)
            {
                VratiUvozKonacna();
                Dokumeta.InsertPrijemKontejneraVoz ins = new Dokumeta.InsertPrijemKontejneraVoz();
                ins.InsertPrijemKontVoz(Convert.ToDateTime(dateTimePicker1.Value.ToString()), 0, 0, Convert.ToDateTime(dateTimePicker1.MinDate.ToString()), DateTime.Now, korisnik, txtReg.Text.ToString(), txtVozac.Text.ToString(),
                    0, txtNapomena.Text.ToString(), 0, 0, 1, 1, 0, 1);
                InsertUvozKonacna ins2 = new InsertUvozKonacna();
                ins2.PrenesiKontejnerIzPlanaNaPrijemnicu(Convert.ToInt32(txtOsnov.Text), Convert.ToInt32(txtID.Text));
                VratiPrijemID();
                RadniNalozi.InsertRN rn = new RadniNalozi.InsertRN();
                rn.InsRN9PrijmCiradeKam(Convert.ToDateTime(dateTimePicker1.Value), korisnik, Convert.ToDateTime(dateTimePicker1.MinDate), 0, Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue)
                    , Convert.ToInt32(Usluga), "", "", PrijemID, txtReg.Text.ToString().TrimEnd(), CariniskiPostupak, VrstaPregleda, Spedicija, Brodar, BrojPlombe, Convert.ToInt32(txtID.Text));

                VratiRNPrijemCiradeID();
                InsertRadniNalogInterni radniNalogInterni = new InsertRadniNalogInterni();
                radniNalogInterni.UpdRadniNalogInterniGenerisan(Convert.ToInt32(txtID.Text), "PRI", PrijemID, "RN9", RadniNalogID);
                System.Windows.MessageBox.Show("Automatski su formirane forme PLANIRANI PRETOVARI i RN ZA KALMARISTU POSTAVKA U PRIVREMENU ZONU,  RN PRIJEM CIRADE ");

            }
            else if (chkIzvoz.Checked == true)
            {
                VratiIzvozKonacna();
                Dokumeta.InsertPrijemKontejneraVoz ins = new Dokumeta.InsertPrijemKontejneraVoz();
                ins.InsertPrijemKontVoz(Convert.ToDateTime(dateTimePicker1.Value.ToString()), 0, 0, Convert.ToDateTime(dateTimePicker1.MinDate.ToString()), DateTime.Now, korisnik, txtReg.Text.ToString(), txtVozac.Text.ToString(),
                    0, txtNapomena.Text.ToString(), 0, 0, 1, 1, 0, 2);
                InsertUvozKonacna ins2 = new InsertUvozKonacna();
                ins2.PrenesiKontejnerIzPlanaNaPrijemnicuIzvoz(Convert.ToInt32(txtOsnov.Text), Convert.ToInt32(txtID.Text));
                VratiPrijemID();
                System.Windows.MessageBox.Show("Automatski su formirane forme PLANIRANI PRETOVARI - PRIJEM  CIRADE ");
               

            }

           }
    }
}
