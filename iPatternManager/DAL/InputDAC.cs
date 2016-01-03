using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DTL;

namespace DAL
{
public class InputDAC
{
    public static Int32 StoreTextInput(String inputText, Int32 areaID)
    {
        using (SqlConnection cnn = WordDAC.NewConnection)
        using (SqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[input_pkg.store_text_input]";

            cmd.Parameters.AddWithValue("@area_id", areaID);
            cmd.Parameters.AddWithValue("@input_text", inputText);
            SqlParameter idParam = cmd.Parameters.Add("@id", SqlDbType.Int);              
            idParam.Direction = ParameterDirection.Output;

            cnn.Open();
            cmd.ExecuteNonQuery();

            return Convert.ToInt32(idParam.Value);
        }      
    }

    public static void TruncateBRSeaFCL()
    {
        using (SqlConnection cnn = WordDAC.NewConnection)
        using (SqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "truncate table brseafcl";

            cnn.Open();
            cmd.ExecuteNonQuery();
        }  
    }

    public static void StoreWordHeader(String word, Int32 wordCount, Int32 analysisInputID)
    {
        using (SqlConnection cnn = WordDAC.NewConnection)
        using (SqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[input_pkg.store_word_header]";

            cmd.Parameters.AddWithValue("@word", word);
            cmd.Parameters.AddWithValue("@word_count", wordCount);
            cmd.Parameters.AddWithValue("@analysis_input_id", analysisInputID);

            cnn.Open();
            cmd.ExecuteNonQuery();
        }  
    }

    public static List<InputDTO> GetInputMessages(Int32 areaID, int take)
    {
        using (SqlConnection cnn = WordDAC.NewConnection)
        using (SqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP(" + take + ") * FROM [analysis_input_without_unknown_vw] aiv where aiv.area_id = @areaID order by event_time desc";
            cmd.Parameters.AddWithValue("areaID", areaID);

            cnn.Open();

            List<InputDTO> inputMessages = new List<InputDTO>();
            using (IDataReader idr = cmd.ExecuteReader())
            {
                while (idr.Read())
					inputMessages.Add(new InputDTO(idr, false));
            }
            return inputMessages;
        }
    }

    public static List<InputDTO> GetInputMessages(int areaID, string searchText, Int32 msgAmount)
    {
        if (String.IsNullOrEmpty(searchText))
            searchText = "";

        using (SqlConnection cnn = WordDAC.NewConnection)
        using (SqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;
            if (msgAmount < 1)
                cmd.CommandText = "SELECT top(50) * FROM [analysis_input_without_unknown_vw] aiv where aiv.area_id = @areaID and lower(text_input) like '%" + searchText.ToLower().Trim() + "%' order by event_time desc";
            else
                cmd.CommandText = "SELECT TOP(@msgAmount) * FROM [analysis_input_without_unknown_vw] aiv where aiv.area_id = @areaID and lower(text_input) like '%" + searchText.ToLower().Trim() + "%' order by event_time desc";

            cmd.Parameters.AddWithValue("areaID", areaID);
            cmd.Parameters.AddWithValue("@msgAmount", msgAmount);

            cnn.Open();

            List<InputDTO> inputMessages = new List<InputDTO>();
            using (IDataReader idr = cmd.ExecuteReader())
            {
                while (idr.Read())
					inputMessages.Add(new InputDTO(idr, false));
            }
            return inputMessages;
        }
    }

    public static List<InputDTO> GetInputMessages(int areaID, Int32 informationTypeID, string searchText, int msgAmount)
    {
        if (String.IsNullOrEmpty(searchText))
            searchText = "";

        using (SqlConnection cnn = WordDAC.NewConnection)
        using (SqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;

            if (msgAmount < 1)
                cmd.CommandText = "SELECT top(50) * FROM [analysis_input_by_type_vw] aiv where aiv.area_id = @areaID and information_type_id = @informationTypeID and lower(text_input) like '%" + searchText.ToLower().Trim() + "%' order by event_time desc";
            else
                cmd.CommandText = "SELECT TOP(@msgAmount) * FROM [analysis_input_by_type_vw] aiv where aiv.area_id = @areaID and information_type_id = @informationTypeID and lower(text_input) like '%" + searchText.ToLower().Trim() + "%' order by event_time desc";
            cmd.Parameters.AddWithValue("areaID", areaID);
            cmd.Parameters.AddWithValue("@informationTypeID", informationTypeID);
            cmd.Parameters.AddWithValue("@msgAmount", msgAmount);

            cnn.Open();

            List<InputDTO> inputMessages = new List<InputDTO>();
            using (IDataReader idr = cmd.ExecuteReader())
            {
                while (idr.Read())
					inputMessages.Add(new InputDTO(idr, true));
            }
            return inputMessages;
        }
    }

    public static List<InputDTO> GetInputMessages(Int32 informationTypeID, Int32 areaID, Int32 msgAmount)
    {
        using (SqlConnection cnn = WordDAC.NewConnection)
        using (SqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;
            if (msgAmount < 1)
                cmd.CommandText = @"SELECT top (50) * FROM [analysis_input_by_type_vw] aiv 
                                where aiv.area_id = @areaID 
                                and information_type_id = @informationTypeID 
                                order by event_time desc";
            else
                cmd.CommandText = @"SELECT TOP( @msgAmount) * FROM [analysis_input_by_type_vw] aiv 
                                where aiv.area_id = @areaID 
                                and information_type_id = @informationTypeID 
                                order by event_time desc";
            cmd.Parameters.AddWithValue("@msgAmount", msgAmount);
            cmd.Parameters.AddWithValue("@informationTypeID", informationTypeID);
            cmd.Parameters.AddWithValue("areaID", areaID);

            cnn.Open();

            List<InputDTO> inputMessages = new List<InputDTO>();
            using (IDataReader idr = cmd.ExecuteReader())
            {
                while (idr.Read())
					inputMessages.Add(new InputDTO(idr, true));
            }
            return inputMessages;
        }
    }

    public static List<InputDTO> GetFilteredInputMessages(Int32 informationTypeID, Int32 areaID, String email)
    {
        using (SqlConnection cnn = WordDAC.NewConnection)
        using (SqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"SELECT * FROM [analysis_input_by_type_filtered_vw] aiv 
                                where aiv.area_id = @areaID 
                                and information_type_id = @informationTypeID 
                                and (email = @email or filter_on_unknown = 0)
                                order by event_time desc";
            cmd.Parameters.AddWithValue("@informationTypeID", informationTypeID);
            cmd.Parameters.AddWithValue("areaID", areaID);
            cmd.Parameters.AddWithValue("email", email);

            cnn.Open();

            List<InputDTO> inputMessages = new List<InputDTO>();
            using (IDataReader idr = cmd.ExecuteReader())
            {
                while (idr.Read())
                    inputMessages.Add(new InputDTO(idr, true));
            }
            return inputMessages;
        }
    }

    public static InputDTO GetInputMessage(int id)
    {
        using (SqlConnection cnn = WordDAC.NewConnection)
        using (SqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM [analysis_input_vw] where id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            cnn.Open();

            using (IDataReader idr = cmd.ExecuteReader())
            {
                if (idr.Read())
					return new InputDTO(idr, false);
                else
                    return null;
            }
        }
    }

    public static void DeleteWordHeaders(int analysisInputID)
    {
        using (SqlConnection cnn = WordDAC.NewConnection)
        using (SqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[input_pkg.delete_word_headers]";

            cmd.Parameters.AddWithValue("@analysis_input_id", analysisInputID);

            cnn.Open();
            cmd.ExecuteNonQuery();
        }  
    }

	public static void StoreMetaData(InputMetaDataDTO metaData)
	{
		using (SqlConnection cnn = WordDAC.NewConnection)
		using (SqlCommand cmd = cnn.CreateCommand())
		{
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[input_pkg.store_meta_data]";

			cmd.Parameters.AddWithValue("@title", metaData.Title);
			cmd.Parameters.AddWithValue("@meta_value", metaData.MetaValue);
			cmd.Parameters.AddWithValue("@analysis_input_id", metaData.InputID);

			cnn.Open();
			cmd.ExecuteNonQuery();
		}
	}

	public static List<InputMetaDataDTO> GetMetaDataList(int analysisInputID)
	{
		using (SqlConnection cnn = WordDAC.NewConnection)
		using (SqlCommand cmd = cnn.CreateCommand())
		{
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT * FROM [input_meta_data] imd where analysis_input_id = @analysisInputID order by event_time desc";
			cmd.Parameters.AddWithValue("@analysisInputID", analysisInputID);

			cnn.Open();

			List<InputMetaDataDTO> inputMetaDataList = new List<InputMetaDataDTO>();
			using (IDataReader idr = cmd.ExecuteReader())
			{
				while (idr.Read())
					inputMetaDataList.Add(new InputMetaDataDTO(idr));
			}
			return inputMetaDataList;
		}
	}

	public static void StoreAttachment(AttachmentDTO attachment)
	{
		using (SqlConnection cnn = WordDAC.NewConnection)
		using (SqlCommand cmd = cnn.CreateCommand())
		{
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[input_pkg.store_attachment]";

			cmd.Parameters.AddWithValue("@analysis_input_id", attachment.AnalysisInputID);
			cmd.Parameters.AddWithValue("@title", attachment.Title);
			cmd.Parameters.AddWithValue("@filename", attachment.Filename);
			cmd.Parameters.AddWithValue("@binary_data", attachment.BinaryData);

			cnn.Open();
			cmd.ExecuteNonQuery();
		}
	}

	public static List<AttachmentDTO> GetAttachments(int analysisInputID)
	{
		using (SqlConnection cnn = WordDAC.NewConnection)
		using (SqlCommand cmd = cnn.CreateCommand())
		{
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT * FROM [attachment] where analysis_input_id = @analysisInputID order by title";
			cmd.Parameters.AddWithValue("@analysisInputID", analysisInputID);

			cnn.Open();

			List<AttachmentDTO> attachments = new List<AttachmentDTO>();
			using (IDataReader idr = cmd.ExecuteReader())
			{
				while (idr.Read())
					attachments.Add(new AttachmentDTO(idr));
			}
			return attachments;
		}
	}

	public static AttachmentDTO GetAttachment(int id)
	{
		using (SqlConnection cnn = WordDAC.NewConnection)
		using (SqlCommand cmd = cnn.CreateCommand())
		{
			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "SELECT * FROM [attachment] where id = @id";
			cmd.Parameters.AddWithValue("@id", id);

			cnn.Open();

			List<AttachmentDTO> attachments = new List<AttachmentDTO>();
			using (IDataReader idr = cmd.ExecuteReader())
			{
				if (idr.Read())
					return new AttachmentDTO(idr);
				else
					return null;
			}
		}
	}
}

}
