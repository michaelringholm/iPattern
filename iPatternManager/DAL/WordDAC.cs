using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DAL
{
	public class WordDAC
	{
		internal static SqlConnection NewConnection
		{
			get
			{
				return new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString);
			}
		}


		public static List<WordHeaderDTO> GetIgnoredWords(Int32 areaID, String searchText)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM ignored_word_vw iw where iw.area_id = @areaID and lower(word) like '%" + searchText.ToLower().Trim() + "%' order by word_count desc, word asc";
				cmd.Parameters.AddWithValue("@areaID", areaID);

				cnn.Open();

				List<WordHeaderDTO> ignoredWords = new List<WordHeaderDTO>();
				using (IDataReader idr = cmd.ExecuteReader())
				{
					while (idr.Read())
						ignoredWords.Add(new WordHeaderDTO(idr));
				}
				return ignoredWords;
			}
		}

		public static List<WordHeaderDTO> GetAllRelevantWords(Int32 areaID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM all_non_irrelevant_input_word_headers_vw anii where anii.area_id = @areaID order by word_count desc, word asc";
				cmd.Parameters.AddWithValue("@areaID", areaID);

				cnn.Open();

				List<WordHeaderDTO> allNonIrrelevantWords = new List<WordHeaderDTO>();
				using (IDataReader idr = cmd.ExecuteReader())
				{
					while (idr.Read())
						allNonIrrelevantWords.Add(new WordHeaderDTO(idr));
				}
				return allNonIrrelevantWords;
			}
		}

		public static List<WordHeaderDTO> GetAllRelevantWords(Int32 areaID, String searchText)
		{
			if (String.IsNullOrEmpty(searchText))
				searchText = "";

			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM all_non_irrelevant_input_word_headers_vw anii where anii.area_id = @areaID and lower(word) like '%" + searchText.ToLower().Trim() + "%' order by word_count desc, word asc";
				cmd.Parameters.AddWithValue("@areaID", areaID);

				cnn.Open();

				List<WordHeaderDTO> attachmentList = new List<WordHeaderDTO>();
				using (IDataReader idr = cmd.ExecuteReader())
				{
					while (idr.Read())
						attachmentList.Add(new WordHeaderDTO(idr));
				}
				return attachmentList;
			}
		}


		/*public Int32 SaveMessage(Nullable<Int32> id, Nullable<Int32> originalMessageId, string title, string content, int stepId)
		{
			using (OracleConnection cnn = DAC.NewConnection)
			using (OracleCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "MESSAGE_PKG.STORE";

				OracleParameter idParam = cmd.Parameters.Add("P_ID", OracleType.Number);
				idParam.Value = (id.HasValue) ? (object)id.Value : DBNull.Value;
				idParam.Direction = ParameterDirection.InputOutput;

				cmd.Parameters.AddWithValue("P_TITLE", title);
				cmd.Parameters.AddWithValue("P_ORIGINAL_MESSAGE_ID", (originalMessageId.HasValue) ? (object)originalMessageId.Value : DBNull.Value);
				cmd.Parameters.AddWithValue("P_CONTENT", content);
				cmd.Parameters.AddWithValue("P_WORKFLOW_STEP_ID", stepId);
				cmd.Parameters.AddWithValue("P_MODIFIED_BY", 1);

				cnn.Open();
				cmd.ExecuteNonQuery();

				return Convert.ToInt32(idParam.Value);
			}
		}*/

		public static void StoreIrrelevantWord(string irrelevantWord, Int32 areaID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "[word_pkg.store_irrelevant_word]";

				cmd.Parameters.AddWithValue("word", irrelevantWord);
				cmd.Parameters.AddWithValue("area_id", areaID);

				cnn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public static RelevantWordDTO GetRelevantWord(int relevantWordID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM relevant_word_vw where id = @id";
				cmd.Parameters.AddWithValue("id", relevantWordID);

				cnn.Open();

				using (IDataReader idr = cmd.ExecuteReader())
				{
					if (idr.Read())
						return new RelevantWordDTO(idr);
					else
						return null;
				}
			}
		}

		public static List<RelevantWordDTO> GetRelevantWords(Int32 informationTypeID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "SELECT * FROM relevant_word where information_type_id = @information_type_id order by creation_type, word";
				cmd.Parameters.AddWithValue("information_type_id", informationTypeID);

				cnn.Open();

				List<RelevantWordDTO> relevantWords = new List<RelevantWordDTO>();
				using (IDataReader idr = cmd.ExecuteReader())
				{
					while (idr.Read())
						relevantWords.Add(new RelevantWordDTO(idr));
				}
				return relevantWords;
			}
		}

		public static void StoreRelevantWord(RelevantWordDTO relevantWord)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "[word_pkg.store_relevant_word]";

				SqlParameter idParam = cmd.Parameters.Add("id", SqlDbType.Int);
				idParam.Value = (relevantWord.ID.HasValue) ? (object)relevantWord.ID.Value : DBNull.Value;
				idParam.Direction = ParameterDirection.InputOutput;

				cmd.Parameters.AddWithValue("word",  relevantWord.Word.Trim());
				cmd.Parameters.AddWithValue("weight", relevantWord.Weight);
				cmd.Parameters.AddWithValue("information_type_id", relevantWord.InformationTypeID);
				cmd.Parameters.AddWithValue("creation_type", relevantWord.CreationType.ToString());

				cnn.Open();
				cmd.ExecuteNonQuery();
			}
		}

		public static void DeleteRelevantWord(int relevantWordID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "[word_pkg.delete_relevant_word]";
				cmd.Parameters.AddWithValue("id", relevantWordID);

				cnn.Open();
				cmd.ExecuteNonQuery();
			}
		}


		public static void DeleteIrrelevantWord(string irrelevantWord, int areaID)
		{
			using (SqlConnection cnn = WordDAC.NewConnection)
			using (SqlCommand cmd = cnn.CreateCommand())
			{
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "[word_pkg.delete_irrelevant_word]";

				cmd.Parameters.AddWithValue("word", irrelevantWord);
				cmd.Parameters.AddWithValue("area_id", areaID);

				cnn.Open();
				cmd.ExecuteNonQuery();
			}
		}
	}
}
