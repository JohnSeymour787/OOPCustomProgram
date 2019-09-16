using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CustomProgram
{  
    static public class ProcessOrderController
    {
        private static List<Order> _orderList = new List<Order>();
        private static Order _customerOrder;



        /// <summary>
        /// Prepares ItemDatabase for a new order by sorting its StockList and creating a new Order object.
        /// </summary>
        public static void NewOrder()
        {
            //Sorting the item list by item name in ascending order
            UIController.ItemDatabase.SortBy(ItemSortType.Name, true);

            _customerOrder = new Order();
        }


        /// <summary>
        /// Adds an item from the ItemDatabase Stocklist to the Order object's list of purchases.  
        /// </summary>
        /// <param name="itemIndex">
        /// Index of the item in UIController's ItemDatabase.StockList which will be added to the order.
        /// </param>
        /// <param name="quantity">
        /// Value used to determine the total cost of the order by calling the Item's Purchase method.
        /// </param>
        public static void AddToOrder(int itemIndex, float quantity)
        {
            //Adding single-item purchase details to the Order's Purchase list.             //Adding the total cost of the purchase based on quantity. If standard item, will also reduce stock quantity. 
            _customerOrder.AddPurchase(UIController.ItemDatabase.StockList[itemIndex].Name, UIController.ItemDatabase.StockList[itemIndex].Purchase(quantity), quantity);
        }


        /// <summary>
        /// Loops through all items in the ItemDatabase StockList, asking the Order to refund each item, returning their stock quanitity.
        /// Then creates a new Order object, removing the old one with its purchase list.
        /// </summary>
        public static void CancelOrder()
        {
            foreach (Item item in UIController.ItemDatabase.StockList)
            {
                _customerOrder.RefundOrder(item);
            }

            NewOrder();
        }


        /// <summary>
        /// Attempts to print the order as a receipt for the customer to have.
        /// Also updates the Item details text file with the current details in ItemDatabase's list of items.
        /// </summary>
        /// <param name="errorMessage">
        /// If returns false, this parameter will contain the execption error message generated. 
        /// </param>
        /// <returns>
        /// true -> Successfully wrote to Receipt and Item textfiles, added to order, and ready for a new order.
        /// false -> Error occurred during writing to either textfile.
        /// </returns>
        public static bool OrderCompleteSuccess(out string errorMessage)
        {
            StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + Program.RECEIPT_FILENAME, true);

            try
            {
                _customerOrder.PrintReciept(streamWriter);
                streamWriter.Close();

                errorMessage = "";

                streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + Program.ITEM_FILENAME, false);
                UIController.ItemDatabase.SaveData(streamWriter);
                streamWriter.Close();
            }
            catch(Exception e)
            {
                streamWriter.Close();
                errorMessage = e.Message;
                return false;
            }

            //Adding to the order list and updating SalesReportController's orderlist
            _orderList.Add(_customerOrder);
            SalesReportController.OrderList = _orderList;


            NewOrder();

            return true;
        }


        public static List<Order> OrderList
        {
            get
            {
                return _orderList;
            }
        }
    }
}
