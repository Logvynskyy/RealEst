namespace RealEst.Core.Models
{
    public interface IBaseModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string DisplayString {  get; }
    }
}
