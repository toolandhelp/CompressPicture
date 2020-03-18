namespace CompressPicture
{
    partial class ContentImgMoving
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
            this.cb01 = new System.Windows.Forms.CheckBox();
            this.cb03 = new System.Windows.Forms.CheckBox();
            this.cb02 = new System.Windows.Forms.CheckBox();
            this.btnTarget = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTarget = new System.Windows.Forms.TextBox();
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
            this.dataGV.Location = new System.Drawing.Point(12, 40);
            this.dataGV.Name = "dataGV";
            this.dataGV.ReadOnly = true;
            this.dataGV.RowTemplate.Height = 23;
            this.dataGV.Size = new System.Drawing.Size(992, 519);
            this.dataGV.TabIndex = 3;
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
            this.btnStart.Location = new System.Drawing.Point(869, 565);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(135, 47);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "开  始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cb01
            // 
            this.cb01.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb01.AutoSize = true;
            this.cb01.Location = new System.Drawing.Point(12, 565);
            this.cb01.Name = "cb01";
            this.cb01.Size = new System.Drawing.Size(270, 16);
            this.cb01.TabIndex = 5;
            this.cb01.Text = "等比例压缩（生成一个新图【xxx_dys.jpg】）";
            this.cb01.UseVisualStyleBackColor = true;
            // 
            // cb03
            // 
            this.cb03.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb03.AutoSize = true;
            this.cb03.Location = new System.Drawing.Point(12, 603);
            this.cb03.Name = "cb03";
            this.cb03.Size = new System.Drawing.Size(456, 16);
            this.cb03.TabIndex = 7;
            this.cb03.Text = "压缩水印图片（在压缩图片上添加一个水印图片生成一个新图【xxx_yssy.jpg】）";
            this.cb03.UseVisualStyleBackColor = true;
            // 
            // cb02
            // 
            this.cb02.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cb02.AutoSize = true;
            this.cb02.Checked = true;
            this.cb02.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb02.Location = new System.Drawing.Point(12, 585);
            this.cb02.Name = "cb02";
            this.cb02.Size = new System.Drawing.Size(324, 16);
            this.cb02.TabIndex = 8;
            this.cb02.Text = "压缩图片（将src上的图片再次处理,先裁剪再质量压缩）";
            this.cb02.UseVisualStyleBackColor = true;
            // 
            // btnTarget
            // 
            this.btnTarget.Location = new System.Drawing.Point(456, 10);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(75, 23);
            this.btnTarget.TabIndex = 11;
            this.btnTarget.Text = "选择";
            this.btnTarget.UseVisualStyleBackColor = true;
            this.btnTarget.Click += new System.EventHandler(this.btnTarget_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "请选择开始盘符：";
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(135, 12);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(347, 21);
            this.txtTarget.TabIndex = 9;
            this.txtTarget.Text = "G:\\";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsslgj,
            this.tssldq,
            this.tsslhs,
            this.tsslgyxm});
            this.statusStrip1.Location = new System.Drawing.Point(0, 629);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1016, 22);
            this.statusStrip1.TabIndex = 12;
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
            // ContentImgMoving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 651);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnTarget);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTarget);
            this.Controls.Add(this.cb02);
            this.Controls.Add(this.cb03);
            this.Controls.Add(this.cb01);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.dataGV);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContentImgMoving";
            this.Text = "内容图片移动";
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
        private System.Windows.Forms.CheckBox cb01;
        private System.Windows.Forms.CheckBox cb03;
        private System.Windows.Forms.CheckBox cb02;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslgj;
        private System.Windows.Forms.ToolStripStatusLabel tssldq;
        private System.Windows.Forms.ToolStripStatusLabel tsslhs;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsslgyxm;
    }
}