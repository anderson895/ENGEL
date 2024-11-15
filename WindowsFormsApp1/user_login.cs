using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class user_login : Form
    {
        public user_login()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var landingform = new landingForm();
            this.Hide();
            landingform.Show();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Check if the username or password is empty
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Connection string for MySQL database
            string connString = "Server=localhost;Database=engel_deleon;Uid=root;Pwd=;";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    await conn.OpenAsync(); // Use async method for non-blocking operation

                    // Prepare the SQL query to check the username and password
                    string query = "SELECT user_id FROM users WHERE user_username=@username AND user_password=@password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    object result = await cmd.ExecuteScalarAsync(); // Execute query and get the user_id

                    // Check if any matching record was found
                    if (result != null)
                    {
                        int userId = Convert.ToInt32(result);
                        MessageBox.Show("Login Successful!");

                        // Proceed to the next form
                        user_home dashboard = new user_home(userId);
                        this.Hide();
                        dashboard.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password. Please try again.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
