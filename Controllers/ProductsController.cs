using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shotnet.Data;
using shopnet.Models;

namespace shotnet.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles="admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDBContext _db;
        public ProductsController(ApplicationDBContext db){
            _db = db;
        }

        [HttpGet]
        public async Task<List<Products>> getProducts(){
            return await _db.products.ToListAsync();
        }

        [HttpGet("categories")]
        public async Task<List<Categories>> GetCategories(){
            return await _db.categories.ToListAsync();
        }

        [HttpGet("filterProducts/{query?}")]
        public async Task<List<Products>> filterProducts(string query = null){
            if(string.IsNullOrEmpty(query)) return await _db.products.ToListAsync();
            var filter = await _db.products.Where(x => x.name.Contains(query)).ToListAsync();
            return filter;
        }

        [HttpGet("filterByCategories/{query}")]
        public async Task<List<Products>> filterByCategories(string query = "all"){
            if(query == "all") return await _db.products.ToListAsync();
            return await _db.products.Where(x => x.category == query).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> retrieveProduct(int? id){
            if(id == null)
                return BadRequest();
            var product = await _db.products.SingleOrDefaultAsync(x => x.id == id);
            if(product == null)
                return BadRequest();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> addProducts(Products model){
            if(ModelState.IsValid){
                _db.products.Add(model);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateProducts(int id, Products model){
            if(ModelState.IsValid){
                var product = await _db.products.SingleOrDefaultAsync(x => x.id == id);
                if(product == null)
                    return BadRequest();
                product.name = model.name;
                product.category = model.category;
                product.price = model.price;
                product.imgUrl = model.imgUrl;
                await _db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteProduct(int? id){
            if(id == null)
                return BadRequest();
            var product = await _db.products.SingleOrDefaultAsync(x => x.id == id);
            if(product == null)
                return BadRequest();
            _db.products.Remove(product);
            await _db.SaveChangesAsync();
            return Ok(product);
        }
    }
}