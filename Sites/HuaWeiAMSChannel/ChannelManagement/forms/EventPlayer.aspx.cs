using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Web.Responsive.Library.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChannelManagement.forms
{
    public partial class EventPlayer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod.ToLower() == "post" && Request.Form["__VIEWSTATE"] == null)
            {
            }
            else
            {
                if (this.IsPostBack == false && this.IsCallback == false)
                    ControllerHelper.ExecuteMethodByRequest(this);

                this.bindingControl.Data = this.Data;
            }
        }

        [ControllerMethod]
        protected void InitByEventID(string id, string channelID)
        {
            AMSEvent eventData = AMSEventSqlAdapter.Instance.Load(id, channelID);

            eventData.NullCheck(string.Format("不能找到ID为{0}的事件", id));

            if (eventData.PosterUrl.IsNullOrEmpty())
                eventData.PosterUrl = UriHelper.MakeAbsolute(new Uri(this.ResolveUrl("~/images/amsPoster1.png"), UriKind.RelativeOrAbsolute), this.Request.Url).ToString();

            this.Data = eventData;
        }

        private AMSEvent Data
        {
            get
            {
                return (AMSEvent)this.ViewState["Data"];
            }
            set
            {
                this.ViewState["Data"] = value;
            }
        }
    }
}