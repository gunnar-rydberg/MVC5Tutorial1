using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmployeeRegister.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [StringLength(32, ErrorMessage = "Maximum length is 32")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [StringLength(32, ErrorMessage = "Maximum length is 32")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public int Salary { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
    }
}