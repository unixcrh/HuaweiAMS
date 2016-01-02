using CutomerSite.Helpers;
using CutomerSite.services;
using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Web.Responsive.Library.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CutomerSite.forms
{
    public partial class NoMuiEventPlayer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ControllerHelper.ExecuteMethodByRequest(this);

            string agentText = this.Request.UserAgent;
            //userAgent.InnerText = agentText + " login_uid: ";

            //if (this.User != null)
            //    userAgent.InnerText += this.User.Identity.Name;

            //userAgent.InnerHtml += "<br/>" + GetCoookies().Replace("\n", "<br/>");
            //allCookies.InnerHtml = "AllCookies: <br/>";

            //if (this.Request.Headers["Cookie"] != null)
            //    allCookies.InnerHtml += this.Request.Headers["Cookie"].Replace(";", "<br/>");

            if (agentText.IndexOf("android 5", StringComparison.OrdinalIgnoreCase) >= 0 ||
                agentText.IndexOf("android 6", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                this.fixedBitrate.Value = "true";
            }
            else
            {
                this.fixedBitrate.Value = "false";
            }
        }

        private AMSChannel Channel
        {
            get;
            set;
        }

        private AMSEvent Event
        {
            get;
            set;
        }

        //private static string GetCoookies()
        //{
        //    StringBuilder strB = new StringBuilder();
        //    foreach (string key in HttpContext.Current.Request.Cookies.AllKeys)
        //    {
        //        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);

        //        strB.AppendFormat("Name={0}, Value={1}\n", cookie.Name, cookie.Value);
        //    }

        //    return strB.ToString();
        //}

        [ControllerMethod]
        protected void InitByEventID(string id)
        {
            if (id.IsNotEmpty())
            {
                AMSEvent eventData = DataHelper.GetEventByID(id);

                if (eventData != null)
                {
                    this.Channel = AMSChannelSqlAdapter.Instance.LoadByID(eventData.ChannelID);
                    this.pageEventData.Value = DataHelper.GetSingleEventJson(this.Channel, eventData, WebHelper.GetVideoAddressType());
                    this.videoTitle.Text = HttpUtility.HtmlEncode(eventData.Name);

                    DataHelper.UpdateUserView(id);
                }

                this.Event = eventData;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.RegisterApplicationRoot();
            this.InitAlternateCDNAddress(this.Channel, this.Event);

            //VideoAddressType videoAddressType = WebHelper.GetVideoAddressType();

            //this.videoAddressType.Value = videoAddressType.ToString();

            //VideoAddressType targetType = VideoAddressType.Mooncake;

            //string buttonText = string.Empty;

            //switch (videoAddressType)
            //{
            //    case VideoAddressType.Default:
            //        targetType = VideoAddressType.Mooncake;
            //        buttonText = "切换到中国CDN";
            //        break;
            //    case VideoAddressType.Mooncake:
            //        targetType = VideoAddressType.Default;
            //        buttonText = "切换到默认CDN";
            //        break;
            //}

            //switchVideoAddressType.HRef = UriHelper.ReplaceUriParams(this.Request.Url.ToString(),
            //    parameters => parameters["videoAddressType"] = targetType.ToString());
            //switchVideoAddressType.InnerText = buttonText;

            base.OnPreRender(e);
        }

        private void InitAlternateCDNAddress(AMSChannel channel, AMSEvent eventData)
        {
            if (channel != null && eventData != null && channel.AlternateCDNEndpoint.IsNotEmpty())
            {
                VideoAddressType videoAddressType = WebHelper.GetVideoAddressType();
                VideoAddressType targetType = VideoAddressType.AlternateCDN;
                string buttonText = this.switchVideoAddressType.InnerText;

                switch (videoAddressType)
                {
                    case VideoAddressType.Default:
                        targetType = VideoAddressType.AlternateCDN;
                        buttonText = "切换到备用CDN";
                        break;
                    case VideoAddressType.AlternateCDN:
                        targetType = VideoAddressType.Default;
                        buttonText = "切换到默认地址";
                        break;
                }

                this.switchVideoAddressType.HRef = UriHelper.ReplaceUriParams(this.Request.Url.ToString(),
                    parameters => parameters["videoAddressType"] = targetType.ToString());
                this.switchVideoAddressType.InnerText = buttonText;
            }
            else
            {
                this.switchVideoAddressType.Attributes["class"] = "btn btn-default disabled";
            }
        }
    }
}