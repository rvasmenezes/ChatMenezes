using ChatMenezes.Domain;
using ChatMenezes.Domain.Aggregates.StockAgg.Interfaces;
using ChatMenezes.Domain.Aggregates.UserAgg.Entities;
using ChatMenezes.Infra.Data;
using ChatMenezes.Infra.Data.Context;
using ChatMenezes.Web.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;

namespace ChatMenezes.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "User.Cokkie";
                    config.LoginPath = "/Account";
                    config.ExpireTimeSpan = TimeSpan.FromHours(8);
                });

            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddHttpContextAccessor();

            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromHours(8);
            });

            services.AddDbContext<EFContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DbContext")));

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                //options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<EFContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromHours(8)
            );

            services.AddAntiforgery();
            services.AddTransient<INotificationService, SignalRNotificationService>();
            services.AddServicesDependencies();
            services.AddInfraDependencies();

            //Outro serviços
            services.AddSingleton(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IOptions<RequestLocalizationOptions> options)
        {

            app.UseRequestLocalization(options.Value);

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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");

                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}
