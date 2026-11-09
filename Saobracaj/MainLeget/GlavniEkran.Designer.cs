namespace Saobracaj.MainLeget
{
    partial class GlavniEkran
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
            this.chartBrodari = new Syncfusion.Windows.Forms.Chart.ChartControl();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.panel11.SuspendLayout();
            this.SuspendLayout();
            //
            // chartBrodari
            //
            this.chartBrodari.BackInterior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.White);
            this.chartBrodari.ChartArea.BackInterior = new Syncfusion.Drawing.BrushInfo(System.Drawing.Color.White);
            this.chartBrodari.Location = new System.Drawing.Point(12, 12);
            this.chartBrodari.Name = "chartBrodari";
            this.chartBrodari.Size = new System.Drawing.Size(460, 360);
            this.chartBrodari.TabIndex = 0;
            this.chartBrodari.Text = "Kontejneri na terminalu po brodaru";
            this.chartBrodari.Title.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.chartBrodari.Titles.Add(this.chartBrodari.Title);
            //
            // panel11
            //
            this.panel11.Controls.Add(this.label28);
            this.panel11.Controls.Add(this.label29);
            this.panel11.Controls.Add(this.label30);
            this.panel11.Location = new System.Drawing.Point(51, 680);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(905, 46);
            this.panel11.TabIndex = 20;
            //
            // label28
            //
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(704, 11);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(161, 25);
            this.label28.TabIndex = 2;
            this.label28.Text = "Kreiraj novi nalog";
            //
            // label29
            //
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(369, 11);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(125, 25);
            this.label29.TabIndex = 1;
            this.label29.Text = "Mrežni status";
            //
            // label30
            //
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(16, 11);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(187, 25);
            this.label30.TabIndex = 0;
            this.label30.Text = "U toku pretovar voza";
            //
            // GlavniEkran
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1021, 751);
            this.Controls.Add(this.chartBrodari);
            this.Controls.Add(this.panel11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GlavniEkran";
            this.Text = "GlavniEkran";
            this.Load += new System.EventHandler(this.GlavniEkran_Load);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Chart.ChartControl chartBrodari;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
    }
}
