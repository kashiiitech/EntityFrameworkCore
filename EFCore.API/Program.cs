using System.Text.Json.Serialization;
using EFCore.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add a DbContext here
builder.Services.AddDbContext<MoviesContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("MoviesContext");
    optionsBuilder.UseSqlServer(connectionString)
        .LogTo(Console.WriteLine);
},
    ServiceLifetime.Scoped,
    ServiceLifetime.Singleton);

var app = builder.Build();

// DIRTY HACK, we will come back to fix this
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MoviesContext>();
// context.Database.EnsureDeleted(); // all the data will be losted
// context.Database.EnsureCreated(); // we will recreate it
// await context.Database.MigrateAsync();

var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
if (pendingMigrations.Count() > 0)
    throw new Exception("Database is not fully migrated for moviesContext.");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();