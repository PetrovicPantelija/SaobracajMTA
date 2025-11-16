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
        public frmProdajniNalogIzvozTabela()
        {
            InitializeComponent();
        }

        private void RefreshGridControl()
        {
            var select = "";
            string firma = Sifarnici.frmLogovanje.Firma;
            switch (firma)
            {
                case "Leget":
                    select = "select ProdajniNalogIzvozStavke.ID, ProdajniNalogIzvozStavke.IDNAdredjenog, ProdajniNalogIzvoz.Korisnik,  Kolicina, JM, TipKontenjera.SkNaziv as TipKontejnera, PArtnerji.PANAziv as Nalogodavac " +
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
        }
    }
}
