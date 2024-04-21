using RealEst.Core.Constants;

namespace RealEst.Core.Models.EnumModels
{
    public class DefectType
    {
        public int Id { get; set; }
        public DefectTypes Code { get; set; }

        public DefectType()
        {

        }

        public DefectType(DefectTypes defectType)
        {
            Code = defectType;
        }
    }
}
