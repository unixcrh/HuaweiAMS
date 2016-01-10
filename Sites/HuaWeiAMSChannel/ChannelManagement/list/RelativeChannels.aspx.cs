using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Conditions;
using MCS.Library.Cloud.AMS.Data.Executors;
using MCS.Library.Core;
using MCS.Web.Responsive.Library;
using MCS.Web.Responsive.Library.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChannelManagement.list
{
    public partial class RelativeChannels : System.Web.UI.Page
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
                    this.QueryCondition = new ChannelInEventQueryCondition();

                    this.QueryCondition.EventID = this.EventID;
                    this.unusedChannels.DataSource = AMSEventSqlAdapter.Instance.LoadUnusedChannels(this.EventID);
                    this.unusedChannels.DataBind();
                }
            }
        }

        private ChannelInEventQueryCondition QueryCondition
        {
            get
            {
                return this.ViewState["QueryCondition"] as ChannelInEventQueryCondition;
            }
            set
            {
                this.ViewState["QueryCondition"] = value;
            }
        }

        private string EventID
        {
            get
            {
                return MCS.Web.Responsive.Library.Request.GetRequestQueryString("id", string.Empty);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.ChannelHeader.Event != null)
                ChannelHeader.CurrentName = this.ChannelHeader.Event.Name;

            base.OnPreRender(e);
        }

        protected void eventChannelDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            this.eventChannelDataSource.Condition = this.QueryCondition;
        }

        protected void refreshBtn_Click(object sender, EventArgs e)
        {
            this.InnerRefreshList();
        }

        private void InnerRefreshList()
        {
            // 重新刷新列表
            this.eventChannelDataSource.LastQueryRowCount = -1;
            this.dataGrid.SelectedKeys.Clear();

            this.dataGrid.DataBind();
        }

        protected void postAddChannelBtn_Click(object sender, EventArgs e)
        {
            string channelID = this.unusedChannels.SelectedValue;

            if (channelID.IsNotEmpty())
            {
                AMSAddChannelInEventExecutor executor = new AMSAddChannelInEventExecutor(this.EventID, channelID);

                executor.Execute();

                this.RefreshChannelsAndList();
            }
        }

        protected void deleteChannelButton_Click(object sender, EventArgs e)
        {
            AMSDeleteChannelsInEventExecutor executor = new AMSDeleteChannelsInEventExecutor(this.EventID, this.dataGrid.SelectedKeys.ToArray());

            executor.Execute();

            this.RefreshChannelsAndList();
        }

        private void RefreshChannelsAndList()
        {
            this.unusedChannels.DataSource = AMSEventSqlAdapter.Instance.LoadUnusedChannels(this.EventID);
            this.unusedChannels.DataBind();
            this.InnerRefreshList();
        }
    }
}