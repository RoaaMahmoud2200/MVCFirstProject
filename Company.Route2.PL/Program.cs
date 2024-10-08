using AutoMapper;
using Company.Route2.BLL.Interfaces;
using Company.Route2.BLL.Repositories;
using Company.Route2.BLL.UnitOfWork;
using Company.Route2.DAL.Data.Contexts;
using Company.Route2.DAL.Models;
using Company.Route2.PL.Mapping;
using Company.Route2.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.Route2.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IDepartementRepository, DepartementRepository>();// ask CLR to create obj from it when he need
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();// ask CLR to create obj from it when he need

            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                //option.UseSqlServer("Server=DESKTOP-7AUTDK2\\MSSQLSERVER01 ; Database=Company ; Trusted_Connection=True; TrustServerCertificate=True");
                option.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultString"]);
            });// ask CLR to create obj from it when he need(allow Dependency Injection )


            builder.Services.AddScoped<IScoped, Scoped>();
            builder.Services.AddSingleton<ISingletone, Singletone>();
            builder.Services.AddTransient<ITransient, Transient>();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(EmployeeProfile));      
            builder.Services.AddAutoMapper(typeof(DepartmentProfile));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });

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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

     
    }
}
