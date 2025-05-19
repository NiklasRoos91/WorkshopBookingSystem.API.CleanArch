using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkshopBooking.Domain.Interfaces;
using WorkshopBooking.Infrastructure.Presistence;
using WorkshopBooking.Infrastructure.Repositories;
using WorkshopBooking.Infrastructure.Services;

namespace WorkshopBooking.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WorkshopBookingDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("WorkshopBookingSystemDb"));
            });

            services.AddHttpClient<IVehicleMakeApiService, VehicleMakeApiService>();

            services.AddScoped(typeof(IGenericInterface<>), typeof(GenericRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}