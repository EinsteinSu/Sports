using System;
using Sports.Common;

namespace Sports.Timing.WaterPolo
{
    public class ThirtySecondsTimeController : SerialPortController
    {
        public ThirtySecondsTimeController(int port, int bitRate = 4800, int dataBits = 8) : base(port, bitRate,
            dataBits)
        {
        }

        public Action<SerialPortThirtySecondsData> DisplayData { get; set; }

        protected override int Header => 0xEF;
        protected override int Tail => 0xBF;

        protected override int Length => 3;

        protected override void ProcessData(int[] ints)
        {
            var thrity = new SerialPortThirtySecondsData();
            var data = ints[0].ToString("X").PadLeft(2, '0');
            data = data.Substring(1, 1) + data.Substring(0, 1);
            thrity.Seconds = data.ToInt();
            thrity.IsStopped = ints[2] == 0xFB;
            DisplayData?.Invoke(thrity);
        }
    }

    public class SerialPortThirtySecondsData
    {
        public int Seconds { get; set; }

        public bool IsStopped { get; set; }

        public override string ToString()
        {
            return $"{Seconds}({IsStopped})";
        }
    }
}