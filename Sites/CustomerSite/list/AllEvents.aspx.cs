using MCS.Library.Cloud.AMS.Data.DataSources;
using MCS.Library.Cloud.AMS.Data.Entities;
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
        private const int PageSize = 5;

        protected void Page_Load(object sender, EventArgs e)
        {
            AMSEventDataSource dataSource = new AMSEventDataSource();

            int totalCount = 0;

            AMSEventCollection events = dataSource.Query(0, PageSize, ref totalCount);

            this.firstPageData.Value = GetEventsJson(events);
            this.totalCount.Value = totalCount.ToString();
            this.pageSize.Value = PageSize.ToString();
        }

        private static string GetEventsJson(IEnumerable<AMSEvent> events)
        {
            ArrayList list = new ArrayList();

            foreach (AMSEvent eventData in events)
            {
                var simpleEventData = new { id = eventData.ID, name = eventData.Name, description = eventData.Description, speakers = eventData.Speakers };

                list.Add(simpleEventData);
            }

            return JSONSerializerExecute.Serialize(list);
        }
    }
}