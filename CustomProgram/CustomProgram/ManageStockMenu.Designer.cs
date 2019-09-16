namespace CustomProgram
{
    partial class ManageStockMenu
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
            this.components = new System.ComponentModel.Container();
            this.backButton = new System.Windows.Forms.Button();
            this.stockDataGridView = new System.Windows.Forms.DataGridView();
            this.itemNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateRecordsButton = new System.Windows.Forms.Button();
            this.instructionsLabel = new System.Windows.Forms.Label();
            this.sortButtons1 = new CustomProgram.SortButtons();
            this.itemDatabaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sortInformationLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.stockDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemDatabaseBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(566, 345);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // stockDataGridView
            // 
            this.stockDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.stockDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stockDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stockDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemNameColumn,
            this.priceColumn,
            this.quantityColumn});
            this.stockDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.stockDataGridView.EnableHeadersVisualStyles = false;
            this.stockDataGridView.Location = new System.Drawing.Point(46, 30);
            this.stockDataGridView.Name = "stockDataGridView";
            this.stockDataGridView.RowHeadersVisible = false;
            this.stockDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.stockDataGridView.Size = new System.Drawing.Size(227, 238);
            this.stockDataGridView.TabIndex = 1;
            // 
            // itemNameColumn
            // 
            this.itemNameColumn.HeaderText = "Item Name:";
            this.itemNameColumn.Name = "itemNameColumn";
            // 
            // priceColumn
            // 
            this.priceColumn.HeaderText = "Price:";
            this.priceColumn.Name = "priceColumn";
            this.priceColumn.Width = 62;
            // 
            // quantityColumn
            // 
            this.quantityColumn.HeaderText = "Amount:";
            this.quantityColumn.Name = "quantityColumn";
            this.quantityColumn.Width = 60;
            // 
            // updateRecordsButton
            // 
            this.updateRecordsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateRecordsButton.Location = new System.Drawing.Point(333, 345);
            this.updateRecordsButton.Name = "updateRecordsButton";
            this.updateRecordsButton.Size = new System.Drawing.Size(113, 50);
            this.updateRecordsButton.TabIndex = 2;
            this.updateRecordsButton.Text = "Update Records";
            this.updateRecordsButton.UseVisualStyleBackColor = true;
            this.updateRecordsButton.Click += new System.EventHandler(this.updateRecordsButton_Click);
            // 
            // instructionsLabel
            // 
            this.instructionsLabel.AutoSize = true;
            this.instructionsLabel.Location = new System.Drawing.Point(330, 19);
            this.instructionsLabel.Name = "instructionsLabel";
            this.instructionsLabel.Size = new System.Drawing.Size(190, 39);
            this.instructionsLabel.TabIndex = 5;
            this.instructionsLabel.Text = "To delete an item, clear any single cell \r\nfrom a row (or all cells) and click \r\n" +
    "\"Update Records\".";
            // 
            // sortButtons1
            // 
            this.sortButtons1.Location = new System.Drawing.Point(333, 83);
            this.sortButtons1.Name = "sortButtons1";
            this.sortButtons1.Size = new System.Drawing.Size(113, 220);
            this.sortButtons1.TabIndex = 6;
            // 
            // itemDatabaseBindingSource
            // 
            this.itemDatabaseBindingSource.DataSource = typeof(CustomProgram.ItemDatabase);
            // 
            // sortInformationLabel
            // 
            this.sortInformationLabel.AutoSize = true;
            this.sortInformationLabel.Location = new System.Drawing.Point(492, 180);
            this.sortInformationLabel.Name = "sortInformationLabel";
            this.sortInformationLabel.Size = new System.Drawing.Size(149, 39);
            this.sortInformationLabel.TabIndex = 8;
            this.sortInformationLabel.Text = "To switch between ascending\r\n and descendingsorts, press\r\n the same button again." +
    "";
            // 
            // ManageStockMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sortInformationLabel);
            this.Controls.Add(this.sortButtons1);
            this.Controls.Add(this.instructionsLabel);
            this.Controls.Add(this.updateRecordsButton);
            this.Controls.Add(this.stockDataGridView);
            this.Controls.Add(this.backButton);
            this.Name = "ManageStockMenu";
            this.Size = new System.Drawing.Size(690, 422);
            this.Load += new System.EventHandler(this.ManageStockMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stockDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemDatabaseBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.DataGridView stockDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn quantityColumn;
        private System.Windows.Forms.BindingSource itemDatabaseBindingSource;
        private System.Windows.Forms.Button updateRecordsButton;
        private System.Windows.Forms.Label instructionsLabel;
        private SortButtons sortButtons1;
        private System.Windows.Forms.Label sortInformationLabel;
    }
}
