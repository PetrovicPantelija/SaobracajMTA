namespace Saobracaj.Drumski
{
    partial class frmPregledDokumenataKamiona
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPregledDokumenataKamiona));
            this.lblOdabir = new System.Windows.Forms.Label();
            this.btnOdaberi = new System.Windows.Forms.Button();
            this.panelDrop = new System.Windows.Forms.Panel();
            this.lblDrop = new System.Windows.Forms.Label();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.lstFajlovi = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblPregledDokumenata = new System.Windows.Forms.Label();
            this.panelDrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOdabir
            // 
            this.lblOdabir.AutoSize = true;
            this.lblOdabir.Location = new System.Drawing.Point(22, 57);
            this.lblOdabir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOdabir.Name = "lblOdabir";
            this.lblOdabir.Size = new System.Drawing.Size(214, 16);
            this.lblOdabir.TabIndex = 0;
            this.lblOdabir.Text = "Odabir dokumenata za dodavanje:";
            // 
            // btnOdaberi
            // 
            this.btnOdaberi.Location = new System.Drawing.Point(14, 12);
            this.btnOdaberi.Margin = new System.Windows.Forms.Padding(4);
            this.btnOdaberi.Name = "btnOdaberi";
            this.btnOdaberi.Size = new System.Drawing.Size(244, 31);
            this.btnOdaberi.TabIndex = 1;
            this.btnOdaberi.Text = "Odaberi jedan ili više dokumenata";
            this.btnOdaberi.UseVisualStyleBackColor = true;
            this.btnOdaberi.Click += new System.EventHandler(this.btnOdaberi_Click);
            // 
            // panelDrop
            // 
            this.panelDrop.AllowDrop = true;
            this.panelDrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDrop.Controls.Add(this.lblDrop);
            this.panelDrop.Controls.Add(this.btnOdaberi);
            this.panelDrop.Location = new System.Drawing.Point(25, 91);
            this.panelDrop.Margin = new System.Windows.Forms.Padding(4);
            this.panelDrop.Name = "panelDrop";
            this.panelDrop.Size = new System.Drawing.Size(514, 57);
            this.panelDrop.TabIndex = 2;
            this.panelDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelDrop_DragDrop);
            this.panelDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.panelDrop_DragEnter);
            // 
            // lblDrop
            // 
            this.lblDrop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDrop.AutoSize = true;
            this.lblDrop.Location = new System.Drawing.Point(343, 19);
            this.lblDrop.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDrop.Name = "lblDrop";
            this.lblDrop.Size = new System.Drawing.Size(146, 16);
            this.lblDrop.TabIndex = 0;
            this.lblDrop.Text = "Ispustite datoteke ovde";
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // lstFajlovi
            // 
            this.lstFajlovi.FormattingEnabled = true;
            this.lstFajlovi.ItemHeight = 16;
            this.lstFajlovi.Location = new System.Drawing.Point(25, 172);
            this.lstFajlovi.Name = "lstFajlovi";
            this.lstFajlovi.Size = new System.Drawing.Size(326, 132);
            this.lstFajlovi.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(384, 184);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 31);
            this.button1.TabIndex = 5;
            this.button1.Text = "Obriši";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // panel3
            // 
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.button2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(98, 38);
            this.panel3.TabIndex = 3;
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
            this.button2.Location = new System.Drawing.Point(10, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 33);
            this.button2.TabIndex = 16;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.panel3);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(867, 40);
            this.panelHeader.TabIndex = 481;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Location = new System.Drawing.Point(25, 364);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(823, 249);
            this.panel2.TabIndex = 485;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(823, 249);
            this.dataGridView1.TabIndex = 2;
            // 
            // lblPregledDokumenata
            // 
            this.lblPregledDokumenata.AutoSize = true;
            this.lblPregledDokumenata.Location = new System.Drawing.Point(22, 332);
            this.lblPregledDokumenata.Name = "lblPregledDokumenata";
            this.lblPregledDokumenata.Size = new System.Drawing.Size(223, 16);
            this.lblPregledDokumenata.TabIndex = 486;
            this.lblPregledDokumenata.Text = "Pregled već postojećih dokumenata";
            // 
            // frmPregledDokumenataKamiona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 625);
            this.Controls.Add(this.lblPregledDokumenata);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lstFajlovi);
            this.Controls.Add(this.panelDrop);
            this.Controls.Add(this.lblOdabir);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmPregledDokumenataKamiona";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dodavanje dokumenata";
            this.Load += new System.EventHandler(this.frmPregledDokumenataKamiona_Load);
            this.panelDrop.ResumeLayout(false);
            this.panelDrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblOdabir;
        private System.Windows.Forms.Button btnOdaberi;
        private System.Windows.Forms.Panel panelDrop;
        private System.Windows.Forms.Label lblDrop;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lstFajlovi;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblPregledDokumenata;
    }
}