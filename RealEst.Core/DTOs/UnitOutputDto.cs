using RealEst.Core.Constants;

namespace RealEst.Core.DTOs
{
    public class UnitOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public UnitTypes UnitType { get; set; }
        public double Footage { get; set; }
        public IList<DefectOutputDto>? Defects { get; set; }
        public string DisplayString => Name + ' ' + Address;
    }
}
