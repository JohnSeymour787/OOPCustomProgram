namespace CustomProgram
{
    partial class ViewStockMenu
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
            this.allItemsListLabel = new System.Windows.Forms.Label();
            this.stockListView = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantityColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sortInformationLabel = new System.Windows.Forms.Label();
            this.sortButtons1 = new CustomProgram.SortButtons();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(712, 362);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // allItemsListLabel
            // 
            this.allItemsListLabel.AutoSize = true;
            this.allItemsListLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allItemsListLabel.Location = new System.Drawing.Point(16, 28);
            this.allItemsListLabel.Name = "allItemsListLabel";
            this.allItemsListLabel.Size = new System.Drawing.Size(156, 24);
            this.allItemsListLabel.TabIndex = 5;
            this.allItemsListLabel.Text = "All Items in Stock:";
            // 
            // stockListView
            // 
            this.stockListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.priceColumn,
            this.quantityColumn});
            this.stockListView.FullRowSelect = true;
            this.stockListView.GridLines = true;
            this.stockListView.Location = new System.Drawing.Point(20, 66);
            this.stockListView.MultiSelect = false;
            this.stockListView.Name = "stockListView";
            this.stockListView.Size = new System.Drawing.Size(226, 297);
            this.stockListView.TabIndex = 1;
            this.stockListView.UseCompatibleStateImageBehavior = false;
            this.stockListView.View = System.Windows.Forms.View.Details;
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Item Name:";
            this.nameColumn.Width = 100;
            // 
            // priceColumn
            // 
            this.priceColumn.Text = "Price:";
            this.priceColumn.Width = 62;
            // 
            // quantityColumn
            // 
            this.quantityColumn.Text = "Amount:";
            // 
            // sortInformationLabel
            // 
            this.sortInformationLabel.AutoSize = true;
            this.sortInformationLabel.Location = new System.Drawing.Point(408, 66);
            this.sortInformationLabel.Name = "sortInformationLabel";
            this.sortInformationLabel.Size = new System.Drawing.Size(228, 26);
            this.sortInformationLabel.TabIndex = 7;
            this.sortInformationLabel.Text = "To switch between ascending and descending\r\nsorts, press the same button again.";
            // 
            // sortButtons1
            // 
            this.sortButtons1.Location = new System.Drawing.Point(289, 66);
            this.sortButtons1.Name = "sortButtons1";
            this.sortButtons1.Size = new System.Drawing.Size(113, 220);
            this.sortButtons1.TabIndex = 6;
            // 
            // ViewStockMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sortInformationLabel);
            this.Controls.Add(this.sortButtons1);
            this.Controls.Add(this.stockListView);
            this.Controls.Add(this.allItemsListLabel);
            this.Controls.Add(this.backButton);
            this.Name = "ViewStockMenu";
            this.Size = new System.Drawing.Size(808, 416);
            this.Load += new System.EventHandler(this.ViewStockMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label allItemsListLabel;
        private System.Windows.Forms.ListView stockListView;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader priceColumn;
        private System.Windows.Forms.ColumnHeader quantityColumn;
        private SortButtons sortButtons1;
        private System.Windows.Forms.Label sortInformationLabel;
    }
}
