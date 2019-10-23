using Model.Enums.Shared;
using Model.Models;
using Model.ModelsMappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels.Sale
{
    public class SaleVM
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(7, MinimumLength = 7)]
        public string SaleCode { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public Guid EmployeeId { get; set; }
        public List<ProductVm> Products { get; set; }
        public decimal Total { get; set; }
    }

    public class ProductVm
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
