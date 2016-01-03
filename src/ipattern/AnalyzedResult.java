/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ipattern;

import java.util.HashMap;

/**
 *
 * @author mrs
 */
public class AnalyzedResult
{
    private HashMap<String, Integer> _wordDensity = new HashMap<String, Integer>();

    /**
     * @return the _wordDensity
     */
    public HashMap<String, Integer> getWordDensity() {
        return _wordDensity;
    }

    /**
     * @param wordDensity the _wordDensity to set
     */
    public void setWordDensity(HashMap<String, Integer> wordDensity) {
        this._wordDensity = wordDensity;
    }
    
}
