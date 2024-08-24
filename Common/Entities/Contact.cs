using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class Contact :BaseEntity
    {
        public string MapUrl { get; set; }
        public string Phone { get; set; }
    
        public string Email { get; set; }
        public string Adress { get; set; }
        public string BusinessHours { get; set; }
        public string LogoUrl { get; set; }



        [NotMapped]
        public IFormFile ImageFile { get; set; }



     
    }
}
