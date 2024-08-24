using Business.DTOs.Product.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Category.Response
{
    public class CategoryWithProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductResponseDTO> Products { get; set; }
    }
}
