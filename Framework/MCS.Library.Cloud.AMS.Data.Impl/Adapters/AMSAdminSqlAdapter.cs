using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Library.Data;
using MCS.Library.Data.Adapters;
using MCS.Library.Data.Builder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Adapters
{
    public class AMSAdminSqlAdapter : UpdatableAndLoadableAdapterBase<AMSAdmin, AMSAdminCollection>
    {
        public static readonly AMSAdminSqlAdapter Instance = new AMSAdminSqlAdapter();

        private AMSAdminSqlAdapter()
        {
        }

        public void SetPassword(string logonName, string password)
        {
            logonName.CheckStringIsNullOrEmpty("logonName");

            WhereSqlClauseBuilder wBuilder = new WhereSqlClauseBuilder();

            wBuilder.AppendItem("LogonName", logonName);

            UpdateSqlClauseBuilder uBuilder = new UpdateSqlClauseBuilder();
            uBuilder.AppendItem("[Password]", CalculatePassword(password));

            string sql = string.Format("UPDATE {0} SET {1} WHERE {2}",
                this.GetTableName(),
                uBuilder.ToSqlString(TSqlBuilder.Instance),
                wBuilder.ToSqlString(TSqlBuilder.Instance));

            DbHelper.RunSql(sql, this.GetConnectionName());
        }

        public AMSAdmin CheckPassword(string logonName, string password)
        {
            logonName.CheckStringIsNullOrEmpty("logonName");

            WhereSqlClauseBuilder builder = new WhereSqlClauseBuilder();

            builder.AppendItem("LogonName", logonName);
            builder.AppendItem("[Password]", CalculatePassword(password));

            string sql = string.Format("SELECT * FROM {0} WHERE {1}",
                this.GetTableName(), builder.ToSqlString(TSqlBuilder.Instance));

            return this.QueryData(sql).SingleOrDefault();
        }

        private static string CalculatePassword(string password)
        {
            string strResult = password;

            using (MD5 md = new MD5CryptoServiceProvider())
            {
                strResult = BitConverter.ToString(md.ComputeHash((new UnicodeEncoding()).GetBytes(password)));
            }

            return strResult;
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }
    }
}
