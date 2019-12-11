using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using shopnet.Models;

namespace shotnet.Data{

    public class ApplicationDBContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>{

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            :base(options){}

        public DbSet<Products> products { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<ShoppingCart> shoppingCart { get; set; }
        public DbSet<Stock> stock { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>{Id=1, Name="admin", NormalizedName="admin"},
                new IdentityRole<int>{Id=2, Name="staff", NormalizedName="staff"}
            );
            builder.Entity<Categories>().HasData(
                new Categories {id = 1, name = "fruits"},
                new Categories {id = 2, name = "meat"},
                new Categories {id = 3, name = "vegetables"},
                new Categories {id = 4, name = "snacks"},
                new Categories {id = 5, name = "drinks"},
                new Categories {id = 6, name = "breads"}
            );
            builder.Entity<ShoppingCart>()
                .HasKey(x => new {x.userId, x.productId});
            builder.Entity<ShoppingCart>()
                .HasOne(x => x.user)
                .WithMany(x => x.cart)
                .HasForeignKey(x => x.userId);
            builder.Entity<ShoppingCart>()
                .HasOne(x => x.product)
                .WithMany(x => x.cart)
                .HasForeignKey(x => x.productId);
                

        }   
    }

}