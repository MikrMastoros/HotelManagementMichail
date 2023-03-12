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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        //Client Menu Button - redirects to client menu form
        private void button2_Click(object sender, EventArgs e)
        {
            ClientForm secondForm = new ClientForm();
            this.Dispose();
            secondForm.Show();
        }
        
        //Room Menu Button - redirects to room menu form
        private void button1_Click(object sender, EventArgs e)
        {
            RoomManagement secondForm = new RoomManagement();
            this.Dispose();
            secondForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StaffManagement secondForm = new StaffManagement();
            this.Dispose();
            secondForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
