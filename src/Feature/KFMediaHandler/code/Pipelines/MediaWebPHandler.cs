using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Pipelines.Upload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SC2021KF.Foundation.MediaConverter.Services;
using Sitecore.Diagnostics;
using System.IO;
using Sitecore.Data.Fields;
using System.Runtime.Serialization.Formatters.Binary;
using Sitecore.Resources.Media;

namespace SC2021KF.Feature.MediaHandler.Pipelines
{
    public class MediaWebPHandler
    {
        private readonly string convertwebp = "convertwebp";
        public void Process(UploadArgs args)
        {
            Assert.ArgumentNotNull((object)args, nameof(args));
            if (HttpContext.Current.Request.Form[convertwebp] == "on") // Checkbox has been pressed to save as WebP format
            Database db = Sitecore.Context.ContentDatabase;
            Item webPMediaTemplate = db.GetItem("/sitecore/templates/jotain/jotain");

            ID unversionedImage = new ID("{F1828A2C-7E5D-4BBD-98CA-320474871548}");
            ID versionedImage = new ID("{C97BA923-8009-4858-BDD5-D8BE5FCCECF7}");
            ID unversionedJpeg = new ID("{DAF085E8-602E-43A6-8299-038FF171349F}");
            ID versionedPjeg = new ID("{EB3FB96C-D56B-4AC9-97F8-F07B24BB9BF7}");

            ID webPBlobRow = new ID(""); //tämä pitää varmaan olla file-tyyppinen eikä attachment

            foreach (Item mediaItem in args.UploadedItems)
            {
                //TODO convert image to webp and save to media library



                args.AbortPipeline();
                if (!args.CloseDialogOnEnd)
                    return;
                string parameter = args.Parameters["message"];
                string fileName = HttpUtility.UrlEncode(args.Properties["filename"] as string ?? string.Empty);
                //jos kuva
                if (mediaItem.TemplateID == unversionedImage || mediaItem.TemplateID == versionedImage || mediaItem.TemplateID == unversionedJpeg || mediaItem.TemplateID == versionedPjeg)
                {
                    //ImageField imageField = mediaItem.Fields["Image"];
                    //MediaItem mediaItem2 = imageField.MediaItem;
                    //Stream stream = mediaItem2.GetMediaStream();
                    //Byte[] bytes = new Byte[stream.Length];
                    //stream.Read(bytes, 0, bytes.Length);
                    //img64.Src = "data:" + mediaItem2.MimeType + ";base64," + Convert.ToBase64String(bytes);
                args.UiResponseHandlerEx.UploadDone(fileName, parameter);
                // we abort pipeline because we handle the saving to media library as WebP
                
            

                    byte[] webPMedia = null;
                    BinaryFormatter bf = new BinaryFormatter();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bf.Serialize(ms, mediaItem.Fields["Blob"]);
                        webPMedia = ms.ToArray();
                    }


                    //kutsutaan teemun koodia että saadaan webP muotoon itemi
                    var webPMediaConverted = WebPConverter.ConvertToWebP(webPMedia);
                    Stream stream = new MemoryStream(webPMediaConverted);
                    var asfsdgrt = MediaManager.Creator.CreateFromStream(stream, mediaItem.Paths.Path, new MediaCreatorOptions());
                    var asd = MediaManager.Creator.AttachStreamToMediaItem(stream, "mediaItemPath", "", null);
                    //MediaItem mediaItem3 = asd;
                    mediaItem.Editing.BeginEdit();
                    //mediaItem["WebPFile"] = asfsdgrt;
                    mediaItem.Editing.EndEdit();
                }
            }
        }
    }
}