namespace BetterSite.SiteTool
{
    partial class SiteTool
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.SourceUrl = new System.Windows.Forms.TextBox();
            this.PullData = new System.Windows.Forms.Button();
            this.PushData = new System.Windows.Forms.Button();
            this.label1SiteCode = new System.Windows.Forms.Label();
            this.SiteCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SiteName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SiteUrl = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SiteProfile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TypeId = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SiteOrderNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SiteImg = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SiteImgBase64 = new System.Windows.Forms.RichTextBox();
            this.SystemMsg = new System.Windows.Forms.TextBox();
            this.CopyMsg = new System.Windows.Forms.Button();
            this.ClearMsg = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.SiteKeywords = new System.Windows.Forms.TextBox();
            this.BackWorker = new System.ComponentModel.BackgroundWorker();
            this.CancelPull = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.CmbViewSite = new System.Windows.Forms.ComboBox();
            this.BtnViewData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SiteImg)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "SourceUrl";
            // 
            // SourceUrl
            // 
            this.SourceUrl.Location = new System.Drawing.Point(88, 44);
            this.SourceUrl.Multiline = true;
            this.SourceUrl.Name = "SourceUrl";
            this.SourceUrl.Size = new System.Drawing.Size(302, 51);
            this.SourceUrl.TabIndex = 1;
            // 
            // PullData
            // 
            this.PullData.Location = new System.Drawing.Point(88, 101);
            this.PullData.Name = "PullData";
            this.PullData.Size = new System.Drawing.Size(95, 28);
            this.PullData.TabIndex = 2;
            this.PullData.Text = "拉取数据";
            this.PullData.UseVisualStyleBackColor = true;
            this.PullData.Click += new System.EventHandler(this.PullData_Click);
            // 
            // PushData
            // 
            this.PushData.Location = new System.Drawing.Point(740, 636);
            this.PushData.Name = "PushData";
            this.PushData.Size = new System.Drawing.Size(95, 28);
            this.PushData.TabIndex = 3;
            this.PushData.Text = "推送数据";
            this.PushData.UseVisualStyleBackColor = true;
            this.PushData.Click += new System.EventHandler(this.PushData_Click);
            // 
            // label1SiteCode
            // 
            this.label1SiteCode.AutoSize = true;
            this.label1SiteCode.Location = new System.Drawing.Point(24, 138);
            this.label1SiteCode.Name = "label1SiteCode";
            this.label1SiteCode.Size = new System.Drawing.Size(53, 12);
            this.label1SiteCode.TabIndex = 0;
            this.label1SiteCode.Text = "SiteCode";
            // 
            // SiteCode
            // 
            this.SiteCode.Location = new System.Drawing.Point(88, 131);
            this.SiteCode.Name = "SiteCode";
            this.SiteCode.Size = new System.Drawing.Size(302, 21);
            this.SiteCode.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "SiteName";
            // 
            // SiteName
            // 
            this.SiteName.Location = new System.Drawing.Point(88, 157);
            this.SiteName.Name = "SiteName";
            this.SiteName.Size = new System.Drawing.Size(302, 21);
            this.SiteName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "SiteUrl";
            // 
            // SiteUrl
            // 
            this.SiteUrl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SiteUrl.Location = new System.Drawing.Point(88, 183);
            this.SiteUrl.Name = "SiteUrl";
            this.SiteUrl.Size = new System.Drawing.Size(302, 21);
            this.SiteUrl.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 235);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "SiteProfile";
            // 
            // SiteProfile
            // 
            this.SiteProfile.Location = new System.Drawing.Point(88, 209);
            this.SiteProfile.Multiline = true;
            this.SiteProfile.Name = "SiteProfile";
            this.SiteProfile.Size = new System.Drawing.Size(302, 59);
            this.SiteProfile.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 332);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "TypeId";
            // 
            // TypeId
            // 
            this.TypeId.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TypeId.FormattingEnabled = true;
            this.TypeId.ItemHeight = 13;
            this.TypeId.Location = new System.Drawing.Point(88, 326);
            this.TypeId.Name = "TypeId";
            this.TypeId.Size = new System.Drawing.Size(121, 21);
            this.TypeId.TabIndex = 6;
            this.TypeId.SelectedIndexChanged += new System.EventHandler(this.TypeId_SelectedIndexChanged);
            this.TypeId.SelectedValueChanged += new System.EventHandler(this.TypeId_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 470);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 12);
            this.label6.TabIndex = 0;
            // 
            // SiteOrderNumber
            // 
            this.SiteOrderNumber.Location = new System.Drawing.Point(325, 325);
            this.SiteOrderNumber.Name = "SiteOrderNumber";
            this.SiteOrderNumber.Size = new System.Drawing.Size(65, 21);
            this.SiteOrderNumber.TabIndex = 1;
            this.SiteOrderNumber.Text = "100";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(242, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "SiteOrderNO.";
            // 
            // SiteImg
            // 
            this.SiteImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SiteImg.Location = new System.Drawing.Point(4, 4);
            this.SiteImg.Name = "SiteImg";
            this.SiteImg.Padding = new System.Windows.Forms.Padding(3);
            this.SiteImg.Size = new System.Drawing.Size(600, 450);
            this.SiteImg.TabIndex = 5;
            this.SiteImg.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(407, 83);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(619, 488);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.SiteImg);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(611, 462);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "图片";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.SiteImgBase64);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(611, 462);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Base64";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SiteImgBase64
            // 
            this.SiteImgBase64.Location = new System.Drawing.Point(4, 5);
            this.SiteImgBase64.Name = "SiteImgBase64";
            this.SiteImgBase64.Size = new System.Drawing.Size(600, 450);
            this.SiteImgBase64.TabIndex = 0;
            this.SiteImgBase64.Text = "";
            this.SiteImgBase64.TextChanged += new System.EventHandler(this.SiteImgBase64_TextChanged);
            // 
            // SystemMsg
            // 
            this.SystemMsg.Location = new System.Drawing.Point(414, 49);
            this.SystemMsg.Multiline = true;
            this.SystemMsg.Name = "SystemMsg";
            this.SystemMsg.Size = new System.Drawing.Size(433, 23);
            this.SystemMsg.TabIndex = 1;
            this.SystemMsg.Text = "msg...";
            // 
            // CopyMsg
            // 
            this.CopyMsg.Location = new System.Drawing.Point(940, 49);
            this.CopyMsg.Name = "CopyMsg";
            this.CopyMsg.Size = new System.Drawing.Size(75, 23);
            this.CopyMsg.TabIndex = 9;
            this.CopyMsg.Text = "复制消息";
            this.CopyMsg.UseVisualStyleBackColor = true;
            this.CopyMsg.Click += new System.EventHandler(this.CopyMsg_Click);
            // 
            // ClearMsg
            // 
            this.ClearMsg.Location = new System.Drawing.Point(853, 49);
            this.ClearMsg.Name = "ClearMsg";
            this.ClearMsg.Size = new System.Drawing.Size(75, 23);
            this.ClearMsg.TabIndex = 9;
            this.ClearMsg.Text = "清空消息";
            this.ClearMsg.UseVisualStyleBackColor = true;
            this.ClearMsg.Click += new System.EventHandler(this.ClearMsg_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 289);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "SiteKeywords";
            // 
            // SiteKeywords
            // 
            this.SiteKeywords.Location = new System.Drawing.Point(88, 273);
            this.SiteKeywords.Multiline = true;
            this.SiteKeywords.Name = "SiteKeywords";
            this.SiteKeywords.Size = new System.Drawing.Size(302, 45);
            this.SiteKeywords.TabIndex = 1;
            // 
            // BackWorker
            // 
            this.BackWorker.WorkerReportsProgress = true;
            this.BackWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackWorker_DoWork);
            this.BackWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackWorker_RunWorkerCompleted);
            // 
            // CancelPull
            // 
            this.CancelPull.Location = new System.Drawing.Point(295, 101);
            this.CancelPull.Name = "CancelPull";
            this.CancelPull.Size = new System.Drawing.Size(95, 28);
            this.CancelPull.TabIndex = 2;
            this.CancelPull.Text = "取消拉取";
            this.CancelPull.UseVisualStyleBackColor = true;
            this.CancelPull.Click += new System.EventHandler(this.CancelPullData_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 359);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "SiteTag";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 351);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 27);
            this.button1.TabIndex = 11;
            this.button1.Text = "加载标签";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(412, 587);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "已拉取站点:";
            // 
            // CmbViewSite
            // 
            this.CmbViewSite.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CmbViewSite.FormattingEnabled = true;
            this.CmbViewSite.Location = new System.Drawing.Point(492, 580);
            this.CmbViewSite.Name = "CmbViewSite";
            this.CmbViewSite.Size = new System.Drawing.Size(367, 24);
            this.CmbViewSite.TabIndex = 13;
            // 
            // BtnViewData
            // 
            this.BtnViewData.Location = new System.Drawing.Point(521, 636);
            this.BtnViewData.Name = "BtnViewData";
            this.BtnViewData.Size = new System.Drawing.Size(95, 28);
            this.BtnViewData.TabIndex = 14;
            this.BtnViewData.Text = "装载数据";
            this.BtnViewData.UseVisualStyleBackColor = true;
            this.BtnViewData.Click += new System.EventHandler(this.BtnViewData_Click);
            // 
            // SiteTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 708);
            this.Controls.Add(this.BtnViewData);
            this.Controls.Add(this.CmbViewSite);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ClearMsg);
            this.Controls.Add(this.CopyMsg);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.TypeId);
            this.Controls.Add(this.PushData);
            this.Controls.Add(this.CancelPull);
            this.Controls.Add(this.PullData);
            this.Controls.Add(this.SiteOrderNumber);
            this.Controls.Add(this.SystemMsg);
            this.Controls.Add(this.SiteProfile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SiteKeywords);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.SiteUrl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SiteName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SiteCode);
            this.Controls.Add(this.label1SiteCode);
            this.Controls.Add(this.SourceUrl);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SiteTool";
            this.Text = "SiteTool";
            this.Load += new System.EventHandler(this.SiteTool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SiteImg)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SourceUrl;
        private System.Windows.Forms.Button PullData;
        private System.Windows.Forms.Button PushData;
        private System.Windows.Forms.Label label1SiteCode;
        private System.Windows.Forms.TextBox SiteCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox SiteName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SiteUrl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SiteProfile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox TypeId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox SiteOrderNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox SiteImg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox SiteImgBase64;
        private System.Windows.Forms.TextBox SystemMsg;
        private System.Windows.Forms.Button CopyMsg;
        private System.Windows.Forms.Button ClearMsg;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox SiteKeywords;
        private System.ComponentModel.BackgroundWorker BackWorker;
        private System.Windows.Forms.Button CancelPull;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox CmbViewSite;
        private System.Windows.Forms.Button BtnViewData;
    }
}

