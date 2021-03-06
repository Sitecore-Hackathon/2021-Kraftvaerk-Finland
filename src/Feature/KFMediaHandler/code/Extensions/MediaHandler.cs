using System.Web;
using Sitecore.Resources.Media;

namespace SC2021KF.Feature.MediaHandler.Extensions
{
    public class MediaHandler : MediaRequestHandler
    {
        public override void ProcessRequest(HttpContext context)
        {
            var mediaRequest = MediaManager.ParseMediaRequest(context.Request);

            if (mediaRequest != null)
            {
                var media = MediaManager.GetMedia(mediaRequest.MediaUri);

                if (media != null)
                {
                    var mediaItem = media.MediaData.MediaItem;
                    if(mediaItem.InnerItem?.Fields["No-Index"]?.ToString() == "1")
                    {
                        context.Response.AppendHeader("X-Robots-Tag", "noindex");
                    }
                }
            }

            base.ProcessRequest(context);
        }
    }
}