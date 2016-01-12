using CutomerSite.services;
using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.DataSources;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Web.Library.Script;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Res = MCS.Web.Responsive.Library;

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
                    timeDescription = GetTimeDescription(eventData.StartTime, eventData.EndTime),
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

        public static AMSEventCollection GetStartedEvents(int pageIndex, int pageSize, int totalCount)
        {
            AMSEventDataSource dataSource = new AMSEventDataSource();

            int retTotalCount = totalCount;

            AMSEventCollection events = dataSource.Query(pageIndex * DataHelper.DefaultPageSize, DataHelper.DefaultPageSize, "EndTime <= GETUTCDATE()", "StartTime DESC", ref retTotalCount);

            events.TotalCount = retTotalCount;

            return events;
        }

        public static AMSEventCollection GetUpcomingEvents(int pageIndex, int pageSize, int totalCount)
        {
            AMSEventDataSource dataSource = new AMSEventDataSource();

            int retTotalCount = totalCount;

            AMSEventCollection events = dataSource.Query(pageIndex * DataHelper.DefaultPageSize, DataHelper.DefaultPageSize, "EndTime > GETUTCDATE()", "StartTime", ref retTotalCount);

            events.TotalCount = retTotalCount;

            return events;
        }

        public static AMSEvent GetEventByID(string id, string channelID = "")
        {
            AMSEvent eventData = AMSEventSqlAdapter.Instance.Load(id, channelID);

            if (eventData != null)
            {
                HttpRequest request = HttpContext.Current.Request;

                if (eventData.PosterUrl.IsNullOrEmpty())
                    eventData.PosterUrl = UriHelper.MakeAbsolute(new Uri("/images/amsPoster1.png", UriKind.RelativeOrAbsolute), request.Url).ToString();
            }

            return eventData;
        }

        public static string GetSingleEventJson(AMSChannel channel, AMSEvent eventData, VideoAddressType addressType)
        {
            var simpleEventData = new
            {
                id = eventData.ID,
                channelID = eventData.ChannelID,
                name = eventData.Name,
                description = eventData.Description,
                speakers = eventData.Speakers,
                url = ChangeVideoAddress(channel, eventData.CDNPlaybackUrl, addressType),
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

        public static void UpdateUserView(string eventID)
        {
            AMSUserView userView = CreateUserView(eventID);

            if (userView != null)
                AMSUserViewSqlAdapter.Instance.UpdateUserView(userView);
        }

        private static string ChangeVideoAddress(AMSChannel channel, string url, VideoAddressType addressType)
        {
            string result = url;

            if (channel != null && channel.AlternateCDNEndpoint.IsNotEmpty() && url.IsNotEmpty())
            {
                Uri target = new Uri(url);

                if (addressType == VideoAddressType.AlternateCDN)
                    result = MergePlaybackCDNHost(target, channel.AlternateCDNEndpoint);
            }

            return result;
        }

        private static string MergePlaybackCDNHost(Uri originalUri, string cdnHost)
        {
            string originalHost = originalUri.Host + (originalUri.Port == 80 ? string.Empty : ":" + originalUri.Port);

            return originalUri.Scheme + "://" + (cdnHost ?? originalHost) + originalUri.PathAndQuery;
        }

        private static string GetTimeDescription(DateTime startTime, DateTime endTime)
        {
            TimeSpan ts = endTime - startTime;

            StringBuilder strB = new StringBuilder();

            strB.AppendFormat("{0:yyyy-MM-dd HH:mm}", startTime);

            strB.AppendFormat(" 时长{0:}小时", ts.Hours);

            if (ts.Minutes > 0)
                strB.AppendFormat("{0:00}分钟", ts.Minutes);

            return strB.ToString();
        }

        private static AMSUserView CreateUserView(string eventID)
        {
            AMSUserView result = null;

            if (HttpContext.Current.User != null)
            {
                string userName = HttpContext.Current.User.Identity.Name;

                if (userName.IsNotEmpty())
                {
                    result = new AMSUserView();

                    result.EventID = eventID;
                    result.UserID = userName;
                    result.UserName = userName;
                    result.LastClientAccessIP = Res.Request.GetClientIP();
                }
            }

            return result;
        }
    }
}