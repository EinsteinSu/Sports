using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sports.Timing;
using Sports.Timing.WaterPolo;

namespace Sports.SerialPort.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var port = int.Parse(args[0]);
            var serialPort = new TotalTimeController(port);
            serialPort.StartListening();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.S:
                          
                            break;
                        case ConsoleKey.O:
                            //SocketController.SendMessage("::1", 123, "test");
                            break;
                        case ConsoleKey.Q:
                            serialPort.EndListening();
                            break;
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
