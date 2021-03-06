using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC2021KF.Feature.MediaHandler.Pipelines
{
    public class MediaWebPHandler
    {
        public void Process(UploadArgs args)
        {
            Database db = Sitecore.Context.ContentDatabase;
            Item webPMediaTemplate = db.GetItem("/sitecore/templates/jotain/jotain");

            ID unversionedImage = new ID("");
            ID versionedImage = new ID("");
            ID unversionedJpeg = new ID("");
            ID versionedPjeg = new ID("");

            ID webPBlobRow = new ID(""); //tämä pitää varmaan olla file-tyyppinen eikä attachment

            foreach (Item mediaItem in args.UploadedItems)
            {
                //jos kuva
                if (mediaItem.TemplateID == unversionedImage || mediaItem.TemplateID == versionedImage || mediaItem.TemplateID == unversionedJpeg || mediaItem.TemplateID == versionedPjeg)
                {
                    //kutsutaan teemun koodia että saadaan webP muotoon itemi
                    var webPMedia = funktio(mediaItem["Blob"]);
                    mediaItem.Editing.BeginEdit();
                    mediaItem["WebPFile"] = webPMedia;
                    mediaItem.Editing.EndEdit();
                }
            }
        }
    }
}