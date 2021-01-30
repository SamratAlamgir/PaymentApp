using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public static class RepositoryExtensions
    {
        public static void RegisterRepositoryTypes(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PaymentDataContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("PaymentDbConnection"));
            });
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }
    }
}
