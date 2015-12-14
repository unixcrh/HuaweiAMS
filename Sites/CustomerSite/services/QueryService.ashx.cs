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
                        AMSEventDataSource dataSource = new AMSEventDataSource();

                        int pageIndex = Res.Request.GetRequestQueryValue("pageIndex", 0);
                        int totalCount = Res.Request.GetRequestQueryValue("totalCount", -1); ;

                        AMSEventCollection events = dataSource.Query(pageIndex * DataHelper.DefaultPageSize, DataHelper.DefaultPageSize, ref totalCount);

                        result = DataHelper.GetEventsListJson(pageIndex, DataHelper.DefaultPageSize, totalCount, events);
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}