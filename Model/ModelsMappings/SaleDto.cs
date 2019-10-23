using Model.Enums.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ModelsMappings
{
    public class SaleDto
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(7, MinimumLength = 7)]
        public string SaleCode { get; set; }
        [Required]
        public State State { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }

        public string FullNameClient { get; set; }
        public string FullNameEmployee { get; set; }
        public decimal Total { get; set; }


    }
}
