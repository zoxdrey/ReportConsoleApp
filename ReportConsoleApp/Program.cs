using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReportConsoleApp
{
    class Program
    {
        static ReportBuilder reportBuilder = new ReportBuilder();
        static void Main(string[] args)
        {
    
            bool endApp = false;
            Console.WriteLine("Приложение принимает команды:");
            Console.WriteLine("BUILD. Без параметров. Возвращает целое: ID запущенной работы.");
            Console.WriteLine("STOP. Принимает целое: ID запущенной работы.");

            while (!endApp)
            {

                string command = Console.ReadLine();

                try
                {
                   
                    
                    if (command == "BUILD")
                    {
                        reportBuilder.Build(); 
                    }
                    if (command.Split(" ")[0] == "STOP")
                    {
                        reportBuilder.Cancel(Int32.Parse(command.Split(" ")[1]));
                    }
                    if(command == "data")
                    {
                        reportBuilder.GertDictData();
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if(command == "q") { endApp = true; }
            }
            return;

        }
    }
}
