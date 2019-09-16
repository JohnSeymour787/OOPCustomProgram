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
    public partial class ManageStockMenu : UserControl
    {
        public ManageStockMenu()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            stockDataGridView.Rows.Clear();
        }


        /// <summary>
        /// Override to clear all rows of the DataGridView and repopulate them with details of all items.
        /// </summary>
        public override void Refresh()
        {
            stockDataGridView.Rows.Clear();

            UIController.PopulateDataGridView(stockDataGridView.Rows);
        }


        private void ManageStockMenu_Load(object sender, EventArgs e)
        {
            Refresh();
        }


        private void updateRecordsButton_Click(object sender, EventArgs e)
        {
            //Item List index (and DataGridView row) counter
            int i = 0;

            foreach (DataGridViewRow row in stockDataGridView.Rows)
            {
                if (!row.CheckRowEmptyCells())
                {
                    //Checking for positive price value as string
                    if ((!Single.TryParse(row.Cells[1].ReturnPriceString(), out float price)) || (price < 0))
                    {
                        MessageBox.Show("Error: Enter a positive price number");
                        return;
                    }

                    //i < count AND row contents NOT null --> Replace list index
                    if (i < UIController.ItemCount())
                    {
                        //Updating item values and checking for a negative quantity string
                        if (!UIController.UpdateItemDetails(i, row.Cells[0].Value.ToString(), price, row.Cells[2].Value.ToString()))
                        {
                            MessageBox.Show("Error: Enter a positive integer for item quantity");
                            return;
                        }
                    }
                    //i >= count AND row contents NOT null --> Add new item to list
                    else
                    {
                        //Adding new item to UIController's ItemDatabase.StockList and checking for a negative quantity string
                        if (!UIController.AddNewItem(row.Cells[0].Value.ToString(), price, row.Cells[2].Value.ToString()))
                        {
                            MessageBox.Show("Error: Enter a positive integer for item quantity");
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
                    if (i < UIController.ItemCount())
                    {
                        UIController.RemoveItemAt(i);
                    }
                    //i > count AND null --> Do nothing and break, at end of dataView table
                    else
                        break;
                }
            }

            //Updating the Item textfile and repopulating the DataGridView
            UIController.UpdateItemFiles();
            Refresh();
        }
    }
}
