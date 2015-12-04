using MCS.Library.Cloud.AMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Impl.Executors
{
    [Serializable]
    public class AMSOperationContext : Dictionary<string, object>
    {
        private AMSOperationType _OperationType = AMSOperationType.None;
        private AMSExecutorBase _Executor = null;
        private UserOperationLogCollection _Logs = null;

        public AMSOperationContext(AMSOperationType opType, AMSExecutorBase executor)
        {
            this._OperationType = opType;
            this._Executor = executor;
        }

        /// <summary>
        /// 获取执行器
        /// </summary>
        public AMSExecutorBase Executor
        {
            get
            {
                return this._Executor;
            }
        }

        /// <summary>
        /// 获取表示操作类型的<see cref="INVOperationType"/>值之一。
        /// </summary>
        public AMSOperationType OperationType
        {
            get
            {
                return this._OperationType;
            }
        }

        /// <summary>
        /// 获取操作日志的集合
        /// </summary>
        public UserOperationLogCollection Logs
        {
            get
            {
                if (this._Logs == null)
                    this._Logs = new UserOperationLogCollection();

                return this._Logs;
            }
        }
    }
}
