using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EmployeeRegister.DataAccessLayer
{

    public class RegisterContext : DbContext
    {
        public DbSet<Models.Employee> Employees { get; set; }

        public RegisterContext() : base("DefaultConnection")
        {
        }
    }
}