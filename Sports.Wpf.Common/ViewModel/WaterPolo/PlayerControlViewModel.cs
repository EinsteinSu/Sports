using Sports.Common.WaterPolo;

namespace Sports.Wpf.Common.ViewModel.WaterPolo
{
    public class PlayerControlViewModel : PlayerData
    {
        //todo combine it with PlayerDisplayController
        public const string Cycle = "●";

        private int _fouls;
        private int _foulTime;
        public string Foul1 => Fouls > 0 ? Cycle : string.Empty;
        public string Foul2 => Fouls > 1 ? Cycle : string.Empty;
        public string Foul3 => Fouls > 2 ? Cycle : string.Empty;


        public override int Fouls
        {
            get => _fouls;
            set
            {
                if (value > _fouls)
                    FoulTime = 20;
                if (value > 3)
                    return;
                if (value < 0)
                    return;
                SetProperty(ref _fouls, value, "Fouls");
                RaisePropertyChanged("Foul1");
                RaisePropertyChanged("Foul2");
                RaisePropertyChanged("Foul3");
            }
        }

        public override int FoulTime
        {
            get => _foulTime;
            set
            {
                if (value > 20)
                    return;
                SetProperty(ref _foulTime, value, "FoulTime");
            }
        }
    }
}