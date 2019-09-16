//
//
//					Program Class
//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CustomProgram
{
    /// <summary>
    /// Contains values representing all user sub-types
    /// </summary>
    public enum UserType { Manager, Cashier, NoType };
   
    /// <summary>
    /// Contains values representing all item sub-types
    /// </summary>
    public enum ItemType { Weighted, Standard, NoType };
    
    /// <summary>
    /// Contains values representing different ways a list of items can be sorted
    /// </summary>
    public enum ItemSortType { Name, Price, Amount };



    static class Program
    {
        /// <summary>
        /// String representing file location for a textfile containing user details
        /// </summary>
        public const string USER_FILENAME = @"\User List.txt";

        /// <summary>
        /// String representing file location for a textfile containing item details
        /// </summary>
        public const string ITEM_FILENAME = @"\Item List.txt";

        /// <summary>
        /// String representing file location for a textfile containing customer receipts
        /// </summary>
        public const string RECEIPT_FILENAME = @"\Receipts.txt";


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitialiseDatabases();


            ItemDatabase itemDatabase;
            UserDatabase userDatabase;

            StreamReader streamReader = new StreamReader(Directory.GetCurrentDirectory() + USER_FILENAME);

            try
            {
                userDatabase = new UserDatabase(streamReader);
                streamReader.Close();

                streamReader = new StreamReader(Directory.GetCurrentDirectory() + ITEM_FILENAME);
                itemDatabase = new ItemDatabase(streamReader);
                streamReader.Close();
            }
            catch (Exception e)
            {
                //Cannot close in a finally block because needs to return when error is caught
                streamReader.Close();
                MessageBox.Show("Error Reading File: " + e.Message);
                return;
            }

            InitialiseControllers(itemDatabase, userDatabase);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new LoginScreen());

            Application.ExitThread();
        }


        /// <summary>
        /// Assigns object sub-types to enumerated types in UserDatabase and ItemDatabase dictionaries
        /// </summary>
        static void InitialiseDatabases()
        {
            UserDatabase.AssignUserType(UserType.Cashier, typeof(Cashier));
            UserDatabase.AssignUserType(UserType.NoType, typeof(Cashier));
            UserDatabase.AssignUserType(UserType.Manager, typeof(Manager));

            ItemDatabase.AssignItemType(ItemType.Standard, typeof(StandardItem));
            ItemDatabase.AssignItemType(ItemType.Weighted, typeof(WeightedItem));
        }

        
        /// <summary>
        /// Initialises the primary controllers with UserDatabase and ItemDatabases
        /// </summary>
        /// <param name="itemDatabase"></param>
        /// <param name="userDatabase"></param>
        static void InitialiseControllers(ItemDatabase itemDatabase, UserDatabase userDatabase)
        {
            UIController.ItemDatabase = itemDatabase;
            ProcessUserController.UserDatabase = userDatabase;
        }
    }
}



//
//
//					User Class
//
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CustomProgram
{
    public abstract class User
    {
        private string _username;
        private string _password;
        private string _fullName;
        private int _employeeNumber;
        private UserType _role;

        public User(string username, string password, string fullName, int employeeNumber, UserType role)
        {
            _username = username;
            _password = password;
            _fullName = fullName;
            _employeeNumber = employeeNumber;
            _role = role;
        }


        /// <summary>
        /// Opens the appropriate main menu depending on the specific user sub-type.
        /// </summary>
        public abstract void OpenUserMenu();


        /// <summary>
        /// Writes all user data including their specific sub-type to the StreamWriter textfile.
        /// </summary>
        /// <param name="streamWriter">
        /// Writer for a user textfile. Must already be opened.
        /// </param>
        public virtual void SaveData(StreamWriter streamWriter)
        {
            streamWriter.WriteLine(_username);
            streamWriter.WriteLine(_password);
            streamWriter.WriteLine(_fullName);
            streamWriter.WriteLine(_employeeNumber);
            streamWriter.WriteLine(_role);
        }


        public UserType Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
            }
        }


        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }


        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }


        public int ID
        {
            get
            {
                return _employeeNumber;
            }
            set
            {
                _employeeNumber = value;
            }
        }


        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }
    }
}


//
//
//					Cashier Class
//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProgram
{
    class Cashier: User
    {
        public Cashier(string username, string password, string fullName, int employeeNumber) : base (username, password, fullName, employeeNumber, UserType.Cashier)
        {

        }

        /// <summary>
        /// Runs a new cashier menu with the name of this user.
        /// </summary>
        public override void OpenUserMenu()
        {
            new CashierMenu(FullName).ShowDialog();
        }
    }
}


//
//
//					Manager Class
//
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProgram
{
    class Manager: User
    {
        public Manager(string username, string password, string fullName, int employeeNumber) : base(username, password, fullName, employeeNumber, UserType.Manager)
        {

        }

        /// <summary>
        /// Runs a new manager menu with the name of this user.
        /// </summary>
        public override void OpenUserMenu()
        {
            (new ManagerMenu(FullName)).ShowDialog();
        }
    }
}


//
//
//					UserDatabase Class
//
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CustomProgram
{
    /// <summary>
    /// Contains basic details for any user type including employee name, username, password, ID number, and role.
    /// </summary>
    public struct UserInfo
    {
        public string employeeName;
        public string username;
        public string password;
        public int ID;
        public UserType role;
    }

    public class UserDatabase: IDataBase
    {
        private List<User> _userList = new List<User>();
        static private Dictionary<UserType, Type> _userTypes = new Dictionary<UserType, Type>();

        public UserDatabase(StreamReader streamReader)
        {
            LoadData(streamReader);
        }


        /// <summary>
        /// Generates a User child class based on the UserInfo structure passed in.
        /// Child type is defined by this structure's UserType enumeral's value.
        /// Child user has inherited fields for username, password, fullname, and ID number based on these respective values in the structure.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public User CreateUser(UserInfo userInfo)
        {
            return (User)Activator.CreateInstance(_userTypes[userInfo.role], new object[] { userInfo.username, userInfo.password, userInfo.employeeName, userInfo.ID});
        }


        /// <summary>
        /// Loads data for each user from a textfile via a passed in StreamReader object.
        /// Creates an appropriate User child class based on the text it reads.
        /// Then adds the new user object to the UserList.
        /// </summary>
        /// <param name="streamReader"></param>
        public void LoadData(StreamReader streamReader)
        {
            User userReadIn;

            UserInfo userInfo = new UserInfo();

            while (!streamReader.EndOfStream)
            {
                userInfo.username= streamReader.ReadLine();
                userInfo.password = streamReader.ReadLine();
                userInfo.employeeName = streamReader.ReadLine();
                userInfo.ID = streamReader.ReadInteger();
                userInfo.role = streamReader.ReadUserType();

                //If there isn't already a user in the list with the same ID or username,
                //Then create a new user object and add it to the list.
                if (!CheckExistingUser(userInfo.ID, userInfo.username))
                {
                    userReadIn = CreateUser(userInfo);
                    _userList.Add(userReadIn);
                }
            }
        }


        /// <summary>
        /// Saves each User in the UserList to a textfile via the passed in streamWriter.
        /// </summary>
        /// <param name="streamWriter"></param>
        public void SaveData(StreamWriter streamWriter)
        {
            foreach (User user in _userList)
            {
                user.SaveData(streamWriter);
            }
        }


        /// <summary>
        /// Loops through all User objects in the UserList checking if a user already exists
        /// with the same ID number or username values passed in.
        /// Returns true if there is a user that matches either of these parameters. 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        /// <returns>
        /// true -> User in UserList that matches parameters.
        /// false -> No match found.
        /// </returns>
        public bool CheckExistingUser(int userId, string username)
        {
            foreach (User user in _userList)
            {
                if ((user.ID == userId) || (user.UserName == username))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Used to assign a UserType enumeral to a Typeof(User Child) object type in the static dictionary for 
        /// creation of specific users based on a UserType enumeral value.
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="userClass"></param>
        static public void AssignUserType(UserType userType, Type userClass)
        {
            _userTypes[userType] = userClass;
        }


        /// <summary>
        /// Checks if there is a user in the UserList that has fields matching both
        /// values for username and password, simultaneously and identically.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>
        /// User object if a user with matching username and password is found.
        /// Null pointer if no user is found.
        /// </returns>
        public User CheckValidLogin(string username, string password)
        {
            foreach (User user in _userList)
            {
                if ((user.UserName == username) && (user.Password == password))
                {
                    return user;
                }
            }
            return null;
        }


        public List<User> UserList
        {
            get
            {
                return _userList;
            }
        }
    }
}


//
//
//					Item Class
//
//
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


//
//
//			StandardItem class		
//
//
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


//
//
//					WeightedItem class
//
//
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


//
//
//					ItemDatabase class
//
//
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


//
//
//					IDataBase Interface
//
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CustomProgram
{
    interface IDataBase
    {
        /// <summary>
        /// Loads data from the StreamReader for the database's primary list
        /// </summary>
        /// <param name="streamReader">
        /// StreamReader used to read in the data for the database. Must be opened before passing.
        /// </param>
        void LoadData(StreamReader streamReader);


        /// <summary>
        /// Saves data from the database's primary list to the StreamWriter
        /// </summary>
        /// <param name="streamWriter">
        /// StreamWriter used to write to the textfile for the database's data. Must be opened before passing.
        /// </param>
        void SaveData(StreamWriter streamWriter);
    }
}

//
//
//					Order class
//
//

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


//
//
//				ExtensionMethods class	
//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CustomProgram
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Reads a line as a string and attempts to convert it to a UserType enumeral value.
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns>
        /// Returns an enumerated value of UserType type if successful based on the string read.
        /// If unsuccessful -> Returns UserType.NoType.
        /// </returns>
        public static UserType ReadUserType(this StreamReader streamReader)
        {
            string userTypeString = streamReader.ReadLine();

            if (Enum.TryParse(userTypeString, true, out UserType userType))
            {
                return userType;
            }
            else
                return UserType.NoType;
        }


        /// <summary>
        /// Reads a line as a string and attempts to convert it to an ItemType enumeral value.
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns>
        /// Returns an enumerated value of ItemType type if successful based on the string read.
        /// If unsuccessful -> Returns ItemType.NoType.
        /// </returns>
        public static ItemType ReadItemType(this StreamReader streamReader)
        {
            string itemTypeString = streamReader.ReadLine();

            if (Enum.TryParse(itemTypeString, true, out ItemType itemType))
            {
                return itemType;
            }
            else
                return ItemType.NoType;
        }


        /// <summary>
        /// Reads a line as a string and converts in to an integer value.
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns>
        /// Integer value that was represented by a string.
        /// </returns>
        public static int ReadInteger(this StreamReader streamReader)
        {
            return Convert.ToInt32(streamReader.ReadLine());
        }


        /// <summary>
        /// Reads a line as a string and converts in to a floating point value.
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns>
        /// Floating point value that was represented by a string.
        /// </returns>
        public static float ReadFloat(this StreamReader streamReader)
        {
            return Convert.ToSingle(streamReader.ReadLine());
        }


        /// <summary>
        /// Reads a single line of a receipts textfile for details of the name of an item purchased and the total cost it was bought for.
        /// </summary>
        /// <param name="streamReader"></param>
        /// <param name="orderName">
        /// Name of the item purchased.
        /// </param>
        /// <param name="orderCost">
        /// Total cost of the item purchased (including multiple quantities bought)
        /// </param>
        public static void ReadReceiptLine(this StreamReader streamReader, ref string orderName, ref float orderCost)
        {
            string line = streamReader.ReadLine();

            //Saving orderName parameter as a string up to the first tab of the line.
            orderName = line.Substring(0, line.IndexOf('\t'));

            //Saving orderCost as a substring from the character after the first '$' char to the end of the line and converting to a floating point value.
            orderCost = Convert.ToSingle(line.Substring(line.IndexOf('$') + 1));
        }


        /// <summary>
        /// Reads a line of a receipts textfile for a quantity value of an item bought. 
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns>
        /// Returns a floating point value of the quantity read.
        /// </returns>
        public static float ReadReceiptQuantity(this StreamReader streamReader)
        {
            string line = streamReader.ReadLine();

            //Substring starting from the char after the the last ':' char of the list, cutting out all spaces and other unnecessary characters,
            //before converting to a floating point number and returning.
            return Convert.ToSingle(line.Substring(line.LastIndexOf(':') + 1).Trim(new char[] { ' ', 'k', 'g' } ));
        }

        
        /// <summary>
        /// Removes non-number chars and returns a string representing a price value in a single cell, ready to be converted to 
        /// a Single.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns>
        /// String value representing a price.
        /// </returns>
        public static string ReturnPriceString(this DataGridViewCell cell)
        {
            return cell.Value.ToString().Trim(new char[] { '$', ' ', 'k', 'g', '/' });
        }


        /// <summary>
        /// Loops through all cells in a DataGridViewRow, checking for any empty cells.
        /// Returns true if there is an empty cell in the row
        /// </summary>
        /// <param name="row"></param>
        /// <returns>
        /// True -> Empty cell found
        /// False -> All cells have values
        /// </returns>
        public static bool CheckRowEmptyCells(this DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.Value == null)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Swaps the positions of two elements in a list based on the indexes passed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="firstIndex"></param>
        /// <param name="secondIndex"></param>
        public static void SwapIndexes<T>(this List<T> list, int firstIndex, int secondIndex)
        {
            if (firstIndex < 0) return;
            if (secondIndex < 0) return;

            if ((firstIndex < list.Count) && (secondIndex < list.Count))
            {
                T temp = list[firstIndex];
                list[firstIndex] = list[secondIndex];
                list[secondIndex] = temp;
            }
        }


        /// <summary>
        /// Removes an element at the given index from the list and adds it back to the end of the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="indexToPushback"></param>
        public static void PushToBack<T>(this List<T> list, int indexToPushback)
        {
            T elementToMove = list[indexToPushback];
            list.Remove(elementToMove);
            list.Add(elementToMove);
        }
    }
}


//
//
//					LoginMenu class
//
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CustomProgram
{
    public partial class LoginScreen : Form
    {

        public LoginScreen()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            

        }

        //Login button clicked:
        private void button1_Click(object sender, EventArgs e)
        {
            //Checking that there is a user with matching username and password fields
            User user = ProcessUserController.ValidateUserLogin(userNameTextBox.Text, passwordTextBox.Text);

            //If a valid user was found
            if (user != null)
            {
                this.Hide();
                userNameTextBox.Clear();
                passwordTextBox.Clear();
                user.OpenUserMenu();

                //Done here not when the logout button is pressed so that the user is still logged out 
                //even if they just click the close window button.
                ProcessUserController.LogoutUser();

                this.Show();

            }
            //Otherwise no such user found
            else
            {
                MessageBox.Show("Invalid username and password combination");

            }
            userNameTextBox.Clear();
            passwordTextBox.Clear();
            userNameTextBox.Focus();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}


//
//
//					CashierMenu class
//
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomProgram
{
    public partial class CashierMenu : Form
    {
        public CashierMenu(string employeeName)
        {
            InitializeComponent();

            loggedInUserLabel.Text = employeeName;
        }

        private void CashierMenu_Load(object sender, EventArgs e)
        {
            viewStockScreen.Dock = DockStyle.Fill;
            processOrderScreen.Dock = DockStyle.Fill;
        }

        private void viewStockButton_Click(object sender, EventArgs e)
        {
            //Repopulating ListView with item details
            viewStockScreen.Refresh();

            viewStockScreen.Show();
        }

        private void newCustomerButton_Click(object sender, EventArgs e)
        {
            processOrderScreen.RefreshListViewData();

            processOrderScreen.Show();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


//
//
//					ManagerMenu class
//
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomProgram
{
    public partial class ManagerMenu : Form
    {
        public ManagerMenu(string employeeName)
        {
            InitializeComponent();
            
            loggedInUserLabel.Text = employeeName;
        }

        private void ManagerMenu_Load(object sender, EventArgs e)
        {
               processOrderScreen.Dock = DockStyle.Fill;
               manageStockScreen.Dock = DockStyle.Fill;
               salesReportScreen.Dock = DockStyle.Fill;
               manageUserScreen.Dock = DockStyle.Fill;
        }

        private void manageStockButton_Click_1(object sender, EventArgs e)
        {
            //Repopulate Item DataGridView with updated Item list details
            manageStockScreen.Refresh();

            manageStockScreen.Show();
        }

        private void manageUsersButton_Click_1(object sender, EventArgs e)
        {
            manageUserScreen.Show();
        }

        private void salesReportButton_Click(object sender, EventArgs e)
        {
            salesReportScreen.Show();
        }

        private void newCustomerButton_Click_1(object sender, EventArgs e)
        {
            processOrderScreen.RefreshListViewData();

            processOrderScreen.Show();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}



//
//
//					ViewStockMenu class
//
//

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
    public partial class ViewStockMenu : UserControl
    {
        public ViewStockMenu()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void ViewStockMenu_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Override to clear all ListView rows before repopulating them with UIController's ItemDatabase's StockList data
        /// </summary>
        public override void Refresh()
        {
            stockListView.Items.Clear();
            UIController.PopulateListView(stockListView);
        }
    }
}


//
//
//					ProcessOrderScreen class
//
//

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


//
//
//					ManageStockMenu class
//
//

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


//
//
//					ManageUserMenu class
//
//

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
    public partial class ManageUserMenu : UserControl
    {
        public ManageUserMenu()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        /// <summary>
        /// Override to clear all rows of the DataGridView before repopulating them again with all user details.
        /// </summary>
        public override void Refresh()
        {
            userDataGridView.Rows.Clear();

            ProcessUserController.PopulateDataGridView(userDataGridView.Rows);
        }


        private void ManageUserMenu_Load(object sender, EventArgs e)
        {
            Refresh();
        }
        

        private void updateRecordsButton_Click(object sender, EventArgs e)
        {
            //Item List index (and DataGridView row) counter
            int i = 0;
            UserInfo userData;

            foreach (DataGridViewRow row in userDataGridView.Rows)
            {
                if (!row.CheckRowEmptyCells())
                {
                    //Checking for positive user ID entered as string
                    if ((!Int32.TryParse(row.Cells[0].Value.ToString(), out int userID)) || (userID <= 0))
                    {
                        MessageBox.Show("Error: Please enter a positive number for the user's ID");
                        return;
                    }

                    //Checking for valid UserType entered as string
                    if (!Enum.TryParse(row.Cells[2].Value.ToString(), true, out UserType userType))
                    {
                        MessageBox.Show("Error: Please enter a valid user type");
                        return;
                    }

                    userData = new UserInfo();
                    userData.ID = userID;
                    userData.role = userType;
                    userData.employeeName = row.Cells[1].Value.ToString();
                    userData.username = row.Cells[3].Value.ToString();
                    userData.password = row.Cells[4].Value.ToString();

                    //i < count AND row contents NOT null --> Replace list index's data
                    if (i < ProcessUserController.UserCount())
                    {
                        ProcessUserController.ReplaceUserData(i, userData);
                    }
                    //i >= count AND row contents NOT null --> Add new item to list
                    else
                    {
                        //Checking if a user with the same ID or username already exists
                        if (!ProcessUserController.AddNewUser(userData))
                        {
                            MessageBox.Show("Error: There is already a user with that ID or username");
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
                    if (i < ProcessUserController.UserCount())
                    {
                        ProcessUserController.RemoveUserAt(i);
                    }
                    //i > count AND null --> Do nothing and break, at end of dataView table
                    else
                        break;
                }
            }

            //Updating user textfile and repopulating the DataGridView
            ProcessUserController.UpdateUserFiles();
            Refresh();
        }
    }
}


//
//
//					SalesReportMenu class
//
//

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
    public partial class SalesReportMenu : UserControl
    {
        public SalesReportMenu()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Refresh();
        }

        private void SalesReportMenu_Load(object sender, EventArgs e)
        {
            this.Refresh();
        }


        private void currentSessionSales_Click(object sender, EventArgs e)
        {
            this.Refresh();
            salesThisSessionLabel.Visible = true;

            //Populating ListView with details from current session sales.
            SalesReportController.SessionSalesReport(salesReportListView);

            //Updating labels to show total customers served and total revenue generated for session.
            customersServedValueLabel.Text = SalesReportController.CustomersServed();
            grandTotalValueLabel.Text = SalesReportController.TotalRevenue();
        }


        private void totalSalesButton_Click(object sender, EventArgs e)
        {
            this.Refresh();
            totalSalesLabel.Visible = true;

            //Populating ListView with details from total sales.
            SalesReportController.EntireSalesReport(salesReportListView);

            //Updating labels to show total customers served and total revenue generated for session.
            customersServedValueLabel.Text = SalesReportController.CustomersServed();
            grandTotalValueLabel.Text = SalesReportController.TotalRevenue();
        }


        /// <summary>
        /// Override to reset labels to empty strings, clear all ListView rows, and set title labels invisible.
        /// </summary>
        public override void Refresh()
        {
            customersServedValueLabel.Text = "";
            grandTotalValueLabel.Text = "";
            salesReportListView.Items.Clear();
            salesThisSessionLabel.Visible = false;
            totalSalesLabel.Visible = false;
        }
    }
}


//
//
//					SortButtons class
//
//

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
    public partial class SortButtons : UserControl
    {
        //Fields for sort order (ascending or descending) of each sort type button
        private bool _alphabeticalSortOrder = true;
        private bool _priceSortOrder = true;
        private bool _amountSortOrder = true;


        public SortButtons()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Uses UIController to sort its ItemDatabase.StockList by Item Name.
        /// Sort order (ascending/descending) is inverted everytime this button is clicked.
        /// Then calls the overridable Refresh() method of the Parent Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alphabetSort_Click(object sender, EventArgs e)
        {
            UIController.SortItemList(ItemSortType.Name, _alphabeticalSortOrder);
            _alphabeticalSortOrder = !_alphabeticalSortOrder;

            Parent.Refresh();
        }


        /// <summary>
        /// Uses UIController to sort its ItemDatabase.StockList by Item Price.
        /// Sort order (ascending/descending) is inverted everytime this button is clicked.
        /// Then calls the overridable Refresh() method of the Parent Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void priceSortButton_Click(object sender, EventArgs e)
        {
            UIController.SortItemList(ItemSortType.Price, _priceSortOrder);
            _priceSortOrder = !_priceSortOrder;

            Parent.Refresh();
        }


        /// <summary>
        /// Uses UIController to sort its ItemDatabase.StockList by Item Quantity.
        /// Sort order (ascending/descending) is inverted everytime this button is clicked.
        /// Then calls the overridable Refresh() method of the Parent Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quantitySortButton_Click(object sender, EventArgs e)
        {
            UIController.SortItemList(ItemSortType.Amount, _amountSortOrder);
            _amountSortOrder = !_amountSortOrder;

            Parent.Refresh();
        }

        private void SortButtons_Load(object sender, EventArgs e)
        {

        }
    }
}


//
//
//					UIController class
//
//

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


//
//
//					SalesReportController class
//
//
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


//
//
//					ProcessOrderController class
//
//
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


//
//
//					ProcessUserController class
//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CustomProgram
{
    static public class ProcessUserController
    {
        private static UserDatabase _userDatabase;
        private static User _loggedInUser;


        /// <summary>
        /// Checks the UserDatabase's UserList for a user with the passed in username and password fields. 
        /// Assigns this user to its _loggedInUser field before returning it.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>
        /// User object if a user with matching password and username is found.
        /// Null pointer if no user is found.
        /// </returns>
        public static User ValidateUserLogin(string userName, string password)
        {
            _loggedInUser = _userDatabase.CheckValidLogin(userName, password);
            return _loggedInUser;
        }


        /// <summary>
        /// Returns the number of Users currently in the UserList.
        /// </summary>
        /// <returns></returns>
        public static int UserCount()
        {
            return _userDatabase.UserList.Count();
        }


        /// <summary>
        /// Populates a DataGridView's rows with details of all users, with 1 user per row.
        /// </summary>
        /// <param name="rows"></param>
        public static void PopulateDataGridView(DataGridViewRowCollection rows)
        {
            rows.Clear();

            foreach (User user in _userDatabase.UserList)
            {
                rows.Add(user.ID, user.FullName, user.Role, user.UserName, user.Password);
            }
        }


        /// <summary>
        /// Sets the _loggedInUser field to null.
        /// </summary>
        public static void LogoutUser()
        {
            _loggedInUser = null;
        }


       /// <summary>
       /// Replaces details of a single user in UserDatabase's UserList, changing their User sub-type object if necessary.
       /// </summary>
       /// <param name="index">
       /// Index of the user in the UserList to be changed.
       /// </param>
       /// <param name="userInfo">
       /// Structure containing all basic details for the user to be updated with.
       /// </param>
       public static void ReplaceUserData(int index, UserInfo userInfo)
        {
            //Checking if the updated user had a role change, in which case, a new User subclass object needs
            //to be created and replace the current one in the list.
            if (_userDatabase.UserList[index].Role != userInfo.role)
            {
                User changedUser = _userDatabase.CreateUser(userInfo);

                //Changing the user referenced to in the list to a different user sub-type but with the same details
                //Garbage collector should then remove the old user sub-type from memory.
                _userDatabase.UserList[index] = changedUser;
            }
            //Otherwise, just update all other user details
            else
            {
                _userDatabase.UserList[index].UserName = userInfo.username;
                _userDatabase.UserList[index].Password = userInfo.password;
                _userDatabase.UserList[index].Role = userInfo.role;
                _userDatabase.UserList[index].ID = userInfo.ID;
                _userDatabase.UserList[index].FullName = userInfo.employeeName;
            }
        }


        /// <summary>
        /// Checks if a user with the same ID and/or username is in the UserDatabase, before creating a 
        /// user sub-type and adding this to the UserDatabase list.
        /// Returns true if successful operation.
        /// </summary>
        /// <param name="userToAdd">
        /// Contains details of a user's:
        /// -ID number
        /// -Full name
        /// -Role
        /// -Username
        /// -Password
        /// </param>
        /// 
        /// <returns>
        /// true -> successfully added a new user
        /// false -> User not added because a user with identical ID or username already exists
        /// </returns>
        public static bool AddNewUser(UserInfo userToAdd)
        {
            if (!_userDatabase.CheckExistingUser(userToAdd.ID, userToAdd.username))
            {
                _userDatabase.UserList.Add(_userDatabase.CreateUser(userToAdd));
                return true;
            }
            return false;
        }


        /// <summary>
        /// Removes a user from the UserDatabase's UserList.
        /// </summary>
        /// <param name="index">
        /// List index of the user to be removed.
        /// </param>
        public static void RemoveUserAt(int index)
        {
            _userDatabase.UserList.RemoveAt(index);
        }


        /// <summary>
        /// Rewrites all User details from UserDatabase's UserList to the UserDetails textfile.
        /// </summary>
        public static void UpdateUserFiles()
        {
            //Opening the user text file in non-append mode
            StreamWriter streamWriter = new StreamWriter(Directory.GetCurrentDirectory() + Program.USER_FILENAME, false);
            try
            {
                _userDatabase.SaveData(streamWriter);
                streamWriter.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating user data: " + e.Message);
                streamWriter.Close();
            }
        }


        public static User LoggedInUser
        {
            get
            {
                return _loggedInUser;
            }
        }

        public static UserDatabase UserDatabase
        {
            get
            {
                return _userDatabase;
            }
            set
            {
                _userDatabase = value;
            }
        }
    }
}