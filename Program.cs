using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using pi.Data;
using pi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEntityFrameworkMySQL().AddDbContext<BancoContext>(
    o => o.UseMySQL(
        builder.Configuration.GetConnectionString("Default")
    )
);

builder.Services.AddScoped<IClienteRepository,ClienteRepository>();
builder.Services.AddScoped<IProdutorRepository,ProdutorRepository>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
