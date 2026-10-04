using Microsoft.Office.Interop.Excel;
using Saobracaj.Dokumenta;
using Saobracaj.Izvoz;
using Saobracaj.RadniNalozi;
using Syncfusion.Drawing;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Saobracaj.VSD
{
    public partial class DnevniAnaliza : Form
    {
        public DnevniAnaliza()
        {
            InitializeComponent();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            var select = "     SELECT [Plan].[ID] as PlanID" +
      " ,[Godina]       ,[Mesec] " +
      " ,[UkupnoDana]      ,[TekuceDana] " +
      " ,[Plan].[Naziv] as PlanNaziv  FROM [Plan] ";
  

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

            foreach (GridColumnDescriptor column in this.gridGroupingControl2.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl2);
    }

        private void gridGroupingControl2_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            if (gridGroupingControl2.Table.CurrentRecord != null)
            {
                textBox1.Text = gridGroupingControl2.Table.CurrentRecord.GetValue("PlanID").ToString();
                //Sve usluge za odredjeni kontejner

                var select = "";


                select = "  SELECT  [Datum]      ,[Komercijalista]      ,[Brend]      ,[Kolicina]      ,[PNC] " +
      " ,[PVrednost]      ,[RUC]      ,[Profit]      ,[PlanID]   FROM [VSD].[dbo].[DnevniERP] ";


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

                foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
                {
                    column.AllowFilter = true;
                }

                GridSummaryColumnDescriptor summaryColumnDescriptor = new GridSummaryColumnDescriptor();
                summaryColumnDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(192, 255, 162));
                summaryColumnDescriptor.DataMember = "Kolicina";
                summaryColumnDescriptor.Format = "{Sum}";
                summaryColumnDescriptor.Name = "Kolicina";
                summaryColumnDescriptor.SummaryType = Syncfusion.Grouping.SummaryType.Int32Aggregate;

                GridSummaryRowDescriptor summaryRowDescriptor = new GridSummaryRowDescriptor();
                summaryRowDescriptor.SummaryColumns.Add(summaryColumnDescriptor);
                summaryRowDescriptor.Appearance.AnySummaryCell.Interior = new BrushInfo(Color.FromArgb(255, 231, 162));

                this.gridGroupingControl1.TableDescriptor.SummaryRows.Add(summaryRowDescriptor);

                 GridDynamicFilter dynamicFilter = new GridDynamicFilter();
                  dynamicFilter.WireGrid(this.gridGroupingControl1);
            }
        }

        // ---------------------------------------------------------------------------------
        // Izvoz u Excel (list "Dnevni promet")
        // ---------------------------------------------------------------------------------
        private const string SviBrendovi = "Svi brendovi";
        private const string PodrazumevaniBrend = "1.REVLON";

        private void DnevniAnaliza_Load(object sender, EventArgs e)
        {
            try
            {
                UcitajPlanove();
                OsveziBrendoveIDatum();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neuspešno učitavanje planova: " + ex.Message, "Dnevne analize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UcitajPlanove()
        {
            var tabela = new System.Data.DataTable();
            using (var da = new SqlDataAdapter("SELECT ID, Naziv FROM dbo.[Plan] ORDER BY Godina DESC, ID DESC", Sifarnici.frmLogovanje.connectionString))
                da.Fill(tabela);

            cboPlan.DisplayMember = "Naziv";
            cboPlan.ValueMember = "ID";
            cboPlan.DataSource = tabela;
            cboPlan.SelectedIndex = tabela.Rows.Count > 0 ? 0 : -1;   // najnoviji plan
        }

        private int? IzabraniPlan()
        {
            if (cboPlan.SelectedValue == null || cboPlan.SelectedValue == DBNull.Value)
                return null;
            return Convert.ToInt32(cboPlan.SelectedValue);
        }

        // Brendovi izabranog plana (prva stavka "Svi brendovi") i poslednji uvezeni dan
        private void OsveziBrendoveIDatum()
        {
            cboBrend.Items.Clear();
            cboBrend.Items.Add(SviBrendovi);

            int? planId = IzabraniPlan();
            if (planId == null)
            {
                cboBrend.SelectedIndex = 0;
                return;
            }

            object poslednjiDan;
            using (var conn = new SqlConnection(Sifarnici.frmLogovanje.connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT DISTINCT Brend FROM dbo.DnevniERP WHERE PlanID = @PlanID ORDER BY Brend", conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", planId.Value);
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            if (rd[0] != DBNull.Value)
                                cboBrend.Items.Add(Convert.ToString(rd[0]));
                        }
                    }
                }
                using (var cmd = new SqlCommand("SELECT MAX(Datum) FROM dbo.DnevniERP WHERE PlanID = @PlanID", conn))
                {
                    cmd.Parameters.AddWithValue("@PlanID", planId.Value);
                    poslednjiDan = cmd.ExecuteScalar();
                }
            }

            int revlon = cboBrend.Items.IndexOf(PodrazumevaniBrend);
            cboBrend.SelectedIndex = revlon >= 0 ? revlon : 0;

            if (poslednjiDan != null && poslednjiDan != DBNull.Value)
            {
                dtpDoDatuma.Value = Convert.ToDateTime(poslednjiDan).Date;
            }
            else
            {
                // plan bez uvezenog prometa: poslednji dan meseca plana
                Tuple<int, int> mesec = new DnevniPrometExcelExport(Sifarnici.frmLogovanje.connectionString).MesecPlana(planId.Value);
                if (mesec != null)
                    dtpDoDatuma.Value = new DateTime(mesec.Item1, mesec.Item2, DateTime.DaysInMonth(mesec.Item1, mesec.Item2));
            }
        }

        private void cboPlan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                OsveziBrendoveIDatum();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neuspešno učitavanje brendova: " + ex.Message, "Dnevne analize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIzvozExcel_Click(object sender, EventArgs e)
        {
            int? planId = IzabraniPlan();
            if (planId == null)
            {
                MessageBox.Show("Izaberite plan.", "Izvoz u Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string brend = cboBrend.SelectedItem == null || (string)cboBrend.SelectedItem == SviBrendovi ? null : (string)cboBrend.SelectedItem;
            DateTime doDatuma = dtpDoDatuma.Value.Date;
            var izvoz = new DnevniPrometExcelExport(Sifarnici.frmLogovanje.connectionString);

            try
            {
                Tuple<int, int> mesec = izvoz.MesecPlana(planId.Value);
                if (mesec != null && (doDatuma.Year != mesec.Item1 || doDatuma.Month != mesec.Item2))
                {
                    MessageBox.Show("Datum mora biti u mesecu izabranog plana.", "Izvoz u Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Neuspešno čitanje plana: " + ex.Message, "Izvoz u Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string putanja;
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel (*.xlsx)|*.xlsx";
                sfd.FileName = DnevniPrometExcelExport.PredlogImena(cboPlan.Text, brend, doDatuma);
                if (sfd.ShowDialog(this) != DialogResult.OK)
                    return;
                putanja = sfd.FileName;
            }

            Cursor = Cursors.WaitCursor;
            btnIzvozExcel.Enabled = false;
            try
            {
                izvoz.Export(planId.Value, brend, doDatuma, putanja);
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Fajl nije sačuvan. Zatvorite fajl u Excelu i pokušajte ponovo.", "Izvoz u Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Nemate pravo upisa u izabrani folder. Izaberite drugi folder.", "Izvoz u Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Izvoz nije uspeo: " + ex.Message, "Izvoz u Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                Cursor = Cursors.Default;
                btnIzvozExcel.Enabled = true;
            }

            if (!izvoz.ImaPrometa)
                MessageBox.Show("Nema prometa za izabrane filtere.", "Izvoz u Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (MessageBox.Show("Fajl je sačuvan. Otvoriti ga?", "Izvoz u Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(putanja) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fajl ne može da se otvori: " + ex.Message, "Izvoz u Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void tabSplitterPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridGroupingControl2_Click(object sender, EventArgs e)
        {
           

              

           
         
        }
    }
}
