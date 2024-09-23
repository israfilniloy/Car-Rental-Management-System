# Car Rental Management System

## Project Overview:
This project demonstrates a comprehensive Car Rental Management System built using C# and SQL Server within a Windows Forms Application. 
The system facilitates the management of car rentals, returns,car information,customer information,dashboard, and user authentication, providing an intuitive interface for both administrators and users.

## Table of Contents:

## Technologies Used:
- **C#**: Core programming language for the Windows Forms application.
- **SQL Server**: Database for storing rental and return records, customer information, user information, and car data.
- **Windows Forms**: Provides the graphical user interface (GUI) for user interactions.
- **Visual Studio**: IDE for building and debugging the application.

## Features:
- **Login From**:
  - Responsible for user authentication, allowing administrators to log in securely to access the car rental management system.
  - Contains fields for entering the username and password, with validation checks to ensure correct credentials are provided.
  - Implements feedback mechanisms for login success or failure, guiding users appropriately.
  - Upon successful login, navigates users to the main dashboard for further actions.

- **Main Form**:
  - Acts as the central hub for navigation to different functionalities of the application, including managing car rentals, returns, and viewing the dashboard.
  - Provides a user-friendly interface for administrators to access various sections like car management, customer management, and rental processing.
  - Ensures smooth transitions between different forms in the application for an efficient user experience.

- **User Authentication**: 
  - Secure login for administrators to access the management features.

- **Car Information**:
  - Manage car records, including adding new cars, updating existing car details, and deleting car entries.
  - Track car registration numbers, availability status, and rental fees.
  - Ensure that only available cars can be rented by updating their status in the database upon rental and return.
  - Retrieve and display car information for users to select when renting a vehicle.

- **Customer Information**:
  - Manage customer records, including adding new customers, updating existing details, and deleting customer entries.
  - View customer names and IDs, ensuring easy access to customer information during rentals and returns.
  - Validate customer input to ensure accurate data entry and maintain database integrity.

- **Car Rental Management**: 
  - Add new rentals by inputting car registration, customer name, and rental dates.
  - View all current rentals in a grid format, allowing for easy management.
  - Update existing rental records with new details.
  - Delete rental records to maintain an accurate database.

- **Car Return Management**: 
  - Track returned cars, including return dates and any associated fines for delays.
  - View return history and current returns in the dashboard.
    
- **Search Functionality**: 
  - Quickly search for rental records by car registration or customer name.
    
- **Dashboard**: 
  - Provides key statistics such as total number of cars, customers, and users.
  - Easy access to manage various sections of the application.

## Database Schema:
**Tables:**
1. **RentalTab**:
   - RentId (Primary Key, int)
   - CarReg (nvarchar(50))
   - CustName (nvarchar(50))
   - RentDate (datetime)
   - ReturnDate (datetime)
   
2. **ReturnTab**:
   - ReturnId (Primary Key, int)
   - CarReg (nvarchar(50))
   - CustName (nvarchar(50))
   - ReturnDate (datetime)
   
3. **CustomerTab**:
   - CustId (Primary Key, int)
   - CustName (nvarchar(50))
   - CustPhone (nvarchar(15))
   
4. **CarTb**:
   - CarId (Primary Key, int)
   - CarReg (nvarchar(50))
   - Brand (nvarchar(50))
   
5. **UTab** (User Table):
   - UserId (Primary Key, int)
   - Username (nvarchar(50))
   - Password (nvarchar(50))

## Setup SQL Server Database:
1. **Open SQL Server Management Studio (SSMS)** and connect to your SQL Server instance.
2. **Create the Database**: Use the provided SQL scripts to set up the database tables and insert initial data.
3. **Update Connection String**: Modify the connection string in your C# project:
   ```csharp
   SqlConnection con = new SqlConnection("Data Source=NILOY\\SQLEXPRESS02;Integrated Security=True");
   ```

## C# Windows Forms Application Setup:
1. **Open Visual Studio** and create a new project using the template:
   - C# > Windows Forms App (.NET Framework)
2. **Design Forms**:
   - Create forms for Car Login,MainFrom,Users,Car information,Customer information,Rentals,Returns and Dashboard.
   - Include `DataGridView` for displaying records and text boxes for input.

3. **Configure SQL Server Connection** in your code:
   ```csharp
   string connectionString = "Data Source=NILOY\\SQLEXPRESS02;Initial Catalog=CarRentalDB;Integrated Security=True;";
   ```

## CRUD Operations:
### For Login From:
To validate user credentials and log in:
```csharp
private void button1_Click(object sender, EventArgs e)
{
    string query = "select count(*) from UTab where Uname= '" + Uname.Text + "' and Upass ='" + Upass.Text + "'";
    con.Open();
    SqlDataAdapter sda = new SqlDataAdapter(query, con);
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
```
### For Users form:
To Insert Data:
```csharp
 private void button1_Click(object sender, EventArgs e)
        {
            if (Uid.Text == "" || Uname.Text == "" || Upass.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    string query = "insert into UTab (Id, Uname, Upass) values (@Id, @Uname, @Upass)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", Uid.Text);
                        cmd.Parameters.AddWithValue("@Uname", Uname.Text);
                        cmd.Parameters.AddWithValue("@Upass", Upass.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
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
```
To Update Data:
```csharp
 private void button2_Click(object sender, EventArgs e)
        {
            if (Uid.Text == "" || Uname.Text == "" || Upass.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    string query = "update UTab set Uname = @Uname, Upass = @Upass where Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Uname", Uname.Text);
                        cmd.Parameters.AddWithValue("@Upass", Upass.Text);
                        cmd.Parameters.AddWithValue("@Id", Uid.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
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
```
 To Delete Data:
```csharp
 private void button3_Click(object sender, EventArgs e)
        {
            if (Uid.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    string query = "delete from UTab where Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", Uid.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("User Deleted Successfully");
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
```
Load Data:
To Populate the data into DataGridView:
```csharp
private void population()
        {
            con.Open();
            string query = "select * from UTab";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            con.Close();
        }
```

### For Car From:
TO Insert Data:
```csharp
private void button1_Click(object sender, EventArgs e)
        {
            if (RegNumTb.Text == "" || BrandTb.Text == "" || ModelTb.Text == "" || PriceTb.Text == "" || Available.SelectedItem == null)
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                try
                {
                    string query = "INSERT INTO CarTb (RegNumTb, BrandTb, ModelTb, PriceTb, Available) VALUES (@RegNumTb, @BrandTb, @ModelTb, @PriceTb, @Available)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RegNumTb", RegNumTb.Text);
                        cmd.Parameters.AddWithValue("@BrandTb", BrandTb.Text);
                        cmd.Parameters.AddWithValue("@ModelTb", ModelTb.Text);
                        cmd.Parameters.AddWithValue("@PriceTb", PriceTb.Text);
                        cmd.Parameters.AddWithValue("@Available", Available.SelectedItem.ToString());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
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
```
TO Update Data:
```csharp
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
                    string query = "update CarTb set BrandTb = @BrandTb, ModelTb = @ModelTb, PriceTb = @PriceTb where RegNumTb= @RegNumTb";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RegNumTb", RegNumTb.Text);
                        cmd.Parameters.AddWithValue("@BrandTb", BrandTb.Text);
                        cmd.Parameters.AddWithValue("@ModelTb", ModelTb.Text);
                        cmd.Parameters.AddWithValue("@PriceTb", PriceTb.Text);
                        cmd.Parameters.AddWithValue("@Available", Available.SelectedItem.ToString());
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
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
```
To Delete Data:
```csharp
 private void button3_Click(object sender, EventArgs e)
        {
            if (RegNumTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    string query = "delete from CarTb where RegNumTb = @RegNumTb";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RegNumTb", RegNumTb.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("Car Deleted Successfully");
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
```
Load Data:
```csharp
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
```
To Search Data:
```csharp
private void search_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string flag = "";
            if (search.SelectedItem.ToString() == "Available")
            {
                flag = "Yes";
            }
            else
            {
                flag = "No";
            }

            try
            {
                con.Open();
                string query = "SELECT * FROM CarTb WHERE Available = @flag";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@flag", flag);
                DataSet ds = new DataSet();
                da.Fill(ds);
                CarDGV.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
```
### Insert Data:
To insert a new rental record:
```csharp
private void AddRental()
{
    using (SqlConnection con = new SqlConnection(connectionString))
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("INSERT INTO RentalTab (RentId, CarReg, CustName, RentDate, ReturnDate) VALUES (@RentId, @CarReg, @CustName, @RentDate, @ReturnDate)", con);
        cmd.Parameters.AddWithValue("@RentId", int.Parse(RentId.Text));
        cmd.Parameters.AddWithValue("@CarReg", CarReg.Text);
        cmd.Parameters.AddWithValue("@CustName", CustName.Text);
        cmd.Parameters.AddWithValue("@RentDate", RentDate.Value);
        cmd.Parameters.AddWithValue("@ReturnDate", ReturnDate.Value);
        cmd.ExecuteNonQuery();
        con.Close();
        MessageBox.Show("Rental Added Successfully");
    }
}
```

### Update Data:
To update a car rental record:
```csharp
private void UpdateRental()
{
    using (SqlConnection con = new SqlConnection(connectionString))
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("UPDATE RentalTab SET CarReg=@CarReg, CustName=@CustName, RentDate=@RentDate, ReturnDate=@ReturnDate WHERE RentId=@RentId", con);
        cmd.Parameters.AddWithValue("@RentId", int.Parse(RentId.Text));
        cmd.Parameters.AddWithValue("@CarReg", CarReg.Text);
        cmd.Parameters.AddWithValue("@CustName", CustName.Text);
        cmd.Parameters.AddWithValue("@RentDate", RentDate.Value);
        cmd.Parameters.AddWithValue("@ReturnDate", ReturnDate.Value);
        cmd.ExecuteNonQuery();
        con.Close();
        MessageBox.Show("Rental Updated Successfully");
    }
}
```

### Delete Data:
To delete a car rental record:
```csharp
private void DeleteRental()
{
    using (SqlConnection con = new SqlConnection(connectionString))
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("DELETE FROM RentalTab WHERE RentId=@RentId", con);
        cmd.Parameters.AddWithValue("@RentId", int.Parse(RentId.Text));
        cmd.ExecuteNonQuery();
        con.Close();
        MessageBox.Show("Rental Deleted Successfully");
    }
}
```

### Load Data:
To load the rental records into a `DataGridView`:
```csharp
private void LoadRentals()
{
    using (SqlConnection con = new SqlConnection(connectionString))
    {
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM RentalTab", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        RentalDGV.DataSource = dt;
    }
}
```

## Dashboard:
The **Dashboard** provides key statistics about the system:
- **Total Cars**: Displays the count of records in the `CarTb` table.
- **Total Customers**: Displays the count of records in the `CustomerTab`.
- **Total Users**: Displays the count of records in the `UTab`.

## Challenges and Considerations:
- **Error Handling**: Implement error handling to manage database connection issues or invalid inputs.
- **Data Validation**: Ensure proper validation to avoid incorrect data entries, such as checking for empty fields before performing operations.
- **Security**: Implement measures to protect sensitive information, especially user passwords.

## How to Run the Project:
1. **Clone the Repository**: Clone the project from the GitHub repository.
   ```
   git clone https://github.com/israfilniloy/CarRentalSystem.git
   ```
2. **Set Up the Database**: Use the provided SQL script to set up the necessary tables and insert initial data.
3. **Build the Project**: Open the project in Visual Studio and build the solution.
4. **Run the Application**: Manage rental records, return cars, and view statistics through the Windows Forms interface.

## Screenshots:
_Add screenshots showing the forms and interface in action here._

---

Feel free to adjust any sections further or let me know if you need more specific information added!
