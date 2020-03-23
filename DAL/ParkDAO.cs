using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class ParkDAO : IParkDAO
    {
        private readonly string connectionString;

        public ParkDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Park> GetParks()
        {
            List<Park> parkList = new List<Park>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql =
@"SELECT *
FROM park";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the command
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Loop through each row
                    while (rdr.Read())
                    {
                        Park park = RowToObject(rdr);
                        parkList.Add(park);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return parkList;
        }

        public Park GetParkByCode(string parkCode)
        {
            Park park = null;
            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql =
@"SELECT *
FROM park 
join weather on park.parkCode = weather.parkCode
where park.parkCode = @parkcode";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkcode", parkCode);

                    // Execute the command
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Loop through each row
                    if (reader.Read())
                    {
                        // Create a city
                        park = RowToObject(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return park;
        }


        private Park RowToObject(SqlDataReader rdr)
        {
            // Create a city
            Park park = new Park();
            park.ParkCode = Convert.ToString(rdr["parkCode"]);
            park.ParkName = Convert.ToString(rdr["parkName"]);
            park.State = Convert.ToString(rdr["state"]);
            park.Acreage = Convert.ToInt32(rdr["acreage"]);
            park.ElevationInFeet = Convert.ToInt32(rdr["elevationInFeet"]);
            park.MilesOfTrail = Convert.ToDouble(rdr["milesOfTrail"]);
            park.NumberOfCampsites = Convert.ToInt32(rdr["numberOfCampsites"]);
            park.Climate = Convert.ToString(rdr["climate"]);
            park.YearFounded = Convert.ToInt32(rdr["yearFounded"]);
            park.AnnualVisitorCount = Convert.ToInt32(rdr["annualVisitorCount"]);
            park.InspirationalQuote = Convert.ToString(rdr["inspirationalQuote"]);
            park.InspirationalQuoteSource = Convert.ToString(rdr["inspirationalQuoteSource"]);
            park.ParkDescription = Convert.ToString(rdr["parkDescription"]);
            park.EntryFee = Convert.ToInt32(rdr["entryFee"]);
            park.NumberOfAnimalSpecies = Convert.ToInt32(rdr["numberOfAnimalSpecies"]);
            return park;
        }

    }
}
