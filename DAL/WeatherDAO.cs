using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class WeatherDAO: IWeatherDAO
    {
        private readonly string connectionString;

        public WeatherDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Weather> GetWeather(string parkCode)
        {
            List<Weather> weatherList = new List<Weather>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql =
@"SELECT *
FROM weather
WHERE parkCode = @parkCode";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkcode", parkCode);


                    // Execute the command
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Loop through each row
                    while (rdr.Read())
                    {
                        Weather weather = RowToObject(rdr);
                        weatherList.Add(weather);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return weatherList;
        }

        private Weather RowToObject(SqlDataReader rdr)
        {
            // Create a city
            Weather weather = new Weather();
            weather.ParkCode = Convert.ToString(rdr["parkCode"]);
            weather.FiveDayForecastValue = Convert.ToInt32(rdr["fiveDayForecastValue"]);
            weather.Low = Convert.ToInt32(rdr["low"]);
            weather.High = Convert.ToInt32(rdr["high"]);
            weather.Forecast = Convert.ToString(rdr["forecast"]);
            return weather;
        }
    }
}
