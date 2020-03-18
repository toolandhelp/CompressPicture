using CompressPicture.DIYTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompressPicture
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //private void btnImgMoving_Click(object sender, EventArgs e)
        //{
        //    ImgMoving from = new ImgMoving();
        //    from.Show();
        //}

        private void btnComPic_Click(object sender, EventArgs e)
        {
            MessageBox.Show("开发中...");
        }
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void tsb02_Click(object sender, EventArgs e)
        {
            ImgMoving02 form = new ImgMoving02();
            form.Show();
        }


        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbMovFile_Click(object sender, EventArgs e)
        {
            FileMoving form = new FileMoving();
            form.Show();
        }

        private void tsbMovContentImg_Click(object sender, EventArgs e)
        {
            ContentImgMoving form = new ContentImgMoving();
            form.Show();

        }

        private void tsbProPicUplToOSS_Click(object sender, EventArgs e)
        {
            ProPicUplToOSS form = new ProPicUplToOSS();
            form.Text = sender.ToString();
            form.Show();
        }

        private void tsbProTitlePicUplToOSS_Click(object sender, EventArgs e)
        {
            ProTitlePicUplToOSS form = new ProTitlePicUplToOSS();
            form.Text = sender.ToString();
            form.Show();
        }

        private void tsbProFileUplToOSS_Click(object sender, EventArgs e)
        {
            ProFileUplToOSS form = new ProFileUplToOSS();
            form.Text = sender.ToString();
            form.Show();
        }
    }
}
