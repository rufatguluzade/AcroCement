using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Contact.Response
{
    public class ContactResponseDTO
    {

        public int Id { get; set; }
        public string MapUrl { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        public string Adress { get; set; }
        public string BusinessHours { get; set; }
        public string LogoUrl { get; set; }



 
    }
}
