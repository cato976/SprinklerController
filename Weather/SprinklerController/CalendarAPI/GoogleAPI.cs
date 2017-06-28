namespace CalendarAPI
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Calendar.v3;
    using Google.Apis.Calendar.v3.Data;
    using Google.Apis.Services;
    using Google.Apis.Util.Store;
    using System;
    using System.IO;
    using System.Threading;

    public class GoogleAPI
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Solkinetics Irrigation";

        public bool InsertEvent(DateTime StartDateTime)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal);
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
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
