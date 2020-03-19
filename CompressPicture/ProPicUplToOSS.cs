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
    public partial class ProPicUplToOSS : Form
    {
        private Comm _comm;
        public ProPicUplToOSS()
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
        private string _G = "G:";
        private string _D = "D:";

        public string ItemImages { get; set; } = "ItemImages";

        //拷贝到的文件夹
        private readonly string _itemfiles = "ItemFiles";
        private readonly string _itemfilescopy = "ItemFiles_Copy";


        private readonly string _nS = @"\s\";
        private readonly string _samll = "_small";


        private long ImgLeng = 20971520; //字节 （20M）

        int gjxm = 0, yxxm = 0;

        private delegate void DelegateWriteMessage(int operate, string itemId, string oldPath, string newPath, int status, string summary);
        private delegate void DelegateCurrent(int count, string itemId = null);
        private delegate void DelegateTimeCost(string times);

        private void btnStart_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();

            this.btnStart.Enabled = false;

            List<Web_ItemLibrary> allData = new List<Web_ItemLibrary>();

            SimpleLoading loadingfrm = new SimpleLoading(this);
            SplashScreenManager loading = new SplashScreenManager(loadingfrm);
            loading.ShowLoading();

            //临时数据  
            //  8a8632fe-fcb0-4e56-b4b9-7bc965885994  =  https://www.pic.jzbl.com/ItemFiles/0d54bf62-e42b-460b-aab4-7e6bd1990221/8a8632fe-fcb0-4e56-b4b9-7bc965885994/6367115379197304639880898.jpg  直接得到原图
            //  91fbcff0-304c-4fa6-b3d7-3f6662cd609a  =  https://www.pic.jzbl.com/ItemFiles/8b166b7c-d1cf-47b2-806a-3a81ee78b6e3/91fbcff0-304c-4fa6-b3d7-3f6662cd609a/6368089569974537847086484_small.jpg  去_small得到原图

            //  3a966d22-56ba-4d23-9ab5-0355523d1604  =  https://www.pic.jzbl.com/itemFiles/6760efac-e98b-4539-adf3-a842280803dd/2019-09-21/3a966d22-56ba-4d23-9ab5-0355523d1604/i/s/ca4e0b991569072379.jpg  去/s/ 得到原图

            //  9a599ffe-039c-4886-a28c-1936b5e4e5eb  =  https://www.pic.jzbl.com/itemFiles/8e5dc0f7-ed86-4d3c-89db-3a8593f6a2d7/2020-03-02/9a599ffe-039c-4886-a28c-1936b5e4e5eb/i/s/950b1f8a1583157228.jpg  去/s/ 加ori_ 得到原图
            string[] tdata = { "8a8632fe-fcb0-4e56-b4b9-7bc965885994", "91fbcff0-304c-4fa6-b3d7-3f6662cd609a", "3a966d22-56ba-4d23-9ab5-0355523d1604"}; //"9a599ffe-039c-4886-a28c-1936b5e4e5eb" 



            using (var context = new DataModelEntities())
            {
                //排除
                allData = context.Web_ItemLibrary
                    .Where(o => o.IsDelete == 0 && !string.IsNullOrEmpty(o.ItemContentBefore))
                    // .Take(50)
                    .OrderByDescending(o => o.CreateDate)
                    .ToList();
                //allData = context.Web_ItemLibrary
                //    .Where(o => tdata.Contains(o.ItemId))
                //    .OrderByDescending(o => o.CreateDate)
                //    .ToList();

                if (allData.Any())
                {
                    loading.CloseWaitForm();
                }
            }
            gjxm = allData.Count;

            this.tsslgyxm.Text = $"共计项目：{gjxm}，有效项目{0}";
            this.tsslgj.Text = 0.ToString();

            //this.ContentImgUploadToOSS(allData, watch);

            Task task = Task.Run(() =>
            {
                this.ContentImgUploadToOSS(allData, watch);
                //this.ContentImgDataCopy(allData, watch);
            });

            // this.btnStart.Enabled = task.IsCompleted;


        }

        const string ossProcess = "?x-oss-process=image/quality,q_{0}/watermark,image_V2F0ZXJtYXJrL3dhdGVybWFyay5wbmc=,size_20,type_d3F5LXplbmhlaQ,text_{1},color_E94743,shadow_50,order_0,align_2,interval_10,t_90,g_sw,x_20,y_20";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="stopwatch"></param>
        private void ContentImgUploadToOSS(List<Web_ItemLibrary> data, Stopwatch stopwatch)
        {
            InfoStatus infoStatus = new InfoStatus();

            int i = 0;


            string[] xtAdmin = { "系统用户01", "系统用户02", "系统用户03", "系统用户04", "系统用户05", "系统用户06", "管理员" };

            string filePath = string.Empty;

            foreach (var item in data)
            {
                if (!string.IsNullOrEmpty(item.ItemContentBefore))
                {

                    string[] imgDatas = ContentHelper.GetImgsByContentBefore(item.ItemContentBefore);

                    string proContent = item.ItemContentBefore.ToLower();

                    if (imgDatas.Length > 0 && imgDatas != null)
                    {

                        yxxm++;
                        this.tsslgyxm.Text = $"共计项目：{gjxm}，有效项目{yxxm}";

                        this.tsslgj.Text = (int.Parse(this.tsslgj.Text) + imgDatas.Length).ToString();

                        string userName = "@" + (xtAdmin.Contains(item.CreateUserName) ? "部落建筑" : item.CreateUserName);

                        var bytes = Encoding.UTF8.GetBytes(userName);

                        char[] padding = { '=' };
                        string userNameURLBase64 = Convert.ToBase64String(bytes, Base64FormattingOptions.None).TrimEnd(padding).Replace('+', '-').Replace('/', '_');


                        List<CallbackUploadInfo> callbacks = new List<CallbackUploadInfo>();

                        for (int j = 0; j < imgDatas.Length; j++)
                        {
                            filePath = imgDatas[j].ToLower()
                                .Replace(_domain, _D)
                                .Replace(_itemfiles.ToLower(), ItemImages.ToLower())
                                .Replace('/', '\\').ToLower();

                            //处理文件（将原图上传到oss）

                            if (filePath.Contains(_samll))
                            {
                                filePath = filePath.Replace(_samll, "");
                            }
                            else if (filePath.Contains(_nS))
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

                                //暂时注释
                                this.OssUpload(ossImgPath, filePath, item.ItemId, ref infoStatus);

                                //内容数据修改
                                //https://www.pic1.jzbl.com/itemfiles/0d54bf62-e42b-460b-aab4-7e6bd1990221/001cc6d4-bd91-4d22-8ad8-e6fa5be7f294/6367192853011954234817412.jpg?x-oss-process=image/quality,q_90/watermark,image_V2F0ZXJtYXJrL3dhdGVybWFyay5wbmc=,size_30,type_d3F5LXplbmhlaQ,text_QOS4iua1t-azm-WiqOaVsOeggeenkeaKgOaciemZkOWFrOWPuA,color_E94743,shadow_50,order_0,align_2,interval_10,t_90,g_sw,x_20,y_20

                                CallbackUploadInfo callback = new CallbackUploadInfo();

                                string ossPro = string.Empty;
                                if (fileInfo.Length >= 500000)
                                {
                                    ossPro = string.Format(ossProcess, 20, userNameURLBase64);
                                }
                                else if (fileInfo.Length >= 400000)
                                {
                                    ossPro = string.Format(ossProcess, 30, userNameURLBase64);
                                }
                                else if (fileInfo.Length >= 300000)
                                {
                                    ossPro = string.Format(ossProcess, 40, userNameURLBase64);
                                }
                                else if (fileInfo.Length >= 200000)
                                {
                                    ossPro = string.Format(ossProcess, 50, userNameURLBase64);
                                }
                                else if (fileInfo.Length >= 100000)
                                {
                                    ossPro = string.Format(ossProcess, 90, userNameURLBase64);
                                }
                                else
                                {
                                    ossPro = string.Format(ossProcess, 100, userNameURLBase64);
                                }
                                string ossUrl = _domainOne + ossImgPath + ossPro;
                                proContent = proContent.Replace(imgDatas[j].ToLower(), ossUrl);

                                callback.smallImgUrl = _domainOne + ossImgPath+ "?x-oss-process=image/quality,q_90/resize,w_400";
                                callback.bigImgUrl = ossUrl;
                                callbacks.Add(callback);
                            }
                            else
                            {
                                infoStatus.iStatus = (int)EnumStatus.OldFileUnExists;
                                infoStatus.Summary = "本地文件不存在";
                            }

                        }

                        // 修改数据
                        this.DataUpdata(item.ItemId, proContent, callbacks);
                        infoStatus.Summary = infoStatus.Summary + ",【" + imgDatas.Length + "】数据修改成功";
                        infoStatus.iStatus = (int)EnumStatus.Ok;








                    }


                }
                else
                {
                    infoStatus.iStatus = (int)EnumStatus.Unknown;
                    infoStatus.Summary = "项目内容不存在";
                }




                if (this.InvokeRequired)
                {
                    this.Invoke(new DelegateWriteMessage(_comm.WriteMessage), new object[] { 2, item.ItemId, filePath, infoStatus.OldPath, infoStatus.iStatus, infoStatus.Summary });
                    this.Invoke(new DelegateCurrent(_comm.Current), new object[] { i, item.ItemId });
                    this.Invoke(new DelegateTimeCost(_comm.TimeCost), new object[] { stopwatch.Elapsed.ToString() });
                }
                else
                {
                    _comm.WriteMessage(2, item.ItemId, filePath, infoStatus.OldPath, infoStatus.iStatus, infoStatus.Summary);
                    _comm.Current(i, item.ItemId);
                    _comm.TimeCost(stopwatch.Elapsed.ToString());
                }


            }
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
        /// <param name="itemContent">项目内容数据</param>
        /// <param name="callback">9宫格对象</param>
        public void DataUpdata(string itemid, string itemContent, List<CallbackUploadInfo> callback)
        {
            try
            {
                using (var context = new DataModelEntities())
                {
                    var itemData = context.Web_ItemLibrary.FirstOrDefault(o => o.ItemId.Equals(itemid));
                    if (itemData != null)
                    {

                        itemData.ItemContentBefore = itemContent;
                        itemData.ResourceObj = callback.Count == 0 ? null : callback.ToJson();

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




        private void ContentImgDataCopy(List<Web_ItemLibrary> data, Stopwatch watch)
        {
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

                            this.tsslgj.Text = (int.Parse(this.tsslgj.Text) + imgDatas.Length).ToString();

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
                                            string nSPath = filePath.Replace(_samll, "").Replace(_nS, "\\");
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
                                            nSPath = nSPath.Replace(nSPath.Split('\\').LastOrDefault(), "Ori_" + nSPath.Split('\\').LastOrDefault());
                                            nSfilePath = nSfilePath.Replace(nSfilePath.Split('\\').LastOrDefault(), "Ori_" + nSfilePath.Split('\\').LastOrDefault());

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
                                        //if (cb01.Checked)
                                        //{

                                        //}
                                        ////2.压缩一把
                                        //if (cb02.Checked)
                                        //{
                                        //    CompressImg.PicCompress(filePathCopy);
                                        //}

                                        ////3.打水印 生成新图  xx_yssy.jpg xx_压缩水印.jpg
                                        //if (cb03.Checked)
                                        //{

                                        //}



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

                MessageBox.Show(" 错误：" + ex);
            }
        }











    }
}
