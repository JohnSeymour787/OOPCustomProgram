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
