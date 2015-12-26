using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Core;
using MCS.Web.Responsive.Library;
using MCS.Web.Responsive.Library.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChannelManagement.Authenticate
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebUtility.RequiredScript(typeof(ClientMsgResources));

            if (this.IsPostBack == false && this.IsCallback == false)
            {

                if (this.Request.UrlReferrer != null)
                    this.BackUrl = this.Request.UrlReferrer.ToString();
                else
                    this.BackUrl = "#";
            }

            this.signInName.Text = GetLogonName();
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.backUrl.HRef = this.BackUrl;

            base.OnPreRender(e);
        }

        private string BackUrl
        {
            get
            {
                return this.ViewState.GetViewStateValue("BackUrl", string.Empty);
            }
            set
            {
                this.ViewState.SetViewStateValue("BackUrl", value);
            }
        }

        protected void changePasswordButton_Click(object sender, EventArgs e)
        {
            try
            {
                AMSAdminSqlAdapter.Instance.SetPassword(GetLogonName(), this.changedPassword.Value);

                this.ClientScript.RegisterStartupScript(this.GetType(), "back",
                    string.Format("document.getElementById(\"{0}\").click();", this.backUrl.ClientID), true);
            }
            catch (System.Exception ex)
            {
                this.errorMessage.Text = HttpUtility.HtmlEncode(ex.Message);
            }
        }

        private static string GetLogonName()
        {
            string logonName = string.Empty;

            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity != null)
                logonName = HttpContext.Current.User.Identity.Name;

            return logonName;
        }
    }
}