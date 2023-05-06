using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace HotelManagementMike
{
    public partial class ClientForm : Form
    {
        private string server = "localhost";
        private string database = "HotelMike";
        private string UserName = "root";
        private string Password = "";
        string connectionString;
        MySqlConnection Connection;
        public ClientForm()
        {
            InitializeComponent();
            connectionString = $"Server={server};Database={database};Uid={UserName};Pwd={Password};";
            MakeInitialConnection();
        }

        private void MakeInitialConnection()
        {
            using (Connection = new MySqlConnection(connectionString))
            {
                try
                {
                    Connection.Open();
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Connection is NOT healthy, could not connect to server!");
                }
            }
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            using (Connection = new MySqlConnection(connectionString))
            {
                Connection.Open();
                MySqlCommand cmd = Connection.CreateCommand();
                cmd.CommandText = "SELECT Clients.*, RESERVATIONS.ReservationID FROM Clients JOIN RESERVATIONS ON Clients.ClientID = RESERVATIONS.ClientID";
                MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                NameBox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                LastNameBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                RoomBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                MobileBox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
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
                if (string.IsNullOrEmpty(NameBox.Text) || string.IsNullOrEmpty(LastNameBox.Text) || string.IsNullOrEmpty(MobileBox.Text) || string.IsNullOrEmpty(RoomBox.Text))
                {
                    MessageBox.Show("Please enter data in all boxes!");
                    return;
                }

                // Set the query to insert a client
                string insertClientQuery = "INSERT INTO Clients (FirstName, LastName, RoomNumber, Mobile) VALUES (@FirstName, @LastName, @RoomNumber, @Mobile); SELECT LAST_INSERT_ID();";

                // Set up connection to write to DB
                using MySqlConnection connection = new MySqlConnection(connectionString);
                {
                    // Insert a client and get the generated ClientID
                    MySqlCommand insertClientCommand = new MySqlCommand(insertClientQuery, connection);
                    insertClientCommand.Parameters.AddWithValue("@FirstName", NameBox.Text);
                    insertClientCommand.Parameters.AddWithValue("@LastName", LastNameBox.Text);
                    insertClientCommand.Parameters.AddWithValue("@RoomNumber", RoomBox.Text);
                    insertClientCommand.Parameters.AddWithValue("@Mobile", MobileBox.Text);
                    connection.Open();
                    int clientId = Convert.ToInt32(insertClientCommand.ExecuteScalar());                
                }
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            RefreshDataGridView();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(RemoveBox.Text))
            {
                MessageBox.Show("Please enter a valid ClientID");
            }
            else
            {
                try
                {
                    string Query = $"DELETE FROM CLIENTS WHERE ClientID=@ClientID";
                    using MySqlConnection connection = new MySqlConnection(connectionString);
                    using MySqlCommand command = new MySqlCommand(Query, connection);
                    command.Parameters.AddWithValue("@ClientID", RemoveBox.Text);
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Client record deleted successfully");
                    RefreshDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}