using Sitecore.Data;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC2021KF.Foundation.XRobotsTag.Services
{
    public static class XRobotsTagHeaderAppender
    {
        private static readonly ID NoIndexID = new ID("{8FD6694C-EF8A-4419-8126-6332E9BCDF8A}");
        private static readonly ID NoFollowID = new ID("{D19C90EA-1101-46A3-B47C-FA50A7D48D7D}");
        public static string CheckXRobotsTagValues(MediaItem mediaItem)
        {
            string value = null;

            if (mediaItem?.InnerItem?.Fields[NoIndexID]?.ToString() == "1")
            {
                value = "noindex";
            }

            if (mediaItem?.InnerItem?.Fields[NoFollowID]?.ToString() == "1")
            {
                value = value != null ? value + ", nofollow" : "nofollow";
            }

            return value;
        }
    }
}