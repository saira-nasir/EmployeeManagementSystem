using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagementSystem
{
    class SalaryData
    {
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }

        private SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public List<SalaryData> salaryEmployeeListData()
        {
            List<SalaryData> listdata = new List<SalaryData>();

            try
            {
                connect.Open();

                string selectData = "SELECT employee_id, full_name, gender, contact_number, position, salary FROM employees WHERE status = 'Active' AND delete_date IS NULL";

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalaryData sd = new SalaryData
                        {
                            EmployeeID = reader["employee_id"].ToString(),
                            Name = reader["full_name"].ToString(),
                            Gender = reader["gender"].ToString(),
                            Contact = reader["contact_number"].ToString(),
                            Position = reader["position"].ToString(),
                            Salary = Convert.ToInt32(reader["salary"])
                        };

                        listdata.Add(sd);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connect.Close();
            }

            return listdata;
        }
    }
}
