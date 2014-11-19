namespace TwitterAPItest
{
    partial class Form1
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tweetBox = new System.Windows.Forms.TextBox();
            this.avatarBox = new System.Windows.Forms.PictureBox();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.pinBox = new System.Windows.Forms.TextBox();
            this.pinBtn = new System.Windows.Forms.Button();
            this.getMoreBtn = new System.Windows.Forms.Button();
            this.rowsAddedTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(250, 250);
            this.webBrowser1.TabIndex = 0;
            // 
            // tweetBox
            // 
            this.tweetBox.Location = new System.Drawing.Point(296, 62);
            this.tweetBox.Multiline = true;
            this.tweetBox.Name = "tweetBox";
            this.tweetBox.Size = new System.Drawing.Size(279, 62);
            this.tweetBox.TabIndex = 1;
            // 
            // avatarBox
            // 
            this.avatarBox.Location = new System.Drawing.Point(268, 8);
            this.avatarBox.Name = "avatarBox";
            this.avatarBox.Size = new System.Drawing.Size(79, 50);
            this.avatarBox.TabIndex = 2;
            this.avatarBox.TabStop = false;
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(354, 8);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(193, 20);
            this.usernameBox.TabIndex = 3;
            // 
            // pinBox
            // 
            this.pinBox.Location = new System.Drawing.Point(269, 230);
            this.pinBox.Name = "pinBox";
            this.pinBox.Size = new System.Drawing.Size(150, 20);
            this.pinBox.TabIndex = 5;
            // 
            // pinBtn
            // 
            this.pinBtn.Location = new System.Drawing.Point(423, 227);
            this.pinBtn.Name = "pinBtn";
            this.pinBtn.Size = new System.Drawing.Size(75, 23);
            this.pinBtn.TabIndex = 6;
            this.pinBtn.Text = "Enter Pin";
            this.pinBtn.UseVisualStyleBackColor = true;
            this.pinBtn.Click += new System.EventHandler(this.pinBtn_Click);
            // 
            // getMoreBtn
            // 
            this.getMoreBtn.Location = new System.Drawing.Point(377, 144);
            this.getMoreBtn.Name = "getMoreBtn";
            this.getMoreBtn.Size = new System.Drawing.Size(132, 23);
            this.getMoreBtn.TabIndex = 7;
            this.getMoreBtn.Text = "Grab Some More";
            this.getMoreBtn.UseVisualStyleBackColor = true;
            this.getMoreBtn.Click += new System.EventHandler(this.getMoreBtn_Click);
            // 
            // rowsAddedTB
            // 
            this.rowsAddedTB.Location = new System.Drawing.Point(505, 227);
            this.rowsAddedTB.Name = "rowsAddedTB";
            this.rowsAddedTB.ReadOnly = true;
            this.rowsAddedTB.Size = new System.Drawing.Size(82, 20);
            this.rowsAddedTB.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(554, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "RT";
            this.label1.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rowsAddedTB);
            this.Controls.Add(this.getMoreBtn);
            this.Controls.Add(this.pinBtn);
            this.Controls.Add(this.pinBox);
            this.Controls.Add(this.usernameBox);
            this.Controls.Add(this.avatarBox);
            this.Controls.Add(this.tweetBox);
            this.Controls.Add(this.webBrowser1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.avatarBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TextBox tweetBox;
        private System.Windows.Forms.PictureBox avatarBox;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.TextBox pinBox;
        private System.Windows.Forms.Button pinBtn;
        private System.Windows.Forms.Button getMoreBtn;
        private System.Windows.Forms.TextBox rowsAddedTB;
        private System.Windows.Forms.Label label1;
    }
}

