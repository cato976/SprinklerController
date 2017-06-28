namespace SprinklerService
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.WindowsServices;
    using Microsoft.Net.Http.Server;
    using SprinklerBO;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    class Program
    {
        static Schedule sprinklerSchedule = new Schedule();

        static void Main(string[] args)
        {
            bool isService = true;
            if(Debugger.IsAttached || args.Contains("--console"))
            {
                isService = false;
            }
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            if(isService)
            {
                pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathToContentRoot = Path.GetDirectoryName(pathToExe);
            }

            var host = new WebHostBuilder()
                .UseWebListener(options =>
                {
                    options.ListenerSettings.Authentication.Schemes = AuthenticationSchemes.None;
                    options.ListenerSettings.Authentication.AllowAnonymous = true;
                })
                .UseContentRoot(pathToContentRoot)
                //.UseIISIntrgration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            if(isService)
            {
                host.RunAsCustomService();
            }
            else
            {
                host.Run();
            }
        }
    }
}