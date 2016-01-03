
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

    [Authorize]
    public class UserController : Controller
    {
        public IMembershipService MembershipService { get; set; }

        public ActionResult Index()
        {
            UserDTO user = UserDAC.RestoreByName(Membership.GetUser().UserName );
            List<UserDTO> users = UserDAC.GetUsers(user.Company_ID);
            List<AreaDTO> areas = null;//UserDAC.GetAreas(user.ID);
            Dictionary<string, List<SelectListItem>> areaNameList = new Dictionary<string,List<SelectListItem>>();

            foreach (UserDTO thisUser in users)
            {
                areas = UserDAC.GetAreas(thisUser.ID);
                List<SelectListItem> areaNames = new List<SelectListItem>();
                foreach(AreaDTO area in areas)
                {
                    SelectListItem item = new SelectListItem { Text = area.Title, Value = area.ID.ToString()};
                    if (area.ID == thisUser.DefaultAreaID)
                        item.Selected = true;
                    areaNames.Add(item);
                }
                areaNameList.Add(thisUser.ID, areaNames);
            }
            areas = UserDAC.GetAreas(user.ID);
            //SelectList testList = new SelectList(areaNames, 2);
            //ViewData.s .CategoryId = new SelectList(query.AsEnumerable(), "CategoryID", "CategoryName", 3);

            //return View("Index", new UserIndexModel { Users = users, AreaNames = testList });
            return View("Index", new UserIndexModel { Users = users, AreaNameList = areaNameList, Areas = areas });
        }

        public ActionResult ChangeDefaultArea(int id)
        {
            ContextManager.Current.SelectedAreaID = id;
            return View("Change");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult SaveUsers(IList<UserDTO> users)
        {
            
            string[] rolelist = Roles.GetRolesForUser();
            if (ModelState.IsValid)
            {
                foreach (UserDTO user in users)
                {
                    if (!String.IsNullOrEmpty(user.ID))
                    {
                        UserDAC.Store(user);
                    }
                }

                UserDTO loggedInUser = UserDAC.RestoreByName(Membership.GetUser().UserName);
                List<UserDTO> reloadedUsers = UserDAC.GetUsers(loggedInUser.Company_ID);
                List<AreaDTO> areas = UserDAC.GetAreas(loggedInUser.ID);
                Dictionary<string, List<SelectListItem>> areaNameList = new Dictionary<string, List<SelectListItem>>();

                foreach (UserDTO thisUser in users)
                {
                    //thisUser.UserName = thisUser.Email;
                    List<SelectListItem> areaNames = new List<SelectListItem>();
                    foreach (AreaDTO area in areas)
                    {
                        SelectListItem item = new SelectListItem { Text = area.Title, Value = area.ID.ToString() };
                        if (area.ID == thisUser.DefaultAreaID)
                            item.Selected = true;
                        areaNames.Add(item);
                    }
                    areaNameList.Add(thisUser.ID, areaNames);
                }
                return View("UserListControl", new UserIndexModel { Users = reloadedUsers, AreaNameList = areaNameList, Areas = areas });

            }
            else
                return Redirect("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Areas(string id)
        {
            UserDTO loggedInUser = UserDAC.RestoreByName(Membership.GetUser().UserName);
            List<AreaDTO> allAreas = AreaDAC.GetAreasByUserCompany( loggedInUser.ID);
            List<AreaDTO> userAreas = AreaDAC.GetAreas(id);
            UserDTO selectedUser = UserDAC.Restore(id);
            List<UserDTO> reloadedUsers = UserDAC.GetUsers(loggedInUser.Company_ID);
            Dictionary<string, List<SelectListItem>> areaNameList = new Dictionary<string, List<SelectListItem>>();

                List<SelectListItem> areaNames = new List<SelectListItem>();
                foreach (AreaDTO area in allAreas)
                {
                    foreach(AreaDTO userArea in userAreas)
                    {
                        if (userArea.ID == area.ID)
                            area.Selected = true;
                    }


                    SelectListItem item = new SelectListItem { Text = area.Title, Value = area.ID.ToString() };
                    if (area.ID == selectedUser.DefaultAreaID)
                        item.Selected = true;
                    areaNames.Add(item);
                }
                areaNameList.Add("0", areaNames);


            //foreach (UserDTO thisUser in reloadedUsers)
            //{
            //    List<SelectListItem> areaNames = new List<SelectListItem>();
            //    foreach (AreaDTO area in areas)
            //    {
            //        SelectListItem item = new SelectListItem { Text = area.Title, Value = area.ID.ToString() };
            //        if (area.ID == thisUser.DefaultAreaID)
            //            item.Selected = true;
            //        areaNames.Add(item);
            //    }
            //    areaNameList.Add(thisUser.ID, areaNames);
            //}
            return View("UserAreasControl", new UserIndexModel { Users = reloadedUsers, AreaNameList = areaNameList, Areas = allAreas, SelectedUser = selectedUser});
        }

        public ActionResult SaveUserAreas(UserDTO selectedUser, List<AreaDTO> areas)
        {
            if (ModelState.IsValid)
            {
                UserDAC.RemoveAreaRelations(selectedUser.ID);
                foreach (AreaDTO area in areas)
                    if(area.Selected)
                        UserDAC.AddAreaRelation(selectedUser.ID, area.ID.Value);

                UserDTO user = UserDAC.RestoreByName(Membership.GetUser().UserName);
                List<UserDTO> users = UserDAC.GetUsers(user.Company_ID);
                areas = null;
                Dictionary<string, List<SelectListItem>> areaNameList = new Dictionary<string, List<SelectListItem>>();

                foreach (UserDTO thisUser in users)
                {
                    areas = UserDAC.GetAreas(thisUser.ID);
                    List<SelectListItem> areaNames = new List<SelectListItem>();
                    foreach (AreaDTO area in areas)
                    {
                        SelectListItem item = new SelectListItem { Text = area.Title, Value = area.ID.ToString() };
                        if (area.ID == thisUser.DefaultAreaID)
                            item.Selected = true;
                        areaNames.Add(item);
                    }
                    areaNameList.Add(thisUser.ID, areaNames);
                }
                areas = UserDAC.GetAreas(user.ID);
                //SelectList testList = new SelectList(areaNames, 2);
                //ViewData.s .CategoryId = new SelectList(query.AsEnumerable(), "CategoryID", "CategoryName", 3);

                //return View("Index", new UserIndexModel { Users = users, AreaNames = testList });
                return Index(); //View("IndexControl", new UserIndexModel { Users = users, AreaNameList = areaNameList, Areas = areas });
            }
            else
                return Redirect("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditUser(String id)
        {
            UserDTO user = UserDAC.Restore(id);
            string[] rolelist = Roles.GetRolesForUser();
            //ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            UserEditModel model = new UserEditModel();
            //model.UserName = user.UserName;
            model.Email = user.Email;
            model.IsAdministrator = user.IsAdministrator;
            model.UserID = id;
            return View(model);
        }

        public ActionResult SaveUser(UserEditModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO loggedInUser = UserDAC.RestoreByName(Membership.GetUser().UserName);
                MembershipUser User = Membership.GetUser(model.Email);


                UserDTO user = new UserDTO(model.UserID, model.Email, loggedInUser.Company_ID, model.Email, loggedInUser.DefaultAreaID);
                if(UserDAC.IsUserNameFree(model.Email) )
                    UserDAC.Store(user);


                if (model.IsAdministrator)
                    UserDAC.AddUserRole(model.Email, "Administrator");
                else
                    UserDAC.RemoveUserRole(model.Email, "Administrator");
                //return RedirectToAction("Index", "User");
                return Redirect("Index");
            }
            else
                return Redirect("Index");
              
        }
    }
}
