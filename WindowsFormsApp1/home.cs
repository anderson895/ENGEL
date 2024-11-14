using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class home : Form
    {
        // Connection string to your MySQL database
        string connectionString = "Server=127.0.0.1;Database=engel_deleon;Uid=root;Pwd=;";

        public home()
        {
            InitializeComponent();
        }

        private void home_Load(object sender, EventArgs e)
        {
            // Call the method to load user data when the form loads
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                // Create a connection to the database
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // Define the query to fetch data from the users table
                    string query = "SELECT user_id, user_fullname, user_username, user_password FROM users";

                    // Create a DataAdapter to fill a DataTable with the query results
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    Table_user.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        // Other event handlers like btnSettings_Click can go here
        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Your settings button functionality here
        }
    }
}
