using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Data;
using Models;
using Microsoft.AspNetCore.Identity;
using System;
using Hooxit.Services.Contracts;
using Hooxit.Services.Implementation.Managers;
using Hooxit.Services.Implementation.ApplicationServices;
using Hooxit.Data.UnitOfWork;
using Hooxit.Data.Contracts;
using Hooxit.Data.Implementation;
using Hooxit.Services;
using Hooxit.Services.Implementation.ApplicationServices.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Hooxit.Services.Implementation;
using Hooxit.Data.Repository;
using Hooxit.Presentation.Write;
using Hooxit.Services.Implementation.Profile;
using Hooxit.Services.Implementation.Company.Interfaces;
using Hooxit.Services.Implementation.Company;
using Hooxit.Services.Implementation.Company.Implemenation;
using Hooxit.Services.Implementation.Company.Implementation;

namespace Hooxit
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HooxitDbContext>(options =>
                options.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Hooxit;Trusted_Connection=True;Integrated Security=True"));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<HooxitDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CandidatePolicy", policy => policy.RequireClaim("CandidateRole", "Candidate"));
                options.AddPolicy("CompanyPolicy", policy => policy.RequireClaim("CompanyRole", "Company"));
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
            });

            services.AddScoped<IHooxitDbContext, HooxitDbContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProfileManager, ProfileManager>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IExperienceService, ExperienceService>();
            services.AddTransient<ICommonDataManager, CommonDataReadManager>();
            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();
            services.AddTransient<IUserPersonalInfoHandler<ChangeEmail>, ChangeEmailHandler>();
            services.AddTransient<IUserPersonalInfoHandler<ChangeCountry>, ChangeCountryHandler>();
            services.AddTransient<IUserPersonalInfoHandler<ChangeCurrentPosition>, ChangeCurrentPositionHandler>();
            services.AddTransient<IPositionsApplicationService, PositionsApplicationService>();
            services.AddTransient<IPositionsManager, PositionsManager>();
            services.AddTransient<ICompanyProfileManager, CompanyProfileManager>();
            services.AddTransient<ICompanyProfileApplicationService, CompanyProfileApplicationService>();
            services.AddTransient<DatabaseSeed>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                //options.Password.RequireDigit = true;
                //options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                //options.Password.RequireLowercase = false;

                // Lockout settings
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //options.Lockout.MaxFailedAccessAttempts = 10;

                //// Cookie settings
                //options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                //options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                //options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOut";

                //// User settings
                //options.User.RequireUniqueEmail = true;
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DatabaseSeed databaseSeeder)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/Hooxit-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.Use((context, next) =>
            {
                var identityName = context.User.Identity.Name;

                if (identityName != null)
                {
                    UserInfo.UserName = identityName;
                }

                return next();
            });

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Profile",
                    template: "Profile/{*username}",
                    defaults: new { controller = "Profile", action = "Profile" });

                routes.MapRoute(
                    name: "Account",
                    template: "{area:exists}/{controller=Account}/{action=Index}/{id?}");
            });

            databaseSeeder.SeedDB(app);
        }
    }
}
