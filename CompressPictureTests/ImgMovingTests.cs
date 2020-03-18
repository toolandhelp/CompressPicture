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
    public class ImgMovingTests
    {
        [TestMethod()]
        public void ImgMovingTest()
        {
            string filePathCopy = @"G:\ItemFiles\d5b4fbaa-40a0-4145-a119-88af91c3bf8f\2019-11-28\94da2994-a9be-4ad1-b903-991771738fda\i\s\1574935822_small.jpg";

            var path = filePathCopy.Split('\\').Reverse();
            Assert.Fail();
        }
    }
}