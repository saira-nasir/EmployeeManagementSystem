using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class Dashboard : UserControl
    {
        private SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            RefreshData(); // Ensure data loads when the control is loaded
        }

        public void RefreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)RefreshData);
                return;
            }

            displayTE();
            displayAE();
            displayIE();
        }

        public void displayTE()
        {
            try
            {
                connect.Open();
                string query = "SELECT COUNT(id) FROM employees WHERE delete_date IS NULL";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                        dashboard_TE.Text = reader[0].ToString();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading total employees: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public void displayAE()
        {
            try
            {
                connect.Open();
                string query = "SELECT COUNT(id) FROM employees WHERE status = @status AND delete_date IS NULL";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@status", "Active");
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                        dashboard_AE.Text = reader[0].ToString();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading active employees: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public void displayIE()
        {
            try
            {
                connect.Open();
                string query = "SELECT COUNT(id) FROM employees WHERE status = @status AND delete_date IS NULL";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@status", "Inactive"); // ✅ Corrected typo from "Ianctive"
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                        dashboard_IE.Text = reader[0].ToString();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inactive employees: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
