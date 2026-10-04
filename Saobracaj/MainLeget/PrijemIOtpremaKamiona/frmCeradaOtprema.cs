using Syncfusion.GridHelperClasses;
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

namespace Saobracaj.MainLeget.PrijemIOtpremaKamiona
{
    public partial class frmCeradaOtprema: Form
    {
        public frmCeradaOtprema()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        private void ChangeTextBox()
        {
            panelHeader.Visible = false;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;

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
                }


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

        private void button8_Click(object sender, EventArgs e)
        {

            VratiPodatke();
        }
        private void VratiPodatke()
        {
            var select = "";


            select = "     SELECT RadniNalogInterni.[ID] as KomNalID, RadniNalogInterni.BrojOsnov as KontID," +
            " DatumPrijema as DatumPrijema, " +
            "  CASE WHEN n1.StatusPrijema = 0 THEN '1-Najava' ELSE '2-Prijem' END as Status,  " +
            "  REgBrKamiona, ImeVozaca, " +
            "  n1.VremeDolaska as VremeDol,  " +
            "  n1.[Datum] ,n1.[Korisnik] ,  RadniNalogInterniPotvrda.KapijaUlaz, RadniNalogInterniPotvrda.Pregledac," +

             "  (SELECT  STUFF((SELECT distinct   '/ ' + Cast(ts.BrojKontejnera as nvarchar(20)) " +
             "  FROM OtpremaKontejneraVozStavke ts where n1.ID = ts.IDNadredjenog " +
            "  FOR XML PATH('')), 1, 1, ''  ) As Skupljen)  " +
             "  as Kontejner , OrganizacioneJedinice.Naziv as Modul,  CASE WHEN n1.Poreklo = 0 THEN 'PLATFORMA' ELSE 'CIRADA' END as POREKLO, RadniNalogInterniPotvrda.Scenario " +
            "  FROM[dbo].[PrijemKontejneraVoz] as n1 " +
            "  inner join organizacioneJedinice on OrganizacioneJedinice.ID = n1.Modul " +
            "  inner join OtpremaKontejneraVozStavke on OtpremaKontejneraVozStavke.IDNadredjenog = n1.ID " +
            "  inner join RadniNalogInterni on RadniNalogInterni.ID = OtpremaKontejneraVozStavke.NalogID " +
            "  inner join RadniNalogInterniPotvrda on RadniNalogInterni.ID = RadniNalogInterniPotvrda.IDNaloga " +
            "  where Vozom = 0  and RadniNalogInterniPotvrda.Kamion = 0 and RadniNalogInterniPotvrda.FazaUsluge = 1   And radninaloginterni.idmanipulacijajed IN ( 70,71)  and n1.Poreklo = 1 order by n1.ID desc";


            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            this.gridGroupingControl2.Table.Records.DeleteAll();

            gridGroupingControl2.DataSource = ds.Tables[0];
            this.gridGroupingControl2.TableDescriptor.VisibleColumns.Remove("Scenario");
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;

            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            // 1. Deo: Scenario = 1 i Pregledac = 0
            GridConditionalFormatDescriptor gcfdPun1 = new GridConditionalFormatDescriptor();
            gcfdPun1.Appearance.AnyRecordFieldCell.BackColor = Color.Yellow;
            gcfdPun1.Appearance.AnyRecordFieldCell.TextColor = Color.Black;
            gcfdPun1.Expression = "[Scenario] = 3 AND [KapijaUlaz] = 1";



            // Dodavanje u grid
            this.gridGroupingControl2.TableDescriptor.ConditionalFormats.Add(gcfdPun1);
            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);
        }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {


                // ČISTO REŠENJE: Sinhronizacija i čišćenje selekcije
                if (gridGroupingControl2.Table.CurrentRecord != null)
                {
                    // 1. Prvo čistimo sve stare redove da se ne bi gomilali u memoriji!
                    gridGroupingControl2.Table.SelectedRecords.Clear();

                    // 2. Dodajemo isključivo i samo ovaj trenutno kliknuti red
                    gridGroupingControl2.Table.SelectedRecords.Add(gridGroupingControl2.Table.CurrentRecord);
                }

            }
            catch (Exception ex) { }
        }

    }
}
