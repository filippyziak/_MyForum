using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyForum.Core.Filters;
using MyForum.Core.Helpers;
using MyForum.Core.Services;
using MyForum.Core.Services.Interface;
using MyForum.Core.Settings;
using MyForum.Data;

namespace MyForum
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
           {
               opt.UseNpgsql(Configuration.GetConnectionString(AppSettingsKeys.ConnectionString));
           });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = Routes.LoginPath;
                options.LogoutPath = Routes.LogoutPath;
                options.ExpireTimeSpan = TimeSpan.FromDays(Constants.TokenExpireTimeInDays);
            });


            services.AddControllersWithViews();

            services.AddHttpContextAccessor();
            services.AddSession();

            #region services
            services.AddSingleton<ITokenClaimsManager, TokenClaimsManager>();

            services.AddScoped<IDatabase, Database>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IResetPasswordService, ResetPasswordService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IDatabaseManager, DatabaseManager>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICryptoService, CryptoService>();
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddSingleton<Alertify>(a => Alertify.Build());
            #endregion

            #region settings

            services.Configure<EmailSettings>(Configuration.GetSection(nameof(EmailSettings)));
            #endregion

            services.AddScoped<OnlyAnonymousFilter>();

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            });
        }
    }
}

