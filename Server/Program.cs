// Very simple ASP.NET service
// Empty response delayed 1 second


var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", async () =>
{
    await Task.Delay(1000);
});

app.Run();
