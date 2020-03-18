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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompressPicture
{
    public partial class ProTitlePicUplToOSS : Form
    {
        public ProTitlePicUplToOSS()
        {
            InitializeComponent();
        }


        private readonly string _domain = "https://www.pic.jzbl.com";
        private readonly string _domainOne = "https://www.pic1.jzbl.com/";
        private static string _D = "D:";

        //拷贝到的文件夹
        private readonly string _itemfiles = "ItemFiles";

        public string ItemImages { get; set; } = "ItemImages";

        private readonly string _nS = @"/s/";
        private readonly string _samll = "_small";

        //oss
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;

        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);

        static string bucketName = Config.BucketName;

        //
        private delegate void DelegateLoad(ref List<Web_ItemLibrary> allData);
        private delegate void DelegateWriteMessage(int operate, string itemId, string oldPath, string newPath, int status, string summary);

        private delegate void DelegateCurrent(int count, string itemId = null);
        private delegate void DelegateTimeCost(string times);


        private List<Web_ItemLibrary> _allData = null;

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;

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
                // TitleImgUpLoad(allData, watch);

                Task task = Task.Run(() =>
                 {
                     TitleImgUpLoad(allData, watch);
                 });

                // btnStart.Enabled = task.IsCompleted;
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




        //1,将图片上传 （只上传大图【原图】）
        //  1.判断本地图片是否存在
        //  2.判断服务器文件是否存在

        /// <summary>
        /// 封面图片上传
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="watch"></param>
        private void TitleImgUpLoad(List<Web_ItemLibrary> data, Stopwatch watch)
        {
            Comm comm = new Comm(this.dataGV, this.tssldq, this.tsslhs);

            int i = 0;

            InfoStatus infoStatus = new InfoStatus();

            foreach (var item in data)
            {
                i++;

                if (!string.IsNullOrEmpty(item.ItemTitleImg))
                {
                    //https://www.pic.jzbl.com/ItemFiles/d5b4fbaa-40a0-4145-a119-88af91c3bf8f/2019-11-28/5a4962f9-a138-4db7-b327-bd25ed7b6023/i/s/1574935666_small.jpg
                    //https://www.pic.jzbl.com/ItemFiles/8b166b7c-d1cf-47b2-806a-3a81ee78b6e3/91fbcff0-304c-4fa6-b3d7-3f6662cd609a/6368089569228158431363684_small.jpg
                    string filePath = item.ItemTitleImg.Replace(_domain, _D)
                        .Replace(_itemfiles, ItemImages);

                    //if (!File.Exists(filePath))
                    //{
                    //    continue;
                    //}

                    string imgPath = string.Empty;
                    try
                    {
                        imgPath = filePath;

                        //判断 图片是否带/i/s/ 并且 图片是否带_samll 
                        if (filePath.Contains(_nS))
                        {
                            imgPath = filePath.Replace(_nS, @"/");
                        }
                        // 否则就全是
                        imgPath = imgPath.Replace(_samll, "").Replace('/','\\');
                       
                        //判断文件是否存在  https://www.pic.jzbl.com/ItemFiles/8b166b7c-d1cf-47b2-806a-3a81ee78b6e3/91fbcff0-304c-4fa6-b3d7-3f6662cd609a/6368089569228158431363684_small.jpg
                        if (File.Exists(imgPath))
                        {
                            string ossImgPath = imgPath
                                .Replace(_D, "")
                                .Replace(ItemImages, _itemfiles).Remove(0,1)
                                .Replace('\\','/').ToLower();
                            //进行上传操作
                            this.OssUpload(ossImgPath, imgPath, item.ItemId,ref infoStatus);
                        }
                        else
                        {
                            infoStatus.Summary = "本地文件不存在";
                            //写入日志
                            Console.WriteLine("没有显示");
                        }


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                    //信息输出

                    if (this.InvokeRequired)
                    {
                        this.Invoke(new DelegateWriteMessage(comm.WriteMessage), new object[] { 1, item.ItemId, item.ItemTitleImg, imgPath, infoStatus.iStatus, infoStatus.Summary });
                        this.Invoke(new DelegateCurrent(comm.Current), new object[] { i, item.ItemId });

                        this.Invoke(new DelegateTimeCost(comm.TimeCost), new object[] { watch.Elapsed.ToString() });
                    }
                    else
                    {
                        comm.WriteMessage(1, item.ItemId, item.ItemTitleImg, imgPath, infoStatus.iStatus, infoStatus.Summary);
                        comm.Current(i, item.ItemId);
                        comm.TimeCost(watch.Elapsed.ToString());
                    }



                }

            }



        }


        /// <summary>
        /// 上传到OSS
        /// </summary>
        /// <param name="ossFilePath">Oss上的图片地址： TempTest/1.jpg</param>
        /// <param name="filePath">D:\test\1.jpg</param>
        /// <param name="itemId">项目ID</param>
        private void OssUpload(string ossFilePath,string filePath,string itemId,ref InfoStatus infoStatus )
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

                this.DataUpdata(itemId, imgsrc);
                infoStatus.Summary = infoStatus.Summary + ",数据修改成功";
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

        //2,修改图片保存地址

        const string imgResize = "?x-oss-process=image/resize,m_fixed,h_400,w_520/quality,q_80";

        /// <summary>
        /// 当前项目
        /// </summary>
        /// <param name="itemid">项目ID</param>
        /// <param name="imgSrc">图片路径</param>
        public void DataUpdata(string itemid,string imgSrc)
        {
            try
            {
                using (var context = new DataModelEntities())
                {
                    var itemData = context.Web_ItemLibrary.FirstOrDefault(o => o.ItemId.Equals(itemid));
                    if (itemData != null)
                    {

                        itemData.ItemTitleImg = imgSrc + imgResize;

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
                    .Where(o => o.IsDelete.ToString().Equals("0")&&!o.ItemTitleImg.Contains("http://www.pic1.jzbl.com/"))
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



    }
}
