using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public interface ISurveyResultDAO
    {
        IList<SurveyResult> GetSurveyResults();
        int SaveSurvey(SurveyResult survey);

        IList<SurveyResultVM> GetSurveyResultsOrdered();

    }
}
