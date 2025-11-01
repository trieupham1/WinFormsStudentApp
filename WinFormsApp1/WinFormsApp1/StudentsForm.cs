using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class StudentsForm : Form
    {
        public StudentsForm()
        {
            InitializeComponent();
            LoadStudents();
        }

        private void LoadStudents()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tblStudents", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Example: Add logic (adjust columns as needed)
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO tblStudents (Name, Age) VALUES (@Name, @Age)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Name", "New Student"); // Replace with actual input
                cmd.Parameters.AddWithValue("@Age", 18); // Replace with actual input
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            LoadStudents();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Example: Update logic (adjust columns as needed)
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["StudentID"].Value);
                string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE tblStudents SET Name=@Name, Age=@Age WHERE StudentID=@StudentID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Name", "Updated Name"); // Replace with actual input
                    cmd.Parameters.AddWithValue("@Age", 20); // Replace with actual input
                    cmd.Parameters.AddWithValue("@StudentID", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadStudents();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Example: Delete logic
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["StudentID"].Value);
                string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM tblStudents WHERE StudentID=@StudentID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@StudentID", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadStudents();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }
    }
}
