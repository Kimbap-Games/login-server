using AspNetCore.Identity.CosmosDb;
using Microsoft.AspNetCore.Identity;

using LoginServer.Models;
using LoginServer.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For cosmosDb connection
var cosmosDbSettings = builder.Configuration.GetSection("CosmosDbSettings").Get<CosmosDbSettings>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>() 
    .AddCosmosDb(options =>
    {
        // Use ConnectionString for config
        options.ConnectionString = builder.Configuration.GetConnectionString("CosmosDbConnection");
        options.DatabaseId = cosmosDbSettings.DatabaseId;
        options.ContainerId = cosmosDbSettings.ContainerId;
        
        options.ContainerProperties = new Microsoft.Azure.Cosmos.ContainerProperties
        {
            Id = cosmosDbSettings.ContainerId,
            PartitionKeyPath = "/id",
        };
    })
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();