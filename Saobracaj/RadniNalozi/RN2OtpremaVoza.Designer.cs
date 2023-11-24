
namespace Saobracaj.RadniNalozi
{
    partial class RN2OtpremaVoza
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RN2OtpremaVoza));
            this.txtDatumRasporeda = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDatumRealizacije = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbrojkontejnera = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboVrstaKontejnera = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tx = new System.Windows.Forms.Label();
            this.txtBrojPlombe = new System.Windows.Forms.TextBox();
            this.txtuvoznik = new System.Windows.Forms.Label();
            this.txtnazivbrodara = new System.Windows.Forms.Label();
            this.txtvrstarobe = new System.Windows.Forms.Label();
            this.cboRoba = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboSaSklad = new System.Windows.Forms.ComboBox();
            this.txtNAPozicijaskladista = new System.Windows.Forms.Label();
            this.txtIDinazivplaniraneusluge = new System.Windows.Forms.Label();
            this.cboSaPoz = new System.Windows.Forms.ComboBox();
            this.cboUsluga = new System.Windows.Forms.ComboBox();
            this.lbl15 = new System.Windows.Forms.Label();
            this.txtNalogRealizovao = new System.Windows.Forms.TextBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboNaSredstvo = new System.Windows.Forms.ComboBox();
            this.cboBrodar = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtBrojVagona = new System.Windows.Forms.TextBox();
            this.txtNalogIzdao = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDatumRasporeda
            // 
            this.txtDatumRasporeda.CustomFormat = "dd.MM.yyyy HH:mm";
            this.txtDatumRasporeda.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDatumRasporeda.Location = new System.Drawing.Point(425, 36);
            this.txtDatumRasporeda.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDatumRasporeda.Name = "txtDatumRasporeda";
            this.txtDatumRasporeda.Size = new System.Drawing.Size(122, 20);
            this.txtDatumRasporeda.TabIndex = 179;
            this.txtDatumRasporeda.Value = new System.DateTime(2023, 6, 23, 0, 0, 0, 0);
            this.txtDatumRasporeda.ValueChanged += new System.EventHandler(this.txtDatumRasporeda_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(332, 36);
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
            this.label6.Location = new System.Drawing.Point(887, 37);
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
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 184;
            this.label2.Text = "NA vozno sredstvo";
            // 
            // txtDatumRealizacije
            // 
            this.txtDatumRealizacije.CustomFormat = "dd.MM.yyyy HH:mm";
            this.txtDatumRealizacije.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDatumRealizacije.Location = new System.Drawing.Point(360, 68);
            this.txtDatumRealizacije.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.txtbrojkontejnera.Location = new System.Drawing.Point(185, 33);
            this.txtbrojkontejnera.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtbrojkontejnera.Name = "txtbrojkontejnera";
            this.txtbrojkontejnera.Size = new System.Drawing.Size(136, 20);
            this.txtbrojkontejnera.TabIndex = 187;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 188;
            this.label3.Text = "Broj kontejnera";
            // 
            // cboVrstaKontejnera
            // 
            this.cboVrstaKontejnera.FormattingEnabled = true;
            this.cboVrstaKontejnera.ItemHeight = 13;
            this.cboVrstaKontejnera.Location = new System.Drawing.Point(659, 33);
            this.cboVrstaKontejnera.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboVrstaKontejnera.Name = "cboVrstaKontejnera";
            this.cboVrstaKontejnera.Size = new System.Drawing.Size(215, 21);
            this.cboVrstaKontejnera.TabIndex = 189;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(571, 36);
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
            // tx
            // 
            this.tx.AutoSize = true;
            this.tx.Location = new System.Drawing.Point(506, 71);
            this.tx.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.tx.Name = "tx";
            this.tx.Size = new System.Drawing.Size(62, 13);
            this.tx.TabIndex = 193;
            this.tx.Text = "Broj plombe";
            // 
            // txtBrojPlombe
            // 
            this.txtBrojPlombe.Location = new System.Drawing.Point(572, 66);
            this.txtBrojPlombe.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBrojPlombe.Name = "txtBrojPlombe";
            this.txtBrojPlombe.Size = new System.Drawing.Size(101, 20);
            this.txtBrojPlombe.TabIndex = 194;
            // 
            // txtuvoznik
            // 
            this.txtuvoznik.AutoSize = true;
            this.txtuvoznik.Location = new System.Drawing.Point(696, 69);
            this.txtuvoznik.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtuvoznik.Name = "txtuvoznik";
            this.txtuvoznik.Size = new System.Drawing.Size(64, 13);
            this.txtuvoznik.TabIndex = 195;
            this.txtuvoznik.Text = "Broj vagona";
            // 
            // txtnazivbrodara
            // 
            this.txtnazivbrodara.AutoSize = true;
            this.txtnazivbrodara.Location = new System.Drawing.Point(9, 110);
            this.txtnazivbrodara.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtnazivbrodara.Name = "txtnazivbrodara";
            this.txtnazivbrodara.Size = new System.Drawing.Size(73, 13);
            this.txtnazivbrodara.TabIndex = 197;
            this.txtnazivbrodara.Text = "Naziv brodara";
            // 
            // txtvrstarobe
            // 
            this.txtvrstarobe.AutoSize = true;
            this.txtvrstarobe.Location = new System.Drawing.Point(287, 110);
            this.txtvrstarobe.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtvrstarobe.Name = "txtvrstarobe";
            this.txtvrstarobe.Size = new System.Drawing.Size(55, 13);
            this.txtvrstarobe.TabIndex = 199;
            this.txtvrstarobe.Text = "Vrsta robe";
            // 
            // cboRoba
            // 
            this.cboRoba.FormattingEnabled = true;
            this.cboRoba.ItemHeight = 13;
            this.cboRoba.Location = new System.Drawing.Point(345, 104);
            this.cboRoba.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboRoba.Name = "cboRoba";
            this.cboRoba.Size = new System.Drawing.Size(186, 21);
            this.cboRoba.TabIndex = 200;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 148);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 201;
            this.label8.Text = "SA  Skladiste";
            // 
            // cboSaSklad
            // 
            this.cboSaSklad.FormattingEnabled = true;
            this.cboSaSklad.ItemHeight = 13;
            this.cboSaSklad.Location = new System.Drawing.Point(86, 142);
            this.cboSaSklad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboSaSklad.Name = "cboSaSklad";
            this.cboSaSklad.Size = new System.Drawing.Size(186, 21);
            this.cboSaSklad.TabIndex = 202;
            // 
            // txtNAPozicijaskladista
            // 
            this.txtNAPozicijaskladista.AutoSize = true;
            this.txtNAPozicijaskladista.Location = new System.Drawing.Point(287, 148);
            this.txtNAPozicijaskladista.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtNAPozicijaskladista.Name = "txtNAPozicijaskladista";
            this.txtNAPozicijaskladista.Size = new System.Drawing.Size(104, 13);
            this.txtNAPozicijaskladista.TabIndex = 203;
            this.txtNAPozicijaskladista.Text = "SA Pozicija skladista";
            // 
            // txtIDinazivplaniraneusluge
            // 
            this.txtIDinazivplaniraneusluge.AutoSize = true;
            this.txtIDinazivplaniraneusluge.Location = new System.Drawing.Point(549, 107);
            this.txtIDinazivplaniraneusluge.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtIDinazivplaniraneusluge.Name = "txtIDinazivplaniraneusluge";
            this.txtIDinazivplaniraneusluge.Size = new System.Drawing.Size(131, 13);
            this.txtIDinazivplaniraneusluge.TabIndex = 204;
            this.txtIDinazivplaniraneusluge.Text = "ID i naziv planirane usluge";
            this.txtIDinazivplaniraneusluge.Click += new System.EventHandler(this.label10_Click);
            // 
            // cboSaPoz
            // 
            this.cboSaPoz.FormattingEnabled = true;
            this.cboSaPoz.ItemHeight = 13;
            this.cboSaPoz.Location = new System.Drawing.Point(404, 145);
            this.cboSaPoz.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboSaPoz.Name = "cboSaPoz";
            this.cboSaPoz.Size = new System.Drawing.Size(186, 21);
            this.cboSaPoz.TabIndex = 205;
            // 
            // cboUsluga
            // 
            this.cboUsluga.FormattingEnabled = true;
            this.cboUsluga.ItemHeight = 13;
            this.cboUsluga.Location = new System.Drawing.Point(684, 107);
            this.cboUsluga.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboUsluga.Name = "cboUsluga";
            this.cboUsluga.Size = new System.Drawing.Size(186, 21);
            this.cboUsluga.TabIndex = 206;
            // 
            // lbl15
            // 
            this.lbl15.AutoSize = true;
            this.lbl15.Location = new System.Drawing.Point(601, 149);
            this.lbl15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl15.Name = "lbl15";
            this.lbl15.Size = new System.Drawing.Size(86, 13);
            this.lbl15.TabIndex = 207;
            this.lbl15.Text = "Nalog realizovao";
            // 
            // txtNalogRealizovao
            // 
            this.txtNalogRealizovao.Location = new System.Drawing.Point(708, 145);
            this.txtNalogRealizovao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNalogRealizovao.Name = "txtNalogRealizovao";
            this.txtNalogRealizovao.Size = new System.Drawing.Size(162, 20);
            this.txtNalogRealizovao.TabIndex = 208;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(876, 71);
            this.lbl2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(59, 13);
            this.lbl2.TabIndex = 209;
            this.lbl2.Text = "Napomena";
            // 
            // txtNapomena
            // 
            this.txtNapomena.Location = new System.Drawing.Point(938, 72);
            this.txtNapomena.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(150, 90);
            this.txtNapomena.TabIndex = 210;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(260, 70);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 211;
            this.label9.Text = "Datum realizacije";
            // 
            // cboNaSredstvo
            // 
            this.cboNaSredstvo.FormattingEnabled = true;
            this.cboNaSredstvo.ItemHeight = 13;
            this.cboNaSredstvo.Location = new System.Drawing.Point(106, 68);
            this.cboNaSredstvo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cboNaSredstvo.Name = "cboNaSredstvo";
            this.cboNaSredstvo.Size = new System.Drawing.Size(144, 21);
            this.cboNaSredstvo.TabIndex = 205;
            // 
            // cboBrodar
            // 
            this.cboBrodar.FormattingEnabled = true;
            this.cboBrodar.ItemHeight = 13;
            this.cboBrodar.Location = new System.Drawing.Point(86, 104);
            this.cboBrodar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.toolStrip1.Size = new System.Drawing.Size(1098, 27);
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
            this.dataGridView1.Location = new System.Drawing.Point(9, 180);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1080, 416);
            this.dataGridView1.TabIndex = 213;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // txtBrojVagona
            // 
            this.txtBrojVagona.Location = new System.Drawing.Point(764, 67);
            this.txtBrojVagona.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBrojVagona.Name = "txtBrojVagona";
            this.txtBrojVagona.Size = new System.Drawing.Size(101, 20);
            this.txtBrojVagona.TabIndex = 194;
            // 
            // txtNalogIzdao
            // 
            this.txtNalogIzdao.Location = new System.Drawing.Point(956, 37);
            this.txtNalogIzdao.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNalogIzdao.Name = "txtNalogIzdao";
            this.txtNalogIzdao.Size = new System.Drawing.Size(132, 20);
            this.txtNalogIzdao.TabIndex = 194;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 36);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 188;
            this.label10.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(28, 33);
            this.txtID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(55, 20);
            this.txtID.TabIndex = 187;
            // 
            // RN2OtpremaVoza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1098, 605);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtNapomena);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.txtNalogRealizovao);
            this.Controls.Add(this.lbl15);
            this.Controls.Add(this.cboUsluga);
            this.Controls.Add(this.cboNaSredstvo);
            this.Controls.Add(this.cboBrodar);
            this.Controls.Add(this.cboSaPoz);
            this.Controls.Add(this.txtIDinazivplaniraneusluge);
            this.Controls.Add(this.txtNAPozicijaskladista);
            this.Controls.Add(this.cboSaSklad);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboRoba);
            this.Controls.Add(this.txtvrstarobe);
            this.Controls.Add(this.txtnazivbrodara);
            this.Controls.Add(this.txtuvoznik);
            this.Controls.Add(this.txtNalogIzdao);
            this.Controls.Add(this.txtBrojVagona);
            this.Controls.Add(this.txtBrojPlombe);
            this.Controls.Add(this.tx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboVrstaKontejnera);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtbrojkontejnera);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDatumRealizacije);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDatumRasporeda);
            this.Controls.Add(this.label7);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RN2OtpremaVoza";
            this.Text = "RN-OtpremaVoza";
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
        private System.Windows.Forms.ComboBox cboVrstaKontejnera;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label tx;
        private System.Windows.Forms.TextBox txtBrojPlombe;
        private System.Windows.Forms.Label txtuvoznik;
        private System.Windows.Forms.Label txtnazivbrodara;
        private System.Windows.Forms.Label txtvrstarobe;
        private System.Windows.Forms.ComboBox cboRoba;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboSaSklad;
        private System.Windows.Forms.Label txtNAPozicijaskladista;
        private System.Windows.Forms.Label txtIDinazivplaniraneusluge;
        private System.Windows.Forms.ComboBox cboSaPoz;
        private System.Windows.Forms.ComboBox cboUsluga;
        private System.Windows.Forms.Label lbl15;
        private System.Windows.Forms.TextBox txtNalogRealizovao;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboNaSredstvo;
        private System.Windows.Forms.ComboBox cboBrodar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtBrojVagona;
        private System.Windows.Forms.TextBox txtNalogIzdao;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtID;
    }
}