using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()  // Adjust the minimum level as needed
//    .WriteTo.Seq("http://localhost:5341") // The URL where your Seq instance is running
//    .CreateLogger();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.WithMachineName()     // Logs the machine name
    .Enrich.WithProcessId()       // Logs the process ID
    .Enrich.WithThreadId()        // Logs the thread ID
    .WriteTo.Seq("http://localhost:5341") // Your Seq server URL
    .CreateLogger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
