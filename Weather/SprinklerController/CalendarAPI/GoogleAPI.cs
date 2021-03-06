﻿namespace CalendarAPI
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using Google.Apis.Services;
    using Google.Apis.Util.Store;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;

    public class GoogleAPI
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Solkinetics Irrigation";

        public Event InsertEvent(DateTime StartDateTime)
        {
            CalendarService service = GetCredentials();

            // Define parameters of request.
            Event evnt = new Event
            {
                Summary = "Start Irrigation",
                Description = "Start Irrigation",
                Start = new EventDateTime { DateTime = StartDateTime },
                End = new EventDateTime { DateTime = StartDateTime.AddMinutes(30) }
            };
            EventsResource.InsertRequest request = service.Events.Insert(evnt, "primary");

            Event createdEvent = request.Execute();
            if (!string.IsNullOrEmpty(createdEvent.HtmlLink))
            {
                return createdEvent;
            }
            else
            {
                return createdEvent;
            }
        }

        public bool DeleteEvent(DateTime StartDateTime)
        {
            CalendarService service = GetCredentials();
            var evnts = FindIrrigationEvents(StartDateTime);

            EventsResource.DeleteRequest request = service.Events.Delete("primary", evnts.Items.FirstOrDefault().Id);
            request.Execute();

            return true;
        }

        public Events FindIrrigationEvents(DateTime StartDateTime = new DateTime())
        {
            CalendarService service = GetCredentials();
            Events irrigationEvents = new Events();

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            //Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if ((StartDateTime == DateTime.MinValue) && !String.IsNullOrEmpty(when) && eventItem.Summary.Contains("Start Irrigation"))
                    {
                        when = AddEvent(irrigationEvents, eventItem);
                    }
                    else if((StartDateTime.Date == eventItem.Start.DateTime.Value.Date) && (StartDateTime.Hour == eventItem.Start.DateTime.Value.Hour) && (StartDateTime.Minute == eventItem.Start.DateTime.Value.Minute) && !String.IsNullOrEmpty(when) && eventItem.Summary.Contains("Start Irrigation"))
                    {
                        when = AddEvent(irrigationEvents, eventItem);
                    }
                    //Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }

                return irrigationEvents;
            }
            else
            {
                //Console.WriteLine("No upcoming events found.");
                return irrigationEvents;
            }
        }

        private static string AddEvent(Events irrigationEvents, Event eventItem)
        {
            string when = eventItem.Start.Date;
            if (irrigationEvents.Items == null)
            {
                irrigationEvents.Items = new List<Event>();
            }
            irrigationEvents.Items.Add(eventItem);
            return when;
        }

        private static CalendarService GetCredentials()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetEnvironmentVariable("HOMEPATH");
                //string credPath = Environment.GetFolderPath(
                //    Environment.SpecialFolder.Personal);
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }
    }
}
