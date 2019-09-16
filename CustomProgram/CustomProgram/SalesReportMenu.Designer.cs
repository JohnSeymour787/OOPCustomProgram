namespace CustomProgram
{
    partial class SalesReportMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backButton = new System.Windows.Forms.Button();
            this.currentSessionSalesButton = new System.Windows.Forms.Button();
            this.totalSalesButton = new System.Windows.Forms.Button();
            this.customersServedLabel = new System.Windows.Forms.Label();
            this.revenueLabel = new System.Windows.Forms.Label();
            this.salesThisSessionLabel = new System.Windows.Forms.Label();
            this.totalSalesLabel = new System.Windows.Forms.Label();
            this.customersServedValueLabel = new System.Windows.Forms.Label();
            this.grandTotalValueLabel = new System.Windows.Forms.Label();
            this.salesReportListView = new System.Windows.Forms.ListView();
            this.itemSold = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.total = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(496, 333);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // currentSessionSalesButton
            // 
            this.currentSessionSalesButton.Location = new System.Drawing.Point(403, 32);
            this.currentSessionSalesButton.Name = "currentSessionSalesButton";
            this.currentSessionSalesButton.Size = new System.Drawing.Size(75, 45);
            this.currentSessionSalesButton.TabIndex = 2;
            this.currentSessionSalesButton.Text = "Sales made this session";
            this.currentSessionSalesButton.UseVisualStyleBackColor = true;
            this.currentSessionSalesButton.Click += new System.EventHandler(this.currentSessionSales_Click);
            // 
            // totalSalesButton
            // 
            this.totalSalesButton.Location = new System.Drawing.Point(403, 111);
            this.totalSalesButton.Name = "totalSalesButton";
            this.totalSalesButton.Size = new System.Drawing.Size(75, 45);
            this.totalSalesButton.TabIndex = 3;
            this.totalSalesButton.Text = "Sales made in total";
            this.totalSalesButton.UseVisualStyleBackColor = true;
            this.totalSalesButton.Click += new System.EventHandler(this.totalSalesButton_Click);
            // 
            // customersServedLabel
            // 
            this.customersServedLabel.AutoSize = true;
            this.customersServedLabel.Location = new System.Drawing.Point(82, 48);
            this.customersServedLabel.Name = "customersServedLabel";
            this.customersServedLabel.Size = new System.Drawing.Size(148, 13);
            this.customersServedLabel.TabIndex = 5;
            this.customersServedLabel.Text = "Number of Customers Served:";
            // 
            // revenueLabel
            // 
            this.revenueLabel.AutoSize = true;
            this.revenueLabel.Location = new System.Drawing.Point(82, 64);
            this.revenueLabel.Name = "revenueLabel";
            this.revenueLabel.Size = new System.Drawing.Size(166, 13);
            this.revenueLabel.TabIndex = 6;
            this.revenueLabel.Text = "Grand Total Revenue Generated:";
            // 
            // salesThisSessionLabel
            // 
            this.salesThisSessionLabel.AutoSize = true;
            this.salesThisSessionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salesThisSessionLabel.Location = new System.Drawing.Point(82, 14);
            this.salesThisSessionLabel.Name = "salesThisSessionLabel";
            this.salesThisSessionLabel.Size = new System.Drawing.Size(163, 24);
            this.salesThisSessionLabel.TabIndex = 7;
            this.salesThisSessionLabel.Text = "Sales this session:";
            this.salesThisSessionLabel.Visible = false;
            // 
            // totalSalesLabel
            // 
            this.totalSalesLabel.AutoSize = true;
            this.totalSalesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalSalesLabel.Location = new System.Drawing.Point(82, 14);
            this.totalSalesLabel.Name = "totalSalesLabel";
            this.totalSalesLabel.Size = new System.Drawing.Size(166, 24);
            this.totalSalesLabel.TabIndex = 8;
            this.totalSalesLabel.Text = "All recorded sales:";
            this.totalSalesLabel.Visible = false;
            // 
            // customersServedValueLabel
            // 
            this.customersServedValueLabel.AutoSize = true;
            this.customersServedValueLabel.Location = new System.Drawing.Point(235, 48);
            this.customersServedValueLabel.Name = "customersServedValueLabel";
            this.customersServedValueLabel.Size = new System.Drawing.Size(0, 13);
            this.customersServedValueLabel.TabIndex = 10;
            // 
            // grandTotalValueLabel
            // 
            this.grandTotalValueLabel.AutoSize = true;
            this.grandTotalValueLabel.Location = new System.Drawing.Point(233, 64);
            this.grandTotalValueLabel.Name = "grandTotalValueLabel";
            this.grandTotalValueLabel.Size = new System.Drawing.Size(0, 13);
            this.grandTotalValueLabel.TabIndex = 11;
            // 
            // salesReportListView
            // 
            this.salesReportListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.itemSold,
            this.quantity,
            this.total});
            this.salesReportListView.GridLines = true;
            this.salesReportListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.salesReportListView.Location = new System.Drawing.Point(86, 98);
            this.salesReportListView.MultiSelect = false;
            this.salesReportListView.Name = "salesReportListView";
            this.salesReportListView.ShowGroups = false;
            this.salesReportListView.Size = new System.Drawing.Size(228, 258);
            this.salesReportListView.TabIndex = 12;
            this.salesReportListView.UseCompatibleStateImageBehavior = false;
            this.salesReportListView.View = System.Windows.Forms.View.Details;
            // 
            // itemSold
            // 
            this.itemSold.Text = "Item Sold:";
            this.itemSold.Width = 100;
            // 
            // quantity
            // 
            this.quantity.Text = "Quantity:";
            // 
            // total
            // 
            this.total.Text = "Total:";
            this.total.Width = 62;
            // 
            // SalesReportMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.salesReportListView);
            this.Controls.Add(this.grandTotalValueLabel);
            this.Controls.Add(this.customersServedValueLabel);
            this.Controls.Add(this.totalSalesLabel);
            this.Controls.Add(this.salesThisSessionLabel);
            this.Controls.Add(this.revenueLabel);
            this.Controls.Add(this.customersServedLabel);
            this.Controls.Add(this.totalSalesButton);
            this.Controls.Add(this.currentSessionSalesButton);
            this.Controls.Add(this.backButton);
            this.Name = "SalesReportMenu";
            this.Size = new System.Drawing.Size(664, 407);
            this.Load += new System.EventHandler(this.SalesReportMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button currentSessionSalesButton;
        private System.Windows.Forms.Button totalSalesButton;
        private System.Windows.Forms.Label customersServedLabel;
        private System.Windows.Forms.Label revenueLabel;
        private System.Windows.Forms.Label salesThisSessionLabel;
        private System.Windows.Forms.Label totalSalesLabel;
        private System.Windows.Forms.Label customersServedValueLabel;
        private System.Windows.Forms.Label grandTotalValueLabel;
        private System.Windows.Forms.ListView salesReportListView;
        private System.Windows.Forms.ColumnHeader itemSold;
        private System.Windows.Forms.ColumnHeader quantity;
        private System.Windows.Forms.ColumnHeader total;
    }
}
