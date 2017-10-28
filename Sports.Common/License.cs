using System;
using System.IO;
using Newtonsoft.Json;

namespace Sports.Common
{
    public interface ILicense
    {
        bool Validate();
    }

    public class License : ILicense
    {
        private readonly string _fileName;

        public License(string name)
        {
            _fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name);
        }

        public bool Validate()
        {
            if (!File.Exists(_fileName))
                return false;
            var data = File.ReadAllText(_fileName);
            if (string.IsNullOrEmpty(data))
                return false;
            var license = JsonConvert.DeserializeObject<LicenseData>(data);
            if (license.Signature.Equals("Sue Su") && license.Date > DateTime.Now)
                return true;
            return false;
        }
    }

    public class LicenseData
    {
        public string Signature { get; set; }

        public DateTime Date { get; set; }
    }
}