using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sports.Wpf.Common.ViewModel.WaterPolo;

namespace Sports.Wpf.Common.Test
{
    [TestClass]
    public class PlayerDisplayViewModelTest
    {
        [TestMethod]
        public void NoFoul()
        {
            var player = new PlayerDisplayViewModel { Fouls = 0 };
            Assert.IsTrue(string.IsNullOrEmpty(player.Foul1));
            Assert.IsTrue(string.IsNullOrEmpty(player.Foul2));
            Assert.IsTrue(string.IsNullOrEmpty(player.Foul3));
        }

        [TestMethod]
        public void Foul1()
        {
            var player = new PlayerDisplayViewModel { Fouls = 1 };
            Assert.IsTrue(player.Foul1.Equals(PlayerDisplayViewModel.Cycle));
            Assert.IsTrue(string.IsNullOrEmpty(player.Foul2));
            Assert.IsTrue(string.IsNullOrEmpty(player.Foul3));
        }

        [TestMethod]
        public void Foul2()
        {
            var player = new PlayerDisplayViewModel { Fouls = 2 };
            Assert.IsTrue(player.Foul1.Equals(PlayerDisplayViewModel.Cycle));
            Assert.IsTrue(player.Foul2.Equals(PlayerDisplayViewModel.Cycle));
            Assert.IsTrue(string.IsNullOrEmpty(player.Foul3));
        }

        [TestMethod]
        public void Foul3()
        {
            var player = new PlayerDisplayViewModel { Fouls = 3 };
            Assert.IsTrue(player.Foul1.Equals(PlayerDisplayViewModel.Cycle));
            Assert.IsTrue(player.Foul2.Equals(PlayerDisplayViewModel.Cycle));
            Assert.IsTrue(player.Foul3.Equals(PlayerDisplayViewModel.Cycle));
        }
    }
}
