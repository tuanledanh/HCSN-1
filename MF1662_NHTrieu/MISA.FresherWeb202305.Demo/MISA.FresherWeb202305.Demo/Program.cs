using MISA.FresherWeb202305.Demo.Application.Interface;

using MISA.FresherWeb202305.Demo.Domain.Interface;



using MISA.FresherWeb202305.Demo.Infracture;
using MySqlConnector;
using System.Data.Common;
using MISA.FresherWeb202305.Demo.Application.Services;

using MISA.FresherWeb202305.Demo.Infracture.Repository;
using MISA.FresherWeb202305.Demo.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using MISA.FresherWeb202305.Demo.Domain.Exceptions;
using MISA.FresherWeb202305.Demo.Domain.UnitOfWork;

using MISA.FresherWeb202305.Demo.Domain.Interface.Assets;
using MISA.FresherWeb202305.Demo.Domain.Service;
using MISA.FresherWeb202305.Demo.Infracture.UnitOfWorks;
using MISA.FresherWeb202305.Demo.Middleware;
using MISA.FresherWeb202305.Demo.Domain.Interface.department;
using MISA.FresherWeb202305.Demo.Domain.Interface.AssetTypes;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Cấu hình validate input
        options.InvalidModelStateResponseFactory = (context) =>
        {
            var errors = context.ModelState.Values.SelectMany(error => error.Errors);
            var errorMsgs = string.Join(", ", errors.Select(error => error.ErrorMessage));

            return new BadRequestObjectResult(new BaseException()
            {
                ErrorCode = 400,
                UserMessage = errorMsgs,
                DevMessage = errorMsgs,
                TraceId = context.HttpContext.TraceIdentifier,
                MoreInfo = "",
                Errors = context.ModelState
            });
        };
    })
 .AddJsonOptions(options =>
 {
     // Sử dụng bộ chuyển đổi CamelCaseNamingPolicy
     options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
 });


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var connectionString = builder.Configuration.GetConnectionString("MisaAccounting");
builder.Services.AddScoped<IUnitOfWork>(provider =>new UnitOfWork(connectionString));
// add services

builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAssetManager, AssetManager>();
builder.Services.AddScoped<IAssetServices, AssetServices>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();
builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();

// Không cần đăng ký lại IAssetRepository và các dịch vụ liên quan
builder.Services.AddScoped<IAssetTypeRepository, AssetTypeRepository>();
builder.Services.AddScoped<IAssetTypeManager, AssetTypeManager>();

builder.Services.AddScoped<IAssetTypeServices, AssetTypeServices>();




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();   
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        }
        );  
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
