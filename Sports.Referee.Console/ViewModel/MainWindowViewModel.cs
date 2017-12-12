using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using log4net;
using Newtonsoft.Json;
using Sports.Business;
using Sports.Common;
using Sports.Common.WaterPolo;
using Sports.DataAccess.Models;
using Sports.Timing;
using Sports.Timing.Interfaces;
using Sports.Timing.WaterPolo;
using Sports.Wpf.Common.ViewModel.WaterPolo;


namespace Sports.Referee.Console.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private RaceViewModel _race;
        private TotalTimeController _totalTimeController;
        private ThirtySecondsTimeController _thirtySecondsTimeController;
        private static readonly ILog Log = LogManager.GetLogger("Referee.MainWindowViewModel");

        public MainWindowViewModel()
        {
            var configFileName =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Configuration.config");
            if (!File.Exists(configFileName))
                throw new FileNotFoundException();
            var config = JsonConvert.DeserializeObject<RefereeConfiguration>(File.ReadAllText(configFileName));
            if (config == null || config.VenueId == 0)
                throw new ConfigurationErrorsException();
            Venue = new VenueMgr().GetItem(config.VenueId);
            Title = $"{Venue.Name} \n {Venue}";

            var controller = new SocketController(512);
            controller.StartListening(new RefereeRequestProcess(r => Race = r), Venue.Port);

            var display = new DisplayMgr().GetDisplayByVenueId(config.VenueId);
            if (display != null)
            {
                Task.Factory.StartNew(() =>
                {
                    SendRaceData(display.IPAddress, display.Port);
                });
            }

            Task.Factory.StartNew(SaveRaceData);

            InitializeSerialDevices();
        }

        ~MainWindowViewModel()
        {
            _quitSendRaceData = true;
            _quitSaveRaceData = true;
        }

        #region methods
        private bool _quitSendRaceData;

        private void SendRaceData(string ipAddress, int port)
        {
            while (true)
            {
                if (_quitSendRaceData)
                    break;
                try
                {
                    var raceRaw = new RaceRaw();
                    raceRaw.FromRace(Race);
                    SocketHelper.SendMessage(ipAddress, port, JsonConvert.SerializeObject(raceRaw), 512);
                }
                catch (Exception e)
                {
                    Log.Error($"Could not send the race data to {ipAddress}({port})", e);
                    //todo: alert ????
                }

                Thread.Sleep(500);
            }
        }


        private bool _quitSaveRaceData;

        private void SaveRaceData()
        {
            while (true)
            {
                if (_quitSaveRaceData)
                    break;
                try
                {
                    if (Race != null && Race.ScheduleId > 0)
                    {
                        var raceRaw = new RaceRaw();
                        raceRaw.FromRace(Race);
                        new ScheduleMgr().SaveRaceData(Race.ScheduleId, JsonConvert.SerializeObject(raceRaw));
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Could not save race data ${e.Message}", e);
                    throw;
                }
                Thread.Sleep(5000);
            }
        }

        private void InitializeSerialDevices()
        {
            var hardwares = new HardwareMgr().GetVenueHardwares(Venue.Id);
            foreach (var hardware in hardwares)
            {
                if (hardware.Usage.Contains("Total"))
                {
                    _totalTimeController = new TotalTimeController(hardware.Port);
                    try
                    {
                        _totalTimeController.StartListening();
                    }
                    catch (Exception e)
                    {
                        Log.Error($"Total time device got error {e.Message}", e);
                    }
                    _totalTimeController.DisplayData = data =>
                    {
                        Log.Debug(data.ToString());
                        Race.TotalTime = data.Time;
                        //todo: check settings whether get this data
                        Race.TeamA.Score = data.ScoreA;
                        Race.TeamB.Score = data.ScoreB;
                        Race.Court = data.Court;
                        Race.IsTimeout = data.Stopped;
                    };

                }
                if (hardware.Usage.Contains("30"))
                {
                    _thirtySecondsTimeController = new ThirtySecondsTimeController(hardware.Port);
                    try
                    {
                        _thirtySecondsTimeController.StartListening();
                    }
                    catch (Exception e)
                    {
                        Log.Error($"30 Seconds device got error {e.Message}", e);
                    }
                    _thirtySecondsTimeController.DisplayData = data =>
                    {
                        Log.Debug(Race.ToString());
                        Race.ThirtySeconds = data.Seconds;
                        if (!data.IsStopped)
                        {
                            Race.TeamA.DecreaseFoulTimes();
                            Race.TeamB.DecreaseFoulTimes();
                        }
                    };
                }
            }
        }
        #endregion

        #region properties
        public string Title { get; set; }

        public Venue Venue { get; set; }

        public RaceViewModel Race
        {
            get => _race;
            set => SetProperty(ref _race, value, "Race");
        }
        #endregion

        private class RefereeRequestProcess : IRequestProcess
        {
            private readonly Action<RaceViewModel> _action;

            public RefereeRequestProcess(Action<RaceViewModel> action)
            {
                _action = action;
            }

            //todo should be test
            public void Process(TcpClient client, NetworkStream stream, byte[] bytesReceived)
            {
                var response = new Command { Type = CommandType.Response, ValueType = typeof(ResponseType) };
                try
                {
                    var message = Encoding.Unicode.GetString(bytesReceived, 0, bytesReceived.Length);
                    var command = JsonConvert.DeserializeObject<Command>(message);
                    if (command != null && command.Type == CommandType.LoadRace)
                    {
                        var scheduleId = int.Parse(command.Value);
                        var teamMgr = new TeamMgr();
                        var mgr = new ScheduleMgr();
                        var s = mgr.GetItem(scheduleId);
                        var teamA = teamMgr.GetItem(s.TeamA);
                        var teamB = teamMgr.GetItem(s.TeamB);
                        var race = new RaceViewModel
                        {
                            ScheduleId = scheduleId,
                            TotalTime = "8:00",
                            Court = 1,
                            TeamA = new TeamControlViewModel { TeamName = teamA.ShortName, Players = GetScheduledPlayers(mgr, scheduleId, teamA.Id) },
                            TeamB = new TeamControlViewModel { TeamName = teamB.ShortName, Players = GetScheduledPlayers(mgr, scheduleId, teamB.Id) }
                        };

                        _action?.Invoke(race);
                        response.Value = ResponseType.Success.ToString();
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Process socket data error {e.Message}", e);
                    response.Value = ResponseType.Faild.ToString();
                }
                var bytesSent = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(response));
                stream.Write(bytesSent, 0, bytesSent.Length);
            }

            private PlayerData[] GetScheduledPlayers(IScheduleMgr mgr, int scheduleId, int teamId)
            {
                var list = mgr.GetScheduledPlayers(scheduleId, teamId).ToList();
                var players = new PlayerData[list.Count];
                int i = 0;
                foreach (var player in list)
                {
                    players[i] = new PlayerControlViewModel
                    {
                        Number = player.BibNumber.ToInt(),
                        Name = player.Name
                    };
                    i++;
                }
                return players;
            }
        }
    }
}