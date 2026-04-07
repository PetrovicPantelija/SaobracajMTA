namespace Saobracaj.Skladista
{
    partial class MainRN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainRN));
            this.label2 = new System.Windows.Forms.Label();
            this.btnCarinskoSkladiste = new Syncfusion.WinForms.Controls.SfButton();
            this.btnKomerijalnoSkladiste = new Syncfusion.WinForms.Controls.SfButton();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 39);
            this.label2.TabIndex = 500;
            this.label2.Text = "Radni nalozi";
            // 
            // btnCarinskoSkladiste
            // 
            this.btnCarinskoSkladiste.AccessibleName = "Button";
            this.btnCarinskoSkladiste.AllowWrapText = true;
            this.btnCarinskoSkladiste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.btnCarinskoSkladiste.Font = new System.Drawing.Font("Segoe UI Semibold", 18F);
            this.btnCarinskoSkladiste.ForeColor = System.Drawing.Color.White;
            this.btnCarinskoSkladiste.ImageMargin = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.btnCarinskoSkladiste.ImageSize = new System.Drawing.Size(70, 80);
            this.btnCarinskoSkladiste.Location = new System.Drawing.Point(313, 290);
            this.btnCarinskoSkladiste.Name = "btnCarinskoSkladiste";
            this.btnCarinskoSkladiste.Size = new System.Drawing.Size(288, 225);
            this.btnCarinskoSkladiste.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.btnCarinskoSkladiste.Style.ForeColor = System.Drawing.Color.White;
            this.btnCarinskoSkladiste.Style.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.btnCarinskoSkladiste.TabIndex = 501;
            this.btnCarinskoSkladiste.Text = "Carinsko skladište";
            this.btnCarinskoSkladiste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCarinskoSkladiste.UseVisualStyleBackColor = false;
            this.btnCarinskoSkladiste.Click += new System.EventHandler(this.btnCarinskoSkladiste_Click);
            // 
            // btnKomerijalnoSkladiste
            // 
            this.btnKomerijalnoSkladiste.AccessibleName = "Button";
            this.btnKomerijalnoSkladiste.AllowWrapText = true;
            this.btnKomerijalnoSkladiste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.btnKomerijalnoSkladiste.Font = new System.Drawing.Font("Segoe UI Semibold", 18F);
            this.btnKomerijalnoSkladiste.ForeColor = System.Drawing.Color.White;
            this.btnKomerijalnoSkladiste.ImageMargin = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.btnKomerijalnoSkladiste.ImageSize = new System.Drawing.Size(70, 80);
            this.btnKomerijalnoSkladiste.Location = new System.Drawing.Point(700, 290);
            this.btnKomerijalnoSkladiste.Name = "btnKomerijalnoSkladiste";
            this.btnKomerijalnoSkladiste.Size = new System.Drawing.Size(288, 225);
            this.btnKomerijalnoSkladiste.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.btnKomerijalnoSkladiste.Style.ForeColor = System.Drawing.Color.White;
            this.btnKomerijalnoSkladiste.Style.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.btnKomerijalnoSkladiste.TabIndex = 501;
            this.btnKomerijalnoSkladiste.Text = "Komercijalno skladište";
            this.btnKomerijalnoSkladiste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnKomerijalnoSkladiste.UseVisualStyleBackColor = false;
            this.btnKomerijalnoSkladiste.Click += new System.EventHandler(this.btnKomerijalnoSkladiste_Click);
            // 
            // MainRN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1016, 578);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCarinskoSkladiste);
            this.Controls.Add(this.btnKomerijalnoSkladiste);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainRN";
            this.Text = "MainRN";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private Syncfusion.WinForms.Controls.SfButton btnCarinskoSkladiste;
        private Syncfusion.WinForms.Controls.SfButton btnKomerijalnoSkladiste;
    }
}