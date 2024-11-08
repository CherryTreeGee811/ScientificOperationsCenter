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
        context.Response.Headers.ContentSecurityPolicy = "default-src 'none'; script-src-elem 'self'; style-src-elem 'self'; img-src 'self'; connect-src *;";
        context.Response.Headers.ContentLanguage = "en-US";
        context.Response.Headers.Append("Permissions-Policy", "camera=(), microphone=(), geolocation=(), bluetooth=(), payment=(), idle-detection=(), accelerometer=(),");
        await context.Response.SendFileAsync(filePath);
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("File not found");
    }
});

app.Run();
