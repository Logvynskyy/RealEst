namespace RealEst.Core.DTOs
{
    public class ContractOutputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Iban { get; set; }
        public string Tennant { get; set; }
        public decimal Price { get; set; }
        public DateTime RentFrom { get; set; }
        public DateTime RentTo { get; set; }
    }
}
