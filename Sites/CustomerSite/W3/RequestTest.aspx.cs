using MCS.Library.Cloud.W3;
using MCS.Library.Cloud.W3.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CutomerSite.W3
{
    public partial class RequestTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.User != null && this.User.Identity != null)
                identity.Text = this.User.Identity.Name;

            W3IssuerConfigurationElement issuerElement = W3Settings.GetSettings().GetSelectedIssuer();

            byte[] samlReq = Encoding.UTF8.GetBytes(SamlHelper.GetSignedRequestDoc(issuerElement.IssuerID, string.Empty, this.Request.Url.ToString()).OuterXml);
            SAMLRequest.InnerText = Convert.ToBase64String(samlReq);
        }
    }
}