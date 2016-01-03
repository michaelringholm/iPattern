/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ipattern.dal;

import dk.ihedge.common.dal.DAC;
import java.sql.CallableStatement;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.Types;

/**
 *
 * @author mrs
 */
public class InputDAC
{
    public static Integer StoreTextInput(String inputText) throws Exception
    {
        //Connection conn = DAC.getDBConnection();
        Connection conn = DAC.createDBConnection();
        CallableStatement cs = null;

        try
        {
            // Stored procedure
            cs = conn.prepareCall("{call [input_pkg.store_text_input](?,?)}");
            
            cs.setString(1, inputText);

            /*cs.setDate(8, new java.sql.Date(brSeaFCL.getValidFrom().getTime()));
            cs.setDate(9, new java.sql.Date(brSeaFCL.getValidTo().getTime()));
            cs.setString(10, brSeaFCL.getCommodity());
            cs.setDate(11, new java.sql.Date(brSeaFCL.getUpdated().getTime()));*/

            // Register the type of the OUT parameter
            cs.registerOutParameter(2, Types.INTEGER);

            cs.execute();

            return cs.getInt(2); 
        }
        finally
        {
            if (cs != null)
              cs.close();
            if (conn != null)
              conn.close();
        }
    }

    public static void TruncateBRSeaFCL() throws Exception
    {
        Connection conn = DAC.createDBConnection();
        PreparedStatement ps = null;

        try
        {
            ps = conn.prepareStatement("truncate table brseafcl");
            ps.execute();
        }
        finally
        {
            if (ps != null)
                ps.close();
            if (conn != null)
                conn.close();
        }
    }

    public static void StoreWordHeader(String word, Integer wordCount, Integer analysisInputID) throws Exception
    {
        Connection conn = DAC.getDBConnection();
        CallableStatement cs = null;

        try
        {
            // Stored procedure
            cs = conn.prepareCall("{call [input_pkg.store_word_header](?,?,?)}");

            cs.setString(1, word);
            cs.setInt(2, wordCount);
            cs.setInt(3, analysisInputID);

            cs.execute();
        }
        finally
        {
            if (cs != null)
              cs.close();
        }
    }
}
