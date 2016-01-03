using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTL;

namespace iPatternManager.Models
{
    public class InputIndexModel
    {
        public List<InputDTO> InputMessages { get; set; }
        public InformationTypeDTO InformationType { get; set; }
    }


    public class InputDetailsModel
    {
		public List<AttachmentDTO> Attachments { get; set; }
		public List<InputMetaDataDTO> InputMetaDataList { get; set; }
        public InputDTO InputMessage { get; set; }
		public AnalysisResultDTO AnalysisResult { get; set; }
    }
}
