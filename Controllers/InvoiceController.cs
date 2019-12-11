using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shopnet.Models;
using shotnet.Data;

namespace shopnet.Controllers{

    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController:Controller{

        private readonly ApplicationDBContext _db;
        public InvoiceController(ApplicationDBContext db){
            _db = db;
        }

        [HttpGet]
        [Authorize(Roles="admin")]
        public async Task<dynamic> GetInvoices(){
            return await _db.invoices.Include(x => x.user)
                .Select(x => new {
                    id = x.id,
                    username = x.user.Email,
                    dateCreated = x.dateCreated.ToShortDateString(),
                }).ToListAsync();
        }

        [HttpGet("invoice-details/{id}")]
        public async Task<dynamic> GetInvoiceDetails(int id){
            var result = await _db.invoices
                .Include(x => x.user)
                .Include(x => x.stocks)
                .Select(x => new {
                    id = x.id,
                    email = x.user.Email,
                    address = x.address,
                    cardName = x.cardName,
                    cardNumber = x.cardNumber,
                    expirarion = x.expiration.ToShortDateString(),
                    cvv = x.cvv,
                    stocks = x.stocks,
                })
                .SingleOrDefaultAsync(x => x.id == id);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<dynamic> getProductsInCart(int id){
            var result = await _db.shoppingCart.Include(x => x.product)
                .Where(x => x.userId == id)
                .Select(x => new{
                    name = x.product.name,
                    quantity = x.quantity,
                    totalPrice = x.quantity * x.product.price
                }).ToListAsync();
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> addInvoice(Invoice model){
            if(ModelState.IsValid){
                model.dateCreated = DateTime.Now;
                _db.invoices.Add(model);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return Unauthorized();
        }

        [HttpPut("addStocks/{id}")]
        public async Task<IActionResult> addStocks(int id, List<Stock> stocks){
            foreach(var stock in stocks){
                stock.invoiceId = id;
                _db.stock.Add(stock);
            }
            await _db.SaveChangesAsync();
            return Ok();
        } 

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteAllFromCart(int id){
            var result = _db.shoppingCart.Where(x => x.userId == id);
            _db.shoppingCart.RemoveRange(result);
            await _db.SaveChangesAsync();
            return Ok();
        }

    }
}