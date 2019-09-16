namespace CustomProgram
{
    partial class ManagerMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.manageStockButton = new System.Windows.Forms.Button();
            this.salesReportButton = new System.Windows.Forms.Button();
            this.newCustomerButton = new System.Windows.Forms.Button();
            this.manageUsersButton = new System.Windows.Forms.Button();
            this.processOrderScreen = new CustomProgram.ProcessOrderScreen();
            this.manageUserScreen = new CustomProgram.ManageUserMenu();
            this.manageStockScreen = new CustomProgram.ManageStockMenu();
            this.salesReportScreen = new CustomProgram.SalesReportMenu();
            this.logoutButton = new System.Windows.Forms.Button();
            this.loggedInUserLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // manageStockButton
            // 
            this.manageStockButton.Location = new System.Drawing.Point(163, 105);
            this.manageStockButton.Name = "manageStockButton";
            this.manageStockButton.Size = new System.Drawing.Size(75, 54);
            this.manageStockButton.TabIndex = 0;
            this.manageStockButton.Text = "Manage\r\nStock";
            this.manageStockButton.UseVisualStyleBackColor = true;
            this.manageStockButton.Click += new System.EventHandler(this.manageStockButton_Click_1);
            // 
            // salesReportButton
            // 
            this.salesReportButton.Location = new System.Drawing.Point(443, 215);
            this.salesReportButton.Name = "salesReportButton";
            this.salesReportButton.Size = new System.Drawing.Size(75, 54);
            this.salesReportButton.TabIndex = 1;
            this.salesReportButton.Text = "Generate\r\nSales Report";
            this.salesReportButton.UseVisualStyleBackColor = true;
            this.salesReportButton.Click += new System.EventHandler(this.salesReportButton_Click);
            // 
            // newCustomerButton
            // 
            this.newCustomerButton.Location = new System.Drawing.Point(443, 105);
            this.newCustomerButton.Name = "newCustomerButton";
            this.newCustomerButton.Size = new System.Drawing.Size(75, 54);
            this.newCustomerButton.TabIndex = 2;
            this.newCustomerButton.Text = "Process\r\nOrder";
            this.newCustomerButton.UseVisualStyleBackColor = true;
            this.newCustomerButton.Click += new System.EventHandler(this.newCustomerButton_Click_1);
            // 
            // manageUsersButton
            // 
            this.manageUsersButton.Location = new System.Drawing.Point(163, 215);
            this.manageUsersButton.Name = "manageUsersButton";
            this.manageUsersButton.Size = new System.Drawing.Size(75, 54);
            this.manageUsersButton.TabIndex = 3;
            this.manageUsersButton.Text = "Manage\r\nUsers";
            this.manageUsersButton.UseVisualStyleBackColor = true;
            this.manageUsersButton.Click += new System.EventHandler(this.manageUsersButton_Click_1);
            // 
            // processOrderScreen
            // 
            this.processOrderScreen.Location = new System.Drawing.Point(12, 215);
            this.processOrderScreen.Name = "processOrderScreen";
            this.processOrderScreen.Size = new System.Drawing.Size(66, 47);
            this.processOrderScreen.TabIndex = 7;
            this.processOrderScreen.Visible = false;
            // 
            // manageUserScreen
            // 
            this.manageUserScreen.Location = new System.Drawing.Point(12, 128);
            this.manageUserScreen.Name = "manageUserScreen";
            this.manageUserScreen.Size = new System.Drawing.Size(66, 47);
            this.manageUserScreen.TabIndex = 6;
            this.manageUserScreen.Visible = false;
            // 
            // manageStockScreen
            // 
            this.manageStockScreen.Location = new System.Drawing.Point(12, 75);
            this.manageStockScreen.Name = "manageStockScreen";
            this.manageStockScreen.Size = new System.Drawing.Size(66, 47);
            this.manageStockScreen.TabIndex = 5;
            this.manageStockScreen.Visible = false;
            // 
            // salesReportScreen
            // 
            this.salesReportScreen.Location = new System.Drawing.Point(33, 39);
            this.salesReportScreen.Name = "salesReportScreen";
            this.salesReportScreen.Size = new System.Drawing.Size(66, 47);
            this.salesReportScreen.TabIndex = 4;
            this.salesReportScreen.Visible = false;
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(694, 412);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 23);
            this.logoutButton.TabIndex = 8;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logout_Click);
            // 
            // loggedInUserLabel
            // 
            this.loggedInUserLabel.AutoSize = true;
            this.loggedInUserLabel.Location = new System.Drawing.Point(6, 5);
            this.loggedInUserLabel.Name = "loggedInUserLabel";
            this.loggedInUserLabel.Size = new System.Drawing.Size(0, 13);
            this.loggedInUserLabel.TabIndex = 9;
            // 
            // ManagerMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.loggedInUserLabel);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.processOrderScreen);
            this.Controls.Add(this.manageUserScreen);
            this.Controls.Add(this.manageStockScreen);
            this.Controls.Add(this.salesReportScreen);
            this.Controls.Add(this.manageUsersButton);
            this.Controls.Add(this.newCustomerButton);
            this.Controls.Add(this.salesReportButton);
            this.Controls.Add(this.manageStockButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ManagerMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manager Menu";
            this.Load += new System.EventHandler(this.ManagerMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button manageStockButton;
        private System.Windows.Forms.Button salesReportButton;
        private System.Windows.Forms.Button newCustomerButton;
        private System.Windows.Forms.Button manageUsersButton;
        private SalesReportMenu salesReportScreen;
        private ManageStockMenu manageStockScreen;
        private ManageUserMenu manageUserScreen;
        private ProcessOrderScreen processOrderScreen;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Label loggedInUserLabel;
    }
}