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
    public partial class Employees : Form
    {
        Functions Con;
        int key = 0;
        public Employees()
        {
            InitializeComponent();
            Con = new Functions();
            ShowEmployees();
            GetDepartments();
        }

        private void ShowEmployees()
        {
            String Query = "Select * from EmployeesTB";
            EmpGV.DataSource = Con.GetData(Query);
        }

        private void GetDepartments()
        {
            string Query = "Select * from DepartmentsTB";
            EmpDep.DisplayMember = Con.GetData(Query).Columns["DepName"].ToString();
            EmpDep.ValueMember = Con.GetData(Query).Columns["DepId"].ToString();
            EmpDep.DataSource = Con.GetData(Query);
        }
        private void Clear()
        {
            EmpName.Text = "";
            EmpGen.SelectedIndex = -1;
            EmpDep.SelectedIndex = -1;
            EmpSal.Text = "";
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Employees page = new Employees();
            page.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Departments page = new Departments();
            page.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Salary page = new Salary();
            page.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Login page = new Login();
            page.Show();
            this.Hide();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (EmpSal.Text == "" || EmpDep.SelectedIndex == -1 || EmpGen.SelectedIndex == -1 || EmpName.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                String name = EmpName.Text;
                String gender = EmpGen.SelectedItem.ToString();
                int department = Convert.ToInt32(EmpDep.SelectedValue.ToString());
                String DOB = EmpDOB.Value.Date.ToShortDateString().ToString();
                String JDate = EmpJD.Value.Date.ToShortDateString().ToString();
                //birthDate = "CAST('"+birthDate+"', 0)";
                int salary = Convert.ToInt32(EmpSal.Text);
                String Query = "insert into EmployeesTB values('{0}','{1}',{2},'{3}','{4}',{5})";
                Query = string.Format(Query, name, gender,department, DOB, JDate, salary);
                Con.SetData(Query);
                ShowEmployees();
                Clear();
                MessageBox.Show("Employee Added!!!");
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (EmpSal.Text == "" || EmpDep.SelectedIndex == -1 || EmpGen.SelectedIndex == -1 || EmpName.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                String name = EmpName.Text;
                String gender = EmpGen.SelectedItem.ToString();
                int department = Convert.ToInt32(EmpDep.SelectedValue.ToString());
                String DOB = EmpDOB.Value.Date.ToShortDateString().ToString();
                String JDate = EmpJD.Value.Date.ToShortDateString().ToString();
                //birthDate = "CAST('"+birthDate+"', 0)";
                int salary = Convert.ToInt32(EmpSal.Text);
                String Query = "Update EmployeesTB set EmpName = '{0}',EmpGen = '{1}',EmpDep = {2},EmpDOB = '{3}',EmpJDate = '{4}',EmpSal = {5} where EmpId = {6}";
                Query = string.Format(Query, name, gender, department, DOB, JDate, salary,key);
                Con.SetData(Query);
                ShowEmployees();
                Clear();
                MessageBox.Show("Employee Updated!!!");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                String Query = "Delete from EmployeesTB where EmpId = {0}";
                Query = string.Format(Query, key);
                Con.SetData(Query);
                ShowEmployees();
                Clear();
                MessageBox.Show("Employee Deleted!!!");
            }
        }

        private void EmpGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EmpGV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EmpName.Text= EmpGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpGen.SelectedItem = EmpGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpDep.SelectedValue = EmpGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpDOB.Text = EmpGV.SelectedRows[0].Cells[4].Value.ToString();
            EmpJD.Text = EmpGV.SelectedRows[0].Cells[5].Value.ToString();
            EmpSal.Text = EmpGV.SelectedRows[0].Cells[6].Value.ToString();
            if (EmpName.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EmpGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
