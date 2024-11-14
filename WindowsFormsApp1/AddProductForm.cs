using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class AddProductForm : Form
    {
        // MySQL connection string
        string connectionString = "Server=127.0.0.1;Database=engel_deleon;Uid=root;Pwd=;";

        public AddProductForm()
        {
            InitializeComponent();
        }

        // Handle Add Button Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (ValidateFields())
            {
                string name = txtProductName.Text;
                decimal price;
                int quantity;
                string description = txtProductDescription.Text;

                if (decimal.TryParse(txtProductPrice.Text, out price) && int.TryParse(txtProductQuantity.Text, out quantity))
                {
                    // Call the method to add product to the database
                    if (AddProductToDatabase(name, price, quantity, description))
                    {
                        MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();

                        // Set DialogResult to OK to indicate success
                        this.DialogResult = DialogResult.OK;
                        this.Close();  // Close the AddProductForm
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while adding the product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid price and quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // Handle Cancel Button Click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close the form without saving anything
            this.Close();
        }

        // Validate that all fields are filled
        private bool ValidateFields()
        {
            if (string.IsNullOrEmpty(txtProductName.Text))
            {
                MessageBox.Show("Please enter the product name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtProductPrice.Text))
            {
                MessageBox.Show("Please enter the product price.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtProductQuantity.Text))
            {
                MessageBox.Show("Please enter the product quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtProductDescription.Text))
            {
                MessageBox.Show("Please enter the product description.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // Method to add product to the database
        private bool AddProductToDatabase(string name, decimal price, int quantity, string description)
        {
            try
            {
                // Create a connection to the MySQL database
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    // SQL query to insert the product into the products table
                    string query = "INSERT INTO product (prod_name, prod_price, prod_stock, prod_description) " +
                                   "VALUES (@name, @price, @quantity, @description)";

                    // Create a command to execute the SQL query
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@quantity", quantity);
                        cmd.Parameters.AddWithValue("@description", description);

                        // Execute the query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // If rowsAffected is greater than 0, the insert was successful
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error (optional) and show the message
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Clear the input fields
        private void ClearFields()
        {
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtProductQuantity.Clear();
            txtProductDescription.Clear();
        }
    }
}
