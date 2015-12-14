using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Web.Library.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CutomerSite.Helpers
{
    public static class DataHelper
    {
        public const int DefaultPageSize = 10;

        public static string GetEventsListJson(int pageIndex, int pageSize, int totalCount, IEnumerable<AMSEvent> events)
        {
            ArrayList list = new ArrayList();

            foreach (AMSEvent eventData in events)
            {
                var simpleEventData = new
                {
                    id = eventData.ID,
                    name = eventData.Name,
                    description = eventData.Description,
                    speakers = eventData.Speakers,
                    logo = eventData.LogoUrl.IsNotEmpty() ? eventData.LogoUrl : "/images/amsPoster1.png"
                };

                list.Add(simpleEventData);
            }

            var allData = new
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
                totalCount = totalCount,
                events = list
            };

            return JSONSerializerExecute.Serialize(allData);
        }

        public static AMSEvent GetEventByID(string id)
        {
            AMSEvent eventData = AMSEventSqlAdapter.Instance.LoadByID(id);

            if (eventData != null)
            {
                HttpRequest request = HttpContext.Current.Request;

                if (eventData.PosterUrl.IsNullOrEmpty())
                    eventData.PosterUrl = UriHelper.MakeAbsolute(new Uri("/images/amsPoster1.png", UriKind.RelativeOrAbsolute), request.Url).ToString();
            }

            return eventData;
        }

        public static string GetSingleEventJson(AMSEvent eventData)
        {
            var simpleEventData = new
            {
                id = eventData.ID,
                name = eventData.Name,
                description = eventData.Description,
                speakers = eventData.Speakers,
                url = eventData.CDNPlaybackUrl,
                poster = eventData.PosterUrl,
                views = string.Format("{0:#,##0}", eventData.Views),
                startTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", eventData.StartTime),
                endTime = string.Format("{0:yyyy-MM-dd HH:mm:ss}", eventData.EndTime)
            };

            return JSONSerializerExecute.Serialize(simpleEventData);
        }

        public static string GetExceptionJson(System.Exception ex)
        {
            ex = ex.GetRealException();

            var error = new { message = ex.Message, stackTrace = ex.StackTrace };

            return JSONSerializerExecute.Serialize(error);
        }
    }
}