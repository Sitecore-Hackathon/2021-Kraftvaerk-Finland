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
using Sitecore;
using Sitecore.SecurityModel;

namespace SC2021KF.Feature.MediaHandler.Pipelines
{
    public class MediaWebPHandler
    {
        private readonly string convertwebp = "convertwebp";

        public void EndUpLoad()
        {

        }
        public void Process(UploadArgs args)
        {
            Database db = Sitecore.Context.ContentDatabase;

            Assert.ArgumentNotNull((object)args, nameof(args));
            if (HttpContext.Current.Request.Form[convertwebp] == "on") // Checkbox has been pressed to save as WebP format
            {
                for (int index = 0; index < args.Files.Count; ++index)
                {
                    try
                    {

                    
                    HttpPostedFile file = args.Files[index];
                    byte[] webPMedia = null;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);

                        webPMedia = ms.ToArray();
                    }

                    Item item = db.GetItem(args.Folder);
                    var webPMediaConverted = WebPConverter.Convert(webPMedia);
                    System.IO.Stream streamData = new System.IO.MemoryStream(webPMediaConverted);
                    
                    var filePathName = file.FileName.Substring(0, file.FileName.IndexOf('.'));

                    var options = new MediaCreatorOptions
                    {
                        IncludeExtensionInItemName = false,
                        Destination = item.Paths.Path + "/" + filePathName,
                        Database = db,
                        Language = args.Language,
                        Versioned = false,
                        AlternateText = filePathName
                    };
                    using (new SecurityDisabler())
                    {
                        var newItem = MediaManager.Creator.CreateFromStream(streamData, filePathName + ".webp", options);
                        args.UploadedItems.Add(newItem);
                    }
                    }
                    catch(Exception e)
                    {
                        throw;
                    }
                }

                args.CloseDialogOnEnd = true;
                args.AbortPipeline();
            }
        }
    }
}