using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressPicture
{
    public static class CompressImg
    {
        /// <summary>
        /// 默认像素
        /// </summary>
        private static int _defaultPixel = 1000;

        /// <summary>
        /// 图片压缩（覆盖当前图片）
        /// </summary>
        /// <param name="originalImgPath"></param>
        public static void PicCompress(string originalImgPath)
        {
            PicCompress(originalImgPath, originalImgPath);
        }

        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="originalImgPath">压缩的原图</param>
        /// <param name="savaImgPath">保存的图片路径</param>
        public static void PicCompress(string originalImgPath, string savaImgPath)
        {
            //如果小于150kb 就不压缩了
            if (File.ReadAllBytes(originalImgPath).Length <= 150 * 1024)
            {
                return;
            }
            string tempPath = savaImgPath.Replace(".", "_.");
            //不能用!
            //Task task = Task.Run(() =>
            //{
            byte[] imageBytes = File.ReadAllBytes(originalImgPath);
            using (Bitmap bitmap = (Bitmap)Image.FromStream(new MemoryStream(imageBytes)))
            {
                //1.计算当前图片的像素大小
                //必须有一个大于1000像素
                if (bitmap.Width > _defaultPixel || bitmap.Height > _defaultPixel)
                {
                    // int maxPixel = bitmap.Width > bitmap.Height  ? bitmap.Width : bitmap.Height;
                    double rate = 0;

                    if (bitmap.Width == bitmap.Height)
                    {
                        using (Image image = SizeImageWithOldPercent(bitmap, _defaultPixel, _defaultPixel))
                        {
                            image.Save(tempPath, ImageFormat.Jpeg);
                            GetPicThumbnail(tempPath, savaImgPath, 80);
                        }
                    }

                    else if (bitmap.Width > bitmap.Height)
                    {
                        rate = _defaultPixel / (double)bitmap.Width;

                        using (Image image = SizeImageWithOldPercent(bitmap, _defaultPixel, (int)(bitmap.Height * rate)))
                        {
                            image.Save(tempPath, ImageFormat.Jpeg);
                            GetPicThumbnail(tempPath, savaImgPath, 80);
                        }
                    }
                    else
                    {
                        rate = _defaultPixel / (double)bitmap.Height;

                        using (Image image = SizeImageWithOldPercent(bitmap, (int)(bitmap.Width * rate), _defaultPixel))
                        {
                            image.Save(tempPath, ImageFormat.Jpeg);
                            GetPicThumbnail(tempPath, savaImgPath, 80);
                        }
                        //上面的压缩的更少
                        //ImgHelper.GetPicThumbnail(ImgHelper.SizeImageWithOldPercent(bitmap, _defaultPixel, (int)(bitmap.Width * rate)), savaImgPath, 80);
                    }



                }
                else
                {
                    using (Image image = bitmap)
                    {
                        image.Save(tempPath, ImageFormat.Jpeg);
                        //直接压缩质量
                        GetPicThumbnail(tempPath, savaImgPath, 80);
                    }
                }


                File.Delete(tempPath);

            }
            // });
        }


        /// <summary> 
        /// jpeg图片压缩  
        /// </summary> 
        /// <param name="sFile"></param> 
        /// <param name="outPath"></param> 
        /// <param name="flag"></param> 
        /// <returns></returns> 
        public static bool GetPicThumbnail(string sFile, string outPath, int flag)
        {
            System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
            ImageFormat tFormat = iSource.RawFormat;
            //以下代码为保存图片时，设置压缩质量 
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100 
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    iSource.Save(outPath, jpegICIinfo, ep);//dFile是压缩后的新路径 
                }
                else
                {
                    iSource.Save(outPath, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                iSource.Dispose();
            }
        }


        /// <summary> 
        /// 按照指定大小缩放图片，但是为了保证图片宽高比自动截取 
        /// </summary> 
        /// <param name="srcImage">原图</param> 
        /// <param name="iWidth"></param> 
        /// <param name="iHeight"></param> 
        /// <returns></returns> 
        public static Bitmap SizeImageWithOldPercent(Image srcImage, int iWidth, int iHeight)
        {
            try
            {
                // 要截取图片的宽度（临时图片） 
                int newW = srcImage.Width;
                // 要截取图片的高度（临时图片） 
                int newH = srcImage.Height;
                // 截取开始横坐标（临时图片） 
                int newX = 0;
                // 截取开始纵坐标（临时图片） 
                int newY = 0;
                // 截取比例（临时图片） 
                double whPercent = 1;
                whPercent = ((double)iWidth / (double)iHeight) * ((double)srcImage.Height / (double)srcImage.Width);
                if (whPercent > 1)
                {
                    // 当前图片宽度对于要截取比例过大时 
                    newW = int.Parse(Math.Round(srcImage.Width / whPercent).ToString());
                }
                else if (whPercent < 1)
                {
                    // 当前图片高度对于要截取比例过大时 
                    newH = int.Parse(Math.Round(srcImage.Height * whPercent).ToString());
                }
                if (newW != srcImage.Width)
                {
                    // 宽度有变化时，调整开始截取的横坐标 
                    newX = Math.Abs(int.Parse(Math.Round(((double)srcImage.Width - newW) / 2).ToString()));
                }
                else if (newH == srcImage.Height)
                {
                    // 高度有变化时，调整开始截取的纵坐标 
                    newY = Math.Abs(int.Parse(Math.Round(((double)srcImage.Height - (double)newH) / 2).ToString()));
                }
                // 取得符合比例的临时文件 
                Bitmap cutedImage = CutImage(srcImage, newX, newY, newW, newH);
                // 保存到的文件 
                Bitmap b = new Bitmap(iWidth, iHeight);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量 
                // g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.InterpolationMode = InterpolationMode.Default;
                g.DrawImage(cutedImage, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(0, 0, cutedImage.Width, cutedImage.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                srcImage.Dispose();
            }
        }



        /// <summary> 
        /// 剪裁 -- 用GDI+ 
        /// </summary> 
        /// <param name="b">原始Bitmap</param> 
        /// <param name="StartX">开始坐标X</param> 
        /// <param name="StartY">开始坐标Y</param> 
        /// <param name="iWidth">宽度</param> 
        /// <param name="iHeight">高度</param> 
        /// <returns>剪裁后的Bitmap</returns> 
        private static Bitmap CutImage(Image b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                // 开始截取坐标过大时，结束处理 
                return null;
            }
            if (StartX + iWidth > w)
            {
                // 宽度过大时只截取到最大大小 
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                // 高度过大时只截取到最大大小 
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight);
                Graphics g = Graphics.FromImage(bmpOut);

                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
            finally
            {
                b.Dispose();
            }
        }

    }
}
