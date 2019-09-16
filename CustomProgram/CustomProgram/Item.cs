using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CustomProgram
{
    public abstract class Item
    {
        protected string _name;
        protected float _price;

        public Item(string name, float price)
        {
            _name = name;
            _price = price;
        }

        /// <summary>
        /// Reads details specific to the Item subclass from the StreamReader.
        /// </summary>
        /// <param name="streamReader">
        /// Reader for an item textfile. Must already be opened.
        /// </param>
        public abstract void Load(StreamReader streamReader);


        /// <summary>
        /// Writes specific Item type and all fields to the textfile of the StreamWriter.
        /// </summary>
        /// <param name="streamWriter">
        /// Writer for an item textfile. Must already be opened.
        /// </param>
        public abstract void Save(StreamWriter streamWriter);


        /// <summary>
        /// Purchase a given amount of this item. Item's stock levels are reduced if applicable.
        /// </summary>
        /// <param name="quantity">
        /// Amount of this item wished to be purchased.
        /// </param>
        /// <returns>
        /// Returns total cost for the passed quantity.
        /// </returns>
        public abstract float Purchase(float quantity);


        /// <summary>
        /// Restores the passed quantity to the item's stock level if applicable.
        /// </summary>
        /// <param name="quantity">
        /// Amount of the item to restore to stock.
        /// </param>
        public abstract void RefundPurchase(float quantity);


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }


        public float Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }


        public abstract string PriceString
        {
            get;
        }

        public abstract string QuantityString
        {
            get;
        }
    }
}
