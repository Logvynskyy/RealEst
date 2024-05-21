using RealEst.Core.DTOs;
using RealEst.Core.Models;

namespace RealEst.Services.Services.Interfaces
{
    public interface IContactService
    {
        ContactDto GetById(int id);
        List<ContactDto> GetAll();
        bool Add(ContactDto contact);
        bool Update(int id, ContactDto contact);
        bool DeleteById(int id);
        Contact DtoToEntity(ContactDto contactDto);
        ContactDto EntityToDto(Contact entity);
    }
}
