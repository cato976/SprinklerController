namespace SprinklerService
{
    using Microsoft.AspNetCore.Hosting;
    public static class WebHostServiceExtensions
    {
        public static void RunAsCustomService(this IWebHost host)
        {
            var webHostService = new SprinklerService(host);
        }
    }
}
