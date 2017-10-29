using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sports.Common;

namespace Sports.ConfigurationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var configFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration.config");
            var config = new RefereeConfiguration {VenueId = 1};
            var str = JsonConvert.SerializeObject(config);
            File.WriteAllText(configFileName, str);
        }
    }
}
