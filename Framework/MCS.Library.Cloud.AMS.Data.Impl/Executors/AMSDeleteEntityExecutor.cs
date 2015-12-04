using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Executors
{
    public class AMSVDeleteEntityExecutor<TKey, TEntity> : AMSExecutorBase
    {
        private TKey _Key;
        private Action<TKey> _DataAction = null;

        public AMSVDeleteEntityExecutor(TKey key, Action<TKey> dataAction, AMSOperationType operationType)
            : base(operationType)
        {
            this._Key = key;
            this._DataAction = dataAction;
        }

        public TKey Entity
        {
            get
            {
                return this._Key;
            }
        }

        protected override object DoOperation(AMSOperationContext context)
        {
            if (this._DataAction != null)
                this._DataAction(this._Key);

            return this._Key;
        }

        protected override string GetOperationDescription()
        {
            string opDesp = EnumItemDescriptionAttribute.GetDescription(OperationType);
            string entityDesp = typeof(TEntity).Name;

            DescriptionAttribute despAttr = AttributeHelper.GetCustomAttribute<DescriptionAttribute>(typeof(TEntity));

            if (despAttr != null)
                entityDesp = despAttr.Description;

            return string.Format("{0}-{1}", opDesp, entityDesp);
        }

        protected override void PrepareOperationLog(AMSOperationContext context)
        {
            UserOperationLog log = new UserOperationLog();

            log.Subject = this.GetOperationDescription();
            log.ResourceID = this._Key.ToString();

            log.FillHttpContext();

            context.Logs.Add(log);
        }
    }
}
