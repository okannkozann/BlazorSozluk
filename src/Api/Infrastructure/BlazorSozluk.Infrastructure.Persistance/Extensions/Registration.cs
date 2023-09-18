using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Infrastructure.Persistance.Context;
using BlazorSozluk.Api.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSozluk.Api.Infrastructure.Persistance.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration )
        {
            services.AddDbContext<BlazorSozlukContext>(conf =>
            {

                var connStr = configuration["BlazorSozlukDbConnectionString"].ToString();
                conf.UseSqlServer(connStr, opt=>
                {
                    opt.EnableRetryOnFailure();
                });
            });


            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();

            return services;
        }
    }
}
