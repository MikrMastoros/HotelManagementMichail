using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace HotelManagementMike
{
    public partial class StaffManagement : Form
    {
        //VARIABLES FOR DB CONNECTION
        ReservationsManagement forthForm = new ReservationsManagement();
        private string server = "localhost";
        private string database = "HotelMike";
        private string UserName = "root";
        private string Password = "";
        //CONNECT US TO MYSQL
        string connectionString;
        MySqlConnection Connection;
        public StaffManagement()
        {
            InitializeComponent();
        }      
        private void StaffManagement_Load(object sender, EventArgs e)
        {
            {
                //Setup db
                connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
                //Makes connection
                Connection = new MySqlConnection(connectionString);
                //Opens connection
                Connection.Open();


            }

            //Sets up command for mysql
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM STAFF";
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            //Puts into a table.
            DataSet ds = new DataSet();
            adap.Fill(ds);
            //Creates a new datagidview object.
            //DataGridView dataGridView1 = new DataGridView();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            DataGridView grid = new DataGridView();
            grid.DataSource = grid;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            StaffNameBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            StaffSurnameBox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            LoginBox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            PasswordBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            Remove.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((StaffNameBox.Text == "") || (StaffSurnameBox.Text == "") || (LoginBox.Text == "") || (PasswordBox.Text == ""))
            {
                MessageBox.Show("Please enter data in ALL boxes!");
            }
            else
            { 
                    try
                    {
                        connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;

                        MySqlConnection MyConnection2 = new MySqlConnection(connectionString);
                        MyConnection2.Open();

                        string Query = "INSERT INTO HotelMike.STAFF(LogUserName,LogPassword,FirstName,LastName) VALUES(@LogUserName, @LogPassword, @FirstName, @LastName);";
                        MySqlCommand MyCommandConnection = new MySqlCommand(Query, MyConnection2);
                        MyCommandConnection.Parameters.AddWithValue("@LogUserName", this.LoginBox.Text);
                        MyCommandConnection.Parameters.AddWithValue("@LogPassword", this.PasswordBox.Text);
                        MyCommandConnection.Parameters.AddWithValue("@FirstName", this.StaffNameBox.Text);
                        MyCommandConnection.Parameters.AddWithValue("@LastName", this.StaffSurnameBox.Text);

                        MyCommandConnection.ExecuteNonQuery();

                        Query = $"CREATE USER '{LoginBox.Text}'@'localhost' IDENTIFIED BY '{PasswordBox.Text}'; GRANT ALL PRIVILEGES ON *.* TO '{LoginBox.Text}'@'localhost'; FLUSH PRIVILEGES;";
                        MyCommandConnection = new MySqlCommand(Query, MyConnection2);
                        MyCommandConnection.ExecuteNonQuery();

                        MessageBox.Show("New Staff successfully added!");

                        MyConnection2.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                

                //RELOADS
                if (Connection.State != ConnectionState.Open)
                {
                    connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
                    Connection = new MySqlConnection(connectionString);
                    Connection.Open();
                }

                MySqlCommand cmd = Connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM STAFF";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
                DataGridView grid = new DataGridView();
                grid.DataSource = grid;
            }
        }

        private void RemoveBox_Click(object sender, EventArgs e)
        {
            if ((RemoveBox.Text == ""))
            {
                MessageBox.Show("Please enter StaffID");
            }
            else
                try
                {
                    //Set up the server name and login details.
                    connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;

                    //Set the query to write
                    //Dont need to ADD ID, its automatically added.
                    string Query = "delete from HotelMike.STAFF where Staffid='" + RemoveBox.Text + "' ; ";

                    //Set up connection to write to DB
                    MySqlConnection MyConnection2 = new MySqlConnection(connectionString);

                    //Sort out the connection object
                    MySqlCommand MyCommandConnection = new MySqlCommand(Query, MyConnection2);

                    //Sort out the reader to write to the DB
                    MySqlDataReader MyReader2;

                    //Open the reader connection.
                    MyConnection2.Open();

                    //Execute reader.
                    MyReader2 = MyCommandConnection.ExecuteReader();

                    MessageBox.Show("Deleted");
                    //RELOADS
                    if (Connection.State != ConnectionState.Open)
                    {
                        //Setup Database
                        connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
                        //Makes the connection
                        Connection = new MySqlConnection(connectionString);
                        //Opens the connection
                        Connection.Open();
                    }
                    //Sets up command for mysql
                    MySqlCommand cmd = Connection.CreateCommand();
                    cmd.CommandText = "SELECT * FROM STAFF";
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    //Puts into a table.
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    //Creates a new datagidview object.
                    //DataGridView dataGridView1 = new DataGridView();
                    dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    DataGridView grid = new DataGridView();
                    grid.DataSource = grid;

                    //Read in data.
                    while (MyReader2.Read())
                    {
                    }
                    MyConnection2.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MainMenu secondForm = new MainMenu();
            secondForm.Show();
             this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}