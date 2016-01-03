using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTL
{
    public class RelevantWordDTO
    {
        public enum CreationTypeEnum { MANUAL, AUTOMATIC };
        public RelevantWordDTO()
        {
        }

        public RelevantWordDTO(IDataReader reader)
        {
            ID = Convert.ToInt32(reader["id"]);
            Word = Convert.ToString(reader["word"]);
            Weight = Convert.ToInt32(reader["weight"]);
            InformationTypeID = Convert.ToInt32(reader["information_type_id"]);
            CreationType = (CreationTypeEnum)Enum.Parse(typeof(CreationTypeEnum), Convert.ToString(reader["creation_type"]));
        }

        public Nullable<Int32> ID { get; set; }
        public String Word { get; set; }
        public Int32 Weight { get; set; }
        public Int32 InformationTypeID { get; set; }
        public CreationTypeEnum CreationType { get; set; }
    }
}
