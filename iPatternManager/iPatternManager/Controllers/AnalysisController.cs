using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace iPatternManager.Controllers
{
    [Authorize]
    public class AnalysisController : Controller
    {
        public ActionResult Index()
        {
            return View(AnalysisDAC.GetAnalysisResults(ContextManager.Current.SelectedAreaID.Value));
        }


        public ActionResult Details(Int32 id)
        {
            return View(AnalysisDAC.GetAnalysisResultItems(id));
        }

    }
}
