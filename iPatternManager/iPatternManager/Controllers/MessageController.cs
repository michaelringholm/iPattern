using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using iPatternManager.Models;
using DTL;
using Common;
using System.IO;
using System.Web.Security;

namespace iPatternManager.Controllers
{
    [Authorize]
	public class MessageController : Controller
	{
		public ActionResult Index()
		{
			return View("Index", new MessageIndexModel
			{				
				UnreadResults = ResultDAC.GetUnreadResults(ContextManager.Current.SelectedAreaID.Value),
				InformationTypes = InformationTypeDAC.GetInformationTypes(ContextManager.Current.SelectedAreaID.Value)
			});
		}

		public ActionResult SaveNew(InformationTypeDTO informationType)
		{
			if (ModelState.IsValid)
			{
				informationType.PossibleLimit = informationType.CertainLimit;
				Int32 id = InformationTypeDAC.StoreInformationType(informationType, ContextManager.Current.SelectedAreaID.Value);
				return Details(id);
			}
			else
				return New(informationType.ParentID);
		}

		public ActionResult New(Nullable<int> id)
		{
			/*InformationTypeDTO informationType = null;
			if(id.HasValue)
				informationType = InformationTypeDAC.GetInformationType(id.Value, ContextManager.Current.AreaID);*/

			return View("New", new InformationTypeDTO { ParentID = id } );
		}

		public ActionResult Delete(Nullable<int> id)
		{
			if (id.HasValue)
				InformationTypeDAC.DeleteInformationType(id.Value, ContextManager.Current.SelectedAreaID.Value);

			return Index();
		}

		public ActionResult Details(Int32 id)
		{
			List<RelevantWordDTO> relevantWords = WordDAC.GetRelevantWords(id);
			RelevantWordDTO relevantWordDTO = new RelevantWordDTO();
			relevantWordDTO.InformationTypeID = id;
			relevantWordDTO.Weight = 1;
			relevantWordDTO.Word = "";

			relevantWords.Add(relevantWordDTO);

			List<RelevantWordDTO> automaticallyWeightedWords = relevantWords.FindAll(rw => rw.CreationType == RelevantWordDTO.CreationTypeEnum.AUTOMATIC).FindAll(rw => rw.Weight > 0);
			List<RelevantWordDTO> manuallyWeightedWords = relevantWords.FindAll(rw => rw.CreationType == RelevantWordDTO.CreationTypeEnum.MANUAL).FindAll(rw => rw.Weight > 0);
			List<RelevantWordDTO> blockedWords = relevantWords.FindAll(rw => rw.Weight == 0);

			return View("Details", new MessageDetailsModel
			{
				InformationType = InformationTypeDAC.GetInformationType(id, ContextManager.Current.SelectedAreaID.Value),
				AutomaticallyWeightedWords = automaticallyWeightedWords,
				ManuallyWeightedWords = manuallyWeightedWords,
				BlockedWords = blockedWords
			});
		}

		[ValidateInput(false)]
		public ActionResult SaveDetails(InformationTypeDTO informationType, IList<RelevantWordDTO> RelevantWordList)
		{
			if (ModelState.IsValid)
			{
				informationType.PossibleLimit = informationType.CertainLimit;
				InformationTypeDAC.StoreInformationType(informationType, ContextManager.Current.SelectedAreaID.Value);

				foreach (RelevantWordDTO relevantWord in RelevantWordList)
				{
					if (!String.IsNullOrEmpty(relevantWord.Word))
					{
						WordDAC.StoreRelevantWord(relevantWord);
					}
				}

				return Details(informationType.ID.Value);
			}
			else
				return Details(informationType.ID.Value);
		}

		public ActionResult DeleteRelevantWord(Int32 id)
		{
			Nullable<Int32> informationTypeID = InformationTypeDAC.GetInformationTypeIDByWordID(id);
			WordDAC.DeleteRelevantWord(id);

			if (informationTypeID.HasValue)
				return Details(informationTypeID.Value);
			else
				return View("Index");
		}

		public ActionResult IgnoreWord(Int32 id)
		{
			Nullable<Int32> informationTypeID = InformationTypeDAC.GetInformationTypeIDByWordID(id);

			RelevantWordDTO relevantWord = WordDAC.GetRelevantWord(id);

			if (relevantWord != null)
				WordDAC.StoreIrrelevantWord(relevantWord.Word, ContextManager.Current.SelectedAreaID.Value);

			WordDAC.DeleteRelevantWord(id);

			if (informationTypeID.HasValue)
				return Details(informationTypeID.Value);
			else
				return View("Index");
		}

		public ActionResult BlockWord(Int32 id)
		{
			Nullable<Int32> informationTypeID = InformationTypeDAC.GetInformationTypeIDByWordID(id);

			RelevantWordDTO relevantWord = WordDAC.GetRelevantWord(id);

			if (relevantWord != null)
			{
				relevantWord.Weight = 0;
				relevantWord.CreationType = RelevantWordDTO.CreationTypeEnum.MANUAL;
				WordDAC.StoreRelevantWord(relevantWord);
			}

			if (informationTypeID.HasValue)
				return Details(informationTypeID.Value);
			else
				return View("Index");
		}

		public ActionResult RenderListControl(Int32 parentID, Int32 level, Nullable<bool> oddLine)
		{
			List<Int32> collapsedElements;

			TempData["OddLine"] = oddLine;

			if (Session["CollapsedElements"] != null)
				collapsedElements = (List<Int32>)Session["CollapsedElements"];
			else
				collapsedElements = new List<Int32>();

			if (parentID == -1)
			{
				return View("InformationTypeListControl", new MessageIndexModel
				{
					CollapsedElements = collapsedElements,
					Level = level,
					UnreadResults = ResultDAC.GetUnreadResults(ContextManager.Current.SelectedAreaID.Value),
                    UserFilteredUnreadResults = ResultDAC.GetUnreadResultsFilteredByMailboxUser(ContextManager.Current.SelectedAreaID.Value, Membership.GetUser().Email),
					InformationTypes = InformationTypeDAC.GetInformationTypes(ContextManager.Current.SelectedAreaID.Value)
				});
			}
			else
			{
				return View("InformationTypeListControl", new MessageIndexModel
				{
					CollapsedElements = collapsedElements,
					Level = level,
					UnreadResults = ResultDAC.GetUnreadResults(ContextManager.Current.SelectedAreaID.Value),
					InformationTypes = InformationTypeDAC.GetInformationTypes(parentID, ContextManager.Current.SelectedAreaID.Value)
				});
			}
		}

		public ActionResult CollapseExpand(Nullable<Int32> id)
		{			
			List<Int32> collapsedElements;

			if (Session["CollapsedElements"] != null)
			{
				collapsedElements = (List<Int32>)Session["CollapsedElements"];

				if (collapsedElements.Contains(id.Value))
					collapsedElements.Remove(id.Value);
				else
					collapsedElements.Add(id.Value);
			}
			else
			{
				collapsedElements = new List<Int32>();
				collapsedElements.Add(id.Value);
				Session["CollapsedElements"] = collapsedElements;
			}

			return RenderListControl(-1, 1, true);
		}

	}
}
