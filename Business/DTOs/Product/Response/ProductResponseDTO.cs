using Business.DTOs.Category.Response;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Product.Response
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string ComplianceStandard { get; set; }
        public List<string> AreasOfApplication { get; set; } = new List<string>();
        public List<string> Advantages { get; set; } = new List<string>();
        public string Weight { get; set; }
        public string Manufacture { get; set; }

        public CategoryResponseDTO Category { get; set; }

    


        public IEnumerable<ProductImage> ProductImages { get; set; }
    }
}
