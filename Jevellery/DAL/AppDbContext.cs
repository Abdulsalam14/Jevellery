
using Jevellery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jevellery.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Earrings", Filename = "category1" },
                new Category() { Id = 2, Name = "Necklages", Filename = "category2" },
                new Category() { Id = 3, Name = "Rings", Filename = "category3" }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 345.00M,
                    CategoryId = 3,
                    Discount = 0,
                    Filename = "product-01"
                },
                new Product()
                {
                    Id = 2,
                    Name = "Minola Golden Necklace",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 2,
                    Discount = 0,
                    Filename = "product-02"
                },
                new Product()
                {
                    Id = 3,
                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 3,
                    Discount = 26,
                    Filename = "product-03"
                },
                new Product()
                {
                    Id = 4,
                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 375.00M,
                    CategoryId = 3,
                    Discount = 29,
                    Filename = "product-04"
                },
                new Product()
                {
                    Id = 5,
                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 450.00M,
                    CategoryId = 3,
                    Discount = 13,
                    Filename = "product-05"
                },
                new Product()
                {
                    Id = 6,
                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 525.00M,
                    CategoryId = 3,
                    Discount = 29,
                    Filename = "product-06"
                },
                new Product()
                {
                    Id = 7,
                    Name = "Minola Silver Earrings",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 1,
                    Discount = 0,
                    Filename = "product-07"
                },
                new Product()
                {
                    Id = 8,
                    Name = "Sone Golden Earrings",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 1,
                    Discount = 0,
                    Filename = "product-08"
                },
                new Product()
                {
                    Id = 9,
                    Name = "Sone Golden Earrings",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 1,
                    Discount = 0,
                    Filename = "product-09"
                },
                new Product()
                {
                    Id = 10,
                    Name = "Venus Diamond Earrings",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 1,
                    Discount = 0,
                    Filename = "product-010"
                },
                new Product()
                {
                    Id = 11,
                    Name = "Venus Golden Necklace",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 2,
                    Discount = 0,
                    Filename = "product-011"
                },
                new Product()
                {
                    Id = 12,
                    Name = "Venus Silver Necklace",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 2,
                    Discount = 0,
                    Filename = "product-012"
                },
                new Product()
                {
                    Id = 13,
                    Name = "Verra Diamond Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 3,
                    Discount = 0,
                    Filename = "product-013"
                }
            );

        }
    };
}

