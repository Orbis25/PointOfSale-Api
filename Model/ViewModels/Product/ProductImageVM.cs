using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels.Product
{
    public class ProductImageVM
    {
        public IFormFile File { get; set; }
        public Guid Id { get; set; }
    }
}
