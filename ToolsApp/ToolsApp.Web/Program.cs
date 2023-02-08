using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Data;
using ToolsApp.Web.Data;
using ToolsApp.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<ScreenBlocker>();

builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddScoped<ToolsAppDapperContext>();
builder.Services.AddScoped<IColorsData, ColorsDapperData>();
builder.Services.AddScoped<ICarsData, CarsDapperData>();

//builder.Services.AddSingleton<IColorsData, ColorsInMemoryData>();
//builder.Services.AddSingleton<ICarsData, CarsInMemoryData>();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
