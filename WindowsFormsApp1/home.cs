using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class home : Form
    {
        string connectionString = "Server=127.0.0.1;Database=engel_deleon;Uid=root;Pwd=;";

        public home()
        {
            InitializeComponent();
        }

        private void home_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            LoadUserData();
            LoadProductData();
        }

        private void LoadUserData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM users";
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    Table_user.DataSource = dataTable;
                    CustomizeUserTableHeaders();
                    AddEditAndDeleteButtonToUserTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user data: " + ex.Message);
            }
        }

        private void CustomizeUserTableHeaders()
        {
            Table_user.Columns["user_id"].HeaderText = "User ID";
            Table_user.Columns["user_fullname"].HeaderText = "Full Name";
            Table_user.Columns["user_username"].HeaderText = "Username";
            Table_user.Columns["user_password"].HeaderText = "Password";
            Table_user.Columns["user_status"].HeaderText = "Status";
        }

        // Add "Edit" button column to the user table
        private void AddEditAndDeleteButtonToUserTable()
        {
            if (Table_user.Columns["btnEdit"] == null)
            {
                DataGridViewButtonColumn btnEditColumn = new DataGridViewButtonColumn
                {
                    Name = "btnEdit",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true
                };
                Table_user.Columns.Add(btnEditColumn);
                Table_user.Columns["btnEdit"].DisplayIndex = Table_user.Columns.Count - 1;
            }

            if (Table_user.Columns["btnDelete"] == null)
            {
                DataGridViewButtonColumn btnDeleteColumn = new DataGridViewButtonColumn
                {
                    Name = "btnDelete",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true
                };
                Table_user.Columns.Add(btnDeleteColumn);
                Table_user.Columns["btnDelete"].DisplayIndex = Table_user.Columns.Count - 1;
            }
        }


        private void LoadProductData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM product";
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    Table_product.DataSource = dataTable;
                    CustomizeProductTableHeaders();
                    AddEditAndDeleteButtonToProductTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading product data: " + ex.Message);
            }
        }

        // Customize headers for product table
        private void CustomizeProductTableHeaders()
        {
            Table_product.Columns["prod_id"].HeaderText = "Product ID";
            Table_product.Columns["prod_name"].HeaderText = "Product Name";
            Table_product.Columns["prod_description"].HeaderText = "Description";
            Table_product.Columns["prod_price"].HeaderText = "Price";
            Table_product.Columns["prod_stock"].HeaderText = "Stock";
        }

        // Add "Edit" button column to the product table
        private void AddEditAndDeleteButtonToProductTable()
        {
            if (Table_product.Columns["btnEdit"] == null)
            {
                DataGridViewButtonColumn btnEditColumn = new DataGridViewButtonColumn
                {
                    Name = "btnEdit",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true
                };
                Table_product.Columns.Add(btnEditColumn);
                Table_product.Columns["btnEdit"].DisplayIndex = Table_product.Columns.Count - 1;
            }

            if (Table_product.Columns["btnDelete"] == null)
            {
                DataGridViewButtonColumn btnDeleteColumn = new DataGridViewButtonColumn
                {
                    Name = "btnDelete",
                    Text = "Delete",
                    UseColumnTextForButtonValue = true
                };
                Table_product.Columns.Add(btnDeleteColumn);
                Table_product.Columns["btnDelete"].DisplayIndex = Table_product.Columns.Count - 1;
            }
        }



        // Handle "Edit" button click inside the user table
        private void Table_user_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is on the Edit button column
            if (e.ColumnIndex == Table_user.Columns["btnEdit"].Index)
            {
                // Ensure that the clicked row is valid and not -1 (which means no row was selected)
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = Table_user.Rows[e.RowIndex]; // Get the clicked row
                    int userId = Convert.ToInt32(selectedRow.Cells["user_id"].Value);
                    string userFullname = selectedRow.Cells["user_fullname"].Value.ToString();
                    string userUsername = selectedRow.Cells["user_username"].Value.ToString();
                    string userPassword = selectedRow.Cells["user_password"].Value.ToString();

                    // Open the EditUserForm with the selected user's details
                    EditUserForm editForm = new EditUserForm(userId, userFullname, userUsername, userPassword);
                    editForm.ShowDialog();

                    // Reload user data after editing
                    LoadUserData();
                }
                else
                {
                    // If no row is selected, show an appropriate message
                    MessageBox.Show("Please select a user to edit.");
                }
            }
            else if (e.ColumnIndex == Table_user.Columns["btnDelete"].Index)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = Table_user.Rows[e.RowIndex];
                    int userId = Convert.ToInt32(selectedRow.Cells["user_id"].Value);

                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this user?", "Delete User", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DeleteUser(userId);
                    }
                }
            }
        }

        private void DeleteUser(int userId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM users WHERE user_id = @userId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User deleted successfully.");
                    LoadUserData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message);
            }
        }

        // Handle "Edit" button click inside the product table
        private void Table_product_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the 'Edit' button column and if a valid row is selected
            if (e.ColumnIndex == Table_product.Columns["btnEdit"].Index)
            {
                // Ensure that the clicked row is valid and not -1 (which means no row was selected)
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = Table_product.Rows[e.RowIndex]; // Get the clicked row
                    int productId = Convert.ToInt32(selectedRow.Cells["prod_id"].Value);
                    string productName = selectedRow.Cells["prod_name"].Value.ToString();
                    string productDescription = selectedRow.Cells["prod_description"].Value.ToString();
                    decimal productPrice = Convert.ToDecimal(selectedRow.Cells["prod_price"].Value);
                    int productStock = Convert.ToInt32(selectedRow.Cells["prod_stock"].Value);

                    // Open the EditProductForm with the selected product details
                    EditProductForm editProductForm = new EditProductForm(productId, productName, productDescription, productPrice, productStock);
                    editProductForm.ShowDialog();

                    // Reload product data after editing
                    LoadProductData();
                }
                else
                {
                    MessageBox.Show("Please select a product to edit.");
                }
            }
            else if (e.ColumnIndex == Table_product.Columns["btnDelete"].Index)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = Table_product.Rows[e.RowIndex];
                    int productId = Convert.ToInt32(selectedRow.Cells["prod_id"].Value);

                    DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DeleteProduct(productId);
                    }
                }
            }
          }


        private void DeleteProduct(int productId)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM product WHERE prod_id = @productId";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@productId", productId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted successfully.");
                    LoadProductData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message);
            }
        }


        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddUserForm addUserForm = new AddUserForm();
            addUserForm.ShowDialog();

            LoadUserData();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            AddProductForm addProductForm = new AddProductForm();
            addProductForm.ShowDialog();

            LoadProductData();
        }
    }
}
