
using Jevellery.Helpers;
using Jewellery.Business.Abstract;
using Jewellery.Business.Concrete;
using Jewellery.DataAccess.Abstract;
using Jewellery.DataAccess.Abstract.Home;
using Jewellery.DataAccess.Concrete.EFEntityFramework;
using Jewellery.DataAccess.Concrete.EFEntityFramework.Home;
using Jewellery.Entities;
using Jewellery.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });
        builder.Services.AddScoped<IProductDal, EFProductDal>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICartDal, EFCartDal>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<ICartProductDal, EFCartProductDal>();
        builder.Services.AddScoped<ICartProductService, CartProductService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ICategoryDal, EFCategoryDal>();
        builder.Services.AddScoped<IPhotoService, PhotoService>();
        builder.Services.AddScoped<IFirstContentService, FirstContentService>();
        builder.Services.AddScoped<IFIrstContentDal, EFFirstContentDal>();





        var conn = builder.Configuration.GetConnectionString("myconn");
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(conn);
        });


        builder.Services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=category}/{action=Index}/{id?}"
        );


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");




        //app.MapDefaultControllerRoute();

        var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
        using (var scope = scopeFactory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetService<RoleManager<AppRole>>();
            var context = scope.ServiceProvider.GetService<AppDbContext>();
            var photoService = scope.ServiceProvider.GetService<IPhotoService>();
            await DbInitializer.SeedAsync(userManager, roleManager, context, photoService);

        }

        app.Run();
    }
}