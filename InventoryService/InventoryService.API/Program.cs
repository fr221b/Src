using InventoryService.API.Features.Product;
using InventoryService.Application;
using InventoryService.Infrastructure;
using InventoryService.Infrastructure.DbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

await app.Services.InitializeInventoryDatabaseAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.MapProductEndpoints();

app.Run();
