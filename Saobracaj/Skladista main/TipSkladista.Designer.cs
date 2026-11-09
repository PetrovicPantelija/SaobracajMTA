namespace Saobracaj.Skladista_main
{
    partial class TipSkladista
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TipSkladista));
            this.btnKomercijalnoSkladiste = new Syncfusion.WinForms.Controls.SfButton();
            this.btnCarinskoSkladiste = new Syncfusion.WinForms.Controls.SfButton();
            this.SuspendLayout();
            // 
            // btnKomercijalnoSkladiste
            // 
            this.btnKomercijalnoSkladiste.AccessibleName = "Button";
            this.btnKomercijalnoSkladiste.AllowWrapText = true;
            this.btnKomercijalnoSkladiste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.btnKomercijalnoSkladiste.Font = new System.Drawing.Font("Segoe UI Semibold", 18F);
            this.btnKomercijalnoSkladiste.ForeColor = System.Drawing.Color.White;
            this.btnKomercijalnoSkladiste.ImageMargin = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.btnKomercijalnoSkladiste.ImageSize = new System.Drawing.Size(70, 80);
            this.btnKomercijalnoSkladiste.Location = new System.Drawing.Point(609, 317);
            this.btnKomercijalnoSkladiste.Name = "btnKomercijalnoSkladiste";
            this.btnKomercijalnoSkladiste.Size = new System.Drawing.Size(288, 225);
            this.btnKomercijalnoSkladiste.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.btnKomercijalnoSkladiste.Style.ForeColor = System.Drawing.Color.White;
            this.btnKomercijalnoSkladiste.Style.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.btnKomercijalnoSkladiste.TabIndex = 503;
            this.btnKomercijalnoSkladiste.Text = "Komercijalno skladište";
            this.btnKomercijalnoSkladiste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnKomercijalnoSkladiste.UseVisualStyleBackColor = false;
            this.btnKomercijalnoSkladiste.Click += new System.EventHandler(this.btnKomercijalnoSkladiste_Click);
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
            this.btnCarinskoSkladiste.Location = new System.Drawing.Point(257, 317);
            this.btnCarinskoSkladiste.Name = "btnCarinskoSkladiste";
            this.btnCarinskoSkladiste.Size = new System.Drawing.Size(288, 225);
            this.btnCarinskoSkladiste.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.btnCarinskoSkladiste.Style.ForeColor = System.Drawing.Color.White;
            this.btnCarinskoSkladiste.Style.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.btnCarinskoSkladiste.TabIndex = 504;
            this.btnCarinskoSkladiste.Text = "Carinsko skladište";
            this.btnCarinskoSkladiste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCarinskoSkladiste.UseVisualStyleBackColor = false;
            this.btnCarinskoSkladiste.Click += new System.EventHandler(this.btnCarinskoSkladiste_Click);
            // 
            // TipSkladista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(909, 554);
            this.Controls.Add(this.btnKomercijalnoSkladiste);
            this.Controls.Add(this.btnCarinskoSkladiste);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TipSkladista";
            this.Text = "TipSkladista";
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton btnKomercijalnoSkladiste;
        private Syncfusion.WinForms.Controls.SfButton btnCarinskoSkladiste;
    }
}