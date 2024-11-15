namespace CommerceAPI_IOON.CommerceAPI.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public Guid? CommerceId { get; set; }
        public Commerce Commerce { get; set; }

        public Guid StateId { get; set; }
        public State State { get; set; }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }

}
