using Syncfusion.Windows.Forms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Izvoz
{
    public partial class frmPrebacivanjeIzPlanaUPlan : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        private void ChangeTextBox()
        {


            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;

                this.BackColor = Color.White;
                this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
                this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
                this.ControlBox = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                Office2010Colors.ApplyManagedColors(this, Color.White);
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                this.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {

                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }



            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                foreach (Control control in tableLayoutPanel1.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }

                    if (control is System.Windows.Forms.TextBox textBox)
                    {

                        textBox.BackColor = Color.White;// Example: Change background color
                        textBox.ForeColor = Color.FromArgb(51, 51, 54); //Boja slova u kvadratu
                        textBox.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                        // Example: Change font
                    }


                    if (control is System.Windows.Forms.Label label)
                    {
                        // Change properties here
                        label.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        label.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);  // Example: Change font

                        // textBox.ReadOnly = true;              // Example: Make text boxes read-only
                    }

                    if (control is DateTimePicker dtp)
                    {
                        dtp.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        dtp.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.CheckBox chk)
                    {
                        chk.ForeColor = Color.FromArgb(110, 110, 115); // Example: Change background color
                        chk.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ListBox lb)
                    {
                        lb.ForeColor = Color.FromArgb(51, 51, 54); // Example: Change background color
                        lb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.ComboBox cb)
                    {
                        cb.ForeColor = Color.FromArgb(51, 51, 54);
                        cb.BackColor = Color.White;// Example: Change background color
                        cb.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }

                    if (control is System.Windows.Forms.NumericUpDown nu)
                    {
                        nu.ForeColor = Color.FromArgb(51, 51, 54);
                        nu.BackColor = Color.White;// Example: Change background color
                        nu.Font = new System.Drawing.Font("Helvetica", 9, System.Drawing.FontStyle.Regular);
                    }
                }
            }
            else
            {
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
            }





        }

        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Helvetica", 12F, GraphicsUnit.Pixel);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 54);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);
            dgv.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 248);


            //Header
            dgv.EnableHeadersVisualStyles = false;
            //   header.Style.Font = new Font("Arial", 12F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 54);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgv.ColumnHeadersHeight = 30;
        }



        public frmPrebacivanjeIzPlanaUPlan()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        private void frmPrebacivanjeIzPlanaUPlan_Load(object sender, EventArgs e)
        {
            var planutovara = "select IzvozKonacnaZaglavlje.ID,(NazivVoza + '-' + Cast(BrVoza as nvarchar(15)) + '-'  + Relacija) as Naziv from IzvozKonacnaZaglavlje " +
            " inner join Voz on Voz.Id = IzvozKonacnaZaglavlje.IdVoza order by IzvozKonacnaZaglavlje.ID desc";
            var planutovaraSAD = new SqlDataAdapter(planutovara, connection);
            var planutovaraSDS = new DataSet();
            planutovaraSAD.Fill(planutovaraSDS);
            cboPlanUtovaraIz.DataSource = planutovaraSDS.Tables[0];
            cboPlanUtovaraIz.DisplayMember = "Naziv";
            cboPlanUtovaraIz.ValueMember = "ID";

            var planutovara2 = "select IzvozKonacnaZaglavlje.ID,(NazivVoza + '-' + Cast(BrVoza as nvarchar(15)) + ' '  + Relacija) as Naziv from IzvozKonacnaZaglavlje " +
            " inner join Voz on Voz.Id = IzvozKonacnaZaglavlje.IdVoza order by IzvozKonacnaZaglavlje.ID desc";
            var planutovara2SAD = new SqlDataAdapter(planutovara2, connection);
            var planutovara2SDS = new DataSet();
            planutovara2SAD.Fill(planutovara2SDS);
            cboPlanUtovaraU.DataSource = planutovara2SDS.Tables[0];
            cboPlanUtovaraU.DisplayMember = "Naziv";
            cboPlanUtovaraU.ValueMember = "ID";
        }

        private void RefreshDataGrid1()
        {
            var select = "      SELECT  IzvozKonacna.ID as ID,  IzvozKonacna.BrojKontejnera,  IzvozKonacna.VrstaKontejnera as VKID, TipKontenjera.Naziv as VRSTAKONTEJNERA, " +
                " IzvozKonacna.BrojVagona,  IzvozKonacna.BrodskaPlomba, " +
 " IzvozKonacna.OstalePlombe, IzvozKonacna.BookingBrodara,      Partnerji.PaNaziv,     IzvozKonacna.CutOffPort, IzvozKonacna.NetoRobe, IzvozKonacna.BrutoRobe, " +
 "  IzvozKonacna.BrutoRobeO, IzvozKonacna.BrojKoleta, IzvozKonacna.BrojKoletaO, IzvozKonacna.CBM, IzvozKonacna.CBMO, " +
  " IzvozKonacna.VrednostRobeFaktura,   (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))" +
 "  FROM IzvozVrstaManipulacije           inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije" +
 "  where IzvozVrstaManipulacije.IDNadredjena = IzvozKonacna.ID            FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga, " +
 "  (SELECT  STUFF((SELECT distinct    '/' + Cast(RTRIM(VrstaRobeHS.HSKod) as nvarchar(20))             FROM IzvozVrstaRobeHS " +
  " inner join VrstaRobeHS on IzvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID" +
 "  where IzvozVrstaRobeHS.IDNadredjena = IzvozKonacna.ID              FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,  " +
 "  (SELECT  STUFF((SELECT distinct    '/' + Cast(RTRIM(NHM.Broj) as nvarchar(20))              FROM IzvozNHM  inner join NHM" +
  " on IzvozNHM.IDNHM = NHM.ID  where IzvozNHM.IDNadredjena = IzvozKonacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,   " +
 "  IzvozKonacna.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT,  " +
  " KontejnerskiTerminali.Oznaka, IzvozKonacna.Cirada, IzvozKonacna.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, (Rtrim(partnerjiKontOsebaMU.PaKOIme) + ' ' + Rtrim(partnerjiKontOsebaMU.PaKoPriimek)) as KontaktOsoba , PaKOOpomba as AdresaKO, " +
 "  Carinarnice.CIOznaka,Carinarnice.Naziv AS Carinarnica,  Partnerji_1.PaNaziv AS Spedicija, KontaktSpeditera,    VrstaRobeADR.UNKod  as ADR , AdresaSlanjaStatusa AS AdresaStatusVozila,   " +
 "  NaslovSlanjaStatusa AS NaslovStatusaVozila, IzvozKonacna.EtaLeget, (VrstaCarinskogPostupka.Oznaka + ' ' + VrstaCarinskogPostupka.Naziv) AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,  " +
 "  IzvozKonacna.AutoDana, IzvozKonacna.NajavaVozila, IzvozKonacna.Vozilo, IzvozKonacna.Vozac, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.Vaganje, IzvozKonacna.VGMTezina, IzvozKonacna.Tara, " +
 "  IzvozKonacna.VGMBrod,                     Partnerji_2.PaNaziv AS Izvoznik, Partnerji_3.PaNaziv AS Klijent1, IzvozKonacna.Napomena1REf,  " +
 "  IzvozKonacna.DobijenNalogKlijent1, Partnerji_4.PaNaziv AS klijent2, " +
 "  IzvozKonacna.Napomena2REf, Partnerji_5.PaNaziv AS Klijent3, IzvozKonacna.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,  " +
 "  IzvozKonacna.NacinPretovara FROM         IzvozKonacna LEFT JOIN TipKontenjera ON IzvozKonacna.VrstaKontejnera = TipKontenjera.ID Left JOIN " +
  " Partnerji ON IzvozKonacna.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON IzvozKonacna.KrajnaDestinacija = KrajnjaDestinacija.ID " +
  " LEFT JOIN         VrstePostupakaUvoz ON IzvozKonacna.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN     KontejnerskiTerminali " +
  " ON IzvozKonacna.MestoPreuzimanja = KontejnerskiTerminali.id LEFT JOIN         MestaUtovara ON IzvozKonacna.MesoUtovara = MestaUtovara.ID " +
  " LEFT JOIN         Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID Left JOIN        Partnerji AS Partnerji_1 " +
  " ON IzvozKonacna.Spedicija = Partnerji_1.PaSifra " +
 " left JOIN         VrstaCarinskogPostupka ON IzvozKonacna.NapomenaReexport = VrstaCarinskogPostupka.id " +
  " left JOIN        InspekciskiTretman ON IzvozKonacna.Inspekcija = InspekciskiTretman.ID " +
  " left JOIN        Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra " +
" left JOIN        Partnerji AS Partnerji_3 ON IzvozKonacna.Klijent1 = Partnerji_3.PaSifra " +
  " left JOIN       Partnerji AS Partnerji_4 ON IzvozKonacna.Klijent2 = Partnerji_4.PaSifra " +
  " left JOIN         Partnerji AS Partnerji_5 ON IzvozKonacna.Klijent3 = Partnerji_5.PaSifra " +
 "  LEFT JOIN          Partnerji AS Partnerji_6 ON IzvozKonacna.SpediterRijeka = Partnerji_6.PaSifra " +
  " LEFT JOIN         uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID  " +
  " LEFT JOIN         partnerjiKontOsebaMU ON IzvozKonacna.KontaktOsoba = partnerjiKontOsebaMU.PaKoZapSt  " +
    " LEFT JOIN         VrstaRobeADR ON IzvozKonacna.ADR = VrstaRobeADR.ID  " +
             " where IzvozKonacna.IdNadredjena = " + Convert.ToInt32(cboPlanUtovaraIz.SelectedValue) + " order by IzvozKonacna.ID desc";

            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            PodesiDatagridView(dataGridView1);

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;
        }

        private void RefreshDataGrid2()
        {
            var select = "      SELECT  IzvozKonacna.ID as ID,  IzvozKonacna.BrojKontejnera,  IzvozKonacna.VrstaKontejnera as VKID, TipKontenjera.Naziv as VRSTAKONTEJNERA, " +
                " IzvozKonacna.BrojVagona,  IzvozKonacna.BrodskaPlomba, " +
 " IzvozKonacna.OstalePlombe, IzvozKonacna.BookingBrodara,      Partnerji.PaNaziv,     IzvozKonacna.CutOffPort, IzvozKonacna.NetoRobe, IzvozKonacna.BrutoRobe, " +
 "  IzvozKonacna.BrutoRobeO, IzvozKonacna.BrojKoleta, IzvozKonacna.BrojKoletaO, IzvozKonacna.CBM, IzvozKonacna.CBMO, " +
  " IzvozKonacna.VrednostRobeFaktura,   (SELECT  STUFF((SELECT distinct    '/' + Cast(VrstaManipulacije.Naziv as nvarchar(20))" +
 "  FROM IzvozVrstaManipulacije           inner join VrstaManipulacije on VrstaManipulacije.ID = IzvozVrstaManipulacije.IDVrstaManipulacije" +
 "  where IzvozVrstaManipulacije.IDNadredjena = IzvozKonacna.ID            FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as VrsteUsluga, " +
 "  (SELECT  STUFF((SELECT distinct    '/' + Cast(RTRIM(VrstaRobeHS.HSKod) as nvarchar(20))             FROM IzvozVrstaRobeHS " +
  " inner join VrstaRobeHS on IzvozVrstaRobeHS.IDVrstaRobeHS = VrstaRobeHS.ID" +
 "  where IzvozVrstaRobeHS.IDNadredjena = IzvozKonacna.ID              FOR XML PATH('')), 1, 1, ''   ) As Skupljen) as HS,  " +
 "  (SELECT  STUFF((SELECT distinct    '/' + Cast(RTRIM(NHM.Broj) as nvarchar(20))              FROM IzvozNHM  inner join NHM" +
  " on IzvozNHM.IDNHM = NHM.ID  where IzvozNHM.IDNadredjena = IzvozKonacna.ID   FOR XML PATH('')), 1, 1, ''  ) As Skupljen) as NHM,   " +
 "  IzvozKonacna.Valuta, KrajnjaDestinacija.Naziv AS KrajnjaDestinacija, VrstePostupakaUvoz.Naziv AS Postupak, KontejnerskiTerminali.Naziv AS PPCNT,  " +
  " KontejnerskiTerminali.Oznaka, IzvozKonacna.Cirada, IzvozKonacna.PlaniraniDatumUtovara, MestaUtovara.Naziv AS MestoUtovara, (Rtrim(partnerjiKontOsebaMU.PaKOIme) + ' ' + Rtrim(partnerjiKontOsebaMU.PaKoPriimek)) as KontaktOsoba , PaKOOpomba as AdresaKO, " +
 "  Carinarnice.CIOznaka,Carinarnice.Naziv AS Carinarnica,  Partnerji_1.PaNaziv AS Spedicija, KontaktSpeditera,    VrstaRobeADR.UNKod  as ADR , AdresaSlanjaStatusa AS AdresaStatusVozila,   " +
 "  NaslovSlanjaStatusa AS NaslovStatusaVozila, IzvozKonacna.EtaLeget, (VrstaCarinskogPostupka.Oznaka + ' ' + VrstaCarinskogPostupka.Naziv) AS Reexport, InspekciskiTretman.Naziv AS InspekciskiTretman,  " +
 "  IzvozKonacna.AutoDana, IzvozKonacna.NajavaVozila, IzvozKonacna.Vozilo, IzvozKonacna.Vozac, IzvozKonacna.DodatneNapomeneDrumski, IzvozKonacna.Vaganje, IzvozKonacna.VGMTezina, IzvozKonacna.Tara, " +
 "  IzvozKonacna.VGMBrod,                     Partnerji_2.PaNaziv AS Izvoznik, Partnerji_3.PaNaziv AS Klijent1, IzvozKonacna.Napomena1REf,  " +
 "  IzvozKonacna.DobijenNalogKlijent1, Partnerji_4.PaNaziv AS klijent2, " +
 "  IzvozKonacna.Napomena2REf, Partnerji_5.PaNaziv AS Klijent3, IzvozKonacna.Napomena3REf, Partnerji_6.PaNaziv AS SpediterRijeka, uvNacinPakovanja.Naziv AS NacinPakovanja,  " +
 "  IzvozKonacna.NacinPretovara FROM         IzvozKonacna LEFT JOIN TipKontenjera ON IzvozKonacna.VrstaKontejnera = TipKontenjera.ID Left JOIN " +
  " Partnerji ON IzvozKonacna.Brodar = Partnerji.PaSifra LEFT JOIN         KrajnjaDestinacija ON IzvozKonacna.KrajnaDestinacija = KrajnjaDestinacija.ID " +
  " LEFT JOIN         VrstePostupakaUvoz ON IzvozKonacna.Postupanje = VrstePostupakaUvoz.ID LEFT JOIN     KontejnerskiTerminali " +
  " ON IzvozKonacna.MestoPreuzimanja = KontejnerskiTerminali.id LEFT JOIN         MestaUtovara ON IzvozKonacna.MesoUtovara = MestaUtovara.ID " +
  " LEFT JOIN         Carinarnice ON IzvozKonacna.MestoCarinjenja = Carinarnice.ID Left JOIN        Partnerji AS Partnerji_1 " +
  " ON IzvozKonacna.Spedicija = Partnerji_1.PaSifra " +
 " left JOIN         VrstaCarinskogPostupka ON IzvozKonacna.NapomenaReexport = VrstaCarinskogPostupka.id " +
  " left JOIN        InspekciskiTretman ON IzvozKonacna.Inspekcija = InspekciskiTretman.ID " +
  " left JOIN        Partnerji AS Partnerji_2 ON IzvozKonacna.Izvoznik = Partnerji_2.PaSifra " +
" left JOIN        Partnerji AS Partnerji_3 ON IzvozKonacna.Klijent1 = Partnerji_3.PaSifra " +
  " left JOIN       Partnerji AS Partnerji_4 ON IzvozKonacna.Klijent2 = Partnerji_4.PaSifra " +
  " left JOIN         Partnerji AS Partnerji_5 ON IzvozKonacna.Klijent3 = Partnerji_5.PaSifra " +
 "  LEFT JOIN          Partnerji AS Partnerji_6 ON IzvozKonacna.SpediterRijeka = Partnerji_6.PaSifra " +
  " LEFT JOIN         uvNacinPakovanja ON IzvozKonacna.NacinPakovanja = uvNacinPakovanja.ID  " +
  " LEFT JOIN         partnerjiKontOsebaMU ON IzvozKonacna.KontaktOsoba = partnerjiKontOsebaMU.PaKoZapSt  " +
    " LEFT JOIN         VrstaRobeADR ON IzvozKonacna.ADR = VrstaRobeADR.ID  " +
             " where IzvozKonacna.IdNadredjena = " + Convert.ToInt32(cboPlanUtovaraU.SelectedValue) + " order by IzvozKonacna.ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshDataGrid1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGrid2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    InsertIzvozKonacna ins = new InsertIzvozKonacna();
                    ins.PrenesiIzPlanUtovaraUPlanUtovara(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(cboPlanUtovaraIz.SelectedValue), Convert.ToInt32(cboPlanUtovaraU.SelectedValue));
                }
            }
            RefreshDataGrid1();
            RefreshDataGrid2();
        }
    }
}
