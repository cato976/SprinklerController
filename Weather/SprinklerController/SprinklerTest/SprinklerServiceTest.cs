namespace SprinklerTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SprinklerService.Controllers;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using TestHelper;

    [TestClass]
    public class SprinklerServiceTest
    {
        [TestMethod]
        public void TestGetSchedule()
        {
            var controller = new SprinklerController();
            var status = controller.GetCurrentStatus();
            Assert.IsNotNull(status);
        }

        [TestMethod]
        public void TestAddSchedule()
        {
            DateTime startTime = DateTime.Now.AddMinutes(5);
            var controller = new SprinklerController();
            var status = controller.AddToSchedule(startTime);
            Assert.AreEqual(true, status);
            Assert.AreEqual(true, controller.DeleteSchedule(startTime));
        }

        [TestMethod]
        public void TestStartIrrigationSystem()
        {
            TaskScheduler scheduler = new SynchronousTaskScheduler();
            var controller = new SprinklerController();
            DateTime startTime = DateTime.Now.AddMinutes(1);
            controller.AddToSchedule(startTime);

            Task.Factory.StartNew(() =>
            {
                controller.StartIrrigationSystem();
                var status = controller.GetCurrentStatus();
                int zoneMinutes = 0;
                status.Zones.ForEach(zone =>
                {
                    zoneMinutes += zone.WateringTime;
                });
                zoneMinutes += 10; //Padding

                DateTime sleepTill = status.StartDateTime;
                if (sleepTill > DateTime.Now)
                {
                    Thread.Sleep(new TimeSpan(sleepTill.Ticks - DateTime.Now.Ticks));  // Sleep till it time to start
                    Thread.Sleep(new TimeSpan((sleepTill.AddSeconds(zoneMinutes).Ticks) - (sleepTill.Ticks))); // Sleep for the watering time
                }
            },
            CancellationToken.None,
            TaskCreationOptions.None,
            scheduler);
            controller.DeleteSchedule(startTime);
        }
    }
}
