namespace CalendarAPITest
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using CalendarAPI;
    using Google.Apis.Calendar.v3.Data;

    [TestClass]
    public class GoogleTest
    {
        [TestMethod]
        public void TestInsert()
        {
            var google = new GoogleAPI();
            var evnt = InsertXMinFromNow(5);
            Assert.AreEqual(string.IsNullOrEmpty(evnt.HtmlLink), false);
            Assert.AreEqual(true, google.DeleteEvent((DateTime)evnt.Start.DateTime));
        }

        [TestMethod]
        public void TestGet()
        {
            var google = new GoogleAPI();
            var events = google.FindIrrigationEvents();
        }

        [TestMethod]
        public void TestGetByDateTime()
        {
            var google = new GoogleAPI();
            var evnt = InsertXMinFromNow(5);
            var events = google.FindIrrigationEvents((DateTime)evnt.Start.DateTime);
            Assert.AreEqual(evnt.Start.DateTime, events.Items.FirstOrDefault().Start.DateTime);
            google.DeleteEvent((DateTime)evnt.Start.DateTime);
        }

        private static Event InsertXMinFromNow(int minutes)
        {
            DateTime startDateTime = DateTime.Now.AddMinutes(minutes);
            var google = new GoogleAPI();
            var evnt = google.InsertEvent(startDateTime);
            return evnt;
        }
    }
}
