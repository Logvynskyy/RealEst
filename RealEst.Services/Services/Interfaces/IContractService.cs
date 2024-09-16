using RealEst.Core.DTOs;
using RealEst.Core.Models;

namespace RealEst.Services.Services.Interfaces
{
    public interface IContractService
    {
        ContractOutputDto GetById(int id);
        List<ContractOutputDto> GetAll();
        bool Add(ContractInputDto contract);
        bool Update(int id, ContractEditDto contract);
        bool DeleteById(int id);
        List<IncomeDto> GetIncome();
        Contract DtoToEntity(ContractInputDto contractDto);
        ContractOutputDto EntityToDto(Contract contract);
    }
}
