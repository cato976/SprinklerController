namespace SprinklerService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SprinklerBO;
    using System;

    public class SprinklerController : Controller
    {
        public Schedule GetCurrentStatus()
        {
            Sprinkler sprinkler = new Sprinkler();
            var schedule = Sprinkler.GetSchedule();
            return schedule;
        }

        [HttpPost]
        public bool AddToSchedule(DateTime startDateTime)
        {
            return Sprinkler.AddToSchedule(startDateTime);
        }

        [HttpDelete]
        public bool DeleteSchedule(DateTime startDateTime)
        {
            return Sprinkler.DeleteFromSchedule(startDateTime);
        }
        public void StartIrrigationSystem()
        {
            Sprinkler.StartIrrigationSystem();
        }
    }
}
