using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.API.ConverTime;
using MISA.WebFresher052023.API.Middleware;
using MISA.WebFresher052023.Application.Interface;
using MISA.WebFresher052023.Application.Service;
using MISA.WebFresher052023.Domain;
using MISA.WebFresher052023.Domain.Exceptions;
using MISA.WebFresher052023.Domain.Interface.department;
using MISA.WebFresher052023.Domain.Interface.FixedAsset;
using MISA.WebFresher052023.Domain.Interface.FixedAssetCategory;
using MISA.WebFresher052023.Domain.Interface.Receiver;
using MISA.WebFresher052023.Domain.Interface.TransferAsset;
using MISA.WebFresher052023.Domain.Interface.TransferAssetDetail;
using MISA.WebFresher052023.Domain.Interface.UnitOfWork;
using MISA.WebFresher052023.Domain.Service;
using MISA.WebFresher052023.Infrastructure.Repository;
using MISA.WebFresher052023.Infrastructure.UnitOfWork;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

// Chuỗi kết nối database
var connectionString = builder.Configuration["connectionString"];


// Add services to the container.
builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
{
    option.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Values.SelectMany(x => x.Errors);

        return new BadRequestObjectResult(new BaseException()
        {
            ErrorCode = 400,
            UserMessage = "Invalid data",
#if DEBUG
            DevMessage = "Invalid data",
#else
            DevMessage = "",
#endif
            TraceId = "",
            HelpLink = "",
            Errors = errors
        }.ToString() ?? "");
    };
}).AddJsonOptions(option =>
{
    option.JsonSerializerOptions.PropertyNamingPolicy = null;

    option.JsonSerializerOptions.Converters.Add(new LocalTimeZone());
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(connectionString));

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();

//

builder.Services.AddScoped<IFixedAssetCategoryRepository, FixedAssetCategoryRepository>();

builder.Services.AddScoped<IFixedAssetCategoryService, FixedAssetCategoryService>();

builder.Services.AddScoped<IFixedAssetCategoryManager, FixedAssetCategoryManager>();

//
builder.Services.AddScoped<IFixedAssetRepository, FixedAssetRepository>();

builder.Services.AddScoped<IFixedAssetService, FixedAssetService>();

builder.Services.AddScoped<IFixedAssetManager, FixedAssetManager>();


//
builder.Services.AddScoped<ITransferAssetRepository, TransferAssetRepository>();

builder.Services.AddScoped<ITransferAssetService, TransferAssetService>();

builder.Services.AddScoped<ITransferAssetManager, TransferAssetManager>();


//
builder.Services.AddScoped<ITransferAssetDetailRepository, TransferAssetDetailRepository>();
builder.Services.AddScoped<ITransferAssetDetailService, TransferAssetDetailService>();
builder.Services.AddScoped<ITransferAssetDetailManager, TransferAssetDetailManager>();

//
builder.Services.AddScoped<IReceiverRepository, ReceiverRepository>();
builder.Services.AddScoped<IReceiverService, ReceiverService>();



//Thêm localization
builder.Services.AddLocalization();
var cultureInfos = new[]
{
    new CultureInfo("vi-VN"),
    new CultureInfo("en-US"),
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = cultureInfos,
    SupportedUICultures = cultureInfos,
    ApplyCurrentCultureToResponseHeaders = true,

};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRequestLocalization(localizationOptions);

app.UseCors("AllowAll");

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
