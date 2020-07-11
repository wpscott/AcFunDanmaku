using AcFunDMJ_WASM.Server.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace AcFunDMJ_WASM.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSignalR();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
            });

            services.AddHostedService<Worker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<DanmakuHub>("/danmaku");
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
