namespace CommerceAPI_IOON.CommerceAPI.Models
{
    public class Sale
    {
        public Guid SaleId { get; set; }
        public DateTime SaleDate { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid CommerceId { get; set; }
        public Commerce Commerce { get; set; }

        public Guid StateId { get; set; }
        public State State { get; set; }

        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }


}
