namespace OutgoingCalls
{
    partial class frmMain
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
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxLine = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPhone1 = new System.Windows.Forms.TextBox();
            this.buttonDial = new System.Windows.Forms.Button();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.txtPhone2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "خط مورد استفاده:";
            // 
            // comboBoxLine
            // 
            this.comboBoxLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLine.FormattingEnabled = true;
            this.comboBoxLine.Location = new System.Drawing.Point(134, 13);
            this.comboBoxLine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxLine.Name = "comboBoxLine";
            this.comboBoxLine.Size = new System.Drawing.Size(732, 24);
            this.comboBoxLine.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "شماره تماس:";
            // 
            // txtPhone1
            // 
            this.txtPhone1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhone1.Location = new System.Drawing.Point(134, 52);
            this.txtPhone1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPhone1.Name = "txtPhone1";
            this.txtPhone1.Size = new System.Drawing.Size(618, 23);
            this.txtPhone1.TabIndex = 5;
            // 
            // buttonDial
            // 
            this.buttonDial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDial.AutoSize = true;
            this.buttonDial.Location = new System.Drawing.Point(761, 52);
            this.buttonDial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonDial.Name = "buttonDial";
            this.buttonDial.Size = new System.Drawing.Size(106, 67);
            this.buttonDial.TabIndex = 6;
            this.buttonDial.Text = "شماره گیری";
            this.buttonDial.UseVisualStyleBackColor = true;
            this.buttonDial.Click += new System.EventHandler(this.buttonDial_Click);
            // 
            // listBoxLog
            // 
            this.listBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.HorizontalScrollbar = true;
            this.listBoxLog.ItemHeight = 16;
            this.listBoxLog.Items.AddRange(new object[] {
            "برای برقراری تماس خروجی یک خط را انتخاب کنید ، یک شماره را وارد کنید و روی شماره " +
                "گیری کلیک کنید."});
            this.listBoxLog.Location = new System.Drawing.Point(17, 135);
            this.listBoxLog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(850, 340);
            this.listBoxLog.TabIndex = 7;
            // 
            // txtPhone2
            // 
            this.txtPhone2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhone2.Location = new System.Drawing.Point(136, 96);
            this.txtPhone2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPhone2.Name = "txtPhone2";
            this.txtPhone2.Size = new System.Drawing.Size(618, 23);
            this.txtPhone2.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "شماره تماس:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 491);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPhone2);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.buttonDial);
            this.Controls.Add(this.txtPhone1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxLine);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "تمای های گرفته شده";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxLine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPhone1;
        private System.Windows.Forms.Button buttonDial;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.TextBox txtPhone2;
        private System.Windows.Forms.Label label4;
    }
}

