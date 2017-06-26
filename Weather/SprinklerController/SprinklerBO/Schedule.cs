namespace SprinklerBO
{
    using System;
    using System.Collections.Generic;

    public class Schedule
    {
        public Schedule()
        {
            Zones = new List<Zone>();
        }

        public DateTime StartDateTime { get; set; }
        public List<Zone> Zones { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
