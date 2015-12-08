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
            Trace.TraceInformation("启动任务");

            AMSTask.StartAllTasks(cancellationTokenSource.Token);

            while (cancellationTokenSource.IsCancellationRequested == false)
            {
                Console.WriteLine("Please input command...");
                string cmd = Console.ReadLine();

                switch (cmd.ToLower())
                {
                    case "exit":
                        cancellationTokenSource.Cancel();
                        break;
                    case "clearevents":
                        DataHelper.ClearAllEvents();
                        Console.WriteLine("All events cleared");
                        break;
                    case "addevent":
                        DataHelper.AddEvent();
                        break;
                    case "clearqueue":
                        DataHelper.ClearQueue();
                        Console.WriteLine("Queue cleared");
                        break;
                    case "clearlocks":
                        LockHelper.ClearAll();
                        Console.WriteLine("Lock cleared");
                        break;
                    case "endevents":
                        Console.WriteLine("{0} events updated.", DataHelper.EndAllRunningEvents());
                        break;
                }
            }
        }
    }
}
