using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTL
{
    public class AnalysisResultDTO
    {
        public AnalysisResultDTO(IDataReader reader)
        {
            ID = Convert.ToInt32(reader["id"]);
            AnalysisInputID = Convert.ToInt32(reader["analysis_input_id"]);
            EventTime = Convert.ToDateTime(reader["event_time"]);
            InformationTypeID = Convert.ToInt32(reader["information_type_id"]);
            InformationTypeTitle = Convert.ToString(reader["information_type_title"]);
			IsRead = Convert.ToBoolean(reader["is_read"]);
        }

        public Nullable<Int32> ID { get; set; }
        public Int32 AnalysisInputID { get; set; }
        public DateTime EventTime { get; set; }
        public Int32 InformationTypeID { get; set; }
        public String InformationTypeTitle { get; private set; }
		public bool IsRead { get; set; }

    }
}
