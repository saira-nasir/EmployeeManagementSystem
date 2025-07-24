using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class AddEmployee : UserControl
    {
        private readonly string _connString =
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public AddEmployee()
        {
            InitializeComponent();
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            try
            {
                using (var con = new SqlConnection(_connString))
                using (var da = new SqlDataAdapter(
                    "SELECT * FROM employees WHERE delete_date IS NULL", con))
                {
                    var dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading data:\n" + ex.Message,
                    "Load Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void addEmployee_addBtn_Click(object sender, EventArgs e)
        {
            var empId = addEmployee_id.Text.Trim();
            if (empId == "" ||
                addEmployee_fullName.Text.Trim() == "" ||
                addEmployee_gender.Text.Trim() == "" ||
                addEmployee_phoneNum.Text.Trim() == "" ||
                addEmployee_position.Text.Trim() == "" ||
                addEmployee_status.Text.Trim() == "" ||
                addEmployee_picture.Image == null)
            {
                MessageBox.Show(
                    "All fields are required.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Save image
            var folder = Path.Combine(Application.StartupPath, "EmployeeImages");
            Directory.CreateDirectory(folder);
            var fileName = empId + ".jpg";
            var filePath = Path.Combine(folder, fileName);
            addEmployee_picture.Image.Save(filePath);

            try
            {
                using (var con = new SqlConnection(_connString))
                {
                    con.Open();
                    // check duplicate
                    using (var cmdCheck = new SqlCommand(
                        "SELECT COUNT(*) FROM employees WHERE employee_id = @id", con))
                    {
                        cmdCheck.Parameters.AddWithValue("@id", empId);
                        var exists = (int)cmdCheck.ExecuteScalar();
                        if (exists > 0)
                        {
                            MessageBox.Show(
                                "Employee ID already exists.",
                                "Duplicate",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // insert
                    using (var cmd = new SqlCommand(
@"INSERT INTO employees
  (employee_id, full_name, gender, contact_number, position, image, salary, insert_date, status)
 VALUES
  (@id,@name,@gender,@contact,@pos,@img,@sal,@ins,@status)", con))
                    {
                        cmd.Parameters.AddWithValue("@id", empId);
                        cmd.Parameters.AddWithValue("@name", addEmployee_fullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@gender", addEmployee_gender.Text.Trim());
                        cmd.Parameters.AddWithValue("@contact", addEmployee_phoneNum.Text.Trim());
                        cmd.Parameters.AddWithValue("@pos", addEmployee_position.Text.Trim());
                        cmd.Parameters.AddWithValue("@img", fileName);
                        cmd.Parameters.AddWithValue("@sal", 0);
                        cmd.Parameters.AddWithValue("@ins", DateTime.Now);
                        cmd.Parameters.AddWithValue("@status", addEmployee_status.Text.Trim());

                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show(
                            rows > 0 ? "Employee added successfully." : "Add failed.",
                            "Add",
                            MessageBoxButtons.OK,
                            rows > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                    }
                }

                ClearFields();
                LoadEmployeeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Add failed:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void addEmployee_updateBtn_Click(object sender, EventArgs e)
        {
            var empId = addEmployee_id.Text.Trim();
            if (empId == "")
            {
                MessageBox.Show(
                    "Select an employee to update.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Optionally re‑save image
            var folder = Path.Combine(Application.StartupPath, "EmployeeImages");
            Directory.CreateDirectory(folder);
            var fileName = empId + ".jpg";
            try
            {
                if (addEmployee_picture.Image != null)
                    addEmployee_picture.Image.Save(Path.Combine(folder, fileName));
            }
            catch { /* swallow */ }

            try
            {
                using (var con = new SqlConnection(_connString))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(
@"UPDATE employees
  SET full_name=@name, gender=@gender, contact_number=@contact,
      position=@pos, status=@status, image=@img
 WHERE employee_id=@id", con))
                    {
                        cmd.Parameters.AddWithValue("@name", addEmployee_fullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@gender", addEmployee_gender.Text.Trim());
                        cmd.Parameters.AddWithValue("@contact", addEmployee_phoneNum.Text.Trim());
                        cmd.Parameters.AddWithValue("@pos", addEmployee_position.Text.Trim());
                        cmd.Parameters.AddWithValue("@status", addEmployee_status.Text.Trim());
                        cmd.Parameters.AddWithValue("@img", fileName);
                        cmd.Parameters.AddWithValue("@id", empId);

                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show(
                            rows > 0 ? "Employee updated successfully." : "Update failed—ID not found.",
                            "Update",
                            MessageBoxButtons.OK,
                            rows > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                    }
                }

                ClearFields();
                LoadEmployeeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Update failed:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void addEmployee_deleteBtn_Click(object sender, EventArgs e)
        {
            var empId = addEmployee_id.Text.Trim();
            if (empId == "")
            {
                MessageBox.Show(
                    "Select an employee to delete.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                    "Are you sure you want to delete this employee?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            try
            {
                using (var con = new SqlConnection(_connString))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(
                        "UPDATE employees SET delete_date=@dt WHERE employee_id=@id", con))
                    {
                        cmd.Parameters.AddWithValue("@dt", DateTime.Now);
                        cmd.Parameters.AddWithValue("@id", empId);

                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show(
                            rows > 0 ? "Employee deleted successfully." : "Delete failed—ID not found.",
                            "Delete",
                            MessageBoxButtons.OK,
                            rows > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                    }
                }

                ClearFields();
                LoadEmployeeData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Delete failed:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dataGridView1.Rows[e.RowIndex];

            addEmployee_id.Text = row.Cells["employee_id"].Value?.ToString();
            addEmployee_fullName.Text = row.Cells["full_name"].Value?.ToString();
            addEmployee_gender.Text = row.Cells["gender"].Value?.ToString();
            addEmployee_phoneNum.Text = row.Cells["contact_number"].Value?.ToString();
            addEmployee_position.Text = row.Cells["position"].Value?.ToString();
            addEmployee_status.Text = row.Cells["status"].Value?.ToString();

            var imgFile = row.Cells["image"].Value?.ToString();
            var p = Path.Combine(Application.StartupPath, "EmployeeImages", imgFile);
            addEmployee_picture.Image = File.Exists(p) ? Image.FromFile(p) : null;
        }

        private void addEmployee_importBtn_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Filter = "Image files (*.jpg;*.png)|*.jpg;*.png" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    addEmployee_picture.Image = Image.FromFile(dlg.FileName);
            }
        }

        private void addEmployee_clearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            addEmployee_id.Clear();
            addEmployee_fullName.Clear();
            addEmployee_gender.SelectedIndex = -1;
            addEmployee_phoneNum.Clear();
            addEmployee_position.SelectedIndex = -1;
            addEmployee_status.SelectedIndex = -1;
            addEmployee_picture.Image = null;
        }

        public void RefreshData()
        {
            LoadEmployeeData();
        }
    }
}
