using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTL
{
    public class InputDTO
    {
        public InputDTO()
        {
        }

        public InputDTO(IDataReader reader, bool hasResultInformation)
        {
            ID = Convert.ToInt32(reader["id"]);
            TextInput = Convert.ToString(reader["text_input"]);
            Status = Convert.ToString(reader["status"]);
            EventTime = Convert.ToDateTime(reader["event_time"]);

			if (hasResultInformation)
			{
				IsRead = Convert.ToBoolean(reader["is_read"]);
				AnalysisResultID = Convert.ToInt32(reader["analysis_result_id"]);
			}
        }

        public Nullable<Int32> ID { get; set; }
        public String TextInput { get; set; }
        public String Status { get; set; }
        public DateTime EventTime { get; set; }

		public Nullable<bool> IsRead { get; set; }
		public Nullable<Int32> AnalysisResultID { get; set; }

        public String TextInputSummary
        {
            get
            {
                if ((!String.IsNullOrEmpty(TextInput)) && TextInput.Length > 50)
                    return TextInput.Substring(0, 49);
                else
                    return TextInput;
            }
        }

        public String TextInputAsHTML
        {
            get
            {
                return TextInput.Replace("\n", "<br />").Replace("\t", "&nbsp;&nbsp;&nbsp;");
            }
        }
    }
}
