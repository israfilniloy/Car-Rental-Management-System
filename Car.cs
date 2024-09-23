using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Car_Rental_Project
{
    public partial class Car : Form
    {
        public Car()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=NILOY\\SQLEXPRESS02;Integrated Security=True");
        private void population()
        {
            con.Open();
            string query = "select * from CarTb";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            CarDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if any of the required fields are empty
            if (RegNumTb.Text == "" || BrandTb.Text == "" || ModelTb.Text == "" || PriceTb.Text == "" || Available.SelectedItem == null)
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    // Define the query with parameter placeholders, including the Available column
                    string query = "INSERT INTO CarTb (RegNumTb, BrandTb, ModelTb, PriceTb, Available) VALUES (@RegNumTb, @BrandTb, @ModelTb, @PriceTb, @Available)";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SqlCommand object
                        cmd.Parameters.AddWithValue("@RegNumTb", RegNumTb.Text);
                        cmd.Parameters.AddWithValue("@BrandTb", BrandTb.Text);
                        cmd.Parameters.AddWithValue("@ModelTb", ModelTb.Text);
                        cmd.Parameters.AddWithValue("@PriceTb", PriceTb.Text);
                        cmd.Parameters.AddWithValue("@Available", Available.SelectedItem.ToString());

                        // Open the connection, execute the query, and close the connection
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    // Show success message
                    MessageBox.Show("Car Successfully Added");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding car: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void Car_Load(object sender, EventArgs e)
        {
            population();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Check if Uid field is empty
            if (RegNumTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    // Define the query with a parameter placeholder
                    string query = "delete from CarTb where RegNumTb = @RegNumTb";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameter to the SqlCommand object
                        cmd.Parameters.AddWithValue("@RegNumTb", RegNumTb.Text);

                        // Open the connection, execute the query, and close the connection
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                    // Show success message
                    MessageBox.Show("Car Deleted Successfully");

                    // Refresh the DataGridView
                    population();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting Car: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RegNumTb.Text == "" || BrandTb.Text == "" || ModelTb.Text == "" || PriceTb.Text == "" || Available.SelectedItem == null)
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    // Define the query with parameter placeholders
                    string query = "update CarTb set BrandTb = @BrandTb, ModelTb = @ModelTb, PriceTb = @PriceTb where RegNumTb= @RegNumTb";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters to the SqlCommand object
                        cmd.Parameters.AddWithValue("@RegNumTb", RegNumTb.Text);
                        cmd.Parameters.AddWithValue("@BrandTb", BrandTb.Text);
                        cmd.Parameters.AddWithValue("@ModelTb", ModelTb.Text);
                        cmd.Parameters.AddWithValue("@PriceTb", PriceTb.Text);
                        cmd.Parameters.AddWithValue("@Available", Available.SelectedItem.ToString());
                        // Open the connection, execute the query, and close the connection
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }

                    // Close the connection (even though 'using' will automatically close it, this is for clarity)
                    con.Close();

                    // Show success message
                    MessageBox.Show("Car Updated Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating Car: " + ex.Message);
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

        private void RegNumTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void CarDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < CarDGV.Rows.Count)
            {
                // Get the selected row
                DataGridViewRow row = CarDGV.Rows[e.RowIndex];

                // Populate text boxes with the selected row's data
                RegNumTb.Text = row.Cells["RegNumTb"].Value.ToString();
                BrandTb.Text = row.Cells["BrandTb"].Value.ToString();
                ModelTb.Text = row.Cells["ModelTb"].Value.ToString();
                PriceTb.Text = row.Cells["PriceTb"].Value.ToString();
                Available.SelectedItem = row.Cells["Available"].Value.ToString();
            }
        }

        private void PriceTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void ModelTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void BrandTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            population();
        }

        private void search_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string flag = "";

            // Check if the selected item is "Available" or not, and set the flag accordingly
            if (search.SelectedItem.ToString() == "Available")
            {
                flag = "Yes";  // Assuming "Yes" means available in your database
            }
            else
            {
                flag = "No";  // Assuming "No" means not available in your database
            }

            try
            {
                // Open the connection
                con.Open();

                // Modify the query to filter cars based on the availability flag
                string query = "SELECT * FROM CarTb WHERE Available = @flag";

                // Use SqlDataAdapter with SqlCommand and parameterized query
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@flag", flag);

                // Fill the DataSet with the filtered result and bind to the DataGridView
                DataSet ds = new DataSet();
                da.Fill(ds);
                CarDGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection
                con.Close();
            }
        }

        private void search_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
