namespace SprinklerService
{
    using Microsoft.AspNetCore.Hosting;
    using System.ServiceProcess;

    public static class WebHostServiceExtensions
    {
        public static void RunAsCustomService(this IWebHost host)
        {
            var webHostService = new SprinklerService(host);
            ServiceBase.Run(webHostService);
        }
    }
}
