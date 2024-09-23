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
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=NILOY\\SQLEXPRESS02;Integrated Security=True");
        private void population()
        {
            con.Open();
            string query = "select * from CustomerTab";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "" || NameTb.Text == "" || AddressTb.Text == "" || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    // Define the query with parameter placeholders, including the Available column
                    string query = "INSERT INTO CustomerTab (IdTb, NameTb, AddressTb, PhoneTb) VALUES (@IdTb, @NameTb, @AddressTb, @PhoneTb)";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SqlCommand object
                        cmd.Parameters.AddWithValue("@IdTb", IdTb.Text);
                        cmd.Parameters.AddWithValue("@NameTb", NameTb.Text);
                        cmd.Parameters.AddWithValue("@AddressTb", AddressTb.Text);
                        cmd.Parameters.AddWithValue("@PhoneTb", PhoneTb.Text);

                        // Open the connection, execute the query, and close the connection
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    // Show success message
                    MessageBox.Show("Customer Successfully Added");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding Customer :" +ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < CustomerDGV.Rows.Count)
            {
                // Get the selected row
                DataGridViewRow row = CustomerDGV.Rows[e.RowIndex];

                // Populate text boxes with the selected row's data
                IdTb.Text = row.Cells["IdTb"].Value.ToString();
                NameTb.Text = row.Cells["NameTb"].Value.ToString();
                AddressTb.Text = row.Cells["AddressTb"].Value.ToString();
                PhoneTb.Text = row.Cells["PhoneTb"].Value.ToString();
            }
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            population();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "" || NameTb.Text == "" || AddressTb.Text == "" || PhoneTb.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    // Define the query with parameter placeholders, including the Available column
                    string query = "update CustomerTab set NameTb = @NameTb, AddressTb = @AddressTb, PhoneTb = @PhoneTb where IdTb = @IdTb";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SqlCommand object
                        cmd.Parameters.AddWithValue("@IdTb", IdTb.Text);
                        cmd.Parameters.AddWithValue("@NameTb", NameTb.Text);
                        cmd.Parameters.AddWithValue("@AddressTb", AddressTb.Text);
                        cmd.Parameters.AddWithValue("@PhoneTb", PhoneTb.Text);

                        // Open the connection, execute the query, and close the connection
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    // Show success message
                    MessageBox.Show("Customer Updated Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Updating Customer :" + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    // Define the query with a parameter placeholder
                    string query = "delete from CustomerTab where IdTb = @IdTb";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameter to the SqlCommand object
                        cmd.Parameters.AddWithValue("@IdTb", IdTb.Text);

                        // Open the connection, execute the query, and close the connection
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    // Show success message
                    MessageBox.Show("Customer Deleted Successfully");

                    // Refresh the DataGridView
                    population();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting Customer: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainfrom mainfrom = new Mainfrom();
            mainfrom.Show();    
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
