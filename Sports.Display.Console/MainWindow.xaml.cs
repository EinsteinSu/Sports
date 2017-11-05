using System;
using System.Net.Sockets;
using System.Text;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using Sports.Common;
using Sports.Common.WaterPolo;
using Sports.Timing;
using Sports.Timing.Interfaces;
using Sports.Wpf.Common.ViewModel.WaterPolo;

namespace Sports.Display.Console
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private static readonly ILog Log = LogManager.GetLogger("MainWindow");

        public MainWindow()
        {
            XmlConfigurator.Configure();
            var displayConsole = new DisplayConsole
            {
                Settings = DisplayConsoleSetting.Load(),
                Race = GetTestData() //new RaceDisplayViewModel()
            };

            InitializeComponent();

            Top = displayConsole.Settings.Top;
            Left = displayConsole.Settings.Left;
            Height = displayConsole.Settings.Height;
            Width = displayConsole.Settings.Width;

            var controller = new SocketController(512);
            controller.StartListening(new WaterPoloRequestProcess(r => { displayConsole.Race = r; }),
                displayConsole.Settings.ListeningPort);

            DataContext = displayConsole;
        }

        private RaceDisplayViewModel GetTestData()
        {
            var display = new RaceDisplayViewModel
            {
                TotalTime = "08:00",
                IsTimeout = false,
                Court = 1,
                TeamA = new TeamDisplayViewModel
                {
                    TeamName = "China(CHN)",
                    Score = 1,
                    FlagUrl = "Assets/CHN.jpg",
                    PauseTime = "01:10",
                    Players = new PlayerData[13]
                },
                TeamB = new TeamDisplayViewModel
                {
                    TeamName = "Japan(JPN)",
                    FlagUrl = "Assets/JPN.jpg",
                    Score = 0,
                    Players = new PlayerData[13]
                }
            };
            for (var i = 0; i < 13; i++)
                if (i == 3)
                {
                    display.TeamA.Players[i] =
                        new PlayerDisplayViewModel { Name = $"Team A {i + 1}", Fouls = 2, FoulTime = 15 };
                    display.TeamB.Players[i] =
                        new PlayerDisplayViewModel { Name = $"Team B {i + 1}", Fouls = 3, FoulTime = 20 };
                }
                else
                {
                    display.TeamA.Players[i] = new PlayerDisplayViewModel { Name = $"Team A {i + 1}" };
                    display.TeamB.Players[i] = new PlayerDisplayViewModel { Name = $"Team B {i + 1}" };
                }
            return display;
        }

        internal class WaterPoloRequestProcess : IRequestProcess
        {
            private readonly Action<RaceDisplayViewModel> _race;

            public WaterPoloRequestProcess(Action<RaceDisplayViewModel> race)
            {
                _race = race;
            }

            public void Process(TcpClient client, NetworkStream stream, byte[] bytesReceived)
            {
                var command = new Command { Type = CommandType.Response, ValueType = typeof(ResponseType) };
                try
                {
                    var message = Encoding.Unicode.GetString(bytesReceived, 0, bytesReceived.Length);
                    Log.Debug($"Got message {message}");
                    var data = JsonConvert.DeserializeObject<RaceDisplayViewModel>(message);
                    _race?.Invoke(data);
                    command.Value = ResponseType.Success.ToString();
                }
                catch (Exception e)
                {
                    command.Value = ResponseType.Faild.ToString();
                    Log.Error($"Could not process message , cause {e.Message}");
                }

                var bytesSent = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(command));
                stream.Write(bytesSent, 0, bytesSent.Length);
            }
        }
    }
}