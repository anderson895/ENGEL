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
            this.navPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Table_user)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(800, 50);
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
            this.navPanel.Size = new System.Drawing.Size(800, 68);
            this.navPanel.TabIndex = 1;
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(205, 18);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(147, 30);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Manage User";
            this.btnHome.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(373, 18);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(131, 30);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Inventory";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(531, 18);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 30);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Logout";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Table_user
            // 
            this.Table_user.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Table_user.Location = new System.Drawing.Point(0, 124);
            this.Table_user.Name = "Table_user";
            this.Table_user.Size = new System.Drawing.Size(788, 314);
            this.Table_user.TabIndex = 2;
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Table_user);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.navPanel);
            this.Name = "home";
            this.Text = "Home";
            this.navPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Table_user)).EndInit();
            this.ResumeLayout(false);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private System.Windows.Forms.DataGridView Table_user;
    }
}
