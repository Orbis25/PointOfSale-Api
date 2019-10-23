using Model.Enums.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [StringLength(14,MinimumLength = 10)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Avatar { get; set; }
        [Required]
        [StringLength(7,MinimumLength = 7)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(13,MinimumLength = 11)]
        public string Dni { get; set; }
        public State State { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
