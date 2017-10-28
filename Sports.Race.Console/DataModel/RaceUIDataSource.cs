using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Sports.Business;
using Sports.Race.Console.View;
using Sports.Race.Console.ViewModel;
using Sports.Timing;
using Sports.Timing.Interfaces;
using Sports.Wpf.Common.DataModel;

namespace Sports.Race.Console.DataModel
{
    public class RaceUIDataSource : IDisposable
    {
        public static SocketController Controller;
        private readonly ObservableCollection<UIDataItem> _items;

        public RaceUIDataSource(IVenueMgr venueMgr, ICentralMgr centralMgr)
        {
            var items = centralMgr.GetItems().ToList();
            if (items.Any() && Controller == null)
            {
                Controller = new SocketController(1024);
                Controller.StartListening(new RaceDataProcess(), items.First().Port);
            }

            _items = new ObservableCollection<UIDataItem>();
            foreach (var venue in venueMgr.GetItems())
            {
                var context = new RaceMgmtViewModel(venueMgr, venue.Id);
                _items.Add(new UIDataItem(venue.Id, venue.Name,
                    venue.ToString(),
                    "Assets/LightGray.png",
                    "race load",
                    new RaceMgmtView {DataContext = context}));
            }
        }


        public static RaceUIDataSource Instance { get; } = new RaceUIDataSource(new VenueMgr(), new CentralMgr());

        public static ObservableCollection<UIDataItem> Items => Instance._items;

        public void Dispose()
        {
            Controller?.StopListening();
        }
    }

    public class RaceDataProcess : IRequestProcess
    {
        public void Process(TcpClient client, NetworkStream stream, byte[] bytesReceived)
        {
            var response = "I got it";
            try
            {
                var message = Encoding.Unicode.GetString(bytesReceived, 0, bytesReceived.Length);
                response = message;
                System.Console.WriteLine(message);
            }
            catch (Exception e)
            {
                response = "error";
            }

            var bytesSent = Encoding.Unicode.GetBytes(response);
            stream.Write(bytesSent, 0, bytesSent.Length);
        }
    }
}