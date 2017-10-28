using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sports.Common.Test
{
    [TestClass]
    public class CommandTest
    {
        const string data = "{\"Type\":0,\"Value\":\"1\",\"ValueType\":\"System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"}";
        [TestMethod]
        public void ToStringTest()
        {
            var cmd = new Command
            {
                Type = CommandType.LoadRace,
                Value = "1",
                ValueType = typeof(int)
            };
            var str = cmd.ToString();
           Assert.AreEqual(data,str);
        }

        [TestMethod]
        public void FromStringTest()
        {
            var cmd = new Command(data);
            Assert.AreEqual(cmd.Type, CommandType.LoadRace);
            Assert.AreEqual(cmd.Value, "1");
            Assert.AreEqual(cmd.ValueType, typeof(int));
        }
    }
}
