using System;
using System.Windows.Forms;

namespace HotelManagementMike
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        // Client Menu Button - redirects to client menu form
        private void clientMenuButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var clientMenuForm = new ClientForm())
                {
                    ClientForm clientsForm = new ClientForm();
                    clientsForm.Owner = this;
                    this.Hide();
                    clientsForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating client menu form: {ex.Message}");
            }
        }

        // Room Menu Button - redirects to room menu form
        private void reservationMenuButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var reservationMenuForm = new ReservationsManagement())
                {
                    ReservationsManagement reservationsForm = new ReservationsManagement();
                    reservationsForm.Owner = this;
                    this.Hide();
                    reservationsForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating reservation menu form: {ex.Message}");
            }
        }

        private void staffManagementButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (var staffManagementForm = new StaffManagement())
                {
                    StaffManagement secondForm = new StaffManagement();
                    this.Dispose();
                    secondForm.Show(); staffManagementForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating staff management form: {ex.Message}");
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (var staffManagementForm = new StaffManagement())
                {
                    StaffManagement secondForm = new StaffManagement();
                    this.Dispose();
                    secondForm.Show(); staffManagementForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating staff management form: {ex.Message}");
            }
        }
    }
}
