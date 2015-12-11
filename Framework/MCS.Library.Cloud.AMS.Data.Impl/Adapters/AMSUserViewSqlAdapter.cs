using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Data.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Impl.Adapters
{
    public class AMSUserViewSqlAdapter : UpdatableAndLoadableAdapterBase<AMSUserView, AMSUserViewCollection>
    {
        public static AMSUserViewSqlAdapter Instance = new AMSUserViewSqlAdapter();

        private AMSUserViewSqlAdapter()
        {
        }

        protected override string GetConnectionName()
        {
            return ConnectionDefine.DBConnectionName;
        }
    }
}
