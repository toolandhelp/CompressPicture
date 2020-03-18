using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompressPicture
{
    public static class ContentHelper
    {


        public static string[] GetImgsByContentBefore(string contentBefore)
        {
            string[] imgsrc = null;
            if (string.IsNullOrEmpty(contentBefore))
                return null;

            //
            if (contentBefore.Contains("data-original"))
            {
                imgsrc =FindImgByContentBefore(contentBefore, @"<img\b[^<>]*?\bdata-original[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>");
            }
            else
            {
                imgsrc = FindImgByContentBefore(contentBefore, @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>");
            }

            return imgsrc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentBefore"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        private static string[] FindImgByContentBefore(string contentBefore, string regex)
        {
            if (string.IsNullOrEmpty(contentBefore) || string.IsNullOrEmpty(regex))
                return null;
            //if (string.IsNullOrEmpty(regex))
            //    return null;
            //if (ContentBefore.Length > 0)
            //{
            // 定义正则表达式用来匹配 img 标签 
            // Regex regImg = new Regex(@"<img\b[^<>]*?\bdata-original[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            Regex regImg = new Regex(regex, RegexOptions.IgnoreCase);

            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(contentBefore);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表 
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            // }

            return sUrlList;

        }

    }
}
