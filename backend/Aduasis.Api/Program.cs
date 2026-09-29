using Aduasis.Api.Configuracion;
using Aduasis.Api.Datos;

var builder = WebApplication.CreateBuilder(args);

// ─────────────────────────────────────────────
// Registro de servicios de la aplicación
// Cada extensión está definida en Configuracion/
// para mantener Program.cs limpio y legible.
// ─────────────────────────────────────────────

builder.Services.AgregarBaseDatos(builder.Configuration);
builder.Services.AgregarAutenticacionJwt(builder.Configuration);
builder.Services.AgregarCors(builder.Configuration);
builder.Services.AgregarServiciosAplicacion();
builder.Services.AgregarValidaciones();
builder.Services.AgregarMapeos();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new()
    {
        Title = "ADUASIS API",
        Version = "v1",
        Description = "API para la gestión de activos tecnológicos - Aduana Nacional La Paz"
    });
});

// ─────────────────────────────────────────────
// Construcción del pipeline HTTP
// ─────────────────────────────────────────────

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opciones =>
    {
        opciones.SwaggerEndpoint("/swagger/v1/swagger.json", "ADUASIS API v1");
        opciones.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseCors("PoliticaCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Inicializar la base de datos (migraciones + seed del administrador)
await InicializadorBD.InicializarAsync(app.Services);

app.Run();
