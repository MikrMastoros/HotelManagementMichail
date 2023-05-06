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

namespace HotelManagementMike
{
    public partial class ReservationsManagement : Form
    {
        private string server = "localhost";
        private string database = "HotelMike";
        private string UserName = "root";
        private string Password = "";
        //CONNECTS TO MYSQL
        string connectionString;
        MySqlConnection Connection;
        public ReservationsManagement()
        {
            InitializeComponent();
            MakeInitialConnection();
            this.Load += new System.EventHandler(this.ReservationsManagement_Load);
        }
        private void ReservationsManagement_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void MakeInitialConnection()
        {
            connectionString = $"Server={server};Database={database};Uid={UserName};Pwd={Password};";
            Connection = new MySqlConnection(connectionString);

            try
            {
                Connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to database: " + ex.Message);
            }

        }

        private void RefreshData()
        {

            using (Connection = new MySqlConnection(connectionString))
            {
                Connection.Open();

                MySqlCommand cmd = Connection.CreateCommand();
                cmd.CommandText = "SELECT RESERVATIONS.*, CLIENTS.FirstName, CLIENTS.LastName, CLIENTS.Mobile, CLIENTS.RoomNumber FROM RESERVATIONS JOIN CLIENTS ON RESERVATIONS.ClientID = CLIENTS.ClientID";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }

        }

        private void Insertbtn(object sender, EventArgs e)
        {
            try
            {
                // Check if all fields are filled
                if (string.IsNullOrEmpty(RoomNBox.Text) || string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(PhoneBox.Text) || string.IsNullOrEmpty(dateTimePicker2.Text))
                {
                    MessageBox.Show("Please enter data in all boxes!");
                    return;
                }

                // Set up the server name and login details.
                string connectionString = $"Data Source={server};Initial Catalog={database};User ID={UserName};Password={Password}";

                // Set the query to insert a client
                string insertClientQuery = "INSERT INTO HotelMike.CLIENTS (FirstName, LastName, RoomNumber, Mobile) VALUES (@FirstName, @LastName, @RoomNumber, @Mobile); SELECT LAST_INSERT_ID();";

                // Set the query to write
                string insertReservationQuery = "INSERT INTO HotelMike.RESERVATIONS (ClientID, CHECKINDATE, CHECKOUTDATE) VALUES (@ClientID, @CheckInDate, @CheckOutDate)";

                // Set up connection to write to DB
                using MySqlConnection connection = new MySqlConnection(connectionString);
                {
                    // Insert a client and get the generated ClientID
                    MySqlCommand insertClientCommand = new MySqlCommand(insertClientQuery, connection);
                    insertClientCommand.Parameters.AddWithValue("@FirstName", FirstNameBox.Text);
                    insertClientCommand.Parameters.AddWithValue("@LastName", LastNameBox.Text);
                    insertClientCommand.Parameters.AddWithValue("@RoomNumber", RoomNBox.Text);
                    insertClientCommand.Parameters.AddWithValue("@Mobile", PhoneBox.Text);
                    connection.Open();
                    int clientId = Convert.ToInt32(insertClientCommand.ExecuteScalar());

                    // Insert the reservation with the generated ClientID
                    MySqlCommand insertReservationCommand = new MySqlCommand(insertReservationQuery, connection);
                    insertReservationCommand.Parameters.AddWithValue("@ClientID", clientId);
                    insertReservationCommand.Parameters.AddWithValue("@CheckInDate", dateTimePicker1.Text);
                    insertReservationCommand.Parameters.AddWithValue("@CheckOutDate", dateTimePicker2.Text);
                    insertReservationCommand.ExecuteNonQuery();
                    MessageBox.Show("Reservation Created Successfully");
                    // Read in data
                    using MySqlCommand cmd = new MySqlCommand("SELECT CLIENTS.FirstName, CLIENTS.LastName, CLIENTS.Mobile, RESERVATIONS.CHECKINDATE, RESERVATIONS.CHECKOUTDATE, CLIENTS.RoomNumber FROM RESERVATIONS JOIN CLIENTS ON RESERVATIONS.ClientID = CLIENTS.ClientID", connection);
                    using MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    using DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    // Set the DataGridView's data source
                    dataGridView1.DataSource = dataSet.Tables[0].DefaultView;
                    RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            RefreshData();
        }

        private void MainMenubtn(object sender, EventArgs e)
        {
            MainMenu secondForm = new MainMenu();
            this.Hide();
            secondForm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RoomNBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            FirstNameBox.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            LastNameBox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            dateTimePicker2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            PhoneBox.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void Exitbtn(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Removebtn(object sender, EventArgs e)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string Query = "DELETE FROM HotelMike.RESERVATIONS WHERE ReservationID=@ReservationID";
            using MySqlCommand command = new MySqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ReservationID", RemoveBox.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Deleted");
            RefreshData();
        }
    }
}