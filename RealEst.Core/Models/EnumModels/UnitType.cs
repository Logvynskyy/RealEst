using RealEst.Core.Constants;

namespace RealEst.Core.Models.EnumModels
{
    public class UnitType
    {
        public int Id { get; set; }
        public UnitTypes Code { get; set; }

        public UnitType()
        {

        }

        public UnitType(UnitTypes unitType)
        {
            Code = unitType;
        }
    }
}
