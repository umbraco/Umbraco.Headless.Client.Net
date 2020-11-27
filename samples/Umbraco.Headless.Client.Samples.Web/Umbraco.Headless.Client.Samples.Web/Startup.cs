using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Umbraco.Headless.Client.Net.Web;
using Umbraco.Headless.Client.Samples.Web.Models;

namespace Umbraco.Headless.Client.Samples.Web
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

            var umbracoConfig = Configuration.GetSection("umbraco");
            var projectAlias = umbracoConfig.GetValue<string>("projectAlias");
            var apiKey = umbracoConfig.GetValue<string>("apiKey");

            // Add Umbraco Headless
            services.AddUmbracoHeadless(projectAlias, apiKey, configuration =>
            {
                configuration.ContentModelTypes.Add<Frontpage>();
                configuration.ContentModelTypes.Add<Textpage>();

                configuration.ElementModelTypes.Add<TextAndImage>();
            });
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

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/robots.txt"))
                {
                    var output = "User-agent: *  \nDisallow: /";
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(output);
                }
                else
                {
                    await next();
                }
            });

            app.UseRouting();

            app.UseAuthorization();

            // Register Headless router
            app.UseUmbracoHeadlessRouter(options =>
            {
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
