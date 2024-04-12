namespace RealEst.Core.Models
{
    public class Contract : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitId { get; set; }
        public string Iban { get; set; }
        public int TennantId { get; set; }
        public decimal Price { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }

        public Contract(int id, string name, int unitId, string iban, int tennantId, decimal price, DateTime rentFrom, DateTime rentTo)
        {
            Id = id;
            Name = name;
            UnitId = unitId;
            Iban = iban;
            TennantId = tennantId;
            Price = price;
            RentFrom = rentFrom;
            RentTo = rentTo;
        }
    }
}
