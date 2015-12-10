using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Worker.Tasks
{
    public static class LockHelper
    {
        public static AMSCheckLockResult ExtendLockTime(AMSEvent eventData)
        {
            AMSLock lockData = PrepareEventLock(eventData);

            return AMSLockSqlAdapter.Instance.ExtendLockTime(lockData);
        }

        public static bool IsLockAvailable(AMSEvent eventData)
        {
            AMSLock lockData = PrepareEventLock(eventData);

            AMSCheckLockResult lockResult = AMSLockSqlAdapter.Instance.AddLock(lockData);

            return lockResult.Available;
        }

        public static void Unlock(AMSEvent eventData)
        {
            try
            {
                AMSLockSqlAdapter.Instance.DeleteLock(eventData.ID);
            }
            catch (System.Exception ex)
            {
                Trace.TraceError(ex.ToString());
            }
        }

        public static void ClearAll()
        {
            AMSLockSqlAdapter.Instance.ClearAll();
        }

        /// <summary>
        /// 准备事件相关的锁
        /// </summary>
        /// <returns></returns>
        public static AMSLock PrepareEventLock(AMSEvent eventData)
        {
            AMSLock lockData = new AMSLock();

            lockData.LockID = eventData.ID;
            lockData.LockType = AMSLockType.EventLock;
            lockData.Description = string.Format("Add lock for event(ID: {0}, Name: {1})", eventData.ID, eventData.Name);

            return lockData;
        }
    }
}
