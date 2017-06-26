namespace WeatherAPITest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WeatherAPI;

    [TestClass]
    public class UnitTestWeatherUnderground
    {
        [TestMethod]
        public void TestGetConditions()
        {
            WeatherUnderground wu = new WeatherUnderground();
            var currentCondition = wu.GetConditions("Sunrise", "FL");
            Assert.AreEqual("Sunrise, FL", currentCondition.DisplayLocation.Full);
        }
    }
}
