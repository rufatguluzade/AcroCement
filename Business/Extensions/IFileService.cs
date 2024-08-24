using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions
{
    public interface IFileService
    {
        Task<string> FileUploadAsync(IFormFile file, string path, string type, int size);
        
    }
}
