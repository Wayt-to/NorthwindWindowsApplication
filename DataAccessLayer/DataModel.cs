using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataModel
    {
        SqlConnection con; SqlCommand cmd;

        public DataModel()
        {
            con = new SqlConnection(ConnectionStrings.ConStr);
            cmd = con.CreateCommand();
        }
        #region Category Funcitons

        //List
        public List<Category> GetCategoryList()
        {
            List<Category> categories = new List<Category>();
            try
            {
                cmd.CommandText = "SELECT * FROM Categories";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category c = new Category();
                    c.CategoryID = reader.GetInt32(0);
                    c.CategoryName = reader.GetString(1);
                    c.Description = reader.GetString(2);
                    categories.Add(c);
                    
                }
                
                return categories;

            }
            finally
            {
                con.Close();
            }
        }

        public DataTable GetCategoryDataTable()
        {
            DataTable dt = new DataTable();
            cmd.CommandText = "SELECT * FROM Categories";
            cmd.Parameters.Clear();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            return dt;
        }

        public bool AddCategory(Category c)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Categories(CategoryName,Description) VALUES (@cn,@d)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@cn",c.CategoryName);
                cmd.Parameters.AddWithValue("d", c.Description);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {

                return false;

            }
            finally
            {
                con.Close();
            }
        }

        public Category GetCategory(int id)
        {
            try
            {
                Category c = new Category();
                cmd.CommandText = "SELECT CategoryID,CategoryName,Description FROM Categories WHERE CategoryID = @id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id",id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    c.CategoryID = reader.GetInt32(0);
                    c.CategoryName = reader.GetString(1);
                    c.Description = reader.IsDBNull(2) ? "" : reader.GetString(2);
                }
                return c;


            }
            catch 
            {
                return null;
                
            }
            finally
            {
                con.Close();

            }
        }

        public void DeleteCategory(int id)
        {
            try
            {
                cmd.CommandText = "DELETE FROM Categories WHERE CategoryID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id",id);
                con.Open();
                cmd.ExecuteNonQuery();   
            }
            finally
            {
                con.Close();
            }
        }

        public bool EditCategory(Category c)
        {
            try
            {
                cmd.CommandText = "UPDATE Categories SET CategoryName = @cn, Description = @d WHERE CategoryID = @id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", c.CategoryID);
                cmd.Parameters.AddWithValue("@cn", c.CategoryName);
                cmd.Parameters.AddWithValue("@d", c.Description);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {

                return false;

            }
            finally
            {
                con.Close();
            }
        }
        #endregion

        #region Product Functions
        public List<Product> GetProductList()
        {
            List<Product> products = new List<Product>();
            try
            {
                cmd.CommandText = "SELECT P.ProductID,P.ProductName,C.CategoryName,S.CompanyName,P.QuantityPerUnit,P.UnitPrice,P.UnitsInStock,P.ReorderLevel,P.Discontinued,P.CategoryID,P.SupplierID FROM Products AS P JOIN Categories AS C ON P.CategoryID=C.CategoryID JOIN Suppliers AS S ON P.SupplierID=S.SupplierID";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product p = new Product();
                    
                    p.ProductID = reader.GetInt32(0);
                    p.ProductName = reader.GetString(1);
                    p.CategoryName = reader.GetString(2);
                    p.SupplierName = reader.GetString(3);
                    p.QuantityPerUnit = reader.GetString(4);
                    p.UnitPrice = reader.GetDecimal(5);
                    p.UnitsOnStock = reader.GetInt16(6);
                    p.ReorderLevel = reader.GetInt16(7);
                    p.Discontinued = reader.GetBoolean(8);
                    p.DiscontinuedStr = reader.GetBoolean(8)?"Yes":"No";
                    p.CategoryID = reader.GetInt32(9);
                    p.SupplierID = reader.GetInt32(10);
                    products.Add(p);

                }

                return products;

            }
            finally
            {
                con.Close();
            }
        }


        #endregion

        #region Employee Functions

        public BindingList<Employee> GetEmployeeList()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                cmd.CommandText = "SELECT * FROM Employees";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee e = new Employee();
                    e.EmployeeID = reader.GetInt32(0);
                    e.FirstName = reader.GetString(1);
                    e.LastName = reader.GetString(2);
                    e.Address = reader.GetString(3);
                    employees.Add(e);
                }
                BindingList<Employee> bind = new BindingList<Employee>(employees);
                return bind;
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable GetEmployeesDataTable()
        {
            DataTable dt = new DataTable();
            cmd.CommandText = "SELECT * FROM Employees";
            cmd.Parameters.Clear();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            return dt;
        }

        public bool UpdateEmployee(Employee e)
        {
            try
            {
                cmd.CommandText = "UPDATE Employees SET FirstName=@fn, LastName=@ln, Address=@ad WHERE EmployeeID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", e.EmployeeID);
                cmd.Parameters.AddWithValue("@ln", e.LastName);
                cmd.Parameters.AddWithValue("@fn", e.FirstName);
                cmd.Parameters.AddWithValue("@ad", e.Address);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;

            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public Employee GetEmployee(int id)
        {
            try
            {
                cmd.CommandText = "SELECT FirstName, LastName, Address FROM Employees WHERE EmployeeID=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Employee em = new Employee();
                while (reader.Read())
                {
                    
                    em.EmployeeID = id;
                    em.FirstName = reader.GetString(0);
                    em.LastName = reader.GetString(1);
                    em.Address = reader.GetString(2);
                }
                return em;

            }
            catch
            {
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public bool AddEmployee(Employee e)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Employees(FirstName, LastName, Address)  VALUES(@fn,@ln,@ad)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ln", e.LastName);
                cmd.Parameters.AddWithValue("@fn", e.FirstName);
                cmd.Parameters.AddWithValue("@ad", e.Address);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;

            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion

    }
}
