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
                        Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930543/bucnq4c02adk4yljbbri.jpg"

                    },
                    new Category() {
                        Name = "Necklages",
                        Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930544/qgofzxaqcwxvmhgk4imk.jpg",

                    },
                    new Category() {
                        Name = "Rings",
                        Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930544/e3qihj7geql7m3alv51e.jpg"

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
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930545/anfvvim7qy3vkidkkjvd.jpg",
                    IsNewArrival = true,
                },
                new Product()
                {

                    Name = "Minola Golden Necklace",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 2,
                    Discount = 0,
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930546/xlu6tedjew9xntbcvnzj.jpg",
                    IsNewArrival = true,
                },
                new Product()
                {

                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 3,
                    Discount = 26,
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930546/wjgmlsrhnbpfwogwa3td.jpg",

                },
                new Product()
                {

                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 375.00M,
                    CategoryId = 3,
                    Discount = 29,
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930546/mb5oo9kk3iubjpcwdsl8.jpg"

                },
                new Product()
                {

                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 450.00M,
                    CategoryId = 3,
                    Discount = 13,
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930547/i29hscsrbizqfkyjd3on.jpg",

                },
                new Product()
                {

                    Name = "Minola Golden Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 525.00M,
                    CategoryId = 3,
                    Discount = 29,
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930548/wzwletlgtefc7higb97y.jpg",

                },
                new Product()
                {

                    Name = "Minola Silver Earrings",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 1,
                    Discount = 0,
                    //Filename = "product-07",
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930549/kbkncdxqffjv5frquknc.jpg",

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
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930549/acnlurxpm3nxzr06yk3v.jpg",

                },
                new Product()
                {

                    Name = "Sone Golden Earrings",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 1,
                    Discount = 0,
                    //Filename = "product-09",
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930550/j4vwspyj3snhrvsz1ji1.jpg",

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
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930550/cemcefrltw2goow6q8ny.jpg",

                },
                new Product()
                {

                    Name = "Venus Golden Necklace",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 2,
                    Discount = 0,
                    //Filename = "product-011"
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930551/ldlrakhdz9dmvrwbl7fs.jpg",
                },
                new Product()
                {

                    Name = "Venus Silver Necklace",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 2,
                    Discount = 0,
                    //Filename = "product-012"
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930551/sttdibzmqc76bhxk1ado.jpg",

                },
                new Product()
                {

                    Name = "Verra Diamond Ring",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat two.",
                    Price = 325.00M,
                    CategoryId = 3,
                    Discount = 0,
                    Filename="https://res.cloudinary.com/drtjslzxf/image/upload/v1709930552/k456tt0cncs079pjyrmd.jpg",

                    IsFeatured = true
                }
                });

                await context.SaveChangesAsync();
            }

        }
    }
}
