namespace CompressPicture
{
    partial class ImgMoving
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
            this.btnStart = new System.Windows.Forms.Button();
            this.txtImgPathInfos = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStart.Location = new System.Drawing.Point(1005, 457);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(105, 44);
            this.btnStart.TabIndex = 20;
            this.btnStart.Text = "开始转移";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtImgPathInfos
            // 
            this.txtImgPathInfos.Location = new System.Drawing.Point(12, 12);
            this.txtImgPathInfos.Multiline = true;
            this.txtImgPathInfos.Name = "txtImgPathInfos";
            this.txtImgPathInfos.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtImgPathInfos.Size = new System.Drawing.Size(1098, 439);
            this.txtImgPathInfos.TabIndex = 21;
            // 
            // ImgMoving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 506);
            this.Controls.Add(this.txtImgPathInfos);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImgMoving";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "移动项目封面图片";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtImgPathInfos;
    }
}