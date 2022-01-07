
namespace ArchiverGT
{
    partial class StartForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.UnzipBotton = new ArchiverGT.Controls.CustBotton();
            this.ArchBotton = new ArchiverGT.Controls.CustBotton();
            this.labelPercent = new System.Windows.Forms.Label();
            this.ProgressArchiving = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.OpenButton = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpenButton)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(182)))), ((int)(((byte)(182)))));
            this.panel1.Controls.Add(this.Cancel_Button);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.UnzipBotton);
            this.panel1.Controls.Add(this.ArchBotton);
            this.panel1.Controls.Add(this.labelPercent);
            this.panel1.Controls.Add(this.ProgressArchiving);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.OpenButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 350);
            this.panel1.TabIndex = 2;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Location = new System.Drawing.Point(279, 241);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Button.TabIndex = 17;
            this.Cancel_Button.Text = "Отмена";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.MenuText;
            this.textBox2.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox2.Location = new System.Drawing.Point(12, 31);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox2.Size = new System.Drawing.Size(354, 131);
            this.textBox2.TabIndex = 15;
            // 
            // UnzipBotton
            // 
            this.UnzipBotton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.UnzipBotton.ForeColor = System.Drawing.Color.White;
            this.UnzipBotton.Location = new System.Drawing.Point(127, 234);
            this.UnzipBotton.Name = "UnzipBotton";
            this.UnzipBotton.Size = new System.Drawing.Size(100, 30);
            this.UnzipBotton.TabIndex = 14;
            this.UnzipBotton.Text = "Разархивировать";
            this.UnzipBotton.Click += new System.EventHandler(this.UnzipBotton_Click);
            // 
            // ArchBotton
            // 
            this.ArchBotton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.ArchBotton.ForeColor = System.Drawing.Color.White;
            this.ArchBotton.Location = new System.Drawing.Point(21, 234);
            this.ArchBotton.Name = "ArchBotton";
            this.ArchBotton.Size = new System.Drawing.Size(100, 30);
            this.ArchBotton.TabIndex = 13;
            this.ArchBotton.Text = "Архивировать";
            this.ArchBotton.Click += new System.EventHandler(this.ArchBotton_Click);
            // 
            // labelPercent
            // 
            this.labelPercent.AutoSize = true;
            this.labelPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPercent.Location = new System.Drawing.Point(39, 185);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(33, 16);
            this.labelPercent.TabIndex = 11;
            this.labelPercent.Text = "0 %";
            // 
            // ProgressArchiving
            // 
            this.ProgressArchiving.Location = new System.Drawing.Point(20, 205);
            this.ProgressArchiving.Name = "ProgressArchiving";
            this.ProgressArchiving.Size = new System.Drawing.Size(325, 23);
            this.ProgressArchiving.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(72, 304);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(273, 20);
            this.textBox1.TabIndex = 2;
            // 
            // OpenButton
            // 
            this.OpenButton.Image = global::ArchiverGT.Properties.Resources.path22;
            this.OpenButton.Location = new System.Drawing.Point(10, 288);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(60, 49);
            this.OpenButton.TabIndex = 4;
            this.OpenButton.TabStop = false;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            this.OpenButton.MouseEnter += new System.EventHandler(this.OpenButton_MouseEnter);
            this.OpenButton.MouseLeave += new System.EventHandler(this.OpenButton_MouseLeave);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // timer2
            // 
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 350);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "StartForm";
            this.Text = "ArchiverGT";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpenButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox OpenButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ProgressBar ProgressArchiving;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label labelPercent;
        private Controls.CustBotton ArchBotton;
        private Controls.CustBotton UnzipBotton;
        public System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Timer timer2;
        public static System.Windows.Forms.TextBox Console;
        private System.Windows.Forms.Button Cancel_Button;
    }
}