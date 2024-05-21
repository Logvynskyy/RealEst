using RealEst.Core.DTOs;

namespace RealEst.Core.Models
{
    public class Contract : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        public string Iban { get; set; }
        public int TennantId { get; set; }
        public Tennant Tennant { get; set; }
        public decimal Price { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }
        public Organisation Organisation { get; init; }
        public string DisplayString => Name + " #" + Id;

        public Contract()
        {
            
        }

        public Contract(string name, int unitId, string iban, int tennantId, decimal price, DateTime rentFrom, DateTime rentTo)
        {
            Name = name;
            UnitId = unitId;
            Iban = iban;
            TennantId = tennantId;
            Price = price;
            RentFrom = rentFrom;
            RentTo = rentTo;
        }

        public Contract(ContractInputDto contract, Organisation organisation)
            : this(contract.Name, contract.UnitId, contract.Iban, contract.TennantId, contract.Price, contract.RentFrom, contract.RentTo)
        {
            Organisation = organisation;
        }
    }
}
