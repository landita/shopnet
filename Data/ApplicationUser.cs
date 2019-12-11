using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using shopnet.Models;

namespace shotnet.Data{

    public class ApplicationUser:IdentityUser<int>{
        public ICollection<ShoppingCart> cart { get; set; }
    }
}