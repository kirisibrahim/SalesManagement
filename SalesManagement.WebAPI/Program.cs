using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesManagement.WebAPI;
using SalesManagement.DataAccess.Repositories.Implementations;
using SalesManagement.DataAccess.Repositories.Interfaces;
using SalesManagement.DataAccess.UnitOfWork;
using SalesManagement.DataAccess.Context;
using AutoMapper;
using SalesManagement.Business.Mappings;
using SalesManagement.Business.Services.Implementations;
using SalesManagement.Business.Services.Interfaces;
using FluentValidation;
using SalesManagement.Business.DTOs;
using SalesManagement.Business.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsý
builder.Services.AddDbContext<SalesManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controller'lar
builder.Services.AddControllers();

// OpenAPI (Swagger)
builder.Services.AddOpenApi();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<InvoiceValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RoleValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SaleValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<StockMovementValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SupplierValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

// Servisler
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IStockMovementService, StockMovementService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();

// Repository'ler
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// UnitOfWork
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// JWT Authentication Ayarlarý
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});


var app = builder.Build();

// Geliþtirme ortamýnda OpenAPI (Swagger)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();