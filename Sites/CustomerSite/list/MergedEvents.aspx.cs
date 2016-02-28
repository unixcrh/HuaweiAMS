using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CutomerSite.Helpers;
using MCS.Library.Cloud.AMS.Data.Entities;

namespace CutomerSite.list
{
    public partial class MergedEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AMSEventCollection upcomingEvents = DataHelper.GetUpcomingEvents(0, DataHelper.MaxPageSize, -1);
            upcomingEvents.TotalCount = -1;

            AMSEventCollection startedEvents = DataHelper.GetStartedEvents(0, DataHelper.DefaultPageSize, -1);

            this.firstPageData.Value = DataHelper.GetEventsListJson(0, DataHelper.DefaultPageSize, startedEvents.TotalCount,
                new Dictionary<string, IEnumerable<AMSEvent>>() {
                    { "upcomingEvents", upcomingEvents },
                    { "startedEvents", startedEvents }
                });

            this.totalCount.Value = startedEvents.TotalCount.ToString();
            this.pageSize.Value = DataHelper.DefaultPageSize.ToString();
        }

        protected override void OnPreRender(EventArgs e)
        {
            this.RegisterApplicationRoot();

            base.OnPreRender(e);
        }
    }
}