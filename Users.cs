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

namespace Car_Rental_Project
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=NILOY\\SQLEXPRESS02;Integrated Security=True");
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void population()
        {
            con.Open();
            string query = "select * from UTab";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);//use for update or modify data
            var ds = new DataSet();// hold the data that i got form database 
            da.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];// use for data table to connect gridview
            con.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) 
        {
            // Check if any of the required fields are emptys
            if (Uid.Text == "" || Uname.Text == "" || Upass.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    // Define the query with parameter placeholders
                    string query = "insert into UTab (Id, Uname, Upass) values (@Id, @Uname, @Upass)";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SqlCommand object
                        cmd.Parameters.AddWithValue("@Id", Uid.Text);
                        cmd.Parameters.AddWithValue("@Uname", Uname.Text);
                        cmd.Parameters.AddWithValue("@Upass", Upass.Text);

                        // Open the connection, execute the query, and close the connection
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    // Show success message
                    MessageBox.Show("User Successfully Added");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding user: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }


        private void Users_Load(object sender, EventArgs e)
        {
            population();
        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < UserDGV.Rows.Count)
            {
                // Get the selected row
                DataGridViewRow row = UserDGV.Rows[e.RowIndex];

                // Populate the text boxes with the selected row's data
                Uid.Text = row.Cells["Id"].Value.ToString();
                Uname.Text = row.Cells["Uname"].Value.ToString();
                Upass.Text = row.Cells["Upass"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Check if Uid field is empty
            if (Uid.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    // Define the query with a parameter placeholder
                    string query = "delete from UTab where Id = @Id";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameter to the SqlCommand object
                        cmd.Parameters.AddWithValue("@Id", Uid.Text);

                        // Open the connection, execute the query, and close the connection
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    // Show success message
                    MessageBox.Show("User Deleted Successfully");

                    // Refresh the DataGridView
                    population();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting user: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if any of the required fields are empty
            if (Uid.Text == "" || Uname.Text == "" || Upass.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    // Define the query with parameter placeholders
                    string query = "update UTab set Uname = @Uname, Upass = @Upass where Id = @Id";//This ensures that the query will only update the user whose Id matches the value in Uid.Text.

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SqlCommand object
                        cmd.Parameters.AddWithValue("@Uname", Uname.Text);
                        cmd.Parameters.AddWithValue("@Upass", Upass.Text);
                        cmd.Parameters.AddWithValue("@Id", Uid.Text);

                        // Open the connection, execute the query, and close the connection
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }

                    // Close the connection (even though 'using' will automatically close it, this is for clarity)
                    con.Close();

                    // Show success message
                    MessageBox.Show("User Updated Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating user: " + ex.Message);
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

        private void Uid_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
