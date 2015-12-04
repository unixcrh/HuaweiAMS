using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using MCS.Library.Data.Adapters;
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

        public void DeleteByID(string id)
        {
            id.CheckStringIsNullOrEmpty("id");

            this.Delete(builder => builder.AppendItem("ID", id));
        }
    }
}
