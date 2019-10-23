using Model.Enums.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Model { get;set; }
        [Required]
        [StringLength(30)]
        public string Type { get; set; }
        [Required]
        [StringLength(30)]
        public string Brand { get; set; }
        [Required]
        [StringLength(7,MinimumLength = 7)]
        public string ProductCode { get; set; }
        [Required]
        [StringLength(125)]
        public string Avatar { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [ForeignKey("SupplierId")]
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        [Required]
        public State State { get; set; } 
        [Required]
        public DateTime CreateAt { get; set; }
    }
}
