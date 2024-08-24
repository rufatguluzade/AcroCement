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
    public class AboutUS :BaseEntity
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }


        [NotMapped]
        public IFormFile ImageFile { get; set; }



        
    }
}
