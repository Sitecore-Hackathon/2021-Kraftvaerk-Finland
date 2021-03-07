using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using SC2021KF.Foundation.MediaConverter.Services;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SC2021KF.Feature.MediaHandler.Services
{
    public class WebpThumbnailService : ThumbnailGenerator
    {   public override MediaStream GetStream(MediaData mediaData, TransformationOptions options)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Stream stream = new MemoryStream();
                WebPConverter.ResizeImage(mediaData.MediaItem.GetMediaStream(), stream, 150, 150, 85);

                return new MediaStream(stream, this.Extension, mediaData.MediaItem);
            }
        }
    }
}