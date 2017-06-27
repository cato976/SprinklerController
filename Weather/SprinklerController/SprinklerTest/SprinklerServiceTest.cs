namespace SprinklerTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SprinklerService.Controllers;

    [TestClass]
    public class SprinklerServiceTest
    {
        [TestMethod]
        public void TestGetSchedule()
        {
            var controller = new SprinklerController();
            var status = controller.GetCurrentStatus();
            Assert.IsNotNull(status);
        }
    }
}
