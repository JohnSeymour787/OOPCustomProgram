using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomProgram
{
    public partial class ManageUserMenu : UserControl
    {
        public ManageUserMenu()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        /// <summary>
        /// Override to clear all rows of the DataGridView before repopulating them again with all user details.
        /// </summary>
        public override void Refresh()
        {
            userDataGridView.Rows.Clear();

            ProcessUserController.PopulateDataGridView(userDataGridView.Rows);
        }


        private void ManageUserMenu_Load(object sender, EventArgs e)
        {
            Refresh();
        }
        

        private void updateRecordsButton_Click(object sender, EventArgs e)
        {
            //Item List index (and DataGridView row) counter
            int i = 0;
            UserInfo userData;

            foreach (DataGridViewRow row in userDataGridView.Rows)
            {
                if (!row.CheckRowEmptyCells())
                {
                    //Checking for positive user ID entered as string
                    if ((!Int32.TryParse(row.Cells[0].Value.ToString(), out int userID)) || (userID <= 0))
                    {
                        MessageBox.Show("Error: Please enter a positive number for the user's ID");
                        return;
                    }

                    //Checking for valid UserType entered as string
                    if (!Enum.TryParse(row.Cells[2].Value.ToString(), true, out UserType userType))
                    {
                        MessageBox.Show("Error: Please enter a valid user type");
                        return;
                    }

                    userData = new UserInfo();
                    userData.ID = userID;
                    userData.role = userType;
                    userData.employeeName = row.Cells[1].Value.ToString();
                    userData.username = row.Cells[3].Value.ToString();
                    userData.password = row.Cells[4].Value.ToString();

                    //i < count AND row contents NOT null --> Replace list index's data
                    if (i < ProcessUserController.UserCount())
                    {
                        ProcessUserController.ReplaceUserData(i, userData);
                    }
                    //i >= count AND row contents NOT null --> Add new item to list
                    else
                    {
                        //Checking if a user with the same ID or username already exists
                        if (!ProcessUserController.AddNewUser(userData))
                        {
                            MessageBox.Show("Error: There is already a user with that ID or username");
                            return;
                        }
                    }
                    //Always incrementing if not null
                    i++;
                }
                //Otherwise, row has at least 1 empty cell
                else
                {
                    //i < count AND row contents null --> Remove list index but don't increment
                    if (i < ProcessUserController.UserCount())
                    {
                        ProcessUserController.RemoveUserAt(i);
                    }
                    //i > count AND null --> Do nothing and break, at end of dataView table
                    else
                        break;
                }
            }

            //Updating user textfile and repopulating the DataGridView
            ProcessUserController.UpdateUserFiles();
            Refresh();
        }
    }
}
