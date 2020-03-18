namespace CompressPicture
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.tsb02 = new System.Windows.Forms.ToolStripButton();
            this.tsb0MovFile = new System.Windows.Forms.ToolStripButton();
            this.tsbMovContentImg = new System.Windows.Forms.ToolStripButton();
            this.tsbProTitlePicUplToOSS = new System.Windows.Forms.ToolStripButton();
            this.tsbProPicUplToOSS = new System.Windows.Forms.ToolStripButton();
            this.tsbProFileUplToOSS = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb02,
            this.tsb0MovFile,
            this.tsbMovContentImg,
            this.tsbProTitlePicUplToOSS,
            this.tsbProPicUplToOSS,
            this.tsbProFileUplToOSS});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(904, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(275, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "1，移动图片（连接数据库，分析数据，移动数据）\r\n2，压缩图片 加水印\r\n";
            // 
            // tsb02
            // 
            this.tsb02.Image = ((System.Drawing.Image)(resources.GetObject("tsb02.Image")));
            this.tsb02.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb02.Name = "tsb02";
            this.tsb02.Size = new System.Drawing.Size(114, 22);
            this.tsb02.Text = "移动封面图片02";
            this.tsb02.Click += new System.EventHandler(this.tsb02_Click);
            // 
            // tsb0MovFile
            // 
            this.tsb0MovFile.Image = ((System.Drawing.Image)(resources.GetObject("tsb0MovFile.Image")));
            this.tsb0MovFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb0MovFile.Name = "tsb0MovFile";
            this.tsb0MovFile.Size = new System.Drawing.Size(76, 22);
            this.tsb0MovFile.Text = "移动文件";
            this.tsb0MovFile.Click += new System.EventHandler(this.tsbMovFile_Click);
            // 
            // tsbMovContentImg
            // 
            this.tsbMovContentImg.Image = ((System.Drawing.Image)(resources.GetObject("tsbMovContentImg.Image")));
            this.tsbMovContentImg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMovContentImg.Name = "tsbMovContentImg";
            this.tsbMovContentImg.Size = new System.Drawing.Size(100, 22);
            this.tsbMovContentImg.Text = "移动内容图片";
            this.tsbMovContentImg.Click += new System.EventHandler(this.tsbMovContentImg_Click);
            // 
            // tsbProTitlePicUplToOSS
            // 
            this.tsbProTitlePicUplToOSS.Image = global::CompressPicture.Properties.Resources.off;
            this.tsbProTitlePicUplToOSS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProTitlePicUplToOSS.Name = "tsbProTitlePicUplToOSS";
            this.tsbProTitlePicUplToOSS.Size = new System.Drawing.Size(196, 22);
            this.tsbProTitlePicUplToOSS.Text = "处理【封面】图片并上传到OSS";
            this.tsbProTitlePicUplToOSS.Click += new System.EventHandler(this.tsbProTitlePicUplToOSS_Click);
            // 
            // tsbProPicUplToOSS
            // 
            this.tsbProPicUplToOSS.Image = global::CompressPicture.Properties.Resources.on;
            this.tsbProPicUplToOSS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProPicUplToOSS.Name = "tsbProPicUplToOSS";
            this.tsbProPicUplToOSS.Size = new System.Drawing.Size(196, 22);
            this.tsbProPicUplToOSS.Text = "处理【内容图片】并上传到OSS";
            this.tsbProPicUplToOSS.Click += new System.EventHandler(this.tsbProPicUplToOSS_Click);
            // 
            // tsbProFileUplToOSS
            // 
            this.tsbProFileUplToOSS.Image = global::CompressPicture.Properties.Resources.on1;
            this.tsbProFileUplToOSS.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProFileUplToOSS.Name = "tsbProFileUplToOSS";
            this.tsbProFileUplToOSS.Size = new System.Drawing.Size(184, 22);
            this.tsbProFileUplToOSS.Text = "处理【资源】文件上传到OSS";
            this.tsbProFileUplToOSS.Click += new System.EventHandler(this.tsbProFileUplToOSS_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量处理图片";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tsb02;
        private System.Windows.Forms.ToolStripButton tsb0MovFile;
        private System.Windows.Forms.ToolStripButton tsbMovContentImg;
        private System.Windows.Forms.ToolStripButton tsbProPicUplToOSS;
        private System.Windows.Forms.ToolStripButton tsbProTitlePicUplToOSS;
        private System.Windows.Forms.ToolStripButton tsbProFileUplToOSS;
    }
}

