using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sports.Timing.Interfaces
{
    public interface ISocketController
    {
        void StartListening(IRequestProcess process, int port);

        void StopListening();
    }

    public interface IRequestProcess
    {
        void Process(TcpClient client, NetworkStream stream, byte[] bytesReceived);
    }
}
