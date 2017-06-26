using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SprinklerService
{
    public partial class SprinklerService : ServiceBase
    {
        private EventLog eventLog;

        public SprinklerService()
        {
            InitializeComponent();
            eventLog = new EventLog();
            if(!EventLog.SourceExists("SprinklerSource"))
            {
                EventLog.CreateEventSource("SprinklerSource", "SprinklerLog");
            }
            eventLog.Source = "SprinklerSourve";
            eventLog.Log = "SprinklerLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("Sprinkler Service Starting");
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Sprinkler Service Stopping");
        }
    }
}
