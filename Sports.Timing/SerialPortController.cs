using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace Sports.Timing
{
    public abstract class SerialPortController
    {
        private readonly SerialPort _serialPort;

        public SerialPortController(int port, int bitRate = 4800, int dataBits = 8)
        {
            _serialPort = new SerialPort($"COM{port}", bitRate, Parity.None, dataBits);
        }

        protected abstract int Header { get; }


        protected abstract int Tail { get; }

        protected virtual int Length => 6;

        protected abstract void ProcessData(int[] ints);

        protected bool IsQuit { get; set; }

        public void StartListening()
        {
            _serialPort.Open();
            Task.Factory.StartNew(Listen);
        }

        protected virtual void Listen()
        {
            while (true)
            {
                if (IsQuit)
                    break;
                if (_serialPort.ReadByte() == Header)
                {
                    var i = 0;
                    var bytes = new int[Length];
                    while (true)
                    {
                        var data = _serialPort.ReadByte();
                        if (data == Tail || i > Length)
                            break;
                        bytes[i] = data;
                        i++;
                    }
                    ProcessData(bytes);
                }
            }
        }

        ~SerialPortController()
        {
            EndListening();
        }

        public void EndListening()
        {
            IsQuit = true;
            _serialPort?.Close();
        }
    }
}