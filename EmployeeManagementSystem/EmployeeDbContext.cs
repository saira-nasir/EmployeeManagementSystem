using System.Data.Entity;
using System.Configuration; // Add this namespace to resolve ConfigurationManager

namespace EmployeeManagementSystem
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() : base("name=DefaultConnection")
        {
            // The base constructor already uses the connection string from App.config.
            // No need to manually set the connection string here.
        }

        public DbSet<Employee> Employees { get; set; }
    }
}