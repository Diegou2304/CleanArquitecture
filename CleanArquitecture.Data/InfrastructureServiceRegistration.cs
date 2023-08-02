using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArquitecture.Infrastructure.Email;
using CleanArquitecture.Infrastructure.Persistence;
using CleanArquitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CleanArquitecture.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StreamerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionString")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<IStreamerRepository, StreamerRepository>();
            services.AddTransient<IEmailService, EmailService>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));

            

            return services;
        }
    }
}
