using MCS.Library.Core;
using Microsoft.IdentityModel.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ChannelManagement
{
    public class Global : System.Web.HttpApplication
    {
        public override void Init()
        {
            this.PostAuthenticateRequest += Global_PostAuthenticateRequest;
            base.Init();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
        }

        private void Global_PostAuthenticateRequest(object sender, EventArgs e)
        {
            ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;

            if (principal != null)
            {
                ClaimsIdentity identity = principal.Identity as ClaimsIdentity;

                if (identity != null)
                {
                    Claim claim = identity.Claims.FirstOrDefault(c => c.ClaimType == "TimeOffset");

                    if (claim != null && claim.Value.IsNotEmpty())
                    {
                        TimeZoneContext.Current.CurrentTimeZone =
                            TimeZoneInfo.CreateCustomTimeZone("TimeZoneInfoContext",
                            TimeSpan.FromMinutes(Convert.ToDouble(claim.Value)),
                            "TimeZoneInfoContext", "TimeZoneInfoContext");
                    }
                }
            }
        }
    }
}