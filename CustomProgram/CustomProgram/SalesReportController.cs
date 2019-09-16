using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CustomProgram
{
    static public class SalesReportController
    {
        private static List<Order> _orderList;


        /// <summary>
        /// When passed a ListView, method will write the details for total amount of each item sold, as well as the total cost made in
        /// sales of this item. 
        /// </summary>
        /// <param name="listView">
        /// Each item and its sales details are written to each row of this ListView.
        /// </param>
        private static void PopulateListView(ListView listView)
        {
            //Sorting the item list by item name in ascending order
            UIController.SortItemList(ItemSortType.Name, true);

            ListViewItem listViewItem = null;

            foreach (Item item in UIController.ItemDatabase.StockList)
            {
                listViewItem = new ListViewItem(item.Name);
                listViewItem.SubItems.Add(TotalItemQuantitySold(item.Name));
                listViewItem.SubItems.Add(TotalItemRevenue(item.Name));

                listView.Items.Add(listViewItem);
            }
        }

        /// <summary>
        /// Populates the ListView with sales details from all orders in ProcessOrderController's OrderList for the program's current runtime session.
        /// </summary>
        /// <param name="listView"></param>
        public static void SessionSalesReport(ListView listView)
        {
            if (_orderList == null)
            {
                return;
            }

            _orderList = ProcessOrderController.OrderList;

            PopulateListView(listView);
        }

        /// <summary>
        /// Populates the ListView with sales details from all orders ever made, by reading the receipts textfile.
        /// </summary>
        /// <param name="listView"></param>
        public static void EntireSalesReport(ListView listView)
        {
            _orderList = ReadReceiptsFile();

            if (_orderList == null)
            {
                return;
            }

            PopulateListView(listView);
        }

        /// <summary>
        /// Reads the receipts textfile of all customer receipts with all order details, putting them into a list of orders.
        /// </summary>
        /// <returns>
        /// Returns a list of Order objects containing purchase details of all customers served.
        /// Null -> if operation failed due to a file reading error.
        /// </returns>
        private static List<Order> ReadReceiptsFile()
        {
            List<Order> orderHistory = new List<Order>();
            Order orderRead;

            StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + Program.RECEIPT_FILENAME);

            try
            {
                while (!streamReader.EndOfStream)
                {
                    orderRead = new Order();
                    //Reading order details for a single customer receipt
                    orderRead.LoadOrder(streamReader);
                    orderHistory.Add(orderRead);
                }
                streamReader.Close();
            }
            catch (Exception e)
            {
                streamReader.Close();
                MessageBox.Show("Error while reading receipts file: " + e.Message);
                return null;
            }

            return orderHistory;
        }



        /// <summary>
        /// Returns a string of the number of customers served in the current list of orders.
        /// </summary>
        /// <returns>
        /// String representing a number of elements in order list.
        /// Empty string if there is no order list currently.
        /// </returns>
        public static string CustomersServed()
        {
            if (_orderList != null)
            {
                return _orderList.Count.ToString();
            }
            else
                return "";
        }

        /// <summary>
        /// Returns a string of total revenue generated from the current list of orders.
        /// </summary>
        /// <returns>
        /// String representing the total of all order totals in the current list of orders.
        /// Empty string if there is no order list currently.
        /// </returns>
        public static string TotalRevenue()
        {
            if (_orderList != null)
            {
                return "$" + CalculateTotalOrderRevenue().ToString("F");
            }
            return "";
        }



        /// <summary>
        /// Calculates the grand total of all orders
        /// </summary>
        /// <returns>
        /// Floating point number for the grand total of revenue generated from all orders.
        /// </returns>
        private static float CalculateTotalOrderRevenue()
        {
            float totalRevenue = 0;
            //Summing all orders's total costs.
            foreach (Order order in _orderList)
            {
                totalRevenue += order.CalculateOrderTotal();
            }
            return totalRevenue;
        }


        /// <summary>
        /// Calculates total revenue generated of a single item.
        /// </summary>
        /// <param name="itemName">
        /// String name of the item for which to find total revenue for.
        /// </param>
        /// <returns>
        /// String representing a price.
        /// </returns>
        private static string TotalItemRevenue(string itemName)
        {
            float totalSingleItemRevenue = 0;
            foreach (Order order in _orderList)
            {
                //Getting each order to calculate the total revenue made from its purchase list, for this item.
                totalSingleItemRevenue += order.TotalItemRevenue(itemName);
            }
            return "$" + totalSingleItemRevenue.ToString("F");
        }


        /// <summary>
        /// Calculates the total quantity of a single item sold.
        /// </summary>
        /// <param name="itemName">
        /// String name of the item for which to find total quantity for.
        /// </param>
        /// <returns>
        /// String representing a quantity.
        /// </returns>
        private static string TotalItemQuantitySold(string itemName)
        {
            float totalSingleItemQuantity = 0;
            foreach (Order order in _orderList)
            {
                //Getting each order to calculate the total quantity sold from its purchase list, for this item.
                totalSingleItemQuantity += order.TotalItemQuantitySold(itemName);
            }

            //If the quantity is not a whole number then it will be a weight
            if (totalSingleItemQuantity % 1 != 0)
                return totalSingleItemQuantity.ToString("F") + " kg";
            else
                return totalSingleItemQuantity.ToString();
        }


        public static List<Order> OrderList
        {
            set
            {
                _orderList = value;
            }
        }
    }
}
