using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlantMonitorWebApp.Repository;
using PlantMonitorWebApp.Repository.Classes;
using PlantMonitorWebApp.Repository.Interfaces;
using PlantMonitorWebApp.Server.Hubs;
using PlantMonitorWebApp.Server.Interfaces;
using PlantMonitorWebApp.Server.Services;
using PlantMonitorWebApp.Server.Services.DataUpdater;
using PlantMonitorWebApp.Server.Services.MessageSender;
using PlantMonitorWebApp.Shared.Factories;
using PlantMonitorWebApp.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<PlantAppContext>(options => options.UseNpgsql(configuration["ConnectionStrings:PlantWebDb"]));
}
else
{
    builder.Services.AddDbContext<PlantAppContext>(options => options.UseSqlite(configuration["ConnectionStrings:SQLite"]));
}
builder.Services.AddLogging(logging => logging.AddConsole());

builder.Services.AddSingleton<SensorSignalRSender>();
builder.Services.AddSingleton<IDataUpdater, RestDataUpdater>();
builder.Services.AddSingleton<IMessageSender, SignalRSender>();
builder.Services.AddSingleton<IDataSourceFactory, RestDataSourceFactory>();
builder.Services.AddScoped<IPlantInterface, PlantRepository>();

// From SignalR tutorial adding a ChatHub. Do we need this as well?
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
builder.Services.AddHostedService<RestSensorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseDeveloperExceptionPage();
}
else
{
    // app.UseExceptionHandler("/Error");
    app.UseDeveloperExceptionPage();  // Good enough for the early stage, bad practice for real production code
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapHub<SensorSignalRSender>("/sensorvaluehub");
app.MapFallbackToFile("index.html");

app.Run();