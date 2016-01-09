using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.DataSources
{
    /// <summary>
    /// 这个DataSource主要用显示某个事件下的频道列表
    /// </summary>
    public class AMSEventDataSource : ObjectDataSourceQueryAdapterBase<AMSEvent, AMSEventCollection>
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            if (qc.OrderByClause.IsNullOrEmpty())
                qc.OrderByClause = "StartTime DESC";
        }
    }
}
