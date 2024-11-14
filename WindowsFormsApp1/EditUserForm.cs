using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class EditUserForm : Form
    {
        private int userId;
        private string connectionString = "Server=127.0.0.1;Database=engel_deleon;Uid=root;Pwd=;";

        public EditUserForm(int userId, string fullname, string username, string password)
        {
            InitializeComponent();
            this.userId = userId;

            // Populate the fields with the current user data
            txtFullname.Text = fullname;
            txtUsername.Text = username;
            txtPassword.Text = password;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string fullname = txtFullname.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Update the user data in the database
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE users SET user_fullname = @fullname, user_username = @username, user_password = @password WHERE user_id = @userId";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("User information updated successfully!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
