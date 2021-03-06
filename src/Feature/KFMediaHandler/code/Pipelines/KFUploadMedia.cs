using Sitecore;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SC2021KF.Feature.MediaHandler.Pipelines
{
    public class KFUploadMedia : Sitecore.Shell.Applications.Media.UploadMedia.UploadMediaForm
    {
        protected override void OnOK(object sender, EventArgs args)
        {
            string str = Context.ClientPage.ClientRequest.Form["convertwebp"];
//            Context.ClientPage.

        }
    }
}