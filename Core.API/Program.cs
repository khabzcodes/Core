using Core.Application;
using Core.Infrastructure;
using Core.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    builder.Services
        .AddInfrastructureServices(builder.Configuration)
        .AddApplicationServices();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        var initializer = scope.ServiceProvider.GetService<ApplicationDbContextInitializer>();
        if (initializer is not null)
        {
            await initializer.InitializeAsync();
            await initializer.SeedAsync();
        }
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
