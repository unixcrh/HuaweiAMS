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
    public class AMSChannelInEventDataSource : ObjectDataSourceQueryAdapterBase<AMSChannelInEvent, AMSChannelInEventCollection>
    {
        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }

        protected override void OnBuildQueryCondition(QueryCondition qc)
        {
            qc.FromClause = "AMS.Channels C INNER JOIN AMS.EventsChannels EC ON C.ID = EC.ChannelID";
            qc.SelectFields = "C.*, EC.DefaultPlaybackUrl, EC.CDNPlaybackUrl, EC.IsDefault";

            if (qc.OrderByClause.IsNullOrEmpty())
                qc.OrderByClause = "IsDefault DESC";
        }
    }
}
