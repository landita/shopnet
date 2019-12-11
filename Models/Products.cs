using System.Collections.Generic;
using shopnet.Models;

namespace shopnet.Models{

    public class Products{
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double price { get; set; }
        public string imgUrl { get; set; }
        public ICollection<ShoppingCart> cart { get; set; }
    }
}