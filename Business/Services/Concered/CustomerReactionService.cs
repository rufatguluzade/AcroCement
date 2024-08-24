using AutoMapper;
using Business.DTOs.Common;
using Business.DTOs.CustomerReaction.Request;
using Business.DTOs.CustomerReaction.Response;
using Business.Exceptions;
using Business.Services.Abstraction;
using Business.Validators.CustomerReaction;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;


namespace Business.Services.Concered
{
    public class CustomerReactionService : ICustomerReactionService
    {
        private readonly ICustomerReactionRepository _customerReactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public CustomerReactionService(ICustomerReactionRepository customerReactionRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
           _customerReactionRepository = customerReactionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        


        }


        public async Task<Response> CreateAsync(CustomerReactionCreateDTO model)
        {
            var result = await new CustomerReactionCreateDTOValidator().ValidateAsync(model);
            if (!result.IsValid)
            {
             

                throw new ValidationException(result.Errors);
            }

            var customerReaction = _mapper.Map<CustomerReaction>(model);


           
            await _customerReactionRepository.CreateAsync(customerReaction);
            await _unitOfWork.CommitAsync();

            return new Response
            {
                Message = "customerReaction ugurla yaradildi"
            };

        }

        public async Task<Response> DeleteAsync(int id)
        {
            var customerReaction = await _customerReactionRepository.GetAsync(id);

            if (customerReaction == null)
            {
                throw new NotFoundException("customerReaction tapilmadi");
            }

            _customerReactionRepository.Delete(customerReaction);

            await _unitOfWork.CommitAsync();

            return new Response
            {
                Message = "data ugurla silindi"
            };

        }

        public async Task<Response<List<CustomerReactionResponseDTO>>> GetAllAsync()
        {
            var response = await _customerReactionRepository.GetAllAsync();

            if (response == null)
            {
                throw new NotFoundException("customerReaction tapilmadi");
            }

            return new Response<List<CustomerReactionResponseDTO>> {
                Data = _mapper.Map<List<CustomerReactionResponseDTO>>(response),
                Message = "Data ugurla getirildi"
                
            };
        }

        public async Task<Response<CustomerReactionResponseDTO>> GetAsync(int id)
        {
            var response = await _customerReactionRepository.GetAsync(id);  

            if(response == null) { throw new NotFoundException("data tapilmadi"); }

            return new Response<CustomerReactionResponseDTO>
            {
                Data = _mapper.Map<CustomerReactionResponseDTO>(response),
                Message = "data ugurla tapildi"
            };

        }

        public async Task<Response> UpdateAsync(int id, CustomerReactionUpdateDTO model)
        {
            var result = await new CustomerReactionUpdateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
          

                throw new ValidationException(result.Errors);
            }

            var existedCustomerReaction = await _customerReactionRepository.GetAsync(id);

            if (existedCustomerReaction == null)
            {
              
                throw new NotFoundException("existedCustomerReaction tapilmadi");
            }

            _mapper.Map(model, existedCustomerReaction);

          

            existedCustomerReaction.ModifiedDate = DateTime.Now;

            _customerReactionRepository.Update(existedCustomerReaction);

            await _unitOfWork.CommitAsync();


          
            return new Response
            {
                Message = "CustomerReaction ugurla moified olundu"
            };
        }
    }
}
