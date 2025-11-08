using Saobracaj.MainLeget;
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
    public partial class LogistikaIzvoza2 : Form
    {
        
        public LogistikaIzvoza2()
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
    }
}

