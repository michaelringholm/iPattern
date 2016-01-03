using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTL;

namespace iPatternManager.Models
{
    public class MetaDataKeyListModel
    {
        public InformationTypeDTO InformationType { get; set; }
        public List<MetaDataKeyDTO> MetaDataKeys { get; set; }
    }

    public class MetaDataValueListModel
    {
        public Int32 AnalysisResultID { get; set; }
        public List<MetaDataValueDTO> MetaDataValues { get; set; }
    }
}
