using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomProgram
{
    public partial class ManagerMenu : Form
    {
        public ManagerMenu(string employeeName)
        {
            InitializeComponent();
            
            loggedInUserLabel.Text = employeeName;
        }

        private void ManagerMenu_Load(object sender, EventArgs e)
        {
               processOrderScreen.Dock = DockStyle.Fill;
               manageStockScreen.Dock = DockStyle.Fill;
               salesReportScreen.Dock = DockStyle.Fill;
               manageUserScreen.Dock = DockStyle.Fill;
        }

        private void manageStockButton_Click_1(object sender, EventArgs e)
        {
            //Repopulate Item DataGridView with updated Item list details
            manageStockScreen.Refresh();

            manageStockScreen.Show();
        }

        private void manageUsersButton_Click_1(object sender, EventArgs e)
        {
            manageUserScreen.Show();
        }

        private void salesReportButton_Click(object sender, EventArgs e)
        {
            salesReportScreen.Show();
        }

        private void newCustomerButton_Click_1(object sender, EventArgs e)
        {
            processOrderScreen.RefreshListViewData();

            processOrderScreen.Show();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
