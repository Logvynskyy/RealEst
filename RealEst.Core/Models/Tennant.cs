namespace RealEst.Core.Models
{
    public class Tennant : IPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Debt { get; set; }
        public bool IsDebtor => Debt > 0;
        public string DisplayString => Name + ' ' + LastName;

        public Tennant()
        {
            
        }

        public Tennant(string name, string lastName, string email, decimal debt = 0)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Debt = debt;
        }
    }
}
