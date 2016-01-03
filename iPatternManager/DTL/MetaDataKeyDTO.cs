using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTL
{
    public class MetaDataKeyDTO
    {
        public MetaDataKeyDTO()
        {
        }

        public MetaDataKeyDTO(IDataReader reader)
        {
            ID = Convert.ToInt32(reader["id"]);
            Title = Convert.ToString(reader["title"]);
            RegEx = Convert.ToString(reader["regex"]);
            InformationTypeID = Convert.ToInt32(reader["information_type_id"]);
            EventTime = Convert.ToDateTime(reader["event_time"]);
        }

        public Nullable<Int32> ID { get; set; }
        public String Title { get; set; }
        public String RegEx { get; set; }
        public Int32 InformationTypeID { get; set; }
        public DateTime EventTime { get; set; }
    }
}
