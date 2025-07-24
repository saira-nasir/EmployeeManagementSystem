using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class Salary : UserControl
    {
        private SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public Salary()
        {
            InitializeComponent();
            displayEmployees();
        }

        public void RefreshData()
        {
            displayEmployees();
            clearFields();
        }

        public void displayEmployees()
        {
            try
            {
                SalaryData ed = new SalaryData();
                List<SalaryData> listData = ed.salaryEmployeeListData();

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = listData;
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying employees: " + ex.Message);
            }
        }

        private void salary_updateBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(salary_employeeID.Text) ||
                string.IsNullOrWhiteSpace(salary_name.Text) ||
                string.IsNullOrWhiteSpace(salary_position.Text) ||
                string.IsNullOrWhiteSpace(salary_salary.Text))
            {
                MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult check = MessageBox.Show($"Update salary for Employee ID: {salary_employeeID.Text.Trim()}?",
                "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                try
                {
                    connect.Open();
                    string query = "UPDATE employees SET salary = @salary, update_date = @updateDate WHERE employee_id = @employeeID";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@salary", salary_salary.Text.Trim());
                        cmd.Parameters.AddWithValue("@updateDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@employeeID", salary_employeeID.Text.Trim());

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Salary updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            displayEmployees();
                            clearFields();
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Check Employee ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while updating salary:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                salary_employeeID.Text = row.Cells["EmployeeID"].Value?.ToString();
                salary_name.Text = row.Cells["Name"].Value?.ToString();
                salary_position.Text = row.Cells["Position"].Value?.ToString();
                salary_salary.Text = row.Cells["Salary"].Value?.ToString();

                salary_employeeID.Enabled = false;
                salary_name.Enabled = false;
                salary_position.Enabled = false;
            }
        }

        public void clearFields()
        {
            salary_employeeID.Text = "";
            salary_name.Text = "";
            salary_position.Text = "";
            salary_salary.Text = "";

            salary_employeeID.Enabled = true;
            salary_name.Enabled = true;
            salary_position.Enabled = true;
        }

        private void salary_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }
    }
}
