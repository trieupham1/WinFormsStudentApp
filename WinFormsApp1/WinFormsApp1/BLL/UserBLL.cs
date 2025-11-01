using StudentManagementApp.DAL;

namespace StudentManagementApp.BLL
{
    public class UserBLL
    {
        private UserDAL userDAL = new UserDAL();

        public bool Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            return userDAL.ValidateUser(username, password);
        }
    }
}