﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

using Microsoft.Reporting.WinForms;

namespace Saobracaj.Dokumenta
{
    public partial class frmAutomobiliKvarovi : Form
    {
        public frmAutomobiliKvarovi()
        {
            InitializeComponent();
            IdGrupe();
            IdForme();
            PravoPristupa();
        }
        public static string code = "frmAutomobiliKvarovi";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();


        public int IdGrupe()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            //Sifarnici.frmLogovanje frm = new Sifarnici.frmLogovanje();         
            string query = "Select IdGrupe from KorisnikGrupa Where Korisnik = " + "'" + Kor.TrimEnd() + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idGrupe = Convert.ToInt32(dr["IdGrupe"].ToString());
            }
            conn.Close();
            return idGrupe;
        }
        private int IdForme()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select IdForme from Forme where Rtrim(Code)=" + "'" + code + "'";
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idForme = Convert.ToInt32(dr["IdForme"].ToString());
            }
            conn.Close();
            return idForme;
        }
        private void PravoPristupa()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            string query = "Select * From GrupeForme Where IdGrupe=" + idGrupe + " and IdForme=" + idForme;
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows == false)
            {
                MessageBox.Show("Nemate prava za pristup ovoj formi", code);
                Pravo = false;
            }
            else
            {
                Pravo = true;
                while (reader.Read())
                {
                    insert = Convert.ToBoolean(reader["Upis"]);
                    if (insert == false)
                    {
                        //tsNew.Enabled = false;
                    }
                    update = Convert.ToBoolean(reader["Izmena"]);
                    if (update == false)
                    {
                        //tsSave.Enabled = false;
                    }
                    delete = Convert.ToBoolean(reader["Brisanje"]);
                    if (delete == false)
                    {
                        //tsDelete.Enabled = false;
                    }
                }
            }

            conn.Close();
        }
        private void frmAutomobiliKvarovi_Load(object sender, EventArgs e)
        {
            var select = "  SELECT     Automobili.RegBr, EvidencijaKvarovaAuto.Automobil, KvaroviAuto.Naziv as KVar, GrupaKvarovaAuto.Naziv AS Grupa, Delavci.DeSifra, Delavci.DePriimek, Delavci.DeIme, " +
                   "   EvidencijaKvarovaAuto.Promenio, EvidencijaKvarovaAuto.Kritican, EvidencijaKvarovaAuto.DatumPrijave, EvidencijaKvarovaAuto.Prijavio, " +
                   "   EvidencijaKvarovaAuto.Kvar, EvidencijaKvarovaAuto.DatumPromene, EvidencijaKvarovaAuto.Napomena, EvidencijaKvarovaAuto.AutomobilId "+
                   "   FROM         EvidencijaKvarovaAuto INNER JOIN " +
                   "   Automobili ON EvidencijaKvarovaAuto.AutomobilID = Automobili.ID INNER JOIN " +
                   "   KvaroviAuto ON EvidencijaKvarovaAuto.Kvar = KvaroviAuto.ID INNER JOIN " +
                   "   GrupaKvarovaAuto ON KvaroviAuto.GrupaKvarovaID = GrupaKvarovaAuto.ID INNER JOIN " +
                   "   Delavci ON EvidencijaKvarovaAuto.Prijavio = Delavci.DeSifra ";

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
    }
}