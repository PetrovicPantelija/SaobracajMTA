namespace Saobracaj.Drumski
{
    partial class frmBrojFaktureModal
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
            this.txtBrojFakture = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBrojFakture
            // 
            this.txtBrojFakture.Location = new System.Drawing.Point(54, 64);
            this.txtBrojFakture.Name = "txtBrojFakture";
            this.txtBrojFakture.Size = new System.Drawing.Size(241, 22);
            this.txtBrojFakture.TabIndex = 8;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(184, 105);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(111, 30);
            this.button5.TabIndex = 7;
            this.button5.Text = "OK";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(51, 33);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(118, 16);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Broj izlazne fakture";
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // frmBrojFaktureModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 163);
            this.Controls.Add(this.txtBrojFakture);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.lblStatus);
            this.Name = "frmBrojFaktureModal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Unosna forma";
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBrojFakture;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label lblStatus;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
    }
}