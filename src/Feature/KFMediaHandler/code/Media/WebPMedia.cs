using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC2021KF.Feature.MediaHandler.Media
{
    public class WebPMedia : Sitecore.Resources.Media.ImageMedia
    {
        public override Sitecore.Resources.Media.Media Clone()
        {
            return new WebPMedia();
        }
        public override void UpdateMetaData(MediaStream mediaStream)
        {
            return;
        }
    }
}