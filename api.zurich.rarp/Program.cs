using api.zurich.rarp.Mapper;
using AutoMapper;
using biz.zurich.rarp.Repository.Clientes;
using biz.zurich.rarp.Repository.Polizas;
using dal.zurich.rarp.DBContext;
using dal.zurich.rarp.Repository.Clientes;
using dal.zurich.rarp.Repository.Polizas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ZurichRarpContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Por esta línea:
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperProfile>());
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
            .WithOrigins("http://localhost:4200", "https://localhost", "http://localhost:8080")
                .AllowCredentials();
            }));

#region REPOSITORIES
builder.Services.AddTransient<IClientesRepository, ClientesRepository>();
builder.Services.AddTransient<IPolizasRepository, PolizasRepository>();
#endregion

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
