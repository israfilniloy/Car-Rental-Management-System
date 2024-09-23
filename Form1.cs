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

namespace Car_Rental_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=NILOY\\SQLEXPRESS02;Integrated Security=True");
      
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Uname.Text = "";
            Upass.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this line shows if the username and pass rows are same in the UTab then its shows 1 means user information is correct
            string query = "select count(*) from UTab where Uname= '" + Uname.Text + "' and Upass ='" + Upass.Text + "'";
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(query,con); //for execute the query result and fill into the data table also con use for application to database connection
            DataTable dt = new DataTable(); 
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Mainfrom mainfrom = new Mainfrom(); 
                mainfrom.Show();    
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Username or password");
            }
            con.Close();
        }

       

      
        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
