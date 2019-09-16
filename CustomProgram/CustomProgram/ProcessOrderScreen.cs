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
    public partial class ProcessOrderScreen : UserControl
    {
        //Used to keep count of how many of a single item a user has purchased so far
        private int _amountLabelValue = 0;
        //Used to keep track of what index in the ListView (and thus what index in the StockList List<Item>) is selected
        //-1 indicates no selection.
        private int _listSelectedIndex = -1;

        public ProcessOrderScreen()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Resets the object's fields to their default values (indicating that no items have been purchased and that no items in
        /// the ListView are selected).
        /// Clears all rows of the stockListView before sorting UIController's ItemDatabase.Stocklist and redrawing the details from
        /// all items in this Stocklist to the stockListView.
        /// </summary>
        public void RefreshListViewData()
        {
            _listSelectedIndex = -1;
            _amountLabelValue = 0;
            weightEntryTextbox.Clear();

            quantityValueLabel.Text = _amountLabelValue.ToString();

            stockListView.Items.Clear();
            //Sorting the item list by item name in ascending order
            UIController.SortItemList(ItemSortType.Name, true);

            UIController.PopulateListView(stockListView);
        }

        private void ProcessOrderScreen_Load(object sender, EventArgs e)
        {
            ProcessOrderController.NewOrder();
            RefreshListViewData();
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            //Asks the ProcessOrderController to write the Order purchases details to a receipts textfile and to update the Items textfile
            //If it fails in either of these file readings then returns an error message to be displayed via a MessageBox.
            if (ProcessOrderController.OrderCompleteSuccess(out string errorMessage))
            {
                MessageBox.Show("Receipt Printed");

                RefreshListViewData();
            }
            else
                MessageBox.Show("Error: " + errorMessage);
        }


        /// <summary>
        /// Gets ProcessOrderController to cancel the current order before clearing the listview data and resetting it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            ProcessOrderController.CancelOrder();

            RefreshListViewData();
        }


        private void addToPurchaseButton_Click(object sender, EventArgs e)
        {
            //If nothing selected then refresh ListView and return
            if (stockListView.SelectedItems.Count == 0)
            {
                RefreshListViewData();
                return;
            }

            int dummyVariable = 0;
            //If the currently selected row doesnt have a quantity string then add to the order (via ProcessORderController) with the value in the weight textbox
            if (!SelectedRowHasValidQuantity(ref dummyVariable))
            {
                //Checking that the value in the weight textbox represents a positive floating point value, before adding to the order.
                if ((float.TryParse(weightEntryTextbox.Text, out float weight)) && (weight > 0))
                {
                    ProcessOrderController.AddToOrder(_listSelectedIndex, weight);
                }
                else
                {
                    MessageBox.Show("Please enter a valid weight value");
                }   
            }
            //Otherwise, add to the order with the current amount in the amountLabel (if at least 1 item has been ordered).
            else if (_amountLabelValue > 0)
            {
                ProcessOrderController.AddToOrder(_listSelectedIndex, _amountLabelValue);

            }
            //Updating ListView to represent changes to ItemDatabase's StockList
            RefreshListViewData();
        }


        private void addQuantityButton_Click(object sender, EventArgs e)
        {
            //If nothing selected then show error messagebox and return
            if (stockListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("No rows selected");
                return;
            }

            int itemStockQuantity = 0;

            //Checks if the item in the current row actually has a quantity.
            //If it does then the current value of that quanitiy is decreased and the value in the row is updated to show this.
            if (SelectedRowHasValidQuantity(ref itemStockQuantity))
            {
                //Ensuring that cannot subtract below 0 stock quantity
                if (itemStockQuantity > 0)
                {
                    //Decreasing the int value of the quantity cell in the ListView and updating the cell with this new value
                    itemStockQuantity--;
                    stockListView.Items[_listSelectedIndex].SubItems[2].Text = itemStockQuantity.ToString();
                    //Increasing the amount purchased label value
                    _amountLabelValue++;
                }
            }
            else
                MessageBox.Show("Cannot have a quantity for a weighted item!");

            //Updating quantity label and keeping the list row selected
            quantityValueLabel.Text = _amountLabelValue.ToString();
            KeepItemSelected();
        }


        private void subtractQuantityButton_Click(object sender, EventArgs e)
        {
            //If nothing selected then show error messagebox and return
            if (stockListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("No rows selected");
                return;
            }

            int itemStockQuantity = 0;

            //Checks if the item in the current row actually has a quantity.
            //If it does then the current value of that quanitiy is decreased and the value in the row is updated to show this.
            if (SelectedRowHasValidQuantity(ref itemStockQuantity))
            {
                //Ensuring that cannot subtract below 0 current-order quantity.
                if (_amountLabelValue > 0)
                {
                    //Increasing the int value of the quantity cell in the ListView and updating the cell with the new value
                    itemStockQuantity++;
                    stockListView.Items[_listSelectedIndex].SubItems[2].Text = itemStockQuantity.ToString();

                    //Decreasing the amount purchased label value
                    _amountLabelValue--;
                }
            }
            //Otherwise the quanitity isnt changed and an error message is written to the screen.
            else
                MessageBox.Show("Cannot have a quantity for a weighted item!");

            //Updating quantity label and keeping the list row selected
            quantityValueLabel.Text = _amountLabelValue.ToString();
            KeepItemSelected();
        }


        /// <summary>
        /// Programmatically highlights the row of the stockListView at the position of the _listSelectedIndex field by setting the row's Selected property to be true.
        /// </summary>
        private void KeepItemSelected()
        {
            if ((_listSelectedIndex >= 0) && (_listSelectedIndex < stockListView.Items.Count))
            {
                stockListView.Items[_listSelectedIndex].Selected = true;
            }
        }


        /// <summary>
        /// Determines if the currently selected ListView row has a quantity string that represents an integer.
        /// Returns true if the quantity column of the selected row is an integer.
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns>
        /// true -> quantity column of the selected row is an integer
        /// false -> quantity column of selected row does not represent an integer
        /// </returns>
        private bool SelectedRowHasValidQuantity(ref int quantity)
        {
            return Int32.TryParse(stockListView.SelectedItems[0].SubItems[2].Text, out quantity);
        }


        /// <summary>
        /// Whenever a new list row is selected in the ListView, this method updates the form's _selectedIndex field
        /// to represent the idex of that selected item in the ItemDatabase List of Items.
        /// _listSelectedIndex is set to -1 when no field is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stockListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If a row is selected then change the label to represent this index
            if (stockListView.SelectedItems.Count != 0)
            {
                _listSelectedIndex = stockListView.Items.IndexOf(stockListView.SelectedItems[0]);
            }
            //Otherwise set label value to indicate no row selection.
            else
                _listSelectedIndex = -1;
        }
    }
}
