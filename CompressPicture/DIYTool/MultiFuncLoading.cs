﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SplashScreenDemo
{
    public partial class MultiFuncLoading : Form
    {
        //保存父窗口信息，主要用于居中显示加载窗体
        private Form partentForm=null;
        public MultiFuncLoading(Form partentForm)
        {
            InitializeComponent();
            this.partentForm = partentForm;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            //设置一些Loading窗体信息
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ControlBox = false;
            this.Text = "有进度，有提示，有取消按钮 Loading...";
            // 下面的方法用来使得Loading窗体居中父窗体显示
            int parentForm_Position_x = this.partentForm.Location.X;
            int parentForm_Position_y = this.partentForm.Location.Y;
            int parentForm_Width = this.partentForm.Width;
            int parentForm_Height = this.partentForm.Height;

            int start_x = (int)(parentForm_Position_x + (parentForm_Width - this.Width) / 2);
            int start_y = (int)(parentForm_Position_y + (parentForm_Height - this.Height) / 2);
            this.Location = new System.Drawing.Point(start_x, start_y);
            
        }

        ///// <summary>
        ///// 改变Loading的进度
        ///// </summary>
        ///// <param name="percent"></param>
        public void SetTxt(string title="Loading...",string lbl1= "加载中，请稍等...", string lbl2= "Please Waitting...")
        {
            // 采用Invoke形式进行操作
            this.Invoke(new MethodInvoker(() =>
            {
                this.Text = title;
                this.lbl_tips.Text = lbl1;
                this.lbl_tips_son.Text = lbl2;
            }));
        }
        public void SetJD(string JDStr,string curstr)
        {
            // 采用Invoke形式进行操作
            this.Invoke(new MethodInvoker(() =>
            {
                this.lbl_jd.Text = JDStr;
                this.lbl_cur.Text = curstr;
            }));
        }


        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //主页面代码
            //public static bool flag = true;
            //private void button2_Click(object sender, EventArgs e)
            //{
            //    flag = true;//flag 为false时候，退出执行耗时操作

            //    MultiFuncLoading loadingfrm = new MultiFuncLoading(this);
            //    // 将Loaing窗口，注入到 SplashScreenManager 来管理
            //    SplashScreenManager loading = new SplashScreenManager(loadingfrm);
            //    loading.ShowLoading();
            //    // 设置loadingfrm操作必须在调用ShowLoading之后执行
            //    loadingfrm.SetTxt("多功能Loaidng界面", "拼命加载中...客官耐心等待", "Please Waitting...");
            //    // try catch 包起来，防止出错
            //    try
            //    {
            //        //模拟耗时操作
            //        for (int i = 0; i < 100; i++)
            //        {
            //            Thread.Sleep(100);
            //            loadingfrm.SetJD("当前：" + i + "/总计：100", "当前进度：" + i);
            //            if (!flag) { break;/*用户点击取消执行后，跳出循环*/ }
            //        }

            //    }
            //    catch (Exception) { /*可选处理异常*/ }
            //    finally { loading.CloseWaitForm(); }
            //}
            // 主页面设置一个全局变量


            // 取消时代码
            // 关闭主页面的标识
            // Demo.flag = false;
        }
    }
}
