using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Import MySQL library

namespace WindowsFormsApp1
{
    public partial class user_home : Form
    {
        public user_home()
        {
            InitializeComponent();
            LoadProductsToCart();
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
                    // Create and display a custom input dialog
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
                                        UpdateProductStock(product.ProdId, product.ProdStock - quantity);

                                        // Update label dynamically
                                        product.ProdStock -= quantity;
                                        prodStock.Text = $"Stock: {product.ProdStock}";

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
    }
}
