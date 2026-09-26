using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Saobracaj.RNI
{
    // Pregled loga izmena (tabela TerminalPrivremeniLog, puni je trigger trg_TerminalPriv_Log)
    public partial class frmTerminalPrivremeniLog : Form
    {
        private const string Kolone = "LogID, TerminalPrivID, Datum, Akcija, Kontejner, Opis, Racunar";
        private const string Redosled = " ORDER BY Datum DESC, LogID DESC";
        private const string FormatDatuma = "dd.MM.yyyy HH:mm:ss";

        private readonly int id;

        public frmTerminalPrivremeniLog(int id, string kontejner)
        {
            this.id = id;
            InitializeComponent();

            Text = "Log - ID " + id + (string.IsNullOrEmpty(kontejner) ? "" : " (" + kontejner + ")");
            dtpOd.Value = DateTime.Today;
            dtpDo.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
        }

        private void frmTerminalPrivremeniLog_Load(object sender, EventArgs e)
        {
            PrikaziZaZapis();
        }

        private void Ucitaj(string sql, string opis, params SqlParameter[] parametri)
        {
            try
            {
                var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
                var dataAdapter = new SqlDataAdapter(sql, s_connection);
                dataAdapter.SelectCommand.Parameters.AddRange(parametri);

                var tabela = new DataTable();
                dataAdapter.Fill(tabela);
                dataGridView1.DataSource = tabela;

                dataGridView1.Columns["LogID"].Width = 70;
                dataGridView1.Columns["TerminalPrivID"].Width = 100;
                dataGridView1.Columns["Datum"].Width = 140;
                dataGridView1.Columns["Datum"].DefaultCellStyle.Format = FormatDatuma;
                dataGridView1.Columns["Akcija"].Width = 70;
                dataGridView1.Columns["Kontejner"].Width = 110;
                dataGridView1.Columns["Racunar"].Width = 120;
                dataGridView1.Columns["Opis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns["Opis"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

                lblStatus.Text = opis + "   |   Redova: " + tabela.Rows.Count;
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Greška pri učitavanju loga";
                MessageBox.Show("Neuspešno učitavanje loga: " + ex.Message, "Log", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Svi upisi za izabrani ID zapisa, najnoviji prvi
        private void PrikaziZaZapis()
        {
            Ucitaj("SELECT " + Kolone + " FROM TerminalPrivremeniLog WHERE TerminalPrivID = @ID" + Redosled,
                "Log za zapis ID " + id,
                new SqlParameter("@ID", SqlDbType.Int) { Value = id });
        }

        // Zadnjih 1000 upisa iz cele tabele loga (svi zapisi)
        private void PrikaziZadnjih1000()
        {
            Ucitaj("SELECT TOP (1000) " + Kolone + " FROM TerminalPrivremeniLog" + Redosled,
                "Zadnjih 1000 upisa iz celog loga");
        }

        // Upisi izabranog zapisa od-do, uključujući i granice (Do važi do kraja izabrane sekunde)
        private void PrikaziPoDatumu()
        {
            DateTime od = dtpOd.Value;
            DateTime doKraja = dtpDo.Value;
            if (od > doKraja)
            {
                MessageBox.Show("Datum 'Od' ne sme biti posle datuma 'Do'.", "Log", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Ucitaj("SELECT " + Kolone + " FROM TerminalPrivremeniLog WHERE TerminalPrivID = @ID AND Datum >= @Od AND Datum < @DoIskljucivo" + Redosled,
                "Log za zapis ID " + id + " od " + od.ToString(FormatDatuma) + " do " + doKraja.ToString(FormatDatuma),
                new SqlParameter("@ID", SqlDbType.Int) { Value = id },
                new SqlParameter("@Od", SqlDbType.DateTime) { Value = od },
                new SqlParameter("@DoIskljucivo", SqlDbType.DateTime) { Value = doKraja.AddSeconds(1) });
        }

        private void btnZaZapis_Click(object sender, EventArgs e)
        {
            PrikaziZaZapis();
        }

        private void btnZadnjih1000_Click(object sender, EventArgs e)
        {
            PrikaziZadnjih1000();
        }

        private void btnPoDatumu_Click(object sender, EventArgs e)
        {
            PrikaziPoDatumu();
        }
    }
}
