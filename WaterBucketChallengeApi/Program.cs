using WaterBucketChallengeApi.Interfaces;
using WaterBucketChallengeApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWaterBucketService, WaterBucketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

// Hacemos la clase Program pública para que sea accesible desde el proyecto de pruebas de integración.
public partial class Program { }
