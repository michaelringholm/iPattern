using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DTL;

namespace DAL
{
    public class InformationTypeDAC
    {
        public static Int32 StoreInformationType(InformationTypeDTO informationType, Int32 areaID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[information_type_pkg.store]";

                SqlParameter idParam = cmd.Parameters.Add("id", SqlDbType.Int);
                idParam.Value = (informationType.ID.HasValue) ? (object)informationType.ID.Value : DBNull.Value;
                idParam.Direction = ParameterDirection.InputOutput;

				SqlParameter parentIDParam = cmd.Parameters.Add("@parent_id", SqlDbType.Int);
				parentIDParam.Value = (informationType.ParentID.HasValue) ? (object)informationType.ParentID.Value : DBNull.Value;
				parentIDParam.Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@title", informationType.Title);
                cmd.Parameters.AddWithValue("@possible_limit", informationType.PossibleLimit);
                cmd.Parameters.AddWithValue("@certain_limit", informationType.CertainLimit);
                cmd.Parameters.AddWithValue("@area_id", areaID);

                cnn.Open();
                cmd.ExecuteNonQuery();

                informationType.ID = Convert.ToInt32(idParam.Value);

                return informationType.ID.Value;
            }
        }

        public static Nullable<Int32> GetInformationTypeIDByWordID(int relevantWordID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT information_type_id FROM relevant_word_vw where id = @id";
                cmd.Parameters.AddWithValue("id", relevantWordID);

                cnn.Open();

                using (IDataReader idr = cmd.ExecuteReader())
                {
                    if (idr.Read())
                        return Convert.ToInt32(idr["information_type_id"]);
                    else
                        return null;
                }
            }
        }

        public static List<InformationTypeDTO> GetInformationTypes(Int32 areaID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM information_type it where it.area_id = @areaID and parent_id is null order by title";
                cmd.Parameters.AddWithValue("areaID", areaID);

                cnn.Open();

                List<InformationTypeDTO> informationTypes = new List<InformationTypeDTO>();
                using (IDataReader idr = cmd.ExecuteReader())
                {
                    while (idr.Read())
                        informationTypes.Add(new InformationTypeDTO(idr));
                }
                return informationTypes;
            }
        }

		public static bool HasChildren(Int32 id, Int32 areaID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT count(*) countx FROM information_type it where it.area_id = @areaID and parent_id = @parentID";
				cmd.Parameters.AddWithValue("parentID", id);
				cmd.Parameters.AddWithValue("areaID", areaID);

				cnn.Open();

				Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
				return count > 0;
			}
		}



		public static List<InformationTypeDTO> GetInformationTypes(Int32 parentID, Int32 areaID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM information_type it where it.area_id = @areaID and parent_id = @parentID order by title";
				cmd.Parameters.AddWithValue("parentID", parentID);
				cmd.Parameters.AddWithValue("areaID", areaID);

				cnn.Open();

				List<InformationTypeDTO> informationTypes = new List<InformationTypeDTO>();
				using (IDataReader idr = cmd.ExecuteReader())
				{
					while (idr.Read())
						informationTypes.Add(new InformationTypeDTO(idr));
				}
				return informationTypes;
			}
		}

        public static InformationTypeDTO GetInformationType(int id, int areaID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM information_type it where it.area_id = @areaID and id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("areaID", areaID);

                cnn.Open();

                using (IDataReader idr = cmd.ExecuteReader())
                {
                    if (idr.Read())
                        return new InformationTypeDTO(idr);
                    else
                        return null;
                }
            }
        }

        public static InformationTypeDTO GetParentInformationType(int id, int areaID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * FROM [information_type] where id = (SELECT parent_id FROM [information_type] where id = @id and area_id = @areaID ) and area_id = @areaID";
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("areaID", areaID);

                cnn.Open();

                using (IDataReader idr = cmd.ExecuteReader())
                {
                    if (idr.Read())
                        return new InformationTypeDTO(idr);
                    else
                        return null;
                }
            }
        }

        public static int? GetInformationTypeIDByMetaDataKeyID(int metaDataKeyID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT information_type_id FROM meta_data_key_vw where id = @id";
                cmd.Parameters.AddWithValue("id", metaDataKeyID);

                cnn.Open();

                using (IDataReader idr = cmd.ExecuteReader())
                {
                    if (idr.Read())
                        return Convert.ToInt32(idr["information_type_id"]);
                    else
                        return null;
                }
            }
        }

		public static void DeleteInformationType(int analysisInputID, int areaID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "[information_type_pkg.delete]";

				cmd.Parameters.AddWithValue("@id", analysisInputID);
				cmd.Parameters.AddWithValue("@area_id", areaID);

				cnn.Open();
				cmd.ExecuteNonQuery();
			}
		}
	}
}
