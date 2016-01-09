using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Adapters
{
    public class AMSEventSqlAdapter : UpdatableAndLoadableAdapterBase<AMSEvent, AMSEventCollection>
    {
        public static readonly AMSEventSqlAdapter Instance = new AMSEventSqlAdapter();

        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }

        public AMSEventCollection LoadByChannelID(string channelID)
        {
            channelID.CheckStringIsNullOrEmpty("channelID");

            return this.LoadByInBuilder(builder => builder.AppendItem(channelID), "ChannelID");
        }

        public AMSEvent LoadByID(string id)
        {
            id.CheckStringIsNullOrEmpty("id");

            return this.LoadByInBuilder(builder => builder.AppendItem(id), "ID").SingleOrDefault();
        }

        /// <summary>
        /// 判断同一频道下是否有时间交叉的事件
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="additionalChannelID">额外的频道</param>
        /// <returns></returns>
        public bool HaveIntersectEvents(AMSEvent eventData, string additionalChannelID = "")
        {
            eventData.NullCheck("eventData");

            DateTime startTime = TimeZoneContext.Current.ConvertTimeToUtc(eventData.StartTime);
            DateTime endTime = TimeZoneContext.Current.ConvertTimeToUtc(eventData.EndTime);

            WhereSqlClauseBuilder builder1 = new WhereSqlClauseBuilder();
            builder1.AppendItem("StartTime", startTime, ">=");
            builder1.AppendItem("StartTime", endTime, "<");

            WhereSqlClauseBuilder builder2 = new WhereSqlClauseBuilder();
            builder2.AppendItem("EndTime", startTime, ">");
            builder2.AppendItem("EndTime", endTime, "<");

            WhereSqlClauseBuilder builder3 = new WhereSqlClauseBuilder();
            builder3.AppendItem("StartTime", startTime, "<");
            builder3.AppendItem("EndTime", startTime, ">");

            WhereSqlClauseBuilder builder4 = new WhereSqlClauseBuilder();
            builder4.AppendItem("StartTime", endTime, "<");
            builder4.AppendItem("EndTime", endTime, ">");

            ConnectiveSqlClauseCollection connectiveTime = new ConnectiveSqlClauseCollection(LogicOperatorDefine.Or,
                builder1, builder2, builder3, builder4);

            WhereSqlClauseBuilder idBuilder = new WhereSqlClauseBuilder();

            idBuilder.AppendItem("ID", eventData.ID, "<>");

            AMSChannelCollection channels = this.LoadRelativeChannels(eventData.ID);

            InSqlClauseBuilder channelIDBuilder = new InSqlClauseBuilder("EC.ChannelID");

            channels.ForEach(c => channelIDBuilder.AppendItem(c.ID));
            channelIDBuilder.AppendItem(eventData.ChannelID);

            if (additionalChannelID.IsNotEmpty())
                channelIDBuilder.AppendItem(additionalChannelID);

            ConnectiveSqlClauseCollection connective = new ConnectiveSqlClauseCollection(LogicOperatorDefine.And, idBuilder, channelIDBuilder, connectiveTime);

            string sql = string.Format("SELECT TOP 1 * FROM {0} E INNER JOIN AMS.EventsChannels EC ON E.ID = EC.EventID WHERE {1}",
                this.GetTableName(), connective.ToSqlString(TSqlBuilder.Instance));

            return this.QueryData(sql).FirstOrDefault() != null;
        }

        /// <summary>
        /// 读取需要启动的事件(时间状态为Stopped)，且加上提前时间
        /// </summary>
        /// <param name="warmupTime">需要提前预热的时间</param>
        /// <returns></returns>
        public AMSEventCollection LoadNeedStartEvents(TimeSpan warmupTime)
        {
            InSqlClauseBuilder stateBuilder = new InSqlClauseBuilder("State");

            stateBuilder.AppendItem(AMSEventState.NotStart.ToString(), AMSEventState.Starting.ToString());

            WhereSqlClauseBuilder timeBuilder = new WhereSqlClauseBuilder();

            timeBuilder.AppendItem("StartTime",
                    string.Format("DATEADD(SECOND, {0}, GETUTCDATE())", (int)warmupTime.TotalSeconds), "<=", true);
            //timeBuilder.AppendItem("StartTime", "GETUTCDATE()", ">", true);

            return this.LoadByBuilder(new ConnectiveSqlClauseCollection(LogicOperatorDefine.And, stateBuilder, timeBuilder));
        }

        /// <summary>
        /// 读取需要停止的事件
        /// </summary>
        /// <returns></returns>
        public AMSEventCollection LoadNeedStopEvents()
        {
            InSqlClauseBuilder stateBuilder = new InSqlClauseBuilder("State");

            stateBuilder.AppendItem(
                AMSEventState.Running.ToString(),
                AMSEventState.Starting.ToString(),
                AMSEventState.Stopping.ToString());

            WhereSqlClauseBuilder timeBuilder = new WhereSqlClauseBuilder();

            timeBuilder.AppendItem("EndTime", "GETUTCDATE()", "<=", true);

            return this.LoadByBuilder(new ConnectiveSqlClauseCollection(LogicOperatorDefine.And, stateBuilder, timeBuilder));
        }

        public void DeleteByID(string id)
        {
            id.CheckStringIsNullOrEmpty("id");

            this.Delete(builder => builder.AppendItem("ID", id));
        }

        public int UpdateState(string eventID, AMSEventState state)
        {
            eventID.CheckStringIsNullOrEmpty("eventID");

            SqlClauseBuilderBase wBuilder = new WhereSqlClauseBuilder().AppendItem("ID", eventID);

            Dictionary<string, object> context = new Dictionary<string, object>();
            ORMappingItemCollection mappings = this.GetMappingInfo(context);

            SqlClauseBuilderBase uBuilder = new UpdateSqlClauseBuilder().AppendItem("State", state.ToString());

            string sql = string.Format("UPDATE {0} SET {1} WHERE {2}",
                mappings.TableName, uBuilder.ToSqlString(TSqlBuilder.Instance), wBuilder.ToSqlString(TSqlBuilder.Instance));

            return DbHelper.RunSql(sql, this.GetConnectionName());
        }

        /// <summary>
        /// 更新某一状态下所有时间的完成时间
        /// </summary>
        /// <param name="endTime"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateEndTime(DateTime endTime, AMSEventState state)
        {
            SqlClauseBuilderBase wBuilder = new WhereSqlClauseBuilder().AppendItem("State", state.ToString());

            SqlClauseBuilderBase uBuilder = new UpdateSqlClauseBuilder().AppendItem("EndTime", endTime);

            Dictionary<string, object> context = new Dictionary<string, object>();
            ORMappingItemCollection mappings = this.GetMappingInfo(context);

            string sql = string.Format("UPDATE {0} SET {1} WHERE {2}",
                mappings.TableName, uBuilder.ToSqlString(TSqlBuilder.Instance), wBuilder.ToSqlString(TSqlBuilder.Instance));

            return DbHelper.RunSql(sql, this.GetConnectionName());
        }

        public AMSChannelCollection LoadRelativeChannels(string eventID)
        {
            eventID.CheckStringIsNullOrEmpty("eventID");

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("EC.EventID", eventID);

            string sql = string.Format("SELECT C.*, EC.IsDefault FROM AMS.Channels C INNER JOIN AMS.EventsChannels EC ON C.ID = EC.ChannelID WHERE {0} ORDER BY EC.IsDefault DESC",
                builder.ToSqlString(TSqlBuilder.Instance));

            DataTable table = DbHelper.RunSqlReturnDS(sql, this.GetConnectionName()).Tables[0];

            AMSChannelCollection channels = new AMSChannelCollection();

            foreach (DataRow row in table.Rows)
            {
                AMSChannelInEvent channel = new AMSChannelInEvent();

                ORMapping.DataRowToObject(row, channel);

                channels.Add(channel);
            }

            return channels;
        }

        /// <summary>
        /// 在一个事件下增加频道(UI上用于频道列表)
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="channelIDs"></param>
        /// <returns></returns>
        public int AddChannel(string eventID, IEnumerable<string> channelIDs)
        {
            eventID.CheckStringIsNullOrEmpty("eventID");
            channelIDs.NullCheck("channelIDs");

            StringBuilder strB = new StringBuilder();

            AMSEventChannel eventChannel = new AMSEventChannel();

            eventChannel.EventID = eventID;
            eventChannel.State = AMSEventState.NotStart;
            eventChannel.IsDefault = false;

            foreach (string channelID in channelIDs)
            {
                eventChannel.ChannelID = channelID;

                if (strB.Length > 0)
                    strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

                strB.Append(ORMapping.GetInsertSql(eventChannel, TSqlBuilder.Instance));
            }

            int result = 0;

            if (strB.Length > 0)
                result = DbHelper.RunSqlWithTransaction(strB.ToString(), this.GetConnectionName());

            return result;
        }

        /// <summary>
        /// 删除某个事件下的频道（UI上用于频道列表）
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="channelIDs"></param>
        /// <param name="includeDefault">是否包含默认频道</param>
        public int DeleteChannels(string eventID, IEnumerable<string> channelIDs, bool includeDefault = false)
        {
            eventID.CheckStringIsNullOrEmpty("eventID");
            channelIDs.NullCheck("channelIDs");

            InSqlClauseBuilder builder = new InSqlClauseBuilder("ChannelID");

            channelIDs.ForEach(channelID => builder.AppendItem(channelID));

            ORMappingItemCollection mappings = ORMapping.GetMappingInfo(typeof(AMSEventChannel));

            string sql = string.Format("DELETE {0} WHERE {1}", mappings.TableName, builder.ToSqlStringWithInOperator(TSqlBuilder.Instance));

            int result = 0;

            if (builder.IsEmpty == false)
            {
                if (includeDefault == false)
                    sql += " AND IsDefault <> 1";

                result = DbHelper.RunSql(sql, this.GetConnectionName());
            }

            return result;
        }

        protected override string GetInsertSql(AMSEvent data, ORMappingItemCollection mappings, Dictionary<string, object> context)
        {
            StringBuilder strB = new StringBuilder();

            strB.Append(base.GetInsertSql(data, mappings, context));
            strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

            AMSEventChannel eventChannel = AMSEventChannel.FromAMSEvent(data);
            strB.Append(ORMapping.GetInsertSql(eventChannel, TSqlBuilder.Instance));

            return strB.ToString();
        }
    }
}
