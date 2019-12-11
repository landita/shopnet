using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using shopnet.Models;
using shotnet.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace shopnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingCartController(
            ApplicationDBContext db,
            UserManager<ApplicationUser> userManager){
                _db = db;
                _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<List<ShoppingCart>> getCart(int id){
            var result = _db.shoppingCart.Where(x => x.userId == id);
            return await result.ToListAsync();
        }
        
        [HttpPost]
        public async Task<IActionResult> addToCart(ShoppingCart cart){
            if(ModelState.IsValid){
                var obj = await _db.shoppingCart.SingleOrDefaultAsync(x => 
                    x.productId == cart.productId && x.userId == cart.userId);
                if(obj == null)
                    _db.shoppingCart.Add(new ShoppingCart{
                        userId = cart.userId,
                        productId = cart.productId,
                        quantity = 1
                    });
                else
                    obj.quantity += 1;
                await _db.SaveChangesAsync();
                return Ok();
            }
            return Unauthorized();
        }

        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> removeFromCart(int userId, int productId){
            var result = await _db.shoppingCart
                .SingleOrDefaultAsync(x => x.userId == userId && x.productId == productId);
            result.quantity -= 1;
            if(result.quantity == 0) _db.shoppingCart.Remove(result);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}