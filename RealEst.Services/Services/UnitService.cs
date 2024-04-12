using RealEst.Core.Models;
using RealEst.DataAccess;

namespace RealEst.Services.Service
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public bool Add(Unit unit)
        {
            try
            {
                //if (!_unitValidator.Validate(unit).FirstOrDefault())
                //    throw new InvalidOperationException("You passed invalid unit!");

                _unitRepository.Add(unit);
                return true;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool DeleteById(int id)
        {
            try
            {
                return _unitRepository.DeleteById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<Unit> GetAll()
        {
            try
            {
                return _unitRepository.GetAll();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public Unit GetById(int id)
        {
            try
            {
                return _unitRepository.GetById(id);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public bool Update(int id, Unit unit)
        {
            try
            {
                // TODO: Add validation
                unit.Id = id;

                _unitRepository.Update(id, unit);
                return true;
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
