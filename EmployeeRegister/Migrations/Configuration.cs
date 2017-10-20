namespace EmployeeRegister.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EmployeeRegister.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeRegister.DataAccessLayer.RegisterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EmployeeRegister.DataAccessLayer.RegisterContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Employees.AddOrUpdate(x => x.Id,
                new Employee() { Id = 1, FirstName = "Adam", LastName = "Smith", Position = "Teacher", Department = "Economics", Salary = 50000 },
                new Employee() { Id = 2, FirstName = "Timm", LastName = "Forks", Position = "Teacher", Department = "Economics", Salary = 50000 },
                new Employee() { Id = 3, FirstName = "John", LastName = "Süsse", Position = "Administrator", Department = "Economics", Salary = 40000 },
                new Employee() { Id = 4, FirstName = "Lisa", LastName = "Green", Position = "Professor", Department = "Physics", Salary = 70000 },
                new Employee() { Id = 5, FirstName = "Ruth", LastName = "Reeds", Position = "Teacher", Department = "Physics", Salary = 50000 }
                );

            for (int i = 6; i < 100; i++)
            {
                context.Employees.AddOrUpdate(x => x.Id,
                    new Employee() { Id = i, FirstName = $"Unit {i}", LastName = "Smith", Position = "Demo", Department = "Demo", Salary = 50000 }
                    );

            }
        }
    }
}
