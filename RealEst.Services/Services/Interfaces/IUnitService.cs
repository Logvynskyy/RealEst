using RealEst.Core.DTOs;
using RealEst.Core.Models;

namespace RealEst.Services.Services.Interfaces
{
    public interface IUnitService
    {
        UnitOutputDto GetById(int id);
        List<UnitOutputDto> GetAll();
        bool Add(UnitDto unitDto);
        bool Update(int id, UnitDto unitDto);
        bool DeleteById(int id);
        Unit DtoToEntity(UnitDto unitDto);
        UnitOutputDto EntityToDto(Unit unit);
    }
}
