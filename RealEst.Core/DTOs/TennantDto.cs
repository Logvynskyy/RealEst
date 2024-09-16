namespace RealEst.Core.DTOs
{
    public class TennantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Debt { get; set; }
        public bool IsDebtor => Debt > 0;
        public string DisplayString => Name + ' ' + LastName;
    }
}
