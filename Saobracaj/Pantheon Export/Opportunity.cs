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

namespace Saobracaj.Pantheon_Export
{
    public partial class Opportunity : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;
        bool status = false;
        public Opportunity()
        {
            InitializeComponent();
            FillCombo();
            panel1.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void FillCombo()
        {
            SqlConnection conn = new SqlConnection(connect);

            var valuta = "Select VaSifra,VaNaziv From Valute";
            var valutaDa = new SqlDataAdapter(valuta, conn);
            var valutaDS = new DataSet();
            valutaDa.Fill(valutaDS);
            cboValuta.DataSource = valutaDS.Tables[0];
            cboValuta.DisplayMember = "VaNaziv";
            cboValuta.ValueMember = "VaSifra";

            var query2 = "Select ID,SifraSubjekta from Odeljenja order by Naziv2 desc";
            var da2 = new SqlDataAdapter(query2, conn);
            var ds2 = new DataSet();
            da2.Fill(ds2);
            cboOdeljenje.DataSource = ds2.Tables[0];
            cboOdeljenje.DisplayMember = "SifraSubjekta";
            cboOdeljenje.ValueMember = "ID";

            var query = "Select PaSifra,PaNaziv from Partnerji order by PaSifra";
            var da = new SqlDataAdapter(query, conn);
            var ds = new DataSet();
            da.Fill(ds);
            cboKlijent.DataSource = ds.Tables[0];
            cboKlijent.DisplayMember = "PaNaziv";
            cboKlijent.ValueMember = "PaSifra";

            var query3 = "select ID,RTrim(Opis) as Opis from Stanice order by ID asc";
            var da3 = new SqlDataAdapter( query3, conn);
            var ds3 = new DataSet();
            da3.Fill(ds3);
            cboStartPoint.DataSource = ds3.Tables[0];
            cboStartPoint.DisplayMember = "Opis";
            cboStartPoint.ValueMember= "ID";

            var query4 = "select ID,Opis from Stanice order by ID asc";
            var da4 = new SqlDataAdapter(query4, conn);
            var ds4 = new DataSet();
            da4.Fill(ds4);
            cboEndPoint.DataSource = ds4.Tables[0];
            cboEndPoint.DisplayMember = "Opis";
            cboEndPoint.ValueMember= "ID";
        }
        int idP;
        string oppID;
        private void tsNew_Click(object sender, EventArgs e)
        {
            status = true;
            var query = "Select (Max(ID)+1) as IDP From Opportunity";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr[0].GetType() == typeof(DBNull))
                { idP = 1; }
                else
                {
                    idP = Convert.ToInt32(dr[0].ToString());
                }
            }
            string g = DateTime.Now.ToString("yy");
            oppID = "OPP-" + idP.ToString() + "-" + g;

            conn.Close();
        }
        public static void ClearFormControls(Form form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txtbox = (TextBox)control;
                    txtbox.Text = string.Empty;
                }
                else if (control is CheckBox)
                {
                    CheckBox chkbox = (CheckBox)control;
                    chkbox.Checked = false;
                }
                else if (control is RadioButton)
                {
                    RadioButton rdbtn = (RadioButton)control;
                    rdbtn.Checked = false;
                }
                else if (control is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)control;
                    dtp.Value = DateTime.Now;
                }
            }
        }
        int won;
        int id;
        private void tsSave_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { won = 1; } else { won = 0; }

            InsertPatheonExport ins = new InsertPatheonExport();
            if (status == true)
            {
                ins.InsOpportunity(oppID, txtNaziv.Text.ToString().TrimEnd(), Convert.ToInt32(cboOdeljenje.SelectedValue), txtProizvod.Text.ToString().TrimEnd(), txtProductFor.Text.ToString().TrimEnd(), Convert.ToInt32(cboKlijent.SelectedValue),
                    txtKontaktOsoba.Text.ToString().TrimEnd(), cboOpportunity.Text, cboJob.Text.ToString().TrimEnd(), Convert.ToDecimal(txtBudzet.Text), cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtEstimated.Text),
                    txtOpis.Text.ToString().TrimEnd(), Convert.ToInt32(cboStartPoint.SelectedValue), Convert.ToInt32(cboEndPoint.SelectedValue), won);
            }
            else
            {
                ins.UpdOpportunity(id,txtNaziv.Text.ToString().TrimEnd(), Convert.ToInt32(cboOdeljenje.SelectedValue), txtProizvod.Text.ToString().TrimEnd(), txtProductFor.Text.ToString().TrimEnd(), Convert.ToInt32(cboKlijent.SelectedValue),
                   txtKontaktOsoba.Text.ToString().TrimEnd(), cboOpportunity.Text, cboJob.Text.ToString().TrimEnd(), Convert.ToDecimal(txtBudzet.Text), cboValuta.SelectedValue.ToString(), Convert.ToDecimal(txtEstimated.Text),
                   txtOpis.Text.ToString().TrimEnd(), Convert.ToInt32(cboStartPoint.SelectedValue), Convert.ToInt32(cboEndPoint.SelectedValue), won);
            }
            ClearFormControls(this);
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertPatheonExport ins = new InsertPatheonExport();
            ins.DelOpportunity(Convert.ToInt32(id));

            ClearFormControls(this);
        }

        private void Opportunity_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            var select = "Select Opportunity.ID,OppID,NazivPosla,Naziv2,RTrim(PaNaziv) as Klijent,OppType,JobType,Budzet,Valuta,EstimatedRevenue,Stanice.Opis as StartPoint,s.Opis as EndPoint," +
                "Odeljenja.ID as OdeljenjeID,PaSifra,Stanice.ID as StartID,s.ID as EndID,Product,ProductFor,Opportunity.Opis " +
                "From Opportunity " +
                "inner join Odeljenja on Opportunity.Odeljenje=Odeljenja.ID " +
                "inner join Partnerji on Opportunity.Klijent=Partnerji.PaSifra " +
                "inner join Stanice on Opportunity.PocetnaStanica=Stanice.ID " +
                "inner join Stanice as s on Opportunity.KrajnjaStanica=s.ID " +
                "Where Won=1 order by Opportunity.ID desc";

            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible=false;
            dataGridView1.Columns[17].Visible=false;
            dataGridView1.Columns[18].Visible = false;
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            var select = "Select Opportunity.ID,OppID,NazivPosla,Naziv2,RTrim(PaNaziv) as Klijent,OppType,JobType,Budzet,Valuta,EstimatedRevenue,Stanice.Opis as StartPoint,s.Opis as EndPoint," +
                "Odeljenja.ID as OdeljenjeID,PaSifra,Stanice.ID as StartID,s.ID as EndID,Product,ProductFor,Opportunity.Opis " +
                "From Opportunity " +
                "inner join Odeljenja on Opportunity.Odeljenje=Odeljenja.ID " +
                "inner join Partnerji on Opportunity.Klijent=Partnerji.PaSifra " +
                "inner join Stanice on Opportunity.PocetnaStanica=Stanice.ID " +
                "inner join Stanice as s on Opportunity.KrajnjaStanica=s.ID " +
                "Where Won=0 order by Opportunity.ID desc";

            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            dataGridView1.BorderStyle = BorderStyle.FixedSingle;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible = false;
            dataGridView1.Columns[17].Visible = false;
            dataGridView1.Columns[18].Visible = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in  dataGridView1.Rows)
            {
                if (row.Selected)
                {
                    id = Convert.ToInt32(row.Cells[0].Value);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var query = "Select Opportunity.ID,OppID,NazivPosla,Naziv2,RTrim(PaNaziv) as Klijent,OppType,JobType,Budzet,Valuta,EstimatedRevenue,Stanice.Opis as StartPoint,s.Opis as EndPoint," +
                "Odeljenja.ID as OdeljenjeID,PaSifra,Stanice.ID as StartID,s.ID as EndID,Product,ProductFor,Opportunity.Opis,KontaktOsoba,Won " +
                "From Opportunity " +
                "inner join Odeljenja on Opportunity.Odeljenje=Odeljenja.ID " +
                "inner join Partnerji on Opportunity.Klijent=Partnerji.PaSifra " +
                "inner join Stanice on Opportunity.PocetnaStanica=Stanice.ID " +
                "inner join Stanice as s on Opportunity.KrajnjaStanica=s.ID " +
                "Where Opportunity.ID="+id;
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                oppID = dr[1].ToString();
                txtNaziv.Text = dr[2].ToString();
                cboOdeljenje.SelectedValue = Convert.ToInt16(dr[12].ToString());
                cboKlijent.SelectedValue = Convert.ToInt32(dr[13].ToString());
                cboOpportunity.Text = dr[5].ToString();
                cboJob.Text = dr[6].ToString();
                txtBudzet.Text = dr[7].ToString();
                cboValuta.SelectedValue= dr[8].ToString();
                txtEstimated.Text= dr[9].ToString();
                txtProizvod.Text= dr[16].ToString();
                txtProductFor.Text= dr[17].ToString();
                txtOpis.Text= dr[18].ToString();
                cboStartPoint.SelectedValue = Convert.ToInt32(dr[14].ToString());
                cboEndPoint.SelectedValue = Convert.ToInt32(dr[15].ToString());
                txtKontaktOsoba.Text = dr[19].ToString();
                int W = Convert.ToInt32(dr[20].ToString());

                if (W == 1) { checkBox1.Checked = true; } else {  checkBox1.Checked = false; }
            }
            conn.Close();

            panel1.Visible = false;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
