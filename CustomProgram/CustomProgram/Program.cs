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
