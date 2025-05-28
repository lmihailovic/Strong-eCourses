using MongoDB.Driver;
using Strong_eCourses.Services;
using Strong_eCourses.Settings;
using Strong_eCourses.Models;
using Strong_eCourses;
using MongoDB.Driver;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var mongoDbSettings=builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
.AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
(mongoDbSettings.ConnectionString, mongoDbSettings.Name);

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDbConfig")
);

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = builder.Configuration.GetSection("MongoDbConfig").Get<MongoDbConfig>();
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped<MongoDBService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();