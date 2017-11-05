using System;
using Newtonsoft.Json;

namespace Sports.Common
{
    public class Command
    {
        public Command() { }

        public Command(string value)
        {
            var cmd = JsonConvert.DeserializeObject<Command>(value);
            Type = cmd.Type;
            Value = cmd.Value;
            ValueType = cmd.ValueType;
        }

        public CommandType Type { get; set; }

        public string Value { get; set; }

        public Type ValueType { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public enum CommandType
    {
        LoadRace, Response
    }

    public enum ResponseType
    {
        Success, Faild
    }
}