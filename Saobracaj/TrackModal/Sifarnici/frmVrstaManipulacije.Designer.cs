namespace Testiranje.Sifarnici
{
    partial class frmVrstaManipulacije
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVrstaManipulacije));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtJM = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkUticeSkladisno = new System.Windows.Forms.CheckBox();
            this.txtJM2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTipManipulacije = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboOrgJed = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOznaka = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRelacija = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCena = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkAdministratvna = new System.Windows.Forms.CheckBox();
            this.chkDrumski = new System.Windows.Forms.CheckBox();
            this.chkDodatna = new System.Windows.Forms.CheckBox();
            this.panelDodatnaUluga = new System.Windows.Forms.Panel();
            this.txtApstrakt1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtApstrakt2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkPotvrdaUradio = new System.Windows.Forms.CheckBox();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.tsPrvi = new System.Windows.Forms.ToolStripButton();
            this.tsNazad = new System.Windows.Forms.ToolStripButton();
            this.tsNapred = new System.Windows.Forms.ToolStripButton();
            this.tsPoslednja = new System.Windows.Forms.ToolStripButton();
            this.cboGrupaVrsteManipulacije = new Syncfusion.WinForms.ListView.SfComboBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCena)).BeginInit();
            this.panelDodatnaUluga.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupaVrsteManipulacije)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DodgerBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1,
            this.tsPrvi,
            this.tsNazad,
            this.tsNapred,
            this.tsPoslednja});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1173, 27);
            this.toolStrip1.TabIndex = 107;
            this.toolStrip1.Text = "Štampaj izveštaj";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtSifra.Location = new System.Drawing.Point(66, 54);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(105, 22);
            this.txtSifra.TabIndex = 108;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 109;
            this.label1.Text = "Šifra:";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtNaziv.Location = new System.Drawing.Point(66, 83);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(437, 22);
            this.txtNaziv.TabIndex = 110;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(9, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 111;
            this.label2.Text = "Naziv:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(12, 247);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1151, 279);
            this.dataGridView1.TabIndex = 112;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // txtJM
            // 
            this.txtJM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtJM.Location = new System.Drawing.Point(66, 111);
            this.txtJM.Name = "txtJM";
            this.txtJM.Size = new System.Drawing.Size(149, 22);
            this.txtJM.TabIndex = 114;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(9, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 115;
            this.label3.Text = "JM 1:";
            // 
            // chkUticeSkladisno
            // 
            this.chkUticeSkladisno.AutoSize = true;
            this.chkUticeSkladisno.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkUticeSkladisno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkUticeSkladisno.ForeColor = System.Drawing.Color.Black;
            this.chkUticeSkladisno.Location = new System.Drawing.Point(18, 142);
            this.chkUticeSkladisno.Name = "chkUticeSkladisno";
            this.chkUticeSkladisno.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkUticeSkladisno.Size = new System.Drawing.Size(109, 19);
            this.chkUticeSkladisno.TabIndex = 238;
            this.chkUticeSkladisno.Text = "Utiče skladišno";
            this.chkUticeSkladisno.UseVisualStyleBackColor = true;
            // 
            // txtJM2
            // 
            this.txtJM2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtJM2.Location = new System.Drawing.Point(305, 111);
            this.txtJM2.Name = "txtJM2";
            this.txtJM2.Size = new System.Drawing.Size(149, 22);
            this.txtJM2.TabIndex = 239;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(246, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 240;
            this.label4.Text = "JM 2:";
            // 
            // cboTipManipulacije
            // 
            this.cboTipManipulacije.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboTipManipulacije.FormattingEnabled = true;
            this.cboTipManipulacije.Location = new System.Drawing.Point(699, 113);
            this.cboTipManipulacije.Name = "cboTipManipulacije";
            this.cboTipManipulacije.Size = new System.Drawing.Size(462, 24);
            this.cboTipManipulacije.TabIndex = 242;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(618, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 241;
            this.label5.Text = "Tip usluge:";
            // 
            // cboOrgJed
            // 
            this.cboOrgJed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboOrgJed.FormattingEnabled = true;
            this.cboOrgJed.Location = new System.Drawing.Point(696, 83);
            this.cboOrgJed.Name = "cboOrgJed";
            this.cboOrgJed.Size = new System.Drawing.Size(210, 24);
            this.cboOrgJed.TabIndex = 244;
            this.cboOrgJed.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(533, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 28);
            this.label6.TabIndex = 243;
            this.label6.Text = "OJ koja izvšava zadatak:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtOznaka
            // 
            this.txtOznaka.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtOznaka.Location = new System.Drawing.Point(699, 143);
            this.txtOznaka.Name = "txtOznaka";
            this.txtOznaka.Size = new System.Drawing.Size(157, 22);
            this.txtOznaka.TabIndex = 245;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(628, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 246;
            this.label7.Text = "Oznaka:";
            // 
            // txtRelacija
            // 
            this.txtRelacija.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtRelacija.Location = new System.Drawing.Point(699, 171);
            this.txtRelacija.Name = "txtRelacija";
            this.txtRelacija.Size = new System.Drawing.Size(462, 22);
            this.txtRelacija.TabIndex = 247;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(630, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 15);
            this.label8.TabIndex = 248;
            this.label8.Text = "Relacija:";
            // 
            // txtCena
            // 
            this.txtCena.DecimalPlaces = 2;
            this.txtCena.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtCena.Location = new System.Drawing.Point(751, 200);
            this.txtCena.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtCena.Name = "txtCena";
            this.txtCena.Size = new System.Drawing.Size(105, 22);
            this.txtCena.TabIndex = 249;
            this.txtCena.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(630, 205);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 15);
            this.label13.TabIndex = 250;
            this.label13.Text = "Cena JM1:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(873, 207);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 16);
            this.label9.TabIndex = 251;
            this.label9.Text = "EUR";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(202, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 15);
            this.label10.TabIndex = 252;
            this.label10.Text = "Grupa usluge:";
            // 
            // chkAdministratvna
            // 
            this.chkAdministratvna.AutoSize = true;
            this.chkAdministratvna.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAdministratvna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkAdministratvna.ForeColor = System.Drawing.Color.Black;
            this.chkAdministratvna.Location = new System.Drawing.Point(18, 166);
            this.chkAdministratvna.Name = "chkAdministratvna";
            this.chkAdministratvna.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkAdministratvna.Size = new System.Drawing.Size(109, 19);
            this.chkAdministratvna.TabIndex = 254;
            this.chkAdministratvna.Text = "Administrativna";
            this.chkAdministratvna.UseVisualStyleBackColor = true;
            // 
            // chkDrumski
            // 
            this.chkDrumski.AutoSize = true;
            this.chkDrumski.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDrumski.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkDrumski.ForeColor = System.Drawing.Color.Black;
            this.chkDrumski.Location = new System.Drawing.Point(11, 191);
            this.chkDrumski.Name = "chkDrumski";
            this.chkDrumski.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkDrumski.Size = new System.Drawing.Size(116, 19);
            this.chkDrumski.TabIndex = 255;
            this.chkDrumski.Text = "Drumska usluga";
            this.chkDrumski.UseVisualStyleBackColor = true;
            // 
            // chkDodatna
            // 
            this.chkDodatna.AutoSize = true;
            this.chkDodatna.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDodatna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkDodatna.ForeColor = System.Drawing.Color.Black;
            this.chkDodatna.Location = new System.Drawing.Point(14, 215);
            this.chkDodatna.Name = "chkDodatna";
            this.chkDodatna.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkDodatna.Size = new System.Drawing.Size(113, 19);
            this.chkDodatna.TabIndex = 256;
            this.chkDodatna.Text = "Dodatna usluga";
            this.chkDodatna.UseVisualStyleBackColor = true;
            this.chkDodatna.CheckedChanged += new System.EventHandler(this.chkDodatna_CheckedChanged);
            // 
            // panelDodatnaUluga
            // 
            this.panelDodatnaUluga.Controls.Add(this.chkPotvrdaUradio);
            this.panelDodatnaUluga.Controls.Add(this.txtApstrakt2);
            this.panelDodatnaUluga.Controls.Add(this.label12);
            this.panelDodatnaUluga.Controls.Add(this.txtApstrakt1);
            this.panelDodatnaUluga.Controls.Add(this.label11);
            this.panelDodatnaUluga.Location = new System.Drawing.Point(168, 143);
            this.panelDodatnaUluga.Name = "panelDodatnaUluga";
            this.panelDodatnaUluga.Size = new System.Drawing.Size(406, 98);
            this.panelDodatnaUluga.TabIndex = 257;
            this.panelDodatnaUluga.Visible = false;
            this.panelDodatnaUluga.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDodatnaUluga_Paint);
            // 
            // txtApstrakt1
            // 
            this.txtApstrakt1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtApstrakt1.Location = new System.Drawing.Point(81, 34);
            this.txtApstrakt1.Name = "txtApstrakt1";
            this.txtApstrakt1.Size = new System.Drawing.Size(310, 22);
            this.txtApstrakt1.TabIndex = 257;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(6, 37);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 15);
            this.label11.TabIndex = 258;
            this.label11.Text = "Naziv kol 1:";
            // 
            // txtApstrakt2
            // 
            this.txtApstrakt2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtApstrakt2.Location = new System.Drawing.Point(81, 62);
            this.txtApstrakt2.Name = "txtApstrakt2";
            this.txtApstrakt2.Size = new System.Drawing.Size(310, 22);
            this.txtApstrakt2.TabIndex = 259;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(6, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 15);
            this.label12.TabIndex = 260;
            this.label12.Text = "Naziv kol 2:";
            // 
            // chkPotvrdaUradio
            // 
            this.chkPotvrdaUradio.AutoSize = true;
            this.chkPotvrdaUradio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkPotvrdaUradio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkPotvrdaUradio.ForeColor = System.Drawing.Color.Black;
            this.chkPotvrdaUradio.Location = new System.Drawing.Point(81, 9);
            this.chkPotvrdaUradio.Name = "chkPotvrdaUradio";
            this.chkPotvrdaUradio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.chkPotvrdaUradio.Size = new System.Drawing.Size(107, 19);
            this.chkPotvrdaUradio.TabIndex = 261;
            this.chkPotvrdaUradio.Text = "Potvrda Uradio";
            this.chkPotvrdaUradio.UseVisualStyleBackColor = true;
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
            // tsPrvi
            // 
            this.tsPrvi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPrvi.Image = ((System.Drawing.Image)(resources.GetObject("tsPrvi.Image")));
            this.tsPrvi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrvi.Name = "tsPrvi";
            this.tsPrvi.Size = new System.Drawing.Size(24, 24);
            this.tsPrvi.Text = "toolStripButton1";
            this.tsPrvi.Click += new System.EventHandler(this.tsPrvi_Click);
            // 
            // tsNazad
            // 
            this.tsNazad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNazad.Image = ((System.Drawing.Image)(resources.GetObject("tsNazad.Image")));
            this.tsNazad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNazad.Name = "tsNazad";
            this.tsNazad.Size = new System.Drawing.Size(24, 24);
            this.tsNazad.Text = "toolStripButton1";
            this.tsNazad.Click += new System.EventHandler(this.tsNazad_Click);
            // 
            // tsNapred
            // 
            this.tsNapred.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNapred.Image = ((System.Drawing.Image)(resources.GetObject("tsNapred.Image")));
            this.tsNapred.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNapred.Name = "tsNapred";
            this.tsNapred.Size = new System.Drawing.Size(24, 24);
            this.tsNapred.Text = "toolStripButton1";
            this.tsNapred.Click += new System.EventHandler(this.tsNapred_Click);
            // 
            // tsPoslednja
            // 
            this.tsPoslednja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPoslednja.Image = ((System.Drawing.Image)(resources.GetObject("tsPoslednja.Image")));
            this.tsPoslednja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPoslednja.Name = "tsPoslednja";
            this.tsPoslednja.Size = new System.Drawing.Size(24, 24);
            this.tsPoslednja.Text = "toolStripButton1";
            this.tsPoslednja.Click += new System.EventHandler(this.tsPoslednja_Click);
            // 
            // cboGrupaVrsteManipulacije
            // 
            this.cboGrupaVrsteManipulacije.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.cboGrupaVrsteManipulacije.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboGrupaVrsteManipulacije.Location = new System.Drawing.Point(292, 48);
            this.cboGrupaVrsteManipulacije.Name = "cboGrupaVrsteManipulacije";
            this.cboGrupaVrsteManipulacije.Size = new System.Drawing.Size(871, 28);
            this.cboGrupaVrsteManipulacije.Style.EditorStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboGrupaVrsteManipulacije.Style.ReadOnlyEditorStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboGrupaVrsteManipulacije.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cboGrupaVrsteManipulacije.Style.TokenStyle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboGrupaVrsteManipulacije.TabIndex = 259;
            // 
            // frmVrstaManipulacije
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1173, 537);
            this.Controls.Add(this.cboGrupaVrsteManipulacije);
            this.Controls.Add(this.panelDodatnaUluga);
            this.Controls.Add(this.chkDrumski);
            this.Controls.Add(this.chkAdministratvna);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.chkDodatna);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCena);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtRelacija);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtOznaka);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboOrgJed);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboTipManipulacije);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtJM2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkUticeSkladisno);
            this.Controls.Add(this.txtJM);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVrstaManipulacije";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vrsta usluge";
            this.Load += new System.EventHandler(this.frmVrstaManipulacije_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCena)).EndInit();
            this.panelDodatnaUluga.ResumeLayout(false);
            this.panelDodatnaUluga.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboGrupaVrsteManipulacije)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsPrvi;
        private System.Windows.Forms.ToolStripButton tsNazad;
        private System.Windows.Forms.ToolStripButton tsNapred;
        private System.Windows.Forms.ToolStripButton tsPoslednja;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtJM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkUticeSkladisno;
        private System.Windows.Forms.TextBox txtJM2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboTipManipulacije;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboOrgJed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOznaka;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRelacija;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown txtCena;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkAdministratvna;
        private System.Windows.Forms.CheckBox chkDrumski;
        private System.Windows.Forms.CheckBox chkDodatna;
        private System.Windows.Forms.Panel panelDodatnaUluga;
        private System.Windows.Forms.CheckBox chkPotvrdaUradio;
        private System.Windows.Forms.TextBox txtApstrakt2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtApstrakt1;
        private System.Windows.Forms.Label label11;
        private Syncfusion.WinForms.ListView.SfComboBox cboGrupaVrsteManipulacije;
    }
}