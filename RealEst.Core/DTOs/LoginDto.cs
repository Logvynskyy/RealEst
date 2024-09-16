namespace RealEst.Core.DTOs
{
    public class LoginDto
    {
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public int OrganisationId { get; set; }
    }
}
