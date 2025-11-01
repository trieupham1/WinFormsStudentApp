using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class InstructorsForm : Form
    {
        public InstructorsForm()
        {
            InitializeComponent();
            LoadInstructors();
        }

        private void LoadInstructors()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tblInstructors", connection);
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
                string query = "INSERT INTO tblInstructors (InstructorName, DepartmentID) VALUES (@InstructorName, @DepartmentID)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@InstructorName", "New Instructor"); // Replace with actual input
                cmd.Parameters.AddWithValue("@DepartmentID", 1); // Replace with actual input
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            LoadInstructors();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["InstructorID"].Value);
                string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE tblInstructors SET InstructorName=@InstructorName, DepartmentID=@DepartmentID WHERE InstructorID=@InstructorID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@InstructorName", "Updated Instructor"); 
                    cmd.Parameters.AddWithValue("@DepartmentID", 2); 
                    cmd.Parameters.AddWithValue("@InstructorID", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadInstructors();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["InstructorID"].Value);
                string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM tblInstructors WHERE InstructorID=@InstructorID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@InstructorID", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadInstructors();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInstructors();
        }
    }
}
