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

namespace HotelManagementMike
{
    public partial class RoomManagement : Form
    {
        private string server = "localhost";
        private string database = "HotelMike";
        private string UserName = "root";
        private string Password = "";
        //CONNECTS TO MYSQL
        string connectionString;
        MySqlConnection Connection;
        public RoomManagement()
        {
            InitializeComponent();
            MakeInitialConnection();
        }

        private void MakeInitialConnection()
        {
            //Sets up database
            connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
            //Makes the connection
            Connection = new MySqlConnection(connectionString);
            //Opens the connection
            Connection.Open();

            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
            else
                MessageBox.Show("Not Connected to server!");

            //Sets up command for mysql
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM ROOMS";
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

        private void button3_Click(object sender, EventArgs e)
        {
            using (MainMenu secondForm = new MainMenu())
            {
                this.Hide();
                secondForm.ShowDialog();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            FirstNameBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            RoomNBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            RoomTBox.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            RemoveBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            PriceBox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {


            if ((RemoveBox.Text == ""))
            {
                MessageBox.Show("Please enter Room Number");
            }
            else
                try
                {
                    //Set up the server name and login details.
                    connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;

                    //Set the query to write
                    //Dont need to ADD ID, its automatically added.
                    string Query = "delete from HotelMike.ROOMS where ROOMNUMBER='" + RemoveBox.Text + "' ; ";

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

                    MessageBox.Show("Deleted Record");

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
            cmd.CommandText = "SELECT * FROM ROOMS";
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if all fields are filled
                if (string.IsNullOrEmpty(FirstNameBox.Text) || string.IsNullOrEmpty(RoomNBox.Text) || string.IsNullOrEmpty(RoomTBox.Text) || string.IsNullOrEmpty(PriceBox.Text))
                {
                    MessageBox.Show("Please enter data in all boxes!");
                    return;
                }

                // Set up the server name and login details.
                string connectionString = $"Data Source={server};Initial Catalog={database};User ID={UserName};Password={Password}";

                // Set the query to write
                // Don't need to ADD ID, it's automatically added.
                string query = "INSERT INTO HotelMike.ROOMS (OCCUPANT, ROOMNUMBER, SIZE, PRICE) VALUES (@Occupant, @RoomNumber, @Size, @Price)";

                // Set up connection to write to DB
                using MySqlConnection connection = new MySqlConnection(connectionString);
                using MySqlCommand command = new MySqlCommand(query, connection);

                // Add parameters to the command
                command.Parameters.AddWithValue("@Occupant", FirstNameBox.Text);
                command.Parameters.AddWithValue("@RoomNumber", RoomNBox.Text);
                command.Parameters.AddWithValue("@Size", RoomTBox.Text);
                command.Parameters.AddWithValue("@Price", PriceBox.Text);

                // Open the connection and execute the query
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Updated Successfully");
                }
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062) // MySQL error number for duplicate key violation
                    {
                        MessageBox.Show("Another occupant already has this room. Please choose a different room.");
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                // Read in data
                using MySqlCommand cmd = new MySqlCommand("SELECT * FROM ROOMS", connection);
                using MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                using DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);

                // Set the DataGridView's data source
                dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
