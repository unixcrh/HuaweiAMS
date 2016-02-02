using MCS.Library.Cloud.W3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CutomerSite.W3
{
    public partial class SamlRequestTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string urn = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST";
            string returnUrl = string.Format("http://amshuawei-customer.azurewebsites.net/w3/SamlResponse.aspx?binding={0}",
                HttpUtility.UrlEncode(urn));

            byte[] samlReq = Encoding.UTF8.GetBytes(SamlHelper.GetSignedRequestDoc("www.huaweiams.com", returnUrl).OuterXml);
            SAMLRequest.InnerText = Convert.ToBase64String(samlReq);
        }
    }
}