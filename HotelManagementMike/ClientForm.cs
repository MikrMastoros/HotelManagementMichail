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
            {
                if ((NameBox.Text == "") || (LastNameBox.Text == "") || (MobileBox.Text == "") || (textBoxRoomType.Text == "") || (RoomBox.Text == ""))
                {
                    MessageBox.Show("Please enter data in ALL boxes!");
                }
                else
                {

                    try
                    {
                        connectionString = @"Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + UserName + ";" + "Password=" + Password;
                        string Query = "Insert into HotelMike.CLIENTS(FirstName,LastName,Mobile,ROOM,PAID,SIZE)values('" + NameBox.Text + "','" + LastNameBox.Text + "','" + MobileBox.Text + "','" + RoomBox.Text + "','" + PaidBox.Text + "','" + textBoxRoomType.Text + "')";
                        MySqlConnection MyConnection2 = new MySqlConnection(connectionString);
                        MySqlCommand MyCommandConnection = new MySqlCommand(Query, MyConnection2);
                        MySqlDataReader MyReader2;
                        MyConnection2.Open();
                        MyReader2 = MyCommandConnection.ExecuteReader();
                        MessageBox.Show("New record successfully written!");
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

        private void PaidBox_CheckedChanged_1(object sender, EventArgs e)
        {
            // The CheckBox control's Text value is changed each time the
            // control is clicked, indicating a checked or unchecked state corverting it to Text.  
            if (PaidBox.Checked)
            {
                PaidBox.Text = "Paid";
            }
            else
            {
                PaidBox.Text = "Unpaid";

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
