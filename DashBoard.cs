using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Car_Rental_Project
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }
         SqlConnection con = new SqlConnection("Data Source=NILOY\\SQLEXPRESS02;Integrated Security=True");

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            string querycar = "select count(*) from CarTb";
            SqlDataAdapter sda = new SqlDataAdapter(querycar, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CarLb1.Text = dt.Rows[0][0].ToString();

            string queryCustomer = "select count(*) from CustomerTab";
            SqlDataAdapter sda1 = new SqlDataAdapter(querycar, con);
            DataTable dt1 = new DataTable();
            sda.Fill(dt);
            CustLb1.Text = dt.Rows[0][0].ToString();

            string queryUsers = "select count(*) from UTab";
            SqlDataAdapter sda2 = new SqlDataAdapter(querycar, con);
            DataTable dt2 = new DataTable();
            sda.Fill(dt);
            UserLb1.Text = dt.Rows[0][0].ToString();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainfrom mainfrom = new Mainfrom();
            mainfrom.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
