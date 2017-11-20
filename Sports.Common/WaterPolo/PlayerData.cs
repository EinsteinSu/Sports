using DevExpress.Mvvm;

namespace Sports.Common.WaterPolo
{
    public class PlayerData : ViewModelBase
    {
        private int _fouls;

        private int _foulTime;

        private string _name;
        private int _number;

        public const int TwentySeconds = 20;

        public int Number
        {
            get => _number;
            set => SetProperty(ref _number, value, "Number");
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, "Name");
        }

        public virtual int Fouls
        {
            get => _fouls;
            set => SetProperty(ref _fouls, value, "Fouls");
        }

        public virtual int FoulTime
        {
            get => _foulTime;
            set => SetProperty(ref _foulTime, value, "FoulTime");
        }

        public void DecreaseFoulTime()
        {
            if (_foulTime > 0)
            {
                FoulTime--;
            }
        }
    }
}