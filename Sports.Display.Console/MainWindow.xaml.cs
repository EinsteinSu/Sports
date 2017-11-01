using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Sports.Common.WaterPolo;
using Sports.Timing;
using Sports.Timing.Interfaces;
using Sports.Wpf.Common.ViewModel.WaterPolo;

namespace Sports.Display.Console
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var displayConsole = new DisplayConsole();
            //todo: load settings from json file

            var controller = new SocketController(512);
            controller.StartListening(new WaterPoloRequestProcess((r) =>
            {
                //todo: set the race model from request
            }), 1234);
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
            for (int i = 0; i < 13; i++)
            {
                if (i == 3)
                {
                    display.TeamA.Players[i] = new PlayerDisplayViewModel { Name = $"Team A {i + 1}", Fouls = 2, FoulTime = 15 };
                    display.TeamB.Players[i] = new PlayerDisplayViewModel { Name = $"Team B {i + 1}", Fouls = 3, FoulTime = 20 };
                }
                else
                {
                    display.TeamA.Players[i] = new PlayerDisplayViewModel { Name = $"Team A {i + 1}" };
                    display.TeamB.Players[i] = new PlayerDisplayViewModel { Name = $"Team B {i + 1}" };
                }

            }
            DataContext = new RaceDisplayViewModel();
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
                try
                {
                    var message = Encoding.Unicode.GetString(bytesReceived, 0, bytesReceived.Length);
                    var data = JsonConvert.DeserializeObject<RaceDisplayViewModel>(message);
                    _race?.Invoke(data);
                }
                catch (Exception e)
                {
                    //todo: replay to error
                    System.Console.WriteLine("error");
                }

                var bytesSent = Encoding.Unicode.GetBytes("I'v got it");
                stream.Write(bytesSent, 0, bytesSent.Length);
            }
        }
    }
}
