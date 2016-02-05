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
            InitTimeOffset();
            //ProcessLoginCookie();
        }

        private static void InitTimeOffset()
        {
            double minuteOffset = Res.Request.GetRequestQueryValue("timeOffset", 0.0d);

            TimeZoneContext.Current.CurrentTimeZone =
                TimeZoneInfo.CreateCustomTimeZone("TimeZoneInfoContext", TimeSpan.FromMinutes(minuteOffset), "TimeZoneInfoContext", "TimeZoneInfoContext");
        }

        private static void ProcessLoginCookie()
        {
            string userName = string.Empty;

            HttpCookie lepusCookie = HttpContext.Current.Request.Cookies.Get("login_uid");

            if (lepusCookie != null)
                userName = lepusCookie.Value;

            if (HttpContext.Current.User == null || HttpContext.Current.User.Identity.Name.IsNullOrEmpty())
            {
                HttpCookie amsCookie = HttpContext.Current.Request.Cookies.Get("ams_login_uid");

                if (amsCookie != null)
                    userName = amsCookie.Value;
            }

            if (userName.IsNullOrEmpty())
                userName = UuidHelper.NewUuidString();

            CreatePrincipal(userName);

            HttpCookie amsResCookie = new HttpCookie("ams_login_uid", userName);
            amsResCookie.Expires = DateTime.MinValue;

            HttpContext.Current.Response.Cookies.Add(amsResCookie);
        }

        private static void CreatePrincipal(string userName)
        {
            GenericIdentity identity = new GenericIdentity(userName);

            GenericPrincipal pricipal = new GenericPrincipal(identity, new string[0]);

            HttpContext.Current.User = pricipal;
        }
    }
}