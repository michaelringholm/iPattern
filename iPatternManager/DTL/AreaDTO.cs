using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace DTL
{
    public class AreaDTO
    {
        public AreaDTO()
        {
        }

        public AreaDTO(IDataReader reader)
        {
            ID = Convert.ToInt32(reader["id"]);
            Title = Convert.ToString(reader["title"]); ;
            CompanyID = Convert.ToInt32(reader["company_id"]);
            EventTime = Convert.ToDateTime(reader["event_time"]);
            Selected = false;
            FilterOnUnknown = Convert.ToBoolean(reader["filter_on_unknown"]);
        }

        public Nullable<Int32> ID { get; set; }
        public String Title { get; set; }
        public DateTime EventTime { get; set; }
        public Boolean Selected { get; set; }
        public int CompanyID { get; set; }
        public Boolean FilterOnUnknown { get; set; }
    }
}
