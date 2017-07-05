namespace SolKineticsIrrigation.Web.ViewModels
{
    using SprinklerBO;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;

    public class ScheduleView : Schedule
    {
        [DisplayName("Next Watering")]
        public string NextWatering
        {
            get
            {
                return base.StartDateTime.ToString();
            }
            private set { }
        }

    }
}
