using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DTL;

namespace DAL
{
    public class UserDAC
    {
        public static String Store(UserDTO user)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[user_pkg.store]";

                SqlParameter idParam = cmd.Parameters.Add("user_id", SqlDbType.UniqueIdentifier);
                
                idParam.Direction = ParameterDirection.Input;
                if (user.ID == null)
                {
                    idParam.Value = DBNull.Value;
                }
                else
                    idParam.Value = new Guid(user.ID);

                cmd.Parameters.AddWithValue("@user_name", user.Email);
                //cmd.Parameters.AddWithValue("@user_id", user.ID);
                cmd.Parameters.AddWithValue("@company_id", user.Company_ID);
                cmd.Parameters.AddWithValue("@default_area_id", user.DefaultAreaID);

                cnn.Open();
                cmd.ExecuteNonQuery();

                return Convert.ToString(idParam.Value);
            }
        }

        public static void AddAreaRelation(String userID, int areaID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[user_pkg.add_area_relation]";

                SqlParameter idParam = cmd.Parameters.Add("user_id", SqlDbType.UniqueIdentifier);
                idParam.Direction = ParameterDirection.Input;
                idParam.Value = new Guid(userID);

                cmd.Parameters.AddWithValue("area_id", areaID);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void RemoveAreaRelations(String userID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Delete from area_users_relation where user_id = @user_id";

                SqlParameter idParam = cmd.Parameters.Add("user_id", SqlDbType.UniqueIdentifier);
                idParam.Direction = ParameterDirection.Input;
                idParam.Value = new Guid(userID);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static void AddUserRole(String userName, String roleName)
        {
            String applicationName = "/";
            int currentTimeUtc = 8;
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[aspnet_UsersInRoles_AddUsersToRoles]";

                cmd.Parameters.AddWithValue("ApplicationName", applicationName);
                cmd.Parameters.AddWithValue("UserNames", userName);
                cmd.Parameters.AddWithValue("RoleNames", roleName);
                cmd.Parameters.AddWithValue("CurrentTimeUtc", currentTimeUtc);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static void RemoveUserRole(String userName, String roleName)
        {
            String applicationName = "/";
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[aspnet_UsersInRoles_RemoveUsersFromRoles]";

                cmd.Parameters.AddWithValue("ApplicationName", applicationName);
                cmd.Parameters.AddWithValue("UserNames", userName);
                cmd.Parameters.AddWithValue("RoleNames", roleName);

                cnn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static UserDTO Restore(string id)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM user_vw where user_id = @id";
                SqlParameter idParam = cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier);
                idParam.Direction = ParameterDirection.Input;
               
                idParam.Value = new Guid(id);

                cnn.Open();

                using (IDataReader idr = cmd.ExecuteReader())
                {
                    if (idr.Read())
                        return new UserDTO(idr);
                    else
                        return null;
                }
            }
        }

        public static UserDTO RestoreByName(string userName)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM user_vw where user_name = @userName";
                cmd.Parameters.AddWithValue("userName", userName);

                cnn.Open();

                using (IDataReader idr = cmd.ExecuteReader())
                {
                    if (idr.Read())
                        return new UserDTO(idr);
                    else
                        return null;
                }
            }
        }

        public static List<UserDTO> GetUsers(int companyID)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT * FROM [user_vw] where company_id = @company_id order by user_name";
                cmd.Parameters.AddWithValue("company_id", companyID);

                List<UserDTO> users = new List<UserDTO>();
                if (cnn.State != ConnectionState.Open) cnn.Open();
                using (IDataReader idr = cmd.ExecuteReader())
                {
                    while (idr.Read())
                        users.Add(new UserDTO(idr));
                }
                return users;
            }
        }

        public static Boolean IsUserNameFree(string userName)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT UserId FROM aspnet_Users where UserName = @userName";
                cmd.Parameters.AddWithValue("userName", userName);

                cnn.Open();

                using (IDataReader idr = cmd.ExecuteReader())
                {
                    if (idr.Read())
                        return false;
                    else
                        return true;
                }
            }
        }


        public static List<AreaDTO> GetAreas(object userGUId)
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


        public static Nullable<int> GetDefaultAreaId(object userGUId)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT id FROM [default_area_vw] 
                                   where user_id = @user_id";
                cmd.Parameters.AddWithValue("user_id", userGUId);

                if (cnn.State != ConnectionState.Open) cnn.Open();
                using (IDataReader idr = cmd.ExecuteReader())
                {
                    if (idr.Read())
                        return Convert.ToInt32(idr["id"]);
                    else
                        return null;
                }

            }

        }

        public static Nullable<int> GetAreaIDByMetaValues(string ownerAddress, string from)
        {
            using (SqlConnection cnn = WordDAC.NewConnection)
            using (SqlCommand cmd = cnn.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[get_area_by_meta_values]";
                cmd.Parameters.AddWithValue("ownerAddress", ownerAddress);
                cmd.Parameters.AddWithValue("from", from);

                if (cnn.State != ConnectionState.Open) cnn.Open();
                using (IDataReader idr = cmd.ExecuteReader())
                {
                    if (idr.Read())
                        return Convert.ToInt32(idr["id"]);
                    else
                        return null;
                }

            }
        }
    }
}
