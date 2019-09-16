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
    public partial class ViewStockMenu : UserControl
    {
        public ViewStockMenu()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void ViewStockMenu_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Override to clear all ListView rows before repopulating them with UIController's ItemDatabase's StockList data
        /// </summary>
        public override void Refresh()
        {
            stockListView.Items.Clear();
            UIController.PopulateListView(stockListView);
        }
    }
}
