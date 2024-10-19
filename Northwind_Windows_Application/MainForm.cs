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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public void FormOpen(Form frm)
        {
            Form[] forms = this.MdiChildren;
            bool isopen = false;
            Type type = frm.GetType();
            
            foreach (Form item in forms)
            {
                if (item.GetType() == frm.GetType())
                {
                    isopen = true;
                    item.Activate();
                }
            }
            if (!isopen)
            {
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void TSMI_Category_Click(object sender, EventArgs e)
        {
            FormOpen(new CategoriesForm());
        }

        private void TSMI_Product_Click(object sender, EventArgs e)
        {
            FormOpen(new ProductsForm());
        }

        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpen(new SuppliersForm());
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOpen(new EmployeeForm());
        }
    }
}
