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
