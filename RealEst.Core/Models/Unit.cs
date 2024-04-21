using RealEst.Core.Constants;

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

        public Unit()
        {
            
        }

        public Unit(string name, string address, UnitTypes unitType, double footage, IList<Defect> defects = null)
        {
            Name = name;
            Address = address;
            UnitType = unitType;
            Footage = footage;
            Defects = defects;
        }
    }
}
