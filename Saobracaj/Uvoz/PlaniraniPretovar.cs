using Saobracaj.RadniNalozi;
using Saobracaj.Sifarnici;
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

namespace Saobracaj.Uvoz
{
    public partial class PlaniraniPretovar : Form
    {
        string korisnik = frmLogovanje.user;
        string s_connection = Sifarnici.frmLogovanje.connectionString;

        public PlaniraniPretovar()
        {
            InitializeComponent();
        }
        private void Fill()
        {
            var select = "SELECT RadniNalogInterni.[ID]  ,RadniNalogInterni.[StatusIzdavanja]  ,[OJIzdavanja], o1.Naziv as Izdao,[OJRealizacije],o2.Naziv as Realizuje,[DatumIzdavanja]" +
                ",[DatumRealizacije]  ,RadniNalogInterni.[Napomena]  , UvozKonacnaVrstaManipulacije.IDVrstaManipulacije,VrstaManipulacije.Naziv,[Uradjen]  ,[Osnov]  ,[BrojOsnov]  ," +
                "[KorisnikIzdao]      ,[KorisnikZavrsio]  ,UvozKonacna.BrojKontejnera, uv.PaNaziv as Platilac  ,TipKontenjera.Naziv as Tipkontejnera, PlanID as PlanUtovara, " +
                "RadniNalogInterni.Pokret, KontejnerStatus.Naziv  " +
                "FROM[RadniNalogInterni] " +
                "inner join OrganizacioneJedinice as o1 on OjIzdavanja = O1.ID " +
                "inner join OrganizacioneJedinice as o2 on OjRealizacije = O2.ID  " +
                "inner join UvozKonacna on UvozKonacna.ID = BrojOsnov  " +
                "inner join UvozKonacnaVrstaManipulacije on UvozKonacnaVrstaManipulacije.ID = RadniNalogInterni.KonkretaIDUsluge  " +
                "inner join VrstaManipulacije on VrstaManipulacije.ID = UvozKonacnaVrstaManipulacije.IDVrstaManipulacije  " +
                "inner join Partnerji uv on uv.PaSifra = UvozKonacnaVrstaManipulacije.Platilac  " +
                " Inner join TipKontenjera on TipKontenjera.ID = UvozKonacna.TipKontejnera  " +
                "Inner join KontejnerStatus on KontejnerStatus.ID = RadniNalogInterni.StatusKontejnera  " +
                "Where KontejnerStatus.ID=3 or KontejnerStatus.ID=4 order by KontejnerStatus.ID asc";
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(s_connection);
            var sklad = "select ID,naziv from Skladista";
            var daSklad = new SqlDataAdapter(sklad, conn);
            var dsSklad = new DataSet();
            daSklad.Fill(dsSklad);
            cboSaSklad.DataSource = dsSklad.Tables[0];
            cboSaSklad.DisplayMember = "Naziv";
            cboSaSklad.ValueMember = "ID";

            var pozicija = "Select Id,Opis from Pozicija";
            var daPoz = new SqlDataAdapter(pozicija, conn);
            var dsPoz = new DataSet();
            daPoz.Fill(dsPoz);
            cboSaPoz.DataSource = dsPoz.Tables[0];
            cboSaPoz.DisplayMember = "Opis";
            cboSaPoz.ValueMember = "ID";
        }
        private void PlaniraniPretovar_Load(object sender, EventArgs e)
        {
            Fill();
            FillCombo();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        string Usluga;
        private void gridGroupingControl1_TableControlCellClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtID.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                    txtKontejner.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojKontejnera").ToString();
                    txtPlanUtovara.Text= gridGroupingControl1.Table.CurrentRecord.GetValue("PlanUtovara").ToString();
                    txtOsnov.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("BrojOsnov").ToString();
                    Usluga= gridGroupingControl1.Table.CurrentRecord.GetValue("IDVrstaManipulacije").ToString();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        string BrojPlombe;
        int Brodar,Spedicija,VrstaPregleda,CariniskiPostupak;
        private void VratiUvozKonacna()
        {
            SqlConnection conn = new SqlConnection(s_connection);

            conn.Open();

            SqlCommand cmd = new SqlCommand("Select BrojPlombe1,Brodar,SpedicijaRTC,VrstaPregleda,CarinskiPostupak From UvozKonacna Where ID="+Convert.ToInt32(txtOsnov.Text),conn);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                BrojPlombe = dr["BrojPlombe1"].ToString();
                Brodar = Convert.ToInt32(dr["Brodar"].ToString());
                Spedicija = Convert.ToInt32(dr["SpedicijaRTC"].ToString());
                VrstaPregleda = Convert.ToInt32(dr["VrstaPregleda"].ToString());
                CariniskiPostupak = Convert.ToInt32(dr["CarinskiPostupak"].ToString());
            }

            conn.Close();
            dr.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Prijemnica frm = new Prijemnica(Convert.ToInt32(txtID.Text),txtKontejner.Text.ToString().TrimEnd(),Convert.ToInt32(cboSaSklad.SelectedValue),Convert.ToInt32(cboSaPoz.SelectedValue));
            frm.Show();
        }

        int PrijemID;
        private void VratiPrijemID()
        {
            SqlConnection conn = new SqlConnection(s_connection);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Max(ID) from PrijemKontejneraVoz",conn);
            SqlDataReader dr=cmd.ExecuteReader();
            while(dr.Read())
            {
                PrijemID = Convert.ToInt32(dr[0].ToString());
            }
            conn.Close();
            dr.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            VratiUvozKonacna();
            Dokumeta.InsertPrijemKontejneraVoz ins = new Dokumeta.InsertPrijemKontejneraVoz();
            ins.InsertPrijemKontVoz(Convert.ToDateTime(dateTimePicker1.Value.ToString()), 0, 0, Convert.ToDateTime(dateTimePicker1.MinDate.ToString()), DateTime.Now, korisnik, txtReg.Text.ToString(), txtVozac.Text.ToString(),
                0, txtNapomena.Text.ToString(), 0, 0, 1, 0, 0, 0);
            InsertUvozKonacna ins2 = new InsertUvozKonacna();
            ins2.PrenesiKontejnerIzPlanaNaPrijemnicu(Convert.ToInt32(txtOsnov.Text));
            VratiPrijemID();
            RadniNalozi.InsertRN rn = new RadniNalozi.InsertRN();
            rn.InsRN9PrijmCiradeKam(Convert.ToDateTime(dateTimePicker1.Value), korisnik, Convert.ToDateTime(dateTimePicker1.MinDate), 0, Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue)
                , Convert.ToInt32(Usluga), "", "", PrijemID, txtReg.Text.ToString().TrimEnd(), CariniskiPostupak, VrstaPregleda, Spedicija, Brodar, BrojPlombe,Convert.ToInt32(txtID.Text));
        }
    }
}
