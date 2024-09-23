using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental_Project
{
    public partial class Mainfrom : Form
    {
        public Mainfrom()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Car car = new Car();
            car.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customer customer = new Customer();
            customer.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rental rental = new Rental();
            rental.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           this.Hide();
            Return Return = new Return();
            Return.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Users users = new Users();
            users.Show();   
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }


        private void Mainfrom_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Create a new instance of the Form1 (Login form)
            Form1 loginForm = new Form1();

            // Show the Login form
            loginForm.Show();

            // Hide the current Mainform
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click_2(object sender, EventArgs e)
        {

            this.Hide();
            DashBoard dashboard = new DashBoard();
            dashboard.Show();
        }
    }
}
