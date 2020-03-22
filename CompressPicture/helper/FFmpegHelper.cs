
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressPicture.helper
{
    public static class FFmpegHelper
    {
        /// <summary>
        /// 获取视频长度
        /// </summary>
        /// <param name="sourceFile">视频源</param>
        /// <returns></returns>
        public static string GetVideoDuration(string sourceFile)
        {
            //   string ffmpegpath = System.Web.HttpContext.Current.Server.MapPath("/") + ffmpegPath;//ffmpeg执行文件的路径
            string ffmpegpath = AppDomain.CurrentDomain.BaseDirectory + "\\" + "ffmpeg.exe";
            using (System.Diagnostics.Process ffmpeg = new System.Diagnostics.Process())
            {
                String duration;  // soon will hold our video's duration in the form "HH:MM:SS.UU"
                String result;  // temp variable holding a string representation of our video's duration
                StreamReader errorreader;  // StringWriter to hold output from ffmpeg

                // we want to execute the process without opening a shell
                ffmpeg.StartInfo.UseShellExecute = false;
                //ffmpeg.StartInfo.ErrorDialog = false;
                ffmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                // redirect StandardError so we can parse it
                // for some reason the output comes through over StandardError
                ffmpeg.StartInfo.RedirectStandardError = true;

                // set the file name of our process, including the full path
                // (as well as quotes, as if you were calling it from the command-line)
                ffmpeg.StartInfo.FileName = ffmpegpath;

                // set the command-line arguments of our process, including full paths of any files
                // (as well as quotes, as if you were passing these arguments on the command-line)
                ffmpeg.StartInfo.Arguments = "-i " + sourceFile;

                // start the process
                ffmpeg.Start();

                // now that the process is started, we can redirect output to the StreamReader we defined
                errorreader = ffmpeg.StandardError;

                // wait until ffmpeg comes back
                ffmpeg.WaitForExit();

                // read the output from ffmpeg, which for some reason is found in Process.StandardError
                result = errorreader.ReadToEnd();

                // a little convoluded, this string manipulation...
                // working from the inside out, it:
                // takes a substring of result, starting from the end of the "Duration: " label contained within,
                // (execute "ffmpeg.exe -i somevideofile" on the command-line to verify for yourself that it is there)
                // and going the full length of the timestamp

                duration = result.Substring(result.IndexOf("Duration: ") + ("Duration: ").Length, ("00:00:00").Length);
                return duration;
            }
        }

        //public static string GetVideoDur()
        //{

        //    /**
        //       * 支持视频格式：mpeg，mpg，avi，dat，mkv，rmvb，rm，mov.
        //       *不支持：wmv
        //       * **/

        //    VideoEncoder.Encoder enc = new VideoEncoder.Encoder();
        //    //ffmpeg.exe的路径，程序会在执行目录（....FFmpeg测试\bin\Debug）下找此文件，
        //    enc.FFmpegPath = "ffmpeg.exe";
        //    //视频路径
        //    string videoFilePath = "d:\\纯粹瑜伽-混合课程.avi";
        //    VideoFile videoFile = new VideoFile(videoFilePath);

        //    enc.GetVideoInfo(videoFile);

        //    TimeSpan totaotp = videoFile.Duration;
        //    string totalTime = string.Format("{0:00}:{1:00}:{2:00}", (int)totaotp.TotalHours, totaotp.Minutes, totaotp.Seconds);

        //    Console.WriteLine("时间长度：{0}", totalTime);
        //    Console.WriteLine("高度：{0}", videoFile.Height);
        //    Console.WriteLine("宽度：{0}", videoFile.Width);
        //    Console.WriteLine("数据速率：{0}", videoFile.VideoBitRate);
        //    Console.WriteLine("数据格式：{0}", videoFile.VideoFormat);
        //    Console.WriteLine("比特率：{0}", videoFile.BitRate);
        //    Console.WriteLine("文件路径：{0}", videoFile.Path);

        //    Console.ReadKey();
        //}
    }
}
