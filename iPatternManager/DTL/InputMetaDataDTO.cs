using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTL
{
	public class InputMetaDataDTO
	{
		public InputMetaDataDTO()
		{
		}

		public InputMetaDataDTO(IDataReader reader)
		{
			Title = Convert.ToString(reader["title"]);
			MetaValue = Convert.ToString(reader["meta_value"]);
			InputID = Convert.ToInt32(reader["analysis_input_id"]);
			EventTime = Convert.ToDateTime(reader["event_time"]);
		}

		public String Title { get; set; }
		public String MetaValue { get; set; }
		public Int32 InputID { get; set; }
		public DateTime EventTime { get; private set; }
	}
}
