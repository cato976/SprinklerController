namespace WeatherAPITest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SprinklerBO;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    public class UnitTestScheduler
    {
        [TestMethod]
        public void TestScheduler()
        {
            TaskScheduler scheduler = new SynchronousTaslScheduler();
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
