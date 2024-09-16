using RealEst.Core.Constants;
using RealEst.Core.Models;

namespace RealEst.Core.DTOs
{
    public class UnitDefectsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public UnitTypes UnitType { get; set; }
        public double Footage { get; set; }
        public IList<Defect>? Defects { get; set; }
    }
}
