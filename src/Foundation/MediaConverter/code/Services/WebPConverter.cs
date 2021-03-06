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
        public static void ConvertToWebP(byte[] fileBytes, string filePath)
        {
            try
            {
                using (Stream fs = new FileStream(filePath, FileMode.Create))
                {
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                    {
                        imageFactory.Load(fileBytes)
                                    .Format(new WebPFormat())
                                    .Quality(100)
                                    .Save(fs);
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }
        }
    }
}