using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompressPicture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressPicture.Tests
{
    [TestClass()]
    public class CompressImgTests
    {
        [TestMethod()]
        public void PicCompressTest()
        {
            string imgPath = @"G:\itemfiles_copy\8b166b7c-d1cf-47b2-806a-3a81ee78b6e3\b56f4368-38bc-4155-8c45-5004381bcdeb\6368075237451912763453355_small.jpg";
            //string imgPath = @"G:\itemfiles_copy\8b166b7c-d1cf-47b2-806a-3a81ee78b6e3\b56f4368-38bc-4155-8c45-5004381bcdeb\6368075237511192306339084_small.jpg";

            CompressImg.PicCompress(imgPath);

            Assert.Fail();
        }
    }
}