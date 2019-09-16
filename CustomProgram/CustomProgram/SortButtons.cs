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
    public partial class SortButtons : UserControl
    {
        //Fields for sort order (ascending or descending) of each sort type button
        private bool _alphabeticalSortOrder = true;
        private bool _priceSortOrder = true;
        private bool _amountSortOrder = true;


        public SortButtons()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Uses UIController to sort its ItemDatabase.StockList by Item Name.
        /// Sort order (ascending/descending) is inverted everytime this button is clicked.
        /// Then calls the overridable Refresh() method of the Parent Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alphabetSort_Click(object sender, EventArgs e)
        {
            UIController.SortItemList(ItemSortType.Name, _alphabeticalSortOrder);
            _alphabeticalSortOrder = !_alphabeticalSortOrder;

            Parent.Refresh();
        }


        /// <summary>
        /// Uses UIController to sort its ItemDatabase.StockList by Item Price.
        /// Sort order (ascending/descending) is inverted everytime this button is clicked.
        /// Then calls the overridable Refresh() method of the Parent Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void priceSortButton_Click(object sender, EventArgs e)
        {
            UIController.SortItemList(ItemSortType.Price, _priceSortOrder);
            _priceSortOrder = !_priceSortOrder;

            Parent.Refresh();
        }


        /// <summary>
        /// Uses UIController to sort its ItemDatabase.StockList by Item Quantity.
        /// Sort order (ascending/descending) is inverted everytime this button is clicked.
        /// Then calls the overridable Refresh() method of the Parent Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quantitySortButton_Click(object sender, EventArgs e)
        {
            UIController.SortItemList(ItemSortType.Amount, _amountSortOrder);
            _amountSortOrder = !_amountSortOrder;

            Parent.Refresh();
        }

        private void SortButtons_Load(object sender, EventArgs e)
        {

        }
    }
}
