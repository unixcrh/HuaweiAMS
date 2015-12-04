using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
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
            AMSEvent eventData = AMSEventSqlAdapter.Instance.LoadByInBuilder(builder => builder.AppendItem(id), "ID").SingleOrDefault();

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
            this.bindingControl.CollectData(true);

            AMSEventSqlAdapter.Instance.Update(this.Data);
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.backUrl.HRef = this.BackUrl;

            base.OnPreRender(e);
        }
    }
}