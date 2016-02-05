using MCS.Library.Cloud.W3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CutomerSite.W3
{
    public partial class LocalResponse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["SAMLResponse"] != null)
            {
                byte[] responseData = Convert.FromBase64String(Request.Form["SAMLResponse"]);
                string xmlString = Encoding.UTF8.GetString(responseData);

                SAMLResponse.InnerText = xmlString;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.PreserveWhitespace = true;
                xmlDoc.LoadXml(xmlString);

                SamlResponseResult result = SamlHelper.CheckAndGetUserIDResponseDoc(xmlDoc);

                this.ResponseUserID.Text = result.UserID;
                FormsAuthentication.SetAuthCookie(result.UserID, false);

                this.Response.Redirect("../list/AllEvents.aspx");
            }
        }
    }
}