using ECommerceProject.Persistance;
using ECommerceProject.Application;
using ECommerceProject.Application.Mappings;
using ECommerceProject.Infrastructure;
using ECommerceProject.Application.Configurations;
using ECommerceProject.Infrastructure.Services;
using ECommerceProject.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using ECommerceProject.WebAPI.Middlewares;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using ECommerceProject.WebAPI.Logging;

var builder = WebApplication.CreateBuilder(args);

//------------------------------------
var masterKey = builder.Configuration["Jwt:EncryptionMasterKey"];

if (string.IsNullOrEmpty(masterKey))
{
    throw new InvalidOperationException("MasterKey bulunamadı!");
}

var tokenOptions = builder.Configuration.GetSection("CustomTokenOption").Get<CustomTokenOption>();
var encryptedConnectionString = builder.Configuration.GetConnectionString("MsSql");

if (tokenOptions == null || string.IsNullOrEmpty(encryptedConnectionString))
{
    throw new InvalidOperationException("Token ayarları veya connection string okunamadı!");
}
// şifreleri anlık olarak RAM üzerinde çözüyoruz
tokenOptions.SecurityKey = EncryptionHelper.Decrypt(tokenOptions.SecurityKey, masterKey);
var decryptedConnectionString = EncryptionHelper.Decrypt(encryptedConnectionString, masterKey);

// Çözülmüş verileri IoC Container'a (DI) kaydediyoruz.
builder.Services.AddSingleton(tokenOptions);

builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(decryptedConnectionString));
//------------------------------------

//builder.Services.AddSerilogExtension(decryptedConnectionString);
builder.Services.AddSerilogExtension(decryptedConnectionString);
Serilog.Debugging.SelfLog.Enable(Console.Error);

// Add services to the container.
builder.Services.AddPersistenceServices(decryptedConnectionString);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ProductProfile).Assembly));

// .NET'in HttpContext'e erişebilmesini sağlar.
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
