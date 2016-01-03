using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iPatternManager.Models;
using DAL;
using DTL;

namespace iPatternManager.Controllers
{
    [Authorize]
    public class MetaDataController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private MetaDataKeyListModel GetMetaDataKeyListModelWithEmptyRow(Int32 informationTypeID)
        {
            List<MetaDataKeyDTO> metaDataKeys = MetaDAC.GetMetaDataKeys(informationTypeID);
            MetaDataKeyDTO emptyMetaDataKey = new MetaDataKeyDTO();
            emptyMetaDataKey.InformationTypeID = informationTypeID;
            emptyMetaDataKey.Title = "";
            emptyMetaDataKey.RegEx = "";

            metaDataKeys.Add(emptyMetaDataKey);

            MetaDataKeyListModel metaDataKeyListModel = new MetaDataKeyListModel
            {
                InformationType = InformationTypeDAC.GetInformationType(informationTypeID, ContextManager.Current.SelectedAreaID.Value),
                MetaDataKeys = metaDataKeys
            };

            return metaDataKeyListModel;
        }

        public ActionResult SaveKeyList(IList<MetaDataKeyDTO> MetaDataKeyList, Int32 informationTypeID)
        {
            foreach (MetaDataKeyDTO metaDataKey in MetaDataKeyList)
                MetaDAC.StoreKey(metaDataKey);

            MetaDataKeyListModel metaDataKeyListModel = GetMetaDataKeyListModelWithEmptyRow(informationTypeID);
            return View("KeyListControl", metaDataKeyListModel);
        }

        public ActionResult DeleteKey(int id)
        {
            Nullable<Int32> informationTypeID = InformationTypeDAC.GetInformationTypeIDByMetaDataKeyID(id);
            MetaDAC.DeleteKey(id);

            MetaDataKeyListModel metaDataKeyListModel = GetMetaDataKeyListModelWithEmptyRow(informationTypeID.Value);
            return View("KeyList", metaDataKeyListModel);
        }

        public ActionResult KeyList(int id)
        {
            MetaDataKeyListModel metaDataKeyListModel = GetMetaDataKeyListModelWithEmptyRow(id);
            return View("KeyList", metaDataKeyListModel);
        }

        public ActionResult ValueList(int id)
        {
            return View("ValueList", new MetaDataValueListModel
            {
				AnalysisResultID = id,
                MetaDataValues = MetaDAC.GetMetaDataValues(id)
            });
        }

        public ActionResult ValueListControl(int id)
        {
            return View("ValueListControl", new MetaDataValueListModel
            {
                AnalysisResultID = id,
                MetaDataValues = MetaDAC.GetMetaDataValues(id)
            });
        }


    }
}
