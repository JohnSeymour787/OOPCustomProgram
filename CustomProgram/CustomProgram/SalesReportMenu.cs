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
    public partial class SalesReportMenu : UserControl
    {
        public SalesReportMenu()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Refresh();
        }

        private void SalesReportMenu_Load(object sender, EventArgs e)
        {
            this.Refresh();
        }


        private void currentSessionSales_Click(object sender, EventArgs e)
        {
            this.Refresh();
            salesThisSessionLabel.Visible = true;

            //Populating ListView with details from current session sales.
            SalesReportController.SessionSalesReport(salesReportListView);

            //Updating labels to show total customers served and total revenue generated for session.
            customersServedValueLabel.Text = SalesReportController.CustomersServed();
            grandTotalValueLabel.Text = SalesReportController.TotalRevenue();
        }


        private void totalSalesButton_Click(object sender, EventArgs e)
        {
            this.Refresh();
            totalSalesLabel.Visible = true;

            //Populating ListView with details from total sales.
            SalesReportController.EntireSalesReport(salesReportListView);

            //Updating labels to show total customers served and total revenue generated for session.
            customersServedValueLabel.Text = SalesReportController.CustomersServed();
            grandTotalValueLabel.Text = SalesReportController.TotalRevenue();
        }


        /// <summary>
        /// Override to reset labels to empty strings, clear all ListView rows, and set title labels invisible.
        /// </summary>
        public override void Refresh()
        {
            customersServedValueLabel.Text = "";
            grandTotalValueLabel.Text = "";
            salesReportListView.Items.Clear();
            salesThisSessionLabel.Visible = false;
            totalSalesLabel.Visible = false;
        }
    }
}
