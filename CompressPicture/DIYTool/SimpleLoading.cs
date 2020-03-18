﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompressPicture.DIYTool
{
    public partial class SimpleLoading : Form
    {
        //public SimpleLoading()
        //{
        //    InitializeComponent();
        //}


        //保存父窗口信息，主要用于居中显示加载窗体
        private Form partentForm = null;
        public SimpleLoading(Form partentForm)
        {
            InitializeComponent();
            this.partentForm = partentForm;
        }

        private void SimpleLoading_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            // 下面的方法用来使得Loading窗体居中父窗体显示
            int parentForm_Position_x = this.partentForm.Location.X;
            int parentForm_Position_y = this.partentForm.Location.Y;
            int parentForm_Width = this.partentForm.Width;
            int parentForm_Height = this.partentForm.Height;

            int start_x = (int)(parentForm_Position_x + (parentForm_Width - this.Width) / 2);
            int start_y = (int)(parentForm_Position_y + (parentForm_Height - this.Height) / 2);
            this.Location = new System.Drawing.Point(start_x, start_y);
        }
    }
}
