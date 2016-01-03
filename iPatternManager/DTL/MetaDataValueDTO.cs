using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTL
{
    public class MetaDataValueDTO
    {
        public MetaDataValueDTO()
        {
        }

        public MetaDataValueDTO(IDataReader reader)
        {
            ID = Convert.ToInt32(reader["id"]);
            MetaValue = Convert.ToString(reader["meta_value"]);
            MetaDataKeyTitle = Convert.ToString(reader["meta_data_key_title"]);
            MetaDataKeyID = Convert.ToInt32(reader["meta_data_key_id"]);
            AnalysisResultID = Convert.ToInt32(reader["analysis_result_id"]);
            EventTime = Convert.ToDateTime(reader["event_time"]);
        }

        public Nullable<Int32> ID { get; set; }
        public String MetaValue { get; set; }
        public Int32 MetaDataKeyID { get; set; }
		public Int32 AnalysisResultID { get; set; }
        public DateTime EventTime { get; set; }
        public String MetaDataKeyTitle { get; private set; }
    }
}
