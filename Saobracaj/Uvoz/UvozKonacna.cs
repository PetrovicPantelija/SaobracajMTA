using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace Saobracaj.Uvoz
{
    public partial class UvozKonacna : Form
    {
        public string connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
        string nalogodavci = "";
        string usluge = "";
        public UvozKonacna()
        {
            InitializeComponent();
            FillCheck();
            FillCombo();
            FillGV();
        }

        public UvozKonacna(int Sifra)
        {
            InitializeComponent();
            FillCheck();
            FillCombo();
            FillGV();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Uvoz uv = new Uvoz();
            uv.Show();
        }
        private void FillGV()
        {
            var select = "Select * From UvozKonacna order by ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connection);

            var dir = "Select ID,Naziv from PredefinisanePoruke order by ID";
            var dirAD = new SqlDataAdapter(dir, conn);
            var dirDS = new DataSet();
            dirAD.Fill(dirDS);
            cbDirigacija.DataSource = dirDS.Tables[0];
            cbDirigacija.DisplayMember = "Naziv";
            cbDirigacija.ValueMember = "ID";
            //carinski postupak
            var dir2 = "Select ID,Naziv from PredefinisanePoruke order by ID";
            var dirAD2 = new SqlDataAdapter(dir2, conn);
            var dirDS2 = new DataSet();
            dirAD2.Fill(dirDS2);
            cboCarinskiPostupak.DataSource = dirDS2.Tables[0];
            cboCarinskiPostupak.DisplayMember = "Naziv";
            cboCarinskiPostupak.ValueMember = "ID";
            //postupak roba/kont
            var dir3 = "Select ID,Naziv from PredefinisanePoruke order by ID";
            var dirAD3 = new SqlDataAdapter(dir3, conn);
            var dirDS3 = new DataSet();
            dirAD3.Fill(dirDS3);
            cbPostupak.DataSource = dirDS3.Tables[0];
            cbPostupak.DisplayMember = "Naziv";
            cbPostupak.ValueMember = "ID";
            //nacin pakovanja
            var dir4 = "Select ID,Naziv from PredefinisanePoruke order by ID";
            var dirAD4 = new SqlDataAdapter(dir4, conn);
            var dirDS4 = new DataSet();
            dirAD4.Fill(dirDS4);
            cbNacinPakovanja.DataSource = dirDS4.Tables[0];
            cbNacinPakovanja.DisplayMember = "Naziv";
            cbNacinPakovanja.ValueMember = "ID";
            //napomena pozicioniranje
            var dir5 = "Select ID,Naziv from PredefinisanePoruke order by ID";
            var dirAD5 = new SqlDataAdapter(dir5, conn);
            var dirDS5 = new DataSet();
            dirAD5.Fill(dirDS5);
            cbNapomenaPoz.DataSource = dirDS5.Tables[0];
            cbNapomenaPoz.DisplayMember = "Naziv";
            cbNapomenaPoz.ValueMember = "ID";

            var brod = "Select ID,Naziv From Brodovi order by ID";
            var brodAD = new SqlDataAdapter(brod, conn);
            var brodDS = new DataSet();
            brodAD.Fill(brodDS);
            cbBrod.DataSource = brodDS.Tables[0];
            cbBrod.DisplayMember = "Naziv";
            cbBrod.ValueMember = "ID";

            var car = "Select ID,Naziv From Carinarnice order by ID";
            var carAD = new SqlDataAdapter(car, conn);
            var carDS = new DataSet();
            carAD.Fill(carDS);
            cbOcarina.DataSource = carDS.Tables[0];
            cbOcarina.DisplayMember = "Naziv";
            cbOcarina.ValueMember = "ID";

            var partner = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            cbVlasnikKont.DataSource = partDS.Tables[0];
            cbVlasnikKont.DisplayMember = "PaNaziv";
            cbVlasnikKont.ValueMember = "PaSifra";
            //uvoznik
            var partner2 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD2 = new SqlDataAdapter(partner2, conn);
            var partDS2 = new DataSet();
            partAD2.Fill(partDS2);
            cboUvoznik.DataSource = partDS2.Tables[0];
            cboUvoznik.DisplayMember = "PaNaziv";
            cboUvoznik.ValueMember = "PaSifra";
            //spedicija na granici
            var partner3 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD3 = new SqlDataAdapter(partner3, conn);
            var partDS3 = new DataSet();
            partAD3.Fill(partDS3);
            cboSpedicijaG.DataSource = partDS3.Tables[0];
            cboSpedicijaG.DisplayMember = "PaNaziv";
            cboSpedicijaG.ValueMember = "PaSifra";
            //spedicija rtc luka leget
            var partner4 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD4 = new SqlDataAdapter(partner4, conn);
            var partDS4 = new DataSet();
            partAD4.Fill(partDS4);
            cboSpedicijaRTC.DataSource = partDS4.Tables[0];
            cboSpedicijaRTC.DisplayMember = "PaNaziv";
            cboSpedicijaRTC.ValueMember = "PaSifra";
            //odredisna spedicija
            var partner5 = "Select PaSifra,PaNaziv From Partnerji order by PaSifra";
            var partAD5 = new SqlDataAdapter(partner5, conn);
            var partDS5 = new DataSet();
            partAD5.Fill(partDS5);
            cbOspedicija.DataSource = partDS5.Tables[0];
            cbOspedicija.DisplayMember = "PaNaziv";
            cbOspedicija.ValueMember = "PaSifra";
            //Panta
            var VRHS = "Select ID,(Rtrim(Naziv) + ' ' + HSKod) as Naziv from VrstaRobeHS order by ID";
            var VRHSAD = new SqlDataAdapter(VRHS, conn);
            var VRHSDS = new DataSet();
            VRHSAD.Fill(VRHSDS);
            cboNazivRobe.DataSource = VRHSDS.Tables[0];
            cboNazivRobe.DisplayMember = "Naziv";
            cboNazivRobe.ValueMember = "ID";


            var nhm = "Select ID,(Rtrim(Broj) + '-' + Naziv) as Naziv from NHM order by ID ";
            var nhmSAD = new SqlDataAdapter(nhm, conn);
            var nhmSDS = new DataSet();
            nhmSAD.Fill(nhmSDS);
            cboNHM.DataSource = nhmSDS.Tables[0];
            cboNHM.DisplayMember = "Naziv";
            cboNHM.ValueMember = "ID";
            //Panta

            var voz = "select ID, (Cast(ID as NVarChar(10)) + '-'+Cast(BrVoza as NVarchar(15)) + '-' + Relacija + '-' + Cast(VremePolaska as nvarchar(20))) as Naziv   from Voz ";
            var vozSAD = new SqlDataAdapter(voz, conn);
            var vozSDS = new DataSet();
            vozSAD.Fill(vozSDS);
            cboVoz.DataSource = vozSDS.Tables[0];
            cboVoz.DisplayMember = "Naziv";
            cboVoz.ValueMember = "ID";


        }
        private void FillCheck()
        {
            var query = "Select PaSifra,PaNaziv From Nalogodavci";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            clNalogodavac.DataSource = ds.Tables[0];
            clNalogodavac.DisplayMember = "PaNaziv";
            clNalogodavac.ValueMember = "PaSifra";

            var select = "Select Naziv,TipManipulacije from VrstaManipulacije";
            var da2 = new SqlDataAdapter(select, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            clVrstaUsluga.DataSource = ds2.Tables[0];
            clVrstaUsluga.DisplayMember = "Naziv";
            clVrstaUsluga.ValueMember = "Naziv";
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uv = new InsertUvozKonacna();
            uv.DelUvozKonacna(Convert.ToInt32(txtID.Text));
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    txtID.Text = row.Cells[0].Value.ToString();
                    dtEtaRijeka.Value = Convert.ToDateTime(row.Cells[1].Value.ToString());
                    dtAtaRijeka.Value = Convert.ToDateTime(row.Cells[2].Value.ToString());
                    txtStatus.Text = row.Cells[3].Value.ToString();
                    txtBrKont.Text = row.Cells[4].Value.ToString();
                    txtTipKont.Text = row.Cells[5].Value.ToString();
                    dtNalogBrodara.Value = Convert.ToDateTime(row.Cells[6].Value.ToString());
                    txtBZ.Text = row.Cells[7].Value.ToString();
                    txtNapomena.Text = row.Cells[8].Value.ToString();
                    txtPIN.Text = row.Cells[9].Value.ToString();
                    cbDirigacija.SelectedValue = row.Cells[10].Value.ToString();
                    cbBrod.SelectedValue = row.Cells[11].Value.ToString();
                    txtTeretnica.Text = row.Cells[12].Value.ToString();
                    txtADR.Text = row.Cells[13].Value.ToString();
                    cbVlasnikKont.SelectedValue = row.Cells[14].Value.ToString();
                    txtBuking.Text = row.Cells[15].Value.ToString();
                    cboUvoznik.SelectedValue = row.Cells[18].Value.ToString();
                    cboNHM.SelectedValue = row.Cells[19].Value.ToString();
                    cboSpedicijaG.SelectedValue = row.Cells[21].Value.ToString();
                    cboSpedicijaRTC.SelectedValue = row.Cells[22].Value.ToString();
                    cboCarinskiPostupak.SelectedValue = row.Cells[23].Value.ToString();
                    cbPostupak.SelectedValue = row.Cells[24].Value.ToString();
                    cbNacinPakovanja.SelectedValue = row.Cells[25].Value.ToString();
                    cbOcarina.SelectedValue = row.Cells[26].Value.ToString();
                    cbOspedicija.SelectedValue = row.Cells[27].Value.ToString();
                    txtMesto.Text = row.Cells[28].Value.ToString();
                    txtKontaktOsoba.Text = row.Cells[29].Value.ToString();
                    txtMail.Text = row.Cells[30].Value.ToString();
                    txtPlomba1.Text = row.Cells[31].Value.ToString();
                    txtPlomba2.Text = row.Cells[32].Value.ToString();
                    txtNetoR.Value = Convert.ToDecimal(row.Cells[33].Value.ToString());
                    txtBrutoR.Value = Convert.ToDecimal(row.Cells[34].Value.ToString());
                    txtTaraK.Value = Convert.ToDecimal(row.Cells[35].Value.ToString());
                    txtBrutoK.Value = Convert.ToDecimal(row.Cells[36].Value.ToString());
                    cbNapomenaPoz.SelectedValue = row.Cells[37].Value.ToString();
                    dtAtaOtprema.Value = Convert.ToDateTime(row.Cells[38].Value.ToString());
                    txtBrojVoza.Text = row.Cells[39].Value.ToString();
                    txtRelacija.Text = row.Cells[40].Value.ToString();
                    dtAtaDolazak.Value = Convert.ToDateTime(row.Cells[41].Value.ToString());

                    string pomNal = row.Cells[16].Value.ToString();
                    string[] nal = pomNal.Split(',');
                    foreach (var word in nal)
                    {
                        for (int i = 0; i < nal.Length; i++)
                        {

                            //if (clNalogodavac.GetSelected(i))
                            //{
                            clNalogodavac.SetItemChecked(i, true);
                            //}

                        }
                    }
                    string pomRob = row.Cells[17].Value.ToString();
                    string[] rob = pomRob.Split(',');
                    foreach (var r in rob)
                    {
                        for (int i = 0; i < rob.Length; i++)
                        {
                            clVrstaUsluga.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clNalogodavac.Items.Count; i++)
            {
                if (clNalogodavac.GetItemCheckState(i) == CheckState.Checked)
                {
                    if (i == 0)
                    {
                        clNalogodavac.SetSelected(i, true);
                        nalogodavci = clNalogodavac.SelectedValue.ToString();
                    }
                    else
                    {
                        clNalogodavac.SetSelected(i, true);
                        nalogodavci = nalogodavci + "," + clNalogodavac.SelectedValue.ToString();
                    }
                }
            }
            for (int i = 0; i < clVrstaUsluga.Items.Count; i++)
            {
                if (clVrstaUsluga.GetItemCheckState(i) == CheckState.Checked)
                {
                    if (i == 0)
                    {
                        clVrstaUsluga.SetSelected(i, true);
                        usluge = clVrstaUsluga.SelectedValue.ToString();
                    }
                    else
                    {
                        clVrstaUsluga.SetSelected(i, true);
                        usluge = usluge + "," + clVrstaUsluga.SelectedValue.ToString();
                    }
                }
            }
            InsertUvozKonacna ins = new InsertUvozKonacna();
            ins.UpdUvozKonacna(Convert.ToInt32(txtID.Text), Convert.ToDateTime(dtEtaRijeka.Value.ToString()),
                Convert.ToDateTime(dtAtaRijeka.Value.ToString()), txtStatus.Text.ToString().TrimEnd(), txtBrKont.Text,
                Convert.ToInt32(txtTipKont.Text.ToString().TrimEnd()), Convert.ToDateTime(dtNalogBrodara.Value.ToString()), txtBZ.Text.ToString().TrimEnd(),
                txtNapomena.Text.ToString().TrimEnd(), txtPIN.Text.ToString().TrimEnd(), Convert.ToInt32(cbDirigacija.SelectedValue), Convert.ToInt32(cbBrod.SelectedValue),
                txtTeretnica.Text, Convert.ToInt32(txtADR.Text), Convert.ToInt32(cbVlasnikKont.SelectedValue), Convert.ToInt32(txtBuking.Text), nalogodavci,
                usluge, Convert.ToInt32(cboUvoznik.SelectedValue), Convert.ToInt32(cboNHM.SelectedValue), Convert.ToInt32(cboNazivRobe.SelectedValue), Convert.ToInt32(cboSpedicijaG.SelectedValue),
                Convert.ToInt32(cboSpedicijaRTC.SelectedValue), Convert.ToInt32(cboCarinskiPostupak.SelectedValue), Convert.ToInt32(cbPostupak.SelectedValue),
                Convert.ToInt32(cbNacinPakovanja.SelectedValue), Convert.ToInt32(cbOcarina.SelectedValue), Convert.ToInt32(cbOspedicija.SelectedValue),
                txtMesto.Text.ToString().TrimEnd(), txtKontaktOsoba.Text.ToString().TrimEnd(), txtMail.Text.ToString(), txtPlomba1.Text,
                txtPlomba2.Text, Convert.ToDecimal(txtNetoR.Value), Convert.ToDecimal(txtBrutoR.Value), Convert.ToDecimal(txtTaraK.Value),
                Convert.ToDecimal(txtBrutoK.Value), Convert.ToInt32(cbNapomenaPoz.SelectedValue), Convert.ToDateTime(dtAtaOtprema.Value.ToString()),
                Convert.ToInt32(txtBrojVoza.Text), txtRelacija.Text.ToString().TrimEnd(), Convert.ToDateTime(dtAtaDolazak.Value.ToString()), Convert.ToInt32(txtKoleta.Value));
            FillGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertUvozKonacnaZaglavlje ins = new InsertUvozKonacnaZaglavlje();
            ins.InsUvozKonacnaZaglavlje(Convert.ToInt32(cboVoz.SelectedValue), txtNapomenaZaglavlje.Text);
            //refreshStavke(); - Dodati
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var select = "SELECT row_number() OVER (ORDER BY UvozKonacna.ID) RB, " +
       " TipKontejnera, BrojKontejnera, " +
       " Partnerji.PaNaziv, Partnerji.PaEMatSt1, (Cast(BrojPlombe1 as nvarchar(25)) + '/' + Cast(BrojPlombe2 as nvarchar(25))) as Plombe, " +
       "  VrstaRobeHS.Naziv as Roba,VrstaRobe.Naziv as NHM, 0 as Koleta, 0 as Tara, 0 as Masarobe, 0 as ukupnatezina, 0 as K447, 0 as tezinapok447 " +
        " FROM UvozKonacna " +
        " inner join Partnerji on PaSifra = VlasnikKontejnera " +
        " left join VrstaRobeHS on VrstaRobeHS.ID = UvozKonacna.NazivRobe " +
        " left join VrstaRobe on VrstaRobe.ID = NHMBroj Where UvozKonacna.ID = " + Convert.ToInt32(txtID.Text);

            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var ds = new DataSet();
            dataAdapter.Fill(ds);

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            object missing = System.Reflection.Missing.Value;
            Workbook wBook = excel.Workbooks.Add(missing);

            Worksheet wSheet = new Worksheet();
            try
            {

                wSheet = (Worksheet)wBook.Worksheets.get_Item(1);
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        wSheet.Cells[1, 15].EntireRow.Font.Bold = true;
                        wSheet.Range["A1:N1"].Interior.Color = System.Drawing.Color.AliceBlue;
                        wSheet.Cells[1, "A"] = "RB";
                        wSheet.Cells[1, "B"] = "Tip Kontejnera";
                        wSheet.Cells[1, "C"] = "Broj kontejnera";
                        wSheet.Cells[1, "D"] = "Partner";
                        wSheet.Cells[1, "E"] = "PIB";
                        wSheet.Cells[1, "F"] = "Plombe";
                        wSheet.Cells[1, "G"] = "Roba";
                        wSheet.Cells[1, "H"] = "NHM";
                        wSheet.Cells[1, "I"] = "Koleta";
                        wSheet.Cells[1, "J"] = "Tara";
                        wSheet.Cells[1, "K"] = "MasaRobe";
                        wSheet.Cells[1, "L"] = "UkupnaTezina";
                        wSheet.Cells[1, "M"] = "K447";
                        wSheet.Cells[1, "N"] = "P447";
                       
                        wSheet.Cells[i + 2, j + 1] = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        wSheet.Cells[i + 2, j + 1].EntireColumn.AutoFit();
                        Borders border = wSheet.Cells[i + 2, j + 1].Borders;
                        border.Weight = 2d;

                    }
                }

                string date = DateTime.Now.ToString("dd-MM-yyyy");
                string path = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                object filename = @"VLeget A106" + date + ".xlsx";
                wBook.SaveAs(filename);
                wBook.Close();
                excel.Quit();
                excel = null;
                wBook = null;
                wSheet = null;


                MessageBox.Show("Dokument je kreiran");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.InsUvozKonacnaNHM(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNHM.SelectedValue));
            FillDG2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.DelUvozKonacnaNHM(Convert.ToInt32(cboNHM.SelectedValue));
            FillDG2();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.InsUvozKonacnaVrstaRobeHS(Convert.ToInt32(txtID.Text), Convert.ToInt32(cboNHM.SelectedValue));
            FillDG3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertUvozKonacna uvK = new InsertUvozKonacna();
            uvK.DelUvozKonacnaVrstaRobeHS(Convert.ToInt32(cboNHM.SelectedValue));
            FillDG3();
        }

        private void FillDG2()
        {
            var select = "  SELECT NHM.Broj, NHM.Naziv, UvozNHM.IDNHM, NHM.ID FROM NHM INNER JOIN " +
                      " UvozKonacnaNHM ON NHM.ID = UvozKonacnaNHM.IDNHM where UvozKonacnanhm.idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by Uvoznhm.ID desc "; 
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

        }

        private void FillDG3()
        {
            var select = "select UvozKonacnaVrstaRobeHS.ID, IDVrstaRobeHS, VrstaRobeHS.HSKod from UvozKonacnaVrstaRobeHS " +
" inner join  VrstaRobeHS on VrstaRobeHS.ID = UvozKonacnaVrstaRobeHS.IDVrstaRobeHS where idnadredjena = " + Convert.ToInt32(txtID.Text) + " order by UvozKonacnaVrstaRobeHS.ID desc ";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        txtIDNHM.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Selected)
                    {
                        txtVrstaRobeHS.Text = row.Cells[0].Value.ToString();

                    }
                }
            }
            catch { }
        }

        private void UvozKonacna_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
