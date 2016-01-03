using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using iPatternManager.Models;
using DAL;
using DTL;

namespace iPatternManager.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();
            ContextManager.Reset();

            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        //[Authorize(Users = "mrs@ihedge.dk, mch@ihedge.dk, mb@ihedge.dk, nicolas@ihedge.dk, eskil@ihedge.dk")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Register()
        {
			MembershipUser user =Membership.GetUser();

            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.Email, model.Password, model.Email);				
    
				if (createStatus == MembershipCreateStatus.Success)
                {
                    MembershipUser user = Membership.GetUser(model.Email);
                    //AreaDAC.AddAreaRelation(user.ProviderUserKey, areaID);
                    //FormsService.SignIn(model.UserName, false /* createPersistentCookie */);

                    UserDTO loggedInUser = UserDAC.RestoreByName(Membership.GetUser().UserName);

                    UserDTO newUser = new UserDTO(null, model.Email, loggedInUser.Company_ID, model.Email, loggedInUser.DefaultAreaID);
                    UserDAC.Store(newUser);
                    if (model.IsAdministrator)
                        UserDAC.AddUserRole(model.Email, "Administrator");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }


        // **************************************
        // URL: /Account/EditUser
        // **************************************

        //[Authorize(Users = "mrs@ihedge.dk, mch@ihedge.dk, mb@ihedge.dk, nicolas@ihedge.dk, eskil@ihedge.dk")]
        [Authorize(Roles = "Administrator")]
        public ActionResult EditUser(String userID)
        {
            UserDTO user = UserDAC.Restore(userID);
            string[] rolelist = Roles.GetRolesForUser();
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            RegisterModel model = new RegisterModel();
            //model.UserName = user.UserName;
            model.Email = user.Email;
            model.IsAdministrator = true;
            model.Password = "ibrain123";
            model.ConfirmPassword = "ibrain123";
            return View("Register", model);
        }

        [HttpPost]
        public ActionResult EditUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.Email, model.Password, model.Email);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    MembershipUser user = Membership.GetUser(model.Email);
                    //AreaDAC.AddAreaRelation(user.ProviderUserKey, areaID);
                    //FormsService.SignIn(model.UserName, false /* createPersistentCookie */);

                    UserDTO loggedInUser = UserDAC.RestoreByName(Membership.GetUser().UserName);

                    UserDTO newUser = new UserDTO(null, model.Email, loggedInUser.Company_ID, model.Email, loggedInUser.DefaultAreaID);
                    UserDAC.Store(newUser);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus)); 
                }
            }
            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }
        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

    }
}
