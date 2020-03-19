using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressPicture
{
    /// <summary>
    /// 
    /// </summary>
   public class InfoStatus
    {
        //iUnExists++;
        //oldPath = filePath;
        //summary = $"路径：{filePath} 状态：< > 文件不存在";
        //status = (int) EnumStatus.OldFileUnExists;

        public int iUnExists { get; set; }
        public string OldPath { get; set; }
        public string Summary { get; set; }
        public int iStatus { get; set; }
    }

    /// <summary>
    /// 返回的上传信息
    /// </summary>
    public class CallbackUploadInfo
    {
        /// <summary>
        /// 压缩图路径（包括视频封面图片路径）
        /// </summary>
        public string smallImgUrl { get; set; }
        /// <summary>
        /// 压缩图路径（包括视频封面图片路径）
        /// </summary>
        public string bigImgUrl { get; set; }
        /// <summary>
        /// 视频地址
        /// </summary>
        public string videoUrl { get; set; }
        /// <summary>
        /// 图片的MD5 前端传入的
        /// </summary>
        public string md5 { get; set; }
        /// <summary>
        /// 文件名称(图片，视频，文件)
        /// </summary>
        public string fileName { get; set; }
    }
}
