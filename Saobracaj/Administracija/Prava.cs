using Saobracaj.Sifarnici;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.Administracija
{
    public partial class Prava : Syncfusion.Windows.Forms.Office2010Form
    {
        string connect = frmLogovanje.connectionString;
        public Prava()
        {
            InitializeComponent();
        }
        List<int> mainKorisnik = new List<int>();
        List<int> karticeKorisnik = new List<int>();
        List<int> formeKorisnik = new List<int>();
        private void FillCombo()
        {
            var select = "Select RTrim(Korisnik) as Korisnik,RTrim(DeIme)+' '+RTrim(DePriimek)+' -- '+RTrim(Korisnik) as k " +
                "From Korisnici " +
                "Inner join Delavci on Korisnici.DeSifra=Delavci.DeSifra " +
                "order by DeIme asc";
            SqlConnection conn = new SqlConnection(connect);
            var dataAdapter = new SqlDataAdapter(select, conn);
            var ds = new System.Data.DataSet();
            dataAdapter.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.DisplayMember = "k";
            comboBox1.ValueMember = "Korisnik";
        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        private void Prava_Load(object sender, EventArgs e)
        {
            FillCombo();
            PopulateTreeView();
        }
        private void PopulateTreeView()
        {
            treeView1.Nodes.Clear();
            string query = "SELECT ID, Naziv FROM KarticeMain";
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TreeNode mainNode = new TreeNode(reader["Naziv"].ToString());
                mainNode.Tag = reader["ID"];
                treeView1.Nodes.Add(mainNode);

                if (mainKorisnik.Contains(Convert.ToInt32(mainNode.Tag)))
                {
                    mainNode.Checked = true;
                }
                PopulateKarticeNodes(mainNode);
            }
            reader.Close();
            conn.Close();
        }
        private void PopulateKarticeNodes(TreeNode parentNode)
        {
            int mainID = (int)parentNode.Tag;
            SqlConnection conn = new SqlConnection(connect);
            string query = "SELECT ID, Naziv FROM Kartice WHERE MainID = @MainID";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@MainID", mainID);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TreeNode karticeNode = new TreeNode(reader["Naziv"].ToString());
                karticeNode.Tag = reader["ID"];
                parentNode.Nodes.Add(karticeNode);

                if (karticeKorisnik.Contains(Convert.ToInt32(karticeNode.Tag)))
                {
                    karticeNode.Checked = true;
                }
                PopulateKarticeFormeNodes(karticeNode);
            }
            reader.Close();
            conn.Close();
        }
        private void PopulateKarticeFormeNodes(TreeNode parentNode)
        {
            string query = "SELECT ID, Naziv FROM KarticeForme WHERE KarticaID = @KarticeID";
            SqlConnection conn = new SqlConnection(connect);
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@KarticeID", parentNode.Tag);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TreeNode karticeFormeNode = new TreeNode(reader["Naziv"].ToString());
                karticeFormeNode.Tag = reader["ID"];
                parentNode.Nodes.Add(karticeFormeNode);

                if (formeKorisnik.Contains(Convert.ToInt32(karticeFormeNode.Tag)))
                {
                    karticeFormeNode.Checked = true;
                }

            }
            reader.Close();
            conn.Close();
        }
        private void PravoKorisnik()
        {
            mainKorisnik.Clear();
            karticeKorisnik.Clear();
            formeKorisnik.Clear();

            string query = "select MainID as Main,KarticaID as Kartica,FormaID as Forma From KarticeKorisnik Where Korisnik = '" + comboBox1.SelectedValue.ToString().TrimEnd() + "'";
            string connect = frmLogovanje.connectionString;
            using (SqlConnection conn = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    mainKorisnik.Add(Convert.ToInt32(dr["Main"].ToString()));
                    karticeKorisnik.Add(Convert.ToInt32(dr["Kartica"].ToString()));
                    formeKorisnik.Add(Convert.ToInt32(dr["Forma"].ToString()));
                }
                conn.Close();
            }
            PopulateTreeView();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PravoKorisnik();
        }
        List<int> mainList = new List<int>();
        List<int> karticeList = new List<int>();
        List<int> formeList = new List<int>();
        private void GetNodes(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    if (node.Parent == null)
                    {
                        mainList.Add((int)node.Tag);
                    }
                    else if (node.Parent.Parent == null)
                    {
                        karticeList.Add((int)node.Tag);
                    }
                    else
                    {
                        formeList.Add((int)node.Tag);
                    }
                }
                GetNodes(node.Nodes);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            GetNodes(treeView1.Nodes);
            InsertKorisnici ins = new InsertKorisnici();

            string obavestenje = "Dodela prava Korisniku:"+comboBox1.SelectedValue.ToString();
            Obavestenje frm = new Obavestenje(obavestenje);
            frm.Show();
            Application.DoEvents();

            int max=0;
            string queryMax = "select Count(ID) From KarticeForme";
            string connect = frmLogovanje.connectionString;
            using (SqlConnection conn = new SqlConnection(connect))
            {
                SqlCommand cmd = new SqlCommand(queryMax, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    max = Convert.ToInt32(dr[0].ToString());
                }
                conn.Close();
            }
            frm.SetProgressBarMaximum(max);

          //  ins.DeletePravo(comboBox1.SelectedValue.ToString().TrimEnd());

            foreach (var i in formeList)
            {
                int main = 0, kartica = 0;
                string query = "select KarticaID From KarticeForme Where ID =" + Convert.ToInt32(i);
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        kartica = Convert.ToInt32(dr[0].ToString());
                    }
                    conn.Close();
                }
                string query2 = "Select MainID From Kartice Where ID=" + kartica;
                using (SqlConnection conn = new SqlConnection(connect))
                {
                    SqlCommand cmd2 = new SqlCommand(query2, conn);
                    conn.Open();
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        main = Convert.ToInt32(dr2[0].ToString());
                    }
                    conn.Close();
                }
                ins.InsertPravo(comboBox1.SelectedValue.ToString().Trim(), main, kartica, Convert.ToInt32(i));
                frm.IncrementProgressBar();
            }
            mainList.Clear();
            karticeList.Clear();
            formeList.Clear();


            PravoKorisnik();
            frm.Close();
        }
    }
}
