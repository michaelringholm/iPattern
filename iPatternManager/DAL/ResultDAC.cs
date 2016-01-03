using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DTL;

namespace DAL
{
	public class ResultDAC
	{

		public static void GenerateResult(Int32 analysisInputID, Int32 areaID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "[result_pkg.generate_result]";

				cmd.Parameters.AddWithValue("@analysis_input_id", analysisInputID);
				cmd.Parameters.AddWithValue("@area_id", areaID);

				cnn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public static string GetResultType(int resultID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT [information_type_title] FROM [analysis_result_vw] where id = @analysis_result_id";
				cmd.Parameters.AddWithValue("@analysis_result_id", resultID);

				cnn.Open();

				using (IDataReader idr = cmd.ExecuteReader())
				{
					if (idr.Read())
						return Convert.ToString(idr["information_type_title"]);
					else
						return null;
				}
			}
		}

		public static Dictionary<Int32, Int32> GetUnreadResults(Int32 areaID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM [unread_results_vw] urv where urv.area_id = @areaID";
				cmd.Parameters.AddWithValue("areaID", areaID);

				cnn.Open();

				Dictionary<Int32, Int32> unreadResults = new Dictionary<Int32, Int32>();
				using (IDataReader idr = cmd.ExecuteReader())
				{
                    while (idr.Read())
                    {
                        //int infoTypeID;
                        //int unread_count;
                        //infoTypeID = Convert.ToInt32(idr["information_type_id"]);
                        //unread_count = Convert.ToInt32(idr["unread_count"]);
                        unreadResults.Add(Convert.ToInt32(idr["information_type_id"]), Convert.ToInt32(idr["unread_count"]));
                    }
				}
				return unreadResults;
			}
		}

        public static Dictionary<Int32, Int32> GetUnreadResultsFilteredByMailboxUser(Int32 areaID, String mailboxUser)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [unread_results_filtered_by_mailbox_user_vw] urv where urv.area_id = @areaID and mailbox_user = @mailboxUser";
                cmd.Parameters.AddWithValue("areaID", areaID);
                cmd.Parameters.AddWithValue("mailboxUser", mailboxUser);

                cnn.Open();

                Dictionary<Int32, Int32> unreadResults = new Dictionary<Int32, Int32>();
                using (IDataReader idr = cmd.ExecuteReader())
                {
                    while (idr.Read())
                    {
                        //int infoTypeID;
                        //int unread_count;
                        //infoTypeID = Convert.ToInt32(idr["information_type_id"]);
                        //unread_count = Convert.ToInt32(idr["unread_count"]);
                        unreadResults.Add(Convert.ToInt32(idr["information_type_id"]), Convert.ToInt32(idr["unread_count"]));
                    }
                }
                return unreadResults;
            }
        }



		public static void UpdateReadStatus(int analysisResultID, bool isRead)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "[result_pkg.update_read_status]";

				cmd.Parameters.AddWithValue("@id", analysisResultID);
				cmd.Parameters.AddWithValue("@is_read", isRead);

				cnn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public static AnalysisResultDTO GetResult(int id)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM [analysis_result_vw] where id = @id";
				cmd.Parameters.AddWithValue("@id", id);

				cnn.Open();

				using (IDataReader idr = cmd.ExecuteReader())
				{
					if (idr.Read())
						return new AnalysisResultDTO(idr);
					else
						return null;
				}
			}
		}

		public static List<AnalysisResultDTO> GetResults(int analysisInputID, int areaID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM [analysis_result_vw] arv where analysis_input_id = @analysisInputID and arv.area_id = @areaID";
				cmd.Parameters.AddWithValue("analysisInputID", analysisInputID);
				cmd.Parameters.AddWithValue("areaID", areaID);

				cnn.Open();

				List<AnalysisResultDTO> results = new List<AnalysisResultDTO>();
				using (IDataReader idr = cmd.ExecuteReader())
				{
					while (idr.Read())
						results.Add(new AnalysisResultDTO(idr));
				}
				return results;
			}
		}
	}
}
