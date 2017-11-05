using System;

namespace Sports.Timing.WaterPolo
{
    public class TotalTimeController : SerialPortController
    {
        public TotalTimeController(int port, int bitRate = 4800, int dataBits = 8) : base(port, bitRate, dataBits)
        {
        }

        public Action<SerialPortTotalTimeData> DisplayData { get; set; }

        protected override int Header => 0xDF;

        protected override int Tail => 0xCF;

        protected override void ProcessData(int[] ints)
        {
            var data = new SerialPortTotalTimeData
            {
                Time = $"{ints[0].ToString("X").PadLeft(2, '0')}:{ints[1].ToString("X").PadLeft(2, '0')}",
                Stopped = ints[2] == 0xFA,
                ScoreA = ints[3],
                ScoreB = ints[4],
                Court = ints[5]
            };
            string item = string.Empty;
            foreach (var i in ints)
            {
                item += $"{i},";
            }
            //Console.WriteLine(item);
            DisplayData?.Invoke(data);
            //Console.WriteLine(data);
        }
    }

    public class SerialPortTotalTimeData
    {
        public string Time { get; set; }

        public bool Stopped { get; set; }

        public int ScoreA { get; set; }

        public int ScoreB { get; set; }

        public int Court { get; set; }

        public override string ToString()
        {
            return $"Total:{Time}, Stopped:{Stopped}, ScoreA {ScoreA}, ScoreB {ScoreB}, Court {Court}";
        }
    }
}