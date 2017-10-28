using DevExpress.Mvvm;
using Sports.Business;
using Sports.Common;
using Sports.Timing;

namespace Sports.Race.Console.ViewModel
{
    public class RaceMgmtViewModel : ViewModelBase
    {
        private readonly IVenueMgr _mgr;
        private readonly int _venueId;
        private DelegateCommand _loadRaceCommand;

        public RaceMgmtViewModel(IVenueMgr mgr, int venueId)
        {
            _mgr = mgr;
            _venueId = venueId;
            LoadRaceCommand = new DelegateCommand(LoadRace);
        }

        public DelegateCommand LoadRaceCommand
        {
            get => _loadRaceCommand;
            set => SetProperty(ref _loadRaceCommand, value, "LoadRaceCommand");
        }


        public void LoadRace()
        {
            if (_mgr.IsStarted(_venueId))
            {
                //todo: just waiting for the referee console's data, notify customer this race has already started
            }
            else
            {
                var venue = _mgr.GetItem(_venueId);
                var command = new Command
                {
                    Type = CommandType.LoadRace,
                    Value = venue.Id.ToString(),
                    ValueType = typeof(int)
                };
                SocketHelper.SendMessage(venue.IPAddress, venue.Port, command.ToString(), 256);
            }
        }
    }
}