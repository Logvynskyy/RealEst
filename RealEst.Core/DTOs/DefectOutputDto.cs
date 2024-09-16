using RealEst.Core.Constants;

namespace RealEst.Core.DTOs
{
    public class DefectOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DefectTypes DefectType { get; set; }
        public string DisplayString => Name;
    }
}
