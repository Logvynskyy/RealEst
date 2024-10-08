﻿using Microsoft.EntityFrameworkCore;
using RealEst.Core.Models;
using RealEst.DataAccess.Interfaces;

namespace RealEst.DataAccess.Implementations
{
    public class ContractRepository : IContractRepository
    {
        private readonly ApplicationContext _applicationContext;

        public ContractRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Add(Contract contract)
        {
            _applicationContext.Contracts.Add(contract);
            _applicationContext.SaveChanges();
        }

        public bool DeleteById(int id)
        {
            var contractToDelete = _applicationContext.Contracts
                .Remove(_applicationContext.Contracts.FirstOrDefault(c => c.Id == id)!);
            var entityState = contractToDelete.State;

            _applicationContext.SaveChanges();

            return entityState == EntityState.Deleted;
        }

        public List<Contract> GetAll()
        {
            return _applicationContext.Contracts
                    .Include(c => c.Unit)
                    .Include(c => c.Tennant)
                    .Include(c => c.Organisation)
                .ToList();
        }

        public Contract GetById(int id)
        {
            return _applicationContext.Contracts
                    .Include(c => c.Unit)
                    .Include(c => c.Tennant)
                    .Include(c => c.Organisation)
                .FirstOrDefault(c => c.Id == id)!;
        }

        public void Update(int id, Contract contract)
        {
            var contractToUpdate = _applicationContext.Contracts
                    .Include(c => c.Unit)
                    .Include(c => c.Tennant)
                    .Include(c => c.Organisation)
                .FirstOrDefault(c => c.Id == id);

            if(contractToUpdate != null)
            {
                contractToUpdate.Price = contract.Price;
                contractToUpdate.Iban = contract.Iban;
                contractToUpdate.Name = contract.Name;
                contractToUpdate.RentFrom = contract.RentFrom;
                contractToUpdate.RentTo = contract.RentTo;

                //_applicationContext.Contracts.Update(contractToUpdate);
                _applicationContext.SaveChanges();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Entity with given ID wasn't found");
            }
        }
    }
}
