namespace RealEst.Core.DTOs
{
    public class ContractEditDto
    {
        public string Name { get; set; }
        public string Iban { get; set; }
        public decimal Price { get; set; }
        public DateTime RentTo { get; set; }
    }
}
