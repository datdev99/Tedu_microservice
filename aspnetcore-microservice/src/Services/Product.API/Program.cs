using Common.Logging;
using Product.API.Extensions;
using Product.API.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Start Product API up");

try
{
    builder.Host.UseSerilog(SeriLogger.Configure);
    builder.AddAppConfiguration();
    //Add services to the container
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();
    
    app.UseInfrastructure();
    
    app.MigrateDatabase<ProductContext>((context, _) =>
    {
        ProductContextSeed.SeedProductAsync(context, Log.Logger).Wait();
    }).Run();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Stop Product API up");
    Log.CloseAndFlush();
}