using RealEst.Core.Constants;

namespace RealEst.Core.Models
{
    public class Defect : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DefectType DefectType { get; set; }

        public Defect(int id, string name, string description, DefectType defectType)
        {
            Id = id;
            Name = name;
            Description = description;
            DefectType = defectType;
        }

    }
}
