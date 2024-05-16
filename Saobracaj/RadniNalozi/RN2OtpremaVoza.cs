using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
//
namespace Saobracaj.RadniNalozi
{
    public partial class RN2OtpremaVoza : Syncfusion.Windows.Forms.Office2010Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        private bool status = false;
        public RN2OtpremaVoza()
        {
            InitializeComponent();
            FillGV();
            FillCombo();
        }

        public RN2OtpremaVoza(string Korisnik, string IDVOza, string IDUsluge, string OtpremaID)
        {
            InitializeComponent();
            FillGV();
            FillCombo();
            txtNalogIzdao.Text = Korisnik;
            cboNaSredstvo.SelectedValue = Convert.ToInt32(IDVOza);
            cboUsluga.SelectedValue = Convert.ToInt32(IDUsluge);
            txtOtpremaID.Text = OtpremaID;
           // RefreshStavkeVoza(OtpremaID);

        }
        /*
        private void RefreshStavkeVoza(string OtpremaID)
        {
            if (cboUsluga.SelectedValue is null)
            {
                MessageBox.Show("Izaberite uslugu");
                return;
            }
            
            Select BookingBrodara, BrodskaPlomba, Brodar, PeriodSkladistenjaOd, PeriodSkladistenjaDo, NetoRobe, BrutoRobe, BrutoRobeO, CBMO, BrojKoletaO, OstalePlombe, Tara, VGMBrod2 from IzvozKonacna Where ID = " + txtKOntejnerID.Text , con);
                                                                                                                                                                                       select IzvozKonacnaVrstaManipulacije.ID as ID, IzvozKonacnaVrstaManipulacije.IDNadredjena as KontejnerID," +
                " IzvozKonacna.BrojKontejnera," +
" IzvozKonacnaVrstaManipulacije.Kolicina,  VrstaManipulacije.ID as ManipulacijaID,VrstaManipulacije.Naziv as ManipulacijaNaziv, " +
" IzvozKonacnaVrstaManipulacije.Cena,OrganizacioneJedinice.ID,   OrganizacioneJedinice.Naziv as OrganizacionaJedinica,  " +
 "  Partnerji.PaSifra as NalogodavacID,PArtnerji.PaNaziv as Platilac" +
" from IzvozKonacnaVrstaManipulacije Inner    join VrstaManipulacije on VrstaManipulacije.ID = IzvozKonacnaVrstaManipulacije.IDVrstaManipulacije" +
" inner" +
" join PArtnerji on IzvozKonacnaVrstaManipulacije.Platilac = PArtnerji.PaSifra  inner " +
" join OrganizacioneJedinice on OrganizacioneJedinice.ID = IzvozKonacnaVrstaManipulacije.OrgJed " +
" inner " +
" join IzvozKonacna on IzvozKonacnaVrstaManipulacije.IDNadredjena = IzvozKonacna.ID where IzvozKonacna.ID = " + Convert.ToInt32(txtKOntejnerID.Text);
            
            var select = "  SELECT OtpremaKontejneraVozStavke.[ID],[IDNadredjenog]       ,[BrojKontejnera],[BrojVagona], " +
             "  KontejnerID " +
             " FROM [dbo].[OtpremaKontejneraVozStavke] " +
     " inner join IzvozKonacnaVrstaManipulacije on IzvozKonacnaVrstaManipulacije.IDNAdredjena = OtpremaKontejneraVozStavke.KontejnerID " +
     " where IdNadredjenog = " + OtpremaID + " and  IZvozKonacnaVrstaManipulacije.IDVrstaManipulacije = " + cboUsluga.SelectedValue + " order by RB";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;

            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }
        */

        private void FillGV()
        {
            var select = "Select * From RNOtpremaVoza order by ID desc";
            SqlConnection conn = new SqlConnection(connect);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
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
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var TipKontenjera = "Select ID,SkNaziv from TipKontenjera order by ID";
            var daTK = new SqlDataAdapter(TipKontenjera, conn);
            var daDS = new DataSet();
            daTK.Fill(daDS);
            cboVrstaKontejnera.DataSource = daDS.Tables[0];
            cboVrstaKontejnera.DisplayMember = "SkNaziv";
            cboVrstaKontejnera.ValueMember = "ID";

            txtNalogIzdao.Text = Sifarnici.frmLogovanje.user.ToString().TrimEnd();
            //usluge->Manipulacije

            var vSredstvo = "select ID,(CONVERT(NVARCHAR,BrVoza) + ' /' +Relacija) As Opis from Voz order by ID desc";
            var daVS = new SqlDataAdapter(vSredstvo, conn);
            var dsVS = new DataSet();
            daVS.Fill(dsVS);
            cboNaSredstvo.DataSource = dsVS.Tables[0];
            cboNaSredstvo.DisplayMember = "Opis";
            cboNaSredstvo.ValueMember = "ID";

            var partner7 = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaSifra";
            var partAD7 = new SqlDataAdapter(partner7, conn);
            var partDS7 = new DataSet();
            partAD7.Fill(partDS7);
            cboBrodar.DataSource = partDS7.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";

            var roba = "Select ID,Naziv From NHM order by ID";
            var daRoba = new SqlDataAdapter(roba, conn);
            var dsRoba = new DataSet();
            daRoba.Fill(dsRoba);
            cboRoba.DataSource = dsRoba.Tables[0];
            cboRoba.DisplayMember = "Naziv";
            cboRoba.ValueMember = "ID";

            var usluge = "Select ID,Naziv from VrstaManipulacije order by ID";
            var daUsluge = new SqlDataAdapter(usluge, conn);
            var dsUsluge = new DataSet();
            daUsluge.Fill(dsUsluge);
            cboUsluga.DataSource = dsUsluge.Tables[0];
            cboUsluga.DisplayMember = "Naziv";
            cboUsluga.ValueMember = "ID";

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
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
        }
        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertRN rn = new InsertRN();
            if (status == true)
            {
                rn.InsRNOtpremaVoza(Convert.ToDateTime(txtDatumRasporeda.Value.ToString()), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value.ToString()), Convert.ToInt32(cboNaSredstvo.SelectedValue), txtBrojPlombe.Text.ToString().TrimEnd(),
                    txtBrojVagona.Text.ToString().TrimEnd(), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboRoba.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text.ToString().TrimEnd());

            }
            else
            {
                rn.UpdRNOtpremaVoza(Convert.ToInt32(txtID.Text.ToString().TrimEnd()), Convert.ToDateTime(txtDatumRasporeda.Value.ToString()), txtbrojkontejnera.Text.ToString().TrimEnd(), Convert.ToInt32(cboVrstaKontejnera.SelectedValue),
                    txtNalogIzdao.Text.ToString().TrimEnd(), Convert.ToDateTime(txtDatumRealizacije.Value.ToString()), Convert.ToInt32(cboNaSredstvo.SelectedValue), txtBrojPlombe.Text.ToString().TrimEnd(),
                    txtBrojVagona.Text.ToString().TrimEnd(), Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboRoba.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue),
                    Convert.ToInt32(cboUsluga.SelectedValue), txtNalogRealizovao.Text.ToString().TrimEnd(), txtNapomena.Text.ToString().TrimEnd());
            }
            FillGV();
            status = false;
        }
        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                InsertRN rn = new InsertRN();
                rn.DelRNOtpremaVoza(Convert.ToInt32(txtID.Text));
                FillGV();
            }
            else
            {
                MessageBox.Show("Izaberite broj zapisa!");
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Selected)
                    {
                        txtID.Text = row.Cells[0].Value.ToString();
                    }
                }
            }
            catch { }
        }
        private void txtDatumRasporeda_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {  DialogResult dialogResult = MessageBox.Show("Formirace se RN za sve Interne Naloge po Vrsti usluge ", "Some Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
            RadniNalozi.InsertRN ir = new InsertRN();
            //PANTA
            ir.InsRNOtpremaVozaCeoVoz(Convert.ToDateTime(txtDatumRasporeda.Value), txtNalogIzdao.Text, Convert.ToDateTime(txtDatumRealizacije.Text), Convert.ToInt32(cboNaSredstvo.SelectedValue), Convert.ToInt32(cboSaSklad.SelectedValue), Convert.ToInt32(cboSaPoz.SelectedValue), Convert.ToInt32(cboUsluga.SelectedValue), "", txtNapomena.Text, Convert.ToInt32(txtOtpremaID.Text));
            FillGV();
   }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }
          
         

        private void RN2OtpremaVoza_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            InsertRN up = new InsertRN();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected == true)
                {
                    up.PotvrdiUradjenRN2(Convert.ToInt32(row.Cells[0].Value.ToString()));
                }

            }
        }
    }
}
