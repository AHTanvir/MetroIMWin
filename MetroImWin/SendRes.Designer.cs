namespace MetroImWin
{
    partial class SendRes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendRes));
            this.dept_spinner = new System.Windows.Forms.ComboBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dept_spinner
            // 
            this.dept_spinner.BackColor = System.Drawing.Color.Honeydew;
            this.dept_spinner.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dept_spinner.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dept_spinner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dept_spinner.FormattingEnabled = true;
            this.dept_spinner.Items.AddRange(new object[] {
            "CSE",
            "EEE",
            "BBA",
            "LLB"});
            this.dept_spinner.Location = new System.Drawing.Point(51, 40);
            this.dept_spinner.Name = "dept_spinner";
            this.dept_spinner.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dept_spinner.Size = new System.Drawing.Size(151, 24);
            this.dept_spinner.TabIndex = 1;
            this.dept_spinner.Text = "Select department";
            this.dept_spinner.SelectedIndexChanged += new System.EventHandler(this.dept_spinner_SelectedIndexChanged);
            // 
            // btn_send
            // 
            this.btn_send.AutoEllipsis = true;
            this.btn_send.BackColor = System.Drawing.Color.DarkOrange;
            this.btn_send.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_send.BackgroundImage")));
            this.btn_send.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_send.CausesValidation = false;
            this.btn_send.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btn_send.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_send.ForeColor = System.Drawing.Color.BurlyWood;
            this.btn_send.Location = new System.Drawing.Point(63, 106);
            this.btn_send.Margin = new System.Windows.Forms.Padding(0);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(123, 48);
            this.btn_send.TabIndex = 8;
            this.btn_send.Text = "Send";
            this.btn_send.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_send.UseVisualStyleBackColor = false;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // SendRes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.ClientSize = new System.Drawing.Size(252, 191);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.dept_spinner);
            this.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SendRes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.SendRes_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox dept_spinner;
        private System.Windows.Forms.Button btn_send;
    }
}