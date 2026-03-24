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

namespace Saobracaj.Izvoz
{
    public partial class frmPregledPorudzbenica: Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        int statusizmene = 0; //  1- Updejt status  -- 2-otkazi
        private GridDynamicFilter dynamicFilter;
        private GridExcelFilter gridExcelFilter;
        private bool filteriInicijalizovani = false; // Zastavica

        public frmPregledPorudzbenica(int st)
        {

            InitializeComponent();
            ChangeTextBox();
            statusizmene = st;
            if (st == 2)
            {
                panelHeader.Visible = false;
                gridGroupingControl1.Dock = DockStyle.Fill;

            }
        }


        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            //  meniHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                //   meniHeader.Visible = true;
                //   meniHeader.Visible = false;
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


                foreach (System.Windows.Forms.Control control in this.Controls)
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
                //meniHeader.Visible = false;
                //meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }
       


        private void RefreshGridControl()
        {
            var select = " SELECT  ProdajniNalogIzvoz.ID as ID,  Partnerji_3.PaNaziv AS Nalogodavac , ProdajniNalogIzvoz.OpisPosla, " + 
          " Partnerji_4.PaNaziv AS Brodar, Partnerji_2.PaNaziv AS Izvoznik, ProdajniNalogIzvoz.Link , ProdajniNalogIzvoz.CuttOfPort, ProdajniNalogIzvoz.BukingNumber " +
          "FROM  ProdajniNalogIzvoz " +
          "LEFT JOIN  Partnerji ON ProdajniNalogIzvoz.Brodar = Partnerji.PaSifra " +
          "LEFT JOIN  Partnerji AS Partnerji_2 ON ProdajniNalogIzvoz.Izvoznik = Partnerji_2.PaSifra " +
          "LEFT JOIN  Partnerji AS Partnerji_3 ON ProdajniNalogIzvoz.Nalogodavac = Partnerji_3.PaSifra " +
          "LEFT JOIN  Partnerji AS Partnerji_4 ON ProdajniNalogIzvoz.Brodar = Partnerji_4.PaSifra " +
          " order by ProdajniNalogIzvoz.ID desc  ";

            //   var s_connection = Sifarnici.frmLogovanje.connectionString;
            //   SqlConnection myConnection = new SqlConnection(s_connection);
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
            this.gridGroupingControl1.TableOptions.ListBoxSelectionMode = SelectionMode.One;

            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            //GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            ////Wiring the Dynamic Filter to GridGroupingControl
            //dynamicFilter.WireGrid(this.gridGroupingControl1);

            //GridExcelFilter gridExcelFilter = new GridExcelFilter();

            ////Wiring GridExcelFilter to GridGroupingControl
            //gridExcelFilter.WireGrid(this.gridGroupingControl1);
            if (!filteriInicijalizovani)
            {
                dynamicFilter = new GridDynamicFilter();
                dynamicFilter.WireGrid(this.gridGroupingControl1);

                gridExcelFilter = new GridExcelFilter();
                gridExcelFilter.WireGrid(this.gridGroupingControl1);

                filteriInicijalizovani = true;
            }
        }

       
        private void btnOtvori_Click(object sender, EventArgs e)
        {
            if (gridGroupingControl1.Table.SelectedRecords.Count > 0)
            {
                // Uzimamo prvi selektovani red
                Record rec = gridGroupingControl1.Table.SelectedRecords[0].Record;

                if (rec != null)
                {
                    int id = Convert.ToInt32(rec.GetValue("ID"));

                    frmProdajniNalogIzvoz pnd = new frmProdajniNalogIzvoz(id);
                    pnd.Show();

                }
            }
        }
        
        private void gridGroupingControl1_TableControlMouseDown(object sender, GridTableControlMouseEventArgs e)
        {
            if (e.Inner.Button == MouseButtons.Right && statusizmene != 2)
            {
                // Dobavljanje pozicije kliknutog reda i kolone
                // Pronađi red i kolonu pod mišem
                int rowIndex, colIndex;
                e.TableControl.PointToRowCol(new System.Drawing.Point(e.Inner.X, e.Inner.Y), out rowIndex, out colIndex);

                // Uzmi stil kliknutog polja
                GridTableCellStyleInfo style = e.TableControl.GetTableViewStyleInfo(rowIndex, colIndex);

                // Proveri da li je kliknuto u redu sa podacima
                if (style.TableCellIdentity.DisplayElement.Kind == DisplayElementKind.Record)
                {
                    // Postavi aktivni red
                    this.gridGroupingControl1.Table.CurrentRecord = style.TableCellIdentity.DisplayElement.ParentRecord;

                    // Prikaži context menu na poziciji miša
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void gridGroupingControl1_TableControlCellClick_1(object sender, GridTableControlCellClickEventArgs e)
        {
            int id = 0;
            if (statusizmene == 2)
            {
                InsertIzvoz ins = new InsertIzvoz();
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    id = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID"));

                    //InsertProdajniNalogIzvoz ipnk = new InsertProdajniNalogIzvoz();

                    DialogResult result = System.Windows.Forms.MessageBox.Show(
                    "Da li ste sigurni da želite da otkažete stavku?", // Message text
                    "Potvrdite", // Title
                    MessageBoxButtons.YesNoCancel, // Buttons
                    MessageBoxIcon.Question // Icon
                    );

                    // Handle the result based on user selection
                    if (result == DialogResult.Yes)
                    {

                        //ins.DelKontejnerIzvoz(id);
                        this.BeginInvoke(new Action(() => {
                            RefreshGridControl();
                        }));
                    }
                }
            }
        }

        private void frmPregledPorudzbenica_Load(object sender, EventArgs e)
        {
            this.Text = "Lista porudžbenica";
            RefreshGridControl();
            if (statusizmene == 2)
            {
                gridGroupingControl1.ContextMenuStrip = null;
            }
            else
            {
                gridGroupingControl1.ContextMenuStrip = contextMenuStrip1;
            }
        }
    }
}
