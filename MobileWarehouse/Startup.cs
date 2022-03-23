using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using MobileWarehouse.Entity.Models;
using MobileWarehouse.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using MobileWarehouse.Entity.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MobileWarehouse.Extensions;
using Serilog;
using Npgsql;

namespace MobileWarehouse
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opt => opt.Filters.Add<GlobalExceptionHandler>());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // indicates whether the publisher will be verified when validating the token
                            ValidateIssuer = true,
                            // a string representing the publisher
                            ValidIssuer = AuthOptions.ISSUER,

                            // whether the consumer of the token will be verified
                            ValidateAudience = true,
                            // token consumer setup
                            ValidAudience = AuthOptions.AUDIENCE,
                            // whether the lifetime will be validated
                            ValidateLifetime = true,

                            // security key installation
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // security key check
                            ValidateIssuerSigningKey = true,
                        };
                    });

            //services.AddDbContextPool<ApplicationContext>(opt => opt.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
            //ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection")),
            //x => x.MigrationsAssembly("Entity")));

            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (databaseUrl != null)
            {
                var databaseUri = new Uri(databaseUrl);
                var userInfo = databaseUri.UserInfo.Split(':');
                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = databaseUri.Host,
                    Port = databaseUri.Port,
                    Username = userInfo[0],
                    Password = userInfo[1],
                    Database = databaseUri.LocalPath.TrimStart('/'),
                    SslMode = SslMode.Require,
                    TrustServerCertificate = true
                };
                services.AddDbContextPool<ApplicationContext>(opt => opt.UseNpgsql(builder.ToString()));
            } else
            {
                services.AddDbContextPool<ApplicationContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                   options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
               });

            services.AddControllersWithViews();
            services.AddEFModule(); 
            services.AddMobileWarehouseModule(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
