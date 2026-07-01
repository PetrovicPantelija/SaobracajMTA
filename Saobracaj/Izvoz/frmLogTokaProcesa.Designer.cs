namespace Saobracaj.Izvoz
{
    partial class frmLogTokaProcesa
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
            this.panelKontejner = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelKontejner
            // 
            this.panelKontejner.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelKontejner.AutoScroll = true;
            this.panelKontejner.Location = new System.Drawing.Point(12, 12);
            this.panelKontejner.Name = "panelKontejner";
            this.panelKontejner.Size = new System.Drawing.Size(1015, 581);
            this.panelKontejner.TabIndex = 2;
            // 
            // frmLogTokaProcesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 609);
            this.Controls.Add(this.panelKontejner);
            this.Name = "frmLogTokaProcesa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmLogTokaProcesa";
            this.Load += new System.EventHandler(this.frmLogTokaProcesa_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelKontejner;
    }
}