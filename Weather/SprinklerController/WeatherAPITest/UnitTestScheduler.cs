namespace WeatherAPITest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SprinklerBO;
    using System.Threading;
    using System.Threading.Tasks;
    using TestHelper;

    [TestClass]
    public class UnitTestScheduler
    {
        [TestMethod]
        public void TestScheduler()
        {
            TaskScheduler scheduler = new SynchronousTaskScheduler();
            Task.Factory.StartNew(() =>
            {
                Sprinkler sprinkler = new Sprinkler();
            },
            CancellationToken.None,
            TaskCreationOptions.None,
            scheduler);
        }
    }
}
