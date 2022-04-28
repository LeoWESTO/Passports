using Microsoft.EntityFrameworkCore;
using Passports.Data;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(connection));

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapDefaultControllerRoute();

app.Run();