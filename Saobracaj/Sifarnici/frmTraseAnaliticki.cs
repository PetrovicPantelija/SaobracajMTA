using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using Syncfusion.Windows.Forms.Grid.Grouping;

namespace Saobracaj.Sifarnici
{
    public partial class frmTraseAnaliticki :  Syncfusion.Windows.Forms.Office2010Form
    {
        public frmTraseAnaliticki()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcwMDg5QDMxMzgyZTM0MmUzMFhQSmlDM0M2bGpxcXVtT1VScTg1a0dtVTFLcUZiK0tLRnpvRTYyRFpMc3M9");
            InitializeComponent();
        }

        private void frmTraseAnaliticki_Load(object sender, EventArgs e)
        {
            var select = "  Select Trase.ID, Trase.Godina, Trase.Pocetna as PocetnaID, stanice.Opis as Pocetna, Trase.Krajnja as KrajnjaID,st2.Opis as Krajnja, Trase.Relacija, Trase.OpisRelacije, Trase.Voz as Trasa, Trase.Rang, " +
            " Trase.TezinaVoza, Trase.TezinaLokomotive, Trase.DuzinaVoza, Trase.ProcenatKocenja, Trase.Cena, Trase.CenaKalk, Trase.Rastojanje, Trase.RastojanjeMag, Trase.RastojanjeReg, Trase.RastojanjeLok, Trase.NajveciOtpor, Trase.NajkraciKol, Trase.OsovinskoOpter " +
            " ,Trase.TipED, Trase.ElektroKM, Trase.DizelKM, Trase.PrevoznoRastojanje from Trase " +
            " inner join stanice on Trase.Pocetna = stanice.ID " +
            " inner join stanice st2 on Trase.Krajnja = st2.ID";

            var s_connection = ConfigurationManager.ConnectionStrings["Saobracaj.Properties.Settings.Perftech_BeogradConnectionString"].ConnectionString;
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
        }
    }
}
