using System;
using System.Configuration;
using System.Data.SqlClient;

namespace StudentManagementApp.DAL
{
    public class UserDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;

        public bool ValidateUser(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM tblUsers WHERE Username=@Username AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                connection.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count == 1;
            }
        }
    }
}