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
using System.Xml.Linq;

namespace Car_Rental_Project
{
    public partial class Rental : Form
    {
        public Rental()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=NILOY\\SQLEXPRESS02;Integrated Security=True");
        private void fillcombo()
        {
            con.Open();
            string query = "select RegNumTb from CarTb";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNumTb", typeof(string));
            dt.Load(rdr);
            CarReg.ValueMember = "RegNumTb";
            CarReg.DataSource = dt;
            con.Close();
        }

        private void fillCustomer() 
        {
            con.Open();
            string query = "select IdTb from CustomerTab";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("IdTb", typeof(int));
            dt.Load(rdr);
            CustCb.ValueMember = "IdTb";
            CustCb.DataSource = dt;
            con.Close();
        }
        private void population()
        {
            con.Open();
            string query = "select * from RentalTab";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RentalDGV.DataSource = ds.Tables[0];
            con.Close();
        }

        private void updateRent()
        {
            try
            {
                string query = "UPDATE CarTb SET Available = @Available WHERE RegNumTb = @RegNum";

                using (SqlConnection con = new SqlConnection("Data Source=NILOY\\SQLEXPRESS02;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Available", "NO");
                        cmd.Parameters.AddWithValue("@RegNum", CarReg.SelectedValue.ToString());

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void BringCustName() //recover the name of a customer from the CustomerTab table in the SQL database
        {
            con.Open();
            string query = "select NameTb from CustomerTab where IdTb = @IdTb";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@IdTb", CustCb.SelectedValue);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);    
            foreach(DataRow dr in dt.Rows)
            {
                CustName.Text = dr["NameTb"].ToString();
            }
            con.Close();
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Rental_Load(object sender, EventArgs e)
        {
            fillcombo();
            fillCustomer();
            population();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CarRegCb_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void CustCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BringCustName();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RentId.Text == "" || CustName.Text == "" || RentFee.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    // Validate and parse dates
                    if (!DateTime.TryParse(RentalDate.Text, out DateTime rentalDate) ||
                        !DateTime.TryParse(ReturnDate.Text, out DateTime returnDate))
                    {
                        MessageBox.Show("Please enter valid dates.");
                        return;
                    }

                    // Define the query with parameter placeholders
                    string query = "INSERT INTO RentalTab (RentId, CarReg, CustName, RentalDate, ReturnDate, RentFee) " +
                                   "VALUES (@RentId, @CarReg, @CustName, @RentalDate, @ReturnDate, @RentFee)";

                    // Create a SqlCommand object with the query and connection
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Add parameters with appropriate data types
                        cmd.Parameters.AddWithValue("@RentId", RentId.Text);
                        cmd.Parameters.AddWithValue("@CustName", CustName.Text);
                        cmd.Parameters.AddWithValue("@RentalDate", rentalDate);  // DateTime object
                        cmd.Parameters.AddWithValue("@ReturnDate", returnDate);  // DateTime object
                        cmd.Parameters.AddWithValue("@RentFee", Convert.ToDecimal(RentFee.Text)); // Convert RentFee to decimal

                        // Ensure CarReg is not null or empty before adding
                        if (CarReg.SelectedValue != null)
                        {
                            cmd.Parameters.AddWithValue("@CarReg", CarReg.SelectedValue.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Please select a car.");
                            return;
                        }

                        // Open the connection, execute the query, and close the connection
                        con.Open();
                        cmd.ExecuteNonQuery();
                        updateRent();  // Update rental records
                        con.Close();
                       
                    }

                    MessageBox.Show("Car Rented Successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Renting Car: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void CustNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RentId.Text == "" || CustName.Text == "" || RentFee.Text == "")
    {
        MessageBox.Show("Missing information");
    }
    else
    {
        try
        {
            // Validate and parse dates
            if (!DateTime.TryParse(RentalDate.Text, out DateTime rentalDate) ||
                !DateTime.TryParse(ReturnDate.Text, out DateTime returnDate))
            {
                MessageBox.Show("Please enter valid dates.");
                return;
            }

            // Define the query with parameter placeholders
            string query = "UPDATE RentalTab SET CarReg = @CarReg, CustName = @CustName, " +
                           "RentalDate = @RentalDate, ReturnDate = @ReturnDate, RentFee = @RentFee " +
                           "WHERE RentId = @RentId";

            // Create a SqlCommand object with the query and connection
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Add parameters with appropriate data types
                cmd.Parameters.AddWithValue("@RentId", RentId.Text);
                cmd.Parameters.AddWithValue("@CustName", CustName.Text);
                cmd.Parameters.AddWithValue("@RentalDate", rentalDate);  // DateTime object
                cmd.Parameters.AddWithValue("@ReturnDate", returnDate);  // DateTime object
                cmd.Parameters.AddWithValue("@RentFee", Convert.ToDecimal(RentFee.Text)); // Convert RentFee to decimal

                // Ensure CarReg is not null or empty before adding
                if (CarReg.SelectedValue != null)
                {
                    cmd.Parameters.AddWithValue("@CarReg", CarReg.SelectedValue.ToString());
                }
                else
                {
                    MessageBox.Show("Please select a car.");
                    return;
                }

                // Open the connection, execute the query, and close the connection
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            // Show success message
            MessageBox.Show("Car Rental Updated Successfully");

            // Refresh the DataGridView to reflect changes
            population();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error updating Car Rental: " + ex.Message);
        }
        finally
        {
            con.Close();
        }
    }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Check if RentId field is empty
            if (RentId.Text == "")
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                // Define the query to delete a record from the RentalTab table
                // Use the correct table name (assumed to be RentalTab)
                string query = "DELETE FROM RentalTab WHERE RentId = @RentId";

                // Create a SqlCommand object with the query and connection
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to the SqlCommand object
                    cmd.Parameters.AddWithValue("@RentId", RentId.Text);

                    // Open the connection, execute the query, and close the connection
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                // Show success message
                MessageBox.Show("Car Deleted Successfully");

                // Refresh the DataGridView to reflect changes
                population();
            }
            catch (Exception ex)
            {
                // Show error message if an exception occurs
                MessageBox.Show("Error deleting Car: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainfrom mainfrom = new Mainfrom();
            mainfrom.Show();
        }

        private void RentFee_TextChanged(object sender, EventArgs e)
        {

        }

        private void RentalDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // this line means that for example if i have 4 rows i can not click 5th rows because which line has not any information 
            // like i only click 0,1,2,3 ...
            if (e.RowIndex >= 0 && e.RowIndex < RentalDGV.Rows.Count)
            {
                // Get the selected row
                DataGridViewRow row = RentalDGV.Rows[e.RowIndex];

                // Populate text boxes with the selected row's data
                RentId.Text = row.Cells["RentId"].Value.ToString();
                CarReg.SelectedValue = row.Cells["CarReg"].Value.ToString();
                CustName.Text = row.Cells["CustName"].Value.ToString();
                RentalDate.Text = row.Cells["RentalDate"].Value.ToString();
                ReturnDate.Text = row.Cells["ReturnDate"].Value.ToString();
                RentFee.Text = row.Cells["RentFee"].Value.ToString();
            }
        }

        private void RentId_TextChanged(object sender, EventArgs e)
        {

        }

        private void CarReg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CustCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
