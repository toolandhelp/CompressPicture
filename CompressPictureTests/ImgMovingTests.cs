using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompressPicture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aliyun.OSS;
using CompressPicture.helper;
using System.IO;

namespace CompressPicture.Tests
{
    [TestClass()]
    public class ImgMovingTests
    {
        static string accessKeyId = Config.AccessKeyId;
        static string accessKeySecret = Config.AccessKeySecret;
        static string endpoint = Config.Endpoint;

        static OssClient client = new OssClient(endpoint, accessKeyId, accessKeySecret);
        static string bucketName = Config.BucketName;

        [TestMethod()]
        public void ImgMovingTest()
        {
            string filePathCopy = @"G:\ItemFiles\d5b4fbaa-40a0-4145-a119-88af91c3bf8f\2019-11-28\94da2994-a9be-4ad1-b903-991771738fda\i\s\1574935822_small.jpg";

            var path = filePathCopy.Split('\\').Reverse();
            Assert.Fail();
        }

        [TestMethod()]
        public void CopyObj()
        {
            //string sourBuck = "https://www.pic1.jzbl.com/buildingcircle/2d0f0642-0f54-412a-a830-cb0b5828ce90/2020-01-06/i/1578285323869";
            string sourcekey = "buildingcircle/2d0f0642-0f54-412a-a830-cb0b5828ce90/2020-01-06/i/1578285323869";
            string targetkey = "buildingcircle/2d0f0642-0f54-412a-a830-cb0b5828ce90/2020-01-06/i/1578285323869.jpg";

            CopyObject(bucketName,sourcekey, bucketName, targetkey);
        }


        [TestMethod]
        public void GetVideoDuration()
        {
            //File file = new File();
            //// 多媒体信息
            //MultimediaInfo info = encoder.getInfo(file);

            string mp4Url = "https://www.pic1.jzbl.com/buildingcircle/a092c1b7-f4c6-4d33-ac3b-7e3d534ed5b1/2020-03-03/v/1583240039140.mp4";

            var datas = FFmpegHelper.GetVideoDuration(mp4Url);

            Console.WriteLine("111");
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

        public static void DeleteObject(string bucketName,string sourcekey)
        {
            try
            {
                //string key = null;
                //var listResult = client.ListObjects(bucketName);
                //foreach (var summary in listResult.ObjectSummaries)
                //{
                //    key = summary.Key;
                //    break;
                //}

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





    }

    internal class Config
    {
        public static string AccessKeyId = "LTAI4FmRKvcZWCxx9xKhrtE2";

        public static string AccessKeySecret = "HMM8yiPdmJinY5kaLWojAX7BrnekuD";

        public static string Endpoint = "oss-cn-shanghai.aliyuncs.com";

        public static string BucketName = "jzbl-pic";
        //public static string DirToDownload = "<your local dir to download file>";

        //public static string FileToUpload = @"C:\Users\Administrator\Downloads\Image\1.jpg";

        //public static string BigFileToUpload = "<your local big file to upload>";
        //public static string ImageFileToUpload = "<your local image file to upload>";
        //public static string CallbackServer = "<your callback server uri>";
    }
}