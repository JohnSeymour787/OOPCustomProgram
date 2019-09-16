using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProgram
{
    class WeightedItem: Item
    {
        public WeightedItem(string name, float pricePerKg) : base(name, pricePerKg)
        {

        }


        /// <summary>
        /// Returns the total cost for purchasing the weight of this item.
        /// </summary>
        /// <param name="weight">
        /// Measured weight of the item being bought.
        /// </param>
        /// <returns>
        /// Returns total cost of the purchase.
        /// </returns>
        public override float Purchase(float weight)
        {
            return _price * weight;
        }


        /// <summary>
        /// Can be used to return quantity back to the stock of this item.
        /// (Currently just used to inherit from the Item class)
        /// </summary>
        /// <param name="quantity"></param>
        public override void RefundPurchase(float quantity)
        {

        }


        /// <summary>
        /// Used to load in this weightedItem's own fields (such as stock quanitity in tonnes, if this feature was chosen to be added)
        /// (Currently just used to inherit from the Item class).
        /// </summary>
        /// <param name="streamReader">
        /// Reader for an item textfile. Must already be opened.
        /// </param>
        public override void Load(StreamReader streamReader)
        {

        }


        /// <summary>
        /// Item saves its type and other fields to the textfile opened by the StreamWriter
        /// </summary>
        /// <param name="streamWriter">
        /// Writer for an item textfile. Must already be opened.
        /// </param>
        public override void Save(StreamWriter streamWriter)
        {
            streamWriter.WriteLine(ItemType.Weighted);
            streamWriter.WriteLine(_name);
            streamWriter.WriteLine(_price.ToString("F"));
        }


        public override string PriceString
        {
            get
            {
                return "$" + _price.ToString("F") + "/kg";
            }
        }


        public override string QuantityString
        {
            get
            {
                return "N/A";
            }
        }
    }
}
