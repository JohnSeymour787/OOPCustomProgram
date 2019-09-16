namespace CustomProgram
{
    partial class SortButtons
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
            this.alphabetSort = new System.Windows.Forms.Button();
            this.quantitySortButton = new System.Windows.Forms.Button();
            this.priceSortButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // alphabetSort
            // 
            this.alphabetSort.Location = new System.Drawing.Point(4, 5);
            this.alphabetSort.Name = "alphabetSort";
            this.alphabetSort.Size = new System.Drawing.Size(104, 41);
            this.alphabetSort.TabIndex = 7;
            this.alphabetSort.Text = "Sort Alphabetically";
            this.alphabetSort.UseVisualStyleBackColor = true;
            this.alphabetSort.Click += new System.EventHandler(this.alphabetSort_Click);
            // 
            // quantitySortButton
            // 
            this.quantitySortButton.Location = new System.Drawing.Point(4, 175);
            this.quantitySortButton.Name = "quantitySortButton";
            this.quantitySortButton.Size = new System.Drawing.Size(104, 41);
            this.quantitySortButton.TabIndex = 6;
            this.quantitySortButton.Text = "Sort by Quantity";
            this.quantitySortButton.UseVisualStyleBackColor = true;
            this.quantitySortButton.Click += new System.EventHandler(this.quantitySortButton_Click);
            // 
            // priceSortButton
            // 
            this.priceSortButton.Location = new System.Drawing.Point(4, 90);
            this.priceSortButton.Name = "priceSortButton";
            this.priceSortButton.Size = new System.Drawing.Size(104, 41);
            this.priceSortButton.TabIndex = 5;
            this.priceSortButton.Text = "Sort by Price";
            this.priceSortButton.UseVisualStyleBackColor = true;
            this.priceSortButton.Click += new System.EventHandler(this.priceSortButton_Click);
            // 
            // SortButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.alphabetSort);
            this.Controls.Add(this.quantitySortButton);
            this.Controls.Add(this.priceSortButton);
            this.Name = "SortButtons";
            this.Size = new System.Drawing.Size(113, 220);
            this.Load += new System.EventHandler(this.SortButtons_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button alphabetSort;
        private System.Windows.Forms.Button quantitySortButton;
        private System.Windows.Forms.Button priceSortButton;
    }
}
