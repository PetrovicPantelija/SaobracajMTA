using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Saobracaj
{

    public partial class Proba : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;

        public Proba()
        {
            InitializeComponent();
            InitializeGrid();
        }
        private TableLayoutPanel gridContainer;
        private int currentPage = 0;
        private int pageSize = 100;

        private void InitializeGrid()
        {
            LoadPage();
        }
        int startRow = 1;
        int endRow = 153;
        private void LoadPage()
        {
            int rc = 0;


            var query = $"SELECT ID, Naziv +'-'+ Kapacitet, ISNULL((SELECT STUFF((SELECT DISTINCT '\n' + CAST(Kontejner AS NVARCHAR(50)) FROM KontejnerTekuce WHERE KontejnerTekuce.Skladiste = Skladista.ID FOR XML PATH('')), 1, 1, '' ) AS Skupljen),'') AS Lokom, ROW_NUMBER() OVER (ORDER BY ID) AS RowNum FROM Skladista WHERE Skladista.ID BETWEEN {startRow} AND {endRow}";

            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            if (dr.HasRows)
            {
                dt.Load(dr);
                rc = dt.Rows.Count;
            }
            conn.Close();

            gridContainer = new TableLayoutPanel();
            gridContainer.Dock = DockStyle.Fill;
            gridContainer.ColumnCount = 14;
            foreach (DataRow row in dt.Rows)
            {
                int id = Convert.ToInt32(row["ID"].ToString());

                //int id = i + (currentPage * pageSize);

                var query2 = $"SELECT Naziv +'-'+ Kapacitet, ISNULL((SELECT STUFF((SELECT DISTINCT '\n' + CAST(Kontejner AS NVARCHAR(50)) FROM KontejnerTekuce WHERE KontejnerTekuce.Skladiste = Skladista.ID FOR XML PATH('')), 1, 1, '' ) AS Skupljen),'') AS Lokom FROM Skladista WHERE Skladista.ID=" + id;
                conn.Open();
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                SqlDataReader dr2 = cmd2.ExecuteReader();

                string naziv = string.Empty;
                string skupljen = string.Empty;

                while (dr2.Read())
                {
                    naziv = dr2[0].ToString();
                    skupljen = dr2[1].ToString();
                }
                conn.Close();

                Panel gridItemPanel = new Panel();
                gridItemPanel.Dock = DockStyle.Left;
                gridItemPanel.Margin = new Padding(5);
                gridItemPanel.Size = new Size(120, 80);
                gridItemPanel.AutoSize = false;

                int numberOfLines = skupljen.Split('\n').Length;

                if (numberOfLines < 2)
                {
                    gridItemPanel.BackColor = Color.DodgerBlue;
                    gridItemPanel.ForeColor = Color.White;
                }
                else if (numberOfLines >= 2 && numberOfLines < 7)
                {
                    gridItemPanel.BackColor = Color.Yellow;
                }
                else
                {
                    gridItemPanel.BackColor = Color.Red;
                    gridItemPanel.ForeColor = Color.White;
                }

                Label nameLabel = new Label();
                nameLabel.Text = naziv;
                nameLabel.Dock = DockStyle.Top;
                nameLabel.TextAlign = ContentAlignment.MiddleCenter;

                Button skupljenButton = new Button();
                skupljenButton.Size = new Size(25, 25);
                skupljenButton.Text = "Pregled";
                skupljenButton.ForeColor = Color.Black;
                skupljenButton.BackColor = Color.LightGray;
                skupljenButton.Dock = DockStyle.Bottom;

                skupljenButton.Click += (sender, e) => ShowSkupljen(skupljen);

                ProgressBar skupljenProgressBar = new ProgressBar();
                skupljenProgressBar.Dock = DockStyle.Bottom;
                skupljenProgressBar.Size = new Size(100, 20);
                skupljenProgressBar.Maximum = 9;

                if (numberOfLines > 1)
                {
                    skupljenProgressBar.Value = Math.Min(numberOfLines, 9);
                }

                gridItemPanel.BorderStyle = BorderStyle.FixedSingle;

                gridItemPanel.Controls.Add(nameLabel);
                gridItemPanel.Controls.Add(skupljenButton);
                gridItemPanel.Controls.Add(skupljenProgressBar);

                gridContainer.Controls.Add(gridItemPanel);
            }

            if (rc == 153)
            {
                Button nextPageButton = new Button();
                nextPageButton.Text = "Sledeća strana";
                nextPageButton.Dock = DockStyle.Left;
                nextPageButton.Click += NextPageButton_Click;
                gridContainer.Controls.Add(nextPageButton);
            }
            if (rc < 153)
            {

                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Size = new Size(1900, 420);
            }
            Controls.Add(gridContainer);
        }

        private void NextPageButton_Click(object sender, EventArgs e)
        {
            // Increment the current page and load the next page
            if (startRow == 1)
            {
                startRow = 154;
                endRow = 305;
            }

            RemoveAll();
            InitializeGrid();
        }

        private void ShowSkupljen(string skupljenValue)
        {
            MessageBox.Show($"{skupljenValue}");
        }
        private void RemoveAll()
        {
            List<Control> controlsToRemove = new List<Control>();

            foreach (Control control in Controls)
            {
                if (control is Panel || control is Button || control is Label || control is ProgressBar)
                {
                    controlsToRemove.Add(control);
                }
                else if (control is Panel)
                {
                    RemovePanel((Panel)control);
                }
            }

            foreach (Control controlToRemove in controlsToRemove)
            {
                Controls.Remove(controlToRemove);
                controlToRemove.Dispose(); // This will permanently remove the control
            }
        }

        private void RemovePanel(Panel panel)
        {
            List<Control> innerControlsToRemove = new List<Control>();

            foreach (Control innerControl in panel.Controls)
            {
                if (innerControl is Panel || innerControl is Button || innerControl is Label || innerControl is ProgressBar)
                {
                    innerControlsToRemove.Add(innerControl);
                }
                else if (innerControl is Panel)
                {
                    RemovePanel((Panel)innerControl);
                }
            }

            foreach (Control innerControlToRemove in innerControlsToRemove)
            {
                panel.Controls.Remove(innerControlToRemove);
                innerControlToRemove.Dispose(); // This will permanently remove the control
            }
        }
    }
}
