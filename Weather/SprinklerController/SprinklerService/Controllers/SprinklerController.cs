namespace SprinklerService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SprinklerBO;

    public class SprinklerController : Controller
    {
        public Schedule GetCurrentStatus()
        {
            Sprinkler sprinkler = new Sprinkler();
            var schedule = Sprinkler.GetSchedule();
            return schedule;
        }
    }
}
