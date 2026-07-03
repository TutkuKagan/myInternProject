using Microsoft.EntityFrameworkCore;
using myInternProject.API.Mapping;
using myInternProject.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//-------------------------------------------con String----------------------------------
string? dbChoice = builder.Configuration["DatabaseProvider"];
    if(dbChoice == "PostgreSQL")
{
    var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
}
    else if (dbChoice == "Oracle")
{
    var connectionString = builder.Configuration.GetConnectionString("OracleConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseOracle(connectionString));
}
    else
{
    throw new  Exception("Invalid DatabaseProvider choice in appsettings.json"); 
}
builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
