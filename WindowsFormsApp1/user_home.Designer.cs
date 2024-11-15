namespace WindowsFormsApp1
{
    partial class user_home
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.user_img = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.user_fullname = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.user_img)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(-6, -1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(697, 508);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // user_img
            // 
            this.user_img.Location = new System.Drawing.Point(688, -1);
            this.user_img.Name = "user_img";
            this.user_img.Size = new System.Drawing.Size(201, 194);
            this.user_img.TabIndex = 1;
            this.user_img.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(697, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // user_fullname
            // 
            this.user_fullname.AutoSize = true;
            this.user_fullname.Location = new System.Drawing.Point(741, 207);
            this.user_fullname.Name = "user_fullname";
            this.user_fullname.Size = new System.Drawing.Size(35, 13);
            this.user_fullname.TabIndex = 3;
            this.user_fullname.Text = "label2";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(700, 324);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(176, 41);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // user_home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(885, 507);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.user_fullname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.user_img);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "user_home";
            this.Text = "User Home";
            ((System.ComponentModel.ISupportInitialize)(this.user_img)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox user_img;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label user_fullname;
        private System.Windows.Forms.Button btnLogout;
    }
}
