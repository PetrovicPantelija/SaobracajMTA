
namespace Saobracaj.Dokumenta
{
    partial class frmPodtrase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPodtrase));
            Syncfusion.Windows.Forms.MetroColorTable metroColorTable2 = new Syncfusion.Windows.Forms.MetroColorTable();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrvi = new System.Windows.Forms.ToolStripButton();
            this.tsNazad = new System.Windows.Forms.ToolStripButton();
            this.tsNapred = new System.Windows.Forms.ToolStripButton();
            this.tsPoslednja = new System.Windows.Forms.ToolStripButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboStanicaOd = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboStanicaDo = new System.Windows.Forms.ComboBox();
            this.cmbKlasa = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.chkElektrificirana = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRastojanjeKM = new System.Windows.Forms.NumericUpDown();
            this.txtTrase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtRN = new System.Windows.Forms.TextBox();
            this.cboPruge = new Syncfusion.Windows.Forms.Tools.MultiColumnComboBox();
            this.txtRB = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRastojanjeKM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPruge)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
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
            this.toolStrip1.Size = new System.Drawing.Size(931, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "Osveži";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(23, 22);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(23, 22);
            this.tsSave.Text = "tsSave";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(23, 22);
            this.tsDelete.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsPrvi
            // 
            this.tsPrvi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPrvi.Image = ((System.Drawing.Image)(resources.GetObject("tsPrvi.Image")));
            this.tsPrvi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrvi.Name = "tsPrvi";
            this.tsPrvi.Size = new System.Drawing.Size(23, 22);
            this.tsPrvi.Text = "toolStripButton1";
            // 
            // tsNazad
            // 
            this.tsNazad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNazad.Image = ((System.Drawing.Image)(resources.GetObject("tsNazad.Image")));
            this.tsNazad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNazad.Name = "tsNazad";
            this.tsNazad.Size = new System.Drawing.Size(23, 22);
            this.tsNazad.Text = "toolStripButton1";
            // 
            // tsNapred
            // 
            this.tsNapred.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNapred.Image = ((System.Drawing.Image)(resources.GetObject("tsNapred.Image")));
            this.tsNapred.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNapred.Name = "tsNapred";
            this.tsNapred.Size = new System.Drawing.Size(23, 22);
            this.tsNapred.Text = "toolStripButton1";
            // 
            // tsPoslednja
            // 
            this.tsPoslednja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPoslednja.Image = ((System.Drawing.Image)(resources.GetObject("tsPoslednja.Image")));
            this.tsPoslednja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPoslednja.Name = "tsPoslednja";
            this.tsPoslednja.Size = new System.Drawing.Size(23, 22);
            this.tsPoslednja.Text = "toolStripButton1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(214, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 146;
            this.label6.Text = "Pruga:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(673, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 148;
            this.label1.Text = "Stanica od:";
            // 
            // cboStanicaOd
            // 
            this.cboStanicaOd.FormattingEnabled = true;
            this.cboStanicaOd.Location = new System.Drawing.Point(752, 38);
            this.cboStanicaOd.Name = "cboStanicaOd";
            this.cboStanicaOd.Size = new System.Drawing.Size(156, 21);
            this.cboStanicaOd.TabIndex = 147;
            this.cboStanicaOd.Leave += new System.EventHandler(this.cboStanicaOd_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(671, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 150;
            this.label2.Text = "Stanica Do:";
            // 
            // cboStanicaDo
            // 
            this.cboStanicaDo.FormattingEnabled = true;
            this.cboStanicaDo.Location = new System.Drawing.Point(752, 75);
            this.cboStanicaDo.Name = "cboStanicaDo";
            this.cboStanicaDo.Size = new System.Drawing.Size(156, 21);
            this.cboStanicaDo.TabIndex = 149;
            this.cboStanicaDo.SelectedValueChanged += new System.EventHandler(this.cboStanicaDo_SelectedValueChanged);
            this.cboStanicaDo.Leave += new System.EventHandler(this.cboStanicaDo_Leave);
            // 
            // cmbKlasa
            // 
            this.cmbKlasa.FormattingEnabled = true;
            this.cmbKlasa.Items.AddRange(new object[] {
            "M",
            "R",
            "L"});
            this.cmbKlasa.Location = new System.Drawing.Point(155, 97);
            this.cmbKlasa.Name = "cmbKlasa";
            this.cmbKlasa.Size = new System.Drawing.Size(85, 21);
            this.cmbKlasa.TabIndex = 153;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(116, 100);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(33, 13);
            this.label28.TabIndex = 154;
            this.label28.Text = "Klasa";
            // 
            // chkElektrificirana
            // 
            this.chkElektrificirana.AutoSize = true;
            this.chkElektrificirana.Location = new System.Drawing.Point(11, 100);
            this.chkElektrificirana.Name = "chkElektrificirana";
            this.chkElektrificirana.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkElektrificirana.Size = new System.Drawing.Size(89, 17);
            this.chkElektrificirana.TabIndex = 155;
            this.chkElektrificirana.Text = "Elektrificirana";
            this.chkElektrificirana.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(257, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 157;
            this.label3.Text = "Rastojanje:";
            // 
            // txtRastojanjeKM
            // 
            this.txtRastojanjeKM.DecimalPlaces = 2;
            this.txtRastojanjeKM.Location = new System.Drawing.Point(323, 99);
            this.txtRastojanjeKM.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtRastojanjeKM.Name = "txtRastojanjeKM";
            this.txtRastojanjeKM.Size = new System.Drawing.Size(64, 20);
            this.txtRastojanjeKM.TabIndex = 156;
            this.txtRastojanjeKM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTrase
            // 
            this.txtTrase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtTrase.Location = new System.Drawing.Point(68, 43);
            this.txtTrase.Name = "txtTrase";
            this.txtTrase.Size = new System.Drawing.Size(65, 20);
            this.txtTrase.TabIndex = 160;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 159;
            this.label4.Text = "Šifra:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 124);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(907, 260);
            this.dataGridView1.TabIndex = 161;
            // 
            // txtRN
            // 
            this.txtRN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRN.Location = new System.Drawing.Point(143, 71);
            this.txtRN.Name = "txtRN";
            this.txtRN.Size = new System.Drawing.Size(65, 20);
            this.txtRN.TabIndex = 162;
            // 
            // cboPruge
            // 
            this.cboPruge.AllowFiltering = false;
            this.cboPruge.BackColor = System.Drawing.Color.GreenYellow;
            this.cboPruge.BeforeTouchSize = new System.Drawing.Size(298, 21);
            this.cboPruge.Filter = null;
            this.cboPruge.Location = new System.Drawing.Point(260, 43);
            this.cboPruge.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.cboPruge.Name = "cboPruge";
            this.cboPruge.ScrollMetroColorTable = metroColorTable2;
            this.cboPruge.Size = new System.Drawing.Size(298, 21);
            this.cboPruge.TabIndex = 163;
            this.cboPruge.Leave += new System.EventHandler(this.cboPruge_Leave);
            // 
            // txtRB
            // 
            this.txtRB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtRB.Location = new System.Drawing.Point(68, 71);
            this.txtRB.Name = "txtRB";
            this.txtRB.Size = new System.Drawing.Size(65, 20);
            this.txtRB.TabIndex = 164;
            // 
            // frmPodtrase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 450);
            this.Controls.Add(this.txtRB);
            this.Controls.Add(this.cboPruge);
            this.Controls.Add(this.txtRN);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtTrase);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRastojanjeKM);
            this.Controls.Add(this.chkElektrificirana);
            this.Controls.Add(this.cmbKlasa);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboStanicaDo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboStanicaOd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmPodtrase";
            this.Text = "Podtrase";
            this.Load += new System.EventHandler(this.frmPodtrase_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRastojanjeKM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPruge)).EndInit();
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboStanicaOd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboStanicaDo;
        private System.Windows.Forms.ComboBox cmbKlasa;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.CheckBox chkElektrificirana;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtRastojanjeKM;
        private System.Windows.Forms.TextBox txtTrase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtRN;
        private Syncfusion.Windows.Forms.Tools.MultiColumnComboBox cboPruge;
        private System.Windows.Forms.TextBox txtRB;
    }
}