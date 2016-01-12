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

        [ControllerMethod]
        protected void InitByEventID(string id, string channelID)
        {
            if (id.IsNotEmpty())
            {
                AMSEvent eventData = DataHelper.GetEventByID(id, channelID);

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
            this.BindRelativeChannels();

            base.OnPreRender(e);
        }

        private void BindRelativeChannels()
        {
            if (this.Event != null)
            {
                AMSChannelCollection channels = AMSEventSqlAdapter.Instance.LoadRelativeChannels(this.Event.ID);

                this.channels.DataSource = channels;
                this.channels.DataValueField = "ID";
                this.channels.DataTextField = "Name";
                this.channels.DataBind();
            }
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