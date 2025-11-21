using Microsoft.Ajax.Utilities;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.IdentityModel.Metadata;
using System.Security.Cryptography.Xml;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Security.Cryptography;

namespace Saobracaj.Izvoz
{
    public partial class frmProdajniNalogIzvozTabela : Form
    {
        int statusizmene = 0; // 0 - Readonly 1- Updejt status
        public frmProdajniNalogIzvozTabela(int st)
        {
            InitializeComponent();
            statusizmene = st;
        }

        private void RefreshGridControl()
        {
            var select = "";
            string firma = Sifarnici.frmLogovanje.Firma;
            switch (firma)
            {
                case "Leget":
                    select = "select ProdajniNalogIzvozStavke.ID as ID, ProdajniNalogIzvozStavke.IDNAdredjenog as BrojDokumenta, ProdajniNalogIzvoz.Korisnik,  Kolicina, JM, TipKontenjera.SkNaziv as TipKontejnera, PArtnerji.PANAziv as Nalogodavac " +
                " , p2.PaNaziv as Brodar, p3.PaNaziv as Izvoznik , OpisPosla, BukingNumber, uvKvalitetKontejnera.Naziv as Kvalitet from ProdajniNalogIzvoz " +
                " inner join ProdajniNalogIzvozStavke on ProdajniNalogIzvoz.ID = ProdajniNalogIzvozStavke.IDNAdredjenog " +
                " inner   join TipKontenjera on TipKontenjera.ID = ProdajniNalogIzvozStavke.TipKontejnera " +
                " inner   join PArtnerji on Partnerji.PaSifra = ProdajniNalogIzvoz.Nalogodavac " +
                " inner   join PArtnerji p2 on p2.PaSifra = ProdajniNalogIzvoz.Brodar " +
                " inner   join PArtnerji p3 on p3.PaSifra = ProdajniNalogIzvoz.Izvoznik " +
                " inner     join uvKvalitetKontejnera on uvKvalitetKontejnera.ID = ProdajniNalogIzvozStavke.KvalitetKontejnera " +
                " where StatusStavke = 'Otvoren'  ";
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

            //foreach (var column in gridGroupingControl1.TableDescriptor.Columns)
            //{
            //    MessageBox.Show(column.Name);
            //}

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
