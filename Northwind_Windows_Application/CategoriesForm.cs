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
using DataAccessLayer;

namespace Northwind_Windows_Application
{
    public partial class CategoriesForm : Form
    {
        int rowIndex = -1;
        public CategoriesForm()
        {
            InitializeComponent();
            //DataGridBasics();
            //DataGridDataBound();
            //DataGridDataTableDataBound();
            //DataGridCollectionToDataTableDataBound();
            //LongWayDataBaseConnecting();
        }


        //public void DataGridBasics()
        //{
        //    //adding columns
        //    dataGridView1.ColumnCount = 3;
        //    dataGridView1.Columns[0].Name = "Column 1";
        //    dataGridView1.Columns[1].Name = "Column 2";
        //    dataGridView1.Columns[2].Name = "Column 3";
        //    //adjusting columns
        //    dataGridView1.Columns[2].Width = 200;
        //    dataGridView1.Columns[1].Width = 200;
        //    //adding rows
        //    dataGridView1.Rows.Add("1", "Murtaza", "Suayipoglu");
        //    dataGridView1.Rows.Add("1", "Murtaza", "Suayipoglu");
        //}
        //public void DataGridDataBound()
        //{
        //    // taking data from somewhere is data bound
        //    //adding columns
        //    dataGridView1.ColumnCount = 3;
        //    dataGridView1.Columns[0].Name = "Column 1";
        //    dataGridView1.Columns[1].Name = "Column 2";
        //    dataGridView1.Columns[2].Name = "Column 3";
        //    //adjusting columns
        //    dataGridView1.Columns[2].Width = 200;
        //    dataGridView1.Columns[1].Width = 200;

        //    List<Category> categories = new List<Category>();
        //    categories.Add(new Category() { ID = 1, Name = "Vegetables", Description = "Brocolies, peppers etc." });
        //    categories.Add(new Category() { ID = 2, Name = "Delicatessen", Description = "Meats, olives etc." });
        //    categories.Add(new Category() { ID = 3, Name = "Clothing", Description = "Shoes, hats etc." });

        //    foreach (Category item in categories)
        //    {
        //        dataGridView1.Rows.Add(item.ID, item.Name, item.Description);
        //    }

        //}
        //public void DataGridDataTableDataBound()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("ID");
        //    dt.Columns.Add("Name");
        //    dt.Columns.Add("Description");

        //    DataRow row = dt.NewRow();
        //    row["ID"] = 1;
        //    row["Name"] = "Vegetables";
        //    row["Description"] = "Description";
        //    dt.Rows.Add(row);
        //    dataGridView1.DataSource = dt;
        //}
        //public void DataGridCollectionToDataTableDataBound()
        //{
        //    List<Category> categories = new List<Category>();
        //    categories.Add(new Category() { ID = 1, Name = "Vegetables", Description = "Brocolies, peppers etc." });
        //    categories.Add(new Category() { ID = 2, Name = "Delicatessen", Description = "Meats, olives etc." });
        //    categories.Add(new Category() { ID = 3, Name = "Clothing", Description = "Shoes, hats etc." });

        //    BindingList<Category> bind = new BindingList<Category>(categories);
        //    dataGridView1.DataSource = new BindingSource(bind, null);
        //}
        //public void LongWayDataBaseConnecting()
        //{
        //    SqlConnection connection = new SqlConnection();
        //    //DESKTOP-AG8FECU
        //    connection.ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=NORTHWND;Integrated Security=True";
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = connection;
        //    cmd.CommandText = "SELECT CategoryID,CategoryName,Description FROM Categories";
        //    connection.Open();
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    List<Category> categories = new List<Category>();
        //    while (reader.Read())
        //    {
        //        Category c = new Category();
        //        c.ID = reader.GetInt32(0);
        //        c.Name = reader.GetString(1);
        //        c.Description = reader.GetString(2);
        //        categories.Add(c);
        //    }
        //    connection.Close();

        //    BindingList<Category> bind = new BindingList<Category>(categories);
        //    dataGridView1.DataSource = new BindingSource(bind, null);
        //}

        DataModel dm = new DataModel();
        private void CategoriesForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dm.GetCategoryDataTable();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_name.Text))
            {
                Category c = new Category();
                c.CategoryName = tb_name.Text;
                c.Description = tb_description.Text;
                if (dm.AddCategory(c))
                {
                    MessageBox.Show("Category Added Successfully","Success", MessageBoxButtons.OK);
                    dataGridView1.DataSource = dm.GetCategoryDataTable();
                }
                else
                {
                    MessageBox.Show("Please try again later", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Category name should not be empty!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void contextMenuStrip1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Right)
            {
                rowIndex = dataGridView1.HitTest(e.X,e.Y).RowIndex;
                
                if (rowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[rowIndex].Selected = true;
                    contextMenuStrip1.Show(dataGridView1,e.X,e.Y);
                }
            }
        }

        private void tsmi_edit_Click(object sender, EventArgs e)
        {
            if (rowIndex != -1)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["CategoryID"].Value);
                Category c = dm.GetCategory(id);
                if (c!=null)
                {
                    tb_id.Text = c.CategoryID.ToString() ;
                    tb_name.Text = c.CategoryName;
                    tb_description.Text = c.Description;
                    btn_edit.Visible = true;

                }
            }
        }

        private void tsmi_delete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["CategoryID"].Value);
            string name = Convert.ToString(dataGridView1.Rows[rowIndex].Cells["CategoryName"].Value);
            if (MessageBox.Show("Are you sure you wanted to delete the category of "+name+" ?","Deleting Category",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                dm.DeleteCategory(id);
                btn_edit.Visible = false;
                tb_description.Text = null;
                tb_id.Text = null;
                tb_name.Text = null;

            }
            dataGridView1.DataSource = dm.GetCategoryDataTable();
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_name.Text))
            {
                Category c = dm.GetCategory(Convert.ToInt32(tb_id.Text));

                c.CategoryName = tb_name.Text;
                c.Description = tb_description.Text;

                if (dm.EditCategory(c))
                {
                    MessageBox.Show("Category Edited Successfully", "Success", MessageBoxButtons.OK);
                    dataGridView1.DataSource = dm.GetCategoryDataTable();
                    btn_edit.Visible = false;
                    tb_description.Text = null;
                    tb_id.Text = null;
                    tb_name.Text = null;
                }
                else
                {
                    MessageBox.Show("An error occurred while editing the category.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Please select a category to edit it.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
    //class Category
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //}
}
