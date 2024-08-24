using Business.DTOs.Common;
using Business.DTOs.Contact.Request;
using Business.DTOs.Contact.Response;
using Business.DTOs.CustomerReaction.Request;
using Business.DTOs.CustomerReaction.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstraction
{
    public interface ICustomerReactionService
    {
        Task<Response> CreateAsync(CustomerReactionCreateDTO model);
        Task<Response> UpdateAsync(int id, CustomerReactionUpdateDTO model);
        Task<Response> DeleteAsync(int id);
        Task<Response<CustomerReactionResponseDTO>> GetAsync(int id);
        Task<Response<List<CustomerReactionResponseDTO>>> GetAllAsync();
    }
}
