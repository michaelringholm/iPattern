using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTL
{
    public class AnalysisResultItemDTO
    {
        public AnalysisResultItemDTO(IDataReader reader)
        {
            ID = Convert.ToInt32(reader["id"]);
            AnalysisResultID = Convert.ToInt32(reader["analysis_result_id"]);
            Word = Convert.ToString(reader["word"]);
            Weight = Convert.ToInt32(reader["weight"]);
            InformationTypeID = Convert.ToInt32(reader["information_type_id"]);
            InformationTypeTitle = Convert.ToString(reader["information_type_title"]);
            WordCount = Convert.ToInt32(reader["word_count"]);
            SubTotalWeight= Convert.ToInt32(reader["sub_total_weight"]);
        }

        public Nullable<Int32> ID { get; set; }
        public Int32 AnalysisResultID { get; set; }
        public String Word { get; set; }
        public Int32 Weight { get; set; }
        public Int32 InformationTypeID { get; set; }
        public String InformationTypeTitle { get; private set; }
        public Int32 WordCount { get; set; }
        public Int32 SubTotalWeight { get; set; }
    }
}
