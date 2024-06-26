﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmAutomobiliKvarovi : Syncfusion.Windows.Forms.Office2010Form
    {
        public frmAutomobiliKvarovi()
        {
            InitializeComponent();
        }


        private void frmAutomobiliKvarovi_Load(object sender, EventArgs e)
        {
            var select = "  SELECT     Automobili.RegBr, EvidencijaKvarovaAuto.Automobil, KvaroviAuto.Naziv as KVar, GrupaKvarovaAuto.Naziv AS Grupa, Delavci.DeSifra, Delavci.DeIme,Delavci.DePriimek,  " +
                   "   EvidencijaKvarovaAuto.Promenio, EvidencijaKvarovaAuto.Kritican, EvidencijaKvarovaAuto.DatumPrijave, EvidencijaKvarovaAuto.Prijavio, " +
                   "   EvidencijaKvarovaAuto.Kvar, EvidencijaKvarovaAuto.DatumPromene, EvidencijaKvarovaAuto.Napomena, EvidencijaKvarovaAuto.AutomobilId " +
                   "   FROM         EvidencijaKvarovaAuto INNER JOIN " +
                   "   Automobili ON EvidencijaKvarovaAuto.AutomobilID = Automobili.ID INNER JOIN " +
                   "   KvaroviAuto ON EvidencijaKvarovaAuto.Kvar = KvaroviAuto.ID INNER JOIN " +
                   "   GrupaKvarovaAuto ON KvaroviAuto.GrupaKvarovaID = GrupaKvarovaAuto.ID INNER JOIN " +
                   "   Delavci ON EvidencijaKvarovaAuto.Prijavio = Delavci.DeSifra ";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
    }
}
