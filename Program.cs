using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using SeniorProject.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    
    //db connection (added builder before services because it changed in .net6)
    builder.Services.AddDbContextPool<ApplicationDbContext>(
               options => options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbConnection")));


builder.Services.AddControllersWithViews();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
  //options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

app.UseAuthorization();

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
