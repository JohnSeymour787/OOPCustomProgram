using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CustomProgram
{
    public class ItemDatabase: IDataBase
    {
        private List<Item> _stockList = new List<Item>();
        static private Dictionary<ItemType, Type> _itemTypes = new Dictionary<ItemType, Type>();

        public ItemDatabase(StreamReader streamReader)
        {
            LoadData(streamReader);
        }

        /// <summary>
        /// Generates a basic Item child class based on the ItemType enumeral passed in.
        /// Child has inherited fields for itemName and itemPrice based on these respective passed in values.
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="itemName"></param>
        /// <param name="itemPrice"></param>
        /// <returns>
        /// Returns a reference to a new specialised Item object.
        /// </returns>
        public Item CreateItem(ItemType itemType, string itemName, float itemPrice)
        {
            return (Item)Activator.CreateInstance(_itemTypes[itemType], new object[] { itemName , itemPrice });
        }


        /// <summary>
        /// Loads data for each item from a textfile via a passed in StreamReader object.
        /// Creates an appropriate Item child class based on the text it reads.
        /// Then adds the new item object to the StockList.
        /// </summary>
        /// <param name="streamReader"></param>
        public void LoadData(StreamReader streamReader)
        {
            ItemType itemType;
            Item itemReadIn;
            string itemName;
            float itemPrice;

            while (!streamReader.EndOfStream)
            {
                itemType = streamReader.ReadItemType();
                itemName = streamReader.ReadLine();
                itemPrice = streamReader.ReadFloat();

                //Specialised item creation
                itemReadIn = CreateItem(itemType, itemName, itemPrice);

                //Specialised item load
                itemReadIn.Load(streamReader);

                _stockList.Add(itemReadIn);
            }
        }

        /// <summary>
        /// Saves each item in the stocklist to a textfile from the passed in streamWriter.
        /// </summary>
        /// <param name="streamWriter"></param>
        public void SaveData(StreamWriter streamWriter)
        {
            foreach (Item item in _stockList)
            {
                item.Save(streamWriter);
            }
        }


        /// <summary>
        /// Sorts the StockList field of this object based on the passed in SortType enumerated type value.
        /// A true parameter results in the StockList being sorted in ascending order.
        /// </summary>
        /// <param name="sortType"></param>
        /// <param name="sortAscending"></param>
        public void SortBy(ItemSortType sortType, bool sortAscending)
        {
            if (_stockList.Count() <= 1) return;

            switch (sortType)
            {
                //Sorting by item name using the List object's built-in sort method
                case ItemSortType.Name:
                    if (sortAscending)
                        _stockList.Sort((x, y) => x.Name.CompareTo(y.Name));
                    else
                        _stockList.Sort((x, y) => y.Name.CompareTo(x.Name));
                    break;

                //Sorting by item price using the List object's built-in sort method
                case ItemSortType.Price:
                    if (sortAscending)
                        _stockList.Sort((x, y) => x.Price.CompareTo(y.Price));
                    else
                        _stockList.Sort((x, y) => y.Price.CompareTo(x.Price));
                    break;

                //Sorting by amount manually such that all N/A amount items (ie, all non-standard items) are always moved to the end of the list
                //All other standard items with actual amounts are then sorted.
                case ItemSortType.Amount:

                    //Bubble sorting by the quantity field of the current and next item, if possible.
                    for (int i = _stockList.Count; i > 0; i--)
                    {
                        for (int j = 0; j < i - 1; j++)
                        {
                            //If next item does not have a quantity then move it to the back of the list
                            if (!Int32.TryParse(_stockList[j + 1].QuantityString, out int quantity))
                            {
                                _stockList.PushToBack(j + 1);
                            }
                            //Otherwise if current item doesn't have a quantity then move it to the back of the list
                            //(this is done second because it is more important that the next item is sortable than the current (in case we are
                            //nearing the end of the list and accessing items with other N/A values)).
                            else if (!Int32.TryParse(_stockList[j].QuantityString, out quantity))
                            {
                                _stockList.PushToBack(j);
                            }
                            //Otherwise, both current and next items in the list are sortable.
                            else
                            {
                                if (sortAscending)
                                {
                                    if (quantity > (_stockList[j + 1] as StandardItem).Quantity)
                                    {
                                        _stockList.SwapIndexes(j, j + 1);
                                    }
                                }
                                else
                                {
                                    if (quantity < (_stockList[j + 1] as StandardItem).Quantity)
                                    {
                                        _stockList.SwapIndexes(j, j + 1);
                                    }
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Used to assign an ItemType enumeral to a Typeof(Item Child) object type in the static dictionary for 
        /// creation of specific items based on an ItemType enum.
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="itemClass"></param>
        static public void AssignItemType(ItemType itemType, Type itemClass)
        {
            _itemTypes[itemType] = itemClass;
        }

        public List<Item> StockList
        {
            get
            {
                return _stockList;
            }
        }
    }
}
