using Model.Enums.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Sale
    {
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(7,MinimumLength = 7)]
        public string SaleCode { get; set; }
        [Required]
        public State State { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal Total { get; set; }
    }
}
