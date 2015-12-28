using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Res = MCS.Web.Responsive.Library;

namespace CutomerSite
{
    public class Global : System.Web.HttpApplication
    {
        public override void Init()
        {
            this.AuthenticateRequest += new EventHandler(Global_AuthenticateRequest);

            base.Init();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Global_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = this.Request.Cookies.Get("login_uid");

            if (cookie != null)
            {
                GenericIdentity identity = new GenericIdentity(cookie.Value);

                GenericPrincipal pricipal = new GenericPrincipal(identity, new string[0]);

                HttpContext.Current.User = pricipal;
            }

            double minuteOffset = Res.Request.GetRequestQueryValue("timeOffset", 0.0d);

            TimeZoneContext.Current.CurrentTimeZone =
                TimeZoneInfo.CreateCustomTimeZone("TimeZoneInfoContext", TimeSpan.FromMinutes(minuteOffset), "TimeZoneInfoContext", "TimeZoneInfoContext");

            HttpCookie resCookie = new HttpCookie("res_cookie1", DateTime.Now.ToString());
            resCookie.Expires = DateTime.MinValue;
            HttpContext.Current.Response.SetCookie(resCookie);

            //HttpCookie resCookie2 = new HttpCookie("res_cookie2", DateTime.Now.ToString());
            //resCookie2.Expires = DateTime.MinValue;
            //HttpContext.Current.Response.SetCookie(resCookie2);
        }
    }
}