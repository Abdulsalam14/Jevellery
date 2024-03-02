using Jevellery.Constants;
using Jevellery.Models;
using Microsoft.AspNetCore.Identity;

namespace Jevellery.Helpers
{
    public static class DbInitializer
    {
        public async static Task SeedAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
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
        }
    }
}
