namespace BetterSite.SiteTool
{
    partial class PushBaidu
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
            this.txturls = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.labpushmsg = new System.Windows.Forms.Label();
            this.pushmsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txturls
            // 
            this.txturls.Location = new System.Drawing.Point(12, 12);
            this.txturls.Multiline = true;
            this.txturls.Name = "txturls";
            this.txturls.Size = new System.Drawing.Size(547, 349);
            this.txturls.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(370, 370);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "百度主动推送";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labpushmsg
            // 
            this.labpushmsg.AutoSize = true;
            this.labpushmsg.Location = new System.Drawing.Point(34, 373);
            this.labpushmsg.Name = "labpushmsg";
            this.labpushmsg.Size = new System.Drawing.Size(59, 12);
            this.labpushmsg.TabIndex = 2;
            this.labpushmsg.Text = "推送信息:";
            // 
            // pushmsg
            // 
            this.pushmsg.AutoSize = true;
            this.pushmsg.Location = new System.Drawing.Point(107, 373);
            this.pushmsg.Name = "pushmsg";
            this.pushmsg.Size = new System.Drawing.Size(0, 12);
            this.pushmsg.TabIndex = 3;
            // 
            // PushBaidu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 402);
            this.Controls.Add(this.pushmsg);
            this.Controls.Add(this.labpushmsg);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txturls);
            this.Name = "PushBaidu";
            this.Text = "PushBaidu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txturls;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labpushmsg;
        private System.Windows.Forms.Label pushmsg;
    }
}