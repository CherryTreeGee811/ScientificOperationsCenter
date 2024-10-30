var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();


app.MapGet("/", async context =>
{
    var filePath = Path.Combine("wwwroot", "index.html");
    if (File.Exists(filePath))
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync(filePath);
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("File not found");
    }
});

app.Run();
