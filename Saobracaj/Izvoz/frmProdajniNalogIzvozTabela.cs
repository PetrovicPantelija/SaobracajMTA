using Microsoft.Ajax.Utilities;
using Microsoft.Office.Interop.Excel;
using Saobracaj.MainLeget.Izvoz;
using Saobracaj.MainLeget.LegNew;
using Saobracaj.Pomocni;
using Saobracaj.Uvoz;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IdentityModel.Metadata;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class frmProdajniNalogIzvozTabela : Form
    {
        int statusizmene = 0; // 0 - Readonly 1- Updejt status
        public frmProdajniNalogIzvozTabela(int st)
        {
            InitializeComponent();
            ChangeTextBox();
            statusizmene = st;
        }

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
           // meniHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
              //  meniHeader.Visible = true;
              //  meniHeader.Visible = false;
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
            var select = "";
            string firma = Sifarnici.frmLogovanje.Firma;
            switch (firma)
            {
                case "Leget":
                    select = "select pnis.ID as ID, pnis.IDNAdredjenog as BrojDokumenta, ProdajniNalogIzvoz.Korisnik,  "+
" Kolicina, JM,pnis.KolicinaFormirana AS VecFormirana, TipKontenjera.SkNaziv as TipKontejnera, PArtnerji.PANAziv as Nalogodavac  , p2.PaNaziv as Brodar, p3.PaNaziv as Izvoznik , OpisPosla, BukingNumber, " +
" uvKvalitetKontejnera.Naziv as Kvalitet, ISNULL(Drumski,0) AS Drumski, "+
" (select Top 1 Naziv from Scenario where pnis.Scenario = Scenario.ID) as ScenarioNaziv, pnis.Scenario " +
" from ProdajniNalogIzvoz inner join ProdajniNalogIzvozStavke as pnis on ProdajniNalogIzvoz.ID = pnis.IDNAdredjenog  inner "+
" join TipKontenjera on TipKontenjera.ID = pnis.TipKontejnera " +
" inner join PArtnerji on Partnerji.PaSifra = ProdajniNalogIzvoz.Nalogodavac  inner "+
" join PArtnerji p2 on p2.PaSifra = ProdajniNalogIzvoz.Brodar "+
" inner join PArtnerji p3 on p3.PaSifra = ProdajniNalogIzvoz.Izvoznik "+
" inner join uvKvalitetKontejnera on uvKvalitetKontejnera.ID = pnis.KvalitetKontejnera  where StatusStavke = 'Otvoren'   ";
                    break;
                default:
                    select = "  ";
                    break;
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
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            this.gridGroupingControl1.TableDescriptor.Columns["Drumski"].Appearance.AnyRecordFieldCell.CellType = "CheckBox";

            //To set '1' and '0' instead of "True" and "False" 
            this.gridGroupingControl1.TableDescriptor.Columns["Drumski"].Appearance.AnyRecordFieldCell.CheckBoxOptions = new GridCheckBoxCellInfo("1", "0", "", true);
            this.gridGroupingControl1.TableDescriptor.Columns["Drumski"].Appearance.AnyRecordFieldCell.ReadOnly = false;
            this.gridGroupingControl1.TableDescriptor.Columns["Drumski"].Appearance.AnyRecordFieldCell.Enabled = true;

            //foreach (var column in gridGroupingControl1.TableDescriptor.Columns)
            //{
            //    MessageBox.Show(column.Name);
            //}
            // kolone koje ne želim da se vide
            var colsToRemove = new[] { "Scenario" }; 
            foreach (var col in colsToRemove)
            {
                if (gridGroupingControl1.TableDescriptor.VisibleColumns.Contains(col))
                {
                    gridGroupingControl1.TableDescriptor.VisibleColumns.Remove(col);
                }
            }

            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);



        }

        private void frmProdajniNalogIzvozTabela_Load(object sender, EventArgs e)
        {
            RefreshGridControl();
            if (statusizmene == 0)
            {
                lblNaslov.Text = "LISTA OTVORENIH STAVKI";
            }
            else if (statusizmene == 1)
            {
                lblNaslov.Text = "IZMENA VREDNOSTI KOLICINE";
            }
            else if (statusizmene == 2)
            {
                lblNaslov.Text = "STORNIRANJE STAVKE";
            }
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            int id = 0;
            if (statusizmene == 2)
            {

                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    id = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID"));

                    InsertProdajniNalogIzvoz ipnk = new InsertProdajniNalogIzvoz();

                    DialogResult result = MessageBox.Show(
                    "Da li ste sigurni da želite da stornirate stavku?", // Message text
                    "Potvrdite", // Title
                    MessageBoxButtons.YesNoCancel, // Buttons
                    MessageBoxIcon.Question // Icon
                    );

                    // Handle the result based on user selection
                    if (result == DialogResult.Yes)
                    {
                        ipnk.UpdStornirajStavku(id);
                        // Add logic to save changes here
                    }


                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }
            }
            if (statusizmene == 0)
            {
                int IzborDrumski = 0;
                int IzborADR = 0;
                int IzborCerada = 1; // 1-platforma, 2 cerada
                bool postojiScenario = false;
                InsertProdajniNalogIzvoz drum = new InsertProdajniNalogIzvoz();
                InsertProdajniNalogIzvoz scin = new InsertProdajniNalogIzvoz();

                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    id = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID"));

                    //Pitanje za Drumski
                    //Pitanje za ADR
                    string a = gridGroupingControl1.Table.CurrentRecord.GetValue("ScenarioNaziv")?.ToString();
                    postojiScenario = !string.IsNullOrWhiteSpace(gridGroupingControl1.Table.CurrentRecord.GetValue("ScenarioNaziv")?.ToString());
                    int scenarioID = 0;

                    //if (postojiScenario == false)
                    //{ 
                    DialogResult result = Saobracaj.Pomocni.CustomMessageBox.Show(
                    "Da li je drumski prevoz U organizaciji Legeta", // Message text
                    "Potvrdite" // Icon
                    );

                    // Handle the result based on user selection
                    if (result == DialogResult.Yes)
                    {

                        IzborDrumski = 1;

                        drum.UpdDrumski(id, 1); // Promeni Drumski
                        gridGroupingControl1.Table.CurrentRecord.SetValue("Drumski", 1);
                        // Add logic to save changes here
                    }
                    else
                    {
                        IzborDrumski = 0;

                        drum.UpdDrumski(id, 0); // Promeni Drumski
                        gridGroupingControl1.Table.CurrentRecord.SetValue("Drumski", 0);
                    }

                    DialogResult result2 = Saobracaj.Pomocni.CustomMessageBox.Show(
                   "Da li je u pitanju ADR roba", // Message text
                   "Potvrdite"
                   );

                    // Handle the result based on user selection
                    if (result2 == DialogResult.Yes)
                    {

                        IzborADR = 1;
                        //ipnk.UpdStornirajStavku(id); // Promeni ADR
                        // Add logic to save changes here
                    }

                        using (NalogIzvozaZaOtpremu ni = new NalogIzvozaZaOtpremu())
                        {
                           
                            // Set the owner so it stays on top of the main form
                            ni.StartPosition = FormStartPosition.CenterParent;
                            var dr = ni.ShowDialog(this); // Modal dialog
                            if (dr == DialogResult.OK)
                            {
                                int izbor = ni.IzabranaOpcija;
                                string Izabrani = "";

                                if (izbor == 1)
                                {
                                    if (IzborADR == 0)
                                    {
                                        scenarioID = 13;
                                        scin.UpdScenario(id, scenarioID, IzborDrumski); // 
                                        Izabrani = "Izabrali ste Scenario 13 - PLATFORMA DIREKTNO PUN ";
                                        IzborCerada = 1;
                                        if (IzborDrumski == 1)
                                        {
                                            Izabrani += " sa Drumskim prevozom u organizaciji Legeta";
                                        }
                                    }
                                    else
                                    {
                                        scenarioID = 26;
                                        Izabrani = "Izabrali ste Scenario 26 (ADR) - PLATFORMA DIREKTNO PUN";
                                        scin.UpdScenario(id, scenarioID, IzborDrumski);
                                        IzborCerada = 1;
                                        if (IzborDrumski == 1)
                                        {
                                            Izabrani += " sa Drumskim prevozom u organizaciji Legeta";
                                        }
                                    }

                                }

                                if (izbor == 2)
                                {
                                    if (IzborADR == 0)
                                    {
                                        scenarioID = 7;
                                        scin.UpdScenario(id, scenarioID, IzborDrumski); //
                                        Izabrani = "Izabrali ste Scenario 7 - PLATFORMA PRAZAN - PUN ";
                                        IzborCerada = 1;
                                        if (IzborDrumski == 1)
                                        {
                                            Izabrani += " sa Drumskim prevozom u organizaciji Legeta";
                                        }
                                    }
                                    else
                                    {
                                        scenarioID = 23;
                                        scin.UpdScenario(id, scenarioID, IzborDrumski); //
                                        Izabrani = "Izabrali ste Scenario 23 (ADR) - PLATFORMA PRAZAN - PUN ";
                                        IzborCerada = 1;
                                        if (IzborDrumski == 1)
                                        {
                                            Izabrani += " sa Drumskim prevozom u organizaciji Legeta";
                                        }
                                    }

                                }

                                if (izbor == 3)
                                {
                                    if (IzborADR == 0)
                                    {
                                        scenarioID = 8;
                                        scin.UpdScenario(id, scenarioID, IzborDrumski); //
                                        Izabrani = "Izabrali ste Scenario 8 - CERADA PRETOVAR PUN ";
                                        IzborCerada = 2;
                                        if (IzborDrumski == 1)
                                        {
                                            Izabrani += " sa Drumskim prevozom u organizaciji Legeta";
                                        }
                                    }
                                    else
                                    {
                                        scenarioID = 24;
                                        scin.UpdScenario(id, scenarioID, IzborDrumski); //
                                        Izabrani = "Izabrali ste Scenario 24 (ADR) - CERADA PRETOVAR PUN ";
                                        IzborCerada = 2;
                                        if (IzborDrumski == 1)
                                        {
                                            Izabrani += " sa Drumskim prevozom u organizaciji Legeta";
                                        }
                                    }

                                }

                                if (izbor == 4)
                                {
                                    if (IzborADR == 0)
                                    {
                                        scenarioID = 9;
                                        scin.UpdScenario(id, scenarioID, IzborDrumski); //
                                        Izabrani = "Izabrali ste Scenario 9 - CERADA SKLADISTE PUN";
                                        IzborCerada = 2;
                                        if (IzborDrumski == 1)
                                        {
                                            Izabrani += " sa Drumskim prevozom u organizaciji Legeta";
                                        }

                                    }
                                    else
                                    {
                                        scenarioID = 25;
                                        scin.UpdScenario(id, scenarioID, IzborDrumski); //
                                        Izabrani = "Izabrali ste Scenario 25 (ADR) - CERADA SKLADISTE PUN";
                                        IzborCerada = 2;
                                        if (IzborDrumski == 1)
                                        {
                                            Izabrani += " sa Drumskim prevozom u organizaciji Legeta";
                                        }
                                    }
                                }
                            CustomOkMessageBox.Show(" " + Izabrani.ToString());
                        }

                       
                    }

                  //  RefreshGridControl();


                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                    // }

                    decimal unetaKolicina;

                    var currentRec = gridGroupingControl1.Table.CurrentRecord;
                    if (currentRec == null) return;

                    decimal gornjaGranica = Convert.ToDecimal(currentRec.GetValue("Kolicina"));
                    var val = currentRec?.GetValue("Scenario");
                    int drumski = Convert.ToInt32(currentRec.GetValue("Drumski")); 

                    if (scenarioID <= 0 && val != null && val != DBNull.Value)
                    {
                        int.TryParse(val.ToString(), out scenarioID);
                    }

                   


                    string korisnik_zaBazu = Sifarnici.frmLogovanje.user;
                    int brojStavkePorudzbeniceID = id;
                    //int brojStavkePorudzbeniceID = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("BrojDokumenta"));
                    //if (Saobracaj.Pomocni.CustomTextBox.Show("Za koliko kontejnera želite da napravite grupni nalog? ", out unetaKolicina, gornjaGranica, "Potvrdite količinu") == DialogResult.OK)
                    //{
                    //    try
                    //    {

                    //        bool success = scin.UpdKolicinaStavkeVecFormirane(brojStavkePorudzbeniceID, unetaKolicina);
                    //        if (!success) // jos jedna provera u bayi smem li updejtovati kolicinu
                    //        {
                    //            MessageBox.Show(
                    //                "Uneta količina premašuje preostalu dozvoljenu količinu.",
                    //                "Upozorenje",
                    //                MessageBoxButtons.OK,
                    //                MessageBoxIcon.Warning);
                    //        }

                    //        List<int> listNoviIDs = new List<int>();
                    //        InsertIzvoz izv = new InsertIzvoz();

                    //        for (int i = 0; i < (int)unetaKolicina; i++)
                    //        {
                    //            int noviID = izv.InsIzvozPorudzbenica(brojStavkePorudzbeniceID, korisnik_zaBazu);
                    //            listNoviIDs.Add(noviID);
                    //        }

                    //        frmGrupniUnosPoljaIzvoz gpu = new frmGrupniUnosPoljaIzvoz(brojStavkePorudzbeniceID, listNoviIDs, scenarioID, drumski);
                    //        gpu.ShowDialog();
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MessageBox.Show("Greška pri upisu: " + ex.Message);
                    //    }
                    //    finally
                    //    {
                    //        // Osiguravamo da se Refresh desi na UI niti nakon svega
                    //        this.BeginInvoke((MethodInvoker)delegate
                    //        {
                    //            RefreshGridControl();
                    //        });
                    //    }
                    //}

                    frmGrupniUnosPoljaIzvoz gpu = new frmGrupniUnosPoljaIzvoz(brojStavkePorudzbeniceID, scenarioID, drumski, IzborCerada);
                    gpu.ShowDialog();
                }
            }



        }
     

        private void gridGroupingControl1_TableControlCurrentCellChanging(object sender, GridTableControlCancelEventArgs e)
        {
          
        }

        private void gridGroupingControl1_TableControlCurrentCellEditingComplete(object sender, GridTableControlEventArgs e)
        {
            int id = 0;
            double Kolicina = 0;
            try
            {
                if (statusizmene == 0)
                {
                    return;
                }
                else if (statusizmene == 1)
                {
                    if (gridGroupingControl1.Table.CurrentRecord != null)
                    {
                        id = Convert.ToInt32(gridGroupingControl1.Table.CurrentRecord.GetValue("ID"));
                        Kolicina = Convert.ToDouble(gridGroupingControl1.Table.CurrentRecord.GetValue("Kolicina"));
                        InsertProdajniNalogIzvoz ipnk = new InsertProdajniNalogIzvoz();
                        ipnk.UpdKolicinaStavke(id, Kolicina);

                        // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                    }
                }
               



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
