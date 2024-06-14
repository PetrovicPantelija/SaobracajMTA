
namespace Saobracaj.Izvoz
{
    partial class frmOtpremaVozaIzPlana
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOtpremaVozaIzPlana));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cboPlanUtovara = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboOperater = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkCIRUradjen = new System.Windows.Forms.CheckBox();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.chkZatvoren = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRegBrKamiona = new System.Windows.Forms.TextBox();
            this.cboVozBuking = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.chkOtprema = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkNajava = new System.Windows.Forms.CheckBox();
            this.txtImeVozaca = new System.Windows.Forms.TextBox();
            this.chkVoz = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpVremeOdlaska = new System.Windows.Forms.DateTimePicker();
            this.cboStatusOtpreme = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dtpDatumOtpreme = new System.Windows.Forms.DateTimePicker();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNalogID = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
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
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1426, 27);
            this.toolStrip1.TabIndex = 245;
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
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(77, 24);
            this.toolStripButton1.Text = "OTVORI VOZ";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(288, 267);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 55);
            this.button2.TabIndex = 285;
            this.button2.Text = "Voz iz plana";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(388, 267);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 55);
            this.button1.TabIndex = 281;
            this.button1.Text = "Prenesi stavke iz plana";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboPlanUtovara
            // 
            this.cboPlanUtovara.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.cboPlanUtovara.FormattingEnabled = true;
            this.cboPlanUtovara.Location = new System.Drawing.Point(2, 291);
            this.cboPlanUtovara.Margin = new System.Windows.Forms.Padding(2);
            this.cboPlanUtovara.Name = "cboPlanUtovara";
            this.cboPlanUtovara.Size = new System.Drawing.Size(281, 21);
            this.cboPlanUtovara.TabIndex = 280;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(50)))));
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label44.Location = new System.Drawing.Point(3, 274);
            this.label44.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(93, 13);
            this.label44.TabIndex = 279;
            this.label44.Text = "Plan utovara voza";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 30);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.txtNalogID);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.cboOperater);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.chkCIRUradjen);
            this.splitContainer1.Panel1.Controls.Add(this.txtSifra);
            this.splitContainer1.Panel1.Controls.Add(this.chkZatvoren);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.txtRegBrKamiona);
            this.splitContainer1.Panel1.Controls.Add(this.cboVozBuking);
            this.splitContainer1.Panel1.Controls.Add(this.label15);
            this.splitContainer1.Panel1.Controls.Add(this.chkOtprema);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.chkNajava);
            this.splitContainer1.Panel1.Controls.Add(this.txtImeVozaca);
            this.splitContainer1.Panel1.Controls.Add(this.chkVoz);
            this.splitContainer1.Panel1.Controls.Add(this.label18);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.dtpVremeOdlaska);
            this.splitContainer1.Panel1.Controls.Add(this.cboStatusOtpreme);
            this.splitContainer1.Panel1.Controls.Add(this.label16);
            this.splitContainer1.Panel1.Controls.Add(this.dtpDatumOtpreme);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtNapomena);
            this.splitContainer1.Panel2.Controls.Add(this.label31);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.White;
            this.splitContainer1.Size = new System.Drawing.Size(1397, 203);
            this.splitContainer1.SplitterDistance = 851;
            this.splitContainer1.TabIndex = 288;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.checkBox2.ForeColor = System.Drawing.Color.White;
            this.checkBox2.Location = new System.Drawing.Point(259, 123);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(51, 17);
            this.checkBox2.TabIndex = 266;
            this.checkBox2.Text = "Izvoz";
            this.checkBox2.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(256, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 265;
            this.label1.Text = "Operator:";
            // 
            // cboOperater
            // 
            this.cboOperater.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboOperater.FormattingEnabled = true;
            this.cboOperater.Location = new System.Drawing.Point(259, 78);
            this.cboOperater.Name = "cboOperater";
            this.cboOperater.Size = new System.Drawing.Size(180, 24);
            this.cboOperater.TabIndex = 264;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(50)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(9, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 242;
            this.label2.Text = "ID:";
            // 
            // chkCIRUradjen
            // 
            this.chkCIRUradjen.AutoSize = true;
            this.chkCIRUradjen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkCIRUradjen.ForeColor = System.Drawing.Color.White;
            this.chkCIRUradjen.Location = new System.Drawing.Point(900, 99);
            this.chkCIRUradjen.Name = "chkCIRUradjen";
            this.chkCIRUradjen.Size = new System.Drawing.Size(88, 19);
            this.chkCIRUradjen.TabIndex = 263;
            this.chkCIRUradjen.Text = "CIR urađen";
            this.chkCIRUradjen.UseVisualStyleBackColor = true;
            this.chkCIRUradjen.Visible = false;
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifra.Location = new System.Drawing.Point(9, 29);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(105, 21);
            this.txtSifra.TabIndex = 241;
            // 
            // chkZatvoren
            // 
            this.chkZatvoren.AutoSize = true;
            this.chkZatvoren.Enabled = false;
            this.chkZatvoren.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkZatvoren.ForeColor = System.Drawing.Color.White;
            this.chkZatvoren.Location = new System.Drawing.Point(900, 74);
            this.chkZatvoren.Name = "chkZatvoren";
            this.chkZatvoren.Size = new System.Drawing.Size(73, 19);
            this.chkZatvoren.TabIndex = 262;
            this.chkZatvoren.Text = "Zatvoren";
            this.chkZatvoren.UseVisualStyleBackColor = true;
            this.chkZatvoren.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.ForeColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(500, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 251;
            this.label4.Text = "Reg br vozila:";
            this.label4.Visible = false;
            // 
            // txtRegBrKamiona
            // 
            this.txtRegBrKamiona.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtRegBrKamiona.Location = new System.Drawing.Point(503, 76);
            this.txtRegBrKamiona.Name = "txtRegBrKamiona";
            this.txtRegBrKamiona.Size = new System.Drawing.Size(150, 21);
            this.txtRegBrKamiona.TabIndex = 250;
            this.txtRegBrKamiona.Visible = false;
            // 
            // cboVozBuking
            // 
            this.cboVozBuking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboVozBuking.FormattingEnabled = true;
            this.cboVozBuking.Location = new System.Drawing.Point(503, 31);
            this.cboVozBuking.Name = "cboVozBuking";
            this.cboVozBuking.Size = new System.Drawing.Size(262, 23);
            this.cboVozBuking.TabIndex = 247;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label15.ForeColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(500, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(136, 13);
            this.label15.TabIndex = 258;
            this.label15.Text = "BROJ VOZA-  REALACIJA:";
            // 
            // chkOtprema
            // 
            this.chkOtprema.AutoSize = true;
            this.chkOtprema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkOtprema.ForeColor = System.Drawing.Color.White;
            this.chkOtprema.Location = new System.Drawing.Point(900, 52);
            this.chkOtprema.Name = "chkOtprema";
            this.chkOtprema.Size = new System.Drawing.Size(146, 19);
            this.chkOtprema.TabIndex = 257;
            this.chkOtprema.Text = "Poslat e mail otpreme";
            this.chkOtprema.UseVisualStyleBackColor = true;
            this.chkOtprema.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.ForeColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(504, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 253;
            this.label5.Text = "Ime Vozača:";
            this.label5.Visible = false;
            // 
            // chkNajava
            // 
            this.chkNajava.AutoSize = true;
            this.chkNajava.BackColor = System.Drawing.Color.Red;
            this.chkNajava.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkNajava.ForeColor = System.Drawing.Color.White;
            this.chkNajava.Location = new System.Drawing.Point(900, 29);
            this.chkNajava.Name = "chkNajava";
            this.chkNajava.Size = new System.Drawing.Size(151, 19);
            this.chkNajava.TabIndex = 256;
            this.chkNajava.Text = "Najava otpreme e-mail";
            this.chkNajava.UseVisualStyleBackColor = false;
            this.chkNajava.Visible = false;
            // 
            // txtImeVozaca
            // 
            this.txtImeVozaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtImeVozaca.Location = new System.Drawing.Point(503, 121);
            this.txtImeVozaca.Name = "txtImeVozaca";
            this.txtImeVozaca.Size = new System.Drawing.Size(188, 21);
            this.txtImeVozaca.TabIndex = 252;
            this.txtImeVozaca.Visible = false;
            // 
            // chkVoz
            // 
            this.chkVoz.AutoSize = true;
            this.chkVoz.Enabled = false;
            this.chkVoz.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkVoz.ForeColor = System.Drawing.Color.White;
            this.chkVoz.Location = new System.Drawing.Point(900, 7);
            this.chkVoz.Name = "chkVoz";
            this.chkVoz.Size = new System.Drawing.Size(64, 19);
            this.chkVoz.TabIndex = 254;
            this.chkVoz.Text = "Vozom";
            this.chkVoz.UseVisualStyleBackColor = true;
            this.chkVoz.Visible = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(11, 103);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(32, 13);
            this.label18.TabIndex = 249;
            this.label18.Text = "ATD:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(256, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 246;
            this.label3.Text = "Status:";
            // 
            // dtpVremeOdlaska
            // 
            this.dtpVremeOdlaska.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpVremeOdlaska.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpVremeOdlaska.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVremeOdlaska.Location = new System.Drawing.Point(9, 124);
            this.dtpVremeOdlaska.Name = "dtpVremeOdlaska";
            this.dtpVremeOdlaska.Size = new System.Drawing.Size(150, 21);
            this.dtpVremeOdlaska.TabIndex = 248;
            this.dtpVremeOdlaska.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // cboStatusOtpreme
            // 
            this.cboStatusOtpreme.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cboStatusOtpreme.FormattingEnabled = true;
            this.cboStatusOtpreme.Items.AddRange(new object[] {
            "1-Najava",
            "2-Otpremljen"});
            this.cboStatusOtpreme.Location = new System.Drawing.Point(259, 30);
            this.cboStatusOtpreme.Name = "cboStatusOtpreme";
            this.cboStatusOtpreme.Size = new System.Drawing.Size(150, 23);
            this.cboStatusOtpreme.TabIndex = 245;
            this.cboStatusOtpreme.Text = "1-Najava";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label16.ForeColor = System.Drawing.Color.Transparent;
            this.label16.Location = new System.Drawing.Point(10, 55);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 13);
            this.label16.TabIndex = 244;
            this.label16.Text = "ETD:";
            // 
            // dtpDatumOtpreme
            // 
            this.dtpDatumOtpreme.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpDatumOtpreme.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.dtpDatumOtpreme.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumOtpreme.Location = new System.Drawing.Point(13, 76);
            this.dtpDatumOtpreme.Name = "dtpDatumOtpreme";
            this.dtpDatumOtpreme.Size = new System.Drawing.Size(150, 21);
            this.dtpDatumOtpreme.TabIndex = 243;
            this.dtpDatumOtpreme.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // txtNapomena
            // 
            this.txtNapomena.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNapomena.Location = new System.Drawing.Point(9, 29);
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(458, 157);
            this.txtNapomena.TabIndex = 260;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(8, 8);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(110, 13);
            this.label31.TabIndex = 261;
            this.label31.Text = "NAPOMENA ZA VOZ";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1398, 272);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Stavke";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1377, 260);
            this.dataGridView1.TabIndex = 168;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(8, 328);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1406, 301);
            this.tabControl1.TabIndex = 282;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(504, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 268;
            this.label6.Text = "NalogID:";
            this.label6.Visible = false;
            // 
            // txtNalogID
            // 
            this.txtNalogID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtNalogID.Location = new System.Drawing.Point(503, 166);
            this.txtNalogID.Name = "txtNalogID";
            this.txtNalogID.Size = new System.Drawing.Size(152, 20);
            this.txtNalogID.TabIndex = 267;
            this.txtNalogID.Visible = false;
            // 
            // frmOtpremaVozaIzPlana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1426, 641);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cboPlanUtovara);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOtpremaVozaIzPlana";
            this.Text = "Formiranje otpreme voza iz plana";
            this.Load += new System.EventHandler(this.frmOtpremaVozaIzPlana_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cboPlanUtovara;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboOperater;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkCIRUradjen;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.CheckBox chkZatvoren;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRegBrKamiona;
        private System.Windows.Forms.ComboBox cboVozBuking;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox chkOtprema;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkNajava;
        private System.Windows.Forms.TextBox txtImeVozaca;
        private System.Windows.Forms.CheckBox chkVoz;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpVremeOdlaska;
        private System.Windows.Forms.ComboBox cboStatusOtpreme;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtpDatumOtpreme;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNalogID;
    }
}