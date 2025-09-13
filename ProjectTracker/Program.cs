using ProjectTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjectTracker;

public static class Program
{
    private static WebApplicationBuilder _builder = null!;
    private static WebApplication _app = null!;

    private static void ConnectSqliteDatabase(string connectionString)
    {
        _builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(_builder.Configuration.GetConnectionString(connectionString)));
    }

    private static void AddServices()
    {
        _builder.Services.AddControllersWithViews();
        ConnectSqliteDatabase("DefaultConnection");
    }

    private static void InitialiseApp()
    {
        _app = _builder.Build();
        if (!_app.Environment.IsDevelopment())
        {
            _app.UseExceptionHandler("/Home/Error");
            _app.UseHsts();
        }

        _app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

        _app.UseHttpsRedirection();
        _app.UseStaticFiles();

        _app.UseRouting();

        _app.UseAuthorization();

        _app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }

    public static void Main(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);
        AddServices();
        
        InitialiseApp();
        _app.Run();
    }
}