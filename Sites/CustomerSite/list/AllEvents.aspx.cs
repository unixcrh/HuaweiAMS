using CutomerSite.Helpers;
using MCS.Library.Cloud.AMS.Data.DataSources;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Web.Library.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CutomerSite.list
{
    public partial class AllEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AMSEventCollection events = DataHelper.GetStartedEvents(0, DataHelper.DefaultPageSize, -1);

            this.firstPageData.Value = DataHelper.GetEventsListJson(0, DataHelper.DefaultPageSize, events.TotalCount, events);
            this.totalCount.Value = events.TotalCount.ToString();
            this.pageSize.Value = DataHelper.DefaultPageSize.ToString();

            //HttpCookie cookie = this.Request.Cookies.Get("login_uid");

            //if (cookie != null)
            //    this.uid.InnerText = cookie.Value;
            //OutputHeaders();
        }

        //private void OutputHeaders()
        //{
        //    foreach (string key in this.Request.Headers.AllKeys)
        //    {
        //        string headerValue = this.Request.Headers[key];

        //        OutputOneHeader(this.httpHeaders, key, headerValue);
        //    }
        //}

        //private void OutputOneHeader(Control container, string key, string headerValue)
        //{
        //    HtmlGenericControl divRow = new HtmlGenericControl("div");
        //    container.Controls.Add(divRow);

        //    HtmlGenericControl divKey = new HtmlGenericControl("span");
        //    divKey.InnerText = key + ": ";

        //    divRow.Controls.Add(divKey);

        //    HtmlGenericControl divValue = new HtmlGenericControl("span");
        //    divValue.InnerText = headerValue;

        //    divRow.Controls.Add(divValue);
        //}
    }
}