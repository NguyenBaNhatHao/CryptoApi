using CryptoApi.Datas;
using Microsoft.EntityFrameworkCore;

using CryptoApi.Models;
using Microsoft.Net.Http.Headers;
using System;

var builder = WebApplication.CreateBuilder(args);
string sConnection = builder.Configuration.GetConnectionString("CryptoConnection");
builder.Services.AddDbContext<CryptoApiDbContext>(opt => opt.UseSqlServer(sConnection));
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

app.Run();
