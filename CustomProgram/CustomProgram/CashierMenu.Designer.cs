namespace CustomProgram
{
    partial class CashierMenu
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
            this.viewStockButton = new System.Windows.Forms.Button();
            this.newCustomerButton = new System.Windows.Forms.Button();
            this.processOrderScreen = new CustomProgram.ProcessOrderScreen();
            this.viewStockScreen = new CustomProgram.ViewStockMenu();
            this.logoutButton = new System.Windows.Forms.Button();
            this.loggedInUserLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // viewStockButton
            // 
            this.viewStockButton.Location = new System.Drawing.Point(164, 117);
            this.viewStockButton.Name = "viewStockButton";
            this.viewStockButton.Size = new System.Drawing.Size(75, 69);
            this.viewStockButton.TabIndex = 2;
            this.viewStockButton.Text = "View\r\nStock";
            this.viewStockButton.UseVisualStyleBackColor = true;
            this.viewStockButton.Click += new System.EventHandler(this.viewStockButton_Click);
            // 
            // newCustomerButton
            // 
            this.newCustomerButton.Location = new System.Drawing.Point(404, 117);
            this.newCustomerButton.Name = "newCustomerButton";
            this.newCustomerButton.Size = new System.Drawing.Size(75, 69);
            this.newCustomerButton.TabIndex = 2;
            this.newCustomerButton.Text = "Process\r\nCustomer";
            this.newCustomerButton.UseVisualStyleBackColor = true;
            this.newCustomerButton.Click += new System.EventHandler(this.newCustomerButton_Click);
            // 
            // processOrderScreen
            // 
            this.processOrderScreen.Location = new System.Drawing.Point(30, 12);
            this.processOrderScreen.Name = "processOrderScreen";
            this.processOrderScreen.Size = new System.Drawing.Size(104, 94);
            this.processOrderScreen.TabIndex = 2;
            this.processOrderScreen.Visible = false;
            // 
            // viewStockScreen
            // 
            this.viewStockScreen.Location = new System.Drawing.Point(30, 130);
            this.viewStockScreen.Name = "viewStockScreen";
            this.viewStockScreen.Size = new System.Drawing.Size(93, 75);
            this.viewStockScreen.TabIndex = 3;
            this.viewStockScreen.Visible = false;
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(713, 415);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 23);
            this.logoutButton.TabIndex = 9;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // loggedInUserLabel
            // 
            this.loggedInUserLabel.AutoSize = true;
            this.loggedInUserLabel.Location = new System.Drawing.Point(5, 3);
            this.loggedInUserLabel.Name = "loggedInUserLabel";
            this.loggedInUserLabel.Size = new System.Drawing.Size(0, 13);
            this.loggedInUserLabel.TabIndex = 10;
            // 
            // CashierMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.loggedInUserLabel);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.viewStockScreen);
            this.Controls.Add(this.processOrderScreen);
            this.Controls.Add(this.newCustomerButton);
            this.Controls.Add(this.viewStockButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CashierMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cashier Menu";
            this.Load += new System.EventHandler(this.CashierMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button viewStockButton;
        private System.Windows.Forms.Button newCustomerButton;
        private ProcessOrderScreen processOrderScreen;
        private ViewStockMenu viewStockScreen;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.Label loggedInUserLabel;
    }
}