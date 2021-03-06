using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SC2021KF.Feature.MediaHandler.Services
{
    public class WebpThumbnailService : ThumbnailGenerator
    {
        public override MediaStream GetStream(MediaData mediaData, TransformationOptions options)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                Stream stream = new MemoryStream();
                using(ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                {
                    imageFactory.Load(mediaData.MediaItem.GetMediaStream()).Resize(new System.Drawing.Size(150, 150))
                        .Format(new WebPFormat() { Quality = 85 }).Save(stream);
                }

                return new MediaStream(stream, this.Extension, mediaData.MediaItem);
            }
        }
    }
}