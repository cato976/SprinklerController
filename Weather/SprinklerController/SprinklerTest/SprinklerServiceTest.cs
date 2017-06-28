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
        public void TestStartIrrigationSystem()
        {
            TaskScheduler scheduler = new SynchronousTaskScheduler();
            Task.Factory.StartNew(() =>
            {
                var controller = new SprinklerController();
                controller.StartIrrigationSystem();
                var status = controller.GetCurrentStatus();
                int zoneMinutes = 0;
                status.Zones.ForEach(zone =>
                {
                    zoneMinutes += zone.WateringTime;
                });
                zoneMinutes += 30; //Padding

                DateTime sleepTill = status.StartDateTime;
                if (sleepTill > DateTime.Now)
                {
                    Thread.Sleep(new TimeSpan(sleepTill.AddSeconds(zoneMinutes).Ticks - sleepTill.Ticks));
                }
            },
            CancellationToken.None,
            TaskCreationOptions.None,
            scheduler);
        }
    }
}
