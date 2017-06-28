namespace CalendarAPITest
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using CalendarAPI;

    [TestClass]
    public class GoogleTest
    {
        [TestMethod]
        public void TestInsert()
        {
            DateTime startDateTime = DateTime.Now.AddMinutes(5);
            var google = new GoogleAPI();
            var evnt = google.InsertEvent(startDateTime);
            Assert.AreEqual(evnt, true);
        }
    }
}
