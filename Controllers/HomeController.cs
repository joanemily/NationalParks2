using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAO parkDAO;
        private IWeatherDAO weatherDAO;

        public HomeController(IParkDAO parkDAO, IWeatherDAO weatherDAO)
        {
            this.parkDAO = parkDAO;
            this.weatherDAO = weatherDAO;
        }

        public IActionResult Index()
        {
            //Get a list of all the parks and pass it into the view.
            IList<Park> parks = parkDAO.GetParks();
            return View(parks);
        }

        [HttpGet]
        public IActionResult Detail(string parkCode, ParkWeatherVM vm) //add temptype as a parameter
        {
            
            // Get the details of the specific park and pass it into the view model.
            vm.Park = parkDAO.GetParkByCode(parkCode);

            //Get the weather for that particular park and pass it into the view model.
            IList<Weather> weather = weatherDAO.GetWeather(parkCode);

            //Seperate today's forecast from the rest of the days and then order the rest of the list.
            weather = vm.GetTodaysForecast(weather);
            vm.Weather = weather.OrderBy(w => w.FiveDayForecastValue).ToList();

            //session will be GET in here, probably put it in to view model
            
            if (HttpContext.Session.GetString("temperature") == null || HttpContext.Session.GetString("temperature") == "F")
            {
                vm.TempType = "F";
                
            }
            else
            {
                vm.TempType = HttpContext.Session.GetString("temperature");
                foreach(Weather w in weather)
                {
                    vm.Today.Low = (int)w.ConvertTemp("C", w.Low);
                    vm.Today.High = (int)w.ConvertTemp("C", w.High);
                    w.Low = (int)w.ConvertTemp("C", w.Low);
                    w.High = (int)w.ConvertTemp("C", w.High);
                }
            }

            return View(vm);
        }

        // make a detail httpPost
        //session will be SET in here
        [HttpPost]
        public IActionResult Detail(string tempType, string parkCode, ParkWeatherVM vm)
        {
            HttpContext.Session.SetString("temperature", tempType);
            return RedirectToAction("Detail", new { parkCode = parkCode, ParkWeatherVM = vm});
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
