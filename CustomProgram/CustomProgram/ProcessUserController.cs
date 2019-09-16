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
