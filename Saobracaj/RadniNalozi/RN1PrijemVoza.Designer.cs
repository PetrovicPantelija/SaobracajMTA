
namespace Saobracaj.RadniNalozi
{
    partial class RN1PrijemVoza
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RN1PrijemVoza));
            this.txtDatumRasporeda = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDatumRealizacije = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbrojkontejnera = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbovrstakontejnera = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBrojPlombe = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cboUvoznik = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cboVrstaRobe = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboNaSkladiste = new System.Windows.Forms.ComboBox();
            this.txtNAPozicijaskladista = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cboNaPoziciju = new System.Windows.Forms.ComboBox();
            this.cboUsluge = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtNalogRealizovao = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboSaVoznog = new System.Windows.Forms.ComboBox();
            this.cboBrodar = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtNalogIzdao = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDatumRasporeda
            // 
            this.txtDatumRasporeda.CustomFormat = "dd.MM.yyyy HH:mm";
            this.txtDatumRasporeda.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDatumRasporeda.Location = new System.Drawing.Point(395, 36);
            this.txtDatumRasporeda.Margin = new System.Windows.Forms.Padding(2);
            this.txtDatumRasporeda.Name = "txtDatumRasporeda";
            this.txtDatumRasporeda.Size = new System.Drawing.Size(122, 20);
            this.txtDatumRasporeda.TabIndex = 179;
            this.txtDatumRasporeda.Value = new System.DateTime(2023, 6, 23, 0, 0, 0, 0);
            this.txtDatumRasporeda.ValueChanged += new System.EventHandler(this.txtDatumRasporeda_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(302, 36);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 13);
            this.label7.TabIndex = 180;
            this.label7.Text = "Datum rasporeda";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(850, 36);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 182;
            this.label6.Text = "Nalog izdao";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 184;
            this.label2.Text = "Sa voznog sredstva";
            // 
            // txtDatumRealizacije
            // 
            this.txtDatumRealizacije.CustomFormat = "dd.MM.yyyy HH:mm";
            this.txtDatumRealizacije.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDatumRealizacije.Location = new System.Drawing.Point(360, 68);
            this.txtDatumRealizacije.Margin = new System.Windows.Forms.Padding(2);
            this.txtDatumRealizacije.Name = "txtDatumRealizacije";
            this.txtDatumRealizacije.Size = new System.Drawing.Size(122, 20);
            this.txtDatumRealizacije.TabIndex = 185;
            this.txtDatumRealizacije.Value = new System.DateTime(2023, 6, 23, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(269, 76);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 186;
            this.label1.Text = " ";
            // 
            // txtbrojkontejnera
            // 
            this.txtbrojkontejnera.Location = new System.Drawing.Point(157, 35);
            this.txtbrojkontejnera.Margin = new System.Windows.Forms.Padding(2);
            this.txtbrojkontejnera.Name = "txtbrojkontejnera";
            this.txtbrojkontejnera.Size = new System.Drawing.Size(142, 20);
            this.txtbrojkontejnera.TabIndex = 187;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 188;
            this.label3.Text = "Broj kontejnera";
            // 
            // cbovrstakontejnera
            // 
            this.cbovrstakontejnera.FormattingEnabled = true;
            this.cbovrstakontejnera.ItemHeight = 13;
            this.cbovrstakontejnera.Location = new System.Drawing.Point(636, 32);
            this.cbovrstakontejnera.Margin = new System.Windows.Forms.Padding(2);
            this.cbovrstakontejnera.Name = "cbovrstakontejnera";
            this.cbovrstakontejnera.Size = new System.Drawing.Size(210, 21);
            this.cbovrstakontejnera.TabIndex = 189;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(548, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 190;
            this.label4.Text = "Vrsta kontejnera";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 412);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 192;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(506, 69);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 193;
            this.label11.Text = "Broj plombe";
            // 
            // txtBrojPlombe
            // 
            this.txtBrojPlombe.Location = new System.Drawing.Point(572, 66);
            this.txtBrojPlombe.Margin = new System.Windows.Forms.Padding(2);
            this.txtBrojPlombe.Name = "txtBrojPlombe";
            this.txtBrojPlombe.Size = new System.Drawing.Size(101, 20);
            this.txtBrojPlombe.TabIndex = 194;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(697, 69);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 195;
            this.label12.Text = "Uvoznik";
            // 
            // cboUvoznik
            // 
            this.cboUvoznik.FormattingEnabled = true;
            this.cboUvoznik.ItemHeight = 13;
            this.cboUvoznik.Location = new System.Drawing.Point(745, 63);
            this.cboUvoznik.Margin = new System.Windows.Forms.Padding(2);
            this.cboUvoznik.Name = "cboUvoznik";
            this.cboUvoznik.Size = new System.Drawing.Size(349, 21);
            this.cboUvoznik.TabIndex = 196;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 110);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 197;
            this.label13.Text = "Naziv brodara";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(287, 110);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(55, 13);
            this.label14.TabIndex = 199;
            this.label14.Text = "Vrsta robe";
            // 
            // cboVrstaRobe
            // 
            this.cboVrstaRobe.FormattingEnabled = true;
            this.cboVrstaRobe.ItemHeight = 13;
            this.cboVrstaRobe.Location = new System.Drawing.Point(345, 104);
            this.cboVrstaRobe.Margin = new System.Windows.Forms.Padding(2);
            this.cboVrstaRobe.Name = "cboVrstaRobe";
            this.cboVrstaRobe.Size = new System.Drawing.Size(186, 21);
            this.cboVrstaRobe.TabIndex = 200;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 148);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 201;
            this.label8.Text = "NA Skladiste";
            // 
            // cboNaSkladiste
            // 
            this.cboNaSkladiste.FormattingEnabled = true;
            this.cboNaSkladiste.ItemHeight = 13;
            this.cboNaSkladiste.Location = new System.Drawing.Point(86, 142);
            this.cboNaSkladiste.Margin = new System.Windows.Forms.Padding(2);
            this.cboNaSkladiste.Name = "cboNaSkladiste";
            this.cboNaSkladiste.Size = new System.Drawing.Size(186, 21);
            this.cboNaSkladiste.TabIndex = 202;
            // 
            // txtNAPozicijaskladista
            // 
            this.txtNAPozicijaskladista.AutoSize = true;
            this.txtNAPozicijaskladista.Location = new System.Drawing.Point(287, 148);
            this.txtNAPozicijaskladista.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtNAPozicijaskladista.Name = "txtNAPozicijaskladista";
            this.txtNAPozicijaskladista.Size = new System.Drawing.Size(105, 13);
            this.txtNAPozicijaskladista.TabIndex = 203;
            this.txtNAPozicijaskladista.Text = "NA Pozicija skladista";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(549, 107);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(131, 13);
            this.label15.TabIndex = 204;
            this.label15.Text = "ID i naziv planirane usluge";
            this.label15.Click += new System.EventHandler(this.label10_Click);
            // 
            // cboNaPoziciju
            // 
            this.cboNaPoziciju.FormattingEnabled = true;
            this.cboNaPoziciju.ItemHeight = 13;
            this.cboNaPoziciju.Location = new System.Drawing.Point(404, 145);
            this.cboNaPoziciju.Margin = new System.Windows.Forms.Padding(2);
            this.cboNaPoziciju.Name = "cboNaPoziciju";
            this.cboNaPoziciju.Size = new System.Drawing.Size(186, 21);
            this.cboNaPoziciju.TabIndex = 205;
            // 
            // cboUsluge
            // 
            this.cboUsluge.FormattingEnabled = true;
            this.cboUsluge.ItemHeight = 13;
            this.cboUsluge.Location = new System.Drawing.Point(684, 107);
            this.cboUsluge.Margin = new System.Windows.Forms.Padding(2);
            this.cboUsluge.Name = "cboUsluge";
            this.cboUsluge.Size = new System.Drawing.Size(186, 21);
            this.cboUsluge.TabIndex = 206;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(601, 149);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 13);
            this.label17.TabIndex = 207;
            this.label17.Text = "Nalog realizovao";
            // 
            // txtNalogRealizovao
            // 
            this.txtNalogRealizovao.Location = new System.Drawing.Point(708, 145);
            this.txtNalogRealizovao.Margin = new System.Windows.Forms.Padding(2);
            this.txtNalogRealizovao.Name = "txtNalogRealizovao";
            this.txtNalogRealizovao.Size = new System.Drawing.Size(162, 20);
            this.txtNalogRealizovao.TabIndex = 208;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(874, 96);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 13);
            this.label16.TabIndex = 209;
            this.label16.Text = "Napomena";
            // 
            // txtNapomena
            // 
            this.txtNapomena.Location = new System.Drawing.Point(937, 96);
            this.txtNapomena.Margin = new System.Windows.Forms.Padding(2);
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(156, 80);
            this.txtNapomena.TabIndex = 210;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(269, 70);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 211;
            this.label9.Text = "Datum realizacije";
            // 
            // cboSaVoznog
            // 
            this.cboSaVoznog.FormattingEnabled = true;
            this.cboSaVoznog.ItemHeight = 13;
            this.cboSaVoznog.Location = new System.Drawing.Point(113, 67);
            this.cboSaVoznog.Margin = new System.Windows.Forms.Padding(2);
            this.cboSaVoznog.Name = "cboSaVoznog";
            this.cboSaVoznog.Size = new System.Drawing.Size(144, 21);
            this.cboSaVoznog.TabIndex = 205;
            // 
            // cboBrodar
            // 
            this.cboBrodar.FormattingEnabled = true;
            this.cboBrodar.ItemHeight = 13;
            this.cboBrodar.Location = new System.Drawing.Point(86, 104);
            this.cboBrodar.Margin = new System.Windows.Forms.Padding(2);
            this.cboBrodar.Name = "cboBrodar";
            this.cboBrodar.Size = new System.Drawing.Size(186, 21);
            this.cboBrodar.TabIndex = 205;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DodgerBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1104, 27);
            this.toolStrip1.TabIndex = 212;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(24, 24);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(24, 24);
            this.tsSave.Text = "tsSave";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(24, 24);
            this.tsDelete.Text = "toolStripButton1";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 177);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1086, 428);
            this.dataGridView1.TabIndex = 213;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 39);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 188;
            this.label10.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(29, 36);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(42, 20);
            this.txtID.TabIndex = 187;
            // 
            // txtNalogIzdao
            // 
            this.txtNalogIzdao.Location = new System.Drawing.Point(916, 33);
            this.txtNalogIzdao.Margin = new System.Windows.Forms.Padding(2);
            this.txtNalogIzdao.Name = "txtNalogIzdao";
            this.txtNalogIzdao.Size = new System.Drawing.Size(177, 20);
            this.txtNalogIzdao.TabIndex = 187;
            // 
            // RN1PrijemVoza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1104, 615);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtNapomena);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtNalogRealizovao);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cboUsluge);
            this.Controls.Add(this.cboSaVoznog);
            this.Controls.Add(this.cboBrodar);
            this.Controls.Add(this.cboNaPoziciju);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtNAPozicijaskladista);
            this.Controls.Add(this.cboNaSkladiste);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboVrstaRobe);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cboUvoznik);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtBrojPlombe);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbovrstakontejnera);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtNalogIzdao);
            this.Controls.Add(this.txtbrojkontejnera);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDatumRealizacije);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDatumRasporeda);
            this.Controls.Add(this.label7);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RN1PrijemVoza";
            this.Text = "RN-PrijemVoza";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker txtDatumRasporeda;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtDatumRealizacije;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbrojkontejnera;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbovrstakontejnera;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBrojPlombe;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboUvoznik;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboVrstaRobe;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboNaSkladiste;
        private System.Windows.Forms.Label txtNAPozicijaskladista;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cboNaPoziciju;
        private System.Windows.Forms.ComboBox cboUsluge;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtNalogRealizovao;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboSaVoznog;
        private System.Windows.Forms.ComboBox cboBrodar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtNalogIzdao;
    }
}