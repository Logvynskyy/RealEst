using RealEst.Core.DTOs;
using RealEst.Core.Models;

namespace RealEst.Services.Services.Interfaces
{
    public interface ITennantService
    {
        TennantDto GetById(int id);
        List<TennantDto> GetAll();
        bool Add(TennantDto tennant);
        bool Update(int id, TennantDto tennant);
        bool DeleteById(int id);
        Tennant DtoToEntity(TennantDto tennantDto);
        TennantDto EntityToDto(Tennant entity);
    }
}
