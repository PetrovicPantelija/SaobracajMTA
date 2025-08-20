namespace Saobracaj.Drumski
{
    partial class DodavanjeSkeniraneFakture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DodavanjeSkeniraneFakture));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.txtNazivDodatihFajlova = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtNaslov = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBeleske = new System.Windows.Forms.TextBox();
            this.lblBeleške = new System.Windows.Forms.Label();
            this.lblUlaznaFaktura = new System.Windows.Forms.Label();
            this.lblIzlaznaFaktura = new System.Windows.Forms.Label();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.txtPrevoznik = new System.Windows.Forms.Label();
            this.lblUlaznaF = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(981, 40);
            this.panelHeader.TabIndex = 480;
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
            // txtNazivDodatihFajlova
            // 
            this.txtNazivDodatihFajlova.Location = new System.Drawing.Point(489, 254);
            this.txtNazivDodatihFajlova.Multiline = true;
            this.txtNazivDodatihFajlova.Name = "txtNazivDodatihFajlova";
            this.txtNazivDodatihFajlova.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNazivDodatihFajlova.Size = new System.Drawing.Size(308, 70);
            this.txtNazivDodatihFajlova.TabIndex = 498;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 16);
            this.label2.TabIndex = 497;
            this.label2.Text = "Skenirane fakture:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(275, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 38);
            this.button1.TabIndex = 496;
            this.button1.Text = "Odabir dokumenata";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtNaslov
            // 
            this.txtNaslov.Location = new System.Drawing.Point(149, 114);
            this.txtNaslov.Name = "txtNaslov";
            this.txtNaslov.Size = new System.Drawing.Size(308, 22);
            this.txtNaslov.TabIndex = 495;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(486, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 16);
            this.label1.TabIndex = 494;
            this.label1.Text = "Broj ulazne fakture";
            // 
            // txtBeleske
            // 
            this.txtBeleske.Location = new System.Drawing.Point(149, 162);
            this.txtBeleske.Multiline = true;
            this.txtBeleske.Name = "txtBeleske";
            this.txtBeleske.Size = new System.Drawing.Size(308, 64);
            this.txtBeleske.TabIndex = 488;
            // 
            // lblBeleške
            // 
            this.lblBeleške.AutoSize = true;
            this.lblBeleške.Location = new System.Drawing.Point(40, 162);
            this.lblBeleške.Name = "lblBeleške";
            this.lblBeleške.Size = new System.Drawing.Size(78, 16);
            this.lblBeleške.TabIndex = 489;
            this.lblBeleške.Text = "Napomena:";
            // 
            // lblUlaznaFaktura
            // 
            this.lblUlaznaFaktura.AutoSize = true;
            this.lblUlaznaFaktura.Location = new System.Drawing.Point(42, 120);
            this.lblUlaznaFaktura.Name = "lblUlaznaFaktura";
            this.lblUlaznaFaktura.Size = new System.Drawing.Size(53, 16);
            this.lblUlaznaFaktura.TabIndex = 491;
            this.lblUlaznaFaktura.Text = "Naslov:";
            // 
            // lblIzlaznaFaktura
            // 
            this.lblIzlaznaFaktura.AutoSize = true;
            this.lblIzlaznaFaktura.Location = new System.Drawing.Point(40, 69);
            this.lblIzlaznaFaktura.Name = "lblIzlaznaFaktura";
            this.lblIzlaznaFaktura.Size = new System.Drawing.Size(66, 16);
            this.lblIzlaznaFaktura.TabIndex = 493;
            this.lblIzlaznaFaktura.Text = "Prevoznik";
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // txtPrevoznik
            // 
            this.txtPrevoznik.AutoSize = true;
            this.txtPrevoznik.Location = new System.Drawing.Point(149, 67);
            this.txtPrevoznik.Name = "txtPrevoznik";
            this.txtPrevoznik.Size = new System.Drawing.Size(0, 16);
            this.txtPrevoznik.TabIndex = 499;
            // 
            // lblUlaznaF
            // 
            this.lblUlaznaF.AutoSize = true;
            this.lblUlaznaF.Location = new System.Drawing.Point(635, 68);
            this.lblUlaznaF.Name = "lblUlaznaF";
            this.lblUlaznaF.Size = new System.Drawing.Size(0, 16);
            this.lblUlaznaF.TabIndex = 500;
            // 
            // DodavanjeSkeniraneFakture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 346);
            this.Controls.Add(this.lblUlaznaF);
            this.Controls.Add(this.txtPrevoznik);
            this.Controls.Add(this.txtNazivDodatihFajlova);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtNaslov);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBeleske);
            this.Controls.Add(this.lblBeleške);
            this.Controls.Add(this.lblUlaznaFaktura);
            this.Controls.Add(this.lblIzlaznaFaktura);
            this.Controls.Add(this.panelHeader);
            this.Name = "DodavanjeSkeniraneFakture";
            this.Text = "DodavanjeSkeniraneFakture";
            this.panelHeader.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtNazivDodatihFajlova;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtNaslov;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBeleske;
        private System.Windows.Forms.Label lblBeleške;
        private System.Windows.Forms.Label lblUlaznaFaktura;
        private System.Windows.Forms.Label lblIzlaznaFaktura;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Label txtPrevoznik;
        private System.Windows.Forms.Label lblUlaznaF;
    }
}