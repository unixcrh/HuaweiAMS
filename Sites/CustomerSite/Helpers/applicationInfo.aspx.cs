using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CutomerSite.Helpers
{
    public partial class applicationInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.ContentType = "appliaiton/javascript";

            string script = string.Format("var applicationRoot = \"{0}\";\n",
                    UriHelper.MakeAbsolute(new Uri("/", UriKind.RelativeOrAbsolute), this.Request.Url));

            this.Response.Write(script);
            this.Response.End();
        }
    }
}