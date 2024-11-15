namespace CommerceAPI_IOON.CommerceAPI.Models
{
    public class SaleDetail
    {
        public Guid DetailId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Guid SaleId { get; set; }
        public Sale Sale { get; set; }
    }


}
