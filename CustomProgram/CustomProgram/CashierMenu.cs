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
    public partial class CashierMenu : Form
    {
        public CashierMenu(string employeeName)
        {
            InitializeComponent();

            loggedInUserLabel.Text = employeeName;
        }

        private void CashierMenu_Load(object sender, EventArgs e)
        {
            viewStockScreen.Dock = DockStyle.Fill;
            processOrderScreen.Dock = DockStyle.Fill;
        }

        private void viewStockButton_Click(object sender, EventArgs e)
        {
            //Repopulating ListView with item details
            viewStockScreen.Refresh();

            viewStockScreen.Show();
        }

        private void newCustomerButton_Click(object sender, EventArgs e)
        {
            processOrderScreen.RefreshListViewData();

            processOrderScreen.Show();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
