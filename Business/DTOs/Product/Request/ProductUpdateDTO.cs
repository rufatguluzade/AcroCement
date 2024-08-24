using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Product.Request
{
    public class ProductUpdateDTO
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string ComplianceStandard { get; set; }
        public List<string> AreasOfApplication { get; set; }
        public List<string> Advantages { get; set; }
        public string Weight { get; set; }
        public string Manufacture { get; set; }


        public int CategoryId { get; set; }



        [NotMapped]
        public IEnumerable<IFormFile> ProductImageFiles { get; set; }
    }
}
