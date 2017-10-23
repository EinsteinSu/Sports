using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sports.Timing.Interfaces;

namespace Sports.Timing
{
    public class SocketController : ISocketController
    {
        private readonly int _compacity;
        private Server _server;

        public SocketController(int compacity)
        {
            _compacity = compacity;
        }

        public void StartListening(IRequestProcess process, int port)
        {
            _server = new Server();
            Task.Factory.StartNew(() =>
            {
                _server.CreateListener(port, process, _compacity);
            });
        }

        public void StopListening()
        {
            _server?.QuitListener();
        }

        public class Server
        {
            private bool _isQuit;

            /// <summary>
            ///     create a listener to retreive the request
            /// </summary>
            /// <param name="port">listening port</param>
            /// <param name="requestProcess">the interface that how to process the request</param>
            /// <param name="campacity">how big can be processed in per times.</param>
            public void CreateListener(int port, IRequestProcess requestProcess, int campacity)
            {
                TcpListener tcpListener;
                var ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
                try
                {
                    tcpListener = new TcpListener(ipAddress, port);
                    tcpListener.Start();
                    Console.WriteLine("Listening ...");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                while (true)
                {
                    if (_isQuit)
                        break;
                    Thread.Sleep(10);
                    var tcpClient = tcpListener.AcceptTcpClient();
                    var bytes = new byte[campacity];
                    var stream = tcpClient.GetStream();
                    stream.Read(bytes, 0, bytes.Length);
                    requestProcess.Process(tcpClient, stream, bytes);
                }
            }

            public void QuitListener()
            {
                _isQuit = true;
            }
        }
    }

    public static class SocketHelper
    {
        public static void SendMessage(string ipAddress, int port, string message, int compacity)
        {
            try
            {
                var client = new TcpClient(ipAddress, port);
                var data = Encoding.Unicode.GetBytes(message);
                var stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                Console.WriteLine("already sent message");
                data = new byte[compacity];
                var bytes = stream.Read(data, 0, data.Length);
                var responseData = Encoding.Unicode.GetString(data, 0, bytes);
                Console.WriteLine(responseData);
                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

}