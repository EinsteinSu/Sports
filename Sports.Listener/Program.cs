using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sports.Timing;
using Sports.Timing.Interfaces;

namespace Sports.Listener
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = int.Parse(args[0]);
            
            SocketController controller = new SocketController(1024);
            controller.StartListening(new DataProcess(), port);
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.S:
                            var data = new TestData();
                            data.Name = "Sue";
                            data.Orders = new List<string>();
                            for (int i = 0; i < 10; i++)
                            {
                                data.Orders.Add($"Orders {i}");
                            }
                            SocketHelper.SendMessage("::1", 123, JsonConvert.SerializeObject(data),512);
                            break;
                        case ConsoleKey.O:
                            SocketHelper.SendMessage("::1", 123, "test",512);
                            break;
                        case ConsoleKey.Q:
                            controller.StopListening();
                            break;
                    }
                }
                Thread.Sleep(100);
            }
        }

        class DataProcess : IRequestProcess
        {
            public void Process(TcpClient client, NetworkStream stream, byte[] bytesReceived)
            {
                string response = "I got it";
                try
                {
                    var message = Encoding.Unicode.GetString(bytesReceived, 0, bytesReceived.Length);
                    Console.WriteLine(message);
                }
                catch (Exception e)
                {
                    response = "error";
                }

                var bytesSent = Encoding.Unicode.GetBytes(response);
                stream.Write(bytesSent, 0, bytesSent.Length);
            }
        }
    }

    public class TestData
    {
        public string Name { get; set; }

        public List<string> Orders { get; set; }
    }
}
