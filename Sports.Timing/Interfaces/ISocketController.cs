using System.Net.Sockets;

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