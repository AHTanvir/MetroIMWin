namespace MetroImWin
{
    partial class Notice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notice));
            this.dept_spinner = new System.Windows.Forms.ComboBox();
            this.type_spinner = new System.Windows.Forms.ComboBox();
            this.beg_id = new System.Windows.Forms.TextBox();
            this.end_id = new System.Windows.Forms.TextBox();
            this.msg = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dept_spinner
            // 
            this.dept_spinner.Cursor = System.Windows.Forms.Cursors.Default;
            this.dept_spinner.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dept_spinner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dept_spinner.FormattingEnabled = true;
            this.dept_spinner.Items.AddRange(new object[] {
            "CSE",
            "EEE",
            "BBA",
            "LLB"});
            this.dept_spinner.Location = new System.Drawing.Point(141, 22);
            this.dept_spinner.Name = "dept_spinner";
            this.dept_spinner.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dept_spinner.Size = new System.Drawing.Size(184, 24);
            this.dept_spinner.TabIndex = 0;
            this.dept_spinner.Text = "        Select department";
            this.dept_spinner.SelectedIndexChanged += new System.EventHandler(this.dept_spinner_SelectedIndexChanged);
            // 
            // type_spinner
            // 
            this.type_spinner.AllowDrop = true;
            this.type_spinner.Cursor = System.Windows.Forms.Cursors.Default;
            this.type_spinner.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.type_spinner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.type_spinner.FormattingEnabled = true;
            this.type_spinner.Items.AddRange(new object[] {
            "Image",
            "Pdf",
            "Text",
            "Result"});
            this.type_spinner.Location = new System.Drawing.Point(141, 71);
            this.type_spinner.Name = "type_spinner";
            this.type_spinner.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.type_spinner.Size = new System.Drawing.Size(184, 24);
            this.type_spinner.TabIndex = 1;
            this.type_spinner.Text = "        Select Notice Type";
            this.type_spinner.SelectedIndexChanged += new System.EventHandler(this.type_spinner_SelectedIndexChanged);
            // 
            // beg_id
            // 
            this.beg_id.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.beg_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.beg_id.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.beg_id.Location = new System.Drawing.Point(141, 129);
            this.beg_id.Name = "beg_id";
            this.beg_id.Size = new System.Drawing.Size(184, 24);
            this.beg_id.TabIndex = 2;
            this.beg_id.Text = "Begnning id";
            this.beg_id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.beg_id.Click += new System.EventHandler(this.beg_id_Click);
            this.beg_id.TextChanged += new System.EventHandler(this.beg_id_TextChanged);
            // 
            // end_id
            // 
            this.end_id.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.end_id.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.end_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.end_id.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.end_id.Location = new System.Drawing.Point(141, 188);
            this.end_id.Name = "end_id";
            this.end_id.Size = new System.Drawing.Size(184, 24);
            this.end_id.TabIndex = 3;
            this.end_id.Text = "Ending id";
            this.end_id.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.end_id.Click += new System.EventHandler(this.end_id_Click);
            // 
            // msg
            // 
            this.msg.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msg.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.msg.Location = new System.Drawing.Point(69, 234);
            this.msg.Multiline = true;
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(339, 86);
            this.msg.TabIndex = 4;
            this.msg.Text = "Write Message";
            this.msg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.msg.Visible = false;
            this.msg.TextChanged += new System.EventHandler(this.msg_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.Location = new System.Drawing.Point(138, 110);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(184, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "e,g 111-222-1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.UseCompatibleTextRendering = true;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Location = new System.Drawing.Point(138, 171);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(184, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "e,g 111-222-10";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btn_send
            // 
            this.btn_send.AutoEllipsis = true;
            this.btn_send.BackColor = System.Drawing.Color.DarkOrange;
            this.btn_send.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_send.BackgroundImage")));
            this.btn_send.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_send.CausesValidation = false;
            this.btn_send.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.btn_send.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_send.ForeColor = System.Drawing.Color.BurlyWood;
            this.btn_send.Location = new System.Drawing.Point(173, 323);
            this.btn_send.Margin = new System.Windows.Forms.Padding(0);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(123, 48);
            this.btn_send.TabIndex = 7;
            this.btn_send.Text = "Send";
            this.btn_send.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_send.UseVisualStyleBackColor = false;
            this.btn_send.Click += new System.EventHandler(this.button1_Click);
            // 
            // Notice
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkOrange;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(484, 380);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.end_id);
            this.Controls.Add(this.beg_id);
            this.Controls.Add(this.type_spinner);
            this.Controls.Add(this.dept_spinner);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.msg);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Notice";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox dept_spinner;
        private System.Windows.Forms.ComboBox type_spinner;
        private System.Windows.Forms.TextBox beg_id;
        private System.Windows.Forms.TextBox end_id;
        private System.Windows.Forms.TextBox msg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_send;
    }
}