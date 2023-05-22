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
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            acisdoc.Read(@"..\..\..\Docs\rh.sat");
        }
    }
}
