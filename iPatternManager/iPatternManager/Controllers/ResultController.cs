using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DTL;
using iPatternManager.Models;

namespace iPatternManager.Controllers
{
    [Authorize]
    public class ResultController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Details(Int32 id)
		{
			ResultDAC.UpdateReadStatus(id, true);
			AnalysisResultDTO result = ResultDAC.GetResult(id);
			return View(new InputDetailsModel()
			{
				InputMetaDataList = InputDAC.GetMetaDataList(result.AnalysisInputID),
				InputMessage = InputDAC.GetInputMessage(result.AnalysisInputID),
				AnalysisResult = result
			});
		}

    }
}
