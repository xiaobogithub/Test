using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System;
using System.Net;

namespace Ethos.Utility
{
    public static class ImageUtility
    {
        public static Stream CreateThumnailImage(Image image, int width, int height)
        {
            Stream thumnailStream = new MemoryStream();
            if (width != 0 && height != 0)
            {
                int thumbnailHeight = height;
                int thumbnailWidth = width;
                int intwidth, intheight;

                if (image.Height > thumbnailHeight)
                {
                    if (image.Width * thumbnailHeight > image.Height * thumbnailWidth)
                    {
                        intwidth = thumbnailWidth;
                        intheight = (image.Height * thumbnailWidth) / image.Width;
                    }
                    else
                    {
                        intheight = thumbnailHeight;
                        intwidth = (image.Width * thumbnailHeight) / image.Height;
                    }
                }
                else
                {
                    if (image.Width > thumbnailWidth)
                    {
                        intwidth = thumbnailWidth;
                        intheight = (image.Height * thumbnailWidth) / image.Width;
                    }
                    else
                    {
                        intwidth = image.Width;
                        intheight = image.Height;
                    }
                }

                //bulid a bitmap image with specific width and height
                using (Bitmap bitmap = new Bitmap(thumbnailWidth, thumbnailHeight, image.PixelFormat))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(ColorTranslator.FromHtml("#F2F2F2"));
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        //start to draw image
                        g.DrawImage(image, new Rectangle((thumbnailWidth - intwidth) / 2, (thumbnailHeight - intheight) / 2, intwidth, intheight), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

                        bitmap.Save(thumnailStream, image.RawFormat);
                        //CreateNewImageFile(fileGuid, fileName, bitmap);
                    }
                }
            }
            thumnailStream.Position = 0;
            return thumnailStream;
        }

        public static Stream CropImage(string normalurl, string bigimageurl, int Width, int Height, int X, int Y)
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(normalurl);
            WebResponse myResp = myReq.GetResponse();
            Stream norstream = myResp.GetResponseStream();
            Image normalimage = Image.FromStream(norstream);

            Image oriimage = normalimage;
            if (!string.IsNullOrEmpty(bigimageurl))
            {
                HttpWebRequest myReq1 = (HttpWebRequest)WebRequest.Create(bigimageurl);
                WebResponse myResp1 = myReq1.GetResponse();
                Stream oristream = myResp1.GetResponseStream();
                oriimage = Image.FromStream(oristream);
                float times = (float)oriimage.Height / normalimage.Height;

                Width = (int)(Width * times);
                Height = (int)(Height * times);
                X = (int)(X * times);
                Y = (int)(Y * times);
            }

            byte[] CropImage = Crop(oriimage, Width, Height, X, Y);
            Stream stream = new MemoryStream(CropImage);

            return stream;
        }

        static byte[] Crop(Image OriginalImage, int Width, int Height, int X, int Y)
        {
            try
            {
                using (Bitmap bmp = new Bitmap(Width, Height))
                {
                    bmp.SetResolution(Width, Height);

                    using (Graphics Graphic = Graphics.FromImage(bmp))
                    {
                        Graphic.SmoothingMode = SmoothingMode.AntiAlias;

                        Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, Width, Height), X, Y, Width, Height, GraphicsUnit.Pixel);

                        MemoryStream ms = new MemoryStream();

                        bmp.Save(ms, ImageFormat.Jpeg);
                        return ms.GetBuffer();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }

        static byte[] Crop(Stream iamgeStream, int Width, int Height, int X, int Y)
        {
            try
            {
                using (Image OriginalImage = Image.FromStream(iamgeStream))
                {
                    using (Bitmap bmp = new Bitmap(Width, Height))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);

                        using (Graphics Graphic = Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;

                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, Width, Height), X, Y, Width, Height, GraphicsUnit.Pixel);

                            MemoryStream ms = new MemoryStream();

                            bmp.Save(ms, ImageFormat.Jpeg);

                            return ms.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }

        public static Stream GetResizeImageFile(Stream imageFile, int targetHeight)
        {
            Stream stream = new MemoryStream(ResizeImageFile(imageFile, targetHeight));
            return stream;
        }

        private static byte[] ResizeImageFile(Stream imageFile, int targetHeight)
        {
            int targetH, targetW;
            using (Image OriginalImage = Image.FromStream(imageFile))
            {
                targetH = targetHeight;
                targetW = (int)(OriginalImage.Width * ((float)targetHeight / (float)OriginalImage.Height));
                using (Bitmap bmp = new Bitmap(targetW, targetH))
                {
                    bmp.SetResolution(targetW, targetH);

                    using (Graphics Graphic = Graphics.FromImage(bmp))
                    {
                        Graphic.SmoothingMode = SmoothingMode.AntiAlias;

                        Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        //Graphic.DrawImage(OriginalImage, new Rectangle(-1, -1, targetW+2, targetH+2), 0, 0, OriginalImage.Width, OriginalImage.Height, GraphicsUnit.Pixel);
                        Graphic.DrawImage(OriginalImage, -1, -1, targetW + 2, targetH + 2);

                        MemoryStream ms = new MemoryStream();

                        bmp.Save(ms,ImageFormat.Jpeg);

                        return ms.GetBuffer();
                    }
                }
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
    }
}
