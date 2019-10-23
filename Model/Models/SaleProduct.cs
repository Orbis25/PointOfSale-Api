using Model.Enums.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class SaleProduct
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid SaleId { get; set; }

        #region Relations
        public Product Product { get; set; }
        public Sale Sale { get; set; }
        #endregion

        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        [Required]
        public int Quantity { get; set; }

        public State State { get; set; }
    }
}
