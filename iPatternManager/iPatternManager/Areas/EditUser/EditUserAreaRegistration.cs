using System.Web.Mvc;

namespace iPatternManager.Areas.EditUser
{
    public class EditUserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "EditUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "EditUser_default",
                "EditUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
