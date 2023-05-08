using Saobracaj.Dokumenta.TrainList;
using Saobracaj.Dokumenta.TrainListItem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;

using Microsoft.Reporting.WinForms;


namespace Saobracaj.Dokumenta
{
    public partial class frmTrainList : Form
    {
        private TrainListDAO _trainList = new TrainListDAO();
        private TrainListItemDAO _trainListItem = new TrainListItemDAO();

        int trainListSelectedRow;
        int trainListItemSelectedRow;
        int OtvaranjeIzNajave = 0;

        public frmTrainList()
        {
            InitializeComponent();
            setStyle();
        }

        public frmTrainList(string Oznaka)
        {
            InitializeComponent();
            setStyle();
            OtvaranjeIzNajave = 1;
            txt_trainNo.Text = Oznaka;
        }

        public frmTrainList(string Oznaka, int i, DateTime PredvidjenoVreme)
        {
            InitializeComponent();
            setStyle();
            OtvaranjeIzNajave = 2;
            groupBoxAddEdit.Visible = true;
            groupBoxAddEdit.Text = "Insert Train Item";
            
            txt_note.Text = "";
            txt_TotalUnitTare.Text = "";
            txt_TotalGoods.Text = "";
            txt_TotalWagonTare.Text = "";
            txt_TotalWeight.Text = "";
            txt_TotalTrainLength.Text = "";
            txt_NHM.Text = "";
            txt_MRN.Text = "";
            txt_Seals.Text = "";
            txt_UN.Text = "";
            // dataGrid2.Rows.Clear();
            btnAddUpdate.Text = "Add";
            departure_time.Value = DateTime.Now;

            txt_trainNo.Text = Oznaka;
        }
        private void frmTrainList_Load(object sender, EventArgs e)
        {
            if (OtvaranjeIzNajave == 2)
            { groupBoxAddEdit.Visible = true; }
            else
            {
                groupBoxAddEdit.Visible = false;
            }
            

            var data = _trainList.GetAll();
            if (data.Count > 0)
            {
                dataGrid1.DataSource = data;
                dataGrid1.Columns[0].Width = 0;
                trainListSelectedRow = dataGrid1.CurrentRow.Index;

                List<TrainListItemModel> itemList = _trainListItem.GetAllBySuperiorId((int)dataGrid1.Rows[trainListSelectedRow].Cells[0].Value);
                if (itemList.Count > 0)
                {
                    dataGrid2.DataSource = itemList;
                    dataGrid2.Columns[0].Width = 0;
                    dataGrid2.Columns[1].Visible = false;

                    Total totalCalc = new Total(itemList, (int)dataGrid1.Rows[trainListSelectedRow].Cells[0].Value);
                    txt_TotalUnitTare.Text = totalCalc.TotalUnitTare.ToString() + " kg";
                    txt_TotalGoods.Text = totalCalc.TotalGoods.ToString() + " kg";
                    txt_TotalWagonTare.Text = totalCalc.TotalWagonTare.ToString() + " kg";
                    txt_TotalWeight.Text = totalCalc.TotalWeight.ToString() + " kg";
                    txt_TotalTrainLength.Text = totalCalc.TotalTrainLength.ToString() + " m";

                    trainListItemSelectedRow = dataGrid2.CurrentRow.Index;
                    List<int> NHM = _trainListItem.GetAllNHMBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
                    txt_NHM.Text = "";
                    foreach (int n in NHM)
                    {
                        txt_NHM.Text += n.ToString() + Environment.NewLine;
                    }
                    List<string> MRN = _trainListItem.GetAllMRNBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
                    txt_MRN.Text = "";
                    foreach (string n in MRN)
                    {
                        txt_MRN.Text += n + Environment.NewLine;
                    }
                    List<string> Seals = _trainListItem.GetAllSealsBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
                    txt_Seals.Text = "";
                    foreach (string n in Seals)
                    {
                        txt_Seals.Text += n + Environment.NewLine;
                    }
                    List<string> UN = _trainListItem.GetAllUNBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
                    txt_UN.Text = "";
                    foreach (string n in UN)
                    {
                        txt_UN.Text += n + Environment.NewLine;
                    }
                }

            }
            if (OtvaranjeIzNajave == 1)
            {
                VratiTrainListID();
                RefreshDatagridKontrolisano();
                RefreshDatagridSumarno();
                SelectROWDataGRID1();
                //Treba obeleziti plan
            }
        }

        private void VratiTrainListID()
        {
            //Panta 
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select ID  from TrainList where KomOznaka = '" + txt_trainNo.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                textBoxSearch.Text = dr["ID"].ToString();

            }

            con.Close();
        }

        /*  private void VratiTara()
          {
              //Panta 
              var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
              SqlConnection con = new SqlConnection(s_connection);

              con.Open();

              SqlCommand cmd = new SqlCommand("select Data  from Config where Code = 'DatumUnosaSmena'", con);
              SqlDataReader dr = cmd.ExecuteReader();

              while (dr.Read())
              {
                  DatumZakljucavanja = Convert.ToDateTime(dr["Data"].ToString());

              }

              con.Close();
          }
        */
        private void dataGrid1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGrid1.Rows)
                {
                    if (row.Selected)
                    {
                        textBoxSearch.Text = row.Cells[0].Value.ToString();

                        // txtOpis.Text = row.Cells[1].Value.ToString();
                    }
                }
                if (dataGrid1.RowCount > 0)
                {
                    trainListSelectedRow = dataGrid1.CurrentRow.Index;
                    List<TrainListItemModel> itemList = _trainListItem.GetAllBySuperiorId((int)dataGrid1.Rows[trainListSelectedRow].Cells[0].Value);
                    dataGrid2.DataSource = itemList;
                    dataGrid2.Columns[0].Width = 0;
                    dataGrid2.Columns[1].Visible = false;


                    Total totalCalc = new Total(itemList, (int)dataGrid1.Rows[trainListSelectedRow].Cells[0].Value);
                    txt_TotalUnitTare.Text = totalCalc.TotalUnitTare.ToString() + " kg";
                    txt_TotalGoods.Text = totalCalc.TotalGoods.ToString() + " kg";
                    txt_TotalWagonTare.Text = totalCalc.TotalWagonTare.ToString() + " kg";
                    txt_TotalWeight.Text = totalCalc.TotalWeight.ToString() + " kg";
                    txt_TotalTrainLength.Text = totalCalc.TotalTrainLength.ToString() + " m";
                    RefreshDatagridKontrolisano();
                    RefreshDatagridSumarno();
                    SelectROWDataGRID1();
                }
               
            }
            catch
            {
               // MessageBox.Show("Nije uspela selekcija stavki");
            }

            //  RefreshDatagridKontrolisano();
            //  RefreshDatagridSumarno();
            //  SelectROWDataGRID1();

        }
        private void dataGrid2_Click(object sender, EventArgs e)
        {
            if (dataGrid2.RowCount > 0)
            {
                trainListItemSelectedRow = dataGrid2.CurrentRow.Index;
                List<int> NHM = _trainListItem.GetAllNHMBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
                txt_NHM.Text = "";
                foreach (int n in NHM)
                {
                    txt_NHM.Text += n.ToString() + Environment.NewLine;
                }
                List<string> MRN = _trainListItem.GetAllMRNBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
                txt_MRN.Text = "";
                foreach (string n in MRN)
                {
                    txt_MRN.Text += n + Environment.NewLine;
                }
                List<string> Seals = _trainListItem.GetAllSealsBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
                txt_Seals.Text = "";
                foreach (string n in Seals)
                {
                    txt_Seals.Text += n + Environment.NewLine;
                }
                List<string> UN = _trainListItem.GetAllUNBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
                txt_UN.Text = "";
                foreach (string n in UN)
                {
                    txt_UN.Text += n + Environment.NewLine;
                }
            }

        }

        private void btn_ImportExcel_Click(object sender, EventArgs e)
        {
            trainListSelectedRow = dataGrid1.CurrentRow.Index;
            _trainListItem.ReadFromExcel((int)dataGrid1.Rows[trainListSelectedRow].Cells[0].Value);


            dataGrid2.DataSource = _trainListItem.GetAllBySuperiorId((int)dataGrid1.Rows[trainListSelectedRow].Cells[0].Value);
            dataGrid2.ClearSelection();
            dataGrid2.Rows[dataGrid2.Rows.Count - 1].Selected = true;
            dataGrid2.FirstDisplayedScrollingRowIndex = dataGrid2.RowCount - 1;
            dataGrid2.Columns[0].Width = 0;
            dataGrid2.Columns[1].Visible = false;

            trainListItemSelectedRow = dataGrid2.Rows.Count - 1;
            List<int> NHM = _trainListItem.GetAllNHMBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
            txt_NHM.Text = "";
            foreach (int n in NHM)
            {
                txt_NHM.Text += n.ToString() + Environment.NewLine;
            }
            List<string> MRN = _trainListItem.GetAllMRNBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
            txt_MRN.Text = "";
            foreach (string n in MRN)
            {
                txt_MRN.Text += n + Environment.NewLine;
            }
            List<string> Seals = _trainListItem.GetAllSealsBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
            txt_Seals.Text = "";
            foreach (string n in Seals)
            {
                txt_Seals.Text += n + Environment.NewLine;
            }
            List<string> UN = _trainListItem.GetAllUNBySuperiorId((int)dataGrid2.Rows[trainListItemSelectedRow].Cells[0].Value);
            txt_UN.Text = "";
            foreach (string n in UN)
            {
                txt_UN.Text += n + Environment.NewLine;
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid1.Rows.Count != 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to delete this item?",
                     "Confirm Delete!",
                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    trainListSelectedRow = dataGrid1.CurrentRow.Index;
                    int id = (int)dataGrid1.Rows[trainListSelectedRow].Cells[0].Value;
                    int resoult = _trainList.Delete(id);
                    MessageBox.Show(resoult + " row(s) is deleted");

                    // Refresh the table
                    dataGrid1.DataSource = _trainList.GetAll();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGrid1.Rows.Count != 0)
            {
                groupBoxAddEdit.Visible = true;
                groupBoxAddEdit.Text = "Edit Train Item";
                btnAddUpdate.Text = "Update";
                trainListSelectedRow = dataGrid1.CurrentRow.Index;
                departure_time.Value = (DateTime)dataGrid1.Rows[trainListSelectedRow].Cells[1].Value;
                txt_trainNo.Text = (string)dataGrid1.Rows[trainListSelectedRow].Cells[2].Value;
                txt_note.Text = (string)dataGrid1.Rows[trainListSelectedRow].Cells[3].Value;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            groupBoxAddEdit.Visible = true;
            groupBoxAddEdit.Text = "Insert Train Item";
            txt_trainNo.Text = "";
            txt_note.Text = "";
            txt_TotalUnitTare.Text = "";
            txt_TotalGoods.Text = "";
            txt_TotalWagonTare.Text = "";
            txt_TotalWeight.Text = "";
            txt_TotalTrainLength.Text = "";
            txt_NHM.Text = "";
            txt_MRN.Text = "";
            txt_Seals.Text = "";
            txt_UN.Text = "";
           // dataGrid2.Rows.Clear();
            btnAddUpdate.Text = "Add";
            departure_time.Value = DateTime.Now;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text == "Search")
            {
                if (string.IsNullOrEmpty(textBoxSearch.Text))
                {
                    dataGrid1.DataSource = _trainList.GetAll();
                }
                else
                {
                    dataGrid1.DataSource = _trainList.Search(textBoxSearch.Text);
                    btnSearch.Text = "Show All";
                }
            }
            else
            {
                dataGrid1.DataSource = _trainList.GetAll();
                btnSearch.Text = "Search";
                textBoxSearch.Text = string.Empty;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            groupBoxAddEdit.Visible = false;
            trainListSelectedRow = dataGrid1.CurrentRow.Index;
            txt_trainNo.Text = string.Empty;
            departure_time.Value = DateTime.Now;
            txt_note.Text = string.Empty;
        }

        private void btnAddUpdate_Click(object sender, EventArgs e)
        {
            if (btnAddUpdate.Text == "Add")
            {
                TrainListModel trainList = new TrainListModel()
                {
                    VremeDolaska = departure_time.Value,
                    KomOznaka = txt_trainNo.Text,
                    Napomena = txt_note.Text
                };
                int resoult = _trainList.Insert(trainList);
                MessageBox.Show(resoult + " row(s) is added");
                // Refresh the table
                dataGrid1.DataSource = _trainList.GetAll();
                dataGrid1.ClearSelection();
                dataGrid1.Rows[dataGrid1.Rows.Count - 1].Selected = true;
                dataGrid1.FirstDisplayedScrollingRowIndex = dataGrid1.RowCount - 1;
                groupBoxAddEdit.Visible = false;
            }
            else
            {
                int id = (int)dataGrid1.Rows[trainListSelectedRow].Cells[0].Value;
                TrainListModel trainList = new TrainListModel()
                {
                    Id = id,
                    VremeDolaska = departure_time.Value,
                    KomOznaka = txt_trainNo.Text,
                    Napomena = txt_note.Text
                };
                int resoult = _trainList.Update(trainList);
                MessageBox.Show(resoult + " row is updated");
                // Refresh the table
                dataGrid1.DataSource = _trainList.GetAll();
                groupBoxAddEdit.Visible = false;
            }

        }

        private void setStyle()
        {
            //dataGrid1.BorderStyle = BorderStyle.None;
            dataGrid1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGrid1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGrid1.BackgroundColor = Color.White;
            dataGrid1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGrid1.AllowUserToAddRows = false;
            dataGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGrid1.RowHeadersVisible = false;
            dataGrid1.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;


            //dataGrid2.BorderStyle = BorderStyle.None;
            dataGrid2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGrid2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGrid2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGrid2.AllowUserToAddRows = false;
            dataGrid2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGrid2.RowHeadersVisible = false;
            dataGrid2.AutoSizeColumnsMode = (DataGridViewAutoSizeColumnsMode)DataGridViewAutoSizeColumnMode.Fill;
        }

        private void RefreshDatagridKontrolisano()
        {
            var select = " select TrainListStavke.ID,TrainListStavke.RedniBroj, OznakaKola, TrainListStavke.SerijaKola,SUBSTRING(OznakaKola, 5,4) as SIfraSerije, VagoniSerije.ID as IDSerije, " +
 " VagoniSerije.Serija, VagoniSerije.BrojOsovina, KontTip, TipKontenjera.ID, TipKontenjera.Naziv " +
" from TrainListStavke Left join VagoniSerije on SUBSTRING(OznakaKola, 5, 4) = VagoniSerije.BrojcanaSerija " +
" left join TipKontenjera on TipKontenjera.Naziv = TrainListStavke.KontTip Where TrainListId = " + Convert.ToInt32(textBoxSearch.Text);


            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

          

        }

        private void RefreshDatagridSumarno()
        {
            var select = " SELECT OznakaKola as Oznaka, Min(RedniBroj) as RB, Min(ID) as ID, Max(TaraKola) as Tara, Min(DuzinaKola) as DuzinaKOla, Sum(Neto) as Neto, SUM(KontTara) as KontTara  FROM TrainListStavke " +
            " Where TrainListId = " + Convert.ToInt32(textBoxSearch.Text) + 
            " group by OznakaKola ";


            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.White;



        }

        private void VratiSumuTaraKola()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("select SUM(t1.Tara) as Tara, SUM(t1.DuzinaKola) as DuzinaKola, Count(Oznaka) as BrojKola from " +
" (SELECT Rednibroj as Oznaka, Max(TaraKola) as Tara, Max(DuzinaKola) as DuzinaKOla FROM TrainListStavke  " +
"Where TrainListId= " + Convert.ToInt32(textBoxSearch.Text) + " group by redniBroj) t1 "  , con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                sTaraKola.Text = dr["Tara"].ToString();
                sDuzinaKola.Text =  dr["DuzinaKola"].ToString();
                txtBrojKola.Text = dr["BrojKola"].ToString();
            }
            con.Close();


        }

        private void VratiSumuNetoDuzinaKola()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT    Sum(Neto) as Neto, Sum(KontTara) as KontTara, (Sum(Neto) + Sum(KontTara)) as Bruto , Count(ID) as BrojKontejnera FROM TrainListStavke " +
            " Where TrainListId = " + Convert.ToInt32(textBoxSearch.Text) 
            , con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                sTaraKont.Text = dr["KontTara"].ToString();
                sNetoKont.Text = dr["Neto"].ToString();
                // txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                VratiBrojKontejnera();
                sBruto.Text = (Convert.ToDecimal(sTaraKont.Text) + Convert.ToDecimal(sNetoKont.Text) + Convert.ToDecimal(sTaraKola.Text)).ToString();
            }
            con.Close();


        }

        private void VratiBrojKontejnera()
        {
            var s_connection = ConfigurationManager.ConnectionStrings["WindowsFormsApplication1.Properties.Settings.NedraConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT    Count(ID) as BrojKontejnera FROM TrainListStavke " +
            " Where KontBroj <> '' and TrainListId = " + Convert.ToInt32(textBoxSearch.Text)
            , con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
               
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
               
            }
            con.Close();


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
      
        }
        private void SelectROWDataGRID1()
        {
           // dataGrid1.ReadOnly = false;
             dataGrid1.ClearSelection(); 
            foreach (DataGridViewRow row in dataGrid1.Rows)
            {
                if (row.Cells[0].Value.ToString() == textBoxSearch.Text )
                {
                    row.Selected = true;
                   // dataGrid1.CurrentRow.Index = dataGridView1.SelectedRows[0].Index;
                }
                  
            }
            VratiSumuTaraKola();
            VratiSumuNetoDuzinaKola();
            
            if (dataGrid1.RowCount > 0)
            {
                trainListSelectedRow = dataGrid1.SelectedRows[0].Index; ;
                List<TrainListItemModel> itemList = _trainListItem.GetAllBySuperiorId((int)dataGrid1.Rows[trainListSelectedRow].Cells[0].Value);
                dataGrid2.DataSource = itemList;
                dataGrid2.Columns[0].Width = 0;
                dataGrid2.Columns[1].Visible = false;


                Total totalCalc = new Total(itemList, (int)dataGrid1.Rows[trainListSelectedRow].Cells[0].Value);
                txt_TotalUnitTare.Text = totalCalc.TotalUnitTare.ToString() + " kg";
                txt_TotalGoods.Text = totalCalc.TotalGoods.ToString() + " kg";
                txt_TotalWagonTare.Text = totalCalc.TotalWagonTare.ToString() + " kg";
                txt_TotalWeight.Text = totalCalc.TotalWeight.ToString() + " kg";
                txt_TotalTrainLength.Text = totalCalc.TotalTrainLength.ToString() + " m";
            }

        }

        private void dataGrid1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertTrainList del = new InsertTrainList();
            del.DelSveStavkeTrainLista(Convert.ToInt32(textBoxSearch.Text));
            dataGrid1.Refresh();
        }
    }
}
