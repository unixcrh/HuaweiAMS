using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Core;
using MCS.Library.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MCS.Library.Cloud.AMS.Data.Executors
{
    public abstract class AMSExecutorBase
    {
        private bool _AutoStartTransaction = true;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="opType"></param>
        protected AMSExecutorBase(AMSOperationType opType)
        {
            this.OperationType = opType;
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public AMSOperationType OperationType
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否自动启动事务
        /// </summary>
        public bool AutoStartTransaction
        {
            get
            {
                return this._AutoStartTransaction;
            }
            protected set
            {
                this._AutoStartTransaction = value;
            }
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <returns></returns>
        public object Execute()
        {
            object result = null;

            ExecutionWrapper(this.GetOperationDescription(),
                    () => result = InternalExecute());

            return result;
        }

        /// <summary>
        /// 执行在事务内具体的数据操作，需要重载
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected abstract object DoOperation(AMSOperationContext context);

        /// <summary>
        /// 准备数据，包括校验数据。这个操作在事务之外
        /// </summary>
        /// <param name="context"></param>
        protected virtual void PrepareData(AMSOperationContext context)
        {
        }

        /// <summary>
        /// 准备操作日志
        /// </summary>
        /// <param name="context"></param>
        protected virtual void PrepareOperationLog(AMSOperationContext context)
        {
        }

        /// <summary>
        /// 得到操作的名称
        /// </summary>
        protected virtual string GetOperationDescription()
        {
            return EnumItemDescriptionAttribute.GetDescription(OperationType);
        }

        private void PersistOperationLog(AMSOperationContext context)
        {
            using (TransactionScope scope = TransactionScopeFactory.Create())
            {
                context.Logs.ForEach(log => UserOperationLogSqlAdapter.Instance.Add(log));

                scope.Complete();
            }
        }

        private object InternalExecute()
        {
            AMSOperationContext context = new AMSOperationContext(this.OperationType, this);

            ExecutionWrapper("PrepareData", () => PrepareData(context));
            ExecutionWrapper("PrepareOperationLog", () => PrepareOperationLog(context));

            object result = null;

            if (this.AutoStartTransaction)
            {
                using (TransactionScope scope = TransactionScopeFactory.Create())
                {
                    ExecutionWrapper("DoOperation", () => result = DoOperation(context));
                    ExecutionWrapper("PersistOperationLog", () => PersistOperationLog(context));

                    scope.Complete();
                }
            }
            else
            {
                ExecutionWrapper("DoOperation", () => result = DoOperation(context));
                ExecutionWrapper("PersistOperationLog", () => PersistOperationLog(context));
            }

            return result;
        }

        private static void ExecutionWrapper(string operationName, Action action)
        {
            operationName.CheckStringIsNullOrEmpty("operationName");
            action.NullCheck("action");

            AMSExecutorLogContextInfo.Writer.WriteLine("\t\t{0}开始：{1:yyyy-MM-dd HH:mm:ss.fff}",
                    operationName, DateTime.Now);

            Stopwatch sw = new Stopwatch();

            sw.Start();
            try
            {
                action();
            }
            finally
            {
                sw.Stop();
                AMSExecutorLogContextInfo.Writer.WriteLine("\t\t{0}结束：{1:yyyy-MM-dd HH:mm:ss.fff}；经过时间：{2:#,##0}毫秒",
                    operationName, DateTime.Now, sw.ElapsedMilliseconds);
            }
        }
    }
}
