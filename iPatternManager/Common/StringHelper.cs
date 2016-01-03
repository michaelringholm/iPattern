using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class StringHelper
    {
        public static String CleanUpWord(String word)
        {
            if (word.Length > 0)
            {
                if(word.EndsWith("\r"))
                    word = word.Substring(0, word.Length - 1);

                if (word.EndsWith(".") || word.EndsWith(",") || word.EndsWith(";") || word.EndsWith("!"))
                    return word.Substring(0, word.Length - 1).Trim();
                else if (word.StartsWith(".") || word.StartsWith(",") || word.EndsWith(";"))
                    return word.Substring(1, word.Length - 1).Trim();
                else
                    return word.Trim();
            }
            else
                return word;
        }
    }
}
