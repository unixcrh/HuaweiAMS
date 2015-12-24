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
    public class AMSUserViewSqlAdapter : UpdatableAndLoadableAdapterBase<AMSUserView, AMSUserViewCollection>
    {
        public static AMSUserViewSqlAdapter Instance = new AMSUserViewSqlAdapter();

        private AMSUserViewSqlAdapter()
        {
        }

        public void UpdateUserView(AMSUserView userView)
        {
            userView.NullCheck("userView");

            Dictionary<string, object> context = new Dictionary<string, object>();
            ORMappingItemCollection mappingInfo = this.GetMappingInfo(context);

            StringBuilder strB = new StringBuilder();

            UpdateSqlClauseBuilder updateBuilder = ORMapping.GetUpdateSqlClauseBuilder(userView, mappingInfo);

            updateBuilder.AppendItem("LastAccessTime", "GETUTCDATE()", "=", true);

            strB.AppendFormat("UPDATE {0} SET {1} WHERE {2}",
                mappingInfo.TableName, updateBuilder.ToSqlString(TSqlBuilder.Instance), 
                ORMapping.GetWhereSqlClauseBuilderByPrimaryKey(userView, mappingInfo).ToSqlString(TSqlBuilder.Instance));

            strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

            strB.AppendFormat("IF @@ROWCOUNT = 0\n");
            strB.Append("BEGIN\n");
            strB.Append(this.GetInsertSql(userView, mappingInfo, context) + "\n");
            strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

            string eventTableName = ORMapping.GetMappingInfo(typeof(AMSEvent)).TableName;

            strB.AppendFormat("UPDATE {0} SET Views = Views + 1 WHERE ID = {1}\n",
                eventTableName, TSqlBuilder.Instance.CheckUnicodeQuotationMark(userView.EventID));
            strB.Append("END\n");

            DbHelper.RunSqlWithTransaction(strB.ToString(), this.GetConnectionName());
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }
    }
}
