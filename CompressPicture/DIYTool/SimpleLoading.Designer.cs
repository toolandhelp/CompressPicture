namespace CompressPicture.DIYTool
{
    partial class SimpleLoading
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
            this.加载中 = new System.Windows.Forms.Label();
            this.lblLoadingImg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // 加载中
            // 
            this.加载中.AutoSize = true;
            this.加载中.Location = new System.Drawing.Point(93, 45);
            this.加载中.Name = "加载中";
            this.加载中.Size = new System.Drawing.Size(107, 12);
            this.加载中.TabIndex = 1;
            this.加载中.Text = "Please Waiting...";
            // 
            // lblLoadingImg
            // 
            this.lblLoadingImg.BackColor = System.Drawing.SystemColors.Control;
            this.lblLoadingImg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLoadingImg.Image = global::CompressPicture.Properties.Resources.loading3;
            this.lblLoadingImg.Location = new System.Drawing.Point(7, 7);
            this.lblLoadingImg.Name = "lblLoadingImg";
            this.lblLoadingImg.Size = new System.Drawing.Size(85, 85);
            this.lblLoadingImg.TabIndex = 0;
            this.lblLoadingImg.Text = " ";
            // 
            // SimpleLoading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 100);
            this.Controls.Add(this.加载中);
            this.Controls.Add(this.lblLoadingImg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SimpleLoading";
            this.Text = "加载中…";
            this.Load += new System.EventHandler(this.SimpleLoading_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLoadingImg;
        private System.Windows.Forms.Label 加载中;
    }
}