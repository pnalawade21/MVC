using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCLocalization.Models
{
    public class EmployeeModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName ="EmpName")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "RegExEmpName")]
        public string Name { get; set; }
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "EmpEmail")]
        [RegularExpression(@"[\w-]+@([\w -]+\.)+[\w-]+", ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "RegExEmpMail")]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "Technology")]
        public string Technology { get; set; }
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "Experience")]
        public decimal Experience { get; set; }
        [Required(ErrorMessageResourceType = typeof(ErrorMsg), ErrorMessageResourceName = "ContactNo")]
        public string ContactNo { get; set; }
    }
    
}