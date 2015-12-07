using MCS.Library.Cloud.AMS.Data.Contracts;
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

        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }
    }
}
