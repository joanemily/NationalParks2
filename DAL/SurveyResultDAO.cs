using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveyResultDAO : ISurveyResultDAO
    {
        private readonly string connectionString;

        public SurveyResultDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<SurveyResult> GetSurveyResults()
        {
            List<SurveyResult> surveyResults = new List<SurveyResult>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql =
@"SELECT *
FROM survey_result";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the command
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Loop through each row
                    while (rdr.Read())
                    {
                        SurveyResult sResult = RowToObject(rdr);
                        surveyResults.Add(sResult);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return surveyResults;
        }

        public int SaveSurvey(SurveyResult survey)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) VALUES (@parkCode, @emailAddress, @state, @activityLevel); Select @@Identity;";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", survey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", survey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", survey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        public IList<SurveyResultVM> GetSurveyResultsOrdered()
        {
            List<SurveyResultVM> surveyResults = new List<SurveyResultVM>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql =
@"SELECT p.parkName as ParkName, s.parkCode as Code, COUNT(s.parkCode) as NumOfFaves
from survey_result s
JOIN park p ON p.parkCode = s.parkCode
Group BY p.parkName, s.parkCode
ORDER BY NumOfFaves DESC";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    // Execute the command
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // Loop through each row
                    while (rdr.Read())
                    {
                        SurveyResultVM sResult = new SurveyResultVM();
                        sResult.ParkName = Convert.ToString(rdr["ParkName"]);
                        sResult.ParkCode = Convert.ToString(rdr["Code"]);
                        sResult.NumOfFaves = Convert.ToInt32(rdr["NumOfFaves"]);
                        surveyResults.Add(sResult);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return surveyResults;
        }

        private SurveyResult RowToObject(SqlDataReader rdr)
        {
            // Create a city
            SurveyResult sResult = new SurveyResult();
            sResult.SurveyId = Convert.ToInt32(rdr["surveyId"]);
            sResult.ParkCode = Convert.ToString(rdr["parkCode"]);
            sResult.EmailAddress = Convert.ToString(rdr["emailAddress"]);
            sResult.State = Convert.ToString(rdr["state"]);
            sResult.ActivityLevel = Convert.ToString(rdr["activityLevel"]);
            return sResult;
        }
    }
}
