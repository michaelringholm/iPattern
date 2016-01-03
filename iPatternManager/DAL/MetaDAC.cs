using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DTL;

namespace DAL
{
    public static class MetaDAC
    {
        public static Int32 StoreKey(MetaDataKeyDTO metaDataKey)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[meta_pkg.store_key]";

                SqlParameter idParam = cmd.Parameters.Add("id", SqlDbType.Int);
                idParam.Value = (metaDataKey.ID.HasValue) ? (object)metaDataKey.ID.Value : DBNull.Value;
                idParam.Direction = ParameterDirection.InputOutput;

                cmd.Parameters.AddWithValue("@title", metaDataKey.Title);
                cmd.Parameters.AddWithValue("@regex", metaDataKey.RegEx);
                cmd.Parameters.AddWithValue("@information_type_id", metaDataKey.InformationTypeID);

                cnn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToInt32(idParam.Value);
            }
        }

        public static Int32 StoreValue(MetaDataValueDTO metaDataValue)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[meta_pkg.store_value]";

                SqlParameter idParam = cmd.Parameters.Add("id", SqlDbType.Int);
                idParam.Value = (metaDataValue.ID.HasValue) ? (object)metaDataValue.ID.Value : DBNull.Value;
                idParam.Direction = ParameterDirection.InputOutput;

                cmd.Parameters.AddWithValue("@meta_value", metaDataValue.MetaValue);
                cmd.Parameters.AddWithValue("@meta_data_key_id", metaDataValue.MetaDataKeyID);
                cmd.Parameters.AddWithValue("@analysis_result_id", metaDataValue.AnalysisResultID);

                cnn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToInt32(idParam.Value);
            }
        }


        public static List<MetaDataKeyDTO> GetMetaDataKeys(Int32 informationTypeID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM meta_data_key_vw mdkv where mdkv.information_type_id = @information_type_id order by title asc";
                cmd.Parameters.AddWithValue("@information_type_id", informationTypeID);

                cnn.Open();

                List<MetaDataKeyDTO> metaDataKeys = new List<MetaDataKeyDTO>();
                using (IDataReader idr = cmd.ExecuteReader())
                {
                    while (idr.Read())
                        metaDataKeys.Add(new MetaDataKeyDTO(idr));
                }
                return metaDataKeys;
            }
        }

        public static List<MetaDataValueDTO> GetMetaDataValues(Int32 analysisResultID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "SELECT * FROM meta_data_value_vw mdvv where mdvv.analysis_result_id = @analysis_result_id order by meta_data_key_title asc";
				cmd.Parameters.AddWithValue("@analysis_result_id", analysisResultID);

                cnn.Open();

                List<MetaDataValueDTO> metaDataValues = new List<MetaDataValueDTO>();
                using (IDataReader idr = cmd.ExecuteReader())
                {
                    while (idr.Read())
                        metaDataValues.Add(new MetaDataValueDTO(idr));
                }
                return metaDataValues;
            }
        }

        public static void DeleteKey(int metaDataKeyID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[meta_pkg.delete_key]";

                cmd.Parameters.AddWithValue("@id", metaDataKeyID);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteValues(int analysisResultID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[meta_pkg.delete_values]";

				cmd.Parameters.AddWithValue("@analysis_result_id", analysisResultID);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
