using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CustomProgram
{
    public static class UIController
    {
        private static ItemDatabase _itemDatabase;


        /// <summary>
        /// When passed a collection of Data-Grid-View-Rows, method will write the details of all items
        /// in the itemDataBase StockList to all rows, with 1 item per row.
        /// </summary>
        /// <param name="rows"></param>
        public static void PopulateDataGridView(DataGridViewRowCollection rows)
        {
            foreach (Item item in _itemDatabase.StockList)
            {
                rows.Add(item.Name, item.PriceString, item.QuantityString);
            }
        }


        /// <summary>
        /// When passed a ListView, method will write the details of all items
        /// in the itemDataBase StockList to all rows of the ListView, with 1 item per row.
        /// </summary>
        /// <param name="listView"></param>
        public static void PopulateListView(ListView listView)
        {
            ListViewItem listViewItem = null;

            foreach (Item item in _itemDatabase.StockList)
            {
                listViewItem = new ListViewItem(item.Name);
                listViewItem.SubItems.Add(item.PriceString);
                listViewItem.SubItems.Add(item.QuantityString);
                listView.Items.Add(listViewItem);
            }
        }


       /// <summary>
       /// Uses the ItemDatabase's SortBy method to sort its StockList based
       /// on the ItemSortType enumeral value passed in.
       /// A true parameter results in the StockList being sorted in ascending order.
       /// </summary>
       /// <param name="sortType"></param>
       /// <param name="sortAscending"></param>
       public static void SortItemList(ItemSortType sortType, bool sortAscending)
        {
            _itemDatabase.SortBy(sortType, sortAscending);
        }


        /// <summary>
        /// Opens a textfile from the location defined in Program class and updates it with
        /// the current ItemDatabase.StockList data.
        /// </summary>
        public static void UpdateItemFiles()
        {
            //Re-saving Item list to text file, in non-append mode
            StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + Program.ITEM_FILENAME, false);
            try
            {
                _itemDatabase.SaveData(streamWriter);
                streamWriter.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating item data: " + e.Message);
                streamWriter.Close();
            }
        }


        /// <summary>
        /// Creates a new item child class with all passed in parameters as fields.
        /// Uses the quantityString parameter to determine what item child class to create.
        /// Returns true for successful operation.
        /// Returns false if the quantityString represented a negative number.
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="price"></param>
        /// <param name="quantityString"></param>
        /// <returns>
        /// true -> If a new item was successfully created and added to the ItemDatabase.StockList
        /// false -> If a new item was not created due to the quantity representing a negative value.
        /// </returns>
        public static bool AddNewItem(string itemName, float price, string quantityString)
        {
            Item newItem;

            //Checking if the string in the "Amount" cell can be read as a positive integer
            //If so, then item is a Standard Item child class
            if (IsStandardItem(quantityString, out int amount))
            {
                if (amount <= 0) return false;

                newItem = _itemDatabase.CreateItem(ItemType.Standard, itemName, price);
                (newItem as StandardItem).Quantity = amount;
            }
            //Otherwise, item is a Weighted Item child
            else
                newItem = _itemDatabase.CreateItem(ItemType.Weighted, itemName, price);

            _itemDatabase.StockList.Add(newItem);

            return true;
        }


        /// <summary>
        /// Removes an item from the ItemDataBase.StockList at the passed in index
        /// </summary>
        /// <param name="index"></param>
        public static void RemoveItemAt(int index)
        {
            if ((index < ItemCount()) && (index > 0))
                _itemDatabase.StockList.RemoveAt(index);
        }


        /// <summary>
        /// Updates the values of the Item in the ItemDatabase's StockList at the given index.
        /// Uses the quantityString parameter to determine if the Item at the index has a quantity field.
        /// If it does, then this field is also updated.
        /// Returns true for successful operation.
        /// Returns false if the quantityString parameter represents a negative number.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="itemName"></param>
        /// <param name="price"></param>
        /// <param name="quantityString"></param>
        /// <returns>
        /// true -> Item at the index successfully updated with all details
        /// false -> Quantity string represents a negative number and thus the Item's quantity field was not updated.
        /// </returns>
        public static bool UpdateItemDetails(int index, string itemName, float price, string quantityString)
        {
            _itemDatabase.StockList[index].Name = itemName;
            _itemDatabase.StockList[index].Price = price;

            if (IsStandardItem(quantityString, out int quantity))
            {
                if (quantity > 0)
                    (_itemDatabase.StockList[index] as StandardItem).Quantity = quantity;
                else
                    return false;
            }

            return true;
        }


        /// <summary>
        /// Attempts to convert a quantityString to an integer by parsing it, thus determining if the item is a standard item child class with a 
        /// quantity or another item child class that doesn't require a quantity.
        /// Returns true if the string can be converted to an integer, meaning it is a Standard Item we are dealing with.
        /// Returns false if the item is not a StandardItem child class.
        /// </summary>
        /// <param name="quantityString"></param>
        /// <param name="quantity"></param>
        /// <returns>
        /// true -> Item being dealt with is a StandardItem child class
        /// false -> Item being dealt with is not a StandardItem child class 
        /// </returns>
        private static bool IsStandardItem(string quantityString, out int quantity)
        {
            return Int32.TryParse(quantityString, out quantity);
        }


        /// <summary>
        /// Returns the number of Items in the ItemDatabase's StockList.
        /// </summary>
        /// <returns></returns>
        public static int ItemCount()
        {
             return _itemDatabase.StockList.Count();
        }


        public static ItemDatabase ItemDatabase
        {
            get
            {
                return _itemDatabase;
            }
            set
            {
                _itemDatabase = value;
            }
        }
    }
}
