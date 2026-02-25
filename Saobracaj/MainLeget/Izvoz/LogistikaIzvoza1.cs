using Saobracaj.MainLeget;
using Syncfusion.WinForms.Controls;
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

namespace Saobracaj.MainLeget.LegNew
{
    public partial class LogistikaIzvoza1 : Form
    {
        
        public LogistikaIzvoza1()
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
            //var main = this.TopLevelControl as NewMain;
            //if (main == null) return;

            //main.OtvoriFormuSaPravom(
            //    btnLogistikaIzvozaPM1.Text,
            //    () => new LogistikaIzvoza2() 

            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnLogistikaIzvozaPM1.Text,
                () => new Saobracaj.Izvoz.frmProdajniNalogIzvoz()
            );
        }
        

        private void btnPodesavanja_Click(object sender, EventArgs e)
        {

        }

        private void btnLogistikaIzvozaPM2_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as NewMain;
            if (main == null) return;

            main.OtvoriFormuSaPravom(
                btnLogistikaIzvozaPM2.Text,
                () => new LogistikaIzvoza2()
            );
        }

        private void btnLogistikaIzvozaPM3_Click(object sender, EventArgs e)
        {

        }

        private void btnLogistikaIzvozaPM4_Click(object sender, EventArgs e)
        {

        }

        private void btnLogistikaIzvozaPM5_Click(object sender, EventArgs e)
        {

        }

        private void btnLogistikaIzvozaPM6_Click(object sender, EventArgs e)
        {

        }
    }
}

