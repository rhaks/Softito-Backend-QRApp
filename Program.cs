using Microsoft.EntityFrameworkCore;
using QRMenuUygulama.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QRMenuUygulama;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<Data.ApplicationContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDatabase")));

        builder.Services.AddSession();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.UseSession();

        app.Run();
    }
}
