using System;
using System.Collections.Generic;
using shotnet.Data;

namespace shopnet.Models{

    public class Invoice {

        public int id { get; set; }
        public string address { get; set; }
        public string cardName { get; set; }
        public string cardNumber { get; set; }
        public DateTime expiration { get; set; }
        public string cvv { get; set; }
        public DateTime dateCreated { get; set; }
        public ApplicationUser user { get; set; }
        public int userId { get; set; }
        public ICollection<Stock> stocks { get; set; }

    }
}