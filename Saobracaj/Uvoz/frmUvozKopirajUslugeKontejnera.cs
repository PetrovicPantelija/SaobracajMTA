using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
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

namespace Saobracaj.Uvoz
{
    public partial class frmUvozKopirajUslugeKontejnera : Form
    {
        int pomIzuvoza = 0;
        private void ChangeTextBox()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;

                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
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

                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }



          





        }

        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dgv.EnableHeadersVisualStyles = false;
            //   header.Style.Font = new Font("Arial", 12F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }

        public frmUvozKopirajUslugeKontejnera(string KontID, int IzUvoza)
        {
            InitializeComponent();
            ChangeTextBox();
            txtID.Text = KontID;
            if (IzUvoza == 1)
            {
                pomIzuvoza = 1;
            VratiBrojKontejneraIzUvoza();
            }
            else { pomIzuvoza = 2; VratiBrojKontejneraIzUvozaKonacna(); }
        }

        private void VratiBrojKontejneraIzUvoza()
        {

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select BrojKontejnera from Uvoz where ID = " + Convert.ToInt32(txtID.Text) , con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtBrKont.Text = dr["BrojKontejnera"].ToString();



            }

            con.Close();
        }

        private void VratiBrojKontejneraIzUvozaKonacna()
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select BrojKontejnera from UvozKonacna where ID = " + Convert.ToInt32(txtID.Text), con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtBrKont.Text = dr["BrojKontejnera"].ToString();



            }

            con.Close();
        }

        private void frmUvozKopirajUslugeKontejnera_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {

            var select = "SELECT Uvoz.ID, " +
                " CASE WHEN Prioritet > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as Prioritet , " +
                " CASE WHEN DobijenBZ > 0 THEN Cast(1 as bit) ELSE Cast(0 as BIT) END as DobijenBZ , " +
                " [BrojKontejnera],TipKontenjera.Naziv as Vrsta_Kontejnera, BrodskaTeretnica as BL,   Brodovi.Naziv as Brod, p1.PaNaziv as Uvoznik, " +
" n1.PaNaziv as NalogodavacZaVoz, Ref1 as Ref1,n2.PaNaziv as NalogodavacZaUsluge, Ref2 as Ref2,n3.PaNaziv as NalogodavacZaDrumski,DobijenNalogBrodara as Dobijen_Nalog_Brodara ,ATABroda,  " +
" Napomena1 as Napomena1,  DobijeBZ as DatumBZ ,PIN,    KontejnerskiTerminali.Naziv as R_L_SRB, pp1.Naziv as Dirigacija_Kontejnera_Za,   BrodskaTeretnica, " +
" VrstaRobeADR.Naziv as ADR, b.PaNaziv as Brodar,pv.PaNaziv as VlasnikKontejnera,     Ref3 as Ref3,         VrstaPregleda as InsTret,p2.PaNaziv as SpedicijaRTC,  " +
" p3.PaNaziv as SpedicijaGranica,       VrstaCarinskogPostupka.Naziv as CarinskiPostupak,  " +
" VrstePostupakaUvoz.Naziv as PostupakSaRobom,uvNacinPakovanja.Naziv as NacinPakovanja, " +
" Napomena as Napomena2, NaslovStatusaVozila as NaslovZaslanjestatusa,  Carinarnice.Naziv as Carinarnica,   " +
" p4.PaNaziv as OdredisnaSpedicija, MestaUtovara.Naziv as MestoIstovara, KontaktOsobe, Email, " +
" BrojPlombe1, BrojPlombe2,    NetoRobe, BrutoRobe, TaraKontejnera, BrutoKontejnera,  Koleta, green FROM Uvoz inner join Partnerji on PaSifra = VlasnikKontejnera " +
" inner join Partnerji p1 on p1.PaSifra = Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = SpedicijaRTC " +
" inner join Partnerji p3 on p3.PaSifra = SpedicijaGranica " +
" inner join TipKontenjera on TipKontenjera.ID = Uvoz.TipKontejnera " +
" inner join Carinarnice on Carinarnice.ID = Uvoz.OdredisnaCarina " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = Uvoz.CarinskiPostupak " +
" inner join Predefinisaneporuke on PredefinisanePoruke.ID = Uvoz.NapomenaZaPozicioniranje " +
" inner join KontejnerskiTerminali on KontejnerskiTerminali.ID = Uvoz.RLTErminali " +
" inner join Partnerji n1 on n1.PaSifra = Nalogodavac1 " +
" inner join Partnerji n2 on n2.PaSifra = Nalogodavac2 " +
" inner join Partnerji n3 on n3.PaSifra = Nalogodavac3 " +
" inner join Partnerji b on b.PaSifra = Uvoz.Brodar " +
" inner join  DirigacijaKontejneraZa pp1 on pp1.ID = Uvoz.DirigacijaKontejeraZa " +
" inner join Brodovi on Brodovi.ID = Uvoz.NazivBroda " +
" inner join VrstaRobeADR on VrstaRobeADR.ID = ADR " +
" inner join VrstePostupakaUvoz on VrstePostupakaUvoz.ID = PostupakSaRobom " +
" inner join MestaUtovara on Uvoz.MestoIstovara = MestaUtovara.ID " +
" inner join uvNacinPakovanja on uvNacinPakovanja.ID = NacinPakovanja " +
" inner join Partnerji p4 on p4.PaSifra = OdredisnaSpedicija " +
" inner join Partnerji pv on pv.PaSifra = Uvoz.VlasnikKontejnera " +
"  order by Uvoz.Prioritet desc, Uvoz.ID desc ";



            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            // dataGridView1.ReadOnly = true;
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();

            //Wiring the Dynamic Filter to GridGroupingControl
            dynamicFilter.WireGrid(this.gridGroupingControl1);

        }

        private void button23_Click(object sender, EventArgs e)
        {
            int Terminalske = 0;
            int Administrativne = 0;
            int Dodatne = 0;

            if (chkTerminalske.Checked == true ) { Terminalske = 1; }
            if (chkAdministrativne.Checked == true) { Administrativne = 1; }
            if (chkDodatne.Checked == true) { Dodatne = 1; }

            if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                InsertUvoz uv = new InsertUvoz();
                foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                {
                    if (chkTerminalske.Checked == true && pomIzuvoza == 1)
                    {
                        uv.KopirajKontejnerskeUslugeTerminalske(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()),1);

                    }
                    if (chkAdministrativne.Checked == true && pomIzuvoza == 1)
                    {
                        uv.KopirajKontejnerskeUslugeAdministrativne(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), 1);

                    }
                    if (chkDodatne.Checked == true && pomIzuvoza == 1)
                    {
                        uv.KopirajKontejnerskeUslugeDodatne(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), 1);

                    }
                    ///UvozKonacna
                    if (chkTerminalske.Checked == true && pomIzuvoza == 2)
                    {
                        uv.KopirajKontejnerskeUslugeTerminalske(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), 2);

                    }
                    if (chkAdministrativne.Checked == true && pomIzuvoza == 2)
                    {
                        uv.KopirajKontejnerskeUslugeAdministrativne(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), 2);

                    }
                    if (chkDodatne.Checked == true && pomIzuvoza == 2)
                    {

                        uv.KopirajKontejnerskeUslugeDodatne(Convert.ToInt32(txtID.Text), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()), 2);
                    }




                }
            }

            RefreshDataGrid();
        }
    }
}
