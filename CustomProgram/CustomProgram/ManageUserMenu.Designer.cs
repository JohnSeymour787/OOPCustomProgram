namespace CustomProgram
{
    partial class ManageUserMenu
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
            this.userDataGridView = new System.Windows.Forms.DataGridView();
            this.idNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.employeeNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passwordColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.updateRecordsButton = new System.Windows.Forms.Button();
            this.instructionsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.userDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(552, 316);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // userDataGridView
            // 
            this.userDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.userDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.userDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idNumberColumn,
            this.employeeNameColumn,
            this.roleColumn,
            this.userNameColumn,
            this.passwordColumn});
            this.userDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.userDataGridView.EnableHeadersVisualStyles = false;
            this.userDataGridView.Location = new System.Drawing.Point(18, 35);
            this.userDataGridView.Name = "userDataGridView";
            this.userDataGridView.RowHeadersVisible = false;
            this.userDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.userDataGridView.Size = new System.Drawing.Size(484, 238);
            this.userDataGridView.TabIndex = 2;
            // 
            // idNumberColumn
            // 
            this.idNumberColumn.HeaderText = "ID:";
            this.idNumberColumn.Name = "idNumberColumn";
            this.idNumberColumn.Width = 70;
            // 
            // employeeNameColumn
            // 
            this.employeeNameColumn.HeaderText = "Employee Name:";
            this.employeeNameColumn.Name = "employeeNameColumn";
            this.employeeNameColumn.Width = 150;
            // 
            // roleColumn
            // 
            this.roleColumn.HeaderText = "Role:";
            this.roleColumn.Name = "roleColumn";
            this.roleColumn.Width = 60;
            // 
            // userNameColumn
            // 
            this.userNameColumn.HeaderText = "Username:";
            this.userNameColumn.Name = "userNameColumn";
            // 
            // passwordColumn
            // 
            this.passwordColumn.HeaderText = "Password:";
            this.passwordColumn.Name = "passwordColumn";
            // 
            // updateRecordsButton
            // 
            this.updateRecordsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateRecordsButton.Location = new System.Drawing.Point(236, 303);
            this.updateRecordsButton.Name = "updateRecordsButton";
            this.updateRecordsButton.Size = new System.Drawing.Size(100, 50);
            this.updateRecordsButton.TabIndex = 3;
            this.updateRecordsButton.Text = "Update Records";
            this.updateRecordsButton.UseVisualStyleBackColor = true;
            this.updateRecordsButton.Click += new System.EventHandler(this.updateRecordsButton_Click);
            // 
            // instructionsLabel
            // 
            this.instructionsLabel.AutoSize = true;
            this.instructionsLabel.Location = new System.Drawing.Point(503, 35);
            this.instructionsLabel.Name = "instructionsLabel";
            this.instructionsLabel.Size = new System.Drawing.Size(171, 39);
            this.instructionsLabel.TabIndex = 4;
            this.instructionsLabel.Text = "To delete a user, clear a single cell\r\n(or all cells) from a row and click \r\n\"Upd" +
    "ate Records\".";
            this.instructionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManageUserMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.instructionsLabel);
            this.Controls.Add(this.updateRecordsButton);
            this.Controls.Add(this.userDataGridView);
            this.Controls.Add(this.backButton);
            this.Name = "ManageUserMenu";
            this.Size = new System.Drawing.Size(677, 382);
            this.Load += new System.EventHandler(this.ManageUserMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.userDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.DataGridView userDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn idNumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn roleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passwordColumn;
        private System.Windows.Forms.Button updateRecordsButton;
        private System.Windows.Forms.Label instructionsLabel;
    }
}
