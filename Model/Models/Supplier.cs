using Model.Enums.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        [Required]
        public State State { get; set; }
        [Required]
        [StringLength(50 , MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(14 , MinimumLength = 10)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(7 , MinimumLength = 7)]
        public string SupplierCode { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
