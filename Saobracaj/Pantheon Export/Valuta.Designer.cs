namespace Saobracaj.Pantheon_Export
{
    partial class Valuta
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnRSD = new System.Windows.Forms.Button();
            this.btnValuta = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.DarkGray;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(1, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(297, 32);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = " Izaberite valutu za sinhronizaciju fakture ";
            // 
            // btnRSD
            // 
            this.btnRSD.Location = new System.Drawing.Point(49, 50);
            this.btnRSD.Name = "btnRSD";
            this.btnRSD.Size = new System.Drawing.Size(93, 37);
            this.btnRSD.TabIndex = 1;
            this.btnRSD.Text = "RSD";
            this.btnRSD.UseVisualStyleBackColor = true;
            this.btnRSD.Click += new System.EventHandler(this.btnRSD_Click);
            // 
            // btnValuta
            // 
            this.btnValuta.Location = new System.Drawing.Point(168, 50);
            this.btnValuta.Name = "btnValuta";
            this.btnValuta.Size = new System.Drawing.Size(93, 37);
            this.btnValuta.TabIndex = 1;
            this.btnValuta.Text = "button1";
            this.btnValuta.UseVisualStyleBackColor = true;
            this.btnValuta.Click += new System.EventHandler(this.btnValuta_Click);
            // 
            // Valuta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(299, 101);
            this.ControlBox = false;
            this.Controls.Add(this.btnValuta);
            this.Controls.Add(this.btnRSD);
            this.Controls.Add(this.textBox1);
            this.Name = "Valuta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Valuta";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnRSD;
        private System.Windows.Forms.Button btnValuta;
    }
}