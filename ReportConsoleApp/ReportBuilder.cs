using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReportConsoleApp
{
    class ReportBuilder
    {
        Reporter reporter = new Reporter();
        Dictionary<int, CancellationTokenSource> Dtasks = new Dictionary<int, CancellationTokenSource>();
        int taskID = 0;
        public void Build()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var task = Task.Run(() => BuildReport(taskID,cancellationToken), cancellationToken);
            taskID++;
            Dtasks.Add(taskID, cts);
        }

        private void BuildReport(int taskID,CancellationToken ct)
        {
            if (ct.IsCancellationRequested)
            {
                Console.WriteLine("Task {0} was cancelled before it got started.", taskID);
            }
            Console.WriteLine(taskID);
            byte[] data;
                double number = GetRandomNumber();
                int n = 0;
                try
                {
                    while (n < number)
                    {
                    if (ct.IsCancellationRequested)
                    {
                        Console.WriteLine("Task {0} cancelled", taskID);
                        return;
                    }
                    Thread.Sleep(1000);
                        n++;
                        if (n >= 20)
                        {
                            reporter.ReportTimeout((int)Task.CurrentId);
                            data = Encoding.Default.GetBytes($"Report ready at {number} s."); ;
                            return;
                        }
                    }
                    if (isFail())
                    {
                        reporter.ReportError((int)Task.CurrentId);
                        throw new Exception("Report failed");
                    }
                    data = Encoding.Default.GetBytes($"Report ready at {number} s.");
                    reporter.ReportSuccess(data, (int)Task.CurrentId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return;
         
        }

        public void Cancel(int taskID)
        {
            if (Dtasks.ContainsKey(taskID)) {
                Dtasks[taskID].Cancel();
            }else {
                Console.WriteLine("task with id {0} not found", taskID);
            }
        }

        private bool isFail()
        {
            if (GetRandomNumber(1, 5) == 3) return true;
            return false;
        }

        private double GetRandomNumber(int firstNumber = 5, int secondNumber = 45)
        {
            Random rnd = new Random();
            int value = rnd.Next(firstNumber, secondNumber);
            return value;
        }

        public void GertDictData()
        {
            foreach (int s in Dtasks.Keys)
            {
                Console.WriteLine("Key = {0}", s);
            }
        }
    }
}