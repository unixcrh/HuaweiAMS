using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Cloud.AMS.Data.Executors;
using MCS.Library.Core;
using MCS.Web.Responsive.Library;
using MCS.Web.Responsive.Library.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Res = MCS.Web.Responsive.Library;

namespace ChannelManagement.forms
{
    public partial class EditEvent : System.Web.UI.Page
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
                    ControllerHelper.ExecuteMethodByRequest(this);

                    if (this.Request.UrlReferrer != null)
                        this.BackUrl = this.Request.UrlReferrer.ToString();
                    else
                        this.BackUrl = "#";
                }

                this.bindingControl.Data = this.Data;
            }
        }

        #region ControllerMethod
        [ControllerMethod(true)]
        protected void InitNewEvent()
        {
            this.Data = new AMSEvent()
            {
                ID = UuidHelper.NewUuidString(),
                ChannelID = Res.Request.GetRequestQueryString("channelID", string.Empty)
            };
        }

        [ControllerMethod]
        protected void InitByEventID(string id)
        {
            AMSEvent eventData = AMSEventSqlAdapter.Instance.LoadByID(id);

            eventData.NullCheck(string.Format("不能找到ID为{0}的事件", id));

            this.Data = eventData;
        }
        #endregion

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

        private string BackUrl
        {
            get
            {
                return this.ViewState.GetViewStateValue("BackUrl", string.Empty);
            }
            set
            {
                this.ViewState.SetViewStateValue("BackUrl", value);
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            try
            {
                this.bindingControl.CollectData(true);

                AMSEventSqlAdapter.Instance.HaveIntersectEvents(this.Data).TrueThrow(
                    "播放时间段{0:yyyy-MM-dd HH:mm}-{1:yyyy-MM-dd HH:mm}与别的事件重叠",
                    this.Data.StartTime, this.Data.EndTime);

                AMSEditEntityExecutor<AMSEvent> executor = new AMSEditEntityExecutor<AMSEvent>(
                    this.Data,
                    data => AMSEventSqlAdapter.Instance.Update(data),
                    AMSOperationType.EditEvent);

                executor.Execute();

                this.ClientScript.RegisterStartupScript(this.GetType(),
                    "back",
                    string.Format("top.document.getElementById(\"{0}\").click();", this.backUrl.ClientID),
                    true);
            }
            catch (System.Exception ex)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "Exception",
                    string.Format("top.$showError('{0}');", ScriptHelper.CheckScriptString(ex.Message, false)),
                    true);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.backUrl.HRef = this.BackUrl;

            base.OnPreRender(e);
        }
    }
}