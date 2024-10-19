using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Northwind_Windows_Application
{
    public partial class EmployeeForm : Form
    {
        int rowIndex = -1;
        DataModel dm = new DataModel();
        public EmployeeForm()
        {
            InitializeComponent();
            dataGridView1.DataSource = dm.GetEmployeesDataTable();
            dataGridView1.RowTemplate.Height = 100;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_firstname.Text))
            {
                Employee em = new Employee();
                em.EmployeeID = Convert.ToInt32(tb_id.Text);
                em.FirstName = tb_firstname.Text;
                em.LastName = tb_lastname.Text;
                em.Address = tb_address.Text;
                if (dm.UpdateEmployee(em))
                {
                    MessageBox.Show("Employee Edited Successfully", "Success", MessageBoxButtons.OK);
                    dataGridView1.DataSource = dm.GetEmployeesDataTable();
                    btn_edit.Visible = false;
                    tb_firstname.Text = null;
                    tb_id.Text = null;
                    tb_lastname.Text = null;
                    tb_address.Text = null;
                }
                else
                {
                    MessageBox.Show("Employee Not Edited Successfully", "Error", MessageBoxButtons.OK);
                }

            }



        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (rowIndex >= 0)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[rowIndex].Selected = true;
                    contextMenuStrip1.Show(dataGridView1, e.X, e.Y);
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rowIndex != -1)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells["EmployeeID"].Value);
                Employee em = dm.GetEmployee(id);
                if (em != null)
                {
                    tb_id.Text = em.EmployeeID.ToString();
                    tb_lastname.Text = em.LastName;
                    tb_firstname.Text = em.FirstName;
                    tb_address.Text = em.Address;
                    btn_edit.Visible = true;

                }
                else
                {
                    MessageBox.Show("Employee data is null", "Error", MessageBoxButtons.OK);

                }
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_firstname.Text))
            {
                Employee em = new Employee();

                em.FirstName = tb_firstname.Text;
                em.LastName = tb_lastname.Text;
                em.Address = tb_address.Text;
                if (dm.AddEmployee(em))
                {
                    MessageBox.Show("Employee Added Successfully", "Success", MessageBoxButtons.OK);
                    dataGridView1.DataSource = dm.GetEmployeesDataTable();
                    btn_edit.Visible = false;
                    tb_firstname.Text = null;
                    tb_id.Text = null;
                    tb_lastname.Text = null;
                    tb_address.Text = null;
                }
                else
                {
                    MessageBox.Show("Employee Couldn't Added Successfully", "Error", MessageBoxButtons.OK);
                }
            }
        }
    }
}
