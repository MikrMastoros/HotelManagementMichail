using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HotelManagementMike
{
    public partial class Login : Form
    {
        private string server = "localhost";
        private string database = "HotelMike";
        private string username = "root";
        private string password = "";
        private string connectionString;
        private MySqlConnection connection;

        public Login()
        {
            InitializeComponent();
            MakeInitialConnection();
        }

        private void MakeInitialConnection()
        {
            // Build connection string
            connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + username + ";" + "Password=" + password;
            // Create connection object
            connection = new MySqlConnection(connectionString);
            // Open connection
            connection.Open();

            // Check if connection is open
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            else
            {
                MessageBox.Show("Connection Error");
            }
        }

        private async void pictureBox1_Click(object sender, EventArgs e)
        {
            // Get entered username and password
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            // Check if either field is empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a username and password.");
                return;
            }

            // Build connection string using entered username and password
            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server = server,
                Database = database,
                UserID = username,
                Password = password
            };

            using (var connection = new MySqlConnection(connectionStringBuilder.ConnectionString))
            {
                try
                {
                    // Open connection asynchronously
                    await connection.OpenAsync();

                    // Build SQL command to check username and password
                    var command = new MySqlCommand("SELECT COUNT(*) FROM Staff WHERE LogUserName = @username AND LogPassword = @password", connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    // Execute command and get result count
                    int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                    if (count > 0)
                    {
                        MessageBox.Show("Logged in successfully.");
                        MainMenu thirdForm = new MainMenu();
                        this.Hide();
                        thirdForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password.");
                        textBoxPassword.Clear();
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Initialize flag variable to track login success
            int loginFlag = 0;

            // Make initial database connection
            MakeInitialConnection();

            try
            {
                // Create SQL command to check username and password
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Staff WHERE LogUserName = @username AND LogPassword = @password";
                cmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
                cmd.Parameters.AddWithValue("@password", textBoxPassword.Text);

                // Execute SQL command and read results
                MySqlDataReader reader = cmd.ExecuteReader();

                // Check if entered username and password match any records
                while (reader.Read())
                {
                    loginFlag = 1;
                }

                // If successful, show staff management form and hide login form
                if (loginFlag == 1)
                {
                    MessageBox.Show("Logged in Successfully");
                    StaffManagement staffManagementForm = new StaffManagement();
                    staffManagementForm.Show();
                    this.Hide();
                }
                // If unsuccessful, display error message and clear fields
                else
                {
                    MessageBox.Show("Invalid username or password.");
                    textBoxUsername.Text = "";
                    textBoxPassword.Text = "";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Username or password invalid");
            }
            finally
            {
                // Close connection if it is open
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void ExitBTN_Click(object sender, EventArgs e)
        {
            // Exit application
            Application.Exit();
        }
    }
}
