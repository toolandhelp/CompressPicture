using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressPicture
{
    /// <summary>
    /// 操作事 （1-封面拷贝，2-文件拷贝，3-内容拷贝）
    /// </summary>
    public enum EnumOperate
    {
        /// <summary>
        /// 封面
        /// </summary>
        [Text("封面")]
        TitleImg = 1,
        /// <summary>
        /// 文件
        /// </summary>
        [Text("文件")]
        File = 2,
        /// <summary>
        /// 内容
        /// </summary>
        [Text("内容")]
        Content = 3

    }
    /// <summary>
    /// 操作状态（1-原始文件不存在，2-新文件已存在，3-拷贝成功，4-未知（），5-失败(错误)，）
    /// </summary>
    public enum EnumStatus
    {
        /// <summary>
        /// 原始文件不存在
        /// </summary>
        [Text("原始文件不存在")]
        OldFileUnExists = 1,
        /// <summary>
        /// 新文件已存在
        /// </summary>
        [Text("新文件已存在")]
        NewFileExists = 2,
        /// <summary>
        /// 拷贝成功
        /// </summary>
        [Text("拷贝成功")]
        Ok = 3,
        /// <summary>
        /// 未知
        /// </summary>
        [Text("未知")]
        Unknown = 4,
        /// <summary>
        /// 失败(错误)
        /// </summary>
        [Text("失败(错误)")]
        Error = 5


    }

}
