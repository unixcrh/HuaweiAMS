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
    public class UserOperationLogSqlAdapter : UpdatableAndLoadableAdapterBase<UserOperationLog, UserOperationLogCollection>
    {
        public static readonly UserOperationLogSqlAdapter Instance = new UserOperationLogSqlAdapter();

        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }

        public UserOperationLogCollection LoadByResourceID(string resourceID)
        {
            resourceID.CheckStringIsNullOrEmpty("resourceID");

            return this.LoadByInBuilder(builder => builder.AppendItem("ResourceID", resourceID),
                oBuilder => oBuilder.AppendItem("CreateTime", FieldSortDirection.Descending), "ResourceID");
        }

        public UserOperationLog LoadByID(long id)
        {
            return this.LoadByInBuilder(builder => builder.AppendItem(id), "ID").SingleOrDefault();
        }

        public long Add(UserOperationLog log)
        {
            log.NullCheck("log");

            Dictionary<string, object> context = new Dictionary<string, object>();

            string sql = this.GetInsertSql(log, this.GetMappingInfo(context), context);

            decimal result = (decimal)DbHelper.RunSqlReturnScalar(string.Format("{0} \n SELECT @@IDENTITY", sql), this.GetConnectionName());

            return decimal.ToInt64(result);
        }
    }
}
