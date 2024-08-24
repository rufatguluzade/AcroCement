using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Product :BaseEntity
    {
       
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string ComplianceStandard { get; set; }
        public List<string>AreasOfApplication { get; set; } = new List<string>();
        public List<string> Advantages { get; set; } = new List<string>();
        public string Weight { get; set; }
        public string Manufacture { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }


        public IEnumerable<ProductImage> ProductImages { get; set; }


        [NotMapped]
        public IEnumerable<IFormFile> ProductImageFiles { get; set; }




    }
}
