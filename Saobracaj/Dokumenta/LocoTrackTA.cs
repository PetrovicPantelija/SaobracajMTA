using Microsoft.ReportingServices.Diagnostics.Internal;
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


namespace Saobracaj.Dokumenta
{
    public partial class LocoTrackTA : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        public LocoTrackTA()
        {
            InitializeComponent();
            SetWebBrowserVersion(11001); // 11001 = IE11
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.ObjectForScripting = true;
            LoadMap();

        }
        private void SetWebBrowserVersion(int version)
        {
            const string keyName = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            string appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            Microsoft.Win32.Registry.SetValue(
                Microsoft.Win32.Registry.CurrentUser + "\\" + keyName,
                appName,
                version,
                Microsoft.Win32.RegistryValueKind.DWord);
        }
        private void LoadMap()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            string html=System.IO.File.ReadAllText(path+"/map.html");
            webBrowser1.DocumentText = html;
        }

        private void LocoTrackTA_Load(object sender, EventArgs e)
        {
            FillGV();
            FillCombo();
        }

        private void rbStandard_CheckedChanged(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("updateLayer", new object[] { "standard" });
        }

        private void rbSignals_CheckedChanged(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("updateLayer", new object[] { "signals" });
        }

        private void rbMaxSpeed_CheckedChanged(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("updateLayer", new object[] { "maxspeed" });
        }
        private void FillGV()
        {
            var query = "WITH RankedActivities AS (" +
                "SELECT AktivnostiStavke.Posao AS Posao,AktivnostiStavke.OznakaPosla AS OznakaPosla,Najava.PrevozniPut as PrevozniPut,VrstaAktivnosti.Naziv AS Aktivnost," +
                "SifVucaStatusi.Naziv AS Status,VucaStatusi.Vreme AS Vreme,AktivnostiStavke.Lokomotiva AS Lokomotiva,AktivnostiStavke.Stanica,Stanice.Longitude," +
                "Stanice.Latitude,Delavci.DeSifra,RTrim(Delavci.DeIme) + ' ' + RTrim(Delavci.DePriimek) AS Izvrsio," +
                "ROW_NUMBER() OVER (PARTITION BY AktivnostiStavke.Posao ORDER BY VucaStatusi.Vreme DESC) AS rn " +
                "FROM AktivnostiStavke " +
                "INNER JOIN Aktivnosti ON AktivnostiStavke.IDNadredjena = Aktivnosti.ID " +
                "INNER JOIN Delavci ON Aktivnosti.Zaposleni = Delavci.DeSifra " +
                "INNER JOIN VucaStatusi ON AktivnostiStavke.ID = VucaStatusi.StavkaAktivnostiID " +
                "INNER JOIN SifVucaStatusi ON VucaStatusi.Status = SifVucaStatusi.ID " +
                "INNER JOIN VrstaAktivnosti ON AktivnostiStavke.VrstaAktivnostiID = VrstaAktivnosti.ID " +
                "INNER JOIN Najava ON AktivnostiStavke.Posao = Najava.ID " +
                "INNER JOIN Stanice ON AktivnostiStavke.Stanica = Stanice.ID " +
                "WHERE Najava.faktura = '' AND VrstaAktivnosti.ID = 61) SELECT * FROM RankedActivities WHERE rn = 1;";

            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(query, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.AutoResizeColumnHeadersHeight();

            dataGridView1.Columns["PrevozniPut"].Visible = false;
            dataGridView1.Columns["Aktivnost"].Visible = false;
            dataGridView1.Columns["Status"].Visible = false;
            dataGridView1.Columns["Vreme"].Visible = false;
            dataGridView1.Columns["Lokomotiva"].Visible = false;
            dataGridView1.Columns["Stanica"].Visible = false;
            dataGridView1.Columns["Longitude"].Visible = false;
            dataGridView1.Columns["Latitude"].Visible = false;
            dataGridView1.Columns["DeSifra"].Visible = false;
            dataGridView1.Columns["Izvrsio"].Visible = false;


            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string oznakaPosla = row.Cells["OznakaPosla"].Value.ToString().TrimEnd();
                string lokomotiva = row.Cells["Lokomotiva"].Value.ToString().TrimEnd();
                string prevozniPut = row.Cells["PrevozniPut"].Value.ToString().TrimEnd();
                string status = row.Cells["Status"].Value.ToString().TrimEnd();
                string vreme = row.Cells["Vreme"].Value.ToString().TrimEnd();
                decimal longitude = Convert.ToDecimal(row.Cells["Longitude"].Value);
                decimal latitude = Convert.ToDecimal(row.Cells["Latitude"].Value);
                string izvrsio = row.Cells["Izvrsio"].Value.ToString().TrimEnd();

                webBrowser1.Document.InvokeScript("addLoco", new object[] { oznakaPosla, lokomotiva, prevozniPut, status, vreme, longitude, latitude, izvrsio });
            }
        }

        private void btnPozicije_Click(object sender, EventArgs e)
        {
            FillGV();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    decimal longitude = Convert.ToDecimal(row.Cells["Longitude"].Value);
                    decimal latitude = Convert.ToDecimal(row.Cells["Latitude"].Value);
                    webBrowser1.Document.InvokeScript("focusPin", new object[] { longitude, latitude });

                }
            }
        }
        private void FillCombo()
        {
            var select = "select distinct Lokomotiva From AktivnostiStavke inner join Najava on AktivnostiStavke.Posao=Najava.ID Where faktura='' order by Lokomotiva desc";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection conn = new SqlConnection(s_connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            da.Fill(ds);
            cboLokomotiva.DataSource = ds.Tables[0];
            cboLokomotiva.DisplayMember = "Lokomotiva";
            cboLokomotiva.ValueMember = "Lokomotiva";

            var select2 = "select ID,Oznaka from Najava Where Faktura=''";
            var da2 = new SqlDataAdapter(select2, conn);
            var ds2 = new System.Data.DataSet();
            da2.Fill(ds2);
            cboPosao.DataSource = ds2.Tables[0];
            cboPosao.DisplayMember = "Oznaka";
            cboPosao.ValueMember = "ID";
        }
    }
}
