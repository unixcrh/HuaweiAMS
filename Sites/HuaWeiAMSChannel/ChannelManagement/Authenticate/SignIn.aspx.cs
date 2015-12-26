using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChannelManagement.Authenticate
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void SignInButton_Click(object sender, EventArgs e)
        {
            try
            {
                string logonName = this.signInName.Text.Trim();

                AMSAdmin user = AMSAdminSqlAdapter.Instance.CheckPassword(logonName, this.password.Value);

                if (user == null)
                    throw new ApplicationException("用户名或密码不正确");

                WriteCookie(user, timeOffset.Value);

                Response.Redirect(this.Context.Request.QueryString["ReturnUrl"]);
            }
            catch (System.Exception ex)
            {
                this.errorMessage.Text = HttpUtility.HtmlEncode(ex.Message);
            }
        }

        private static void WriteCookie(AMSAdmin user, string timeOffsetValue)
        {
            SessionAuthenticationModule sam = (SessionAuthenticationModule)
                HttpContext.Current.ApplicationInstance.Modules["SessionAuthenticationModule"];

            IClaimsPrincipal principal =
               new ClaimsPrincipal(new GenericPrincipal(new GenericIdentity(user.LogonName), null));

            principal.Identities[0].Claims.Add(new Claim("TimeOffset", timeOffsetValue));
            principal.Identities[0].Claims.Add(new Claim("AMSAdminID", user.UserID));
            principal.Identities[0].Claims.Add(new Claim("AMSAdminName", user.Name));

            SessionSecurityToken token = sam.CreateSessionSecurityToken(principal, null, DateTime.Now, DateTime.Now.AddMinutes(20), false);

            sam.WriteSessionTokenToCookie(token);
        }
    }
}