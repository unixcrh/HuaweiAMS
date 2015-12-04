using MCS.Library.Caching;
using MCS.Library.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Data.Executors
{
    public sealed class AMSExecutorLogContextInfo
    {
        public static TextWriter Writer
        {
            get
            {
                object writer = null;

                if (ObjectContextCache.Instance.TryGetValue("AMSExecutorLogContextInfoWriter", out writer) == false)
                {
                    StringBuilder strB = new StringBuilder(1024);

                    writer = new StringWriter(strB);

                    ObjectContextCache.Instance["AMSExecutorLogContextInfoWriter"] = writer;
                }

                return (TextWriter)writer;
            }
        }

        /// <summary>
        /// 提交到真正的Logger
        /// </summary>
        public static void CommitInfoToLogger()
        {
            StringBuilder strB = ((StringWriter)Writer).GetStringBuilder();

            if (strB.Length > 0)
            {
                try
                {
                    Logger logger = LoggerFactory.Create("AMSExecutor");

                    if (logger != null)
                        logger.Write(strB.ToString(), LogPriority.Normal, 8008, TraceEventType.Information, "AMSExecutor上下文信息");
                }
                catch (System.Exception)
                {
                }
                finally
                {
                    strB.Clear();
                }
            }
        }
    }
}
