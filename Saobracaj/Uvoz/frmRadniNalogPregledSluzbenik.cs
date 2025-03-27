using Saobracaj.Izvoz;
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
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class frmRadniNalogPregledSluzbenik : Form
    {
        string Korisnik = "";
        string s_connection = Sifarnici.frmLogovanje.connectionString;

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
                meniHeader.Visible = false;
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }
        /*
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }
        */
        public frmRadniNalogPregledSluzbenik(string KorisnikTekuci)
        {
            InitializeComponent();
            Korisnik = KorisnikTekuci;
            ChangeTextBox();
            //    ChangeTextBoxGradientPanel1();
            //   ChangeTextBoxGradientPanel2();
            //  ChangeTextBoxGradientPanel3();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            string DodatniAND = " ";

            DodatniAND = DodatniAND + " AND rn.Forma in ('GATE IN KAMION' , 'GATE IN KAMION IZVOZ', 'GATE IN KAMION TERMINAL', 'GATE OUT KAMION', 'GATE OUT KAMION TERMINAL')";


            var select = "Select * from (SELECT rn.[ID]  ,UvozKonacna.BrojKontejnera,VrstaManipulacije.Naziv,[Uradjen]  , TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv as KS ,  " +
" (select Top 1 OperacijaUradjena from KontejnerTekuce inner join UvozKonacna  on UvozKonacna.BrojKontejnera = KontejnerTekuce.Kontejner  where UvozKonacna.ID = rn.BrojOsnov) as ZadnjaOperacija, " +
" rn.BrojDokPrevoza, rn.BrojRN, " +
" rn.[StatusIzdavanja]  , [OJIzdavanja], o1.Naziv as Izdao, " +
" [OJRealizacije], o2.Naziv as Realizuje,  [DatumIzdavanja],[DatumRealizacije]  ,rn.[Napomena]  ,  UvozKonacnaVrstaManipulacije.IDVrstaManipulacije, " +
" [Osnov]  ,[BrojOsnov]  ,[KorisnikIzdao] ,  [KorisnikZavrsio]  ,uv.PaNaziv as Platilac  , " +
" PlanID as PlanUtovara, rn.Pokret, TipDokPrevoza, TipRN  FROM[RadniNalogInterni] rn " +
" inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID " +
" inner join UvozKonacna on UvozKonacna.ID = BrojOsnov  inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
" inner join Partnerji uv on uv.PaSifra = UvozKonacnaVrstaManipulacije.Platilac " +
" Inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera " +
" Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera " +
" where 1=1  " + DodatniAND +
" union " +
" SELECT rn.[ID] , IzvozKonacna.BrojKontejnera,VrstaManipulacije.Naziv,[Uradjen],TipKontenjera.Naziv as Tipkontejnera, KontejnerStatus.Naziv as KS, " +
" (select Top 1 OperacijaUradjena from KontejnerTekuce inner join IzvozKonacna  on IzvozKonacna.BrojKontejnera = KontejnerTekuce.Kontejner  where IzvozKonacna.ID = rn.BrojOsnov) as ZadnjaOperacija, " +
"  rn.BrojDokPrevoza, " +
" rn.BrojRN , rn.[StatusIzdavanja]  , [OJIzdavanja], o1.Naziv as Izdao, " +
" [OJRealizacije], o2.Naziv as Realizuje,  [DatumIzdavanja],[DatumRealizacije]  ,rn.[Napomena]  , " +
" IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije, [Osnov]  ,[BrojOsnov]  , " +
" [KorisnikIzdao] ,   [KorisnikZavrsio]  ,uv.PaNaziv as Platilac  , " +
" PlanID as PlanUtovara, rn.Pokret, TipDokPrevoza, TipRN  FROM[RadniNalogInterni] rn inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
" inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID  inner join IzvozKonacna on IzvozKonacna.ID = BrojOsnov " +
" inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.ID = rn.KonkretaIDUsluge " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije " +
" inner join Partnerji uv on uv.PaSifra = IzvozKonacnaVrstaManipulacije.Platilac " +
" Inner join TipKontenjera on TipKontenjera.ID = IzvozKonacna.VrstaKOntejnera " +
" Inner join KontejnerStatus on KontejnerStatus.ID = rn.StatusKontejnera " +
" where 1=1  " + DodatniAND + " ) as t1 " +
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

            //  GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            //dynamicFilter.WireGrid(this.gridGroupingControl1);

        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            /*
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
            */
        }
        private void frmRadniNalogPregledSluzbenik_Load(object sender, EventArgs e)
        {
            /*
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
           */
        }

        private void gridGroupingControl1_TableControlCellButtonClicked(object sender, GridTableControlCellButtonClickedEventArgs e)
        {
            /*
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
            */
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

        int ProveriDaLiJeUradjenaPredhodnaOperacija(string Nalog)
        {
            int Uradjen = 1;
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select top 1 ID, BrojOsnov, Uradjen from RadniNalogInterni where ID < " + txtNALOGID.Text + " and ID > " + txtNALOGID.Text + " -10 and BrojOsnov = (Select BrojOsnov from RadniNalogInterni where ID = " + txtNALOGID.Text + ") order by ID DEsc ", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Uradjen = Convert.ToInt32(dr["Uradjen"].ToString().TrimEnd());
            }
            con.Close();
            return Uradjen;

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            int i = 0;

            i = ProveriDaLiJeUradjenaPredhodnaOperacija(txtNALOGID.Text);
            if (i == 0)
            {
                MessageBox.Show("Nije zavrsena predhodna usluga ne mozete generisati novu!!!");
                return;
            }
            if (txtTipBroj.Text != "0" || txtRNBroj.Text != "0")
            {
                MessageBox.Show("Vec su napravljeni RN i Vozno sredstvo");
                return;
            
            }
            
            string Forma = VratiFormu();
            int KISUsl = 0;
            int OJ = VratiOJIzdavanja();
            if (Forma == "GATE IN VOZ")
            {
                MessageBox.Show("Formirate GATE IN VOZ");
                frmPrijemVozaIzPlana rd1 = new frmPrijemVozaIzPlana(Convert.ToInt32(txtNALOGID.Text), 0, OJ);
                rd1.Show();
            }
            if (Forma == "GATE OUT KAMION" || Forma == "GATE OUT KAMION TERMINAL")
            {

                MessageBox.Show("Formirate GATE OUT KAMION Platforma");
                KISUsl = VratiKonkretanIDUsluge();
                Saobracaj.Izvoz.frmOtpremaKontejneraKamionomIzKontejnera okk = new Izvoz.frmOtpremaKontejneraKamionomIzKontejnera(textBox1.Text, txtNALOGID.Text, Korisnik, 0, OJ, txtBrojKontejnera.Text);
                okk.Show();


            }

            if (Forma == "GATE IN KAMION" || Forma == "GATE IN KAMION IZVOZ" || Forma == "GATE IN KAMION TERMINAL")
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

            if (Forma == "GATE OUT PRETOVAR")
            {
                //
                MessageBox.Show("Formirate GATE IN kamion Cirada");
                Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz prijemplat = new Saobracaj.Dokumenta.frmPrijemKontejneraKamionLegetUvoz(Korisnik, 0, txtNALOGID.Text, 1, 1);
                prijemplat.Show();

            }

            if (Forma == "GATE IN PRETOVAR")
            {
                MessageBox.Show("Formirate GATE OUT kamion Cirada");
                Saobracaj.Izvoz.frmOtpremaKontejneraKamionomIzKontejnera okk = new Izvoz.frmOtpremaKontejneraKamionomIzKontejnera(textBox1.Text, txtNALOGID.Text, Korisnik, 1, OJ,txtBrojKontejnera.Text);
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
                    txtBrojKontejnera.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojKontejnera").ToString();
                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
