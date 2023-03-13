using FoodBox.Core;
using FoodBox.Core.Models;
using FoodBox.Core.Services;
using FoodBox.Data;
using FoodBox.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
);

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IStoreProductService, StoreProductService>();

builder
    .Services
    .AddIdentity<Employee, IdentityRole>()
    .AddEntityFrameworkStores<FoodBoxDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FoodBoxDbContext>(options =>
{
    var connectionstring = builder.Configuration.GetConnectionString("DevConnection");
    options.UseSqlServer(connectionstring, x => x.MigrationsAssembly("FoodBox.Data"));
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
