using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using System.Text;
using DTL;
using System.Web.Security;

namespace iPatternManager.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
			/*byte[] bytes = System.IO.File.ReadAllBytes(@"C:\Users\Michael\Dropbox\Android intro.pdf");
			InputDAC.StoreAttachment(new AttachmentDTO { Title = "Intro", BinaryData = bytes, AnalysisInputID = 223 });
			bytes = System.IO.File.ReadAllBytes(@"C:\Users\Michael\Dropbox\Getting Started.pdf");
			InputDAC.StoreAttachment(new AttachmentDTO { Title = "Getting Started", BinaryData = bytes, AnalysisInputID = 226 });*/

			//byte[] bytes = System.IO.File.ReadAllBytes(@"C:\Users\Michael\Documents\Demo.docx");
			//InputDAC.StoreAttachment(new AttachmentDTO { Title = "Demo", BinaryData = bytes, AnalysisInputID = 214 });

            //String test = Membership.GetUser("mrs@ihedge.dk").ResetPassword();
            //Membership.GetUser("mrs@ihedge.dk").ChangePassword("z3>ML.0YXSvS6E", "iBrain4ever");
            
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
