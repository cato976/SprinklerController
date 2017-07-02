namespace SprinklerBO
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using CalendarAPI;
    using WeatherAPI;

    /*
     * At a certain time of a certain day start watering the zones, one zone at a time
     * until all zones have been water for the predefined amount of time
     */
    public class Sprinkler
    {
        static Schedule sprinklerSchedule = new Schedule();

        public Sprinkler()
        {
            SetupSchedule();
        }

        private static void SetupSchedule()
        {
            //sprinklerSchedule.StartDateTime = DateTime.Now.AddSeconds(10);

            sprinklerSchedule.City = "Sunrise";
            sprinklerSchedule.State = "Florida";
            var zone = new Zone{
                Name = "Front Lawn",
                Number = 3,
                WateringTime = 15
            };
            if (!sprinklerSchedule.Zones.Contains(zone))
            {
                sprinklerSchedule.Zones.Add(zone);
            }
            zone = new Zone{
                Name = "Back Lawn",
                Number = 1,
                WateringTime = 15
            };
            if (!sprinklerSchedule.Zones.Contains(zone))
            {
                sprinklerSchedule.Zones.Add(zone);
            }
            zone = new Zone{
                Name = "Side Lawn",
                Number = 2,
                WateringTime = 20
            };
            if (!sprinklerSchedule.Zones.Contains(zone))
            {
                sprinklerSchedule.Zones.Add(zone);
            }
            zone = new Zone{
                Name = "Front Flower Bed",
                Number = 4,
                WateringTime = 30
            };
            if (!sprinklerSchedule.Zones.Contains(zone))
            {
                sprinklerSchedule.Zones.Add(zone);
            }

            //StartSchedule();
        }

        private static void StartSchedule()
        {
            SleepToTarget Temp = new SleepToTarget(sprinklerSchedule.StartDateTime, StartZones);
            Temp.Start();
        }

        static void StartZones()
        {
            Debug.WriteLine("Check weather conditions");
            WeatherUnderground wu = new WeatherUnderground();
            var currentCondition = wu.GetConditions(sprinklerSchedule.City, sprinklerSchedule.State);
            if (currentCondition.Weather.ToLower().Contains("rain") || currentCondition.Weather.ToLower().Contains("snow"))
            {
                Debug.WriteLine("Its currently raining/snowing.  Skipping this schedule");
                return;
            }
            else if (currentCondition.Wind_MPH > 15)
            {
                Debug.WriteLine(string.Format("The current wind speed is {0} MPH. Skipping this schedule", currentCondition.Wind_MPH));
                return;
            }

            Debug.WriteLine("Starting Zones");

            sprinklerSchedule.Zones.ForEach(zone =>
            {
                Debug.WriteLine(string.Format("Watering Zone:{0} for {1} minutes", zone.Name, zone.WateringTime));
                Console.WriteLine(string.Format("Watering Zone:{0} for {1} minutes", zone.Name, zone.WateringTime));
                Debug.WriteLine(string.Format("Energizing solenoid {0}", zone.Number));
                zone.Status = StatusType.On;

                DateTime Now = DateTime.Now;
                DateTime TargetTime = Now.AddSeconds(zone.WateringTime);
                while (Now < TargetTime)
                {
                    int SleepMilliseconds = (int)Math.Round((TargetTime - Now).TotalMilliseconds / 2);
                    Debug.WriteLine(DateTime.Now.ToString() + ": " + SleepMilliseconds * 2 + " Milliseconds till watering is done");
                    Console.WriteLine(DateTime.Now.ToString() + ": " + SleepMilliseconds * 2 + " Milliseconds till watering is done");
                    Thread.Sleep(SleepMilliseconds > 250 ? SleepMilliseconds : 250);
                    Now = DateTime.Now;
                }

                Debug.WriteLine(string.Format("Done watering Zone:{0}", zone.Name));
                Console.WriteLine(string.Format("Done watering Zone:{0}", zone.Name));
                Debug.WriteLine(string.Format("De-Energizing solenoid {0}", zone.Number));
                zone.Status = StatusType.Off;
            });

            Debug.WriteLine("Done Watering Zones");
        }

        public static Schedule GetSchedule()
        {
            GoogleAPI google = new GoogleAPI();
            var events = google.FindIrrigationEvents();
            if (events.Items != null)
            {
                var evnt = events.Items.FirstOrDefault();
                sprinklerSchedule.StartDateTime = (DateTime)evnt.Start.DateTime;
            }
            SetupSchedule();
            return sprinklerSchedule;
        }

        public static bool AddToSchedule(DateTime startDateTime)
        {
            GoogleAPI google = new GoogleAPI();

            var ent = google.InsertEvent(startDateTime);
            if (!string.IsNullOrEmpty(ent.HtmlLink))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DeleteFromSchedule(DateTime startDateTime)
        {
            GoogleAPI google = new GoogleAPI();
            return google.DeleteEvent(startDateTime);
        }

        public static void StartIrrigationSystem()
        {
            GetSchedule();
            if (sprinklerSchedule.StartDateTime > DateTime.Now)
            {
                StartSchedule();
            }
        }
    }
}
