using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTL;

namespace iPatternManager.Models
{
    public enum CurrentListEnum { None, Manual, Automatic, Blocked };
    public class MessageDetailsModel
    {
        public Int32 ItemCounter { get; set; }
        public CurrentListEnum CurrentList { get; set; }
        public InformationTypeDTO InformationType { get; set; }

        public List<RelevantWordDTO> ManuallyWeightedWords { get; set; }
        public List<RelevantWordDTO> AutomaticallyWeightedWords { get; set; }
        public List<RelevantWordDTO> BlockedWords { get; set; }

        public List<RelevantWordDTO> CurrentWords 
        {
            get
            {
                switch (CurrentList)
                {
                    case CurrentListEnum.Manual: return ManuallyWeightedWords;break;
                    case CurrentListEnum.Automatic: return AutomaticallyWeightedWords;break;
                    case CurrentListEnum.Blocked: return BlockedWords;break;
                    default: return ManuallyWeightedWords;
                }
            }
        }
    }


	public class MessageIndexModel
	{
		public List<Int32> CollapsedElements { get; set; }
		public Int32 Level { get; set; }
        public Dictionary<Int32, Int32> UnreadResults { get; set; }
        public Dictionary<Int32, Int32> UserFilteredUnreadResults { get; set; }
		public List<InformationTypeDTO> InformationTypes { get; set; }
	}
}
