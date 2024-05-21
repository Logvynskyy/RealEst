namespace RealEst.Core.Models
{
    public class Organisation : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayString => Name;
    }
}
