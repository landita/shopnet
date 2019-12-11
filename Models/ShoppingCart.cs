using shotnet.Data;

namespace shopnet.Models
{
    public class ShoppingCart
    {
        public ApplicationUser user { get; set; }
        public int userId { get; set; }

        public Products product { get; set; }
        public int productId { get; set; }

        public int quantity { get; set; }
    }
}