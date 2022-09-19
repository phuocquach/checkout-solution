using CheckoutService.Extension;
using CheckoutService.Features.Basket.Handler;
using CheckoutService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CheckoutService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            AddServiceToBuilder(builder);

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void AddServiceToBuilder(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<CheckoutDBContext>(options =>
            {
                options.UseSqlServer(connectionString, builder => { builder.MigrationsAssembly("CheckoutService"); }
                );
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(typeof(Program).Assembly, typeof(GetBasket).Assembly);
            builder.Services.RegisterServicesAsScoped(typeof(Program).Assembly, typeof(GetBasket).Assembly);
        }

       
    }
}