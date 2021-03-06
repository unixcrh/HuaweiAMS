﻿using MCS.Library.Cloud.AMS.Data.Contracts;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Adapters
{
    public class AMSChannelSqlAdapter : UpdatableAndLoadableAdapterBase<AMSChannel, AMSChannelCollection>, IAMSChannelAdapter
    {
        public static readonly AMSChannelSqlAdapter Instance = new AMSChannelSqlAdapter();

        public AMSChannelCollection GetAllChannels()
        {
            return this.Load(builder => builder.AppendItem("1", "1"));
        }

        public AMSChannel LoadByID(string id)
        {
            id.CheckStringIsNullOrEmpty("id");

            return this.LoadByInBuilder(builder => builder.AppendItem(id), "ID").SingleOrDefault();
        }

        /// <summary>
        /// 读取需要停止的频道
        /// </summary>
        /// <param name="leadTime">多长时间之内没有节目</param>
        /// <returns></returns>
        public AMSChannelCollection LoadNeedStopChannels(TimeSpan leadTime)
        {
            InSqlClauseBuilder inBuilder = new InSqlClauseBuilder("OuterC.State");

            inBuilder.AppendItem(
                AMSChannelState.Running.ToString(),
                AMSChannelState.Starting.ToString(),
                AMSChannelState.Stopping.ToString());

            //得到从现在开始到leadTime时间段内，需要启动的节目
            WhereSqlClauseBuilder startTimeBuilder = new WhereSqlClauseBuilder();

            startTimeBuilder.AppendItem("E.StartTime", "GETUTCDATE()", ">=", true);
            startTimeBuilder.AppendItem("E.StartTime",
                string.Format("DATEADD(second, {0}, GETUTCDATE())", (int)leadTime.TotalSeconds),
                "<", true);

            //得到还没有结束的节目
            WhereSqlClauseBuilder endTimeBuilder = new WhereSqlClauseBuilder();

            endTimeBuilder.AppendItem("E.EndTime", "GETUTCDATE()", ">", true);
            endTimeBuilder.AppendItem("E.EndTime",
                string.Format("DATEADD(second, {0}, GETUTCDATE())", (int)leadTime.TotalSeconds),
                "<=", true);

            ConnectiveSqlClauseCollection connective = new ConnectiveSqlClauseCollection(LogicOperatorDefine.Or,
                startTimeBuilder, endTimeBuilder);

            //子查询，表示在未来一段时间内需要播放的节目
            string subSql = string.Format("SELECT C.ID FROM AMS.Channels C INNER JOIN AMS.EventsChannels EC ON C.ID = EC.ChannelID INNER JOIN AMS.Events E ON EC.EventID = E.ID WHERE {0}",
                connective.ToSqlString(TSqlBuilder.Instance));

            string sql = string.Format("SELECT * FROM AMS.Channels OuterC WHERE {0} AND OuterC.ID NOT IN ({1})",
                        inBuilder.ToSqlStringWithInOperator(TSqlBuilder.Instance),
                        subSql);

            return this.QueryData(sql);
        }

        /// <summary>
        /// 更新所有的频道数据
        /// </summary>
        /// <param name="channels"></param>
        public int UpdateAllChannels(AMSChannelCollection channels)
        {
            channels.NullCheck("channels");

            StringBuilder strB = new StringBuilder();

            Dictionary<string, object> context = new Dictionary<string, object>();
            ORMappingItemCollection mappings = this.GetMappingInfo(context);

            strB.AppendFormat("DELETE {0} ", mappings.TableName);

            foreach (AMSChannel channel in channels)
            {
                if (strB.Length > 0)
                    strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

                strB.Append(this.GetInsertSql(channel, mappings, context));
            }

            return DbHelper.RunSqlWithTransaction(strB.ToString(), this.GetConnectionName());
        }

        /// <summary>
        /// 更新频道的状态
        /// </summary>
        /// <param name="channelID"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateState(string channelID, AMSChannelState state)
        {
            channelID.CheckStringIsNullOrEmpty("channelID");

            SqlClauseBuilderBase wBuilder = new WhereSqlClauseBuilder().AppendItem("ID", channelID);

            Dictionary<string, object> context = new Dictionary<string, object>();
            ORMappingItemCollection mappings = this.GetMappingInfo(context);

            SqlClauseBuilderBase uBuilder = new UpdateSqlClauseBuilder().AppendItem("State", state.ToString());

            string sql = string.Format("UPDATE {0} SET {1} WHERE {2}",
                mappings.TableName, uBuilder.ToSqlString(TSqlBuilder.Instance), wBuilder.ToSqlString(TSqlBuilder.Instance));

            return DbHelper.RunSql(sql, this.GetConnectionName());
        }

        public void InitChannels()
        {
            DbHelper.RunSqlWithTransaction("EXECUTE AMS.InitChannels", this.GetConnectionName());
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }
    }
}
