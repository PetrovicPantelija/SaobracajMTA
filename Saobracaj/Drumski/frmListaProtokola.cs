using Syncfusion.Windows.Forms;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj.Drumski
{
    public partial class frmListaProtokola: Form
    {
        int id = 0;
        int tipTransporta = 0;
        private ContextMenuStrip meniZaDugme;
        public frmListaProtokola(int ID, int TipTransporta)
        {
            id = ID;
            tipTransporta = TipTransporta;
            InitializeComponent();
            InicijalizujMeniZaDugme();
            ChangeTextBox();
            RefreshDataGrid1();
        }
        private void ChangeTextBox()
        {

            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
          


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


                foreach (Control control in this.Controls)
                {
                    if (control is System.Windows.Forms.Button buttons)
                    {

                        buttons.BackColor = Color.FromArgb(90, 199, 249); // Example: Change background color  -- Svetlo plava
                        buttons.ForeColor = Color.White;  //51; 51; 54  - Pozadina Bela
                        buttons.Font = new System.Drawing.Font("Helvetica", 9);  // Example: Change font
                        buttons.FlatStyle = FlatStyle.Flat;
                    }
                }


                foreach (Control control in this.Controls)
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
       
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                //  this.BackColor = Color.White;
                // toolStripHeader.Visible = true;
            }
        }
        private void InicijalizujMeniZaDugme()
        {
            meniZaDugme = new ContextMenuStrip();

            // Kreiramo tri opcije
            ToolStripMenuItem subAutodan = new ToolStripMenuItem("Auto dan");
            ToolStripMenuItem subDodatnaRuta = new ToolStripMenuItem("Dodatni trošak transporta");
            ToolStripMenuItem subOstali = new ToolStripMenuItem("Razno");

            // Povezujemo događaje na klik
            subAutodan.Click += SubAutodan_Click;
            subDodatnaRuta.Click += SubDodatnaRuta_Click;
            subOstali.Click += SubOstali_Click;

            // Dodajemo ih direktno u meni za dugme
            meniZaDugme.Items.Add(subAutodan);
            meniZaDugme.Items.Add(subDodatnaRuta);
            meniZaDugme.Items.Add(subOstali);
        }

        private void SubOstali_Click(object sender, EventArgs e)
        {
            frmProtokolTransportnogNaloga frm = new frmProtokolTransportnogNaloga(0,3, id, tipTransporta);
            frm.ShowDialog();
            RefreshDataGrid1();
        }

        private void SubDodatnaRuta_Click(object sender, EventArgs e)
        {
            frmProtokolTransportnogNaloga frm = new frmProtokolTransportnogNaloga(0,2, id, tipTransporta);
            frm.ShowDialog();
            RefreshDataGrid1();
        }

        private void SubAutodan_Click(object sender, EventArgs e)
        {
            frmProtokolTransportnogNaloga frm = new frmProtokolTransportnogNaloga(0,1, id, tipTransporta);
            frm.ShowDialog();
            RefreshDataGrid1();
        }


        private void btnKreirajNovi_Click(object sender, EventArgs e)
        {
            // Prikazuje meni tačno ispod donje ivice dugmeta
            meniZaDugme.Show(btnKreirajNovi, new Point(0, btnKreirajNovi.Height));
        }


        private void RefreshDataGrid1()
        {
            string s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection conn = new SqlConnection(s_connection);
       
            using (conn)
            {
                conn.Open();

                var select = @"
                   SELECT tp. Naziv,pnd.TipProtokola, pnd.RadniNalogDrumskiID as NalogID, pnd.id,  pa.PaNaziv AS Nalogodavac,ik.BrojKontejnera,
                   pnd.Trosak, pnd.Cena, pnd.Opis
                   FROM ProtokolRadniNalogDrumski pnd
                   INNER JOIN RadniNalogDrumski rn on rn.ID =  pnd.RadniNalogDrumskiID
                   INNER JOIN IzvozKonacna ik ON rn.KontejnerID = ik.ID 
                   LEFT JOIN Partnerji pa on pa.PaSifra =  ik.Klijent3
                   LEFT JOIN TipProtokola tp on tp.ID = pnd.TipProtokola
                   WHERE pnd.RadniNalogDrumskiID = @ID AND rn.Uvoz = 0
                   UNION 
                   SELECT tp. Naziv,pnd.TipProtokola, pnd.RadniNalogDrumskiID as NalogID, pnd.id,  pa.PaNaziv AS Nalogodavac, i.BrojKontejnera,
                   pnd.Trosak, pnd.Cena, pnd.Opis
                   FROM ProtokolRadniNalogDrumski pnd
                   INNER JOIN RadniNalogDrumski rn on rn.ID =  pnd.RadniNalogDrumskiID
                   INNER JOIN Izvoz i ON rn.KontejnerID = i.ID 
                   LEFT JOIN Partnerji pa on pa.PaSifra =  i.Klijent3
                   LEFT JOIN TipProtokola tp on tp.ID = pnd.TipProtokola
                   WHERE pnd.RadniNalogDrumskiID = @ID AND rn.Uvoz = 0
                   UNION 
                   SELECT tp. Naziv, pnd.TipProtokola,pnd.RadniNalogDrumskiID as NalogID, pnd.id,  pa.PaNaziv AS Nalogodavac, rn.BrojKontejnera,
                   pnd.Trosak, pnd.Cena, pnd.Opis
                   FROM ProtokolRadniNalogDrumski pnd
                   INNER JOIN RadniNalogDrumski rn on rn.ID =  pnd.RadniNalogDrumskiID
                   LEFT JOIN Partnerji pa on pa.PaSifra =  rn.Klijent
                   LEFT JOIN TipProtokola tp on tp.ID = pnd.TipProtokola
                   WHERE pnd.RadniNalogDrumskiID = @ID AND rn.Uvoz in (-1,2,3, 4, 5) ";

                // Bind baze
                SqlDataAdapter da = new SqlDataAdapter(select, conn);
                da.SelectCommand.Parameters.AddWithValue("@ID", id);

                var ds = new System.Data.DataSet();
                da.Fill(ds);
                dataGridView1.ReadOnly = true;
                dataGridView1.DataSource = ds.Tables[0];

                dataGridView1.RowHeadersWidth = 30;

                if (dataGridView1.Columns.Contains("TipProtokola"))
                {
                    dataGridView1.Columns["TipProtokola"].Visible = false;
                }

                if (dataGridView1.Columns.Contains("ID"))
                {
                    dataGridView1.Columns["ID"].Visible = false;
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    
                    int tipTransporta = Convert.ToInt32(ds.Tables[0].Rows[0]["TipProtokola"]);

                    if (tipTransporta == 2)
                    {
                        if (dataGridView1.Columns.Contains("BrojKontejnera"))
                        {
                            dataGridView1.Columns["BrojKontejnera"].Visible = false;
                        }
                    }
                    else
                    {
                        // Za svaki slučaj, ako se grid ponovo iscrtava, vratimo je da bude vidljiva ako nije tip 2
                        if (dataGridView1.Columns.Contains("BrojKontejnera"))
                        {
                            dataGridView1.Columns["BrojKontejnera"].Visible = true;
                        }
                    }
                }
            }

            PodesiDatagridView(dataGridView1);
       
        }

        private void PodesiDatagridView(DataGridView dgv)
        {

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 199, 249); // Selektovana boja
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.BackgroundColor = Color.White;

            dgv.DefaultCellStyle.Font = new Font("Helvetica", 12F, GraphicsUnit.Pixel);
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



        private void otvoriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            int idProtokola = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
            int tipProtokola = Convert.ToInt32(dataGridView1.CurrentRow.Cells["TipProtokola"].Value);
            bool autodan = false;
            bool dodatnaRuta = false;
            bool ostalo = false;

            if (tipProtokola== 1)
                autodan = true;
            else if (tipProtokola == 2)
                dodatnaRuta = true;
            else if(tipProtokola == 3)
                ostalo = true;
            frmProtokolTransportnogNaloga frm = new frmProtokolTransportnogNaloga(idProtokola, tipProtokola, id, tipTransporta);
            frm.ShowDialog();
            RefreshDataGrid1();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Provera da li je klik na validan red (ne header)
                if (e.RowIndex >= 0)
                {
                    // Očisti prethodnu selekciju
                    dataGridView1.ClearSelection();

                    // Selektuj kliknuti red
                    dataGridView1.Rows[e.RowIndex].Selected = true;

                    // Postavi current cell (bitno za dalje operacije)
                    dataGridView1.CurrentCell =
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex >= 0 ? e.ColumnIndex : 0];

                    // Prikaži context menu
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows == null || dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

   
            DataGridViewRow selektovaniRed = dataGridView1.CurrentRow;
            if (selektovaniRed == null || selektovaniRed.Index < 0)
            {
                return;
            }

            int idProtokola = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
            int tipProtokola = Convert.ToInt32(dataGridView1.CurrentRow.Cells["TipProtokola"].Value);
            bool autodan = false;
            bool dodatnaRuta = false;
            bool ostalo = false;

            if (tipProtokola == 1)
                autodan = true;
            else if (tipProtokola == 2)
                dodatnaRuta = true;
            else if (tipProtokola == 3)
                ostalo = true;
            frmProtokolTransportnogNaloga frm = new frmProtokolTransportnogNaloga(idProtokola, tipProtokola, id, tipTransporta);
            frm.ShowDialog();
            RefreshDataGrid1();
        }

        private void stornirajToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows == null || dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }


            DataGridViewRow selektovaniRed = dataGridView1.CurrentRow;
            if (selektovaniRed == null || selektovaniRed.Index < 0)
            {
                return;
            }

            int idProtokola = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);

            DialogResult result = MessageBox.Show(
                                    "Da li ste sigurni da želite da stornirate protokol?",
                                    "Potvrda storniranja",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question
                                );

            // Ako korisnik klikne na 'No' (ili zatvori prozor), samo prekida se izvršavanje
            if (result != DialogResult.Yes)
            {
                return;
            }

            InsertProtokolTransportnogNaloga ins = new InsertProtokolTransportnogNaloga();
            ins.DelProtokolRadniNalogDrumski(idProtokola);

            // Opciono: poruka o uspehu ili osvežavanje prikaza
            MessageBox.Show("Protokol je uspešno storniran.", "Obaveštenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshDataGrid1();
        }
    }
}
