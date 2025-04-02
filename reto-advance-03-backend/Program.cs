
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using reto_advance_03_backend.Data;
using reto_advance_03_backend.Entities;
using reto_advance_03_backend.Repositories;
using reto_advance_03_backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod();
    });
});

// Add in-memory DB
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseInMemoryDatabase("MyInMemoryDb"));

// Register repositories
builder.Services.AddScoped<IVehiculoRepository, VehiculoRepository>();
builder.Services.AddScoped<IHorarioRestriccionRepository, HorarioRestriccionRepository>();

// Register services
builder.Services.AddScoped<IRestriccionesService, RestriccionesService>();

// Add services to the container.
builder.Services.AddAuthentication().AddJwtBearer(options => {
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("correct horse battery staple correct horse battery staple correct horse battery staple"));
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = key
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MyDbContext>();

    context.HorariosRestriccion.AddRange(
        new HorarioRestriccion { Id = 1, DiaSemana = 1, TerminaPlaca = "[\"1\",\"2\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 2, DiaSemana = 1, TerminaPlaca = "[\"1\",\"2\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 3, DiaSemana = 2, TerminaPlaca = "[\"3\",\"4\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 4, DiaSemana = 2, TerminaPlaca = "[\"3\",\"4\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 5, DiaSemana = 3, TerminaPlaca = "[\"5\",\"6\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 6, DiaSemana = 3, TerminaPlaca = "[\"5\",\"6\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 7, DiaSemana = 4, TerminaPlaca = "[\"7\",\"8\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 8, DiaSemana = 4, TerminaPlaca = "[\"7\",\"8\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) },
        new HorarioRestriccion { Id = 9, DiaSemana = 5, TerminaPlaca = "[\"9\",\"0\"]", Inicio = new TimeSpan(6, 0, 0), Fin = new TimeSpan(9, 30, 0) },
        new HorarioRestriccion { Id = 10, DiaSemana = 5, TerminaPlaca = "[\"9\",\"0\"]", Inicio = new TimeSpan(16, 0, 0), Fin = new TimeSpan(20, 0, 0) }
    );

    context.Vehiculos.AddRange(
        new Vehiculo { Id = 1, Placa = "PBA1231", Color = "Rojo", Modelo = "Sedán", Chasis = "3VWFE21C04M000001", Kilometraje = 15000, AnioModelo = 2005, Marca = "Volkswagen" },
        new Vehiculo { Id = 2, Placa = "PBA1232", Color = "Azul", Modelo = "Coupé", Chasis = "1G1FP21S0NL100002", Kilometraje = 20000, AnioModelo = 2008, Marca = "Chevrolet" },
        new Vehiculo { Id = 3, Placa = "PBA1233", Color = "Verde", Modelo = "Hatchback", Chasis = "JHMFA16506S000003", Kilometraje = 12000, AnioModelo = 2010, Marca = "Honda" },
        new Vehiculo { Id = 4, Placa = "PBA1234", Color = "Negro", Modelo = "SUV", Chasis = "JM1GL1W55H1130004", Kilometraje = 22000, AnioModelo = 2015, Marca = "Mazda" },
        new Vehiculo { Id = 5, Placa = "PBA1235", Color = "Blanco", Modelo = "Pickup", Chasis = "1N6AD07W65C400005", Kilometraje = 25000, AnioModelo = 2018, Marca = "Nissan" },
        new Vehiculo { Id = 6, Placa = "PBA1236", Color = "Amarillo", Modelo = "Convertible", Chasis = "SAJDA42B52PA00006", Kilometraje = 13000, AnioModelo = 2003, Marca = "Jaguar" },
        new Vehiculo { Id = 7, Placa = "PBA1237", Color = "Gris", Modelo = "Furgoneta", Chasis = "YV1AS982091106007", Kilometraje = 18000, AnioModelo = 2011, Marca = "Volvo" },
        new Vehiculo { Id = 8, Placa = "PBA1238", Color = "Naranja", Modelo = "Deportivo", Chasis = "ZHWGU43T88LA00008", Kilometraje = 14000, AnioModelo = 2008, Marca = "Lamborghini" },
        new Vehiculo { Id = 9, Placa = "PBA1239", Color = "Morado", Modelo = "Berlina", Chasis = "WBAVL1C57EVY00009", Kilometraje = 16000, AnioModelo = 2014, Marca = "BMW" },
        new Vehiculo { Id = 10, Placa = "PBA1240", Color = "Cian", Modelo = "Monovolumen", Chasis = "WAUZZZ8K69A000010", Kilometraje = 17000, AnioModelo = 2009, Marca = "Audi" },
        new Vehiculo { Id = 11, Placa = "PBA1241", Color = "Magenta", Modelo = "Camioneta", Chasis = "5J8TB4H53DL000011", Kilometraje = 24000, AnioModelo = 2013, Marca = "Acura" },
        new Vehiculo { Id = 12, Placa = "PBA1242", Color = "Turquesa", Modelo = "Roadster", Chasis = "WP0CB2A97CS000012", Kilometraje = 21000, AnioModelo = 2012, Marca = "Porsche" },
        new Vehiculo { Id = 13, Placa = "PBA1243", Color = "Oro", Modelo = "Microcoche", Chasis = "3CZRE38509G700013", Kilometraje = 19000, AnioModelo = 2009, Marca = "Honda" },
        new Vehiculo { Id = 14, Placa = "PBA1244", Color = "Plata", Modelo = "Compacto", Chasis = "WAUDF78E58A000014", Kilometraje = 23000, AnioModelo = 2008, Marca = "Audi" },
        new Vehiculo { Id = 15, Placa = "PBA1245", Color = "Cobre", Modelo = "Todoterreno", Chasis = "SALTY19444A000015", Kilometraje = 26000, AnioModelo = 2004, Marca = "Land Rover" },
        new Vehiculo { Id = 16, Placa = "PBA1246", Color = "Cereza", Modelo = "Minivan", Chasis = "2HNYD284X8H000016", Kilometraje = 11000, AnioModelo = 2008, Marca = "Acura" },
        new Vehiculo { Id = 17, Placa = "PBA1247", Color = "Esmeralda", Modelo = "Económico", Chasis = "JN1CV6AP0CM000017", Kilometraje = 13000, AnioModelo = 2012, Marca = "Infiniti" },
        new Vehiculo { Id = 18, Placa = "PBA1248", Color = "Marfil", Modelo = "Luxury", Chasis = "SCBZU25E11CX000018", Kilometraje = 21000, AnioModelo = 2001, Marca = "Bentley" },
        new Vehiculo { Id = 19, Placa = "PBA1249", Color = "Azul marino", Modelo = "Utilitario", Chasis = "1GKS2MEF4CR000019", Kilometraje = 23000, AnioModelo = 2012, Marca = "GMC" },
        new Vehiculo { Id = 20, Placa = "PBA1250", Color = "Vino", Modelo = "Crossover", Chasis = "3GNDA13D76S000020", Kilometraje = 22000, AnioModelo = 2006, Marca = "Chevrolet" }
    );

    context.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
