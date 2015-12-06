using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using System;
using System.Collections.Generic;
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
            timeBuilder.AppendItem("StartTime", "GETUTCDATE()", ">", true);

            return this.LoadByBuilder(new ConnectiveSqlClauseCollection(LogicOperatorDefine.And, stateBuilder, timeBuilder));
        }

        /// <summary>
        /// 读取需要停止的事件
        /// </summary>
        /// <returns></returns>
        public AMSEventCollection LoadNeedStopEvents()
        {
            InSqlClauseBuilder stateBuilder = new InSqlClauseBuilder("State");

            stateBuilder.AppendItem(AMSEventState.Running.ToString(), AMSEventState.Stopping.ToString());

            WhereSqlClauseBuilder timeBuilder = new WhereSqlClauseBuilder();

            timeBuilder.AppendItem("EndTime", "GETUTCDATE()", "<=", true);

            return this.LoadByBuilder(new ConnectiveSqlClauseCollection(LogicOperatorDefine.And, stateBuilder, timeBuilder));
        }

        public void DeleteByID(string id)
        {
            id.CheckStringIsNullOrEmpty("id");

            this.Delete(builder => builder.AppendItem("ID", id));
        }
    }
}
