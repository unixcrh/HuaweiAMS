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
    public partial class SignInBridge : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loginForm.Action = W3Settings.GetSettings().SignInUri;

            W3IssuerConfigurationElement issuerElement = W3Settings.GetSettings().GetSelectedIssuer();

            string xml = SamlHelper.GetSignedRequestDoc(issuerElement.IssuerID, string.Empty).OuterXml;

            byte[] samlReq = Encoding.UTF8.GetBytes(xml);
            SAMLRequest.InnerText = Convert.ToBase64String(samlReq);
        }
    }
}