using CutomerSite.Helpers;
using MCS.Library.Cloud.AMS.Data.DataSources;
using MCS.Library.Cloud.AMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CutomerSite.list
{
    public partial class UpcomingEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AMSEventCollection events = DataHelper.GetUpcomingEvents(0, DataHelper.DefaultPageSize, -1);

            this.firstPageData.Value = DataHelper.GetEventsListJson(0, DataHelper.DefaultPageSize, events.TotalCount, events);
            this.totalCount.Value = events.TotalCount.ToString();
            this.pageSize.Value = DataHelper.DefaultPageSize.ToString();
        }
    }
}