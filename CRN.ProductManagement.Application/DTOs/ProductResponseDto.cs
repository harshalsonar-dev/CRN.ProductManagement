using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRN.ProductManagement.Application.DTOs
{
    public class ProductResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public ProductDto? Data { get; set; }
    }
}
