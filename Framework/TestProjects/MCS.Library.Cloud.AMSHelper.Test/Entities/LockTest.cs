using MCS.Library.Cloud.AMS.Data.Adapters;
using MCS.Library.Cloud.AMS.Data.Entities;
using MCS.Library.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace MCS.Library.Cloud.AMSHelper.Test.Entities
{
    [TestClass]
    public class LockTest
    {
        [TestMethod]
        [TestCategory("Locks")]
        [Description("增加一个新的锁")]
        public void AddNewLockTest()
        {
            AMSLock lockData = new AMSLock();
            lockData.LockID = UuidHelper.NewUuidString();

            AMSCheckLockResult result = AMSLockSqlAdapter.Instance.AddLock(lockData);

            Assert.IsTrue(result.Available);
            Assert.IsTrue(result.Lock.LockTime > DateTime.MinValue);
        }

        [TestMethod]
        [TestCategory("Locks")]
        [Description("试图增加一个在有效期内的已经存在的锁")]
        public void AddExistedNewLockTest()
        {
            AMSLock lockData = new AMSLock();
            lockData.LockID = UuidHelper.NewUuidString();

            AMSCheckLockResult result = AMSLockSqlAdapter.Instance.AddLock(lockData);

            //重复加锁
            result = AMSLockSqlAdapter.Instance.AddLock(lockData);

            Assert.IsFalse(result.Available);
            Assert.AreEqual(AMSCheckLockStatus.Locked, result.LockStatus);
        }

        [TestMethod]
        [TestCategory("Locks")]
        [Description("增加一个在有效期之外的已经存在的锁")]
        public void AddExistedExpiredNewLockTest()
        {
            AMSLock lockData = new AMSLock();
            lockData.EffectiveTime = TimeSpan.FromSeconds(1);
            lockData.LockID = UuidHelper.NewUuidString();

            AMSCheckLockResult result = AMSLockSqlAdapter.Instance.AddLock(lockData);

            Thread.Sleep(1100);

            //重复加锁
            result = AMSLockSqlAdapter.Instance.AddLock(lockData);

            Assert.IsTrue(result.Available);
            Assert.AreEqual(AMSCheckLockStatus.LockExpired, result.LockStatus);
            Assert.IsTrue(result.Lock.LockTime > DateTime.MinValue);
        }

        [TestMethod]
        [TestCategory("Locks")]
        [Description("试图延长一个在有效期内的已经存在的锁")]
        public void ExtendExistedLockTest()
        {
            AMSLock lockData = new AMSLock();
            lockData.LockID = UuidHelper.NewUuidString();

            AMSCheckLockResult result = AMSLockSqlAdapter.Instance.AddLock(lockData);

            Thread.Sleep(500);

            //延长
            AMSCheckLockResult extendedResult = AMSLockSqlAdapter.Instance.ExtendLockTime(lockData);

            Assert.IsTrue(extendedResult.Lock.LockTime > result.Lock.LockTime);

            Console.WriteLine("Original time: {0:yyyy-MM-dd HH:mm:ss.ffff}, extended time: {1:yyyy-MM-dd HH:mm:ss.ffff}",
                result.Lock.LockTime, extendedResult.Lock.LockTime);
        }

        [TestMethod]
        [TestCategory("Locks")]
        [Description("试图延长一个在过期的已经存在的锁")]
        public void ExtendExistedExpiredLockTest()
        {
            AMSLock lockData = new AMSLock();
            lockData.LockID = UuidHelper.NewUuidString();
            lockData.EffectiveTime = TimeSpan.FromSeconds(1);

            AMSCheckLockResult result = AMSLockSqlAdapter.Instance.AddLock(lockData);

            Thread.Sleep(1100);

            //延长
            AMSCheckLockResult extendedResult = AMSLockSqlAdapter.Instance.ExtendLockTime(lockData);

            Assert.IsTrue(extendedResult.Lock.LockTime > result.Lock.LockTime);

            Console.WriteLine("Original time: {0:yyyy-MM-dd HH:mm:ss.ffff}, extended time: {1:yyyy-MM-dd HH:mm:ss.ffff}",
                result.Lock.LockTime, extendedResult.Lock.LockTime);
        }
    }
}
