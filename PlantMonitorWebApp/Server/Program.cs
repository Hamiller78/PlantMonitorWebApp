using Microsoft.AspNetCore.ResponseCompression;
using PlantMonitorWebApp.Server.Hubs;
using PlantMonitorWebApp.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<SensorValueHub>();  // This was a guess to get the SensorValueHub with Clients injected into the MockSensorService, didn't work

// From SignalR tutorial adding a ChatHub. Do we need this as well?
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
// builder.Services.AddHostedService<MockSensorService>();
builder.Services.AddHostedService<RestSensorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapHub<SensorValueHub>("/sensorvaluehub");
app.MapFallbackToFile("index.html");

app.Run();