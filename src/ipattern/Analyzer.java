/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ipattern;

import dk.ihedge.common.dal.DAC;
import dk.ihedge.common.util.FileUtils;
import ipattern.dal.InputDAC;
import ipattern.dal.ResultDAC;
import java.util.HashMap;

/**
 *
 * @author mrs
 */
public class Analyzer {
    public static AnalyzedResult AnalyzeText() throws Exception
    {
        System.setProperty("dbConnectionString", "jdbc:sqlserver://localhost;databaseName=ipattern;");
        System.setProperty("dbUser", "ipattern_user");
        System.setProperty("dbPassword", "demo");

        String text = FileUtils.readAllText("c:\\Subversion\\Sandbox\\iPattern\\data\\LoanApplication.txt", "ISO-8859-1");
        Integer analysisInputID = InputDAC.StoreTextInput(text);

        AnalyzedResult analyzedResult = new AnalyzedResult();

        HashMap<String, Integer> wordDensity = analyzedResult.getWordDensity();

        String[] words = text.split(" ");
        int totalWords = words.length;

        for(String word : words)
        {
            word = CleanUpWord(word);
            Integer wordCount = 1;
            if(wordDensity.containsKey(word.toLowerCase()))
            {
                wordCount = wordDensity.get(word.toLowerCase());
                wordCount++;
            }

            wordDensity.put(word.toLowerCase(), wordCount);
        }


        // Irrelevant words
        // Interesting words
        // Related words

        //analyzedResult.getWordDensity().put("hello", 32);
        //analyzedResult.getWordDensity().put("tomato", 16);

        for (String word : analyzedResult.getWordDensity().keySet())
        {
            InputDAC.StoreWordHeader(word, analyzedResult.getWordDensity().get(word), analysisInputID);
        }

        GenerateResult(analysisInputID);

        DAC.close();

        return analyzedResult;
    }

    private static String CleanUpWord(String word)
    {
        if(word.length() > 1)
        {
            if(word.endsWith(".") || word.endsWith(",") || word.endsWith(";"))
                return word.substring(0, word.length()-1);
            else if(word.startsWith(".") || word.startsWith(",") || word.endsWith(";"))
                return word.substring(1, word.length()-1);
            else
                return word;
        }
        else
            return word;
    }

    
    private static void GenerateResult(Integer analysisInputID) throws Exception
    {
        ResultDAC.GenerateResult(analysisInputID);
    }
}
