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
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=NILOY\\SQLEXPRESS02;Integrated Security=True");
        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

       
        private void populationReturn()
        {
            con.Open();
            string query = "select * from ReturnTab";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Mainfrom mainfrom = new Mainfrom();
            mainfrom.Show();
        }

        private void Return_Load(object sender, EventArgs e)
        {
            population();
            populationReturn();
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (ReturnId.Text == "" || CarReg.Text == "" || CustName.Text == "")
    {
        MessageBox.Show("Missing information");
    }
    else
    {
        try
        {
            // Validate and parse dates
            if (!DateTime.TryParse(ReturnDate.Text, out DateTime returnDate))
            {
                MessageBox.Show("Please enter valid dates.");
                return;
            }

            // Define the query to insert a return record
            string query = "INSERT INTO ReturnTab (ReturnId, CarReg, CustName, ReturnDate) " +
                           "VALUES (@ReturnId, @CarReg, @CustName, @ReturnDate)";

            // Create a SqlCommand object with the query and connection
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // Add parameters with appropriate data types
                cmd.Parameters.AddWithValue("@ReturnId", ReturnId.Text);
                cmd.Parameters.AddWithValue("@CustName", CustName.Text);
                cmd.Parameters.AddWithValue("@ReturnDate", returnDate);  // DateTime object
                cmd.Parameters.AddWithValue("@CarReg", CarReg.Text);  // CarReg from TextBox

                // Open the connection, execute the query, and close the connection
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                // Refresh the DataGridViews
                populationReturn();  // Refresh ReturnDGV (Cars Returned)
                    // Delete car from RentalTab after return
            }

            // Show success message
            MessageBox.Show("Car Successfully Returned");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error Returning Car: " + ex.Message);
        }
        finally
        {
            con.Close();
        }
    }
        }


        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // this line means that for example if i have 4 rows i can not click 5th rows because which line has not any information 
            // like i only click 0,1,2,3 ...
            if (e.RowIndex >= 0 && e.RowIndex < RentalDGV.Rows.Count)
            {
                // Get the selected row
                DataGridViewRow row = RentalDGV.Rows[e.RowIndex];

                // Populate text boxes with the selected row's data
  
                CarReg.Text = row.Cells["CarReg"].Value.ToString();
                CustName.Text = row.Cells["CustName"].Value.ToString();
                ReturnDate.Text = row.Cells["ReturnDate"].Value.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (ReturnId.Text == "")
            {
                MessageBox.Show("Missing Information");
                return;
            }

            try
            {
                // Define the query to delete a record from the RentalTab table
                // Use the correct table name (assumed to be RentalTab)
                string query = "DELETE FROM ReturnTab WHERE ReturnId = @ReturnId";

                // Create a SqlCommand object with the query and connection
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameter to the SqlCommand object
                    cmd.Parameters.AddWithValue("@ReturnId", ReturnId.Text);

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

        private void ReturnDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < ReturnDGV.Rows.Count)
            {
                // Get the selected row
                DataGridViewRow row = ReturnDGV.Rows[e.RowIndex];

                // Populate text boxes with the selected row's data
                ReturnId.Text = row.Cells["ReturnId"].Value.ToString();
                CarReg.Text = row.Cells["CarReg"].Value.ToString();
                CustName.Text = row.Cells["CustName"].Value.ToString();
                ReturnDate.Text = row.Cells["ReturnDate"].Value.ToString();
            }
        }
    }
}
