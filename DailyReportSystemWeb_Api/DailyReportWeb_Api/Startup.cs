using DailyReportWeb_Api.Identity;
using DailyReportWeb_Api.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyReportWeb_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("constr"),
                b => b.MigrationsAssembly("DailyReportWeb_Api")));
            //services.AddControllers();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            //identity framwork DI
            //  services.AddDefaultIdentity<ApplicationUser>()
            //    .AddRoles<IdentityRole>()
            //.AddEntityFrameworkStores<ApplicationDbContext>();
           // services.AddScoped<RoleManager<IdentityRole>>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
            services.AddTransient<UserManager<ApplicationUser>, ApplicationUserManager>();
            services.AddTransient<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            services.AddTransient<RoleManager<ApplicationRole>, ApplicationRoleManager>();
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserStore<ApplicationUserStore>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleManager<ApplicationRoleManager>()
            .AddSignInManager<ApplicationSignInManager>()
            .AddRoleStore<ApplicationRoleStore>().AddDefaultTokenProviders();
            services.AddScoped<ApplicationRoleStore>();
            services.AddScoped<ApplicationUserStore>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DailyReportWeb_Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DailyReportWeb_Api v1"));
            }
           // IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.
           //GetRequiredService<IServiceScopeFactory>();
           // using (IServiceScope scope = serviceScopeFactory.CreateScope())
           // {
           //     var roleManager = scope.ServiceProvider.GetRequiredService
           //         <RoleManager<ApplicationRole>>();
           //     var userManager = scope.ServiceProvider.GetRequiredService
           //         <UserManager<ApplicationUser>>();
           // }
           
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
