using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;

using PlantMonitorWebApp.Repository;
using PlantMonitorWebApp.Repository.Classes;
using PlantMonitorWebApp.Repository.Interfaces;
using PlantMonitorWebApp.Server.Hubs;
using PlantMonitorWebApp.Server.Interfaces;
using PlantMonitorWebApp.Server.Services;
using PlantMonitorWebApp.Server.Services.DataUpdater;
using PlantMonitorWebApp.Server.Services.ImageManager;
using PlantMonitorWebApp.Server.Services.MessageSender;
using PlantMonitorWebApp.Shared.Factories;
using PlantMonitorWebApp.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;

var configuration = builder.Configuration;
configuration.AddEnvironmentVariables();  // to load Azure WebApp configuration variables

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<PlantAppContext>(options => options.UseSqlServer(configuration.GetConnectionString("PlantWebDb")));

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.AddAzureWebAppDiagnostics();
});
builder.Services.Configure<AzureFileLoggerOptions>(configuration =>
{
    configuration.FileName = "my-azure-diagnostics";
    configuration.FileSizeLimit = 1024;
    configuration.RetainedFileCountLimit = 5;
});

builder.Services.AddSingleton<SensorSignalRSender>();
builder.Services.AddSingleton<IDataUpdater, RestDataUpdater>();
builder.Services.AddSingleton<IMessageSender, SignalRSender>();
builder.Services.AddSingleton<IDataSourceFactory, RestDataSourceFactory>();
builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<IImageStorageHandler, ImageAzureBlobHandler>();

// From SignalR tutorial adding a ChatHub. Do we need this as well?
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
builder.Services.AddHostedService<RestSensorService>();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["ConnectionStrings:ImageBlobStorage:blob"], preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["ConnectionStrings:ImageBlobStorage:queue"], preferMsi: true);
});

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

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapHub<SensorSignalRSender>("/sensorvaluehub");
app.MapFallbackToFile("index.html");

app.Run();