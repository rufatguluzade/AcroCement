using Business.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> FileUploadAsync(IFormFile file, string path, string type, int size)
        {
            if (!file.CheckFileType(type))
                throw new NotFoundException("This image is not a file");
            if (!file.CheckFileSize(size))
                throw new NotFoundException("Image size is large");

            string fileName = $"{Guid.NewGuid()}-{file.FileName}";
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path, fileName);

            using FileStream stream = new FileStream(uploadPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;

        }
    }
}
