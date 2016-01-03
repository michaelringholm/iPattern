using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DTL
{
    public class WordHeaderDTO
    {
        public WordHeaderDTO(String word, Int32 wordCount)
        {
            Word = word;
            WordCount = wordCount;
        }

        public WordHeaderDTO(IDataReader reader)
        {
            Word = (String)reader["word"];
            WordCount = (Int32)reader["word_count"];
        }

        public String Word { get; set; }
        public Int32 WordCount { get; set; }
        //public Int32 AnalysisRunID { get; set; }
    }
}
