using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public Dictionary<string, string> WeatherAdvice = new Dictionary<string, string>()
        {
            {"sunny", "Make sure to wear sunblock!" },
            {"rain", "Make sure to pack your rain gear and wear waterproof shoes!" },
            {"thunderstorms", "Seek shelter immediately and avoid hiking on exposed ridges!" },
            {"snow", "Make sure to pack your snowshoes!" }
        };

        public Dictionary<string, string> TempWarnings = new Dictionary<string, string>()
        {
            {"high", "Please bring an extra gallon of water." },
            {"drop", "Make sure to wear breathable layers." },
            {"low", "Be aware of the danger of exposurer in frigid temperatures." }
        };

        public double ConvertTemp(string measurement, int temp)
        {
            double result = 0;

            if (measurement == "F")
            {
                result = (temp * 1.8) + 32;
            }
            else
            {
                result = (temp - 32) / 1.8;
            }

            return result;
        }


        public string GiveWarning(int high, int low)
        {
            string warning = " ";
            if (high - low > 20)
            {
                warning = TempWarnings["drop"];
            }
            else if (high > 75)
            {
                warning = TempWarnings["high"];
            }
            else if (low < 20)
            {
                warning = TempWarnings["low"];
            }
            return warning;
        }


        public string GiveAdvice(string forecast)
        {
            string advice = null;
            if (forecast == "sunny")
            {
                advice = WeatherAdvice["sunny"];
            }
            else if (forecast == "rain")
            {
                advice = WeatherAdvice["rain"];
            }
            else if (forecast == "thunderstorms")
            {
                advice = WeatherAdvice["thunderstorms"];
            }
            else if (forecast == "snow")
            {
                advice = WeatherAdvice["snow"];
            }
            return advice;
        }


        public string DisplayWeatherIcon(string forecast)
        {
            if (forecast == "partly cloudy")
            {
                return "partlyCloudy";
            }
            else
            {
                return forecast;
            }

        }

       

    }
}