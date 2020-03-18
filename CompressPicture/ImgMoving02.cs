using CompressPicture.DIYTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompressPicture
{
    public partial class ImgMoving02 : Form
    {
        public ImgMoving02()
        {
            InitializeComponent();
        }


        private readonly string _domain = "http://www.pic.jzbl.com";
        private static string _G = "G:";

        //拷贝到的文件夹
        private readonly string _itemfiles = "ItemFiles";
        private readonly string _itemfilescopy = "ItemFiles_Copy";

        private readonly string _nS = @"\s\";
        private readonly string _samll = "_small";




        private delegate void DelegateLoad(ref List<Web_ItemLibrary> allData);
        private delegate void DelegateWriteMessage(int operate, string itemId, string oldPath, string newPath, int status, string summary);

        private delegate void DelegateCurrent(int count, string itemId = null);
        private delegate void DelegateTimeCost(string times);


        private List<Web_ItemLibrary> _allData = null;

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            txtTarget.Enabled = false;
            btnTarget.Enabled = false;

            Stopwatch watch = new Stopwatch();
          
            watch.Start();


            List<Web_ItemLibrary> allData = new List<Web_ItemLibrary>();

            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new DelegateLoad(Load),new object[] {allData });
            //   // this.Invoke(new DelegateWriteMessage(WriteMessage), new object[] { 0, "", "", "", 0, summary });
            //}
            //else
            //{

            

            Task taskData = Task.Run(() =>
            {
                this.LoadData(ref allData);
            });

            //}
            //页面还是会卡在那里
            taskData.Wait();

            this.tsslgj.Text = $"共计：{allData.Count}";


            if (taskData.IsCompleted && allData.Any())
            {

                Task task = Task.Run(() =>
                 {
                     TitleImgCopy(allData,watch);
                 });

                btnStart.Enabled = task.IsCompleted;
                btnTarget.Enabled = task.IsCompleted;
                txtTarget.Enabled = task.IsCompleted;
            }
            else
            {
                MessageBox.Show("d=====(￣▽￣*)b");
            }

         
            #region 22


            //MethodInvoker invoker = new MethodInvoker(LoadData);

            //invoker.BeginInvoke(null, null);

            //if (_allData != null && _allData.Any())
            //{
            //    Task task = Task.Run(() =>
            //    {
            //        TitleImgCopy(_allData);
            //    });

            //    btnStart.Enabled = task.IsCompleted;
            //}
            //else
            //{
            //    MessageBox.Show("d=====(￣▽￣*)b");
            //}
            #endregion
        }


        /// <summary>
        /// 移动图片
        /// </summary>
        private void TitleImgCopy(List<Web_ItemLibrary> data, Stopwatch watch)
        {
            _G = txtTarget.Text.TrimEnd('\\');
            Comm comm = new Comm(this.dataGV,this.tssldq,this.tsslhs);

            string summary = string.Empty;
            string oldPath = string.Empty;
            int operate = (int)EnumOperate.TitleImg;
            string itemId = string.Empty;
            string newPath = string.Empty;
            int status = 0;


            int iExists = 1, iUnExists = 1;

            for (int i = 0; i < data.Count; i++)
            {
                itemId = data[i].ItemId;

                string filePath = data[i].ItemTitleImg.Replace(_domain, _G).Replace('/', '\\').ToLower();

                if (!File.Exists(filePath))
                {
                    iUnExists++;
                    oldPath = filePath;
                    summary = $"路径：{filePath} 状态：< > 文件不存在";
                    status = (int)EnumStatus.OldFileUnExists;
                }
                else
                {
                    iExists++;
                    // summary = $"路径：{filePath} 状态：< > 待复制";
                    //没有就创建一个基本文件夹

                    string filePathCopy = filePath.Replace(_itemfiles.ToLower(), _itemfilescopy.ToLower());
                    try
                    {
                        //没有文件夹还需要创建
                        //拷贝原始文件
                        string fileName = filePathCopy.Split('\\').LastOrDefault();
                        string basePath = filePathCopy.Replace(fileName, "");
                        if (!Directory.Exists(basePath))
                            Directory.CreateDirectory(basePath);

                        //判断文件是否存在
                        if (File.Exists(filePathCopy))
                        {
                            status = (int)EnumStatus.NewFileExists;
                            oldPath = filePath;
                            newPath = filePathCopy;
                            summary = $"拷贝(新路径原图)文件：{filePath} 到 {filePathCopy} 状态：》文件已存在《";

                        }
                        else
                        {
                            File.Copy(filePath, filePathCopy);

                            status = (int)EnumStatus.Ok;
                            oldPath = filePath;
                            newPath = filePathCopy;
                            summary = $"拷贝(压缩图片)文件：{filePath} 到 {filePathCopy} 状态：》成功《";
                        }

                        //判断图片是否带_samll
                        if (filePath.Contains(_samll))
                        {
                            //判断图片是否带/i/s/
                            if (filePath.Contains(_nS))
                            {
                                string nSPath = filePath.Replace(_nS, @"\").Replace(_samll, "");
                                string nSfilePath = filePathCopy.Replace(_nS, @"\").Replace(_samll, "");

                                //判断文件是否存在
                                if (File.Exists(nSfilePath))
                                {
                                    status = (int)EnumStatus.NewFileExists;
                                    oldPath = nSPath;
                                    newPath = nSfilePath;
                                    summary = $"拷贝(新路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》文件已存在《";
                                }
                                else
                                {
                                    File.Copy(nSPath, nSfilePath);

                                    status = (int)EnumStatus.Ok;
                                    oldPath = nSPath;
                                    newPath = nSfilePath;
                                    summary = $"拷贝(新路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》成功《";
                                }
                            }
                            else
                            {
                                string nSPath = filePath.Replace(_samll, "");
                                string nSfilePath = filePathCopy.Replace(_samll, "");
                                //判断文件是否存在
                                if (File.Exists(nSfilePath))
                                {
                                    status = (int)EnumStatus.NewFileExists;
                                    oldPath = nSPath;
                                    newPath = nSfilePath;
                                    summary = $"拷贝(老路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》文件以存在《";
                                }
                                else
                                {
                                    File.Copy(nSPath, nSfilePath);

                                    status = (int)EnumStatus.Ok;
                                    oldPath = nSPath;
                                    newPath = nSfilePath;
                                    summary = $"拷贝(老路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》成功《";
                                }
                            }
                        }
                        else
                        {
                            status = (int)EnumStatus.Unknown;
                            summary = $"封面路径：{filePath} 不包含_small 状态：》未知《";
                        }

                    }
                    catch (Exception ex)
                    {
                        status = (int)EnumStatus.Error;
                        summary = $"拷贝文件：{filePath} 状态：》失败{ex}";
                    }


                }

                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateWriteMessage(comm.WriteMessage), new object[] { operate, itemId, oldPath, newPath, status, summary });
                    this.Invoke(new DelegateCurrent(comm.Current), new object[] { i });
                    this.Invoke(new DelegateTimeCost(comm.TimeCost), new object[] { watch.Elapsed.ToString() });
                }
                else
                {
                    comm.WriteMessage(operate, itemId, oldPath, newPath, status, summary);
                    comm.Current(i);
                    comm.TimeCost(watch.Elapsed.ToString());
                }

            }


            summary = $"共计{data.Count}条，本地存在{iExists}条，不存在{iUnExists}条";


            if (this.InvokeRequired)
            {
                this.Invoke(new DelegateWriteMessage(comm.WriteMessage), new object[] { 0, "", "", "", 0, summary });
            }
            else
            {
                comm.WriteMessage(operate, itemId, oldPath, newPath, status, summary);
            }


            if (!watch.IsRunning)
            {
                watch.Stop();
            }


        }



      

        /// <summary>
        ///  显示 load 返回数据
        /// </summary>
        /// <param name="form"></param>

        private void LoadData(ref List<Web_ItemLibrary> allData)
        {
           // List<Web_ItemLibrary> allData = new List<Web_ItemLibrary>();

            SimpleLoading loadingfrm = new SimpleLoading(this);
            //将Loaing窗口，注入到 SplashScreenManager 来管理
            SplashScreenManager loading = new SplashScreenManager(loadingfrm);
            loading.ShowLoading();
            using (var context = new DataModelEntities())
            {
                allData = context.Web_ItemLibrary
                    .Where(o => o.IsDelete.ToString().Equals("0"))
                    .OrderByDescending(o => o.CreateDate)
                    .ToList();

                if (allData.Any())
                {
                    loading.CloseWaitForm();
                }
            }

         //   return allData;
        }

        /// <summary>
        ///  显示 load 返回数据
        /// </summary>
        /// <param name="form"></param>

        private void LoadData()
        {
            // List<Web_ItemLibrary> allData = new List<Web_ItemLibrary>();

            SimpleLoading loadingfrm = new SimpleLoading(this);
            //将Loaing窗口，注入到 SplashScreenManager 来管理
            SplashScreenManager loading = new SplashScreenManager(loadingfrm);
            loading.ShowLoading();
            using (var context = new DataModelEntities())
            {
                _allData = context.Web_ItemLibrary
                .Where(o => o.IsDelete.ToString().Equals("0"))
                .OrderByDescending(o => o.CreateDate)
                .ToList();

                if (_allData.Any())
                {
                    loading.CloseWaitForm();
                }
            }

            //   return allData;
        }

        private void btnTarget_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择开始盘符";

            if (dialog.ShowDialog() == DialogResult.OK)
            {

                txtTarget.Text = dialog.SelectedPath.Split(':').FirstOrDefault() + ":\\";


                DirectoryInfo folder = new DirectoryInfo(txtTarget.Text);
                var dics = folder.GetDirectories();

                bool isHuav = false;

                foreach (var item in dics)
                {
                    if (item.Name.ToUpper().Equals(_itemfiles.ToUpper()))
                    {
                        isHuav = true;
                        break;
                    }
                }

                if (!isHuav)
                {
                    MessageBox.Show($"当前盘符{txtTarget.Text}里没有[{_itemfiles}]文件夹");
                }
               
            }
        }
     
    }
}
