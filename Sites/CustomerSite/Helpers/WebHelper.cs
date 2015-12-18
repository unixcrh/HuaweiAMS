using CutomerSite.services;
using MCS.Library.Core;
using MCS.Web.Responsive.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CutomerSite.Helpers
{
    public static class WebHelper
    {
        public static void RegisterApplicationRoot(this Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(),
                "ApplicationRoot",
                string.Format("var applicationRoot = \"{0}\";",
                    UriHelper.MakeAbsolute(new Uri("/", UriKind.RelativeOrAbsolute), page.Request.Url)),
                true);
        }

        public static VideoAddressType GetVideoAddressType()
        {
            return Request.GetRequestQueryValue("videoAddressType", VideoAddressType.Default);
        }
    }
}