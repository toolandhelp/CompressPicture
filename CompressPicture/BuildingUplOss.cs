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
    public partial class BuildingUplOss : Form
    {
        private Comm _comm;
        public BuildingUplOss()
        {
            InitializeComponent();

            _comm = new Comm(this.dataGV, this.tssldq, this.tsslhs);
        }


        //oss
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;

        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        static string bucketName = Config.BucketName;


        private readonly string _domain = "https://www.pic.jzbl.com";
        private readonly string _domainOne = "https://www.pic1.jzbl.com/";
        private static string _G = "G:";
        private static string _D = "D:";

        public string ItemImages { get; set; } = "ItemImages";

        //拷贝到的文件夹
        private readonly string _itemfiles = "ItemFiles";
        private readonly string _itemfilescopy = "ItemFiles_Copy";

        private readonly string _nS = @"\s\";
        private readonly string _samll = "_small";




        private delegate void DelegateLoad(ref List<Web_UserBuildingCircle> allData);
        private delegate void DelegateWriteMessage(int operate, string itemId, string oldPath, string newPath, int status, string summary);

        private delegate void DelegateCurrent(int count, string itemId = null);
        private delegate void DelegateTimeCost(string times);


        private List<Web_UserBuildingCircle> _allData = null;

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;

            Stopwatch watch = new Stopwatch();

            watch.Start();


            List<Web_UserBuildingCircle> allData = new List<Web_UserBuildingCircle>();

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

                BuildingUpOss(allData, watch);

                //Task task = Task.Run(() =>
                // {
                //     TitleImgCopy(allData,watch);
                // });

                // btnStart.Enabled = task.IsCompleted;
            }
            else
            {
                MessageBox.Show("d=====(￣▽￣*)b");
            }


        }



        private void BuildingUpOss(List<Web_UserBuildingCircle> data, Stopwatch watch)
        {
            //处理1，图文数据
            var imgTextData = data.Where(o => o.TalkType.ToString().Equals("1")).OrderByDescending(o => o.CreateDate).ToList();

            if (imgTextData != null)
            {
                //处理封面图片

                //测试数据
                //string[] tdata = { "66bdda7a-25e3-4af1-b7e0-63f15e33d00e", "549c2ea5-924c-4e33-9aba-1d0b82fe4edc" };
                //imgTextData = imgTextData.Where(o => tdata.Contains(o.TalkId)).ToList();

                imgTextPro(imgTextData);

                imgTextResourceObj(imgTextData);

            }

            //处理2，视频数据
            var videoData = data.Where(o => o.TalkType.ToString().Equals("2")).OrderByDescending(o => o.CreateDate).ToList();
            if (videoData != null)
            {
                //  videoPro(imgTextData);
            }


            //处理3，问答数据
            var qandAData = data.Where(o => o.TalkType.ToString().Equals("3")).OrderByDescending(o => o.CreateDate).ToList();

            //处理5，文章
            var articleData = data.Where(o => o.TalkType.ToString().Equals("5")).OrderByDescending(o => o.CreateDate).ToList();

            //处理6，
            var sVideoData = data.Where(o => o.TalkType.ToString().Equals("6")).OrderByDescending(o => o.CreateDate).ToList();





        }


        private long ImgLeng = 20971520; //字节 （20M）

        const string firstTitletImg = "?x-oss-process=image/resize,m_fixed,h_400,w_520/quality,q_80";
        const string smallImg = "?x-oss-process=image/quality,q_90/resize,w_400";
        const string bigImg = "?x-oss-process=image/quality,q_{0}/watermark,image_V2F0ZXJtYXJrL3dhdGVybWFyay5wbmc=,shadow_50,order_0,align_2,interval_10,t_90,g_sw,x_20,y_20";



        InfoStatus infoStatus = new InfoStatus();



        private void imgTextResourceObj(List<Web_UserBuildingCircle> data)
        {
            //var domainData = data.Where(o => !string.IsNullOrEmpty(o.ResourceObj) && o.FirstTitleImg.Contains(_domainOne)).ToList();

            List<CallbackUploadInfo> callbackUploadInfos = new List<CallbackUploadInfo>();

            foreach (var item in data)
            {
                CallbackUploadInfo callbackUploadInfo = new CallbackUploadInfo();

                string url = item.FirstTitleImg.Split('?').FirstOrDefault();

                callbackUploadInfo.smallImgUrl = url+ smallImg;
                callbackUploadInfo.bigImgUrl = url + string.Format(bigImg, 90);

                callbackUploadInfos.Add(callbackUploadInfo);

                // 保存数据
                try
                {
                    using (var context = new DataModelEntities())
                    {
                        var itemData = context.Web_UserBuildingCircle.FirstOrDefault(o => o.TalkId.Equals(item.TalkId));
                        if (itemData != null)
                        {
                            itemData.ResourceObj = callbackUploadInfos.Count == 0 ? null : callbackUploadInfos.ToJson();

                            itemData.TalkTitle = itemData.TalkTitle.Equals("NULL") ? null : itemData.TalkTitle;

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
        }

        private void imgTextPro(List<Web_UserBuildingCircle> data)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //过来只有在服务器上的数据
            var domainData = data.Where(o => !string.IsNullOrEmpty(o.FirstTitleImg) && o.FirstTitleImg.Contains(_domain)).ToList();

            string domainOSSFirstTitlet = string.Empty;

            foreach (var item in domainData)
            {
                infoStatus.Summary = "处理再" + _domain + "域名数据";

                //处理封面图片数据
                string filePath = item.FirstTitleImg.ToLower()
                                  .Replace(_domain, _D)
                                  .Replace(_itemfiles.ToLower(), ItemImages.ToLower())
                                  .Replace('/', '\\').ToLower();

                if (filePath.Contains(_nS))
                {
                    filePath = filePath.Replace(_nS, "\\");

                    //判断/s/里面的图片是否带 Ori_
                    string tempFilePath = filePath.Replace(filePath.Split('\\').LastOrDefault(), "Ori_" + filePath.Split('\\').LastOrDefault());

                    if (File.Exists(tempFilePath))
                        filePath = tempFilePath;
                }

                //判断文件是否存在
                if (File.Exists(filePath))
                {
                    var fileInfo = new System.IO.FileInfo(filePath);
                    if (fileInfo.Length >= ImgLeng)
                    {
                        CompressImg.PicCompress(filePath);
                    }

                    // 移动操作
                    string ossImgPath = filePath
                      .Replace(ItemImages.ToLower(), _itemfiles.ToLower())
                      .Remove(0, 3)
                      .Replace('\\', '/').ToLower();

                    //处理带Ori的数据
                    if (ossImgPath.Contains("Ori_".ToLower()))
                        ossImgPath = ossImgPath.Replace("Ori_".ToLower(), "");

                    this.OssUpload(ossImgPath, filePath, ref infoStatus);

                    domainOSSFirstTitlet = _domainOne + ossImgPath + firstTitletImg;

                }


                //处理 对象数据
                var obj = item.ResourceObj.ToList<CallbackUploadInfo>();


                List<CallbackUploadInfo> callbackUploadInfos = new List<CallbackUploadInfo>();

                if (obj != null)
                {
                    foreach (var itemRes in obj)
                    {
                        string filePathRes = itemRes.smallImgUrl
                                 .Replace(_domain, _D)
                                 .Replace(_itemfiles.ToLower(), ItemImages.ToLower())
                                 .Replace('/', '\\').ToLower();

                        if (filePathRes.Contains(_nS))
                        {
                            filePathRes = filePathRes.Replace(_nS, "\\");

                            //判断/s/里面的图片是否带 Ori_
                            string tempFilePath = filePathRes.Replace(filePathRes.Split('\\').LastOrDefault(), "Ori_" + filePathRes.Split('\\').LastOrDefault());

                            if (File.Exists(tempFilePath))
                                filePathRes = tempFilePath;

                            //判断文件是否存在
                            if (File.Exists(filePathRes))
                            {
                                var fileInfo = new System.IO.FileInfo(filePathRes);
                                if (fileInfo.Length >= ImgLeng)
                                {
                                    CompressImg.PicCompress(filePathRes);
                                }

                                // 移动操作
                                string ossImgPath = filePathRes
                                  .Replace(ItemImages.ToLower(), _itemfiles.ToLower())
                                  .Remove(0, 3)
                                  .Replace('\\', '/').ToLower();

                                //处理带Ori的数据
                                if (ossImgPath.Contains("Ori_".ToLower()))
                                    ossImgPath = ossImgPath.Replace("Ori_".ToLower(), "");

                                this.OssUpload(ossImgPath, filePathRes, ref infoStatus);

                                CallbackUploadInfo callback = new CallbackUploadInfo();

                                string ossPro = string.Empty;
                                if (fileInfo.Length >= 500000)
                                {
                                    ossPro = string.Format(bigImg, 20);
                                }
                                else if (fileInfo.Length >= 400000)
                                {
                                    ossPro = string.Format(bigImg, 30);
                                }
                                else if (fileInfo.Length >= 300000)
                                {
                                    ossPro = string.Format(bigImg, 40);
                                }
                                else if (fileInfo.Length >= 200000)
                                {
                                    ossPro = string.Format(bigImg, 50);
                                }
                                else if (fileInfo.Length >= 100000)
                                {
                                    ossPro = string.Format(bigImg, 90);
                                }
                                else
                                {
                                    ossPro = string.Format(bigImg, 100);
                                }

                                string ossUrl = _domainOne + ossImgPath + ossPro;

                                callback.smallImgUrl = _domainOne + ossImgPath + "?x-oss-process=image/quality,q_90/resize,w_400";
                                callback.bigImgUrl = ossUrl;
                                callback.fileName = itemRes.fileName;

                                callbackUploadInfos.Add(callback);

                            }

                        }
                    }
                }



                // 保存数据
                try
                {
                    using (var context = new DataModelEntities())
                    {
                        var itemData = context.Web_UserBuildingCircle.FirstOrDefault(o => o.TalkId.Equals(item.TalkId));
                        if (itemData != null)
                        {
                            itemData.FirstTitleImg = domainOSSFirstTitlet;
                            itemData.ResourceObj = callbackUploadInfos.Count == 0 ? null : callbackUploadInfos.ToJson();

                            itemData.TalkContent = string.IsNullOrEmpty(itemData.TalkContent.Trim()) ? itemData.TalkTitle : itemData.TalkContent;
                            itemData.TalkTitle = string.IsNullOrEmpty(itemData.TalkTitle) ? null : "NULL";

                            context.SaveChanges();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed with error info: {0}", ex.Message);
                    MessageBox.Show(string.Format("保存数据出错！{0}", ex.Message));
                }









                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateWriteMessage(_comm.WriteMessage), new object[] { 2, item.TalkId, filePath, infoStatus.OldPath, infoStatus.iStatus, infoStatus.Summary });
                    this.Invoke(new DelegateCurrent(_comm.Current), new object[] { 1, item.TalkId });
                    this.Invoke(new DelegateTimeCost(_comm.TimeCost), new object[] { watch.Elapsed.ToString() });
                }
                else
                {
                    _comm.WriteMessage(2, item.TalkId, filePath, infoStatus.OldPath, infoStatus.iStatus, infoStatus.Summary);
                    _comm.Current(1, item.TalkId);
                    _comm.TimeCost(watch.Elapsed.ToString());
                }





            }



            // 修改https://www.pic1.jzbl.com 的后缀
            var domainOneData = data.Where(o => !string.IsNullOrEmpty(o.FirstTitleImg) && o.FirstTitleImg.Contains(_domainOne)).ToList();
            // var domainOneData = data.Where(o =>o.TalkId.Equals("615a6f97-4fda-439a-b5c4-3b0e708bce13")).ToList();


            foreach (var itemOne in domainOneData)
            {

                infoStatus.Summary = "处理再" + _domainOne + "域名数据";

                List<CallbackUploadInfo> callbackUploadInfos = new List<CallbackUploadInfo>();

                //处理oss图片有后缀的，只压缩图片就行
                if (itemOne.FirstTitleImg.Split('.').LastOrDefault().ToLower().Equals("jpg"))
                {
                    domainOSSFirstTitlet = itemOne.FirstTitleImg + firstTitletImg;

                    //处理 对象数据
                    var obj = itemOne.ResourceObj.ToList<CallbackUploadInfo>();


                    if (obj != null)
                    {
                        foreach (var itemRes in obj)
                        {

                            CallbackUploadInfo callback = new CallbackUploadInfo();

                            string ossPro = string.Empty;

                            ossPro = string.Format(bigImg, 90);

                            string ossUrl = itemRes.smallImgUrl + ossPro;

                            callback.smallImgUrl = itemRes.smallImgUrl + "?x-oss-process=image/quality,q_90/resize,w_400";
                            callback.bigImgUrl = ossUrl;
                            callback.fileName = itemRes.fileName;

                            callbackUploadInfos.Add(callback);

                        }
                    }

                    infoStatus.Summary = "处理oss图片有后缀的，只压缩图片就行";


                }
                else
                {
                    string sourcekey = itemOne.FirstTitleImg.Replace(_domainOne, "");
                    string targetkey = sourcekey + ".jpg";


                    CopyObject(bucketName, sourcekey, bucketName, targetkey);

                    domainOSSFirstTitlet = _domainOne + targetkey;

                    //处理 对象数据
                    var obj = itemOne.ResourceObj.ToList<CallbackUploadInfo>();

                    if (obj != null)
                    {
                        foreach (var itemRes in obj)
                        {

                            CallbackUploadInfo callback = new CallbackUploadInfo();


                            string sourcekeylist = itemRes.smallImgUrl.Replace(_domainOne, "");
                            string targetkeylist = sourcekeylist + ".jpg";

                            CopyObject(bucketName, sourcekeylist, bucketName, targetkeylist);


                            string ossPro = string.Empty;

                            ossPro = string.Format(bigImg, 90);

                            string ossUrl = itemRes.smallImgUrl + ossPro;

                            callback.smallImgUrl = itemRes.smallImgUrl + "?x-oss-process=image/quality,q_90/resize,w_400";
                            callback.bigImgUrl = ossUrl;
                            callback.fileName = itemRes.fileName;

                            callbackUploadInfos.Add(callback);

                        }
                    }


                    infoStatus.Summary = "处理oss图片没有后缀，先拷贝，再删除原图";


                }


                // 保存数据
                try
                {
                    using (var context = new DataModelEntities())
                    {
                        var itemData = context.Web_UserBuildingCircle.FirstOrDefault(o => o.TalkId.Equals(itemOne.TalkId));
                        if (itemData != null)
                        {
                            itemData.FirstTitleImg = domainOSSFirstTitlet;
                            itemData.ResourceObj = callbackUploadInfos.Count == 0 ? null : callbackUploadInfos.ToJson();

                            itemData.TalkContent = string.IsNullOrEmpty(itemData.TalkContent.Trim()) ? itemData.TalkTitle : itemData.TalkContent;
                            itemData.TalkTitle = string.IsNullOrEmpty(itemData.TalkTitle) ? null : "NULL";

                            context.SaveChanges();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed with error info: {0}", ex.Message);
                    MessageBox.Show(string.Format("保存数据出错！{0}", ex.Message));
                }






                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateWriteMessage(_comm.WriteMessage), new object[] { 2, itemOne.TalkId, "---------", infoStatus.OldPath, infoStatus.iStatus, infoStatus.Summary });
                    this.Invoke(new DelegateCurrent(_comm.Current), new object[] { 1, itemOne.TalkId });
                    this.Invoke(new DelegateTimeCost(_comm.TimeCost), new object[] { watch.Elapsed.ToString() });
                }
                else
                {
                    _comm.WriteMessage(2, itemOne.TalkId, "---------", infoStatus.OldPath, infoStatus.iStatus, infoStatus.Summary);
                    _comm.Current(1, itemOne.TalkId);
                    _comm.TimeCost(watch.Elapsed.ToString());
                }





            }



        }


        private void videoPro(List<Web_UserBuildingCircle> data)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //过来只有在服务器上的数据https://www.pic.jzbl.com
            var domainData = data.Where(o => o.FirstTitleImg.Contains(_domain)).ToList();

            string domainOSSFirstTitlet = string.Empty;

            foreach (var item in domainData)
            {

                //处理封面图片数据
                string filePath = item.FirstTitleImg.ToLower()
                                    .Replace(_domain, _D)
                                    .Replace(_itemfiles.ToLower(), ItemImages.ToLower())
                                    .Replace('/', '\\').ToLower();

                if (filePath.Contains(_nS))
                {
                    filePath = filePath.Replace(_nS, "\\");

                    //判断/s/里面的图片是否带 Ori_
                    string tempFilePath = filePath.Replace(filePath.Split('\\').LastOrDefault(), "Ori_" + filePath.Split('\\').LastOrDefault());

                    if (File.Exists(tempFilePath))
                        filePath = tempFilePath;
                }

                //判断文件是否存在
                if (File.Exists(filePath))
                {
                    var fileInfo = new System.IO.FileInfo(filePath);
                    if (fileInfo.Length >= ImgLeng)
                    {
                        CompressImg.PicCompress(filePath);
                    }

                    // 移动操作
                    string ossImgPath = filePath
                      .Replace(ItemImages.ToLower(), _itemfiles.ToLower())
                      .Remove(0, 3)
                      .Replace('\\', '/').ToLower();

                    //处理带Ori的数据
                    if (ossImgPath.Contains("Ori_".ToLower()))
                        ossImgPath = ossImgPath.Replace("Ori_".ToLower(), "");

                    this.OssUpload(ossImgPath, filePath, ref infoStatus);

                    domainOSSFirstTitlet = _domainOne + ossImgPath + firstTitletImg;

                }


                //处理 对象数据
                var obj = item.ResourceObj.ToList<CallbackUploadInfo>();


                List<CallbackUploadInfo> callbackUploadInfos = new List<CallbackUploadInfo>();

                if (obj != null)
                {
                    foreach (var itemRes in obj)
                    {
                        string filePathRes = itemRes.smallImgUrl
                                 .Replace(_domain, _D)
                                 .Replace(_itemfiles.ToLower(), ItemImages.ToLower())
                                 .Replace('/', '\\').ToLower();

                        if (filePathRes.Contains(_nS))
                        {
                            filePathRes = filePathRes.Replace(_nS, "\\");

                            //判断/s/里面的图片是否带 Ori_
                            string tempFilePath = filePathRes.Replace(filePathRes.Split('\\').LastOrDefault(), "Ori_" + filePathRes.Split('\\').LastOrDefault());

                            if (File.Exists(tempFilePath))
                                filePathRes = tempFilePath;

                            //判断文件是否存在
                            if (File.Exists(filePathRes))
                            {
                                var fileInfo = new System.IO.FileInfo(filePathRes);
                                if (fileInfo.Length >= ImgLeng)
                                {
                                    CompressImg.PicCompress(filePathRes);
                                }

                                // 移动操作
                                string ossImgPath = filePathRes
                                  .Replace(ItemImages.ToLower(), _itemfiles.ToLower())
                                  .Remove(0, 3)
                                  .Replace('\\', '/').ToLower();

                                //处理带Ori的数据
                                if (ossImgPath.Contains("Ori_".ToLower()))
                                    ossImgPath = ossImgPath.Replace("Ori_".ToLower(), "");

                                this.OssUpload(ossImgPath, filePathRes, ref infoStatus);

                                CallbackUploadInfo callback = new CallbackUploadInfo();

                                string ossPro = string.Empty;
                                if (fileInfo.Length >= 500000)
                                {
                                    ossPro = string.Format(bigImg, 20);
                                }
                                else if (fileInfo.Length >= 400000)
                                {
                                    ossPro = string.Format(bigImg, 30);
                                }
                                else if (fileInfo.Length >= 300000)
                                {
                                    ossPro = string.Format(bigImg, 40);
                                }
                                else if (fileInfo.Length >= 200000)
                                {
                                    ossPro = string.Format(bigImg, 50);
                                }
                                else if (fileInfo.Length >= 100000)
                                {
                                    ossPro = string.Format(bigImg, 90);
                                }
                                else
                                {
                                    ossPro = string.Format(bigImg, 100);
                                }

                                string ossUrl = _domainOne + ossImgPath + ossPro;

                                callback.smallImgUrl = _domainOne + ossImgPath + smallImg;
                                callback.bigImgUrl = ossUrl;
                                callback.fileName = itemRes.fileName;

                                callbackUploadInfos.Add(callback);

                            }

                        }
                    }
                }



                // 保存数据
                try
                {
                    using (var context = new DataModelEntities())
                    {
                        var itemData = context.Web_UserBuildingCircle.FirstOrDefault(o => o.TalkId.Equals(item.TalkId));
                        if (itemData != null)
                        {
                            itemData.FirstTitleImg = domainOSSFirstTitlet;
                            itemData.ResourceObj = callbackUploadInfos.Count == 0 ? null : callbackUploadInfos.ToJson();

                            itemData.TalkContent = string.IsNullOrEmpty(itemData.TalkContent.Trim()) ? itemData.TalkTitle : itemData.TalkContent;
                            itemData.TalkTitle = string.IsNullOrEmpty(itemData.TalkTitle) ? null : "NULL";

                            context.SaveChanges();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed with error info: {0}", ex.Message);
                    MessageBox.Show(string.Format("保存数据出错！{0}", ex.Message));
                }









                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateWriteMessage(_comm.WriteMessage), new object[] { 2, item.TalkId, filePath, infoStatus.OldPath, infoStatus.iStatus, infoStatus.Summary });
                    this.Invoke(new DelegateCurrent(_comm.Current), new object[] { 1, item.TalkId });
                    this.Invoke(new DelegateTimeCost(_comm.TimeCost), new object[] { watch.Elapsed.ToString() });
                }
                else
                {
                    _comm.WriteMessage(2, item.TalkId, filePath, infoStatus.OldPath, infoStatus.iStatus, infoStatus.Summary);
                    _comm.Current(1, item.TalkId);
                    _comm.TimeCost(watch.Elapsed.ToString());
                }





            }




        }



        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="sourceBucket"></param>
        /// <param name="sourceKey"></param>
        /// <param name="targetBucket"></param>
        /// <param name="targetKey"></param>
        public static void CopyObject(string sourceBucket, string sourceKey, string targetBucket, string targetKey)
        {
            try
            {
                //var metadata = new ObjectMetadata();
                //metadata.AddHeader("mk1", "mv1");
                //metadata.AddHeader("mk2", "mv2");
                var req = new CopyObjectRequest(sourceBucket, sourceKey, targetBucket, targetKey);
                //{
                //    NewObjectMetadata = metadata
                //};
                client.CopyObject(req);

                Console.WriteLine("Copy object succeeded");

                DeleteObject(sourceBucket, sourceKey);

            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="sourcekey"></param>
        public static void DeleteObject(string bucketName, string sourcekey)
        {
            try
            {
                string key = null;
                var listResult = client.ListObjects(bucketName);
                foreach (var summary in listResult.ObjectSummaries)
                {
                    key = summary.Key;
                    break;
                }

                client.DeleteObject(bucketName, sourcekey);

                Console.WriteLine("Delete object succeeded");
            }
            //catch (OssException ex)
            //{
            //    Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
            //        ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            //}
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 上传到OSS
        /// </summary>
        /// <param name="ossFilePath">Oss上的图片地址： TempTest/1.jpg</param>
        /// <param name="filePath">D:\test\1.jpg</param>
        private void OssUpload(string ossFilePath, string filePath, ref InfoStatus infoStatus)
        {
            //上传到OSS
            try
            {
                client.PutObject(bucketName, ossFilePath, filePath);
                Console.WriteLine("Put object:{0} succeeded", ossFilePath);
                infoStatus.Summary = "上传成功";

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
        /// 移动图片
        /// </summary>
        private void TitleImgCopy(List<Web_ItemLibrary> data, Stopwatch watch)
        {

            Comm comm = new Comm(this.dataGV, this.tssldq, this.tsslhs);

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

        private void LoadData(ref List<Web_UserBuildingCircle> allData)
        {
            // List<Web_ItemLibrary> allData = new List<Web_ItemLibrary>();

            SimpleLoading loadingfrm = new SimpleLoading(this);
            //将Loaing窗口，注入到 SplashScreenManager 来管理
            SplashScreenManager loading = new SplashScreenManager(loadingfrm);
            loading.ShowLoading();
            using (var context = new DataModelEntities())
            {
                allData = context.Web_UserBuildingCircle
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
                _allData = context.Web_UserBuildingCircle
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
