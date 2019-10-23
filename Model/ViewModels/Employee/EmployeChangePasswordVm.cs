using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels.Employee
{
    public class EmployeChangePasswordVm
    {
      [Required(ErrorMessage = "el {0} es requerido")]
      public Guid Id { get; set; }
      [Required(ErrorMessage = "el {0} es requerido")]
       public string Password { get; set; }
    }
}
