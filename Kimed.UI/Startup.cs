using AutoMapper;
using kimed.Business.Interface;
using kimed.Business.Repository;
using Kimed.Data.Context;
using Kimed.Data.Interface;
using Kimed.Data.Repository;
using Kimed.Infraestructure.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kimed.UI
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
            services.AddControllersWithViews();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new KimedProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddRazorPages();
            services.AddDbContext<KimedContext>(item => item.UseSqlite(Configuration.GetConnectionString("KimedDB")), ServiceLifetime.Transient);
            LoadScopes(services);
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
            app.UseHttpsRedirection();
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

        #region Private Methods 
        private static void LoadScopes(IServiceCollection services)
        {
            services.AddScoped(typeof(IDefaultRepository<>), typeof(DefaultRepository<>));
            services.AddScoped<IInfoBusiness, InfoBusiness>();
        }
        #endregion

    }
}
