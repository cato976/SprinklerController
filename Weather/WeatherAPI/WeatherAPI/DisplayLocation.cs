﻿namespace WeatherAPI
{
    using Newtonsoft.Json;
    public class DisplayLocation
    {
        [JsonProperty(PropertyName = "full")]
        public string Full { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string state_name { get; set; }
        public string country { get; set; }
        public string country_iso3166 { get; set; }
        public string zip { get; set; }
        public string magic { get; set; }
        public string wmo { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string elevation { get; set; }
    }
}
