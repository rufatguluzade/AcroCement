using AutoMapper;

using Business.DTOs.Common;
using Business.DTOs.Contact.Request;
using Business.DTOs.Contact.Response;
using Business.Exceptions;
using Business.Extensions;
using Business.Helpers;
using Business.Services.Abstraction;

using Business.Validators.Contact;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Hosting;


namespace Business.Services.Concered
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IWebHostEnvironment _env;

        public ContactService(IContactRepository contactRepository, IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _contactRepository = contactRepository ;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _env = env;


        }

        public async Task<Response> CreateAsync(ContactCreateDTO model)
        {
            var result = await new ContactCreateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var contact = _mapper.Map<Contact>(model);


            if (contact.ImageFile == null)
            {

                throw new ValidationException("Image daxil edilmelidir");
            }



            if (!contact.ImageFile.CheckFileSize(1000))
            {

                throw new ValidationException("Image olcusu max 1 mb olamlidir");

            }
            if (!contact.ImageFile.CheckFileType("image/jpeg"))
            {


                throw new ValidationException("Image jpg tipi olmalidir");
            }


            contact.LogoUrl = contact.ImageFile.CreateImage(_env, "img", "contact");



            await _contactRepository.CreateAsync(contact);
            await _unitOfWork.CommitAsync();


            return new Response
            {
                Message = "Contact ugurla yaradildi"
            };

        }

        public async Task<Response> DeleteAsync(int id)
        {
            var contact = await _contactRepository.GetAsync(id);

            if (contact == null)
            {

                throw new ValidationException("contact tapilmadi");
            }

            _contactRepository.Delete(contact);
            await _unitOfWork.CommitAsync();



            return new Response
            {
                Message = "contact ugurla silindi"
            };
        }

        public async Task<Response<List<ContactResponseDTO>>> GetAllAsync()
        {
            var response = await _contactRepository.GetAllAsync();

            if (response == null)
            {

                throw new NotFoundException("contact tapilmadi");
            }



            return new Response<List<ContactResponseDTO>>
            {
                Data = _mapper.Map<List<ContactResponseDTO>>(response),
                Message = "contact ugurla getirildi"
            };
        }

        public async Task<Response<ContactResponseDTO>> GetAsync(int id)
        {
            var contacts = await _contactRepository.GetAsync(id);
            if (contacts == null)
            {
                throw new NotFoundException("contactlar tapilmadi");
            }


            return new Response<ContactResponseDTO>
            {
                Data = _mapper.Map<ContactResponseDTO>(contacts),
                Message = "data ugurla getirildi"
            };
        }

        public async Task<Response> UpdateAsync(int id, ContactUpdateDTO model)
        {
            var result = await new ContactUpdateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }



            var existedContact = await _contactRepository.GetAsync(id);

            if (existedContact == null)
            {

                throw new NotFoundException("contact tapilmadi");
            }
            _mapper.Map(model, existedContact);


            if (existedContact.ImageFile == null)
            {

                throw new ValidationException("Image daxil edilmelidir");
            }






            if (!existedContact.ImageFile.CheckFileSize(1000))
            {
                throw new ValidationException("Image olcusu max 1 mb olamlidir");

            }
            if (!existedContact.ImageFile.CheckFileType("image/jpeg"))
            {


                throw new ValidationException("Image jpg tipi olmalidir");

            }

            Helper.DeleteFile(_env, existedContact.LogoUrl, "img", "contact");
            existedContact.LogoUrl = model.ImageFile.CreateImage(_env, "img", "contact");



            existedContact.ModifiedDate = DateTime.Now;


            _contactRepository.Update(existedContact);

            await _unitOfWork.CommitAsync();

            return new Response
            {
                Message = "Contact ugurla modified olundu"
            };
        }
    }
}
