using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using HotelManagementMike;

namespace HotelManagementMike
{
    public partial class Login : Form
    {
        StaffManagement StaffManagement = new StaffManagement();
        private string server = "localhost";
        private string database = "HotelMike";
        private string UserName = "root";
        private string Password = "";
        //Makes connection to MYSQL
        string connectionString;
        MySqlConnection Connection;
        public Login()
        {
            InitializeComponent();
            MakeInitialConnection();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MakeInitialConnection()
        {
            //Setup database
            connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
            //Makes connection
            Connection = new MySqlConnection(connectionString);
            //Opens connection
            Connection.Open();

            if (Connection.State == ConnectionState.Open)
            {
                MessageBox.Show("Connected!");
                Connection.Close();
            }
            else
                MessageBox.Show("Connection Error");

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string USERNAME;
            string PASSWORD;
            int LoginFlag = 0;
            if (Connection.State != ConnectionState.Open)
            {
                //Setup db
                connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
                //Makes connection
                Connection = new MySqlConnection(connectionString);
                //Opens connection
                Connection.Open();
                MessageBox.Show("Now running!");

            }
            else
            {
                MessageBox.Show("CLOSED!");
            }
            try
            {
                //Initialises the Command
                MySqlCommand cmd = Connection.CreateCommand();
                //SQL query
                cmd.CommandText = "SELECT * FROM Staff";
                //initialises the Reader
                MySqlDataReader reader = cmd.ExecuteReader();

                //READS IN THE ENTIRE DB
                while (reader.Read())
                {
                    USERNAME = reader.GetString("LogUserName");
                    PASSWORD = reader.GetString("LogPassword");

                    //Need to use a flag otherwise (due to the while), the message is shown for all rows.
                    if ((textBoxUsername.Text == USERNAME) & (textBoxPassword.Text == PASSWORD))
                        LoginFlag = 1;
                }

                if (LoginFlag == 1)
                {
                    MessageBox.Show("Logged in Successfully");   
                    //opens and shows the main menu form
                    MainMenu thirdForm = new MainMenu();
                    this.Hide();
                    thirdForm.Show();
                }
                else
                {
                    MessageBox.Show("(Username/Password) Incorrect!");
                    textBoxUsername.Text = "";
                    textBoxPassword.Text = "";
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string USERNAME;
            string PASSWORD;
            int LoginFlag = 0;
            if (Connection.State != ConnectionState.Open)
            {
                //Setup db
                connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
                //Makes connection
                Connection = new MySqlConnection(connectionString);
                //Opens connection
                Connection.Open();
                MessageBox.Show("Now Open!");

            }
            else
            {
                MessageBox.Show("CLOSED!");
            }
            try
            {
                //Initialises the Command
                MySqlCommand cmd = Connection.CreateCommand();
                //SQL query
                cmd.CommandText = "SELECT * FROM Staff";
                //initialises the Reader
                MySqlDataReader reader = cmd.ExecuteReader();

                //READS IN THE ENTIRE database
                while (reader.Read())
                {
                    USERNAME = reader.GetString("LogUserName");
                    PASSWORD = reader.GetString("LogPassword");

                    //Need to use a flag otherwise (due to the while), the message is shown for all rows.
                    if ((textBoxUsername.Text == USERNAME) & (textBoxPassword.Text == PASSWORD))
                        LoginFlag = 1;
                }

                if (LoginFlag == 1)
                {
                    MessageBox.Show("Logged in Successfully");
                    //Hide Login Screen
                    StaffManagement.Show();
                    this.Hide();    
                }
                else
                {
                    MessageBox.Show("(Username/Password) Incorrect!");
                    textBoxUsername.Text = "";
                    textBoxPassword.Text = "";
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }

            }
        }

        private void ExitBTN_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}