using CutomerSite.Helpers;
using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Web.Library.Script;
using MCS.Web.Responsive.Library.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MCS.Web.Responsive.Library;
using Res = MCS.Web.Responsive.Library;
using CutomerSite.services;
using System.Collections.Specialized;

namespace CutomerSite.forms
{
    public partial class EventPlayer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ControllerHelper.ExecuteMethodByRequest(this);

            string agentText = this.Request.UserAgent;
            userAgent.InnerText = agentText + " login_uid: ";

            if (this.User != null)
                userAgent.InnerText += this.User.Identity.Name;

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

        [ControllerMethod]
        protected void InitByEventID(string id)
        {
            if (id.IsNotEmpty())
            {
                AMSEvent eventData = DataHelper.GetEventByID(id);

                if (eventData != null)
                {
                    this.pageEventData.Value = DataHelper.GetSingleEventJson(eventData, WebHelper.GetVideoAddressType());
                    this.videoTitle.Text = HttpUtility.HtmlEncode(eventData.Name);

                    DataHelper.UpdateUserView(id);
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.RegisterApplicationRoot();

            VideoAddressType videoAddressType = WebHelper.GetVideoAddressType();

            this.videoAddressType.Value = videoAddressType.ToString();

            VideoAddressType targetType = VideoAddressType.Mooncake;

            string buttonText = string.Empty;

            switch (videoAddressType)
            {
                case VideoAddressType.Default:
                    targetType = VideoAddressType.Mooncake;
                    buttonText = "切换到中国CDN";
                    break;
                case VideoAddressType.Mooncake:
                    targetType = VideoAddressType.Default;
                    buttonText = "切换到默认CDN";
                    break;
            }

            switchVideoAddressType.HRef = UriHelper.ReplaceUriParams(this.Request.Url.ToString(),
                parameters => parameters["videoAddressType"] = targetType.ToString());
            switchVideoAddressType.InnerText = buttonText;

            base.OnPreRender(e);
        }
    }
}