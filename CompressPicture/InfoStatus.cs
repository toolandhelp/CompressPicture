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
}
