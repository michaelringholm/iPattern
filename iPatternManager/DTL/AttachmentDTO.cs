using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTL
{
	public class AttachmentDTO
	{
		public AttachmentDTO()
		{
		}

		public AttachmentDTO(IDataReader reader)
		{
			ID = Convert.ToInt32(reader["id"]);
			Title = Convert.ToString(reader["title"]);
			BinaryData = (byte[])reader["binary_data"];
			Filename = Convert.ToString(reader["filename"]);
			AnalysisInputID = Convert.ToInt32(reader["analysis_input_id"]);
			EventTime = Convert.ToDateTime(reader["event_time"]);
		}

		public Nullable<Int32> ID { get; private set; }
		public Int32 AnalysisInputID { get; set; }
		public String Title { get; set; }
		public byte[] BinaryData { get; set; }
		public String Filename { get; set; }
		public DateTime EventTime { get; private set; }
	}
}
