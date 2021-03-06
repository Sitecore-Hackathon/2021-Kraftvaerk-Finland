using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Pipelines.HttpRequest;

namespace SC2021KF.Feature.XRobotsTag.Pipelines
{
    public class XRobotsTagHeaderAppender : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            Sitecore.Diagnostics.Log.Info("XRobotsTagHeaderAppender", typeof(XRobotsTagHeaderAppender));
        }
    }
}