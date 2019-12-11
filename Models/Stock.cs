namespace shopnet.Models
{
    public class Stock
    {
        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double totalPrice { get; set; }
        public Invoice invoice { get; set; }
        public int invoiceId { get; set; }
    }
}