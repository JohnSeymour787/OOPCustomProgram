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
