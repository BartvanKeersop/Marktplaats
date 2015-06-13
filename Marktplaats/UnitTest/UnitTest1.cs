using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class TestGebruiker
        {
            private Gebruiker gebruiker = new Gebruiker(1, "Jaap", 1);
            private Advertentie advertentie = new Advertentie(1, "titel");
            gebruiker.AangeradenAdvertenties.Add(advertentie);

         [TestMethod]
         public void CheckBestaatAlCheck()
         {
            Assert.AreEqual((gebruiker.BestaatAlCheck(gebruiker, advertentie)), true);
         }

       }
    }
}
