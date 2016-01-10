using MCS.Library.Cloud.AMS.Data.Conditions;
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
                }

                //this.bindingControl.Data = this.QueryCondition;
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
                return MCS.Web.Responsive.Library.Request.GetRequestQueryString("eventID", string.Empty);
            }
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
    }
}