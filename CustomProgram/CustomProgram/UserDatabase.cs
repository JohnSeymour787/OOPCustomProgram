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
