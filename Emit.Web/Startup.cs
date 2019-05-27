using Eimt.Application.Interfaces;
using Eimt.Application.Jobs;
using Eimt.Application.Services;
using Eimt.Application.Services.Impl;
using Eimt.DAL.Repository;
using Eimt.DAL.UnitOfWork;
using Eimt.Persistence;
using EiMT.Infrastructure.EmailSender;
using Emit.Web.Hubs;
using Emit.Web.Scedular;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Emit.Web
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
            services.AddDbContext<EiMTDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EiMTConnectionString")));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IMessageSender, EmailSender>();
            services.AddScoped<IUserMessageSender, UserEmailSender>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISectorService, SectorService>();
            services.AddScoped<IDocumentService, ExelService>();
            services.AddHttpContextAccessor();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.LoginPath = "/user/login";
               });
            services.AddSignalR();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.Configure<EmailSenderConfiguration>(Configuration.GetSection("EmailSender"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddQuartz(typeof(DeleteOldConfirmationTokensJob));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>("/notification");
                routes.MapHub<ChatHub>("/chathub");
            });

            app.UseQuartz();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=User}/{action=Index}/{id?}");
            });
        }
    }
}
