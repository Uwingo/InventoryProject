using Entities.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using Services.EFCore;
using System.Reflection;
using System.Text;

namespace Inventory.Extension
{
    public static class ServiceExtension
    {
        public static void ConfiguratioSQLContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Repositories")));
        }
        public static void ConfiguerRepostoryManager(this IServiceCollection services)
        {

            services.AddScoped<IRepositoryBrand, RepositoryBrand>();
            services.AddScoped<IRepositoryConsumable, RepositoryConsumable>();
            services.AddScoped<IRepositoryInventory, RepositoryInventory>();
            services.AddScoped<IRepositoryMeasurementUnit, RepositoryMeasurementUnit>();
            services.AddScoped<IRepositoryModel, RepositoryModel>();
            services.AddScoped<IRepositoryProduct, RepositoryProduct>();
            services.AddScoped<IRepositoryProductType, RepositoryProductType>();
            services.AddScoped<IRepositoryStockChange, RepositoryStockChange>();
            services.AddScoped<IRepositorySupplier, RepositorySupplier>();
            services.AddScoped<IRepositoryWarehouse, RepositoryWarehouse>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            
            services.AddScoped<IRepositoryBase<Brand>, RepositoryBase<Brand>>();
            services.AddScoped<IRepositoryBase<Consumable>, RepositoryBase<Consumable>>();
            services.AddScoped<IRepositoryBase<InventoryStock>, RepositoryBase<InventoryStock>>();
            services.AddScoped<IRepositoryBase<MeasurementUnit>, RepositoryBase<MeasurementUnit>>();
            services.AddScoped<IRepositoryBase<Model>, RepositoryBase<Model>>();
            services.AddScoped<IRepositoryBase<Product>, RepositoryBase<Product>>();
            services.AddScoped<IRepositoryBase<ProductType>, RepositoryBase<ProductType>>();
            services.AddScoped<IRepositoryBase<StockChange>, RepositoryBase<StockChange>>();
            services.AddScoped<IRepositoryBase<Supplier>, RepositoryBase<Supplier>>();
            services.AddScoped<IRepositoryBase<Warehouse>, RepositoryBase<Warehouse>>();

        }
        public static void ConfiguerServiceManager(this IServiceCollection services)
        { // service referanslar
            services.AddScoped<IServiceBrand, ServiceBrand>();
            services.AddScoped<IServiceConsumable, ServiceConsumable>();
            services.AddScoped<IServiceMeasurementUnit, ServiceMeasurementUnit>();
            services.AddScoped<IServiceModel, ServiceModel>();
            services.AddScoped<IServiceProduct, ServiceProduct>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IServiceProductType, ServiceProductType>();
            services.AddScoped<IServiceInventory, ServiceInventory>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<Services.Contracts.IAuthenticationService, Services.EFCore.AuthenticationService>();
            services.AddScoped<IServiceStockChange, ServiceStockChange>();
            services.AddScoped<IServiceSupplier, ServiceSupplier>();
            services.AddScoped<IServiceWarehouse, ServiceWarehouse>();
        }
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, Role>
                (
                    opts =>
                    {
                        opts.Password.RequireDigit = true;
                        opts.Password.RequireLowercase = true;
                        opts.Password.RequireUppercase = true;
                        opts.Password.RequireNonAlphanumeric = true;
                        opts.Password.RequiredLength = 8;

                        opts.User.RequireUniqueEmail = true;

                    }
                ).AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LoginPath = "/User/Login";
                })
                .AddJwtBearer(options =>
                {
                    var jwtSettings = configuration.GetSection("JwtSettings");
                    var secretKey = jwtSettings["SecretKey"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["ValidateIssue"],
                        ValidAudience = jwtSettings["ValidateAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });
        }


    }
}
