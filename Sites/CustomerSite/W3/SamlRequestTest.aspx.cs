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
            string returnUrl = this.Request.Url.ToString();

            string xml = SamlHelper.GetSignedRequestDoc("www.huaweiams.com", returnUrl).OuterXml;
            SAMLRequestXml.InnerText = xml;

            byte[] samlReq = Encoding.UTF8.GetBytes(xml);
            SAMLRequest.InnerText = Convert.ToBase64String(samlReq);

            privateCAInfo.InnerText = SamlHelper.GetEmbededPrivateCertificate().ToString();
        }
    }
}