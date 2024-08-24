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
    public class Blog :BaseEntity
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime BlogDate { get; set; }

        public List<BlogTag> BlogTags { get; set; }



        [NotMapped]
        public IFormFile ImageFile { get; set; }



        [NotMapped]
        [MaxLength(3)]
        public List<int> TagIds { get; set; }


    }
}
