using Saobracaj.MainLeget;
using Saobracaj.TrackModal.Sifarnici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Testiranje.Sifarnici;

namespace Saobracaj.MainLeget.LegNew.Podesavanjesistema
{
    public partial class Podesavanje1 : Form
    {
        string Korisnik = Sifarnici.frmLogovanje.user;
        public Podesavanje1()
        {
            InitializeComponent();
           // OpenInPanel2(new MainLeget.Info(OpenInPanel2));

           // btnLogistikaUvoza.Click += (s, e) => OpenInPanel2(new MainLeget.UvozMain(OpenInPanel2));
        }/*
        public void OpenInPanel2(Form child)
        {
            if(_activeChild != null)
            {
                _activeChild.Close();
                _activeChild.Dispose();
                _activeChild = null;
            }
            _activeChild = child;
            child.TopLevel = false;
            child.FormBorderStyle=FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            child.AutoScaleMode=AutoScaleMode.None;

            splitContainer2.Panel2.Controls.Clear();
            splitContainer2.Panel2.Controls.Add(child);
            child.Show();
            child.BringToFront();
            child.Focus();
        }
        */

        private void MainLeget_Load(object sender, EventArgs e)
        {
           // Paneli();
        }
      
        private void MainForm_ResizeRedraw(object sender, EventArgs e)
        {

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
           //OpenInPanel2(new MainLeget.Info(OpenInPanel2));
        }

        private void btnLogistikaUvoza_Click(object sender, EventArgs e)
        {
            //panel1.Visible = false;
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
           
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogistikaIzvozaPM1_Click(object sender, EventArgs e)
        {
            Saobracaj.TrackModal.Sifarnici.frmZone zone = new TrackModal.Sifarnici.frmZone();
            zone.Show();
        }

        private void btnPodesavanja_Click(object sender, EventArgs e)
        {

        }

        private void btnLogistikaIzvozaPM2_Click(object sender, EventArgs e)
        {
            Saobracaj.TrackModal.Sifarnici.SkladisteGrupa jm = new SkladisteGrupa();
            jm.Show();
        }

        private void btnLogistikaIzvozaPM3_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmSkladista")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmSkladista sklad = new frmSkladista(Korisnik);
                sklad.Show();
            }
        }

        private void btnLogistikaIzvozaPM4_Click(object sender, EventArgs e)
        {

            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPozicija")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                frmPozicija poz = new frmPozicija(Korisnik);
                poz.Show();
            }
        }

        private void btnLogistikaIzvozaPM5_Click(object sender, EventArgs e)
        {
            frmTipKontejnera tk = new frmTipKontejnera(Korisnik);
            tk.Show();
        }

        private void sfButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void sfPartnerji_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            bool bFormNameOpen = false;
            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == "frmPArtnerji")
                {
                    bFormNameOpen = true;
                    frm.Activate();
                    frm.WindowState = FormWindowState.Normal;
                }
            }
            if (bFormNameOpen == false)
            {
                Sifarnici.frmPartnerji poz = new Sifarnici.frmPartnerji();
                poz.Show();
            }
        }
    }
}

