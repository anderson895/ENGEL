using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class EditProductForm : Form
    {
        // Connection string to your MySQL database
        string connectionString = "Server=127.0.0.1;Database=engel_deleon;Uid=root;Pwd=;";

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductDescription { get; set; }  // Added field for description

        public EditProductForm(int productId, string productName, string productDescription, decimal productPrice, int productStock)
        {
            InitializeComponent();

            // Initialize product data
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductQuantity = productStock;
            ProductDescription = productDescription;  // Initialize description

            // Populate fields with product data
            txtProductName.Text = productName;
            txtProductPrice.Text = productPrice.ToString();
            txtProductQuantity.Text = productStock.ToString();
            txtProductDescription.Text = productDescription;  // Populate description field
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(txtProductName.Text) || string.IsNullOrEmpty(txtProductPrice.Text) ||
                    string.IsNullOrEmpty(txtProductQuantity.Text) || string.IsNullOrEmpty(txtProductDescription.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                // Update product data in the database
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE product SET prod_name = @name, prod_price = @price, prod_quantity = @quantity, prod_description = @description WHERE prod_id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", ProductId);
                    cmd.Parameters.AddWithValue("@name", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@price", Convert.ToDecimal(txtProductPrice.Text));
                    cmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(txtProductQuantity.Text));
                    cmd.Parameters.AddWithValue("@description", txtProductDescription.Text);  // Include description

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product updated successfully.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update product.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        // Close the form without saving changes
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
