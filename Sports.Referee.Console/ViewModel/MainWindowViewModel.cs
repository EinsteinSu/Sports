using System;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Text;
using DevExpress.Mvvm;
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

            Race = new RaceControllViewModel
            {
                TotalTime = "8:00",
                Court = 1,
                TeamA = new TeamControlViewModel { Score = 1 },
                TeamB = new TeamControlViewModel { TimeoutCount = 1 }
            };
            Race.TeamA.TeamName = "CHN";
            Race.TeamB.TeamName = "RUS";
            Race.TeamA.Players = new PlayerData[13];
            Race.TeamB.Players = new PlayerData[13];
            for (int i = 0; i < 13; i++)
            {
                Race.TeamA.Players[i] = new PlayerControlViewModel { Number = i + 1, Name = $"Team A {i}" };
                Race.TeamB.Players[i] = new PlayerControlViewModel { Number = i + 1, Name = $"Team A {i}" };
            }
            InitializeSerialDevices();
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
                        //todo: logging
                    }
                    _totalTimeController.DisplayData = data =>
                    {
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
                    }
                    _thirtySecondsTimeController.DisplayData = data =>
                    {

                    };
                }
            }
        }

        public string Title { get; set; }

        public Venue Venue { get; set; }

        public RaceViewModel Race
        {
            get => _race;
            set => SetProperty(ref _race, value, "Race");
        }

        private class RefereeRequestProcess : IRequestProcess
        {
            private readonly Action<RaceViewModel> _action;

            public RefereeRequestProcess(Action<RaceViewModel> action)
            {
                _action = action;
            }

            public void Process(TcpClient client, NetworkStream stream, byte[] bytesReceived)
            {
                try
                {
                    var message = Encoding.Unicode.GetString(bytesReceived, 0, bytesReceived.Length);
                    var command = JsonConvert.DeserializeObject<Command>(message);
                    if (command != null)
                    {
                        var id = int.Parse(command.Value);
                        var teamMgr = new TeamMgr();
                        var s = new ScheduleMgr().GetItem(id);
                        var teamA = teamMgr.GetItem(s.TeamA);
                        var teamB = teamMgr.GetItem(s.TeamB);
                        var race = new RaceViewModel
                        {
                            TeamA = new TeamData { TeamName = teamA.ShortName },
                            TeamB = new TeamData { TeamName = teamB.ShortName }
                        };
                        _action?.Invoke(race);
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("error " + e.Message);
                }
                var bytesSent = Encoding.Unicode.GetBytes("I've got it");
                stream.Write(bytesSent, 0, bytesSent.Length);
            }
        }
    }
}