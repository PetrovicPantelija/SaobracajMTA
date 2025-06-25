namespace Saobracaj.Drumski
{
    partial class frmEmailLogin
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
            this.txtLozinka = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.button1 = new Syncfusion.WinForms.Controls.SfButton();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtEmail = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.txtLozinka)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLozinka
            // 
            this.txtLozinka.BeforeTouchSize = new System.Drawing.Size(375, 28);
            this.txtLozinka.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLozinka.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtLozinka.Location = new System.Drawing.Point(45, 154);
            this.txtLozinka.Margin = new System.Windows.Forms.Padding(4);
            this.txtLozinka.Name = "txtLozinka";
            this.txtLozinka.PasswordChar = '●';
            this.txtLozinka.Size = new System.Drawing.Size(375, 28);
            this.txtLozinka.TabIndex = 47;
            this.txtLozinka.UseSystemPasswordChar = true;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.Location = new System.Drawing.Point(45, 45);
            this.autoLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(141, 24);
            this.autoLabel1.TabIndex = 48;
            this.autoLabel1.Text = "E-mail korisnika";
            // 
            // autoLabel2
            // 
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.autoLabel2.Location = new System.Drawing.Point(45, 125);
            this.autoLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(74, 24);
            this.autoLabel2.TabIndex = 49;
            this.autoLabel2.Text = "Lozinka";
            // 
            // button1
            // 
            this.button1.AccessibleName = "Button";
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(45, 232);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(376, 47);
            this.button1.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            this.button1.Style.ForeColor = System.Drawing.Color.White;
            this.button1.TabIndex = 50;
            this.button1.Text = "Potvrdi";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(54)))), ((int)(((byte)(56)))));
            this.autoLabel3.Location = new System.Drawing.Point(54, 30);
            this.autoLabel3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(0, 58);
            this.autoLabel3.TabIndex = 54;
            // 
            // txtEmail
            // 
            this.txtEmail.BeforeTouchSize = new System.Drawing.Size(375, 28);
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtEmail.Location = new System.Drawing.Point(45, 73);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(375, 28);
            this.txtEmail.TabIndex = 62;
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // frmEmailLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(487, 324);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtLozinka);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.autoLabel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmEmailLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Podaci pošiljaoca";
            ((System.ComponentModel.ISupportInitialize)(this.txtLozinka)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtLozinka;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.WinForms.Controls.SfButton button1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtEmail;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
    }
}