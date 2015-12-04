using MCS.Library.Cloud.AMS.Data.Conditions;
using Res = MCS.Web.Responsive.Library;
using MCS.Web.Responsive.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChannelManagement.list
{
    public partial class EventsInChannel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod.ToLower() == "post" && Request.Form["__VIEWSTATE"] == null)
            {

            }
            else
            {
                if (this.IsPostBack == false && this.IsCallback == false)
                {
                    this.QueryCondition = new EventQueryCondition();

                    this.QueryCondition.ChannelID = this.ChannelID;
                }

                //this.bindingControl.Data = this.QueryCondition;
            }
        }

        private EventQueryCondition QueryCondition
        {
            get
            {
                return this.ViewState["QueryCondition"] as EventQueryCondition;
            }
            set
            {
                this.ViewState["QueryCondition"] = value;
            }
        }

        private string ChannelID
        {
            get
            {
                return MCS.Web.Responsive.Library.Request.GetRequestQueryString("channelID", string.Empty);
            }
        }

        protected void eventDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            this.eventDataSource.Condition = this.QueryCondition;
        }

        protected override void OnPreRender(EventArgs e)
        {
            addEventButton.HRef = this.ResolveUrl(string.Format("~/forms/EditEvent.aspx?channelID={0}", this.ChannelID));

            base.OnPreRender(e);
        }
    }
}