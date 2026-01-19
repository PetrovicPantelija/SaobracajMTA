namespace Saobracaj.Drumski
{
    partial class frmAutomobilEdit
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAutomobilEdit));
            this.label1 = new System.Windows.Forms.Label();
            this.cboPrevoznik = new System.Windows.Forms.ComboBox();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.txtZaposleniID = new System.Windows.Forms.TextBox();
            this.txtZaposleni = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.cboTipVozila = new System.Windows.Forms.ComboBox();
            this.txtVozacTelefon = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.txtVozac = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtRegBr = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.txtLKVozaca = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.meniHeader = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrvi = new System.Windows.Forms.ToolStripButton();
            this.tsNazad = new System.Windows.Forms.ToolStripButton();
            this.tsNapred = new System.Windows.Forms.ToolStripButton();
            this.tsPoslednja = new System.Windows.Forms.ToolStripButton();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.btnDodavanjePartnera = new System.Windows.Forms.Button();
            this.lblNaslov = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.meniHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(851, 92);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 729;
            this.label1.Text = "Prevoznik:";
            // 
            // cboPrevoznik
            // 
            this.cboPrevoznik.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPrevoznik.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPrevoznik.FormattingEnabled = true;
            this.cboPrevoznik.Location = new System.Drawing.Point(850, 112);
            this.cboPrevoznik.Margin = new System.Windows.Forms.Padding(4);
            this.cboPrevoznik.Name = "cboPrevoznik";
            this.cboPrevoznik.Size = new System.Drawing.Size(308, 24);
            this.cboPrevoznik.TabIndex = 728;
            // 
            // txtSifra
            // 
            this.txtSifra.BackColor = System.Drawing.Color.PeachPuff;
            this.txtSifra.Enabled = false;
            this.txtSifra.Location = new System.Drawing.Point(28, 112);
            this.txtSifra.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(113, 22);
            this.txtSifra.TabIndex = 727;
            // 
            // txtZaposleniID
            // 
            this.txtZaposleniID.Location = new System.Drawing.Point(747, 112);
            this.txtZaposleniID.Margin = new System.Windows.Forms.Padding(4);
            this.txtZaposleniID.Name = "txtZaposleniID";
            this.txtZaposleniID.Size = new System.Drawing.Size(86, 22);
            this.txtZaposleniID.TabIndex = 726;
            this.txtZaposleniID.Visible = false;
            // 
            // txtZaposleni
            // 
            this.txtZaposleni.Location = new System.Drawing.Point(431, 112);
            this.txtZaposleni.Margin = new System.Windows.Forms.Padding(4);
            this.txtZaposleni.Name = "txtZaposleni";
            this.txtZaposleni.Size = new System.Drawing.Size(308, 22);
            this.txtZaposleni.TabIndex = 725;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(432, 149);
            this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(68, 16);
            this.label40.TabIndex = 724;
            this.label40.Text = "Tip vozila:";
            // 
            // cboTipVozila
            // 
            this.cboTipVozila.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTipVozila.FormattingEnabled = true;
            this.cboTipVozila.Location = new System.Drawing.Point(431, 170);
            this.cboTipVozila.Margin = new System.Windows.Forms.Padding(4);
            this.cboTipVozila.Name = "cboTipVozila";
            this.cboTipVozila.Size = new System.Drawing.Size(308, 24);
            this.cboTipVozila.TabIndex = 723;
            // 
            // txtVozacTelefon
            // 
            this.txtVozacTelefon.Location = new System.Drawing.Point(850, 230);
            this.txtVozacTelefon.Margin = new System.Windows.Forms.Padding(4);
            this.txtVozacTelefon.Name = "txtVozacTelefon";
            this.txtVozacTelefon.Size = new System.Drawing.Size(308, 22);
            this.txtVozacTelefon.TabIndex = 721;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(851, 210);
            this.label41.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(129, 16);
            this.label41.TabIndex = 722;
            this.label41.Text = "Broj telefona vozača";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(432, 210);
            this.label43.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(122, 16);
            this.label43.TabIndex = 719;
            this.label43.Text = "Lična karta vozača:";
            // 
            // txtVozac
            // 
            this.txtVozac.Location = new System.Drawing.Point(28, 230);
            this.txtVozac.Margin = new System.Windows.Forms.Padding(4);
            this.txtVozac.Name = "txtVozac";
            this.txtVozac.Size = new System.Drawing.Size(308, 22);
            this.txtVozac.TabIndex = 720;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(29, 151);
            this.label44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(104, 16);
            this.label44.TabIndex = 717;
            this.label44.Text = "Registarski broj:";
            // 
            // txtRegBr
            // 
            this.txtRegBr.Location = new System.Drawing.Point(28, 171);
            this.txtRegBr.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegBr.Name = "txtRegBr";
            this.txtRegBr.Size = new System.Drawing.Size(308, 22);
            this.txtRegBr.TabIndex = 718;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(29, 210);
            this.label45.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(48, 16);
            this.label45.TabIndex = 715;
            this.label45.Text = "Vozač:";
            // 
            // txtLKVozaca
            // 
            this.txtLKVozaca.Location = new System.Drawing.Point(431, 230);
            this.txtLKVozaca.Margin = new System.Windows.Forms.Padding(4);
            this.txtLKVozaca.Name = "txtLKVozaca";
            this.txtLKVozaca.Size = new System.Drawing.Size(308, 22);
            this.txtLKVozaca.TabIndex = 716;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(432, 92);
            this.label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(70, 16);
            this.label46.TabIndex = 714;
            this.label46.Text = "Zaposleni:";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(29, 92);
            this.label47.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(80, 16);
            this.label47.TabIndex = 713;
            this.label47.Text = "Šifra zapisa:";
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblNaslov);
            this.panel1.Controls.Add(this.btnDodavanjePartnera);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1185, 40);
            this.panel1.TabIndex = 731;
            // 
            // panel5
            // 
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.button2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(119, 38);
            this.panel5.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button1.Location = new System.Drawing.Point(59, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 33);
            this.button1.TabIndex = 15;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(15, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 33);
            this.button2.TabIndex = 14;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // meniHeader
            // 
            this.meniHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.meniHeader.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.meniHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1,
            this.tsPrvi,
            this.tsNazad,
            this.tsNapred,
            this.tsPoslednja});
            this.meniHeader.Location = new System.Drawing.Point(0, 0);
            this.meniHeader.Name = "meniHeader";
            this.meniHeader.Size = new System.Drawing.Size(1185, 27);
            this.meniHeader.TabIndex = 730;
            this.meniHeader.Text = "Osveži";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(29, 24);
            this.tsNew.Text = "Novi";
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(29, 24);
            this.tsSave.Text = "tsSave";
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(29, 24);
            this.tsDelete.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsPrvi
            // 
            this.tsPrvi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPrvi.Image = ((System.Drawing.Image)(resources.GetObject("tsPrvi.Image")));
            this.tsPrvi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrvi.Name = "tsPrvi";
            this.tsPrvi.Size = new System.Drawing.Size(29, 24);
            this.tsPrvi.Text = "toolStripButton1";
            // 
            // tsNazad
            // 
            this.tsNazad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNazad.Image = ((System.Drawing.Image)(resources.GetObject("tsNazad.Image")));
            this.tsNazad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNazad.Name = "tsNazad";
            this.tsNazad.Size = new System.Drawing.Size(29, 24);
            this.tsNazad.Text = "toolStripButton1";
            // 
            // tsNapred
            // 
            this.tsNapred.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNapred.Image = ((System.Drawing.Image)(resources.GetObject("tsNapred.Image")));
            this.tsNapred.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNapred.Name = "tsNapred";
            this.tsNapred.Size = new System.Drawing.Size(29, 24);
            this.tsNapred.Text = "toolStripButton1";
            // 
            // tsPoslednja
            // 
            this.tsPoslednja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPoslednja.Image = ((System.Drawing.Image)(resources.GetObject("tsPoslednja.Image")));
            this.tsPoslednja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPoslednja.Name = "tsPoslednja";
            this.tsPoslednja.Size = new System.Drawing.Size(29, 24);
            this.tsPoslednja.Text = "toolStripButton1";
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // btnDodavanjePartnera
            // 
            this.btnDodavanjePartnera.AutoSize = true;
            this.btnDodavanjePartnera.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodavanjePartnera.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDodavanjePartnera.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnDodavanjePartnera.FlatAppearance.BorderSize = 0;
            this.btnDodavanjePartnera.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnDodavanjePartnera.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnDodavanjePartnera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodavanjePartnera.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnDodavanjePartnera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnDodavanjePartnera.Location = new System.Drawing.Point(119, 0);
            this.btnDodavanjePartnera.Margin = new System.Windows.Forms.Padding(4);
            this.btnDodavanjePartnera.Name = "btnDodavanjePartnera";
            this.btnDodavanjePartnera.Size = new System.Drawing.Size(365, 38);
            this.btnDodavanjePartnera.TabIndex = 23;
            this.btnDodavanjePartnera.Text = "Novi prevoznik/kamioner";
            this.btnDodavanjePartnera.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDodavanjePartnera.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDodavanjePartnera.UseVisualStyleBackColor = true;
            this.btnDodavanjePartnera.Click += new System.EventHandler(this.btnDodavanjePartnera_Click);
            // 
            // lblNaslov
            // 
            this.lblNaslov.AutoSize = true;
            this.lblNaslov.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblNaslov.Location = new System.Drawing.Point(484, 0);
            this.lblNaslov.Name = "lblNaslov";
            this.lblNaslov.Padding = new System.Windows.Forms.Padding(0, 11, 0, 0);
            this.lblNaslov.Size = new System.Drawing.Size(37, 27);
            this.lblNaslov.TabIndex = 24;
            this.lblNaslov.Text = "label";
            this.lblNaslov.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmAutomobilEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 284);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.meniHeader);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboPrevoznik);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.txtZaposleniID);
            this.Controls.Add(this.txtZaposleni);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.cboTipVozila);
            this.Controls.Add(this.txtVozacTelefon);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.txtVozac);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.txtRegBr);
            this.Controls.Add(this.label45);
            this.Controls.Add(this.txtLKVozaca);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.label47);
            this.Name = "frmAutomobilEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Automobil detalji";
            this.Load += new System.EventHandler(this.frmAutomobilEdit_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.meniHeader.ResumeLayout(false);
            this.meniHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPrevoznik;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.TextBox txtZaposleniID;
        private System.Windows.Forms.TextBox txtZaposleni;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ComboBox cboTipVozila;
        private System.Windows.Forms.TextBox txtVozacTelefon;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox txtVozac;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtRegBr;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox txtLKVozaca;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStrip meniHeader;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsPrvi;
        private System.Windows.Forms.ToolStripButton tsNazad;
        private System.Windows.Forms.ToolStripButton tsNapred;
        private System.Windows.Forms.ToolStripButton tsPoslednja;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Label lblNaslov;
        private System.Windows.Forms.Button btnDodavanjePartnera;
    }
}