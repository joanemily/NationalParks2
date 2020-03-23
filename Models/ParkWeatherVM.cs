using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class ParkWeatherVM
    {
        public string ParkCode { get; set; }
        public Park Park { get; set; }
        public string TempType { get; set; }

        public Weather Day { get; set; }

         public Dictionary<int, string> TempWarnings = new Dictionary<int, string>();
       

        public IList<Weather> Weather = new List<Weather>();

        public Weather Today { get; set; }

        public IList<Weather> GetTodaysForecast(IList<Weather> weather)
        {
            Weather = weather;

            foreach (Weather day in weather.ToList())
            {
                if (day.FiveDayForecastValue == 1)
                {
                    Today = day;
                    weather.Remove(day);
                }
                else
                {
                    continue;
                }
            }

            return weather;
        }


    }
}
