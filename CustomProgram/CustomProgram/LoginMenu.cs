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
