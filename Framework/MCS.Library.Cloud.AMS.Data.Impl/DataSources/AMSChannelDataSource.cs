using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Impl.DataSources
{
    public class AMSChannelDataSource : ObjectDataSourceQueryAdapterBase<AMSChannel, AMSChannelCollection>
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            if (qc.OrderByClause.IsNullOrEmpty())
                qc.OrderByClause = "IsDefault DESC";
        }
    }
}
