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

namespace Sports.Referee.Console.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private RaceViewModel _race;

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
            controller.StartListening(new RefereeRequestProcess((r) => Race = r), Venue.Port);
            Race = new RaceViewModel();
            //todo: listen and process hardware stuffs
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