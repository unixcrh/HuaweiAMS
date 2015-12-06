using MCS.Library.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Entities
{
    [Serializable]
    public class AMSCheckLockException : Exception
    {
        public AMSCheckLockException() :
            base()
        {
        }

        public AMSCheckLockException(string message) :
            base(message)
        {
        }

        public AMSCheckLockException(string message, System.Exception innerException) :
            base(message, innerException)
        {
        }

        public static string CheckLockResultToMessage(AMSCheckLockResult checkResult)
        {
            checkResult.NullCheck("checkResult");

            StringBuilder strB = new StringBuilder();

            strB.AppendFormat("申请{0}失败。", EnumItemDescriptionAttribute.GetDescription(checkResult.Lock.LockType));

            if (checkResult.Lock.LockPersonName.IsNotEmpty())
                strB.AppendFormat("正在由\"{0}\"执行\"{1}\"。", checkResult.Lock.LockPersonName, checkResult.Lock.Description);
            else
                strB.AppendFormat("正在执行\"{0}\"", checkResult.Lock.Description);

            strB.Append("请稍后再尝试。");

            return strB.ToString();
        }
    }
}
