using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThinkBridgeSoft.Areas.Shopbridge.Models;

namespace ThinkBridgeSoft
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            new GetFolderPath(configuration);
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddHttpClient();
            services.AddRazorPages();
            RegisterDBContext(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            var loSupportedCultures = new[] { "en-US" };
            var loLocalizationOption = new RequestLocalizationOptions().SetDefaultCulture(loSupportedCultures[0])
                .AddSupportedCultures(loSupportedCultures)
                .AddSupportedUICultures(loSupportedCultures);

            app.UseRequestLocalization(loLocalizationOption);
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void RegisterDBContext(IServiceCollection services)
        {
            string lsDBConnectionSting = Configuration.GetConnectionString("DBConnection");

            services.AddDbContext<ShopBridgeDbContext>(options =>
                options.UseSqlServer(lsDBConnectionSting), ServiceLifetime.Transient, ServiceLifetime.Transient);
        }
    }
}
