using Business.DTOs.Tag.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Blog.Response
{
    public class BlogResponseDTO
    {

        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime BlogDate { get; set; }

        public List<TagResponseDTO> Tags { get; set; }
    }
}
