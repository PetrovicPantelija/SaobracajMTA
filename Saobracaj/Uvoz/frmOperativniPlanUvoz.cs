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
    public partial class frmOperativniPlanUvoz: Form
    {
        public frmOperativniPlanUvoz()
        {
            InitializeComponent();
            ChangeTextBox();
            RefreshGrid();
        }

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;

                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);




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
                panelHeader.Visible = false;

                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }


        private void RefreshGrid()
        {
            var select = "";
      
                select = "select  UvozVrstaManipulacije.ID as UKID, UvozVrstaManipulacije.IDNadredjena as KontejnerID, Uvoz.BrojKontejnera, " +
                        " VrstaManipulacije.ID as UslugaID,VrstaManipulacije.Naziv as UslugaNaziv, " +
                         " OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
                        " RadniNalogDrumski.NalogID, CONVERT(varchar,RadniNalogDrumski.DatumKreiranjaNaloga,104) AS KreiranjeNaloga, StatusVozila.Naziv AS StatusVozila, " +
                        " CONVERT(varchar,RadniNalogDrumski.DatumPromeneStatusa,104) AS PromenaStatusa, Automobili.RegBr, RadniNalogDrumski.Uvoz " +
                        " from UvozVrstaManipulacije " +
                        " Inner join VrstaManipulacije on VrstaManipulacije.ID = UvozVrstaManipulacije.IDVrstaManipulacije " +
                        " inner join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozVrstaManipulacije.OrgJed " +
                        " inner join Uvoz on UvozVrstaManipulacije.IDNadredjena = Uvoz.ID" +
                        " left join RadniNalogDrumski on Uvoz.ID = RadniNalogDrumski.KontejnerID and UvozVrstaManipulacije.IDVrstaManipulacije = RadniNalogDrumski.IDVrstaManipulacije AND UvozVrstaManipulacije.ID = RadniNalogDrumski.UKID " +
                        " left join StatusVozila  ON StatusVozila.ID = RadniNalogDrumski.Status " +
                        " left join Automobili ON RadniNalogDrumski.KamionID = Automobili.ID" +
                        " where  OrganizacioneJedinice.Naziv = 'Drumski prevoz' "+
                        "UNION " +
                         "select  UvozKonacnaVrstaManipulacije.ID as UKID,UvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID, UvozKonacna.BrojKontejnera, " +
                        " VrstaManipulacije.ID as UslugaID,VrstaManipulacije.Naziv as UslugaNaziv, " +
                        " OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
                        " RadniNalogDrumski.NalogID, CONVERT(varchar,RadniNalogDrumski.DatumKreiranjaNaloga,104) AS KreiranjeNaloga, StatusVozila.Naziv AS StatusVozila," +
                        " CONVERT(varchar,RadniNalogDrumski.DatumPromeneStatusa,104) AS PromenaStatusa, Automobili.RegBr,  RadniNalogDrumski.Uvoz  " +
                        " from UvozKonacnaVrstaManipulacije " +
                        " Inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije" +
                        " inner join OrganizacioneJedinice on OrganizacioneJedinice.ID = UvozKonacnaVrstaManipulacije.OrgJed " +
                        " inner join UvozKonacna on UvozKonacnaVrstaManipulacije.IDNadredjena = UvozKonacna.ID  " +
                        " left join RadniNalogDrumski on UvozKonacna.ID = RadniNalogDrumski.KontejnerID and UvozKonacnaVrstaManipulacije.IDVrstaManipulacije = RadniNalogDrumski.IDVrstaManipulacije AND UvozKonacnaVrstaManipulacije.ID = RadniNalogDrumski.UKID " +
                        " left join StatusVozila  ON StatusVozila.ID = RadniNalogDrumski.Status " +
                        " left join Automobili ON RadniNalogDrumski.KamionID = Automobili.ID" +
                        " where  OrganizacioneJedinice.Naziv = 'Drumski prevoz' ";

            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new System.Data.DataSet();
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

        private void button1_Click(object sender, EventArgs e)
        {
            InsertUvoz isu = new InsertUvoz();
            int uvoz = 1;

            List<(int kontejnerID, int manipulacijaID,int UKID)> stavke = new List<(int, int,int)>();

            foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
            {
                object nalogIdValue = selectedRecord.Record.GetValue("NalogID");
                if (nalogIdValue != null && int.TryParse(nalogIdValue.ToString(), out int nalogId) && nalogId != 0)
                {
                    MessageBox.Show("Radni nalog se može kreirati samo za stavke koje još nisu dodeljene nalogu.",
                                    "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int UKID = Convert.ToInt32(selectedRecord.Record.GetValue("UKID"));
                int kontejnerID = Convert.ToInt32(selectedRecord.Record.GetValue("KontejnerID"));
                int manipulacijaID = Convert.ToInt32(selectedRecord.Record.GetValue("UslugaID"));
                stavke.Add((kontejnerID, manipulacijaID, UKID));
            }

            if (stavke.Count > 0)
            {
                isu.KreirajRadniNalogDrumski(stavke, uvoz);
                RefreshGrid();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int uvoz = 1;
            InsertUvoz isu = new InsertUvoz();
            List<(int kontejnerID, int manipulacijaID, int UKID)> stavkeBezNaloga = new List<(int, int, int)>();
            HashSet<int> nalogIds = new HashSet<int>();

            foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
            {
                object nalogIdValue = selectedRecord.Record.GetValue("NalogID");
                int nalogId = 0;
                if (nalogIdValue != null && int.TryParse(nalogIdValue.ToString(), out int parsedId))
                {
                    nalogId = parsedId;
                }

                if (nalogId != 0)
                {
                    nalogIds.Add(nalogId);
                }
                else
                {
                    int UKID = Convert.ToInt32(selectedRecord.Record.GetValue("UKID"));
                    int kontejnerID = Convert.ToInt32(selectedRecord.Record.GetValue("KontejnerID"));
                    int manipulacijaID = Convert.ToInt32(selectedRecord.Record.GetValue("UslugaID"));
                    stavkeBezNaloga.Add((kontejnerID, manipulacijaID, UKID));
                }
            }

            if (nalogIds.Count > 1)
            {
                MessageBox.Show("Stavke se mogu dodati samo u jedan nalog. Selektovano je više različitih naloga.",
                                "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nalogIds.Count == 1 && stavkeBezNaloga.Count > 0)
            {
                int nalogId = nalogIds.First();

                DialogResult result = MessageBox.Show($"Da li želite da dodate stavke u postojeći nalog ID: {nalogId}?",
                                                      "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    isu.UpdateRadniNalogDrumski(stavkeBezNaloga, nalogId, uvoz);
                    RefreshGrid();
                }
            }
        }
    }
}
