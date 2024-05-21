using RealEst.Core.DTOs;
using RealEst.Core.Models;

namespace RealEst.Services.Services.Interfaces
{
    public interface IUnitService
    {
        UnitDto GetById(int id);
        List<UnitDto> GetAll();
        bool Add(UnitDto unitDto);
        bool Update(int id, UnitDto unitDto);
        bool DeleteById(int id);
        Unit DtoToEntity(UnitDto unitDto);
        UnitDto EntityToDto(Unit unit);
    }
}
