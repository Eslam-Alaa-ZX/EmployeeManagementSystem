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
    public partial class Salary : Form
    {
        Functions Con;
        int key = 0;
        public Salary()
        {
            InitializeComponent();
            Con = new Functions();
            ShowSalary();
            GetEmployees();
        }

        private void ShowSalary()
        {
            String Query = "Select * from SalaryTB";
            SalGV.DataSource = Con.GetData(Query);
        }

        private void GetEmployees()
        {
            string Query = "Select * from EmployeesTB";
            SalEmp.DisplayMember = Con.GetData(Query).Columns["EmpName"].ToString();
            SalEmp.ValueMember = Con.GetData(Query).Columns["EmpId"].ToString();
            SalEmp.DataSource = Con.GetData(Query);
        }

        private void GetEmpSalary()
        {
            string Query = "Select * from EmployeesTB where EmpId = {0}";
            Query = string.Format(Query, SalEmp.SelectedValue.ToString());
            foreach (DataRow dr in Con.GetData(Query).Rows)
            {
                Total.Text = (Convert.ToInt32(dr["EmpSal"].ToString()) * Convert.ToInt32(Attendance.Text)).ToString();
            }
        }

        private void Clear()
        {
            Attendance.Text = "1";
            GetEmpSalary();
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
            if (SalEmp.SelectedIndex == -1 || Total.Text == "" )
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                int name = Convert.ToInt32(SalEmp.SelectedValue.ToString());
                int attendance = Convert.ToInt32(Attendance.Text);
                String date = Period.Value.Date.Month+" - "+ Period.Value.Date.Year;
                int amount = Convert.ToInt32(Total.Text);
                String now = DateTime.Now.Date.ToShortDateString();
                String Query = "insert into SalaryTB values({0},{1},'{2}',{3},'{4}')";
                Query = string.Format(Query, name, attendance, date, amount, now);
                Con.SetData(Query);
                ShowSalary();
                Clear();
                MessageBox.Show("Salary Added!!!");
            }
        }

        private void SalEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Attendance.Text == "")
            {
                Attendance.Text = "0";
                Attendance.SelectAll();
                //MessageBox.Show("Days can not be null");
            }
            else if (Convert.ToInt32(Attendance.Text) > 31)
            {
                MessageBox.Show("Days can not be grater than 31");
                Attendance.SelectAll();
            }
            else
            {
                GetEmpSalary();
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (SalEmp.SelectedIndex == -1 || Total.Text == "")
            {
                MessageBox.Show("Missing Data!!!");
            }
            else
            {
                int name = Convert.ToInt32(SalEmp.SelectedValue.ToString());
                int attendance = Convert.ToInt32(Attendance.Text);
                String date = Period.Value.Date.Month + " - " + Period.Value.Date.Year;
                int amount = Convert.ToInt32(Total.Text);
                String now = DateTime.Now.Date.ToShortDateString();
                String Query = "Update SalaryTB set Employee = {0},Atendance = {1},Period = '{2}',Amount = {3},PayDate = '{4}' where SalId = {5}";
                Query = string.Format(Query, name, attendance, date, amount, now,key);
                Con.SetData(Query);
                ShowSalary();
                Clear();
                MessageBox.Show("Salary Updated!!!");
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
                String Query = "Delete from SalaryTB where SalId = {0}";
                Query = string.Format(Query, key);
                Con.SetData(Query);
                ShowSalary();
                Clear();
                MessageBox.Show("Diagnosis Deleted!!!");
            }
        }

        private void SalGV_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SalEmp.SelectedValue= SalGV.SelectedRows[0].Cells[1].Value.ToString();
            Attendance.Text = SalGV.SelectedRows[0].Cells[2].Value.ToString();
            Period.Text = SalGV.SelectedRows[0].Cells[3].Value.ToString();
            Total.Text = SalGV.SelectedRows[0].Cells[4].Value.ToString();
            if (Total.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(SalGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
    }
}
