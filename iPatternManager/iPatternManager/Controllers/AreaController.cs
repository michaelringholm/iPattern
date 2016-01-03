using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iPatternManager.Models;
using DAL;
using DTL;
using System.Web.Security;

namespace iPatternManager.Controllers
{
    //[Authorize(Users="mrs@ihedge.dk, mb@ihedge.dk, mch@ihedge.dk, nicolas@ihedge.dk, eskil@ihedge.dk")]
    [Authorize]
    public class AreaController : Controller
    {
        public ActionResult Index()
        {
            List<AreaDTO> areas = AreaDAC.GetAreasByUserCompany(Membership.GetUser().ProviderUserKey);
            String companyTitle = AreaDAC.GetCompanyTitleIDByID(areas[0].CompanyID);
            areas.Add(new AreaDTO{ Title = "" } );
            return View("Index", new AreaIndexModel { Areas = areas, CompanyTitle = companyTitle });
        }

        public ActionResult ChangeArea(int id)
        {
            ContextManager.Current.SelectedAreaID = id;
            return View("Change");
        }

        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Details(Int32 id)
        {
            /*List<RelevantWordDTO> relevantWords = WordDAC.GetRelevantWords(id);
            RelevantWordDTO relevantWordDTO = new RelevantWordDTO();
            relevantWordDTO.InformationTypeID = id;
            relevantWordDTO.Weight = 1;
            relevantWordDTO.Word = "";

            relevantWords.Add(relevantWordDTO);

            return View("Details", new DetailsModel(WordDAC.GetInformationType(id), relevantWords));*/
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult SaveAreas(IList<AreaDTO> areas)
        {
			if (ModelState.IsValid)
			{
                UserDTO loggedInUser = UserDAC.RestoreByName(Membership.GetUser().UserName);
                foreach (AreaDTO area in areas)
                {
                    area.CompanyID = loggedInUser.Company_ID;
                    if(!String.IsNullOrEmpty(area.Title))
                        AreaDAC.StoreArea(area);
                }
                //List<AreaDTO> newAreas = AreaDAC.GetAreas(Membership.GetUser().ProviderUserKey);

                List<AreaDTO> newAreas = AreaDAC.GetAreasByUserCompany(Membership.GetUser().ProviderUserKey);
                newAreas.Add(new AreaDTO { Title = "" });
                return View("IndexControl", new AreaIndexModel { Areas = newAreas });
                //return Redirect("IndexControl");
			}
			else
				return Redirect("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Remove(Int32 id)
        {
            AreaDAC.Delete(id);
            return Index();
            //return Details(areaID);
        }
    }
}
