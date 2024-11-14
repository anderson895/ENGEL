using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get user inputs from textboxes
            string fullname = txtFullname.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string status = cmbStatus.SelectedItem.ToString(); // Assuming you have a ComboBox for status

            // Save the new user to the database
            try
            {
                using (MySqlConnection conn = new MySqlConnection("Server=127.0.0.1;Database=engel_deleon;Uid=root;Pwd=;"))
                {
                    conn.Open();
                    string query = "INSERT INTO users (user_fullname, user_username, user_password, user_status) " +
                                   "VALUES (@Fullname, @Username, @Password, @Status)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Fullname", fullname);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Status", status);

                    cmd.ExecuteNonQuery(); // Execute the query

                    MessageBox.Show("User added successfully!");
                    this.Close(); // Close the form after saving
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }

}
