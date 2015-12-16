using CutomerSite.Helpers;
using MCS.Library.Cloud.AMS.Data.DataSources;
using MCS.Library.Cloud.AMS.Data.Entities;
using System;
using System.Web;
using Res = MCS.Web.Responsive.Library;

namespace CutomerSite.services
{
    public enum OperationType
    {
        None,
        AllEvents,
        UpcomingEvents,
        SingleEvent
    }

    /// <summary>
    /// Summary description for QueryService
    /// </summary>
    public class QueryService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string result = string.Empty;

            try
            {
                //throw new Exception("产生了错误...");

                OperationType opType = Res.Request.GetRequestQueryValue("opType", OperationType.None);

                switch (opType)
                {
                    case OperationType.AllEvents:
                        result = QueryEvents((pageIndex, pageSize, totalCount) => DataHelper.GetStartedEvents(pageIndex, pageSize, totalCount));
                        break;
                    case OperationType.UpcomingEvents:
                        result = QueryEvents((pageIndex, pageSize, totalCount) => DataHelper.GetUpcomingEvents(pageIndex, pageSize, totalCount));
                        break;
                    case OperationType.SingleEvent:
                        string eventID = Res.Request.GetRequestQueryString("id", string.Empty);
                        AMSEvent eventData = DataHelper.GetEventByID(eventID);
                        if (eventData != null)
                            result = DataHelper.GetSingleEventJson(eventData);

                        break;
                }
            }
            catch (System.Exception ex)
            {
                result = DataHelper.GetExceptionJson(ex);
            }
            finally
            {
                context.Response.Write(result);
                context.Response.End();
            }
        }

        private static string QueryEvents(Func<int, int, int, AMSEventCollection> getEvents)
        {
            int pageIndex = Res.Request.GetRequestQueryValue("pageIndex", 0);
            int totalCount = Res.Request.GetRequestQueryValue("totalCount", -1);

            AMSEventCollection events = getEvents(pageIndex, DataHelper.DefaultPageSize, totalCount);

            return DataHelper.GetEventsListJson(pageIndex, DataHelper.DefaultPageSize, events.TotalCount, events);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}