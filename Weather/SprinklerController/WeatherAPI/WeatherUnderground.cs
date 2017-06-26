namespace WeatherAPI
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.IO;
    using System.Net;

    public class WeatherUnderground
    {
        public CurrentObservation GetConditions(string city, string state)
        {
            CurrentObservation currentObservation = null;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.wunderground.com/api/a1f4d283d1ffab72/conditions/q/Florida/Sunrise.json");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "GET";
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponseAsync().Result;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                JObject json = JObject.Parse(responseText);
                currentObservation = JsonConvert.DeserializeObject<CurrentObservation>(json["current_observation"].ToString());
                return currentObservation;
            }
        }
    }
}
