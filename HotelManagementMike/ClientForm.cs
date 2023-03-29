using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HotelManagementMike
{
    public partial class ClientForm : Form
    {
        StaffManagement thirdForm = new StaffManagement();
        private string server = "localhost";
        private string database = "HotelMike";
        private string UserName = "root";
        private string Password = "";
        string connectionString;
        MySqlConnection Connection;
        public ClientForm()
        {
            InitializeComponent();
            MakeInitialConnection();

            if (Connection.State != ConnectionState.Open)
            {
                connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
                Connection = new MySqlConnection(connectionString);
                Connection.Open();

            }
        }

        private void MakeInitialConnection()
        {
            connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
            Connection = new MySqlConnection(connectionString);
            Connection.Open();

            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
            else
                MessageBox.Show("Connection is NOT healthy, could not connect to server!");
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

            {
                connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
                Connection = new MySqlConnection(connectionString);
                Connection.Open();


            }
            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM CLIENTS";
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adap.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            DataGridView grid = new DataGridView();
            grid.DataSource = grid;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            NameBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            MobileBox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            LastNameBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            RoomBox.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            PaidBox.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            RemoveBox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MainMenu secondForm = new MainMenu();
            this.Hide();
            secondForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if all fields are filled
                if (string.IsNullOrEmpty(NameBox.Text) || string.IsNullOrEmpty(LastNameBox.Text) || string.IsNullOrEmpty(MobileBox.Text) || string.IsNullOrEmpty(textBoxRoomType.Text) || string.IsNullOrWhiteSpace(RoomBox.Text))
                {
                    MessageBox.Show("Please enter data in all boxes!");
                    return;
                }

                // Set up the server name and login details.
                string connectionString = $"Data Source={server};Initial Catalog={database};User ID={UserName};Password={Password}";

                // Set the query to write
                // Don't need to ADD ID, it's automatically added.
                string query = "INSERT INTO HotelMike.CLIENTS (FirstName, LastName, Mobile, ROOMNUMBER, PAID, SIZE) VALUES (@FirstName, @LastName, @Mobile, @RoomNumber, @Paid, @Size)";

                // Set up connection to write to DB
                using MySqlConnection connection = new MySqlConnection(connectionString);
                using MySqlCommand command = new MySqlCommand(query, connection);

                // Add parameters to the command
                command.Parameters.AddWithValue("@FirstName", NameBox.Text);
                command.Parameters.AddWithValue("@LastName", LastNameBox.Text);
                command.Parameters.AddWithValue("@Mobile", MobileBox.Text);
                command.Parameters.AddWithValue("@RoomNumber", RoomBox.Text);
                command.Parameters.AddWithValue("@Paid", PaidBox.Text);
                command.Parameters.AddWithValue("@Size", textBoxRoomType.Text);

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
                using MySqlCommand cmd = new MySqlCommand("SELECT * FROM CLIENTS", connection);
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

        private void RefreshDataGridView()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                var query = "SELECT * FROM CLIENTS";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }

            }

        private void PaidBox_CheckedChanged_1(object sender, EventArgs e)
        {
            // The CheckBox control's Text value is changed each time the
            // control is clicked, indicating a checked or unchecked state corverting it to Text.  
            if (PaidBox.Checked)
            {
                PaidBox.Text = "Paid";
                PaidBox.Checked = true;
            }
            else
            {
                PaidBox.Text = "Unpaid";
                PaidBox.Checked = false;

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (RemoveBox.Text == "")
            {
                MessageBox.Show("Please enter Valid ClientID");
            }
            else
                try
                {
                 
                    connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
                    string Query = "delete from HotelMike.CLIENTS where Clientid='" + RemoveBox.Text + "' ; ";
                    MySqlConnection MyConnection2 = new MySqlConnection(connectionString);
                    MySqlCommand MyCommandConnection = new MySqlCommand(Query, MyConnection2);
                    MySqlDataReader MyReader2;
                    MyConnection2.Open();
                    MyReader2 = MyCommandConnection.ExecuteReader();
                    MessageBox.Show("Deleted");
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
                connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
                Connection = new MySqlConnection(connectionString);
                Connection.Open();

            }

            MySqlCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM CLIENTS";
            MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adap.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            DataGridView grid = new DataGridView();
            grid.DataSource = grid;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RemoveBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
