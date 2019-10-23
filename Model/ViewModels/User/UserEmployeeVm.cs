using Model.Enums.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels.User
{
    public class UserEmployeeVm : UserVm
    {
        public string Address { get; set; }
        [Required]
        [StringLength(14, MinimumLength = 10)]
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        [Required]
        [StringLength(7, MinimumLength = 7)]
        public string EmployeeCode { get; set; }
        [Required]
        public string Dni { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public Guid Id { get; set; }
    }
}
