using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTL
{
    public class UserDTO
    {
        public string ID { get; set; }
        public String Email { get; set; }
        public int Company_ID { get; set; }
        //public string UserName { get; set; }
        public DateTime EventTime { get; set; }
        public int DefaultAreaID { get; set; }
        public String DefaultAreaTitle { get; set; }
        public Boolean IsAdministrator { get; set; }

        public UserDTO()
        {
        }
        
        public UserDTO(IDataReader reader)
        {
            ID = Convert.ToString(reader["user_id"]);
            Company_ID = Convert.ToInt32(reader["company_id"]);
            EventTime = Convert.ToDateTime(reader["event_time"]);
            Email = Convert.ToString(reader["email"]);
            //UserName = Convert.ToString(reader["user_name"]);
            DefaultAreaID = Convert.ToInt32(reader["default_area_id"]);
            DefaultAreaTitle = Convert.ToString(reader["default_area_title"]);
            IsAdministrator = Convert.ToBoolean(reader["isAdministrator"]);
        }

        public UserDTO(String id, String email,int company_ID, string userName, int defaultAreaID)
        {
            ID = id;
            Email = email;
            Company_ID = company_ID;
            //UserName = userName;
            DefaultAreaID = defaultAreaID;
        }
    }
}
