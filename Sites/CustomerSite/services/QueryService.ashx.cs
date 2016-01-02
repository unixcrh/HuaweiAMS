using CutomerSite.Helpers;
using MCS.Library.Cloud.AMS.Data.Adapters;
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
    /// 
    /// </summary>
    public enum VideoAddressType
    {
        Default,
        AlternateCDN
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
                //System.Threading.Thread.Sleep(2000);
                //throw new ApplicationException("这是一个异常...");

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
                        {
                            AMSChannel channel = AMSChannelSqlAdapter.Instance.LoadByID(eventData.ChannelID);
                            result = DataHelper.GetSingleEventJson(channel, eventData, WebHelper.GetVideoAddressType());
                        }
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