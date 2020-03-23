using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Capstone.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        /*
         *  TODO 01: Create a DAL folder and pull in data from NPGeek database
         *  TODO 02: Use the DALs to populate the data in ParkDAO SurveyResultDAO and WeatherDAO
         *  TODO 03: Edit the HomeController so you can view a list of all national parks (picture of park, name, location, and short summary)
         *  TODO 04: Clicking on a park from the home page will bring you to a view that shows that park in detail, this detail page includes all of the info
         *  in the park data source (detail view in HomeController)
         *  TODO 05: On the detail page for a particular park, allow the user to see a 5-day weather forecast, use capstone paper to see all required elements
         *  TODO 06: Allow the user the option to change temperature preference to fahrenheit or celsius, this will be saved in a session/cookie and will stay the same
         *  while the user is browsing the website
         *  TODO 07: Add a SurveyController that will contain a view allowing the user to take a survey, and POST that information to the database, then they 
         *  will be redirected to a survey_results page containing data from the survey_result SQL table
         */
    }
}
