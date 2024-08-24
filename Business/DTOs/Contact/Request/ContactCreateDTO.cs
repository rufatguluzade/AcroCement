using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Contact.Request
{
    public class ContactCreateDTO
    {
        public string MapUrl { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
        public string Adress { get; set; }
        public string BusinessHours { get; set; }
        



        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
