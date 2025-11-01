using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class CoursesForm : Form
    {
        public CoursesForm()
        {
            InitializeComponent();
            LoadCourses();
        }

        private void LoadCourses()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tblCourses", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO tblCourses (CourseName, Credits, DepartmentID, InstructorID) VALUES (@CourseName, @Credits, @DepartmentID, @InstructorID)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@CourseName", "New Course"); // Replace with actual input
                cmd.Parameters.AddWithValue("@Credits", 3); // Replace with actual input
                cmd.Parameters.AddWithValue("@DepartmentID", 1); // Replace with actual input
                cmd.Parameters.AddWithValue("@InstructorID", 1); // Replace with actual input
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            LoadCourses();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["CourseID"].Value);
                string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE tblCourses SET CourseName=@CourseName, Credits=@Credits, DepartmentID=@DepartmentID, InstructorID=@InstructorID WHERE CourseID=@CourseID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@CourseName", "Updated Course"); // Replace with actual input
                    cmd.Parameters.AddWithValue("@Credits", 4); // Replace with actual input
                    cmd.Parameters.AddWithValue("@DepartmentID", 2); // Replace with actual input
                    cmd.Parameters.AddWithValue("@InstructorID", 2); // Replace with actual input
                    cmd.Parameters.AddWithValue("@CourseID", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadCourses();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["CourseID"].Value);
                string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM tblCourses WHERE CourseID=@CourseID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@CourseID", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadCourses();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCourses();
        }
    }
}
