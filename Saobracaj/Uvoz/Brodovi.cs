using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Saobracaj.Uvoz
{
    public partial class Brodovi : Syncfusion.Windows.Forms.Office2010Form
    {
        bool status = false;
        string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;



        public Brodovi()
        {
            InitializeComponent();
            FillGV();
            sfButton1.Paint += sfButton1_Paint;
        }

        private void Brodovi_Load(object sender, EventArgs e)
        {

        }
        private void FillGV()
        {
            var select = "Select * From Brodovi order by ID desc";
            SqlConnection conn = new SqlConnection(connection);
            var da = new SqlDataAdapter(select, conn);
            var ds = new DataSet();
            da.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];


            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 50;

            DataGridViewColumn column2 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 250;

            DataGridViewColumn column3 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Oznaka";
            dataGridView1.Columns[2].Width = 70;

        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            tsNew.Enabled = false;
            status = true;
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            InsertBrodovi ins = new InsertBrodovi();
            if (status == true)
            {
                ins.InsBrodovi(txtNaziv.Text.ToString().TrimEnd(), txtOznaka.Text.ToString().TrimEnd());
            }
            else
            {
                ins.UpdBrodovi(Convert.ToInt32(txtID.Text), txtNaziv.Text.ToString().TrimEnd(), txtOznaka.Text.ToString().TrimEnd());
            }
            FillGV();
            tsNew.Enabled = true;
            status = false;
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            InsertBrodovi ins = new InsertBrodovi();
            ins.DelBrodovi(Convert.ToInt32(txtID.Text));
            FillGV();
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
                        txtNaziv.Text = row.Cells[1].Value.ToString();
                        txtOznaka.Text = row.Cells[2].Value.ToString();
                    }
                }
            }
            catch { }
        }

        private void txtNaziv_TextChanged(object sender, EventArgs e)
        {

        }

        private void sfButton1_Paint(object sender, PaintEventArgs e)
        {
            int radius = 5;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(this.sfButton1.ClientRectangle.X + 1,
                                           this.sfButton1.ClientRectangle.Y + 1,
                                           this.sfButton1.ClientRectangle.Width - 2,
                                           this.sfButton1.ClientRectangle.Height - 2);
            sfButton1.Region = new Region(GetRoundedRect(rect, radius));
            rect = new Rectangle(rect.X + 1, rect.Y + 1, rect.Width - 2, rect.Height - 2);
            e.Graphics.DrawPath(new Pen(Color.Red), GetRoundedRect(rect, radius));
        }
        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);
            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}

