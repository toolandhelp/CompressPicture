using Aliyun.OSS;
using Aliyun.OSS.Common;
using CompressPicture.DIYTool;
using CompressPicture.OSS;
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
    public partial class ProFileUplToOSS : Form
    {
       // private Comm _comm;
        public ProFileUplToOSS()
        {
            InitializeComponent();
            //==
          //  _comm = new Comm(this.dataGV, this.tssldq,this.tsslhs);
        }


        //oss
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;

        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        static string bucketName = Config.BucketName;

        private readonly string _domain = "http://www.pic.jzbl.com";
        private readonly string _domainOne = "https://www.pic1.jzbl.com/";
        private static string _D = "D:";

        public string ItemImages { get; set; } = "ItemImages";

        //拷贝到的文件夹
        private readonly string _itemfiles = "ItemFiles";
        private readonly string _itemfilescopy = "ItemFiles_Copy";

        private delegate void DelegateWriteMessage(int operate, string itemId, string oldPath, string newPath, int status, string summary);

        private delegate void DelegateCurrent(int count, string itemId = null);
        private delegate void DelegateTimeCost(string times);
        private delegate void DelegateTFileSize(long len);
        private void btnStart_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            btnStart.Enabled = false;

            List<Web_ItemLibrary> allData = new List<Web_ItemLibrary>();

            SimpleLoading loadingfrm = new SimpleLoading(this);
            SplashScreenManager loading = new SplashScreenManager(loadingfrm);
            loading.ShowLoading();
            using (var context = new DataModelEntities())
            {
                allData = context.Web_ItemLibrary
                    .Where(o => !string.IsNullOrEmpty(o.ItemFilePath.ToString()) && o.IsDelete == 0)
                    .OrderByDescending(o => o.CreateDate)
                    .ToList();

                if (allData.Any())
                {
                    loading.CloseWaitForm();
                }
            }


            this.tsslgj.Text = $"共计：{allData.Count}";


            // this.DataProcess(allData, watch);

            Task task = Task.Run(() =>
            {
                this.DataProcess(allData, watch);
            });

            btnStart.Enabled = task.IsCompleted;

        }

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="data">需要处理的数据</param>
        /// <param name="watch">计时器</param>
        private void DataProcess(List<Web_ItemLibrary> data, Stopwatch watch)
        {
            Comm comm = new Comm(this.dataGV, this.tssldq, this.tsslhs);

            InfoStatus infoStatus = new InfoStatus();

            int i = 0;
            long fileLength = 0;

            foreach (var item in data)
            {
                i++;

                string filePath = string.Empty;
                if (!item.ItemFilePath.Contains(_domainOne))
                {
                    filePath = DESEncrypt.Decrypt(item.ItemFilePath);

                    filePath = filePath.Replace(_domain, _D).Replace('/', '\\');
                    filePath = filePath.Split('&').FirstOrDefault().ToLower();

                    //D:\ItemImages\0d54bf62-e42b-460b-aab4-7e6bd1990221\76d408e5-2122-4e0f-b496-2d907186cbf7\6367830614380614892523145.rar
                    filePath = filePath.Replace(_itemfiles.ToLower(), ItemImages.ToLower());

                    if (File.Exists(filePath))
                    {

                        var fileInfo = new System.IO.FileInfo(filePath);
                        //fileLength = fileLength + fileInfo.Length;
                        fileLength += fileInfo.Length;


                        string ossImgPath = filePath
                                   .Replace(ItemImages.ToLower(), _itemfiles.ToLower())
                                   .Remove(0, 3)
                                   .Replace('\\', '/').ToLower();

                        this.OssUpload(ossImgPath, filePath, item.ItemId, ref infoStatus);
                    }
                    else
                    {
                        infoStatus.iStatus = (int)EnumStatus.OldFileUnExists;
                        infoStatus.Summary = "本地文件不存在";
                        //写入日志
                        Console.WriteLine("没有显示");
                    }


                }
                else
                {
                    infoStatus.iStatus = (int)EnumStatus.NewFileExists;
                    infoStatus.Summary = "数据已经传输出[" + item.ItemFilePath+"]";
                    //写入日志
                    Console.WriteLine("没有显示");
                }


                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateWriteMessage(comm.WriteMessage), new object[] { 2, item.ItemId, filePath, infoStatus.OldPath, infoStatus.iStatus, infoStatus.Summary });
                    this.Invoke(new DelegateCurrent(comm.Current), new object[] { i, item.ItemId });
                    this.Invoke(new DelegateTimeCost(comm.TimeCost), new object[] { watch.Elapsed.ToString() });
                    this.Invoke(new DelegateTFileSize(TFileSize), new object[] { fileLength });
                }
                else
                {
                    comm.WriteMessage(2, item.ItemId, filePath, infoStatus.OldPath, infoStatus.iStatus, infoStatus.Summary);
                    comm.Current(i, item.ItemId);
                    comm.TimeCost(watch.Elapsed.ToString());
                    this.TFileSize(fileLength);
                }

            }
        }


        /// <summary>
        /// 总大小
        /// </summary>
        /// <param name="len"></param>
        private void TFileSize(long len)
        {
            this.tsslfileTSize.Text = "文件总大小:" + len;
        }



        /// <summary>
        /// 上传到OSS
        /// </summary>
        /// <param name="ossFilePath">Oss上的图片地址： TempTest/1.jpg</param>
        /// <param name="filePath">D:\test\1.jpg</param>
        /// <param name="itemId">项目ID</param>
        private void OssUpload(string ossFilePath, string filePath, string itemId, ref InfoStatus infoStatus)
        {
            //上传到OSS
            try
            {
                client.PutObject(bucketName, ossFilePath, filePath);
                Console.WriteLine("Put object:{0} succeeded", ossFilePath);
                infoStatus.Summary = "上传成功";
                //修改数据文件
                string imgsrc = _domainOne + ossFilePath
                    .Replace(ItemImages, _itemfiles);
                //文件路径进行加密


                this.DataUpdata(itemId, imgsrc);
                infoStatus.Summary = infoStatus.Summary + ",数据修改成功";
                infoStatus.iStatus = (int)EnumStatus.Ok;
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);

                MessageBox.Show(string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}", ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
                MessageBox.Show(string.Format("Failed with error info:{0}", ex.Message));
            }

        }

        /// <summary>
        /// 当前项目
        /// </summary>
        /// <param name="itemid">项目ID</param>
        /// <param name="fileSrcStr">加密的文件路径</param>
        public void DataUpdata(string itemid, string fileSrcStr)
        {
            try
            {
                using (var context = new DataModelEntities())
                {
                    var itemData = context.Web_ItemLibrary.FirstOrDefault(o => o.ItemId.Equals(itemid));
                    if (itemData != null)
                    {

                        itemData.ItemFilePath = fileSrcStr;

                        context.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
                MessageBox.Show(string.Format("保存数据出错！{0}", ex.Message));
            }
        }





        /// <summary>
        /// 移动图片
        /// </summary>
        //private void FileDataCopy(List<Web_ItemLibrary> data, Stopwatch watch)
        //{
        //    string summary = string.Empty;
        //    string oldPath = string.Empty;
        //    int operate = (int)EnumOperate.File;
        //    string itemId = string.Empty;
        //    string newPath = string.Empty;
        //    int status = 0;


        //    int iExists = 1, iUnExists = 1;

        //    for (int i = 0; i < data.Count; i++)
        //    {
        //        itemId = data[i].ItemId;

        //        string filePath = DESEncrypt.Decrypt(data[i].ItemFilePath);

        //        filePath = filePath.Replace(_domain, _D).Replace('/', '\\');
        //        filePath = filePath.Split('&').FirstOrDefault().ToLower();

        //        //D:\ItemImages\0d54bf62-e42b-460b-aab4-7e6bd1990221\76d408e5-2122-4e0f-b496-2d907186cbf7\6367830614380614892523145.rar
        //        filePath = filePath.Replace("D:".ToLower(), _D.ToLower()).Replace("ItemImages".ToLower(), _itemfiles.ToLower());

        //        if (!File.Exists(filePath))
        //        {
        //            iUnExists++;
        //            oldPath = filePath;
        //            summary = $"路径：{filePath} 状态：< > 文件不存在";
        //            status = (int)EnumStatus.OldFileUnExists;
        //        }
        //        else
        //        {
        //            iExists++;
        //            // summary = $"路径：{filePath} 状态：< > 待复制";
        //            //没有就创建一个基本文件夹

        //            string filePathCopy = filePath.ToLower().Replace(_itemfiles.ToLower(), _itemfilescopy.ToLower());
        //            try
        //            {
        //                //没有文件夹还需要创建
        //                //拷贝原始文件
        //                string fileName = filePathCopy.Split('\\').LastOrDefault();
        //                string basePath = filePathCopy.Replace(fileName, "");
        //                if (!Directory.Exists(basePath))
        //                    Directory.CreateDirectory(basePath);

        //                //判断文件是否存在
        //                if (File.Exists(filePathCopy))
        //                {
        //                    status = (int)EnumStatus.NewFileExists;
        //                    oldPath = filePath;
        //                    newPath = filePathCopy;
        //                    summary = $"拷贝文件：{filePath} 到 {filePathCopy} 状态：》文件已存在《";

        //                }
        //                else
        //                {
        //                    File.Copy(filePath, filePathCopy);

        //                    status = (int)EnumStatus.Ok;
        //                    oldPath = filePath;
        //                    newPath = filePathCopy;
        //                    summary = $"拷贝(压缩图片)文件：{filePath} 到 {filePathCopy} 状态：》成功《";
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                status = (int)EnumStatus.Error;
        //                summary = $"拷贝文件：{filePath} 状态：》失败{ex}";
        //            }


        //        }

        //        if (this.InvokeRequired)
        //        {
        //            this.Invoke(new DelegateWriteMessage(_comm.WriteMessage), new object[] { operate, itemId, oldPath, newPath, status, summary });
        //            this.Invoke(new DelegateCurrent(_comm.Current), new object[] { i });
        //            this.Invoke(new DelegateTimeCost(_comm.TimeCost), new object[] { watch.Elapsed.ToString() });
        //        }
        //        else
        //        {
        //            _comm.WriteMessage(operate, itemId, oldPath, newPath, status, summary);
        //            _comm.Current(i);
        //            _comm.TimeCost(watch.Elapsed.ToString());
        //        }

        //    }


        //    summary = $"共计{data.Count}条，本地存在{iExists}条，不存在{iUnExists}条";


        //    if (this.InvokeRequired)
        //    {
        //        this.Invoke(new DelegateWriteMessage(_comm.WriteMessage), new object[] { 0, "", "", "", 0, summary });
        //    }
        //    else
        //    {
        //        _comm.WriteMessage(operate, itemId, oldPath, newPath, status, summary);
        //    }


        //    if (!watch.IsRunning)
        //    {
        //        watch.Stop();
        //    }



        //}





    }
}
