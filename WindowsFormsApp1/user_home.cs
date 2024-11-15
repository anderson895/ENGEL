using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Import MySQL library

namespace WindowsFormsApp1
{
    public partial class user_home : Form
    {
        private int userId;

        // Constructor with user_id as a parameter
        private Timer refreshTimer;

        public user_home(int userId)
        {
            InitializeComponent();
            this.userId = userId;

            // Initialize and start the timer
            refreshTimer = new Timer();
            refreshTimer.Interval = 2000; // 5000 milliseconds = 5 seconds
            refreshTimer.Tick += RefreshCart;
            refreshTimer.Start();

            // Call method to load the user's full name
            LoadUserFullName();
            LoadProductsToCart();
        }
        private void RefreshCart(object sender, EventArgs e)
        {
            // Clear the existing controls in the FlowLayoutPanel
            flowLayoutPanel1.Controls.Clear();

            // Load the products again to reflect any changes
            LoadProductsToCart();
        }


        private void LoadUserFullName()
        {
            try
            {
                using (var conn = new MySqlConnection("Server=localhost;Database=engel_deleon;Uid=root;Pwd=;"))
                {
                    conn.Open();
                    var query = "SELECT user_fullname FROM users WHERE user_id=@userId";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                            user_fullname.Text = result.ToString();
                        else
                            MessageBox.Show("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        // Load products and add them to the cart
        private void LoadProductsToCart()
        {
            List<Product> products = GetProductsFromDatabase();

            foreach (var product in products)
            {
                // Create a new panel for each product
                Panel productPanel = new Panel
                {
                    Size = new Size(200, 300),
                    BackColor = Color.White,
                    Margin = new Padding(10)
                };

                // Product Name
                Label prodName = new Label
                {
                    Text = product.ProdName,
                    Location = new Point(12, 137),
                    AutoSize = true,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };

                // Product Description
                Label prodDescription = new Label
                {
                    Text = product.ProdDescription,
                    Location = new Point(12, 162),
                    AutoSize = true,
                    Font = new Font("Arial", 8)
                };

                // Product Price
                Label prodPrice = new Label
                {
                    Text = $"Price: PHP {product.ProdPrice:F2}",
                    Location = new Point(15, 189),
                    AutoSize = true,
                    Font = new Font("Arial", 9, FontStyle.Regular)
                };

                // Product Stock
                Label prodStock = new Label
                {
                    Text = $"Stock: {product.ProdStock}",
                    Location = new Point(15, 210),
                    AutoSize = true,
                    Font = new Font("Arial", 9, FontStyle.Regular)
                };

                // Purchase Button
                Button btnPurchase = new Button
                {
                    Text = "Purchase",
                    Location = new Point(15, 240),
                    Size = new Size(75, 30),
                    BackColor = Color.LightGreen
                };

                btnPurchase.Click += (sender, args) =>
                {
                    // Create and display a custom input dialog for quantity
                    using (Form inputDialog = new Form())
                    {
                        inputDialog.Width = 300;
                        inputDialog.Height = 200;
                        inputDialog.Text = "Purchase Quantity";

                        Label lblPrompt = new Label
                        {
                            Text = $"Enter quantity for {product.ProdName}:",
                            AutoSize = true,
                            Location = new Point(10, 20)
                        };

                        TextBox txtQuantity = new TextBox
                        {
                            Location = new Point(10, 50),
                            Width = 260
                        };

                        Button btnOk = new Button
                        {
                            Text = "OK",
                            DialogResult = DialogResult.OK,
                            Location = new Point(10, 90)
                        };

                        Button btnCancel = new Button
                        {
                            Text = "Cancel",
                            DialogResult = DialogResult.Cancel,
                            Location = new Point(90, 90)
                        };

                        inputDialog.Controls.Add(lblPrompt);
                        inputDialog.Controls.Add(txtQuantity);
                        inputDialog.Controls.Add(btnOk);
                        inputDialog.Controls.Add(btnCancel);
                        inputDialog.AcceptButton = btnOk;
                        inputDialog.CancelButton = btnCancel;

                        if (inputDialog.ShowDialog() == DialogResult.OK)
                        {
                            if (int.TryParse(txtQuantity.Text, out int quantity) && quantity > 0)
                            {
                                if (quantity <= product.ProdStock)
                                {
                                    DialogResult result = MessageBox.Show(
                                        $"You are about to purchase {quantity} x {product.ProdName} for a total of PHP {(quantity * product.ProdPrice):F2}.\n\nProceed?",
                                        "Confirm Purchase",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

                                    if (result == DialogResult.Yes)
                                    {
                                        // Deduct stock and update database
                                        int newStock = product.ProdStock - quantity;
                                        UpdateProductStock(product.ProdId, newStock);

                                        // Update label dynamically
                                        product.ProdStock = newStock;
                                        prodStock.Text = $"Stock: {product.ProdStock}";  // Update stock label in real time

                                        MessageBox.Show("Purchase successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Insufficient stock available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid quantity entered. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                };

                productPanel.Controls.Add(prodName);
                productPanel.Controls.Add(prodDescription);
                productPanel.Controls.Add(prodPrice);
                productPanel.Controls.Add(prodStock);
                productPanel.Controls.Add(btnPurchase);

                // Add panel to the FlowLayoutPanel
                flowLayoutPanel1.Controls.Add(productPanel);
            }
        }




        private void UpdateProductStock(int productId, int newStock)
        {
            string connectionString = "Server=localhost;Database=engel_deleon;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE product SET prod_stock = @NewStock WHERE prod_id  = @ProductId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewStock", newStock);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    command.ExecuteNonQuery();
                }
            }
        }


        // Fetch products from the database
        private List<Product> GetProductsFromDatabase()
        {
            List<Product> products = new List<Product>();
            string connectionString = "Server=localhost;Database=engel_deleon;Uid=root;Pwd=;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM product";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProdId = reader.GetInt32(0),
                                ProdName = reader.GetString(1),
                                ProdDescription = reader.GetString(2),
                                ProdPrice = reader.GetDecimal(3),
                                ProdStock = reader.GetInt32(4),
                            });
                        }
                    }
                }
            }

            return products;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Form editForm = new Form();
            editForm.Text = "Edit User Details";
            editForm.Size = new Size(300, 250); 

            string fullName = string.Empty;
            string username = string.Empty;
            string currentPassword = string.Empty;


            using (var conn = new MySqlConnection("Server=localhost;Database=engel_deleon;Uid=root;Pwd=;"))
            {
                conn.Open();
                var query = "SELECT user_fullname, user_username, user_password FROM users WHERE user_id=@userId"; // Select fullname, username, and password
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fullName = reader.GetString("user_fullname");
                            username = reader.GetString("user_username");
                            currentPassword = reader.GetString("user_password"); // Store current password
                        }
                        else
                        {
                            MessageBox.Show("User not found.");
                            return;
                        }
                    }
                }
            }

            // Create controls to edit user details
            Label lblFullName = new Label { Text = "Full Name:", Location = new Point(10, 20), AutoSize = true };
            TextBox txtFullName = new TextBox { Text = fullName, Location = new Point(100, 20), Width = 150 };

            Label lblUsername = new Label { Text = "Username:", Location = new Point(10, 60), AutoSize = true };
            TextBox txtUsername = new TextBox { Text = username, Location = new Point(100, 60), Width = 150 };

            Label lblPassword = new Label { Text = "New Password:", Location = new Point(10, 100), AutoSize = true };
            TextBox txtPassword = new TextBox { Location = new Point(100, 100), Width = 150, PasswordChar = '*' };

            Button btnSave = new Button { Text = "Save", Location = new Point(100, 140) };

            btnSave.Click += (s, args) =>
            {
                // Validate input
                if (string.IsNullOrEmpty(txtFullName.Text) || string.IsNullOrEmpty(txtUsername.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Determine if password needs to be updated
                string newPassword = string.IsNullOrEmpty(txtPassword.Text) ? currentPassword : txtPassword.Text; // Use current password if no new password is provided

                // Update user details in the database
                using (var conn = new MySqlConnection("Server=localhost;Database=engel_deleon;Uid=root;Pwd=;"))
                {
                    conn.Open();
                    string updateQuery = "UPDATE users SET user_fullname = @FullName, user_username = @Username, user_password = @Password WHERE user_id = @UserId";
                    using (var cmd = new MySqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@Password", newPassword); // Save the new password (or current password if none entered)
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Your details have been updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                editForm.Close(); // Close the edit form after saving
            };

            // Add controls to the edit form
            editForm.Controls.Add(lblFullName);
            editForm.Controls.Add(txtFullName);
            editForm.Controls.Add(lblUsername);
            editForm.Controls.Add(txtUsername);
            editForm.Controls.Add(lblPassword);
            editForm.Controls.Add(txtPassword);
            editForm.Controls.Add(btnSave);

            // Show the edit form
            editForm.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
           

            // Optionally, show a confirmation message
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                this.Close();
                user_login loginForm = new user_login();
                loginForm.Show();
            }
        }

    }
}
