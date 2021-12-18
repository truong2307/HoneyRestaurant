using Honey.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Honey.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Samosa",
                Price = 15,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://drive.google.com/uc?export=view&id=1At6q0VQ4wfR2JxjSoZaQ2D2EH7-NVPG0",
                CategoryId = 1
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Paneer Tikka",
                Price = 13.99,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://drive.google.com/uc?export=view&id=16msZeudPNADnIvOWGU5A99-lg2ilZ_2J",
                CategoryId = 1

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Sweet Pie",
                Price = 10.99,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://drive.google.com/uc?export=view&id=1AI7Hmr3Y7YBjVNhhRxO5tN3y9xENff8e",
                CategoryId = 2

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Pav Bhaji",
                Price = 15,
                Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://drive.google.com/uc?export=view&id=15gVOFDEpQn2vAqGMLsEeA8ul-27hHTwL",
                CategoryId = 3
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 1,
                Name = "Appetizer"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 2,
                Name = "Dessert"
            });
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 3,
                Name = "Entree"
            });
        }
    }
}
