using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace StudentManagementApp
{
    public partial class DepartmentsForm : Form
    {
        public DepartmentsForm()
        {
            InitializeComponent();
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tblDepartments", connection);
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
                string query = "INSERT INTO tblDepartments (DepartmentName) VALUES (@DepartmentName)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@DepartmentName", "New Department"); // Replace with actual input
                connection.Open();
                cmd.ExecuteNonQuery();
            }
            LoadDepartments();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["DepartmentID"].Value);
                string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE tblDepartments SET DepartmentName=@DepartmentName WHERE DepartmentID=@DepartmentID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@DepartmentName", "Updated Department"); // Replace with actual input
                    cmd.Parameters.AddWithValue("@DepartmentID", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadDepartments();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["DepartmentID"].Value);
                string connectionString = ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM tblDepartments WHERE DepartmentID=@DepartmentID";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@DepartmentID", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadDepartments();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDepartments();
        }
    }
}
