using Business.DTOs.Common;
using Business.DTOs.Contact.Request;
using Business.DTOs.Contact.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstraction
{
    public interface IContactService
    {
        Task<Response> CreateAsync(ContactCreateDTO model);
        Task<Response> UpdateAsync(int id,ContactUpdateDTO model);
        Task<Response> DeleteAsync(int id);
        Task<Response<ContactResponseDTO>> GetAsync(int id);
        Task<Response<List<ContactResponseDTO>>> GetAllAsync();
    }
}
