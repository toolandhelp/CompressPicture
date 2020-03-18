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
    public partial class FileMoving : Form
    {
        private Comm _comm;
        public FileMoving()
        {
            InitializeComponent();
            //==
            _comm = new Comm(this.dataGV, this.tssldq,this.tsslhs);
        }


        private readonly string _domain = "http://www.pic.jzbl.com";
        private string _G = "G:";

        //拷贝到的文件夹
        private readonly string _itemfiles = "ItemFiles";
        private readonly string _itemfilescopy = "ItemFiles_Copy";

        private delegate void DelegateWriteMessage(int operate, string itemId, string oldPath, string newPath, int status, string summary);

        private delegate void DelegateCurrent(int count, string itemId = null);
        private delegate void DelegateTimeCost(string times);
        private void btnStart_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            btnStart.Enabled = false;
            txtTarget.Enabled = false;
            btnTarget.Enabled = false;

            List<Web_ItemLibrary> allData = new List<Web_ItemLibrary>();

            SimpleLoading loadingfrm = new SimpleLoading(this);
            SplashScreenManager loading = new SplashScreenManager(loadingfrm);
            loading.ShowLoading();
            using (var context = new DataModelEntities())
            {
                allData = context.Web_ItemLibrary
                    .Where(o => !string.IsNullOrEmpty(o.ItemFilePath.ToString()) && o.IsDelete == 0)
                    .OrderBy(o => o.CreateDate)
                    .ToList();

                if (allData.Any())
                {
                    loading.CloseWaitForm();
                }
            }


            this.tsslgj.Text = $"共计：{allData.Count}";

            Task task = Task.Run(() =>
            {
                this.FileDataCopy(allData,watch);
            });

            btnStart.Enabled = task.IsCompleted;
            btnTarget.Enabled = task.IsCompleted;
            txtTarget.Enabled = task.IsCompleted;

        }


        /// <summary>
        /// 移动图片
        /// </summary>
        private void FileDataCopy(List<Web_ItemLibrary> data, Stopwatch watch)
        {
            _G = txtTarget.Text.TrimEnd('\\');

            string summary = string.Empty;
            string oldPath = string.Empty;
            int operate = (int)EnumOperate.File;
            string itemId = string.Empty;
            string newPath = string.Empty;
            int status = 0;


            int iExists = 1, iUnExists = 1;

            for (int i = 0; i < data.Count; i++)
            {
                itemId = data[i].ItemId;

                string filePath = DESEncrypt.Decrypt(data[i].ItemFilePath);

                filePath = filePath.Replace(_domain, _G).Replace('/', '\\');
                filePath = filePath.Split('&').FirstOrDefault().ToLower();

                //D:\ItemImages\0d54bf62-e42b-460b-aab4-7e6bd1990221\76d408e5-2122-4e0f-b496-2d907186cbf7\6367830614380614892523145.rar
                filePath = filePath.Replace("D:".ToLower(), _G.ToLower()).Replace("ItemImages".ToLower(), _itemfiles.ToLower());

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

                    string filePathCopy = filePath.ToLower().Replace(_itemfiles.ToLower(), _itemfilescopy.ToLower());
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
                            summary = $"拷贝文件：{filePath} 到 {filePathCopy} 状态：》文件已存在《";

                        }
                        else
                        {
                            File.Copy(filePath, filePathCopy);

                            status = (int)EnumStatus.Ok;
                            oldPath = filePath;
                            newPath = filePathCopy;
                            summary = $"拷贝(压缩图片)文件：{filePath} 到 {filePathCopy} 状态：》成功《";
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
                    this.Invoke(new DelegateWriteMessage(_comm.WriteMessage), new object[] { operate, itemId, oldPath, newPath, status, summary });
                    this.Invoke(new DelegateCurrent(_comm.Current), new object[] { i });
                    this.Invoke(new DelegateTimeCost(_comm.TimeCost), new object[] { watch.Elapsed.ToString() });
                }
                else
                {
                    _comm.WriteMessage(operate, itemId, oldPath, newPath, status, summary);
                    _comm.Current(i);
                    _comm.TimeCost(watch.Elapsed.ToString());
                }

            }


            summary = $"共计{data.Count}条，本地存在{iExists}条，不存在{iUnExists}条";


            if (this.InvokeRequired)
            {
                this.Invoke(new DelegateWriteMessage(_comm.WriteMessage), new object[] { 0, "", "", "", 0, summary });
            }
            else
            {
                _comm.WriteMessage(operate, itemId, oldPath, newPath, status, summary);
            }


            if (!watch.IsRunning)
            {
                watch.Stop();
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
