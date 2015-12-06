using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Entities
{
    [Serializable]
    public class AMSCheckLockResult
    {
        public bool Available
        {
            get
            {
                return this.LockStatus == AMSCheckLockStatus.NotLocked || this.LockStatus == AMSCheckLockStatus.LockExpired;
            }
        }

        public AMSCheckLockStatus LockStatus
        {
            get;
            set;
        }

        private AMSLock _Lock = null;

        public AMSLock Lock
        {
            get
            {
                return this._Lock;
            }
            set
            {
                this._Lock = value;
            }
        }
    }
}
