using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Conditions;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Cloud.AMS.Data.Executors;
using MCS.Web.Responsive.Library;
using MCS.Web.Responsive.Library.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Res = MCS.Web.Responsive.Library;

namespace ChannelManagement.list
{
    public partial class EventsInChannel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebUtility.RequiredScript(typeof(ClientMsgResources));

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

        protected void deleteEventButton_Click(object sender, EventArgs e)
        {
            foreach (string id in this.dataGrid.SelectedKeys)
            {
                AMSDeleteEntityExecutor<string, AMSEvent> executor = new AMSDeleteEntityExecutor<string, AMSEvent>(
                    id,
                    key => AMSEventSqlAdapter.Instance.DeleteByID(key),
                    AMSOperationType.DeleteEvent);

                executor.Execute();
            }

            this.InnerRefreshList();
        }

        protected void stopEventButton_Click(object sender, EventArgs e)
        {
            AMSEventSqlAdapter.Instance.SendStopEventMessages(this.dataGrid.SelectedKeys.ToArray());

            this.InnerRefreshList();
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            this.InnerRefreshList();
        }

        protected override void OnPreRender(EventArgs e)
        {
            addEventButton.HRef = this.ResolveUrl(string.Format("~/forms/EditEvent.aspx?channelID={0}", this.ChannelID));

            base.OnPreRender(e);
        }

        private void InnerRefreshList()
        {
            // 重新刷新列表
            this.eventDataSource.LastQueryRowCount = -1;
            this.dataGrid.SelectedKeys.Clear();

            this.dataGrid.DataBind();
        }
    }
}