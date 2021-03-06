using System;
using System.Web;
using Sitecore.Data;
using Sitecore.Resources.Media;

namespace SC2021KF.Feature.MediaHandler.Extensions
{
    public class MediaHandler : MediaRequestHandler
    {
        private readonly string xRobotsTagHeader = "X-Robots-Tag: ";
        private readonly ID NoIndexID = new ID("{8FD6694C-EF8A-4419-8126-6332E9BCDF8A}");
        private readonly ID NoFollowID = new ID("{D19C90EA-1101-46A3-B47C-FA50A7D48D7D}");
        public override void ProcessRequest(HttpContext context)
        {
            try
            {
                var mediaRequest = MediaManager.ParseMediaRequest(context.Request);

                if (mediaRequest != null)
                {
                    var media = MediaManager.GetMedia(mediaRequest.MediaUri);

                    if (media != null)
                    {
                        var mediaItem = media.MediaData.MediaItem;
                        string value = null;

                        if (mediaItem?.InnerItem?.Fields[NoIndexID]?.ToString() == "1")
                        {
                            value = "noindex";
                        }

                        if (mediaItem?.InnerItem?.Fields[NoFollowID]?.ToString() == "1")
                        {
                            value = value != null ? value + ", nofollow" : "nofollow";
                        }

                        if (value != null)
                        {
                            context.Response.AppendHeader(xRobotsTagHeader, value);
                        }
                    }
                }
            }
            catch(Exception e)
            {
                Sitecore.Diagnostics.Log.Error("Unable to evaluate SEO fields for media item", e, typeof(MediaHandler));
            }

            base.ProcessRequest(context);
        }
    }
}