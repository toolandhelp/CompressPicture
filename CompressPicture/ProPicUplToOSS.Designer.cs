namespace CompressPicture
{
    partial class ProPicUplToOSS
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
            this.dataGV = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperateKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OldPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Summary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslgj = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssldq = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslhs = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslgyxm = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(1007, 614);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(135, 47);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "开  始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // dataGV
            // 
            this.dataGV.AllowUserToAddRows = false;
            this.dataGV.AllowUserToDeleteRows = false;
            this.dataGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.OperateKey,
            this.Operate,
            this.ItemId,
            this.OldPath,
            this.NewPath,
            this.Status,
            this.Summary,
            this.CreateDate});
            this.dataGV.Location = new System.Drawing.Point(4, 1);
            this.dataGV.Name = "dataGV";
            this.dataGV.ReadOnly = true;
            this.dataGV.RowTemplate.Height = 23;
            this.dataGV.Size = new System.Drawing.Size(1138, 607);
            this.dataGV.TabIndex = 5;
            // 
            // Id
            // 
            this.Id.HeaderText = "主键";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // OperateKey
            // 
            this.OperateKey.HeaderText = "操作值";
            this.OperateKey.Name = "OperateKey";
            this.OperateKey.ReadOnly = true;
            // 
            // Operate
            // 
            this.Operate.HeaderText = "操作事件";
            this.Operate.Name = "Operate";
            this.Operate.ReadOnly = true;
            // 
            // ItemId
            // 
            this.ItemId.HeaderText = "项目ID";
            this.ItemId.Name = "ItemId";
            this.ItemId.ReadOnly = true;
            // 
            // OldPath
            // 
            this.OldPath.HeaderText = "老图路径";
            this.OldPath.Name = "OldPath";
            this.OldPath.ReadOnly = true;
            // 
            // NewPath
            // 
            this.NewPath.HeaderText = "新图路径";
            this.NewPath.Name = "NewPath";
            this.NewPath.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "操作状态";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Summary
            // 
            this.Summary.HeaderText = "摘要";
            this.Summary.Name = "Summary";
            this.Summary.ReadOnly = true;
            // 
            // CreateDate
            // 
            this.CreateDate.HeaderText = "操作时间";
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.ReadOnly = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsslgj,
            this.tssldq,
            this.tsslhs,
            this.tsslgyxm});
            this.statusStrip1.Location = new System.Drawing.Point(0, 664);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1146, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 17);
            this.toolStripStatusLabel1.Text = "图片数量：";
            // 
            // tsslgj
            // 
            this.tsslgj.Name = "tsslgj";
            this.tsslgj.Size = new System.Drawing.Size(15, 17);
            this.tsslgj.Text = "0";
            // 
            // tssldq
            // 
            this.tssldq.Name = "tssldq";
            this.tssldq.Size = new System.Drawing.Size(75, 17);
            this.tssldq.Text = "当前：0【】";
            // 
            // tsslhs
            // 
            this.tsslhs.Name = "tsslhs";
            this.tsslhs.Size = new System.Drawing.Size(44, 17);
            this.tsslhs.Text = "耗时：";
            // 
            // tsslgyxm
            // 
            this.tsslgyxm.Name = "tsslgyxm";
            this.tsslgyxm.Size = new System.Drawing.Size(158, 17);
            this.tsslgyxm.Text = "共计项目：{0}，有效项目{0}";
            // 
            // ProPicUplToOSS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 686);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.dataGV);
            this.Name = "ProPicUplToOSS";
            this.Text = "ProPicUplToOSS";
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView dataGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperateKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn OldPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Summary;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsslgj;
        private System.Windows.Forms.ToolStripStatusLabel tssldq;
        private System.Windows.Forms.ToolStripStatusLabel tsslhs;
        private System.Windows.Forms.ToolStripStatusLabel tsslgyxm;
    }
}