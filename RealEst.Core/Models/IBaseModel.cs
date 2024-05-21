namespace RealEst.Core.Models
{
    public interface IBaseModel
    {
        int Id { get; set; }
        string Name { get; set; }
        Organisation Organisation { get; init; }
        string DisplayString {  get; }
    }
}
