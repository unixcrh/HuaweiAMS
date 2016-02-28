using CutomerSite.Helpers;
using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.DataSources;
using MCS.Library.Cloud.AMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Web;
using Res = MCS.Web.Responsive.Library;

namespace CutomerSite.services
{
    public enum OperationType
    {
        None,
        AllEvents,
        UpcomingEvents,
        SingleEvent,
        MergedEvents
    }

    /// <summary>
    /// 
    /// </summary>
    public enum VideoAddressType
    {
        Default,
        AlternateCDN
    }

    public enum TechOrderType
    {
        Html5,
        AzureHtml5JS
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
                OperationType opType = Res.Request.GetRequestQueryValue("opType", OperationType.None);

                switch (opType)
                {
                    case OperationType.AllEvents:
                        result = QueryEvents((pageIndex, pageSize, totalCount) => DataHelper.GetStartedEvents(pageIndex, pageSize, totalCount));
                        break;
                    case OperationType.UpcomingEvents:
                        result = QueryEvents((pageIndex, pageSize, totalCount) => DataHelper.GetUpcomingEvents(pageIndex, pageSize, totalCount));
                        break;
                    case OperationType.MergedEvents:
                        result = QueryEvents((pageIndex, pageSize, totalCount) =>
                        {
                            AMSEventCollection upcomingEvents = DataHelper.GetUpcomingEvents(0, DataHelper.MaxPageSize, -1);
                            upcomingEvents.TotalCount = -1;

                            AMSEventCollection startedEvents = DataHelper.GetStartedEvents(pageIndex, pageSize, totalCount);

                            return new Dictionary<string, IEnumerable<AMSEvent>>() {
                                { "upcomingEvents", upcomingEvents },
                                { "startedEvents", startedEvents }
                            };
                        }
                        );

                        break;
                    case OperationType.SingleEvent:
                        string eventID = Res.Request.GetRequestQueryString("id", string.Empty);
                        string channelID = Res.Request.GetRequestQueryString("channelID", string.Empty);

                        AMSEvent eventData = DataHelper.GetEventByID(eventID, channelID);

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

        private static string QueryEvents(Func<int, int, int, Dictionary<string, IEnumerable<AMSEvent>>> getEvents)
        {
            int pageIndex = Res.Request.GetRequestQueryValue("pageIndex", 0);
            int totalCount = Res.Request.GetRequestQueryValue("totalCount", -1);

            Dictionary<string, IEnumerable<AMSEvent>> eventsDict = getEvents(pageIndex, DataHelper.DefaultPageSize, totalCount);

            foreach(KeyValuePair<string, IEnumerable<AMSEvent>> kp in eventsDict)
            {
                AMSEventCollection events = kp.Value as AMSEventCollection;

                if (events != null && events.TotalCount >= 0)
                    totalCount = events.TotalCount;
            }

            return DataHelper.GetEventsListJson(pageIndex, DataHelper.DefaultPageSize, totalCount, eventsDict);
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