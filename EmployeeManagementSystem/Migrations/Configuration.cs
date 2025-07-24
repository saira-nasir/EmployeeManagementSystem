using System.Data.Entity.Migrations;

namespace EmployeeManagementSystem.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeManagementSystem.EmployeeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EmployeeManagementSystem.EmployeeDbContext context)
        {
            // Seed data if needed
        }
    }
}