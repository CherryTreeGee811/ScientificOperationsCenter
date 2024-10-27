using MongoDB.Driver;
using MongoDB.Bson;
using ScientificOperationsCenter.DAL;
using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.DAL.Interfaces;
using ScientificOperationsCenter.BusinessLogic.Interfaces;
using ScientificOperationsCenter.BusinessLogic;
using ScientificOperationsCenter.Mappers;
using ScientificOperationsCenter.Mappers.Interfaces;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
//var settings = MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("MongoDB"));

Log.Logger = new LoggerConfiguration()
    // .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] <{Level:u3}> {Message:lj}{NewLine}{Exception}")
    .WriteTo.File("logs/log.txt", outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss}] <{Level:u3}> {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Using MSSQL for now, can be replaced with MongoDB later
builder.Services.AddDbContext<ScientificOperationsCenterContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));


builder.Services.AddScoped<IScientificOperationsCenterContext, ScientificOperationsCenterContext>();
builder.Services.AddScoped<ITemperaturesRepository, TemperaturesRepository>();
builder.Services.AddScoped<ITemperaturesService, TemperaturesService>();
builder.Services.AddScoped<ITemperaturesMapper, TemperaturesMapper>();
builder.Services.AddScoped<IRadiationMeasurementsRepository, RadiationMeasurementsRepository>();
builder.Services.AddScoped<IRadiationMeasurementsService, RadiationMeasurementsService>();
builder.Services.AddScoped<IRadiationMeasurementsMapper, RadiationMeasurementsMapper>();

// Set the ServerApi field of the settings object to set the version of the Stable API on the client
//settings.ServerApi = new ServerApi(ServerApiVersion.V1);


// Create a new client and connect to the database server
//var client = new MongoClient(settings);


// Send a ping to confirm a successful connection to MongoDB
/*try
{
    var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
    Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}*/


builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
);

Log.Information("Web server starting up.");

app.Run();