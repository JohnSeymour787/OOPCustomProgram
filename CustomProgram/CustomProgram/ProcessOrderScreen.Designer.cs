namespace CustomProgram
{
    partial class ProcessOrderScreen
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
            this.stockListView = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.priceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantityColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.addQuantityButton = new System.Windows.Forms.Button();
            this.subtractQuantityButton = new System.Windows.Forms.Button();
            this.quantityLabel = new System.Windows.Forms.Label();
            this.quantityValueLabel = new System.Windows.Forms.Label();
            this.addToPurchaseButton = new System.Windows.Forms.Button();
            this.doneButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.weightLabel = new System.Windows.Forms.Label();
            this.weightEntryTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(595, 308);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // stockListView
            // 
            this.stockListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.priceColumn,
            this.quantityColumn});
            this.stockListView.FullRowSelect = true;
            this.stockListView.GridLines = true;
            this.stockListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.stockListView.HideSelection = false;
            this.stockListView.Location = new System.Drawing.Point(45, 34);
            this.stockListView.MultiSelect = false;
            this.stockListView.Name = "stockListView";
            this.stockListView.Size = new System.Drawing.Size(226, 297);
            this.stockListView.TabIndex = 0;
            this.stockListView.UseCompatibleStateImageBehavior = false;
            this.stockListView.View = System.Windows.Forms.View.Details;
            this.stockListView.SelectedIndexChanged += new System.EventHandler(this.stockListView_SelectedIndexChanged);
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
            // addQuantityButton
            // 
            this.addQuantityButton.Location = new System.Drawing.Point(475, 105);
            this.addQuantityButton.Name = "addQuantityButton";
            this.addQuantityButton.Size = new System.Drawing.Size(28, 23);
            this.addQuantityButton.TabIndex = 3;
            this.addQuantityButton.Text = "+";
            this.addQuantityButton.UseVisualStyleBackColor = true;
            this.addQuantityButton.Click += new System.EventHandler(this.addQuantityButton_Click);
            // 
            // subtractQuantityButton
            // 
            this.subtractQuantityButton.Location = new System.Drawing.Point(428, 105);
            this.subtractQuantityButton.Name = "subtractQuantityButton";
            this.subtractQuantityButton.Size = new System.Drawing.Size(28, 23);
            this.subtractQuantityButton.TabIndex = 4;
            this.subtractQuantityButton.Text = "-";
            this.subtractQuantityButton.UseVisualStyleBackColor = true;
            this.subtractQuantityButton.Click += new System.EventHandler(this.subtractQuantityButton_Click);
            // 
            // quantityLabel
            // 
            this.quantityLabel.AutoSize = true;
            this.quantityLabel.Location = new System.Drawing.Point(425, 63);
            this.quantityLabel.Name = "quantityLabel";
            this.quantityLabel.Size = new System.Drawing.Size(49, 13);
            this.quantityLabel.TabIndex = 5;
            this.quantityLabel.Text = "Quantity:";
            // 
            // quantityValueLabel
            // 
            this.quantityValueLabel.AutoSize = true;
            this.quantityValueLabel.Location = new System.Drawing.Point(490, 63);
            this.quantityValueLabel.Name = "quantityValueLabel";
            this.quantityValueLabel.Size = new System.Drawing.Size(13, 13);
            this.quantityValueLabel.TabIndex = 6;
            this.quantityValueLabel.Text = "0";
            // 
            // addToPurchaseButton
            // 
            this.addToPurchaseButton.Location = new System.Drawing.Point(428, 158);
            this.addToPurchaseButton.Name = "addToPurchaseButton";
            this.addToPurchaseButton.Size = new System.Drawing.Size(75, 23);
            this.addToPurchaseButton.TabIndex = 7;
            this.addToPurchaseButton.Text = "Add";
            this.addToPurchaseButton.UseVisualStyleBackColor = true;
            this.addToPurchaseButton.Click += new System.EventHandler(this.addToPurchaseButton_Click);
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(471, 249);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(75, 23);
            this.doneButton.TabIndex = 8;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(365, 249);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Location = new System.Drawing.Point(362, 208);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(65, 13);
            this.weightLabel.TabIndex = 10;
            this.weightLabel.Text = "Weight (kg):";
            // 
            // weightEntryTextbox
            // 
            this.weightEntryTextbox.Location = new System.Drawing.Point(428, 205);
            this.weightEntryTextbox.Name = "weightEntryTextbox";
            this.weightEntryTextbox.Size = new System.Drawing.Size(100, 20);
            this.weightEntryTextbox.TabIndex = 11;
            // 
            // ProcessOrderScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.weightEntryTextbox);
            this.Controls.Add(this.weightLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.addToPurchaseButton);
            this.Controls.Add(this.quantityValueLabel);
            this.Controls.Add(this.quantityLabel);
            this.Controls.Add(this.subtractQuantityButton);
            this.Controls.Add(this.addQuantityButton);
            this.Controls.Add(this.stockListView);
            this.Controls.Add(this.backButton);
            this.Name = "ProcessOrderScreen";
            this.Size = new System.Drawing.Size(690, 346);
            this.Load += new System.EventHandler(this.ProcessOrderScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.ListView stockListView;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader priceColumn;
        private System.Windows.Forms.ColumnHeader quantityColumn;
        private System.Windows.Forms.Button addQuantityButton;
        private System.Windows.Forms.Button subtractQuantityButton;
        private System.Windows.Forms.Label quantityLabel;
        private System.Windows.Forms.Label quantityValueLabel;
        private System.Windows.Forms.Button addToPurchaseButton;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label weightLabel;
        private System.Windows.Forms.TextBox weightEntryTextbox;
    }
}
