using GymManagementSystem.BLL.Services.Members.Classes;
using GymManagementSystem.BLL.Services.Members.Intefaces;
using GymManagementSystem.DAL.Data.Context;
using GymManagementSystem.DAL.Repositories.Classes;
using GymManagementSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using GymManagementSystem.BLL.Services.Members.MemberValidations;

namespace GymManagementSystem.PL
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
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IPlanRepository,PlanRepository>();

            builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));

            builder.Services.AddScoped<IMemberService, MemberService>();

            builder.Services.AddValidatorsFromAssemblyContaining<CreateMemberViewModelValidator>();

            builder.Services.AddScoped<IMemberRepository, MemberRepository>(); 

            builder.Services.AddScoped<IHealthRecordRepository, HealthRecordRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


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
