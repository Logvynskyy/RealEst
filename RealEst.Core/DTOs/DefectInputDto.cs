using RealEst.Core.Constants;

namespace RealEst.Core.DTOs
{
    public class DefectInputDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DefectTypes DefectType { get; set; }
    }
}
