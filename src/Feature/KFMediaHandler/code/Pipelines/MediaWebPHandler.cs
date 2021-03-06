using Sitecore.Data;
using Sitecore.Pipelines.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace SC2021KF.Feature.MediaHandler.Pipelines
{
    public class MediaWebPHandler
    {
        private readonly string convertwebp = "convertwebp";
        public void Process(UploadArgs args)
        {
            Assert.ArgumentNotNull((object)args, nameof(args));
            if (HttpContext.Current.Request.Form[convertwebp] == "on") // Checkbox has been pressed to save as WebP format
            {
                //TODO convert image to webp and save to media library



                args.AbortPipeline();
                if (!args.CloseDialogOnEnd)
                    return;
                string parameter = args.Parameters["message"];
                string fileName = HttpUtility.UrlEncode(args.Properties["filename"] as string ?? string.Empty);
                args.UiResponseHandlerEx.UploadDone(fileName, parameter);
                // we abort pipeline because we handle the saving to media library as WebP
                
            }
            
            //Item webPMediaTemplate = db.GetItem("/sitecore/templates/jotain/jotain");

            //ID unversionedImage = new ID("");
            //ID versionedImage = new ID("");
            //ID unversionedJpeg = new ID("");
            //ID versionedPjeg = new ID("");

            //ID webPBlobRow = new ID(""); //tämä pitää varmaan olla file-tyyppinen eikä attachment

            //foreach (Item mediaItem in args.UploadedItems)
            //{
            //    //jos kuva
            //    if (mediaItem.TemplateID == unversionedImage || mediaItem.TemplateID == versionedImage || mediaItem.TemplateID == unversionedJpeg || mediaItem.TemplateID == versionedPjeg)
            //    {
            //        //kutsutaan teemun koodia että saadaan webP muotoon itemi
            //        //var webPMedia = funktio(mediaItem["Blob"]);
            //        //mediaItem.Editing.BeginEdit();
            //        //mediaItem["WebPFile"] = webPMedia;
            //        //mediaItem.Editing.EndEdit();
            //    }
            //}
        }
    }
}