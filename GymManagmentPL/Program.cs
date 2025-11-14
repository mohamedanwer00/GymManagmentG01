using GymManagementBLL.BusinessServices.Implementation;
using GymManagmentBLL.BusinessServices.Implememtation;
using GymManagmentBLL.BusinessServices.Interfaces;
using GymManagmentBLL.Mapping;
using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Data.SeedData;
using GymManagmentDAL.Repositories.Implementations;
using GymManagmentDAL.Repositories.Interfaces;
using GymManagmentDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagmentPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region DI Regsiteration
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
            });

            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericReposatory<>));
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            //builder.Services.AddScoped(typeof(IPlanRepository), typeof(PlanReposatory));
            builder.Services.AddScoped(typeof(ISessionRepository), typeof(SessionRepository));
            builder.Services.AddScoped(typeof(IMembershipRepository), typeof(MembershipRepository));
            builder.Services.AddScoped(typeof(IBookingRepository), typeof(BookingRepository));

            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();


            builder.Services.AddAutoMapper(X => X.AddProfile(new MappingProfile()));

            builder.Services.AddScoped<IMemberServices, MemberServices>();
            builder.Services.AddScoped<IPlanServices, PlanServices>();
            builder.Services.AddScoped<ISessionServices, SessionServices>();
            builder.Services.AddScoped<ITrainerServices, TrainerServices>();
            builder.Services.AddScoped<IMembershipService, MembershipService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            //builder.Services.AddScoped<IAttachementService, AttachementService>();




            #endregion

            var app = builder.Build();

            #region seeding data
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();

            var pendingMigrations = dbContext.Database.GetPendingMigrations();

            if (pendingMigrations?.Any() ?? false)
                dbContext.Database.Migrate();


            GymDbContextSeeding.SeedDate(dbContext);
            #endregion

            #region Configure PipeLine [Middlew]

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


            #endregion

            app.Run();
        }
    }
}
