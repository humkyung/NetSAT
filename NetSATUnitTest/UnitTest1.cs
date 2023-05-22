using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NetSATUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var acisdoc = new NetSAT.AcisDoc();
            acisdoc.Read(@"g:\Projects\NetSAT\Docs\rh.sat");
        }
    }
}
