using CollegeProject.Data;
using CollegeProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));

var server = builder.Configuration["DBServer"] ?? "demoappdb";
var port = builder.Configuration["DBPort"] ?? "1433";
var database = builder.Configuration["DBDatabase"] ?? "SupplierPortalPortal_Docker";
var user = builder.Configuration["DBUser"] ?? "sa";
var password = builder.Configuration["DBPassword"] ?? "Test@123";
var connectionString = $"Server={server},{port};Database={database};User={user};Password={password};";
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.LoginPath = "/Home/Home/Login";
    options.AccessDeniedPath = "/Home/Home/Login";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBelongToAdmin", policy => policy.RequireClaim("Role", "Admin"));
    options.AddPolicy("MustBelongToWareHouseManager", policy => policy.RequireClaim("Role", "WareHouseManager"));
    options.AddPolicy("MustBelongToDeliveryPerson", policy => policy.RequireClaim("Role", "DeliveryPerson"));
    options.AddPolicy("MustBelongToSupplier", policy => policy.RequireClaim("Role", "Supplier"));
    options.AddPolicy("MustBelongToCustomer", policy => policy.RequireClaim("Role", "Customer"));
});

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Home}/{controller=Home}/{action=Login}/{id?}");

app.Run();
