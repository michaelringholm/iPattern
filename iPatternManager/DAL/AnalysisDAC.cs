using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DTL;
using System.Data;
using Common;

namespace DAL
{
    public static class AnalysisDAC
    {
        public static List<AnalysisResultDTO> GetAnalysisResults(Int32 areaID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM analysis_result_vw arv where area_id = @areaID order by event_time desc";
                cmd.Parameters.AddWithValue("@areaID", areaID);

                cnn.Open();

                List<AnalysisResultDTO> analysisResults = new List<AnalysisResultDTO>();
                using (IDataReader idr = cmd.ExecuteReader())
                {
                    while (idr.Read())
                        analysisResults.Add(new AnalysisResultDTO(idr));
                }
                return analysisResults;
            }
        }



        public static List<AnalysisResultItemDTO> GetAnalysisResultItems(Int32 analysisResultID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM analysis_result_item_vw where analysis_result_id = @analysis_result_id order by information_type_id, word asc";
                cmd.Parameters.AddWithValue("analysis_result_id", analysisResultID);

                cnn.Open();

                List<AnalysisResultItemDTO> analysisResultItems = new List<AnalysisResultItemDTO>();
                using (IDataReader idr = cmd.ExecuteReader())
                {
                    while (idr.Read())
                        analysisResultItems.Add(new AnalysisResultItemDTO(idr));
                }
                return analysisResultItems;
            }
        }

        public static Nullable<Int32> GetInformationTypeIDByAnalysisResult(int analysisResultID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT information_type_id FROM [analysis_result_vw] where id = @analysis_result_id";
                cmd.Parameters.AddWithValue("@analysis_result_id", analysisResultID);

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

        public static void StoreWordHeaders(String text, int analysisInputID)
        {                        
            Dictionary<String, Int32> wordDensity = new Dictionary<String, Int32>();

            String[] words = text.Split(new char[2] { ' ', '\n' });
            int totalWords = words.Count();

            foreach (String word in words)
            {
                String cleanedWord = StringHelper.CleanUpWord(word);
                Int32 wordCount = 1;

                if (!String.IsNullOrEmpty(cleanedWord))
                {
                    if (wordDensity.ContainsKey(cleanedWord.ToLower()))
                    {
                        wordCount = wordDensity[cleanedWord.ToLower()];
                        wordCount++;
                    }

                    wordDensity[cleanedWord.ToLower()] = wordCount;
                }
            }


            foreach (String word in wordDensity.Keys)
            {
                InputDAC.StoreWordHeader(word, wordDensity[word], analysisInputID);
            }
        }
    }
}
