using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CustomProgram
{
    class StandardItem: Item
    {
        private int _stockQuantity;

        //For creating a Standard item loaded in from a file of abstract Item types
        public StandardItem(string name, float price) : base(name, price)
        {

        }


        /// <summary>
        /// Detracts the amount from this Item's stock quantity before returning the total cost 
        /// for the amount bought.
        /// </summary>
        /// <param name="amount">
        /// Amount of the item wished to be bought.
        /// </param>
        /// <returns>
        /// Returns total cost of the purchase.
        /// </returns>
        public override float Purchase(float amount)
        {
            _stockQuantity -= (int)amount;
            return _price * amount;
        }


        /// <summary>
        /// Adds a quantity back to this item's stock quantity field.
        /// </summary>
        /// <param name="quantity">
        /// Amount of the item wished to be refunded.
        /// </param>
        public override void RefundPurchase(float quantity)
        {
            _stockQuantity += (int)quantity;
        }


        /// <summary>
        /// Loads in data for fields specific to this object type using the StreamReader at an opened textfile.
        /// </summary>
        /// <param name="streamReader">
        /// Reader for an item textfile. Must already be opened.
        /// </param>
        public override void Load(StreamReader streamReader)
        {
            _stockQuantity = streamReader.ReadInteger();
        }


        /// <summary>
        /// Item saves its type and other fields to the textfile opened by the StreamWriter
        /// </summary>
        /// <param name="streamWriter">
        /// Writer for an item textfile. Must already be opened.
        /// </param>
        public override void Save(StreamWriter streamWriter)
        {
            streamWriter.WriteLine(ItemType.Standard);
            streamWriter.WriteLine(_name);
            streamWriter.WriteLine(_price.ToString("F"));
            streamWriter.WriteLine(_stockQuantity);
        }


        public override string PriceString
        {
            get
            {
                return "$" + _price.ToString("F");
            }
        }


        public override string QuantityString
        {
            get
            {
                return _stockQuantity.ToString();
            }
        }


        public int Quantity
        {
            get
            {
                return _stockQuantity;
            }
            set
            {
                _stockQuantity = value;
            }
        }
    }
}
