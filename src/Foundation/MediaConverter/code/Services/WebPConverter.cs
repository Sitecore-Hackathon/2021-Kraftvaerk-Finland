using System;
using System.IO;
using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;

namespace SC2021KF.Foundation.MediaConverter.Services
{
    public class WebPConverter
    {
        /// <summary>
        /// Convert file to webp format
        /// </summary>
        /// <param name="fileBytes"></param>
        /// <param name="filePath"></param>
        public static byte[] ConvertToWebP(byte[] fileBytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                    {
                        imageFactory.Load(fileBytes)
                                    .Format(new WebPFormat())
                                    .Quality(100)
                                    .Save(ms);
                    }
                    return ms.ToArray();
                }
            }
            catch (Exception e)
            {
                Sitecore.Diagnostics.Log.Error("Unable to convert bytearray to webpformat", e, typeof(WebPConverter));
            }
            return null;
        }
    }
}