using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MvcTask.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcTaskContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MvcTaskContext") ?? throw new InvalidOperationException("Connection string 'MvcTaskContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Assuming Startup class is in the root namespace
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();