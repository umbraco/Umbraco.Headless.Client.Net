var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Umbraco Heartcore.
builder.Services.AddUmbracoHeartcore(options =>
{
    // register all strongly typed models from the current assembly.
    options.AddModels(typeof(Program).Assembly);
});//.AddPreview(); // uncomment to enable preview

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();