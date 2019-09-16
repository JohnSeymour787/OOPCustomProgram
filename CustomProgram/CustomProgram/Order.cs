using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CustomProgram
{
    /// <summary>
    /// Contains details of a single item purchase including item name, quantity purchased, and purchase cost.
    /// </summary>
    struct Purchase
    {
        public string name;
        public float quantity;
        public float cost;
    }


    public class Order
    {
        private List<Purchase> _purchases = new List<Purchase>();

        public Order()
        {

        }

        /// <summary>
        /// Calculates the total cost of a single item sold in a list of purchases.
        /// </summary>
        /// <param name="itemName">
        /// Name of the item to calculate total revenue generated for.
        /// </param>
        /// <returns>
        /// Returns total cost of a single item in this order's purchase list.
        /// </returns>
        public float TotalItemRevenue(string itemName)
        {
            float total = 0;

            foreach (Purchase purchase in _purchases)
            {
                if (purchase.name == itemName)
                {
                    total += purchase.cost;
                }
            }
            return total;
        }


        /// <summary>
        /// Calculates the total quantity of a single item sold in a list of purchases.
        /// </summary>
        /// <param name="itemName">
        /// Name of the item to calculate total quantity sold for.
        /// </param>
        /// <returns>
        /// Returns total quantity sold of a single item in this order's purchase list.
        /// </returns>
        public float TotalItemQuantitySold(string itemName)
        {
            float total = 0;

            foreach (Purchase purchase in _purchases)
            {
                if (purchase.name == itemName)
                {
                    total += purchase.quantity;
                }
            }
            return total;
        }


        /// <summary>
        /// Adds a new item purchase to the current order with the passed name, cost, and quantity parameters.
        /// Item added to purchase list.
        /// </summary>
        /// <param name="name">
        /// Name of the item that has been purchased
        /// </param>
        /// <param name="cost">
        /// Total cost of the item that has been bought
        /// </param>
        /// <param name="quantity">
        /// Quantity of the item that has been purchased
        /// </param>
        public void AddPurchase(string name, float cost, float quantity)
        {
            Purchase purchase = new Purchase();
            purchase.name = name;
            purchase.cost = cost;
            purchase.quantity = quantity;

            _purchases.Add(purchase);
        }


        /// <summary>
        /// Restores the quantity of an item from each purchase made of that item, in the purchase list.
        /// </summary>
        /// <param name="itemToRefund">
        /// Item that needs to be refunded.
        /// </param>
        public void RefundOrder(Item itemToRefund)
        {
            foreach(Purchase purchase in _purchases)
            {
                if (purchase.name == itemToRefund.Name)
                {
                    itemToRefund.RefundPurchase(purchase.quantity);
                }
            }
        }

        /// <summary>
        /// Calculates the total cost of this single customer order for all items.
        /// </summary>
        /// <returns>
        /// Returns total cost of all purchases in purchase list
        /// </returns>
        public float CalculateOrderTotal()
        {
            float orderTotal = 0;

            foreach (Purchase purchase in _purchases)
            {
                orderTotal += purchase.cost;
            }

            return orderTotal;
        }

        
        /// <summary>
        /// Prints a receipt containing details of all purchases in this order object, including the total order cost.
        /// </summary>
        /// <param name="streamWriter">
        /// StreamWriter used to print to the receipts textfile. Must be opened before calling this method.
        /// </param>
        public void PrintReciept(StreamWriter streamWriter)
        {
            streamWriter.WriteLine("Thank you for shopping at Store!");
            streamWriter.WriteLine();
            foreach (Purchase purchase in _purchases)
            {
                streamWriter.WriteLine(purchase.name + "\t\t$" + purchase.cost.ToString("F2"));
                streamWriter.Write(" Qty: " + purchase.quantity.ToString("0.##"));

                //If the quantity is not an whole number then it is a weight so add "kg" to the string and end the line
                if (purchase.quantity % 1 != 0)
                    streamWriter.WriteLine("kg");
                else
                    streamWriter.WriteLine();

            }

            streamWriter.WriteLine();
            streamWriter.WriteLine("Total:\t\t$" + CalculateOrderTotal().ToString("F2"));
            streamWriter.WriteLine();
            streamWriter.WriteLine();
        }


        /// <summary>
        /// Reads all purchase details for a single order written to the receipts textfile and saves this data in its _purchases field.
        /// </summary>
        /// <param name="streamReader">
        /// StreamReader used to read the receipts textfile. Must be opened before calling this method.
        /// </param>
        public void LoadOrder(StreamReader streamReader)
        {
            Purchase purchase;

            streamReader.ReadLine();
            streamReader.ReadLine();

            //Reading each line of the receipt until a line space is detected, indicating the end of that
            //receipt order.
            while((char)streamReader.Peek() != '\r')
            {
                purchase = new Purchase();
                streamReader.ReadReceiptLine(ref purchase.name, ref purchase.cost);
                purchase.quantity = streamReader.ReadReceiptQuantity();

                _purchases.Add(purchase);
            }

            //Reading over the next 4 lines (including the unneeded total cost line) of the receipt to 
            //end the reading for this order.
            streamReader.ReadLine();
            streamReader.ReadLine();    //Total cost line
            streamReader.ReadLine();
            streamReader.ReadLine();
        }
    }
}
