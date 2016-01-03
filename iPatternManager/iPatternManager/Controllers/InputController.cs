using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using iPatternManager.Models;
using BL;
using DTL;
using Common;
using System.IO;
using System.Web.Security;

namespace iPatternManager.Controllers
{
    [Authorize]
    public class InputController : Controller
    {
        public ActionResult ReanalyzeAllItems(Nullable<int> informationTypeID)
        {
            List<InputDTO> inputMessages = null;
            if (informationTypeID.HasValue)
            {
                InformationTypeDTO informationType = InformationTypeDAC.GetInformationType(informationTypeID.Value, ContextManager.Current.SelectedAreaID.Value);
                if (informationType.Title == "Unknown")
                    inputMessages = InputDAC.GetFilteredInputMessages(informationTypeID.Value, ContextManager.Current.SelectedAreaID.Value, Membership.GetUser().Email);
                else
                    inputMessages = InputDAC.GetInputMessages(informationTypeID.Value, ContextManager.Current.SelectedAreaID.Value, 0);
            }
            else
                inputMessages = InputDAC.GetInputMessages(ContextManager.Current.SelectedAreaID.Value, 100000);

            if (inputMessages != null)
            {
                foreach(InputDTO inputMessage in inputMessages)
                    Analyzer.RerunAnalysis(inputMessage.ID.Value, ContextManager.Current.SelectedAreaID.Value);
            }

            return Index(informationTypeID);
        }

        public ActionResult Rerun(int id)
        {
            AnalysisResultDTO currentResult = ResultDAC.GetResult(id);
            List<AnalysisResultDTO> results = Analyzer.RerunAnalysis(currentResult.AnalysisInputID, ContextManager.Current.SelectedAreaID.Value);

            if (results.Count(a => a.InformationTypeID == currentResult.InformationTypeID) > 0)
                return Index(results.Find(a => a.InformationTypeID == currentResult.InformationTypeID).InformationTypeID);
            else
                return Index(results[0].InformationTypeID);
        }

        public ActionResult RerunByAnalysisInputID(int id)
        {
            List<AnalysisResultDTO> results = Analyzer.RerunAnalysis(id, ContextManager.Current.SelectedAreaID.Value);
            return Index(null);
        }

        public ActionResult Index(Nullable<int> id)
        {
            if (id.HasValue)
            {
                List<InputDTO> inputMessages = new List<InputDTO>() ;
                InformationTypeDTO informationType = InformationTypeDAC.GetInformationType(id.Value, ContextManager.Current.SelectedAreaID.Value);
                if (informationType.Title == "Unknown") // id.Value == 203)
                    inputMessages = InputDAC.GetFilteredInputMessages(id.Value, ContextManager.Current.SelectedAreaID.Value, Membership.GetUser().Email);
                else
                    inputMessages = InputDAC.GetInputMessages(id.Value, ContextManager.Current.SelectedAreaID.Value,0);
                return View("Index", new InputIndexModel() { InputMessages = inputMessages, InformationType = InformationTypeDAC.GetInformationType(id.Value, ContextManager.Current.SelectedAreaID.Value) });
            }
            else
                return View("Index", new InputIndexModel() { InputMessages = InputDAC.GetInputMessages(ContextManager.Current.SelectedAreaID.Value, 50) });
        }

        public ActionResult Search(String searchText, Nullable<Int32> informationTypeID, String msgAmount)
        {
            Int32 msgAmountInt;
            if (!Int32.TryParse(msgAmount, out msgAmountInt))
                msgAmountInt = 50;
            if (informationTypeID.HasValue)
                return View("InputListControl", new InputIndexModel() { InputMessages = InputDAC.GetInputMessages(ContextManager.Current.SelectedAreaID.Value, informationTypeID.Value, searchText, msgAmountInt) });
            else
                return View("InputListControl", new InputIndexModel() { InputMessages = InputDAC.GetInputMessages(ContextManager.Current.SelectedAreaID.Value, searchText,msgAmountInt) });
        }

        public ActionResult Move(Nullable<Int32> informationTypeID, IList<SelectListItem> CheckedList)
        {
            /* Remove the ones with no value, as these are not checked - MRS */
            CheckedList = CheckedList.Where(a => a.Value != null).ToList<SelectListItem>();

            /* Move these */

            if (informationTypeID.HasValue)
                return View("InputListControl", new InputIndexModel() { InputMessages = InputDAC.GetInputMessages(informationTypeID.Value, ContextManager.Current.SelectedAreaID.Value,0) });
            else
                return View("InputListControl", new InputIndexModel() { InputMessages = InputDAC.GetInputMessages(ContextManager.Current.SelectedAreaID.Value, 50) });
        }

        public ActionResult Details(Int32 id)
        {
            //ResultDAC.UpdateReadStatus(id, true);
            return View(new InputDetailsModel()
            {
                InputMetaDataList = InputDAC.GetMetaDataList(id),
                InputMessage = InputDAC.GetInputMessage(id),
            });
        }

        public ActionResult InputMetaDataListControl(InputDetailsModel model)
        {
            return View("InputMetaDataListControl", model);
        }

        public ActionResult AttachmentListControl(InputDetailsModel model)
        {
            model.Attachments = InputDAC.GetAttachments(model.InputMessage.ID.Value);
            return View("AttachmentListControl", model);
        }

        public ActionResult ShowAttachment(int id)
        {

            //List<AttachmentDTO> attachments = InputDAC.GetAttachments(216);
            //MemoryStream ms = new MemoryStream(attachments[0].BinaryData);
            AttachmentDTO attachment = InputDAC.GetAttachment(id);
            MemoryStream ms = new MemoryStream(attachment.BinaryData);
            return new ImageResult(ms, FileTypeHelper.GetContentType(attachment.Filename));
        }
    }
}
