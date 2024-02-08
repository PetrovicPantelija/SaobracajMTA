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

namespace Saobracaj.RadniNalozi
{


    public partial class frmPregledSkladistaNovi : Form
    {
        private string connect = Sifarnici.frmLogovanje.connectionString;

        public frmPregledSkladistaNovi()
        {
            InitializeComponent();
            groupBox1.Visible = false;
            BtnHandler(this.Controls);
        }

        private void frmPregledSkladistaNovi_Load(object sender, EventArgs e)
        {
            var query = "SELECT Naziv,ISNULL((SELECT STUFF((SELECT DISTINCT '\n' + CAST(Kontejner AS NVARCHAR(50)) FROM KontejnerTekuce WHERE KontejnerTekuce.Skladiste = Skladista.ID FOR XML PATH('')), 1, 1, '' ) AS Skupljen),'') AS Lokom FROM Skladista";

            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            if (dr.HasRows)
            {
                dt.Load(dr);
            }
            string dugme = "";
            string kNaziv = "";
            conn.Close();
            foreach(Control c in this.Controls)
            {
                if(c is Button)
                {
                    dugme = c.Text.ToString();

                    foreach (DataRow row in dt.Rows)
                    {
                        kNaziv = row[0].ToString().TrimEnd().ToUpper();
                        if (dugme.Equals(kNaziv))
                        {
                            var query2 = "SELECT ISNULL((SELECT STUFF((SELECT DISTINCT '\n' + CAST(Kontejner AS NVARCHAR(50)) FROM KontejnerTekuce WHERE KontejnerTekuce.Skladiste = Skladista.ID FOR XML PATH('')), 1, 1, '' ) AS Skupljen),'') AS Lokom FROM Skladista Where Skladista.Naziv='" + kNaziv + "'";
                            conn.Open();
                            SqlCommand cmd2 = new SqlCommand(query2, conn);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            string skupljen = string.Empty;
                            while (dr2.Read())
                            {
                                skupljen = dr2[0].ToString().Trim();
                            }
                            conn.Close();
                            int numberOfLines = skupljen.Split('\n').Length;
                            if (numberOfLines < 2)
                            {
                                c.BackColor = Color.DodgerBlue;
                                c.ForeColor = Color.White;
                            }
                            else if (numberOfLines >= 2 && numberOfLines < 7)
                            {
                                c.BackColor = Color.Yellow;
                            }
                            else
                            {
                                c.BackColor = Color.Red;
                                c.ForeColor = Color.White;
                            }
                        }
                    }
                }
            }
        }
        private void BtnHandler(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Cick;
                }
                else if (c.HasChildren)
                {
                    BtnHandler(c.Controls);
                }
            }
        }
        int id;
        private void Button_Cick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            SqlConnection conn = new SqlConnection(connect);
            if (btn != null)
            {
                string btnNaziv = btn.Text;
                var query = "SELECT ID,ISNUll(RTrim(Naziv)+'-'+RTrim(Kapacitet),0) as Skladiste, ISNULL((SELECT STUFF((SELECT DISTINCT '\n' + CAST(Kontejner AS NVARCHAR(50)) FROM KontejnerTekuce WHERE KontejnerTekuce.Skladiste = Skladista.ID FOR XML PATH('')), 1, 1, '' ) AS Skupljen),'') AS Lokom FROM Skladista WHERE Skladista.Naziv='"+btnNaziv.ToString().TrimEnd()+"'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    string skupljen = string.Empty;

                    while (dr.Read())
                    {
                        groupBox1.Visible = true;
                        id = Convert.ToInt32(dr["ID"]);
                        groupBox1.Text = dr[1].ToString();
                        skupljen = dr[2].ToString();
                        label4.Text = skupljen.ToString();
                    }
                    conn.Close();
                }



            }
        }
    }
}
