using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private ISurveyResultDAO surveyResultDAO;
        private IParkDAO parkDAO;

        public SurveyController(ISurveyResultDAO surveyResultDAO, IParkDAO parkDAO)
        {
            this.surveyResultDAO = surveyResultDAO;
            this.parkDAO = parkDAO;

        }

        [HttpGet]
        public IActionResult Index()
        {
            //Get a list of parks for the select list in the view.
            IList<Park> parks = parkDAO.GetParks();
            SurveyResultVM vm = new SurveyResultVM();
            vm.Parks = new SelectList(parks, "ParkCode", "ParkName");

            //Return the view.
            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(SurveyResultVM vm)
        {
            //If the form is not completely filled out.
            if (!ModelState.IsValid)
            {
                //Get a list of the parks for the selectlist in the view.
                IList<Park> parks = parkDAO.GetParks();
                vm.Parks = new SelectList(parks, "ParkCode", "ParkName");

                //return the view with the errors.
                return View(vm);
            }

            //If the form is completely filled out, save the survey to the database.
            SurveyResult survey = vm.Survey;
            surveyResultDAO.SaveSurvey(survey);

            //Return the result view.
            return RedirectToAction("Results");
            
        }

        public IActionResult Results()
        {
            IList<SurveyResultVM> surveys = surveyResultDAO.GetSurveyResultsOrdered();

            return View(surveys); 
        }

    }
}