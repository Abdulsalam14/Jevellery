using Jevellery.Constants;
using Jewellery.Business.Abstract;
using Jewellery.Business.Concrete;
using Jewellery.Entities;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Jevellery.Helpers
{
    public static class DbInitializer
    {
        public async static Task SeedAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppDbContext context, IPhotoService photoservice)
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new AppRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
            if (await userManager.FindByNameAsync("admin01") == null)
            {
                var user = new AppUser
                {
                    UserName = "admin01",
                    Email = "admin01@gmail.com"
                };
                var password = "Admin_01";
                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }


            if (!context.Categories.Any())
            {
                await context.Categories.AddRangeAsync(new[]
                {
                    new Category() { 
                        Name = "Earrings",
                        //Filename = "category1"
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\category1.jpg"),

                    },
                    new Category() {
                        Name = "Necklages", 
                        //Filename = "category2" 
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\category2.jpg"),

                    },
                    new Category() { 
                        Name = "Rings", 
                        //Filename = "category3" 
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\category3.jpg"),

                    }
                });
                await context.SaveChangesAsync();

            }

            if (!context.Products.Any())
            {
                await context.Products.AddRangeAsync(new[]
                {

                    new Product()
                {
                    Name = "Minola Golden Earrings",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 1,
                    Discount = 0,
                    //Filename = "product-01",
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-01.jpg"),
                    IsNewArrival = true,
                },
                new Product()
                {

                    Name = "Minola Golden Necklace",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 2,
                    Discount = 0,
                    //Filename = "product-02",
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-02.jpg"),
                    IsNewArrival = true,
                },
                new Product()
                {

                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 3,
                    Discount = 26,
                    //Filename = "product-03"
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-03.jpg"),

                },
                new Product()
                {

                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 375.00M,
                    CategoryId = 3,
                    Discount = 29,
                    //Filename = "product-04"
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-04.jpg"),

                },
                new Product()
                {

                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 450.00M,
                    CategoryId = 3,
                    Discount = 13,
                    //Filename = "product-05"
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-05.jpg"),

                },
                new Product()
                {

                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 525.00M,
                    CategoryId = 3,
                    Discount = 29,
                    //Filename = "product-06"
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-06.jpg"),

                },
                new Product()
                {

                    Name = "Minola Silver Earrings",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 1,
                    Discount = 0,
                    //Filename = "product-07",
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-07.jpg"),

                    IsNewArrival = true
                },
                new Product()
                {

                    Name = "Sone Golden Earrings",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 1,
                    Discount = 0,
                    //Filename = "product-08"
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-08.jpg"),

                },
                new Product()
                {

                    Name = "Sone Golden Earrings",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 1,
                    Discount = 0,
                    //Filename = "product-09",
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-09.jpg"),

                    IsFeatured = true
                },
                new Product()
                {

                    Name = "Venus Diamond Earrings",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 1,
                    Discount = 0,
                    //Filename = "product-010"
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-010.jpg"),

                },
                new Product()
                {

                    Name = "Venus Golden Necklace",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 2,
                    Discount = 0,
                    //Filename = "product-011"
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-011.jpg"),

                },
                new Product()
                {

                    Name = "Venus Silver Necklace",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 2,
                    Discount = 0,
                    //Filename = "product-012"
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-012.jpg"),

                },
                new Product()
                {

                    Name = "Verra Diamond Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 3,
                    Discount = 0,
                    //Filename = "product-013",
                    Filename=await photoservice.UploadImageFromPathAsync("C:\\Users\\HP\\source\\repos\\Jevellery\\Jevellery\\wwwroot\\img\\Products\\product-013.jpg"),

                    IsFeatured = true
                }
                });

                await context.SaveChangesAsync();
            }

        }
    }
}
