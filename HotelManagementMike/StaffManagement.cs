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

namespace HotelManagementMike
{
    public partial class StaffManagement : Form
    {
        //VARIABLES FOR DB CONNECTION
        RoomManagement forthForm = new RoomManagement();
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

        private void MakeInitialConnection()
        {
            //Setup db
            connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
            //Makes connection
            Connection = new MySqlConnection(connectionString);
            //Opens connection
            Connection.Open();

            if (Connection.State == ConnectionState.Open)
            {
                //MessageBox.Show("Connection Healthy!");
                Connection.Close();
            }
            else
                MessageBox.Show("Connection is NOT healthy, could not connect to server!");
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
            {
                //NOTE: Here we want to disable the ID box, as we will never change it only see it,
                //so we dont need to check it for content in the IF, NOR send info to the database.
                if ((StaffNameBox.Text == "") || (StaffSurnameBox.Text == "") || (LoginBox.Text == "") || (PasswordBox.Text == ""))
                {
                    MessageBox.Show("Please enter data in ALL boxes!");
                }
                else
                {

                    try
                    {
                        //Set up the server name and login details.
                        connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;

                        //Set the query to write
                        //Dont need to ADD ID, its automatically added.
                        string Query = "insert into HotelMike.STAFF(LogUserName,LogPassword,FirstName,LastName) values('" + this.LoginBox.Text + "','" + this.PasswordBox.Text + "','" + this.StaffNameBox.Text + "','" + this.StaffSurnameBox.Text + "'); ";

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

                        MessageBox.Show("New Staff successfully added!");

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
            }
            //RELOADS
            if (Connection.State != ConnectionState.Open)
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

        private void button2_Click(object sender, EventArgs e)
        {
            ClientForm secondForm = new ClientForm();
            this.Hide();
            secondForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainMenu secondForm = new MainMenu();
            this.Hide();
            secondForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}