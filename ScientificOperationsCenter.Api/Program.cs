using MongoDB.Driver;
using MongoDB.Bson;
using ScientificOperationsCenter.Api.DAL;
using Microsoft.EntityFrameworkCore;
using ScientificOperationsCenter.Api.Extensions;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);
//var settings = MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("MongoDB"));


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                          .AllowAnyOrigin()
                          .WithMethods("GET")
                          .WithHeaders(headers: new[] { "Accept", "Accept-Language" });
                      });
});


// Using MSSQL for now, can be replaced with MongoDB later
builder.Services.AddDbContext<ScientificOperationsCenterContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));


builder.Services.AddScientificOperationsCenterScopes();


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


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Scientific Operations Center API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors(MyAllowSpecificOrigins);


app.ApplyMigrations();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();