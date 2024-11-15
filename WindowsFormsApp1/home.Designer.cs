using System;

namespace WindowsFormsApp1
{
    partial class home
    {
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel navPanel;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnExit;

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.navPanel = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.Table_user = new System.Windows.Forms.DataGridView();
            this.Table_product = new System.Windows.Forms.DataGridView();
            this.btnAddUser = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.navPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Table_user)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table_product)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(-6, -3);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1530, 62);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Manager User";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // navPanel
            // 
            this.navPanel.BackColor = System.Drawing.Color.LightGray;
            this.navPanel.Controls.Add(this.btnHome);
            this.navPanel.Controls.Add(this.btnSettings);
            this.navPanel.Controls.Add(this.btnExit);
            this.navPanel.Location = new System.Drawing.Point(0, 50);
            this.navPanel.Name = "navPanel";
            this.navPanel.Size = new System.Drawing.Size(1524, 68);
            this.navPanel.TabIndex = 1;
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(552, 26);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(147, 30);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Manage User";
            this.btnHome.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(720, 26);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(131, 30);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Inventory";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(878, 26);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(98, 30);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Logout";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Table_user
            // 
            this.Table_user.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Table_user.Location = new System.Drawing.Point(12, 185);
            this.Table_user.Name = "Table_user";
            this.Table_user.Size = new System.Drawing.Size(754, 631);
            this.Table_user.TabIndex = 2;
            this.Table_user.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Table_user_CellContentClick);
            // 
            // Table_product
            // 
            this.Table_product.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Table_product.Location = new System.Drawing.Point(772, 185);
            this.Table_product.Name = "Table_product";
            this.Table_product.Size = new System.Drawing.Size(752, 631);
            this.Table_product.TabIndex = 3;
            this.Table_product.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Table_product_CellContentClick);
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(13, 156);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(75, 23);
            this.btnAddUser.TabIndex = 4;
            this.btnAddUser.Text = "Add user";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(772, 156);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Add Product";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1545, 828);
            this.Controls.Add(this.navPanel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.Table_product);
            this.Controls.Add(this.Table_user);
            this.Controls.Add(this.lblHeader);
            this.Name = "home";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.home_Load);
            this.navPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Table_user)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table_product)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var landingform = new landingForm();
            this.Hide();
            landingform.Show();
        }

        private System.Windows.Forms.DataGridView Table_user;
        private System.Windows.Forms.DataGridView Table_product;
        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button button1;
    }
}
