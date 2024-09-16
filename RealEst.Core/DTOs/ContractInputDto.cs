namespace RealEst.Core.DTOs
{
    public class ContractInputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitId { get; set; }
        public string Iban { get; set; }
        public int TennantId { get; set; }
        public decimal Price { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }
    }
}
