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
            // GlavniEkran
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1021, 751);
            this.Controls.Add(this.chartBrodari);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GlavniEkran";
            this.Text = "GlavniEkran";
            this.Load += new System.EventHandler(this.GlavniEkran_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Chart.ChartControl chartBrodari;
    }
}
