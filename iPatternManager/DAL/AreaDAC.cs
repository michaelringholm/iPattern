using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DTL;

namespace DAL
{
    public static class AreaDAC
    {
        public static AreaDTO Restore(string areaTitle)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM area_vw where title = @areaTitle";
                cmd.Parameters.AddWithValue("areaTitle", areaTitle);

                cnn.Open();

                using (IDataReader idr = cmd.ExecuteReader())
                {
                    if (idr.Read())
                        return new AreaDTO(idr);
                    else
                        return null;
                }
            }
        }

        public static List<AreaDTO> GetAreas( object userGUId)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT * FROM [area_with_users_vw] 
                                where user_id = @user_id
                                order by title asc";
                cmd.Parameters.AddWithValue("user_id", userGUId);

                List<AreaDTO> areas = new List<AreaDTO>();
                if (cnn.State != ConnectionState.Open) cnn.Open();
                using (IDataReader idr = cmd.ExecuteReader())
                {
                    while (idr.Read())
                        areas.Add(new AreaDTO(idr));
                }
                return areas;
            }
        }

        public static List<AreaDTO> GetAreasByUserCompany( object userGUID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT * FROM [area_by_company_user_vw] 
                                where user_id = @user_id
                                order by title asc";
                cmd.Parameters.AddWithValue("user_id", userGUID);

                List<AreaDTO> areas = new List<AreaDTO>();
                if (cnn.State != ConnectionState.Open) cnn.Open();
                using (IDataReader idr = cmd.ExecuteReader())
                {
                    while (idr.Read())
                        areas.Add(new AreaDTO(idr));
                }
                return areas;
            }
        }

        public static Nullable<int> GetAreaIDByAddress(string ownerAddress, string from)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[input_pkg.get_area_by_address]";
                SqlParameter idParam = cmd.Parameters.Add("area_id", SqlDbType.Int);
                idParam.Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("mailbox_user", ownerAddress);
                cmd.Parameters.AddWithValue("from", from);

                cnn.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(idParam.Value);

            }
        }


        public static String GetCompanyTitleIDByID(int companyID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT company_title FROM [company_vw] where id = @company_ID";
                cmd.Parameters.AddWithValue("@company_ID", companyID);

                cnn.Open();
                return Convert.ToString(cmd.ExecuteScalar());
            }
        }

        public static Int32 StoreArea(AreaDTO area)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[area_pkg.store]";

                SqlParameter idParam = cmd.Parameters.Add("id", SqlDbType.Int);
                idParam.Value = (area.ID.HasValue) ? (object)area.ID.Value : DBNull.Value;
                idParam.Direction = ParameterDirection.InputOutput;

                cmd.Parameters.AddWithValue("@title", area.Title);
                cmd.Parameters.AddWithValue("@company_id", area.CompanyID);
                cmd.Parameters.AddWithValue("@filter_on_unknown", area.FilterOnUnknown);
                
                cnn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToInt32(idParam.Value);
            }
        }

        public static void AddAreaRelation(AreaDTO area, UserDTO user)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[area_pkg.store_user_relation]";

                cmd.Parameters.AddWithValue("@area_id", area.ID);
                cmd.Parameters.AddWithValue("@user_id", user.ID);
                cmd.Parameters.AddWithValue("@filter_on_unknown", area.FilterOnUnknown);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void Delete(int areaID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[area_pkg.delete]";

                cmd.Parameters.AddWithValue("@id", areaID);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
