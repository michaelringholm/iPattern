using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public static class ErrorDAC
    {
        public static void LogError(String program, String className, String methodName, Exception ex)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[error_pkg.log_error]";

                cmd.Parameters.AddWithValue("@program", program);
                cmd.Parameters.AddWithValue("@class_name", className);
                cmd.Parameters.AddWithValue("@method_name", methodName);
                cmd.Parameters.AddWithValue("@message", ex.Message);
                cmd.Parameters.AddWithValue("@stack_trace", ex.StackTrace);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
