using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Notes.Identitiy.Data;
using Notes.Identitiy.Models;
using System.Collections.Generic;
using System.IO;

namespace Notes.Identitiy
{
    public class Startup
    {

        public IConfiguration AppConfiguration { get; }

        public Startup(IConfiguration appConfiguration)
        {
            AppConfiguration = appConfiguration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = AppConfiguration.GetValue<string>("DbConnection");
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 5;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;

            }).AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();


            services.AddIdentityServer().AddInMemoryApiResources(Configuration.ApiResources)
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryIdentityResources(new List<IdentityResource>(Configuration.IdentityResources))
                .AddInMemoryApiScopes(new List<ApiScope>(Configuration.ApiScopes))
                .AddInMemoryClients(new List<Client>(Configuration.Clients))
                .AddDeveloperSigningCredential();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Notes.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });
            services.AddControllersWithViews();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            app.UseRouting();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider
                (Path.Combine(env.ContentRootPath, "Styles")),
                RequestPath = "/styles"
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
