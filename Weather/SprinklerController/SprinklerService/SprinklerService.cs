namespace SprinklerService
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.WindowsServices;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;
    public partial class SprinklerService : WebHostService
    {
        private EventLog eventLog;

        //public SprinklerService(string[] args)
        //{
        //    InitializeComponent();
        //    eventLog = new EventLog();
        //    if(!EventLog.SourceExists("SprinklerSource"))
        //    {
        //        EventLog.CreateEventSource("SprinklerSource", "SprinklerLog");
        //    }
        //    eventLog.Source = "SprinklerSourve";
        //    eventLog.Log = "SprinklerLog";
        //}
        public SprinklerService(IWebHost host) : base(host)
        {
            //InitializeComponent();
            //string eventSourceName = "SprinklerSource";
            //string logName = "SprinklerLog";
            //if (args.Count() > 0) { eventSourceName = args[0]; }
            //if (args.Count() > 1) { logName = args[1]; }

            //eventLog = new System.Diagnostics.EventLog();
            //if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
            //{
            //    System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
            //}
            //eventLog.Source = eventSourceName; eventLog.Log = logName;
        }

        protected override void OnStarting(string[] args)
        {
            eventLog.WriteEntry("Sprinkler Service Starting");
            base.OnStarting(args);
        }

        protected override void OnStopping()
        {
            eventLog.WriteEntry("Sprinkler Service Stopping");
            base.OnStopping();
        }
    }
}
