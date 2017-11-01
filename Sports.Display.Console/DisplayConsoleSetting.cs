using System;
using System.IO;
using Newtonsoft.Json;

namespace Sports.Display.Console
{
    public class DisplayConsoleSetting
    {
        public int Top { get; set; }

        public int Left { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int ListeningPort { get; set; }

        private static string CheckAndCreateFolder()
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return Path.Combine(folder, "Setting.config");
        }

        public static DisplayConsoleSetting Load()
        {
            var fileName = CheckAndCreateFolder();
            if (!File.Exists(fileName))
                return new DisplayConsoleSetting();
            return JsonConvert.DeserializeObject<DisplayConsoleSetting>(File.ReadAllText(fileName));
        }

        public void Save()
        {
            var fileName = CheckAndCreateFolder();
            File.WriteAllText(fileName, JsonConvert.SerializeObject(this));
        }
    }
}