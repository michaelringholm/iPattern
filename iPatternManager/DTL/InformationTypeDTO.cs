using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace DTL
{
    public class InformationTypeDTO
    {
        public InformationTypeDTO()
        {
        }

        public InformationTypeDTO(Nullable<Int32> id, String title, Int32 possibleLimit, Int32 certainLimit)
        {
            ID = id;
            Title = title;
            PossibleLimit = possibleLimit;
            CertainLimit = certainLimit;
        }

        public InformationTypeDTO(IDataReader reader)
        {
            ID = Convert.ToInt32(reader["id"]);
            Title = Convert.ToString(reader["title"]);
            PossibleLimit = Convert.ToInt32(reader["possible_limit"]);
            CertainLimit = Convert.ToInt32(reader["certain_limit"]);

			Object parentID = reader["parent_id"];
			if(parentID != DBNull.Value)
				ParentID = Convert.ToInt32(parentID);
        }

        public Nullable<Int32> ID { get; set; }

		[Required(ErrorMessage = "Navn skal angives")]
        public String Title { get; set; }
        public Int32 PossibleLimit { get; set; }

		[Required( ErrorMessage = "Grænseværdi skal angives")]
		[Range( 1, 5000, ErrorMessage="Grænsevædien skal være mellem 1 og 5000")]
        public Int32 CertainLimit { get; set; }
		public Nullable<Int32> ParentID { get; set; }
    }
}
