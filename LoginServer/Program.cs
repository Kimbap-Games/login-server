using AspNetCore.Identity.CosmosDb;
using AspNetCore.Identity.CosmosDb.Containers;
using AspNetCore.Identity.CosmosDb.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using LoginServer.Models;
using LoginServer.Settings;
using LoginServer.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For cosmosDb connection
// The Cosmos connection string
var connectionString = builder.Configuration.GetConnectionString("CosmosDbConnection");

// Name of the Cosmos database to use
var cosmosIdentityDbName = builder.Configuration.GetValue<string>("CosmosIdentityDbName");

var setupCosmosDb = builder.Configuration.GetValue<string>("SetupCosmosDb");

Console.WriteLine($"CosmosIdentityDbName: {cosmosIdentityDbName}, ConnectionString: {connectionString}");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseCosmos(
        connectionString: connectionString,
        databaseName: cosmosIdentityDbName
    );
});


builder.Services.AddCosmosIdentity<ApplicationDbContext, IdentityUser, IdentityRole, string>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true; // Always a good idea :)
            // options.Password.RequireDigit = true; // 비밀번호에 숫자를 포함해야 함
            // options.Password.RequireLowercase = true; // 비밀번호에 소문자를 포함해야 함
            // options.Password.RequireUppercase = true; // 비밀번호에 대문자를 포함해야 함
            // options.Password.RequireNonAlphanumeric = true; // 비밀번호에 특수 문자를 포함해야 함
            // options.Password.RequiredLength = 8; // 비밀번호의 최소 길이는 8자
            // options.Password.RequiredUniqueChars = 1; // 비밀번호에 필요한 고유 문자 
        })
    .AddDefaultUI() // Use this if Identity Scaffolding is in use
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