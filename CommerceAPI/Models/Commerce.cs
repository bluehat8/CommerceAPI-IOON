namespace CommerceAPI_IOON.CommerceAPI.Models
{
    public class Commerce
    {
        public Guid CommerceId { get; set; }
        public string CommerceName { get; set; }
        public string Address { get; set; }
        public string RUC { get; set; }

        public Guid StateId { get; set; }
        public State State { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }


}
