namespace RealEst.Core.Models
{
    public interface IPerson : IBaseModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
    }
}
