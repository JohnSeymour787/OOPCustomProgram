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
