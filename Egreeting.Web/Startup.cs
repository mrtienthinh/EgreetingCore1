using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Egreeting.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Egreeting.Models.AppContext;
using Egreeting.Business.IBusiness;
using Egreeting.Business.Business;
using log4net;
using Microsoft.Extensions.Logging;
using Egreeting.Models.Models;

namespace Egreeting.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            StaticConfig = configuration;
            StaticHostEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public static IConfiguration StaticConfig { get; private set; }
        public static IWebHostEnvironment StaticHostEnvironment { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EgreetingContext>(options =>
                options
                .UseLazyLoadingProxies()
                .UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<EgreetingContext>();

            services.AddScoped<DbContext, EgreetingContext>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<EgreetingContext>()
                    .AddDefaultUI()
                    .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddTransient<ICategoryBusiness, CategoryBusiness>();
            services.AddTransient<IEcardBusiness, EcardBusiness>();
            services.AddTransient<IEgreetingRoleBusiness, EgreetingRoleBusiness>();
            services.AddTransient<IEgreetingUserBusiness, EgreetingUserBusiness>();
            services.AddTransient<IFeedbackBusiness, FeedbackBusiness>();
            services.AddTransient<IOrderBusiness, OrderBusiness>();
            services.AddTransient<IOrderDetailBusiness, OrderDetailBusiness>();
            services.AddTransient<IPaymentBusiness, PaymentBusiness>();
            services.AddTransient<IScheduleSenderBusiness, ScheduleSenderBusiness>();
            services.AddTransient<ISubcriberBusiness, SubcriberBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
