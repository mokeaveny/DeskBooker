using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DeskBookerDBConnection");

builder.Services.AddScoped<IDbConnection>(db => new SqlConnection(connectionString));

var app = builder.Build();

app.Run();