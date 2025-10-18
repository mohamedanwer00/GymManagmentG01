using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using GymManagmentDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                options.UseSqlServer(builder.Configuration["ConnectionStrings :DefaultConnection"]);
            });

            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericReposatory<>));
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            //builder.Services.AddScoped(typeof(IPlanRepository), typeof(PlanReposatory));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
