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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompressPicture
{
    public partial class ContentImgMoving : Form
    {
        private Comm _comm;
        public ContentImgMoving()
        {
            InitializeComponent();
          //==
            _comm = new Comm(this.dataGV,this.tssldq,this.tsslhs);
        }

        private readonly string _domain = "http://www.pic.jzbl.com";
        private string _G = "G:";

        //拷贝到的文件夹
        private readonly string _itemfiles = "ItemFiles";
        private readonly string _itemfilescopy = "ItemFiles_Copy";


        private readonly string _nS = @"\s\";
        private readonly string _samll = "_small";

        int gjxm = 0, yxxm = 0;

        private delegate void DelegateWriteMessage(int operate, string itemId, string oldPath, string newPath, int status, string summary);
        private delegate void DelegateCurrent(int count, string itemId = null);
        private delegate void DelegateTimeCost(string times);

        private void btnStart_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            this.btnStart.Enabled = false;
            this.cb01.Enabled = false;
            this.cb02.Enabled = false;
            this.cb03.Enabled = false;
            txtTarget.Enabled = false;
            btnTarget.Enabled = false;

            List <Web_ItemLibrary> allData = new List<Web_ItemLibrary>();

            SimpleLoading loadingfrm = new SimpleLoading(this);
            SplashScreenManager loading = new SplashScreenManager(loadingfrm);
            loading.ShowLoading();
            using (var context = new DataModelEntities())
            {
                //排除
                allData = context.Web_ItemLibrary
                    .Where(o => o.IsDelete == 0 && !string.IsNullOrEmpty(o.ItemContentBefore))
                    // .Take(50)
                    .OrderByDescending(o => o.CreateDate)
                    .ToList();

                if (allData.Any())
                {
                    loading.CloseWaitForm();
                }
            }
            gjxm = allData.Count;

            this.tsslgyxm.Text = $"共计项目：{gjxm}，有效项目{0}";
            this.tsslgj.Text = 0.ToString();

            Task task = Task.Run(() =>
            {
                this.ContentImgDataCopy(allData,watch);
            });

            this.btnStart.Enabled = task.IsCompleted;
            this.cb01.Enabled = task.IsCompleted;
            this.cb02.Enabled = task.IsCompleted;
            this.cb03.Enabled = task.IsCompleted;
            btnTarget.Enabled = task.IsCompleted;
            txtTarget.Enabled = task.IsCompleted;
        }


        private void ContentImgDataCopy(List<Web_ItemLibrary> data, Stopwatch watch)
        {
            _G = txtTarget.Text.TrimEnd('\\');

            string summary = string.Empty;
            string oldPath = string.Empty;
            int operate = (int)EnumOperate.Content;
            string itemId = string.Empty;
            string newPath = string.Empty;
            int status = 0;


           // int iExists = 1, iUnExists = 1;
            try
            {

                for (int i = 0; i < data.Count; i++)
                {
                    itemId = data[i].ItemId;

                    string content = data[i].ItemContentBefore;

                    if (!string.IsNullOrEmpty(content))
                    {
                        string[] imgDatas = ContentHelper.GetImgsByContentBefore(content);

                        if (imgDatas.Length > 0 && imgDatas != null)
                        {
                            yxxm++;
                            this.tsslgyxm.Text = $"共计项目：{gjxm}，有效项目{yxxm}";

                            this.tsslgj.Text = (int.Parse(this.tsslgj.Text)+ imgDatas.Length).ToString();

                            for (int j = 0; j < imgDatas.Length; j++)
                            {
                                string filePath = imgDatas[j].Replace(_domain, _G).Replace('/', '\\').ToLower();

                                string filePathCopy = filePath.Replace(_itemfiles.ToLower(), _itemfilescopy.ToLower());

                                //判断文件是否存在
                                if (!File.Exists(filePath))
                                {
                                    oldPath = filePath;
                                    summary = $"路径：{filePath} 状态：< > 源文件不存在";
                                    status = (int)EnumStatus.OldFileUnExists;
                                }
                                else
                                {
                                    //判断文件是否存在
                                    if (File.Exists(filePathCopy))
                                    {
                                        status = (int)EnumStatus.NewFileExists;
                                        oldPath = filePath;
                                        newPath = filePathCopy;
                                        summary = $"拷贝(src 压缩图片)文件：{filePath} 到 {filePathCopy} 状态：》文件已存在《";
                                    }
                                    else
                                    {

                                        string tfileName = filePathCopy.Split('\\').LastOrDefault();
                                        string tbasePath = filePathCopy.Replace(tfileName, "");

                                        if (!Directory.Exists(tbasePath))
                                            Directory.CreateDirectory(tbasePath);

                                        //处理掉SRC上的图片
                                        File.Copy(filePath, filePathCopy);

                                        status = (int)EnumStatus.Ok;
                                        oldPath = filePath;
                                        newPath = filePathCopy;
                                        summary = $"拷贝src 上的图片：{filePath} 到 {filePathCopy} 状态：》成功《";

                                        //再针对SRC上的图片进行拷贝
                                        if (filePath.Contains(_samll))  //判断图片是否带_smaill （最开始那一批）
                                        {
                                            //计算原图
                                            string nSPath = filePath.Replace(_samll, "").Replace(_nS,"\\");
                                            string nSfilePath = filePathCopy.Replace(_samll, "").Replace(_nS, "\\"); 

                                            if (File.Exists(nSfilePath))
                                            {
                                                status = (int)EnumStatus.NewFileExists;
                                                oldPath = nSPath;
                                                newPath = nSfilePath;
                                                summary = $"拷贝(新路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》文件已存在1《";
                                            }
                                            else
                                            {
                                                string fileName = nSfilePath.Split('\\').LastOrDefault();
                                                string basePath = nSfilePath.Replace(fileName, "");

                                                if (!Directory.Exists(basePath))
                                                    Directory.CreateDirectory(basePath);

                                                File.Copy(nSPath, nSfilePath);

                                                status = (int)EnumStatus.Ok;
                                                oldPath = nSPath;
                                                newPath = nSfilePath;
                                                summary = $"拷贝(新路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》成功《";
                                            }
                                        }
                                        else if (filePath.Contains(_nS))  //判断图片是否带 /s/  （第二批）
                                        {
                                            //计算原图
                                            string nSPath = filePath.Replace(_nS, "\\");
                                            string nSfilePath = filePathCopy.Replace(_nS, "\\");

                                            if (File.Exists(nSfilePath))
                                            {
                                                status = (int)EnumStatus.NewFileExists;
                                                oldPath = nSPath;
                                                newPath = nSfilePath;
                                                summary = $"拷贝(新路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》文件已存在2《";
                                            }
                                            else
                                            {
                                                string fileName = nSfilePath.Split('\\').LastOrDefault();
                                                string basePath = nSfilePath.Replace(fileName, "");

                                                if (!Directory.Exists(basePath))
                                                    Directory.CreateDirectory(basePath);

                                                File.Copy(nSPath, nSfilePath);

                                                status = (int)EnumStatus.Ok;
                                                oldPath = nSPath;
                                                newPath = nSfilePath;
                                                summary = $"拷贝(新路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》成功《";
                                            }

                                            // itemFiles/8e5dc0f7-ed86-4d3c-89db-3a8593f6a2d7/2019-12-19/7ccc9e54-69e9-487d-a8fb-fd3ca2610cc9/i/Ori_4e9ef95d1576763073.jpg
                                            nSPath = nSPath.Replace(nSPath.Split('\\').LastOrDefault(),"Ori_" + nSPath.Split('\\').LastOrDefault());
                                            nSfilePath= nSfilePath.Replace(nSfilePath.Split('\\').LastOrDefault(), "Ori_" + nSfilePath.Split('\\').LastOrDefault());

                                            if (File.Exists(nSPath) && !File.Exists(nSfilePath))
                                            {
                                                string fileName = nSfilePath.Split('\\').LastOrDefault();
                                                string basePath = nSfilePath.Replace(fileName, "");

                                                if (!Directory.Exists(basePath))
                                                    Directory.CreateDirectory(basePath);

                                                File.Copy(nSPath, nSfilePath);

                                                status = (int)EnumStatus.Ok;
                                                oldPath = nSPath;
                                                newPath = nSfilePath;
                                                summary = $"拷贝(新路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》成功《";
                                            }
                                            else if (File.Exists(nSfilePath))
                                            {
                                                status = (int)EnumStatus.NewFileExists;
                                                oldPath = nSPath;
                                                newPath = nSfilePath;
                                                summary = $"拷贝(新路径原图)文件：{nSPath} 到 {nSfilePath} 状态：》文件已存在3《";
                                            }


                                        }

                                        //对 filePathCopy 的图片进行处理
                                        //1.等比例压缩    xx_dblys.jpg xx_等比例压缩.jpg
                                        if (cb01.Checked)
                                        {

                                        }
                                        //2.压缩一把
                                        if (cb02.Checked)
                                        {
                                            CompressImg.PicCompress(filePathCopy);
                                        }

                                        //3.打水印 生成新图  xx_yssy.jpg xx_压缩水印.jpg
                                        if (cb03.Checked)
                                        {

                                        }



                                    }


                                }


                                //数据写入
                                if (this.InvokeRequired)
                                {
                                    this.Invoke(new DelegateWriteMessage(_comm.WriteMessage), new object[] { operate, itemId, oldPath, newPath, status, summary });
                                    this.Invoke(new DelegateCurrent(_comm.Current), new object[] { i, itemId.ToString() });
                                    this.Invoke(new DelegateTimeCost(_comm.TimeCost), new object[] { watch.Elapsed.ToString() });
                                }
                                else
                                {
                                    _comm.WriteMessage(operate, itemId, oldPath, newPath, status, summary);
                                    _comm.Current(i, "");
                                    _comm.TimeCost(watch.Elapsed.ToString());
                                }

                            }

                        }

                    }
                    else
                    {
                        status = 4;
                        summary = "内容为空";

                    }

                


                }


                //summary = $"共计{data.Count}条，本地存在{iExists}条，不存在{iUnExists}条";


                //if (this.InvokeRequired)
                //{
                //    this.Invoke(new DelegateWriteMessage(_comm.WriteMessage), new object[] { 0, "", "", "", 0, summary });
                //}
                //else
                //{
                //    _comm.WriteMessage(operate, itemId, oldPath, newPath, status, summary);
                //}

                if (!watch.IsRunning)
                {
                    watch.Stop();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(" 错误："+ex);
            }
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
