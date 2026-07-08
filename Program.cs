using Garantias.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Controllers
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
    });

// ✅ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


var app = builder.Build();


// Aplicar migraciones automáticamente en Docker / CI
if (
    app.Environment.IsDevelopment() ||
    Environment.GetEnvironmentVariable("APPLY_MIGRATIONS") == "true"
)
{
    using var scope = app.Services.CreateScope();

    var db =
        scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var retries = 10;

    while (retries > 0)
    {
        try
        {
            db.Database.Migrate();

            Console.WriteLine("Migraciones aplicadas correctamente.");

            break;
        }
        catch (Exception ex)
        {
            retries--;

            Console.WriteLine(
                $"No se pudo conectar a SQL Server. Reintentos restantes: {retries}"
            );

            Console.WriteLine(ex.Message);

            if (retries == 0)
            {
                throw;
            }

            await Task.Delay(5000);
        }
    }
}

// ✅ Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Evitar redirección HTTPS dentro de Docker si se desactiva por variable
if (
    Environment.GetEnvironmentVariable("DISABLE_HTTPS_REDIRECTION") != "true"
)
{
app.UseHttpsRedirection();
}

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

