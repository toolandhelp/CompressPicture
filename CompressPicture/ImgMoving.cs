using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using CompressPicture.DIYTool;

namespace CompressPicture
{
    public partial class ImgMoving : Form
    {
        public ImgMoving()
        {
            InitializeComponent();
        }

        private readonly string _domain = "http://www.pic.jzbl.com";
        private readonly string _G = "G:";

        //拷贝到的文件夹
        private readonly string _itemfiles = "ItemFiles";
        private readonly string _itemfilescopy = "ItemFiles_Copy";

        private readonly string _nS = @"\s\";
        private readonly string _samll = "_small";


        private delegate void DelegateWriteMessage(string message);

        //private void btnGetData_Click(object sender, EventArgs e)
        //{

        //    using (var context = new DataModelEntities())
        //    {
        //        var data = context.Web_ItemLibrary
        //            .Where(o => o.IsDelete.ToString().Equals("0"))
        //            .OrderByDescending(o => o.CreateDate)
        //            .ToList();

        //        MessageBox.Show($"DataCount:{data.Count()}");
        //        Debug.WriteLine("DataCount:" + data.Count());

        //    }

        //}

        private void btnStart_Click(object sender, EventArgs e)
        {


            SimpleLoading loadingfrm = new SimpleLoading(this);
            //将Loaing窗口，注入到 SplashScreenManager 来管理
            SplashScreenManager loading = new SplashScreenManager(loadingfrm);
            loading.ShowLoading();
            using (var context = new DataModelEntities())
            {
                var data = context.Web_ItemLibrary
                .Where(o => o.IsDelete.ToString().Equals("0"))
                .OrderByDescending(o => o.CreateDate)
                .ToList();

                if (data.Any())
                {
                    loading.CloseWaitForm();
                }
            }

          
            return;


            Task task = Task.Run(() =>
            {
                TitleImgCopy();
            });

            btnStart.Enabled = task.IsCompleted;

        }


        /// <summary>
        /// 移动图片
        /// </summary>
        private void TitleImgCopy()
        {
            using (var context = new DataModelEntities())
            {
                string message = "开始获取数据,请等待…";

                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateWriteMessage(WriteMessage), new object[] { message });
                }
                else
                {
                    this.WriteMessage(message);
                }

                SimpleLoading loadingfrm = new SimpleLoading(this);
                //将Loaing窗口，注入到 SplashScreenManager 来管理
                SplashScreenManager loading = new SplashScreenManager(loadingfrm);
                loading.ShowLoading();

                var data = context.Web_ItemLibrary
                    .Where(o => o.IsDelete.ToString().Equals("0"))
                    .OrderByDescending(o => o.CreateDate)
                    .ToList();

              

                int iExists = 1,iUnExists=1;
              

                for (int i = 0; i < data.Count; i++)
                {
                    string filePath = data[i].ItemTitleImg.Replace(_domain, _G).Replace('/', '\\');

                    if (!File.Exists(filePath))
                    {
                        iUnExists++;
                        message = $"路径：{filePath} 状态：< > 文件不存在";
                    }
                    else
                    {
                        iExists++;
                        message = $"路径：{filePath} 状态：< > 待复制";
                        //没有就创建一个基本文件夹

                        string filePathCopy = filePath.Replace(_itemfiles, _itemfilescopy);
                        try
                        {
                            //没有文件夹还需要创建
                            //拷贝原始文件
                            string fileName=  filePathCopy.Split('\\').LastOrDefault();
                            string basePath = filePathCopy.Replace(fileName, "");
                            if (!Directory.Exists(basePath))
                                Directory.CreateDirectory(basePath);

                            //判断文件是否存在
                            if (File.Exists(filePathCopy))
                            {
                                message = $"拷贝(新路径原图)文件：{filePath} 到 {filePathCopy} 状态：》文件以存在《";
                            }
                            else
                            {
                                File.Copy(filePath, filePathCopy);
                                message = $"拷贝(压缩图片)文件：{filePath} 到 {filePathCopy} 状态：》成功《";
                            }

                            if (filePath.Contains(_samll))
                            {
                                if (filePath.Contains(_nS))
                                {
                                    string nSPath = filePath.Replace(_nS, @"\").Replace(_samll, "");
                                    string nSfilePath = filePathCopy.Replace(_nS, @"\").Replace(_samll, "");

                                    //判断文件是否存在
                                    if (File.Exists(nSfilePath))
                                    {
                                        message = $"拷贝(新路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》文件以存在《";
                                    }
                                    else
                                    {
                                        File.Copy(nSPath, nSfilePath);
                                        message = $"拷贝(新路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》成功《";
                                    }
                                }
                                else
                                {
                                    string nSPath = filePath.Replace(_samll, "");
                                    string nSfilePath = filePathCopy.Replace(_samll, "");
                                    //判断文件是否存在
                                    if (File.Exists(nSfilePath))
                                    {
                                        message = $"拷贝(老路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》文件以存在《";
                                    }
                                    else
                                    {
                                        File.Copy(nSPath, nSfilePath);
                                        message = $"拷贝(老路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》成功《";
                                    }
                                }
                            }
                            else
                            {
                                message = $"封面路径：{filePath} 不包含_small 状态：》未知《";
                            }

                        }
                        catch (Exception ex)
                        {
                            message = $"拷贝文件：{filePath} 状态：》失败{ex}";
                        }
                        

                    }
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new DelegateWriteMessage(WriteMessage), new object[] { message });
                    }
                    else
                    {
                        this.WriteMessage(message);
                    }
                 
                }


                message = $"共计{data.Count}条，本地存在{iExists}条，不存在{iUnExists}条";


                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateWriteMessage(WriteMessage), new object[] { message });
                }
                else
                {
                    this.WriteMessage(message);
                }





            }


        }



        /// <summary>
        /// 日志输出
        /// </summary>
        /// <param name="message"></param>
        private void WriteMessage(string message)
        {
            txtImgPathInfos.AppendText(message + Environment.NewLine);
        }

    }


}
