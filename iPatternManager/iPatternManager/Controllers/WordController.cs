using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using iPatternManager.Models;

namespace iPatternManager.Controllers
{
    [Authorize]
	[ValidateInput(false)]
	public class WordController : Controller
	{
		[ValidateInput(false)]
		public ActionResult Index()
		{
			return View(new WordIndexModel() { WordHeaders = WordDAC.GetAllRelevantWords(ContextManager.Current.SelectedAreaID.Value) });
		}

		[ValidateInput(false)]
		public ActionResult UnignoreWord(String id)
		{
			WordDAC.DeleteIrrelevantWord(HttpUtility.UrlDecode(id), ContextManager.Current.SelectedAreaID.Value);
			return View("Index", new WordIndexModel { WordHeaders = WordDAC.GetAllRelevantWords(ContextManager.Current.SelectedAreaID.Value) });
		}

		[ValidateInput(false)]
		public ActionResult IgnoreWord(String id)
		{
			WordDAC.StoreIrrelevantWord(HttpUtility.UrlDecode(id), ContextManager.Current.SelectedAreaID.Value);
			return View("Index", new WordIndexModel { WordHeaders = WordDAC.GetAllRelevantWords(ContextManager.Current.SelectedAreaID.Value) });
		}

		[ValidateInput(false)]
		public ActionResult Search(String searchText, Nullable<bool> showIgnoredWords)
		{
			if (showIgnoredWords.HasValue && (showIgnoredWords.Value == true))
				return View("WordListControl", new WordIndexModel()
				{
					ShowIgnoredWords = true,
					WordHeaders = WordDAC.GetIgnoredWords(ContextManager.Current.SelectedAreaID.Value, searchText)
				});
			else
				return View("WordListControl", new WordIndexModel()
				{
					ShowIgnoredWords = false,
					WordHeaders = WordDAC.GetAllRelevantWords(ContextManager.Current.SelectedAreaID.Value, searchText)
				});
		}
	}
}
