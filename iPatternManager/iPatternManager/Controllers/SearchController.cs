using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iPatternManager.Models;
using System.Data.Linq;
using DAL;
using DTL;

namespace iPatternManager.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            var data = new DataClassesDataContext().analysis_inputs.Take(20);
            List<SearchResultItemDTO> searchResults = new List<SearchResultItemDTO>();

            foreach (var dataRow in data)
            {
                foreach (var ar in dataRow.analysis_results)
                {
                    var input = ar.analysis_input.input_meta_datas.Where(m => m.title == "subject").SingleOrDefault();
                    String title = "[Ingen titel]";
                    if (input != null)
                        title = input.meta_value;

                    String textInput = ar.analysis_input.text_input;

                    int startPos = textInput.IndexOf(" ", 0);
                    int endPos = startPos + 300;

                    if (endPos > textInput.Length - 1)
                        endPos = textInput.Length - 1;

                    String summary = textInput.Substring(startPos, endPos - startPos);

                    searchResults.Add(new SearchResultItemDTO { Title = title, Summary = summary });
                }
            }

            return View("Index", new SearchIndexModel {  SearchResults = searchResults });
        }

        public ActionResult Search(String phrase)
        {
            phrase = phrase.ToLower();

            var data = new DataClassesDataContext().analysis_inputs.Where(ai => ai.text_input.Contains(phrase)).Take(20);
            List<SearchResultItemDTO> searchResults = new List<SearchResultItemDTO>();

            foreach (var dataRow in data)
            {
                foreach (var ar in dataRow.analysis_results)
                {
                    var input = ar.analysis_input.input_meta_datas.Where(m => m.title == "subject").SingleOrDefault();
                    String title = "[Ingen titel]";
                    if (input != null)
                        title = input.meta_value;

                    String textInput = ar.analysis_input.text_input;

                    int phraseStartPos = textInput.ToLower().IndexOf(phrase);
                    int phraseEndPos = phraseStartPos + phrase.Length;

                    int startSearchPos = phraseStartPos -100;
                    if(startSearchPos < 0)
                        startSearchPos = 0;

                    int startPos = textInput.IndexOf(" ", startSearchPos);
                    int endPos = startPos + 300;

                    if (endPos > textInput.Length-1)
                        endPos = textInput.Length - 1;

                    String summary = textInput.Substring(startPos, endPos-startPos);
                    
                    //summary = summary.Replace(phrase, "<b>" + phrase + "</b>");
                    int replacePos = summary.ToLower().IndexOf(phrase);
                    summary = summary.Insert(replacePos, "<b>");
                    summary = summary.Insert(replacePos+phrase.Length+3, "</b>");

                    searchResults.Add(new SearchResultItemDTO {  Title = title, Summary = summary, Location = GetLocation(ar) });
                }
            }

            return View("SearchListControl", new SearchIndexModel {  SearchResults = searchResults });
            //return View("SearchListControl", new SearchIndexModel {  SearchResults = new DataClassesDataContext().analysis_inputs.Where(ai => ai.text_input.Contains(phrase)).Take(20).ToList() });
        }

        private string GetLocation(analysis_result ar)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Beskedmapper", Value = "/Message/Index" });

            int id = ar.information_type_id;
            DTL.InformationTypeDTO parentInformationType = DAL.InformationTypeDAC.GetParentInformationType(id, iPatternManager.ContextManager.Current.SelectedAreaID.Value);
            while (parentInformationType != null)
            {
                items.Insert(1, new SelectListItem { Text = parentInformationType.Title, Value = "/Input/Index/" + parentInformationType.ID.Value });
                id = parentInformationType.ID.Value;
                parentInformationType = DAL.InformationTypeDAC.GetParentInformationType(id, iPatternManager.ContextManager.Current.SelectedAreaID.Value);
            }

            items.Add(new SelectListItem { Text = ar.information_type.title, Value = "/Input/Index/" + ar.information_type_id });
            items.Add(new SelectListItem { Text = ar.id.ToString(), Value = "/Result/Details/" + ar.id });

            String locationHTML = "";

            foreach (var item in items)
                locationHTML += "/<a class=\"searchResult\" href=\"" + item.Value + "\">" + item.Text + "</a>";

            return locationHTML;
        }

    }
}
