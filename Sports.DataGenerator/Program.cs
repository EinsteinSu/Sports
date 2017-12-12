using Sports.Business;
using Sports.DataAccess;
using Sports.DataAccess.Models;

namespace Sports.DataGenerator
{
    internal class Program
    {
        private static readonly SportDataContext Context = new SportDataContext();
        private static readonly string _teamAShortName = "CNH";
        private static readonly string _teamAName = "China";
        private static readonly string _teamBShortName = "USA";
        private static readonly string _teamBName = "American";
        private static readonly string _venueName = "Venue";
        private static readonly string _venueIp = ":::1";
        private static readonly int _venuePort = 1234;
        private static readonly string _displayName = "Display";
        private static readonly string _displayIp = ":::1";
        private static readonly int _displayPort = 2345;
        private static readonly string _centralIp = ":::1";
        private static readonly int _centeralPort = 3456;
        
        private static void Main(string[] args)
        {
            var id = CreateTeam(_teamAShortName, _teamAName);
            CreatePlayers(id,_teamAShortName);
            id = CreateTeam(_teamBShortName, _teamBName);
            CreatePlayers(id, _teamBShortName);
            CreateCentral();
            id = CreateVenue();
            CreateHardware(id,3,"Total");
            CreateHardware(id, 4,"30");
            CreateDisplay(id);
            
        }

        private static void CreateCentral()
        {
            var central = new Central
            {
                IPAddress = _centralIp,
                Port = _centeralPort
            };
            new CentralMgr().Add(central);
        }

        private static void CreateHardware(int venueId, int comNumber,string usage)
        {
            var hardware = new Hardware
            {
                Port = comNumber,
                VenueId = venueId,
                Usage = usage
            };
            new HardwareMgr().Add(hardware);
        }

        private static int CreateVenue()
        {
            var venue = new Venue
            {
                Name = _venueName,
                IPAddress = _venueIp,
                Port = _venuePort
            };
            var mgr = new VenueMgr();
            mgr.Add(venue);
            return venue.Id;
        }

        private static void CreateDisplay(int venueId)
        {
            var display = new Display
            {
                Name = _displayName,
                IPAddress = _displayIp,
                Port = _displayPort,
                VenueId = venueId
            };
            new DisplayMgr().Add(display);
        }

        private static int CreateTeam(string shortName, string name)
        {
            var team = new Team();
            team.Code = team.ShortName = shortName;
            team.Name = name;
            var mgr = new TeamMgr();
            mgr.Add(team);
            return team.Id;
        }

        private static void CreatePlayers(int teamId, string name)
        {
            var mgr = new PlayerMgr();
            for (var i = 0; i < 20; i++)
            {
                var p = new Player
                {
                    Name = $"Player {i + 1} of Team {name}",
                    Gender = Gender.Male,
                    TeamId = teamId
                };
                mgr.Add(p);
            }
        }
    }
}