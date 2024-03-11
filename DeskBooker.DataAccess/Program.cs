using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DeskBookerDBConnection");

builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(connectionString));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "DeskBookerDAL",
            Version = "v1"
        });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();