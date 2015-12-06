using MCS.Library.Cloud.AMS.Worker.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCS.Library.Cloud.AMS.Worker.Test
{
    class Program
    {
        private static readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        static void Main(string[] args)
        {
            while (true)
            {
                AMSTask.ExeucteAllTasks(cancellationTokenSource.Token).Wait();

                Trace.TraceInformation("Working");
                Task.Delay(1000).Wait();
            }
        }
    }
}
