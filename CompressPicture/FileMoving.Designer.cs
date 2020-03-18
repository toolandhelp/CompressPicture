namespace CompressPicture
{
    partial class FileMoving
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnTarget = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslgj = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssldq = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslhs = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.dataGV.Location = new System.Drawing.Point(3, 40);
            this.dataGV.Name = "dataGV";
            this.dataGV.ReadOnly = true;
            this.dataGV.RowTemplate.Height = 23;
            this.dataGV.Size = new System.Drawing.Size(1091, 522);
            this.dataGV.TabIndex = 2;
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
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(959, 568);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(135, 47);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "开  始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnTarget
            // 
            this.btnTarget.Location = new System.Drawing.Point(444, 10);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(75, 23);
            this.btnTarget.TabIndex = 7;
            this.btnTarget.Text = "选择";
            this.btnTarget.UseVisualStyleBackColor = true;
            this.btnTarget.Click += new System.EventHandler(this.btnTarget_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "请选择开始盘符：";
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(123, 12);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(347, 21);
            this.txtTarget.TabIndex = 5;
            this.txtTarget.Text = "G:\\";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslgj,
            this.tssldq,
            this.tsslhs});
            this.statusStrip1.Location = new System.Drawing.Point(0, 619);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1096, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslgj
            // 
            this.tsslgj.Name = "tsslgj";
            this.tsslgj.Size = new System.Drawing.Size(51, 17);
            this.tsslgj.Text = "共计：0";
            // 
            // tssldq
            // 
            this.tssldq.Name = "tssldq";
            this.tssldq.Size = new System.Drawing.Size(51, 17);
            this.tssldq.Text = "当前：0";
            // 
            // tsslhs
            // 
            this.tsslhs.Name = "tsslhs";
            this.tsslhs.Size = new System.Drawing.Size(44, 17);
            this.tsslhs.Text = "耗时：";
            // 
            // FileMoving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 641);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnTarget);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTarget);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.dataGV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileMoving";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "文件移动";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGV)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslgj;
        private System.Windows.Forms.ToolStripStatusLabel tssldq;
        private System.Windows.Forms.ToolStripStatusLabel tsslhs;
    }
}