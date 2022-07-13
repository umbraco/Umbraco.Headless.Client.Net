namespace Umbraco.Headless.Client.Samples.MVC
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

            // Add Umbraco Heartcore.
            services
                .AddUmbracoHeartcore(options =>
                {
                    // register all strongly typed models from the current assembly.
                    options.AddModels(GetType().Assembly);
                })
                // uncomment to enable preview
                // .AddPreview()
                ;
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

            //Configures route to wwwroot/robots.txt to tell search engine crawlers which URLs the crawler can access.
            app.UseRobotsTxt();

            app.UseRouting();

            app.UseAuthorization();

            // Enable Heartcore routing,
            app.UseUmbracoHeartcoreRouting();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
