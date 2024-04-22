using System;
using System.Windows.Forms;

namespace Saobracaj.Dokumenta
{
    public partial class frmParametriObracuna : Form
    {
        bool status = false;
        public frmParametriObracuna()
        {
            InitializeComponent();

        }
        public static string code = "frmParametriObracuna";
        public bool Pravo;
        int idGrupe;
        int idForme;
        bool insert;
        bool update;
        bool delete;
        string Kor = Sifarnici.frmLogovanje.user.ToString();
        string niz = "";
        private void tsNew_Click(object sender, EventArgs e)
        {
            txtSifra.Text = "";
            txtSifra.Enabled = false;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {

            if (status == true)
            {
                Sifarnici.InsertParametri ins = new Sifarnici.InsertParametri();
                ins.InsParametri(Convert.ToDouble(txtMinimalna.Text));
                //RefreshDataGrid();
                status = false;
            }
            else
            {
                Sifarnici.InsertParametri upd = new Sifarnici.InsertParametri();
                upd.UpdParametri(Convert.ToInt32(txtSifra.Text), Convert.ToDouble(txtMinimalna.Text));
                status = false;
                txtSifra.Enabled = false;
                // RefreshDataGrid();
            }
        }
    }
}
