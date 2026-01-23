

using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Syncfusion.Grouping;
using System.Data.Common;
using Saobracaj.Uvoz;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Diagram;

namespace Saobracaj.Tehnologija
{
    public partial class frmDefinisiPoziciju : Form
    {
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
            panelHeader.Visible = false;
            this.ControlBox = true;
            // this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                // toolStripHeader.Visible = false;
                panelHeader.Visible = true;
                meniHeader.Visible = false;
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                splitContainer1.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (Control control in splitContainer1.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


                foreach (Control control in splitContainer1.Controls)
                {

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
                panelHeader.Visible = false;
                meniHeader.Visible = true;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }

        public frmDefinisiPoziciju()
        {
            InitializeComponent();
            ChangeTextBox();
        }
        int Pozicija=0;
        public frmDefinisiPoziciju(int pozicija) 
        {
            InitializeComponent();
            Pozicija = pozicija;
            ProveriPoziciju();
        }
        private void ProveriPoziciju()
        {
           /* if (Pozicija == 0)
                return;

            using (SqlConnection conn = new SqlConnection(connection))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(
                    "SELECT ID FROM PredefinisanePozicije WHERE Skladiste1 = @poz",
                    conn))
                {
                    cmd.Parameters.AddWithValue("@poz", Pozicija);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        
                        if (!dr.HasRows)
                        {
                            splitContainer1.Panel2Collapsed = true;
                            splitContainer1.Panel1Collapsed = false;
                            
                        }
                        else
                        {
                            
                            splitContainer1.Panel1Collapsed = true;
                            splitContainer1.Panel2Collapsed = false;
                        }
                    }
                }
                conn.Close();
            }*/
        }
        private void VratiZapisePozicije()
        {
            if (Pozicija == 0)
                return;

            cboNaSkladiste.SelectedValue = Pozicija;
            cboNaSkladiste2.SelectedValue = Pozicija;
            cboNaSkladiste3.SelectedValue = Pozicija;
            cboNaSkladiste4.SelectedValue = Pozicija;
            cboNaSkladiste5.SelectedValue = Pozicija;
            cboNaSkladiste6.SelectedValue = Pozicija;
            FillDG1Pozicija(Pozicija);
            FillDG2Pozicija(Pozicija);
        }


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void frmDefinisiPoziciju_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);

            var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
            var tkAD = new SqlDataAdapter(tipkontejnera, conn);
            var tkDS = new DataSet();
            tkAD.Fill(tkDS);
            txtTipKont.DataSource = tkDS.Tables[0];
            txtTipKont.DisplayMember = "SkNaziv";
            txtTipKont.ValueMember = "ID";

            var tipkontejnera2 = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
            var tkAD2 = new SqlDataAdapter(tipkontejnera2, conn);
            var tkDS2 = new DataSet();
            tkAD2.Fill(tkDS2);
            txtTipKont2.DataSource = tkDS2.Tables[0];
            txtTipKont2.DisplayMember = "SkNaziv";
            txtTipKont2.ValueMember = "ID";

            var bro = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaNaziv";
            var broAD = new SqlDataAdapter(bro, conn);
            var broDS = new DataSet();
            broAD.Fill(broDS);
            cboBrodar.DataSource = broDS.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";


            var bro2 = "Select PaSifra,PaNaziv From Partnerji where Brodar = 1 order by PaNaziv";
            var broAD2 = new SqlDataAdapter(bro2, conn);
            var broDS2 = new DataSet();
            broAD2.Fill(broDS2);
            cboBrodar2.DataSource = broDS2.Tables[0];
            cboBrodar2.DisplayMember = "PaNaziv";
            cboBrodar2.ValueMember = "PaSifra";

            var partner = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD = new SqlDataAdapter(partner, conn);
            var partDS = new DataSet();
            partAD.Fill(partDS);
            cboIzvoznik.DataSource = partDS.Tables[0];
            cboIzvoznik.DisplayMember = "PaNaziv";
            cboIzvoznik.ValueMember = "PaSifra";

            var nalogodavac1 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var nal1AD = new SqlDataAdapter(nalogodavac1, conn);
            var nal1DS = new DataSet();
            nal1AD.Fill(nal1DS);
            cboNalogodavac1.DataSource = nal1DS.Tables[0];
            cboNalogodavac1.DisplayMember = "PaNaziv";
            cboNalogodavac1.ValueMember = "PaSifra";

            var select6 = " Select Distinct ID, Naziv   From uvKvalitetKontejnera order by ID";
            var s_connection6 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboKvalitet.DataSource = ds6.Tables[0];
            cboKvalitet.DisplayMember = "Naziv";
            cboKvalitet.ValueMember = "ID";


            var select7 = " Select Distinct ID, Naziv   From uvKvalitetKontejnera order by ID";
            var s_connection7 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection7 = new SqlConnection(s_connection7);
            var c7 = new SqlConnection(s_connection7);
            var dataAdapter7 = new SqlDataAdapter(select7, c7);

            var commandBuilder7 = new SqlCommandBuilder(dataAdapter7);
            var ds7 = new DataSet();
            dataAdapter7.Fill(ds7);
            cboKvalitet2.DataSource = ds7.Tables[0];
            cboKvalitet2.DisplayMember = "Naziv";
            cboKvalitet2.ValueMember = "ID";

            var sklad = "select ID,naziv from Skladista order by ID";
            var daSklad = new SqlDataAdapter(sklad, conn);
            var dsSklad = new DataSet();
            daSklad.Fill(dsSklad);
            cboNaSkladiste.DataSource = dsSklad.Tables[0];
            cboNaSkladiste.DisplayMember = "Naziv";
            cboNaSkladiste.ValueMember = "ID";

            var sklad2 = "select ID,naziv from Skladista order by ID";
            var daSklad2 = new SqlDataAdapter(sklad2, conn);
            var dsSklad2 = new DataSet();
            daSklad2.Fill(dsSklad2);
            cboNaSkladiste2.DataSource = dsSklad2.Tables[0];
            cboNaSkladiste2.DisplayMember = "Naziv";
            cboNaSkladiste2.ValueMember = "ID";

            var sklad3 = "select ID,naziv from Skladista order by ID";
            var daSklad3 = new SqlDataAdapter(sklad3, conn);
            var dsSklad3 = new DataSet();
            daSklad3.Fill(dsSklad3);
            cboNaSkladiste3.DataSource = dsSklad3.Tables[0];
            cboNaSkladiste3.DisplayMember = "Naziv";
            cboNaSkladiste3.ValueMember = "ID";

            var sklad4 = "select ID,naziv from Skladista order by ID";
            var daSklad4 = new SqlDataAdapter(sklad4, conn);
            var dsSklad4 = new DataSet();
            daSklad4.Fill(dsSklad4);
            cboNaSkladiste4.DataSource = dsSklad4.Tables[0];
            cboNaSkladiste4.DisplayMember = "Naziv";
            cboNaSkladiste4.ValueMember = "ID";

            var sklad5 = "select ID,naziv from Skladista order by ID";
            var daSklad5 = new SqlDataAdapter(sklad5, conn);
            var dsSklad5 = new DataSet();
            daSklad5.Fill(dsSklad5);
            cboNaSkladiste5.DataSource = dsSklad5.Tables[0];
            cboNaSkladiste5.DisplayMember = "Naziv";
            cboNaSkladiste5.ValueMember = "ID";

            var sklad6 = "select ID,naziv from Skladista order by ID";
            var daSklad6 = new SqlDataAdapter(sklad6, conn);
            var dsSklad6 = new DataSet();
            daSklad6.Fill(dsSklad6);
            cboNaSkladiste6.DataSource = dsSklad6.Tables[0];
            cboNaSkladiste6.DisplayMember = "Naziv";
            cboNaSkladiste6.ValueMember = "ID";

            // select distinct bookingbrodara from IzvozKonacna
            var sklad7 = "select distinct bookingbrodara from IzvozKonacna";
            var daSklad7 = new SqlDataAdapter(sklad7, conn);
            var dsSklad7 = new DataSet();
            daSklad7.Fill(dsSklad7);
            cboBooking.DataSource = dsSklad7.Tables[0];
            cboBooking.DisplayMember = "bookingbrodara";
            cboBooking.ValueMember = "bookingbrodara";


            conn.Close();

            FillDG1();
            FillDG2();

            VratiZapisePozicije();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            InsertDefinisiPoziciju ins = new InsertDefinisiPoziciju();
           
                ins.InsDefinisiPoziciju(0, Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(txtTipKont.SelectedValue), Convert.ToInt32(cboKvalitet.SelectedValue), 0, 0, 0, Convert.ToInt32(cboNaSkladiste.SelectedValue), Convert.ToInt32(cboNaSkladiste2.SelectedValue), Convert.ToInt32(cboNaSkladiste3.SelectedValue));
        
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            InsertDefinisiPoziciju ins = new InsertDefinisiPoziciju();

            ins.InsDefinisiPoziciju(1, Convert.ToInt32(cboBrodar2.SelectedValue), Convert.ToInt32(txtTipKont2.SelectedValue), Convert.ToInt32(cboKvalitet2.SelectedValue), Convert.ToInt32(cboBooking.SelectedValue), Convert.ToInt32(cboIzvoznik.SelectedValue), Convert.ToInt32(cboNalogodavac1.SelectedValue), Convert.ToInt32(cboNaSkladiste4.SelectedValue), Convert.ToInt32(cboNaSkladiste5.SelectedValue), Convert.ToInt32(cboNaSkladiste6.SelectedValue));
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl1.Table.CurrentRecord != null)
                {
                    textBox1.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                    cboBrodar.SelectedValue= gridGroupingControl1.Table.CurrentRecord.GetValue("BrodarID");
                    txtTipKont.SelectedValue = gridGroupingControl1.Table.CurrentRecord.GetValue("VrstaKontID");
                    cboKvalitet.SelectedValue = gridGroupingControl1.Table.CurrentRecord.GetValue("KvalitetID");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void FillDG1()
        {
      
            var select = "";
        
                select = "   SELECT        PredefinisanePozicije.ID, PredefinisanePozicije.Opcija, Partnerji_1.PaNaziv AS Brodar, TipKontenjera.Naziv AS VrstaKontejnera, " +
               " uvKvalitetKontejnera.Naziv AS Kvalitet, Partnerji.PaNaziv AS Izvoznik, " +
              " Partnerji_2.PaNaziv AS NalogodavacZaVoz, PredefinisanePozicije.BookingBrodara,PredefinisanePozicije.Brodar as BrodarID,PredefinisanePozicije.VrstaKontejnera as VrstaKontID,PredefinisanePozicije.Kvalitet as KvalitetID  " +
              "    FROM  PredefinisanePozicije INNER JOIN " +
                "           Partnerji AS Partnerji_1 ON PredefinisanePozicije.Brodar = Partnerji_1.PaSifra INNER JOIN " +
                "           TipKontenjera ON PredefinisanePozicije.VrstaKontejnera = TipKontenjera.ID INNER JOIN " +
                "           uvKvalitetKontejnera ON PredefinisanePozicije.Kvalitet = uvKvalitetKontejnera.ID INNER JOIN " +
                 "          Partnerji ON PredefinisanePozicije.Izvoznik = Partnerji.PaSifra INNER JOIN " +
                 "          Partnerji AS Partnerji_2 ON PredefinisanePozicije.NalogodavacZaVoz = Partnerji_2.PaSifra " +
                  "         inner join Skladista on Skladista.ID = Skladiste1 " +
                  "          inner join Skladista as S2 on S2.ID = Skladiste2 " +
                   "           inner join Skladista as S3 on S3.ID = Skladiste3 " +
                   "           where Opcija = 0 ";



            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("BrodarID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("VrstaKontID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("KvalitetID");


            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }
        private void FillDG1Pozicija(int pozicija)
        {

            var select = "";

            select = "   SELECT        PredefinisanePozicije.ID, PredefinisanePozicije.Opcija, Partnerji_1.PaNaziv AS Brodar, TipKontenjera.Naziv AS VrstaKontejnera, " +
           " uvKvalitetKontejnera.Naziv AS Kvalitet, Partnerji.PaNaziv AS Izvoznik, " +
          " Partnerji_2.PaNaziv AS NalogodavacZaVoz, PredefinisanePozicije.BookingBrodara," +
          "PredefinisanePozicije.Brodar as BrodarID,PredefinisanePozicije.VrstaKontejnera as VrstaKontID,PredefinisanePozicije.Kvalitet as KvalitetID " +
          "    FROM  PredefinisanePozicije INNER JOIN " +
            "           Partnerji AS Partnerji_1 ON PredefinisanePozicije.Brodar = Partnerji_1.PaSifra INNER JOIN " +
            "           TipKontenjera ON PredefinisanePozicije.VrstaKontejnera = TipKontenjera.ID INNER JOIN " +
            "           uvKvalitetKontejnera ON PredefinisanePozicije.Kvalitet = uvKvalitetKontejnera.ID INNER JOIN " +
             "          Partnerji ON PredefinisanePozicije.Izvoznik = Partnerji.PaSifra INNER JOIN " +
             "          Partnerji AS Partnerji_2 ON PredefinisanePozicije.NalogodavacZaVoz = Partnerji_2.PaSifra " +
              "         inner join Skladista on Skladista.ID = Skladiste1 " +
              "          inner join Skladista as S2 on S2.ID = Skladiste2 " +
               "           inner join Skladista as S3 on S3.ID = Skladiste3 " +
               "           where Opcija = 0 and Skladista.ID="+pozicija;



            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl1.DataSource = ds.Tables[0];
            gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;

            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("BrodarID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("VrstaKontID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("KvalitetID");



            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }
        private void FillDG2Pozicija(int pozicija)
        {
            var select = "";

            select = "   SELECT PredefinisanePozicije.ID, PredefinisanePozicije.Opcija, Partnerji_1.PaNaziv AS Brodar, TipKontenjera.Naziv AS VrstaKontejnera, " +
           " uvKvalitetKontejnera.Naziv AS Kvalitet, Partnerji.PaNaziv AS Izvoznik, " +
          " Partnerji_2.PaNaziv AS NalogodavacZaVoz, PredefinisanePozicije.BookingBrodara," +
          "PredefinisanePozicije.Brodar as BrodarID,PredefinisanePozicije.VrstaKontejnera as VrstaKontID,PredefinisanePozicije.Kvalitet as KvalitetID," +
          "PredefinisanePozicije.Izvoznik as IzvoznikID, PredefinisanePozicije.NalogodavacZaVoz as NalogodavacID " +
          "    FROM  PredefinisanePozicije INNER JOIN " +
            "           Partnerji AS Partnerji_1 ON PredefinisanePozicije.Brodar = Partnerji_1.PaSifra INNER JOIN " +
            "           TipKontenjera ON PredefinisanePozicije.VrstaKontejnera = TipKontenjera.ID INNER JOIN " +
            "           uvKvalitetKontejnera ON PredefinisanePozicije.Kvalitet = uvKvalitetKontejnera.ID INNER JOIN " +
             "          Partnerji ON PredefinisanePozicije.Izvoznik = Partnerji.PaSifra INNER JOIN " +
             "          Partnerji AS Partnerji_2 ON PredefinisanePozicije.NalogodavacZaVoz = Partnerji_2.PaSifra " +
              "         inner join Skladista on Skladista.ID = Skladiste1 " +
              "          inner join Skladista as S2 on S2.ID = Skladiste2 " +
               "           inner join Skladista as S3 on S3.ID = Skladiste3 " +
               "           where Opcija = 1 and Skladista.ID="+pozicija;



            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;

            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("BrodarID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("VrstaKontID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("KvalitetID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("NalogodavacID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("IzvoznikID");


            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
        }
        private void FillDG2()
        {

            var select = "";

            select = "   SELECT PredefinisanePozicije.ID, PredefinisanePozicije.Opcija, Partnerji_1.PaNaziv AS Brodar, TipKontenjera.Naziv AS VrstaKontejnera, " +
           " uvKvalitetKontejnera.Naziv AS Kvalitet, Partnerji.PaNaziv AS Izvoznik, " +
          " Partnerji_2.PaNaziv AS NalogodavacZaVoz, PredefinisanePozicije.BookingBrodara, " +
           "PredefinisanePozicije.Brodar as BrodarID,PredefinisanePozicije.VrstaKontejnera as VrstaKontID,PredefinisanePozicije.Kvalitet as KvalitetID," +
          "PredefinisanePozicije.Izvoznik as IzvoznikID, PredefinisanePozicije.NalogodavacZaVoz as NalogodavacID " +
          "    FROM  PredefinisanePozicije INNER JOIN " +
            "           Partnerji AS Partnerji_1 ON PredefinisanePozicije.Brodar = Partnerji_1.PaSifra INNER JOIN " +
            "           TipKontenjera ON PredefinisanePozicije.VrstaKontejnera = TipKontenjera.ID INNER JOIN " +
            "           uvKvalitetKontejnera ON PredefinisanePozicije.Kvalitet = uvKvalitetKontejnera.ID INNER JOIN " +
             "          Partnerji ON PredefinisanePozicije.Izvoznik = Partnerji.PaSifra INNER JOIN " +
             "          Partnerji AS Partnerji_2 ON PredefinisanePozicije.NalogodavacZaVoz = Partnerji_2.PaSifra " +
              "         inner join Skladista on Skladista.ID = Skladiste1 " +
              "          inner join Skladista as S2 on S2.ID = Skladiste2 " +
               "           inner join Skladista as S3 on S3.ID = Skladiste3 " +
               "           where Opcija = 1 ";



            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gridGroupingControl2.DataSource = ds.Tables[0];
            gridGroupingControl2.ShowGroupDropArea = true;
            this.gridGroupingControl2.TopLevelGroupOptions.ShowFilterBar = true;

this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("BrodarID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("VrstaKontID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("KvalitetID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("NalogodavacID");
            this.gridGroupingControl1.TableDescriptor.VisibleColumns.Remove("IzvoznikID");


            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }
            







        }

        private void gridGroupingControl2_TableControlCellClick(object sender, Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (gridGroupingControl2.Table.CurrentRecord != null)
                {
                    textBox2.Text = gridGroupingControl2.Table.CurrentRecord.GetValue("ID").ToString();
                    cboBrodar.SelectedValue = gridGroupingControl2.Table.CurrentRecord.GetValue("BrodarID");
                    cboBooking.SelectedValue = gridGroupingControl2.Table.CurrentRecord.GetValue("BookingBrodara");
                    txtTipKont2.SelectedValue = gridGroupingControl2.Table.CurrentRecord.GetValue("VrstaKontID");
                    cboKvalitet2.SelectedValue = gridGroupingControl2.Table.CurrentRecord.GetValue("KvalitetID");
                    cboIzvoznik.SelectedValue = gridGroupingControl2.Table.CurrentRecord.GetValue("IzvoznikID");
                    cboNalogodavac1.SelectedValue = gridGroupingControl2.Table.CurrentRecord.GetValue("NalogodavacID");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            FillDG1();
            FillDG2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDefinisiPoziciju frm = new frmDefinisiPoziciju();
            frm.Show();
        }
    }
}
