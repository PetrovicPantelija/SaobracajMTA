namespace Saobracaj.Drumski
{
    partial class frmFaktureDetalji
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFaktureDetalji));
            this.lblIzlaznaFaktura = new System.Windows.Forms.Label();
            this.txtIzlaznaFaktura = new System.Windows.Forms.TextBox();
            this.lblDatum = new System.Windows.Forms.Label();
            this.dtpDatumIzlazneFakture = new System.Windows.Forms.DateTimePicker();
            this.lblUlaznaFaktura = new System.Windows.Forms.Label();
            this.txtUlaznaFaktura = new System.Windows.Forms.TextBox();
            this.lblVoz = new System.Windows.Forms.Label();
            this.lblBeleške = new System.Windows.Forms.Label();
            this.txtBeleske = new System.Windows.Forms.TextBox();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.panelHeader = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button21 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtVoz = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIzlaznaFaktura
            // 
            this.lblIzlaznaFaktura.AutoSize = true;
            this.lblIzlaznaFaktura.Location = new System.Drawing.Point(33, 74);
            this.lblIzlaznaFaktura.Name = "lblIzlaznaFaktura";
            this.lblIzlaznaFaktura.Size = new System.Drawing.Size(121, 16);
            this.lblIzlaznaFaktura.TabIndex = 12;
            this.lblIzlaznaFaktura.Text = "Broj izlazne fakture:";
            // 
            // txtIzlaznaFaktura
            // 
            this.txtIzlaznaFaktura.Location = new System.Drawing.Point(212, 72);
            this.txtIzlaznaFaktura.Name = "txtIzlaznaFaktura";
            this.txtIzlaznaFaktura.Size = new System.Drawing.Size(220, 22);
            this.txtIzlaznaFaktura.TabIndex = 11;
            // 
            // lblDatum
            // 
            this.lblDatum.AutoSize = true;
            this.lblDatum.Location = new System.Drawing.Point(491, 73);
            this.lblDatum.Name = "lblDatum";
            this.lblDatum.Size = new System.Drawing.Size(175, 16);
            this.lblDatum.TabIndex = 10;
            this.lblDatum.Text = "Datum slanja izlazne fakture:";
            // 
            // dtpDatumIzlazneFakture
            // 
            this.dtpDatumIzlazneFakture.CustomFormat = "dd.MM.yyyy.";
            this.dtpDatumIzlazneFakture.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumIzlazneFakture.Location = new System.Drawing.Point(720, 68);
            this.dtpDatumIzlazneFakture.Name = "dtpDatumIzlazneFakture";
            this.dtpDatumIzlazneFakture.Size = new System.Drawing.Size(135, 22);
            this.dtpDatumIzlazneFakture.TabIndex = 9;
            // 
            // lblUlaznaFaktura
            // 
            this.lblUlaznaFaktura.AutoSize = true;
            this.lblUlaznaFaktura.Location = new System.Drawing.Point(35, 131);
            this.lblUlaznaFaktura.Name = "lblUlaznaFaktura";
            this.lblUlaznaFaktura.Size = new System.Drawing.Size(119, 16);
            this.lblUlaznaFaktura.TabIndex = 8;
            this.lblUlaznaFaktura.Text = "Broj ulazne fakture:";
            // 
            // txtUlaznaFaktura
            // 
            this.txtUlaznaFaktura.Location = new System.Drawing.Point(212, 128);
            this.txtUlaznaFaktura.Name = "txtUlaznaFaktura";
            this.txtUlaznaFaktura.Size = new System.Drawing.Size(220, 22);
            this.txtUlaznaFaktura.TabIndex = 7;
            // 
            // lblVoz
            // 
            this.lblVoz.AutoSize = true;
            this.lblVoz.Location = new System.Drawing.Point(491, 131);
            this.lblVoz.Name = "lblVoz";
            this.lblVoz.Size = new System.Drawing.Size(33, 16);
            this.lblVoz.TabIndex = 6;
            this.lblVoz.Text = "Voz:";
            // 
            // lblBeleške
            // 
            this.lblBeleške.AutoSize = true;
            this.lblBeleške.Location = new System.Drawing.Point(35, 211);
            this.lblBeleške.Name = "lblBeleške";
            this.lblBeleške.Size = new System.Drawing.Size(145, 16);
            this.lblBeleške.TabIndex = 4;
            this.lblBeleške.Text = "Beleške ulazne fakture:";
            // 
            // txtBeleske
            // 
            this.txtBeleske.Location = new System.Drawing.Point(212, 198);
            this.txtBeleske.Multiline = true;
            this.txtBeleske.Name = "txtBeleske";
            this.txtBeleske.Size = new System.Drawing.Size(372, 98);
            this.txtBeleske.TabIndex = 3;
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // panelHeader
            // 
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.button4);
            this.panelHeader.Controls.Add(this.button3);
            this.panelHeader.Controls.Add(this.button1);
            this.panelHeader.Controls.Add(this.panel3);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(899, 40);
            this.panelHeader.TabIndex = 478;
            // 
            // button4
            // 
            this.button4.AutoSize = true;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Dock = System.Windows.Forms.DockStyle.Left;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button4.Location = new System.Drawing.Point(516, 0);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(222, 38);
            this.button4.TabIndex = 22;
            this.button4.Text = "Pregled skeniranih dokumenata";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Dock = System.Windows.Forms.DockStyle.Left;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button3.Location = new System.Drawing.Point(318, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(198, 38);
            this.button3.TabIndex = 21;
            this.button3.Text = "Dodaj skeniranu fakturu";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button1.Location = new System.Drawing.Point(120, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(198, 38);
            this.button1.TabIndex = 20;
            this.button1.Text = "Dodaj skeniranu prevoznicu";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.button21);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(120, 38);
            this.panel3.TabIndex = 3;
            // 
            // button21
            // 
            this.button21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button21.BackgroundImage")));
            this.button21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button21.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button21.FlatAppearance.BorderSize = 0;
            this.button21.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button21.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button21.Location = new System.Drawing.Point(49, 4);
            this.button21.Margin = new System.Windows.Forms.Padding(4);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(36, 33);
            this.button21.TabIndex = 19;
            this.button21.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Visible = false;
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
            this.button2.Click += new System.EventHandler(this.btnSnimi_Click);
            // 
            // txtVoz
            // 
            this.txtVoz.AutoSize = true;
            this.txtVoz.Location = new System.Drawing.Point(720, 125);
            this.txtVoz.Name = "txtVoz";
            this.txtVoz.Size = new System.Drawing.Size(0, 16);
            this.txtVoz.TabIndex = 479;
            // 
            // frmFaktureDetalji
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 322);
            this.Controls.Add(this.txtVoz);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.txtBeleske);
            this.Controls.Add(this.lblBeleške);
            this.Controls.Add(this.lblVoz);
            this.Controls.Add(this.txtUlaznaFaktura);
            this.Controls.Add(this.lblUlaznaFaktura);
            this.Controls.Add(this.dtpDatumIzlazneFakture);
            this.Controls.Add(this.lblDatum);
            this.Controls.Add(this.txtIzlaznaFaktura);
            this.Controls.Add(this.lblIzlaznaFaktura);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmFaktureDetalji";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pregled detalja fakture";
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIzlaznaFaktura;
        private System.Windows.Forms.TextBox txtIzlaznaFaktura;
        private System.Windows.Forms.Label lblDatum;
        private System.Windows.Forms.DateTimePicker dtpDatumIzlazneFakture;
        private System.Windows.Forms.Label lblUlaznaFaktura;
        private System.Windows.Forms.TextBox txtUlaznaFaktura;
        private System.Windows.Forms.Label lblVoz;
        private System.Windows.Forms.Label lblBeleške;
        private System.Windows.Forms.TextBox txtBeleske;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label txtVoz;
        private System.Windows.Forms.Button button4;
    }
}