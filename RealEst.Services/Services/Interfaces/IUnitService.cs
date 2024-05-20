using RealEst.Core.DTOs;

namespace RealEst.Services.Services.Interfaces
{
    public interface IUnitService
    {
        UnitDto GetById(int id);
        List<UnitDto> GetAll();
        bool Add(UnitDto unitDto);
        bool Update(int id, UnitDto unitDto);
        bool DeleteById(int id);
    }
}
