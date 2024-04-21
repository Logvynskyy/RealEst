namespace RealEst.Core.Models
{
    public class Contract : IBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Unit Unit { get; set; }
        public string Iban { get; set; }
        public Tennant Tennant { get; set; }
        public decimal Price { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }

        public Contract()
        {
            
        }

        public Contract(string name, Unit unit, string iban, Tennant tennant, decimal price, DateTime rentFrom, DateTime rentTo)
        {
            Name = name;
            Unit = unit;
            Iban = iban;
            Tennant = tennant;
            Price = price;
            RentFrom = rentFrom;
            RentTo = rentTo;
        }
    }
}
