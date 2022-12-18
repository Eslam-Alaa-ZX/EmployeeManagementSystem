using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class Departments : Form
    {
        Functions Con;
        int key = 0;
        public Departments()
        {
            InitializeComponent();
            Con = new Functions();
            ShowDepartments();
        }

        private void ShowDepartments()
        {
            String Query = "Select * from DepartmentsTB";
            DepGV.DataSource = Con.GetData(Query);
        }

        private void Clear()
        {
            DepName.Text = "";
            
        }

        private void Departments_Load(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (DepName.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                String name = DepName.Text;
                String Query = "insert into DepartmentsTB values('{0}')";
                Query = string.Format(Query, name);
                Con.SetData(Query);
                ShowDepartments();
                Clear();
                MessageBox.Show("Department Added!!!");
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (DepName.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                String name = DepName.Text;
                String Query = "Update DepartmentsTB set DepName = '{0}' where DepId = {1}";
                Query = string.Format(Query, name,key);
                Con.SetData(Query);
                ShowDepartments();
                Clear();
                MessageBox.Show("Department Updated!!!");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select A Department!!!");
            }
            else
            {

                String Query = "Delete from DepartmentsTB where DepId = {0}";
                Query = string.Format(Query, key);
                Con.SetData(Query);
                ShowDepartments();
                Clear();
                MessageBox.Show("Department Deleted!!!");
            }
        }

        private void DepGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DepGV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            Console.WriteLine(DepGV.SelectedRows.Count);
            DepName.Text = DepGV.SelectedRows[0].Cells[1].Value.ToString();
            if (DepName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(DepGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Employees page = new Employees();
            page.Show();
            this.Hide();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            Departments page = new Departments();
            page.Show();
            this.Hide();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Salary page = new Salary();
            page.Show();
            this.Hide();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            Login page = new Login();
            page.Show();
            this.Hide();
        }
    }
}
