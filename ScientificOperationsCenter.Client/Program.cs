var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();


async static Task ServeIndexHtml(HttpContext context)
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
}


app.MapGet("/", async context => await ServeIndexHtml(context));
app.MapGet("/login", async context => await ServeIndexHtml(context));
app.MapGet("/temperatures", async context => await ServeIndexHtml(context));
app.MapGet("/temperatures/day", async context => await ServeIndexHtml(context));
app.MapGet("/temperatures/month", async context => await ServeIndexHtml(context));
app.MapGet("/temperatures/year", async context => await ServeIndexHtml(context));
app.MapGet("/radiation-measurements", async context => await ServeIndexHtml(context));
app.MapGet("/radiation-measurements/day", async context => await ServeIndexHtml(context));
app.MapGet("/radiation-measurements/month", async context => await ServeIndexHtml(context));
app.MapGet("/radiation-measurements/year", async context => await ServeIndexHtml(context));


app.Run();