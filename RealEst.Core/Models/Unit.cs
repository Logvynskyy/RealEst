using RealEst.Core.Constants;
using RealEst.Core.DTOs;

namespace RealEst.Core.Models
{
    public class Unit : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public UnitTypes UnitType { get; set; }
        public double Footage { get; set; }
        public IList<Defect>? Defects { get; set; }
        public Organisation Organisation { get; set; }
        public string DisplayString => Name + ' ' + Address;

        public Unit()
        {
            
        }

        public Unit(string name, string address, UnitTypes unitType, double footage,
            IList<Defect> defects = null)
        {
            Name = name;
            Address = address;
            UnitType = unitType;
            Footage = footage;
            Defects = defects;
        }

        public Unit(UnitDto unitDto, Organisation organisation)
            : this(unitDto.Name, unitDto.Address, unitDto.UnitType, unitDto.Footage, unitDto.Defects)
        {
            Organisation = organisation;
        }
    }
}
