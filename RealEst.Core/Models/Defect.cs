using RealEst.Core.Constants;

namespace RealEst.Core.Models
{
    public class Defect : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DefectTypes DefectType { get; set; }
        public string DisplayString => Name;

        public Defect()
        {
            
        }

        public Defect(int id, string name, string description, DefectTypes defectType)
        {
            Id = id;
            Name = name;
            Description = description;
            DefectType = defectType;
        }

    }
}
