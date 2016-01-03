/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ipattern.dal;

import dk.ihedge.common.dal.DAC;
import java.sql.CallableStatement;
import java.sql.Connection;

/**
 *
 * @author mrs
 */
public class ResultDAC
{
    
    public static void GenerateResult(Integer analysisInputID) throws Exception
    {
        Connection conn = DAC.getDBConnection();
        CallableStatement cs = null;

        try
        {
            // Stored procedure
            cs = conn.prepareCall("{call [result_pkg.generate_result](?)}");

            cs.setInt(1, analysisInputID);

            cs.execute();
        }
        finally
        {
            if (cs != null)
              cs.close();
        }
    }
}
