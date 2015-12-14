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
using System.Web.UI.WebControls;

namespace CutomerSite.list
{
    public partial class AllEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AMSEventDataSource dataSource = new AMSEventDataSource();

            int totalCount = 0;

            AMSEventCollection events = dataSource.Query(0, DataHelper.DefaultPageSize, ref totalCount);

            this.firstPageData.Value = DataHelper.GetEventsListJson(0, DataHelper.DefaultPageSize, totalCount, events);
            this.totalCount.Value = totalCount.ToString();
            this.pageSize.Value = DataHelper.DefaultPageSize.ToString();
        }
    }
}