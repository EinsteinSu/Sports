using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sports.DataAccess;

namespace Sports.Business.Test
{
    [TestClass]
    public class TestBase
    {
        protected readonly SportDataContext Context = new SportDataContext();

        [TestCleanup]
        public void Clean()
        {
            Context.Dispose();
        }
    }
}
