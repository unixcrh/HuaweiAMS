using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using MCS.Library.Data.Mapping;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MCS.Library.Cloud.AMS.Data.Adapters
{
    public class AMSQueueSqlAdapter : IQueue<AMSQueueItem>
    {
        public static AMSQueueSqlAdapter Instance = new AMSQueueSqlAdapter();

        public void AddMessages(string category, params AMSQueueItem[] messages)
        {
            messages.NullCheck("message");

            StringBuilder strB = new StringBuilder();

            foreach (AMSQueueItem item in messages)
            {
                if (strB.Length > 0)
                    strB.Append(TSqlBuilder.Instance.DBStatementSeperator);

                strB.Append(ORMapping.GetInsertSql(item, TSqlBuilder.Instance));
            }

            if (strB.Length > 0)
                DbHelper.RunSqlWithTransaction(strB.ToString(), GetConnectionName());
        }

        public IEnumerable<AMSQueueItem> GetMessages(string category, int count = 1)
        {
            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                AMSQueueItemCollection result = QueryData(GetQuerySql(category, count));

                InSqlClauseBuilder builder = new InSqlClauseBuilder("ID");

                result.ForEach(item => builder.AppendItem(item.ID));

                if (builder.IsEmpty == false)
                {
                    string sql = string.Format("DELETE {0} WHERE {1}", GetTableName(), builder.ToSqlStringWithInOperator(TSqlBuilder.Instance));

                    DbHelper.RunSql(sql, GetConnectionName());
                }

                scope.Complete();

                return result;
            }
        }

        public IEnumerable<AMSQueueItem> PeekMessages(string category, int count = 1)
        {
            return QueryData(GetQuerySql(category, count));
        }

        public void ClearQueue()
        {
            DbHelper.RunSql(string.Format("TRUNCATE TABLE {0}", GetTableName()),
                GetConnectionName());
        }

        private static AMSQueueItemCollection QueryData(string sql)
        {
            AMSQueueItemCollection result = new AMSQueueItemCollection();

            DataView view = DbHelper.RunSqlReturnDS(sql, GetConnectionName()).Tables[0].DefaultView;

            ORMapping.DataViewToCollection(result, view);

            return result;
        }

        private static string GetQuerySql(string category, int count)
        {
            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            if (category.IsNotEmpty())
                builder.AppendItem("Category", category);

            string sql = string.Format("SELECT TOP {0} * FROM {1} WITH(UPDLOCK READPAST)", count, GetTableName());

            if (builder.IsEmpty == false)
                sql += " WHERE " + builder.ToSqlString(TSqlBuilder.Instance);

            sql += " ORDER BY [ID]";

            return sql;
        }

        private static string GetTableName()
        {
            return ORMapping.GetMappingInfo<AMSQueueItem>().TableName;
        }

        private static string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }
    }
}
